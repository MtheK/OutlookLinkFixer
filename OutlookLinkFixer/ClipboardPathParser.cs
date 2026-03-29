using System;

// Helper for parsing clipboard text to a file or directory path
public static class ClipboardPathParser
{
    public static string Parse(string text)
    {


        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        string path = text.Trim();

        // NEU: Wenn ein Text in Anführungszeichen steht, extrahiere den ersten in Anführungszeichen stehenden Teil
        int quoteStart = path.IndexOf('"');
        if (quoteStart >= 0)
        {
            int quoteEnd = path.IndexOf('"', quoteStart + 1);
            if (quoteEnd > quoteStart)
            {
                path = path.Substring(quoteStart + 1, quoteEnd - quoteStart - 1).Trim();
            }
        }

        // Spezialfall: https://outlook.office.com/local/path/file://Ardianet.net/de/3_SA/...
        if (path.StartsWith("https://outlook.office.com/local/path/file://Ardianet.net/", StringComparison.OrdinalIgnoreCase))
        {
            string rest = path.Substring("https://outlook.office.com/local/path/file://Ardianet.net/".Length);
            rest = Uri.UnescapeDataString(rest.Replace('/', '\\'));
            return "\\\\ardianet.net\\" + rest;
        }

        // Spezialfall: ardianet.net-Links als UNC-Pfad umwandeln
        if (path.StartsWith("http://ardianet.net/", StringComparison.OrdinalIgnoreCase))
        {
            string rest = path.Substring("http://ardianet.net/".Length);
            rest = Uri.UnescapeDataString(rest.Replace('/', '\\'));
            return "\\\\ardianet.net\\" + rest;
        }
        
        // Falls eckige Klammern vorkommen, nur Inhalt in erster Klammer verwenden
        // TODO: führt dazu dass Dateien mit eckigen Klammern im Namen nicht korrekt erkannt werden, z.B. "C:\Test\[Datei].txt" -> "Datei].txt"
        int open = path.IndexOf('[');
        int close = path.IndexOf(']', open + 1);
        if (open >= 0 && close > open)
        {
            path = path.Substring(open + 1, close - open - 1).Trim();
        }

        // Remove file:// or file:/// prefix if present
        if (path.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
        {
            // UNC-Pfad: file://\\server\share
            if (path.StartsWith("file://\\", StringComparison.OrdinalIgnoreCase))
            {
                path = path.Substring(7); // nach file://
            }
            // Lokaler Pfad: file://C:/...
            else if (path.Length > 7 && path[7] == ':')
            {
                path = path.Substring(7);
            }
            // Sonst: file:///C:/...
            else if (path.StartsWith("file:///", StringComparison.OrdinalIgnoreCase))
            {
                path = path.Substring(8);
            }
        }

        // Sonderfall: http(s) vorangestellt (z.B. http://// oder http://\\)
        if (path.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || path.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            // Entferne http(s)://
            int idx = path.IndexOf("://", StringComparison.OrdinalIgnoreCase);
            if (idx > 0)
            {
                path = path.Substring(idx + 3);
            }
            // Immer als UNC behandeln: doppelten Backslash voranstellen, falls nicht vorhanden
            if (!path.StartsWith("\\"))
            {
                path = "\\" + path.TrimStart('/');
            }
        }

        // Netzlaufwerk: beginne mit doppeltem Backslash (nur wenn schon Backslashes vorhanden)
        // Sonst keine Manipulation

        // Convert forward slashes to backslashes
        path = path.Replace("/", "\\");

        return path;
    }
}
