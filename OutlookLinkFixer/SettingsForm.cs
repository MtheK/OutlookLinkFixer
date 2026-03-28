using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OutlookLinkFixer
{
    public partial class SettingsForm : Form
    {
        private AppSettings settings;
        public SettingsForm(AppSettings current)
        {
            if (Environment.OSVersion.Version.Major >= 6)
                SetHighDpiMode();
            InitializeComponent();
            settings = current;
            numericTimeout.Value = Math.Max(2, Math.Min(20, settings.TimeoutSeconds));
            textPrograms.Text = settings.ProgramList;
            radioExclude.Checked = settings.ExcludeMode;
            radioInclude.Checked = !settings.ExcludeMode;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SetHighDpiMode()
        {
            try
            {
                typeof(Application).GetMethod("SetHighDpiMode")?.Invoke(null, new object[] { (int)2 }); // PerMonitorV2
            }
            catch { }
        }

        private void btnAutostart_Click(object sender, EventArgs e)
        {
            try
            {
                string exePath = Application.ExecutablePath;
                string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "OutlookLinkFixer.lnk");
                var shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
                var shortcut = shell.GetType().InvokeMember("CreateShortcut", System.Reflection.BindingFlags.InvokeMethod, null, shell, new object[] { shortcutPath });
                shortcut.GetType().InvokeMember("TargetPath", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { exePath });
                shortcut.GetType().InvokeMember("WorkingDirectory", System.Reflection.BindingFlags.SetProperty, null, shortcut, new object[] { Path.GetDirectoryName(exePath) });
                shortcut.GetType().InvokeMember("Save", System.Reflection.BindingFlags.InvokeMethod, null, shortcut, null);
                Process.Start("explorer.exe", Environment.GetFolderPath(Environment.SpecialFolder.Startup));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Hinzufügen zu Autostart: " + ex.Message);
            }
        }

        private void btnOpenProgramFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", AppDomain.CurrentDomain.BaseDirectory);
        }

        private void linkRepo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/MtheK/OutlookLinkFixer") { UseShellExecute = true });
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            settings.TimeoutSeconds = (int)numericTimeout.Value;
            settings.ProgramList = textPrograms.Text.Trim();
            settings.ExcludeMode = radioExclude.Checked;
            settings.Save();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
