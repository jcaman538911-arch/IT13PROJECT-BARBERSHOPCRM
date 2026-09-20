using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

partial class SystemMaintenanceForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        lblHeader = new Label();
        pnlAction1 = new Panel();
        lblAction1Title = new Label();
        lblAction1Desc = new Label();
        btnRunBackup = new Button();
        pnlAction2 = new Panel();
        lblAction2Title = new Label();
        lblAction2Desc = new Label();
        btnOptimizeMemory = new Button();
        pnlAction3 = new Panel();
        lblAction3Title = new Label();
        lblAction3Desc = new Label();
        btnClearLogs = new Button();
        txtConsole = new TextBox();
        lblConsole = new Label();
        pnlAction1.SuspendLayout();
        pnlAction2.SuspendLayout();
        pnlAction3.SuspendLayout();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.AutoSize = true;
        lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblHeader.Location = new Point(20, 20);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(276, 25);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "System Maintenance Console";
        // 
        // pnlAction1
        // 
        pnlAction1.BackColor = ThemeHelper.CardBackground;
        pnlAction1.BorderStyle = BorderStyle.FixedSingle;
        pnlAction1.Controls.Add(btnRunBackup);
        pnlAction1.Controls.Add(lblAction1Desc);
        pnlAction1.Controls.Add(lblAction1Title);
        pnlAction1.Location = new Point(20, 60);
        pnlAction1.Name = "pnlAction1";
        pnlAction1.Size = new Size(300, 140);
        pnlAction1.TabIndex = 1;
        // 
        // lblAction1Title
        // 
        lblAction1Title.AutoSize = true;
        lblAction1Title.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblAction1Title.ForeColor = ThemeHelper.PrimaryNavy;
        lblAction1Title.Location = new Point(15, 15);
        lblAction1Title.Name = "lblAction1Title";
        lblAction1Title.Size = new Size(181, 20);
        lblAction1Title.TabIndex = 0;
        lblAction1Title.Text = "💾 System Data Backup";
        // 
        // lblAction1Desc
        // 
        lblAction1Desc.Font = new Font("Segoe UI", 9F);
        lblAction1Desc.ForeColor = ThemeHelper.TextSecondary;
        lblAction1Desc.Location = new Point(15, 40);
        lblAction1Desc.Name = "lblAction1Desc";
        lblAction1Desc.Size = new Size(270, 40);
        lblAction1Desc.TabIndex = 1;
        lblAction1Desc.Text = "Creates an in-memory JSON state snapshot of all store datasets.";
        // 
        // btnRunBackup
        // 
        btnRunBackup.BackColor = ThemeHelper.PrimaryNavy;
        btnRunBackup.FlatAppearance.BorderSize = 0;
        btnRunBackup.FlatStyle = FlatStyle.Flat;
        btnRunBackup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnRunBackup.ForeColor = Color.White;
        btnRunBackup.Location = new Point(15, 90);
        btnRunBackup.Name = "btnRunBackup";
        btnRunBackup.Size = new Size(270, 32);
        btnRunBackup.TabIndex = 2;
        btnRunBackup.Text = "Run Backup Routine";
        btnRunBackup.UseVisualStyleBackColor = false;
        btnRunBackup.Click += btnRunBackup_Click;
        // 
        // pnlAction2
        // 
        pnlAction2.BackColor = ThemeHelper.CardBackground;
        pnlAction2.BorderStyle = BorderStyle.FixedSingle;
        pnlAction2.Controls.Add(btnOptimizeMemory);
        pnlAction2.Controls.Add(lblAction2Desc);
        pnlAction2.Controls.Add(lblAction2Title);
        pnlAction2.Location = new Point(340, 60);
        pnlAction2.Name = "pnlAction2";
        pnlAction2.Size = new Size(300, 140);
        pnlAction2.TabIndex = 2;
        // 
        // lblAction2Title
        // 
        lblAction2Title.AutoSize = true;
        lblAction2Title.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblAction2Title.ForeColor = ThemeHelper.PrimaryNavy;
        lblAction2Title.Location = new Point(15, 15);
        lblAction2Title.Name = "lblAction2Title";
        lblAction2Title.Size = new Size(207, 20);
        lblAction2Title.TabIndex = 0;
        lblAction2Title.Text = "⚡ Memory & Cache Reclaim";
        // 
        // lblAction2Desc
        // 
        lblAction2Desc.Font = new Font("Segoe UI", 9F);
        lblAction2Desc.ForeColor = ThemeHelper.TextSecondary;
        lblAction2Desc.Location = new Point(15, 40);
        lblAction2Desc.Name = "lblAction2Desc";
        lblAction2Desc.Size = new Size(270, 40);
        lblAction2Desc.TabIndex = 1;
        lblAction2Desc.Text = "Triggers Garbage Collection and clears temporary cache buffers.";
        // 
        // btnOptimizeMemory
        // 
        btnOptimizeMemory.BackColor = ThemeHelper.SecondaryNavy;
        btnOptimizeMemory.FlatAppearance.BorderSize = 0;
        btnOptimizeMemory.FlatStyle = FlatStyle.Flat;
        btnOptimizeMemory.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnOptimizeMemory.ForeColor = Color.White;
        btnOptimizeMemory.Location = new Point(15, 90);
        btnOptimizeMemory.Name = "btnOptimizeMemory";
        btnOptimizeMemory.Size = new Size(270, 32);
        btnOptimizeMemory.TabIndex = 2;
        btnOptimizeMemory.Text = "Optimize Memory";
        btnOptimizeMemory.UseVisualStyleBackColor = false;
        btnOptimizeMemory.Click += btnOptimizeMemory_Click;
        // 
        // pnlAction3
        // 
        pnlAction3.BackColor = ThemeHelper.CardBackground;
        pnlAction3.BorderStyle = BorderStyle.FixedSingle;
        pnlAction3.Controls.Add(btnClearLogs);
        pnlAction3.Controls.Add(lblAction3Desc);
        pnlAction3.Controls.Add(lblAction3Title);
        pnlAction3.Location = new Point(660, 60);
        pnlAction3.Name = "pnlAction3";
        pnlAction3.Size = new Size(300, 140);
        pnlAction3.TabIndex = 3;
        // 
        // lblAction3Title
        // 
        lblAction3Title.AutoSize = true;
        lblAction3Title.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblAction3Title.ForeColor = ThemeHelper.PrimaryNavy;
        lblAction3Title.Location = new Point(15, 15);
        lblAction3Title.Name = "lblAction3Title";
        lblAction3Title.Size = new Size(183, 20);
        lblAction3Title.TabIndex = 0;
        lblAction3Title.Text = "🧹 System Diagnostic Logs";
        // 
        // lblAction3Desc
        // 
        lblAction3Desc.Font = new Font("Segoe UI", 9F);
        lblAction3Desc.ForeColor = ThemeHelper.TextSecondary;
        lblAction3Desc.Location = new Point(15, 40);
        lblAction3Desc.Name = "lblAction3Desc";
        lblAction3Desc.Size = new Size(270, 40);
        lblAction3Desc.TabIndex = 1;
        lblAction3Desc.Text = "Flushes diagnostic log buffers and generates log summary report.";
        // 
        // btnClearLogs
        // 
        btnClearLogs.BackColor = ThemeHelper.CardHeaderBg;
        btnClearLogs.FlatAppearance.BorderSize = 0;
        btnClearLogs.FlatStyle = FlatStyle.Flat;
        btnClearLogs.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnClearLogs.ForeColor = ThemeHelper.TextPrimary;
        btnClearLogs.Location = new Point(15, 90);
        btnClearLogs.Name = "btnClearLogs";
        btnClearLogs.Size = new Size(270, 32);
        btnClearLogs.TabIndex = 2;
        btnClearLogs.Text = "Flush & Report Logs";
        btnClearLogs.UseVisualStyleBackColor = false;
        btnClearLogs.Click += btnClearLogs_Click;
        // 
        // lblConsole
        // 
        lblConsole.AutoSize = true;
        lblConsole.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblConsole.ForeColor = ThemeHelper.PrimaryNavy;
        lblConsole.Location = new Point(20, 220);
        lblConsole.Name = "lblConsole";
        lblConsole.Size = new Size(211, 20);
        lblConsole.TabIndex = 4;
        lblConsole.Text = "Maintenance Execution Console";
        // 
        // txtConsole
        // 
        txtConsole.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtConsole.BackColor = ThemeHelper.DarkNavy;
        txtConsole.BorderStyle = BorderStyle.FixedSingle;
        txtConsole.Font = new Font("Consolas", 10F);
        txtConsole.ForeColor = Color.FromArgb(242, 237, 229);
        txtConsole.Location = new Point(20, 250);
        txtConsole.Multiline = true;
        txtConsole.Name = "txtConsole";
        txtConsole.ReadOnly = true;
        txtConsole.ScrollBars = ScrollBars.Vertical;
        txtConsole.Size = new Size(940, 360);
        txtConsole.TabIndex = 5;
        // 
        // SystemMaintenanceForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(txtConsole);
        Controls.Add(lblConsole);
        Controls.Add(pnlAction3);
        Controls.Add(pnlAction2);
        Controls.Add(pnlAction1);
        Controls.Add(lblHeader);
        FormBorderStyle = FormBorderStyle.None;
        Name = "SystemMaintenanceForm";
        Text = "System Maintenance";
        pnlAction1.ResumeLayout(false);
        pnlAction1.PerformLayout();
        pnlAction2.ResumeLayout(false);
        pnlAction2.PerformLayout();
        pnlAction3.ResumeLayout(false);
        pnlAction3.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblHeader;
    private Panel pnlAction1;
    private Label lblAction1Title;
    private Label lblAction1Desc;
    private Button btnRunBackup;
    private Panel pnlAction2;
    private Label lblAction2Title;
    private Label lblAction2Desc;
    private Button btnOptimizeMemory;
    private Panel pnlAction3;
    private Label lblAction3Title;
    private Label lblAction3Desc;
    private Button btnClearLogs;
    private Label lblConsole;
    private TextBox txtConsole;
}
