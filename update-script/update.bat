@echo off
REM Bootstrapper: Holt immer die neueste update.ps1 aus dem Main-Branch von GitHub und führt sie aus
setlocal
set SCRIPT_DIR=%~dp0
set PS1_URL=https://raw.githubusercontent.com/MtheK/OutlookLinkFixer/main/update-script/update.ps1
set PS1_PATH=%SCRIPT_DIR%update.ps1

echo [Bootstrap] Lade neueste update.ps1 von GitHub ...
REM Versuche mit PowerShell zu laden
powershell -NoProfile -Command "try { Invoke-WebRequest -Uri '%PS1_URL%' -OutFile '%PS1_PATH%' -UseBasicParsing } catch { Write-Host 'Download fehlgeschlagen!'; exit 1 }"
if exist "%PS1_PATH%" (
	echo [Bootstrap] Starte update.ps1 ...
	start "Updater" powershell -NoProfile -ExecutionPolicy Bypass -File "%PS1_PATH%"
) else (
	echo [Bootstrap] update.ps1 konnte nicht geladen werden!
    pause
)
REM Skript endet hier, damit update.bat überschrieben werden kann