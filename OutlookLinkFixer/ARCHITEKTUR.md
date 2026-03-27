# OutlookLinkFixer – Entscheidungs- und Architekturnotizen

- Popup für Datei/Ordner-Aktionen wird als ContextMenuStrip (wie Tray-Menü) angezeigt, nicht mehr als eigenes Form.
- Menü enthält: "Datei öffnen", "Ordner öffnen", "Abbrechen / Pfad kopieren" (kopiert Pfad in die Zwischenablage, öffnet aber nichts).
- Menü schließt automatisch nach 3 Sekunden oder per ESC.
- Endlosschleife durch Clipboard-Änderung wird verhindert: Nach "Abbrechen / Pfad kopieren" wird das Popup beim nächsten ClipboardUpdate unterdrückt, wenn der Text identisch ist.
- Hotkey- und SendKeys-Logik wurde entfernt, nur noch Clipboard-Listener.
- Path-Parsing ist ausgelagert in ClipboardPathParser.cs und unterstützt:
	- UNC, file://, http://, eckige Klammern
	- Spezialfall: ardianet.net-Links werden zu UNC-Pfaden gemappt (inkl. %20 → Leerzeichen)
	- Pfade in Anführungszeichen im Fließtext werden extrahiert und erkannt (z.B. "T:\...\file.txt")
	- file://-Fragmente werden aus beliebigen Links extrahiert und geparst (z.B. Outlook Web)
	- Wenn eine Datei nicht existiert, aber der Ordner, wird das Menü für den Ordner angezeigt
- Menü erscheint nur, wenn Outlook (klassisch, neu) oder VS Code im Vordergrund ist (Fenster-/Prozessprüfung)
- HTTP/HTTPS-Links werden ignoriert, außer ardianet.net
- Tray-Icon bleibt mit ContextMenuStrip für Info/Beenden.
- Ziel: Möglichst native, moderne Optik und Verhalten (Win11-like) mit WinForms-Mitteln.
- Für User-Features siehe README.md, für Build/Run siehe copilot-instructions.md
- Projekt wurde aus "cs_test" umbenannt und modernisiert (März 2026)
