# OutlookLinkFixer

Ein Windows-Tool, das lokale Datei- und Ordnerpfade aus der Zwischenablage erkennt und komfortabel öffnet – speziell für Outlook Web, Outlook Desktop (alt/neu) und ähnliche Umgebungen.

- Überwacht die Zwischenablage systemweit
- Zeigt ein natives Kontextmenü (Datei öffnen, Ordner öffnen, Abbrechen/Pfad kopieren) an der Mausposition
- Spezialbehandlung: Web-Links auf ardianet.net werden automatisch in UNC-Netzwerkpfade umgewandelt (inkl. %20 → Leerzeichen)
- Erkennt und extrahiert Datei-/Ordnerpfade auch aus Anführungszeichen im Fließtext
- Erkennt file://-Fragmente in beliebigen Links (z.B. Outlook Web)
- Menü erscheint auch, wenn die Datei nicht existiert, aber der Ordner vorhanden ist
- Menü erscheint nur, wenn Outlook (alt/neu) oder VS Code im Vordergrund ist (Fenster-/Prozessprüfung)
- Menü schließt automatisch nach 3 Sekunden oder per ESC
- Endlosschleifen durch Clipboard-Änderung werden verhindert
- Moderne, native Optik (WinForms, ContextMenuStrip)

## Projektstruktur
- OutlookLinkFixer/OutlookLinkFixer.csproj – Hauptprojekt (.NET 8, Windows Forms)
- Program.cs – Hauptlogik, Tray, Menü, Clipboard, Fenstererkennung
- ClipboardPathParser.cs – Pfad-Parsing, UNC-Mapping, Spezialfälle
- ARCHITEKTUR.md – Architektur- und Entscheidungsnotizen

## Build & Run
- .NET 8.0 SDK erforderlich (Build)
- Zielsystem benötigt .NET 8 Runtime (mindestens WindowsDesktop.App 8.x)
- Build: `dotnet build OutlookLinkFixer/OutlookLinkFixer.csproj`
- Run:  `dotnet run --project OutlookLinkFixer/OutlookLinkFixer.csproj`

## Hinweise & Limitierungen
- Funktioniert nur, wenn Outlook (klassisch, neu) oder VS Code im Vordergrund ist
- HTTP/HTTPS-Links werden ignoriert, außer ardianet.net (UNC-Mapping)
- Keine Hotkey-Logik mehr, nur noch Clipboard-Listener
- Pfaderkennung und Parsing-Details siehe [ARCHITEKTUR.md](OutlookLinkFixer/ARCHITEKTUR.md)
- Projekt wurde aus "cs_test" umbenannt und modernisiert
