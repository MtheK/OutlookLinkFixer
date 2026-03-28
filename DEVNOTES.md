# OutlookLinkFixer – DevNotes

## Architektur & Technik
- Siehe [ARCHITEKTUR.md](OutlookLinkFixer/ARCHITEKTUR.md) für Parsing-Logik, Menüverhalten, Tray-Integration, Clipboard-Handling.
- Hauptlogik: [Program.cs](OutlookLinkFixer/Program.cs)
- Pfad-Parsing: [ClipboardPathParser.cs](OutlookLinkFixer/ClipboardPathParser.cs)
- Settings/Autostart: [SettingsForm.cs], [AppSettings.cs]

## Build & Deployment
- .NET 8.0 SDK erforderlich
- Build: `dotnet build OutlookLinkFixer/OutlookLinkFixer.csproj`
- Publish: `dotnet publish -c Release -r win-x64` (SingleFile, nicht self-contained)
- Für reines Single-File ohne DLLs: `<PublishSingleFile>true</PublishSingleFile>`, `<SelfContained>true</SelfContained>` (macht EXE groß)
- Für Tray- und Programm-Icon muss icon.ico als Datei im Projekt liegen (siehe csproj)
- settings.json wird im Ausgabeverzeichnis gespeichert

## Hinweise für Entwickler
- Tray-Icon ist per BalloonTip und Hinweis beim ersten Start sichtbar
- Kontextmenü erscheint nur, wenn Outlook (klassisch/neu) oder VS Code im Vordergrund ist
- Keine Hotkeys, nur Clipboard-Listener
- Endlosschleifen durch Clipboard-Änderung werden verhindert
- Siehe [copilot-instructions.md](.github/copilot-instructions.md) für Agenten/AI-Workflow

## ToDos & Ideen
- (Hier können Entwickler offene Punkte, Bugs, Ideen ergänzen)
