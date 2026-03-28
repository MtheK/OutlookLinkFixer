# OutlookLinkFixer

OutlookLinkFixer ist ein Windows-Tool, das lokale Datei- und Ordnerpfade aus der Zwischenablage erkennt und komfortabel öffnet – speziell für Outlook Web, Outlook Desktop (alt/neu) und ähnliche Umgebungen.

## Funktionen
- Überwacht die Zwischenablage systemweit
- Zeigt ein Kontextmenü (Datei öffnen, Ordner öffnen, Pfad kopieren, Abbrechen) direkt an der Mausposition
- Erkennt Datei-/Ordnerpfade auch aus Anführungszeichen oder file://-Links (z.B. aus Outlook Web)
- Spezialbehandlung: ardianet.net-Links werden automatisch in UNC-Pfade umgewandelt
- Menü erscheint nur, wenn Outlook (klassisch/neu) oder VS Code im Vordergrund ist
- Menü schließt automatisch nach wenigen Sekunden oder per ESC
- Keine Hotkeys nötig, nur Clipboard-Listener
- Modernes, natives Windows-Design (WinForms)

## Erste Schritte
1. .NET 8 Desktop Runtime installieren (https://dotnet.microsoft.com/download/dotnet/8.0)
2. OutlookLinkFixer.exe starten
3. Das Symbol erscheint im Infobereich (Tray) neben der Uhr
4. Kopiere einen lokalen Pfad oder Link – das Menü erscheint automatisch

## Hinweise
- Funktioniert nur, wenn Outlook (klassisch, neu) oder VS Code im Vordergrund ist
- HTTP/HTTPS-Links werden ignoriert, außer ardianet.net (UNC-Mapping)
- Siehe [ARCHITEKTUR.md](OutlookLinkFixer/ARCHITEKTUR.md) für technische Details
- Entwicklerinfos und Build-Anleitung: siehe [DEVNOTES.md](DEVNOTES.md)

## Beispiele für erkannte Pfade und Links

Folgende Formate werden erkannt und verarbeitet:

**Pfade mit eckigen Klammern:**
- `[T:\Projekte\Ordner mit Leerzeichen\Datei [2026].pdf]`

**Direkte Windows-Pfade:**
- `T:\Projekte\MeinOrdner\Datei.txt`
- `\\server\share\Ordner\Datei.docx`

**Pfade in Anführungszeichen:**
- "T:\Projekte\Mein Ordner\Datei mit Leerzeichen.txt"

**file://-Links (z.B. aus Outlook Web):**
- `file:///T:/Projekte/MeinOrdner/Datei.txt`
- `file://server/share/Ordner/Datei.docx`

**Spezialfall ardianet.net (UNC-Mapping):**
- `https://ardianet.net/Ordner/Datei%20mit%20Leerzeichen.txt` → wird zu `\\ardianet.net\Ordner\Datei mit Leerzeichen.txt`

**Pfade in Fließtext:**
- Bitte öffnen Sie "T:\Projekte\Test\Doku.pdf" oder den Ordner `\\server\share\Daten`.

**Nicht unterstützte Links:**
- HTTP/HTTPS-Links (außer ardianet.net) werden ignoriert

**Hinweis:**
Wenn eine Datei nicht existiert, aber der Ordner, erscheint das Menü für den Ordner.