# OutlookLinkFixer Auto-Update Script
# Legt dieses Skript in "update-script" ab. Es sucht die EXE eine Ebene höher.

$ErrorActionPreference = 'Stop'


# --- Einstellungen ---
$exeName = "OutlookLinkFixer.exe"
$baseDir = $PSScriptRoot  # Jetzt: Skriptverzeichnis
$exePath = Join-Path $baseDir $exeName
$repo = "MtheK/OutlookLinkFixer"
$latestReleaseUrl = "https://github.com/$repo/releases/latest"

Write-Host "[Updater] Suche EXE unter: $exePath"

# --- Lokale Version ermitteln ---
$localVersion = $null
if (Test-Path $exePath) {
    $verObj = (Get-Item $exePath).VersionInfo.ProductVersion
    if ($verObj) {
        # Nur den Teil vor + oder - nehmen (SemVer: 0.2.0+build, 0.2.0-beta)
        $mainVer = ($verObj -split '[\+-]')[0]
        # Nur die ersten 3 Stellen nehmen
        $localVersion = ($mainVer -split '\.')[0..2] -join '.'
        Write-Host "[Updater] Lokale Version: $localVersion"
    }
} else {
    Write-Host "[Updater] EXE nicht gefunden, Update wird erzwungen."
}



# --- Neueste Version und ZIP-URL direkt aus GitHub-API holen ---
$apiUrl = "https://api.github.com/repos/$repo/releases/latest"
Write-Host "[Updater] Lade Release-Info von $apiUrl ..."
$releaseInfo = Invoke-RestMethod -Uri $apiUrl -Headers @{ 'User-Agent' = 'UpdaterScript' }
$tag = $releaseInfo.tag_name
$remoteVersion = $tag.TrimStart('v')
Write-Host "[Updater] Neueste Version: $remoteVersion"
$assetName = "OutlookLinkFixer_v$remoteVersion.zip"

# --- Vergleich ---
$needsUpdate = $false
if (-not $localVersion) {
    $needsUpdate = $true
    Write-Host "[Updater] Kein lokales Programm, Update erforderlich."
} elseif ($localVersion -ne $remoteVersion) {
    Write-Host "[Updater] Version unterschiedlich ($localVersion < $remoteVersion), Update erforderlich."
    $needsUpdate = $true
} else {
    Write-Host "[Updater] Bereits aktuell. Kein Update nötig."
}

if (-not $needsUpdate) {
    exit 0
}

# --- Download ZIP ---
$asset = $releaseInfo.assets | Where-Object { $_.name -eq $assetName }
if (-not $asset) {
    Write-Host "[Updater] Release-Asset $assetName nicht gefunden!"
    exit 1
}
$zipUrl = $asset.browser_download_url
$tmpZip = Join-Path $env:TEMP "OutlookLinkFixer_update.zip"
Write-Host "[Updater] Lade ZIP von $zipUrl ..."
Invoke-WebRequest -Uri $zipUrl -OutFile $tmpZip

# --- OutlookLinkFixer beenden ---
Write-Host "[Updater] Beende laufende OutlookLinkFixer.exe ..."
Stop-Process -Name "OutlookLinkFixer" -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2


# --- Entpacken ---
Write-Host "[Updater] Entpacke ZIP nach $baseDir ..."
Expand-Archive -Path $tmpZip -DestinationPath $baseDir -Force
Remove-Item $tmpZip -Force

# --- Starten ---
Write-Host "[Updater] Starte OutlookLinkFixer.exe ..."
Start-Process $exePath

Write-Host "[Updater] Update abgeschlossen."
exit 0
