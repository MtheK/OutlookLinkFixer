using System.Linq;
using System.Text;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using OutlookLinkFixer;

public class HotkeyContext : ApplicationContext
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    private static bool IsOutlookOrVSCodeActive()
    {
        IntPtr hwnd = GetForegroundWindow();
        if (hwnd == IntPtr.Zero) return false;
        GetWindowThreadProcessId(hwnd, out uint pid);
        try
        {
            var proc = Process.GetProcessById((int)pid);
            string name = proc.ProcessName.ToLowerInvariant();
            // Outlook klassisch: OUTLOOK, neues Outlook: olk.exe oder outlook.exe, VS Code: Code
            return name.Contains("outlook") || name.Contains("olk") || name.Contains("code");
        }
        catch { return false; }
    }

    private NotifyIcon trayIcon;
    private ContextMenuStrip trayMenu;

    private string lastClipboardText = null;
    private bool suppressNextPopup = false;
    private AppSettings settings;
    public HotkeyContext()
    {
        // Settings laden
        settings = AppSettings.Load();

        // Clipboard listener
        ClipboardNotification.ClipboardUpdate += OnClipboardUpdate;

        // Tray-Icon und Menü
        trayMenu = new ContextMenuStrip();
        trayMenu.Items.Add("Einstellungen...", null, (s, e) => {
            using (var dlg = new SettingsForm(settings))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    settings = AppSettings.Load();
                }
            }
        });
        //trayMenu.Items.Add("Info", null, (s, e) => MessageBox.Show("Lokale Links Öffner\nKopieren eines Pfads zeigt Popup", "Info"));
        trayMenu.Items.Add("Beenden", null, (s, e) => ExitThread());

        trayIcon = new NotifyIcon()
        {
            Icon = SystemIcons.Application,
            ContextMenuStrip = trayMenu,
            Text = "OutlookLinkFixer",
            Visible = true
        };
        trayIcon.MouseDoubleClick += TrayIcon_MouseDoubleClick;
    }

    private void TrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            using (var dlg = new SettingsForm(settings))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    settings = AppSettings.Load();
                }
            }
        }
    }

    private void OnClipboardUpdate(object sender, EventArgs e)
    {
        try
        {
            if (!IsSelectedProgramActive())
            {
                return;
            }

            string text = Clipboard.GetText();
            if (suppressNextPopup)
            {
                // Popup unterdrücken, aber suppressNextPopup erst zurücksetzen, wenn sich der Clipboard-Inhalt geändert hat
                if (lastClipboardText == text)
                {
                    return;
                }
                else
                {
                    suppressNextPopup = false;
                }
            }
            //if (lastClipboardText == text)
            //    return;


            suppressNextPopup = false;

            string path = ClipboardPathParser.Parse(text);
            lastClipboardText = path;

            string menuTarget = null;
            bool fileExists = false;
            bool dirExists = false;
            if (!string.IsNullOrEmpty(path))
            {
                fileExists = System.IO.File.Exists(path);
                dirExists = System.IO.Directory.Exists(path);
                if (fileExists || dirExists)
                {
                    menuTarget = path;
                }
                else
                {
                    // Wenn Datei nicht existiert, aber der Ordner existiert, Menü für Ordner anzeigen
                    string folder = System.IO.Path.GetDirectoryName(path);
                    if (!string.IsNullOrEmpty(folder) && System.IO.Directory.Exists(folder))
                    {
                        menuTarget = folder;
                        dirExists = true;
                        fileExists = false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(menuTarget))
            {
                var menu = new ContextMenuStrip();
                var itemFile = new ToolStripMenuItem("Datei öffnen");
                var itemFolder = new ToolStripMenuItem("Ordner öffnen");
                var itemCopy = new ToolStripMenuItem("Pfad kopieren");
                var itemCancel = new ToolStripMenuItem($"Abbrechen ({settings.TimeoutSeconds})");
                itemFile.Click += (s, e2) =>
                {
                    try { Process.Start(new ProcessStartInfo(path) { UseShellExecute = true }); } catch (Exception ex) { MessageBox.Show("Fehler beim Öffnen der Datei: " + ex.Message); }
                    menu.Close();
                };
                itemFolder.Click += (s, e2) =>
                {
                    try
                    {
                        string folder = dirExists ? menuTarget : System.IO.Path.GetDirectoryName(menuTarget);
                        if (!string.IsNullOrEmpty(folder))
                            Process.Start(new ProcessStartInfo("explorer.exe", folder));
                        else
                            MessageBox.Show("Ordnerpfad konnte nicht ermittelt werden.");
                    }
                    catch (Exception ex) { MessageBox.Show("Fehler beim Öffnen des Ordners: " + ex.Message); }
                    menu.Close();
                };
                itemCopy.Click += (s, e2) =>
                {
                    try {
                        suppressNextPopup = true;
                        Clipboard.SetText(path);
                    } catch { }
                    menu.Close();
                };
                itemCancel.Click += (s, e2) =>
                {
                    suppressNextPopup = true;
                    menu.Close();
                };
                if (dirExists) itemFile.Enabled = false;
                menu.Items.Add(itemFile);
                menu.Items.Add(itemFolder);
                menu.Items.Add(new ToolStripSeparator());
                menu.Items.Add(itemCopy);
                menu.Items.Add(itemCancel);

                // ESC schließt das Menü
                menu.PreviewKeyDown += (s, e2) =>
                {
                    if (e2.KeyCode == Keys.Escape) menu.Close();
                };
                menu.KeyDown += (s, e2) =>
                {
                    if (e2.KeyCode == Keys.Escape) menu.Close();
                };

                // Countdown-Anzeige für "Abbrechen"
                int countdown = Math.Max(2, Math.Min(20, settings.TimeoutSeconds));
                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 1000;
                timer.Tick += (s, e2) =>
                {
                    countdown--;
                    if (countdown > 0)
                    {
                        itemCancel.Text = $"Abbrechen ({countdown})";
                    }
                    else
                    {
                        timer.Stop();
                        menu.Close();
                    }
                };
                menu.Opening += (s, e2) => timer.Start();
                menu.Closed += (s, e2) => timer.Stop();

                // Menü an Mausposition anzeigen
                menu.Show(Cursor.Position);
                // Fokus für ESC
                menu.Focus();
            }
        }
        catch { /* ignore clipboard errors */ }
    }

    private bool IsSelectedProgramActive()
    {
        // Programme prüfen
        string[] progs = (settings.ProgramList ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => p.Trim().ToLowerInvariant()).Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
        bool isActive = false;
        IntPtr hwnd = GetForegroundWindow();
        if (hwnd != IntPtr.Zero)
        {
            GetWindowThreadProcessId(hwnd, out uint pid);
            try
            {
                var proc = Process.GetProcessById((int)pid);
                string name = proc.ProcessName.ToLowerInvariant();
                if (settings.ExcludeMode)
                    isActive = !progs.Any(p => name.Contains(p));
                else
                    isActive = progs.Any(p => name.Contains(p));
            }
            catch { }
        }
        if (!isActive)
            return false;
        return true;
    }

    // Clipboard notification helper
    public static class ClipboardNotification
{
    public static event EventHandler ClipboardUpdate;
    private static NotificationForm form = new NotificationForm();

    private class NotificationForm : Form
    {
        public NotificationForm()
        {
            NativeMethods.SetParent(Handle, NativeMethods.HWND_MESSAGE);
            NativeMethods.AddClipboardFormatListener(Handle);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE)
                ClipboardUpdate?.Invoke(null, EventArgs.Empty);
            base.WndProc(ref m);
        }
    }
    private static class NativeMethods
    {
        public const int WM_CLIPBOARDUPDATE = 0x031D;
        public static IntPtr HWND_MESSAGE = new IntPtr(-3);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}

    protected override void ExitThreadCore()
    {
        trayIcon.Visible = false;
        base.ExitThreadCore();
    }
}



static class Program
{
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        bool createdNew;
        using (var mutex = new System.Threading.Mutex(true, "OutlookLinkFixer_Mutex", out createdNew))
        {
            if (!createdNew)
            {
                MessageBox.Show("OutlookLinkFixer läuft bereits.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HotkeyContext());
        }
    }
}
