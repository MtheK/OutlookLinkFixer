namespace OutlookLinkFixer
{
    partial class SettingsForm : System.Windows.Forms.Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.NumericUpDown numericTimeout;
        private System.Windows.Forms.TextBox textPrograms;
        private System.Windows.Forms.RadioButton radioExclude;
        private System.Windows.Forms.RadioButton radioInclude;
        private System.Windows.Forms.Button btnAutostart;
        private System.Windows.Forms.Button btnOpenProgramFolder;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label labelTimeout;
        private System.Windows.Forms.Label labelPrograms;
        private System.Windows.Forms.GroupBox groupMode;
        private System.Windows.Forms.LinkLabel linkRepo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.numericTimeout = new System.Windows.Forms.NumericUpDown();
            this.textPrograms = new System.Windows.Forms.TextBox();
            this.radioExclude = new System.Windows.Forms.RadioButton();
            this.radioInclude = new System.Windows.Forms.RadioButton();
            this.btnAutostart = new System.Windows.Forms.Button();
            this.btnOpenProgramFolder = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.labelTimeout = new System.Windows.Forms.Label();
            this.labelPrograms = new System.Windows.Forms.Label();
            this.groupMode = new System.Windows.Forms.GroupBox();
            this.linkRepo = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).BeginInit();
            this.groupMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericTimeout
            // 
            this.numericTimeout.Location = new System.Drawing.Point(140, 20);
            this.numericTimeout.Minimum = 2;
            this.numericTimeout.Maximum = 20;
            this.numericTimeout.Value = 3;
            this.numericTimeout.Name = "numericTimeout";
            this.numericTimeout.Size = new System.Drawing.Size(60, 23);
            // 
            // labelTimeout
            // 
            this.labelTimeout.Text = "Timeout (Sekunden):";
            this.labelTimeout.Location = new System.Drawing.Point(10, 22);
            this.labelTimeout.Size = new System.Drawing.Size(130, 15);
            // 
            // groupMode
            // 
            this.groupMode.Controls.Add(this.radioExclude);
            this.groupMode.Controls.Add(this.radioInclude);
            this.groupMode.Controls.Add(this.labelPrograms);
            this.groupMode.Controls.Add(this.textPrograms);
            this.groupMode.Location = new System.Drawing.Point(10, 55);
            this.groupMode.Size = new System.Drawing.Size(290, 100);
            this.groupMode.Text = "Popup-Anzeige-Modus";
            // 
            // radioExclude
            // 
            this.radioExclude.Text = "Alle, außer...";
            this.radioExclude.Location = new System.Drawing.Point(10, 18);
            this.radioExclude.Size = new System.Drawing.Size(100, 17);
            // 
            // radioInclude
            // 
            this.radioInclude.Text = "Nur...";
            this.radioInclude.Location = new System.Drawing.Point(120, 18);
            this.radioInclude.Size = new System.Drawing.Size(100, 17);
            this.radioInclude.Checked = true;
            // 
            // labelPrograms
            // 
            this.labelPrograms.Text = "Programmliste (kommagetrennt):";
            this.labelPrograms.Location = new System.Drawing.Point(10, 45);
            this.labelPrograms.Size = new System.Drawing.Size(200, 15);
            // 
            // textPrograms
            // 
            this.textPrograms.Location = new System.Drawing.Point(10, 65);
            this.textPrograms.Size = new System.Drawing.Size(250, 23);
            // 
            // btnAutostart
            // 
            this.btnAutostart.Text = "Zu Autostart hinzufügen";
            this.btnAutostart.Location = new System.Drawing.Point(10, 170);
            this.btnAutostart.Size = new System.Drawing.Size(150, 25);
            this.btnAutostart.Click += new System.EventHandler(this.btnAutostart_Click);
            // 
            // btnOpenProgramFolder
            // 
            this.btnOpenProgramFolder.Text = "Programmordner öffnen";
            this.btnOpenProgramFolder.Location = new System.Drawing.Point(170, 170);
            this.btnOpenProgramFolder.Size = new System.Drawing.Size(130, 25);
            this.btnOpenProgramFolder.Click += new System.EventHandler(this.btnOpenProgramFolder_Click);
            // 
            // btnOK
            // 
            this.btnOK.Text = "OK";
            this.btnOK.Location = new System.Drawing.Point(60, 210);
            this.btnOK.Size = new System.Drawing.Size(70, 25);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.Location = new System.Drawing.Point(150, 210);
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // linkRepo
            // 
            this.linkRepo.Text = "GitHub: OutlookLinkFixer";
            this.linkRepo.Location = new System.Drawing.Point(10, 250);
            this.linkRepo.Size = new System.Drawing.Size(200, 20);
            this.linkRepo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRepo_LinkClicked);
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(320, 280);
            this.Controls.Add(this.numericTimeout);
            this.Controls.Add(this.labelTimeout);
            this.Controls.Add(this.groupMode);
            this.Controls.Add(this.btnAutostart);
            this.Controls.Add(this.btnOpenProgramFolder);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.linkRepo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OutlookLinkFixer - Einstellungen";
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).EndInit();
            this.groupMode.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
