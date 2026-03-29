@echo off
REM Bootstrapper: Holt immer die neueste update.ps1 aus dem Main-Branch von GitHub und führt sie aus
setlocal
set SCRIPT_DIR=%~dp0
set PS1_URL=https://raw.githubusercontent.com/MtheK/OutlookLinkFixer/main/update-script/update.ps1
set PS1_PATH=%SCRIPT_DIR%update.ps1

echo [Bootstrap] Lade neueste update.ps1 von GitHub ...
REM Alte update.ps1 löschen, damit garantiert neu geladen wird
if exist "%PS1_PATH%" del /f /q "%PS1_PATH%"
REM Versuche mit PowerShell zu laden, Cache-Control und Pragma Header setzen
powershell -NoProfile -Command "try { Invoke-WebRequest -Uri '%PS1_URL%' -OutFile '%PS1_PATH%' -Headers @{ 'Cache-Control' = 'no-cache'; 'Pragma' = 'no-cache' } -UseBasicParsing } catch { Write-Host 'Download fehlgeschlagen!'; exit 1 }"
if exist "%PS1_PATH%" (
	echo [Bootstrap] Starte update.ps1 ...
	start "Updater" powershell -NoProfile -ExecutionPolicy Bypass -File "%PS1_PATH%"
) else (
	echo [Bootstrap] update.ps1 konnte nicht geladen werden!
)
REM Skript endet hier, damit update.bat überschrieben werden kann