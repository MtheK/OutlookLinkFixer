# OutlookLinkFixer – Copilot Workspace Instructions

## Purpose
This file provides guidance for AI agents (e.g., GitHub Copilot) working in this repository. It summarizes key conventions, build/run instructions, and architectural decisions. **Link, don’t embed**: For details, see [README.md](../README.md) and [ARCHITEKTUR.md](../OutlookLinkFixer/ARCHITEKTUR.md).

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

---

## Documentation
- [README.md](../README.md): Features, build/run, limitations
- [ARCHITEKTUR.md](../OutlookLinkFixer/ARCHITEKTUR.md): Architecture, design decisions

---

## When in doubt
- **Link to documentation** above rather than duplicating details
- If a convention or pitfall is not covered, prefer modern, idiomatic C#/.NET 8 and WinForms practices
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
