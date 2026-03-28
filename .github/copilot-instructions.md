# OutlookLinkFixer – Copilot Workspace Instructions

## Purpose
This file provides guidance for AI agents (e.g., GitHub Copilot) working in this repository. It summarizes key conventions, build/run instructions, and architectural decisions. **Link, don’t embed**: For user-facing info, see [README.md](../README.md); for developer/build/architecture, see [DEVNOTES.md](../DEVNOTES.md) and [ARCHITEKTUR.md](../OutlookLinkFixer/ARCHITEKTUR.md).

---

## Build & Run
- **Build:** `dotnet build OutlookLinkFixer/OutlookLinkFixer.csproj`
- **Run:**  `dotnet run --project OutlookLinkFixer/OutlookLinkFixer.csproj`
- **Requirements:** .NET 8.0 SDK (build), .NET 8 Runtime (WindowsDesktop.App 8.x) on target system

See [README.md](../README.md#build--run) for more.

---

## Project Structure
- Main code: [OutlookLinkFixer/](../OutlookLinkFixer/)
  - [Program.cs](../OutlookLinkFixer/Program.cs): Main logic (tray, menu, clipboard, window detection)
  - [ClipboardPathParser.cs](../OutlookLinkFixer/ClipboardPathParser.cs): Path parsing, UNC mapping, special cases
  - [ARCHITEKTUR.md](../OutlookLinkFixer/ARCHITEKTUR.md): Architecture/decision notes

---

## Key Conventions & Decisions
- **Clipboard monitoring only** (no hotkeys)
- **Context menu** shown at mouse position, only if Outlook (classic/new) or VS Code is foreground
- **Special handling** for ardianet.net links (mapped to UNC paths)
- **Modern WinForms UI** (ContextMenuStrip, Win11-like)
- **No endless clipboard loops** (see ARCHITEKTUR.md)
- **Advanced path recognition:**
  - Recognizes file paths in quotation marks within text (e.g. "T:\...\file.txt").
  - Extracts and parses file:// fragments from any link (e.g. Outlook Web links).
  - If a file does not exist, but the folder does, the menu is shown for the folder.
  - See [ClipboardPathParser.cs](../OutlookLinkFixer/ClipboardPathParser.cs) and [Program.cs](../OutlookLinkFixer/Program.cs) for details.

---

## Documentation
- [README.md](../README.md): User-facing features, build/run, limitations
- [ARCHITEKTUR.md](../OutlookLinkFixer/ARCHITEKTUR.md): Architecture, design decisions, parsing logic

---

## When in doubt
- **Link to documentation** above rather than duplicating details
- Avoid duplicating feature/architecture explanations—keep user-facing info in README, technische Details in ARCHITEKTUR.md, and reference both here.
- For new features, update this file and [ARCHITEKTUR.md](../OutlookLinkFixer/ARCHITEKTUR.md) as needed

---

## Example Prompts
- "How do I build and run OutlookLinkFixer?"
- "What is the architecture for clipboard monitoring?"
- "How are ardianet.net links handled?"
- "Where is the main menu logic implemented?"

---

## Next steps
- Consider agent customizations for WinForms UI patterns, clipboard handling, or process/window detection.
- To create: `/create-instruction winforms-ui-patterns` or `/create-skill clipboard-parsing`.
