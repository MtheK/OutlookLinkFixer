# Changelog

## [v0.2.1] – 2026-03-29
### Added
- Automatischer Update-Mechanismus (update.bat lädt immer aktuelle update.ps1 von GitHub)
- Publish erzeugt automatisch ZIP-Archiv des Release-Ordners
- settings.json und OutlookLinkFixer.pdb werden beim Publish entfernt

### Changed
- Verbesserte Caching-Vermeidung beim Update (Cache-Control/Pragma, Löschen vor Download)
- update.ps1 bleibt nach Ausführung offen für Status/Fehleranzeige

### Fixed
- Diverse Bugfixes und Robustheit bei Parsing und Menü

## [v0.2.0] – 2026-03-28
### Added
- Autostart-Abfrage beim ersten Start (mit Hinweis auf spätere Änderung in den Einstellungen)
- BalloonTip und Hinweis beim ersten Start für bessere Tray-Sichtbarkeit
- Unterstützung für Pfade mit eckigen Klammern in der Erkennung
- DEVNOTES.md für Entwickler, README.md für Anwender, klare Doku-Trennung
- Beispielsektion in README für alle unterstützten Formate

### Fixed
- Diverse Build- und Kompilierfehler
- Verbesserte Fehlerbehandlung bei Autostart-Einrichtung

### Changed
- Tray-Icon wird per BalloonTip hervorgehoben
- Menü- und Parsing-Logik weiter verbessert

----

Siehe DEVNOTES.md und ARCHITEKTUR.md für technische Details und Entwicklerhinweise.
