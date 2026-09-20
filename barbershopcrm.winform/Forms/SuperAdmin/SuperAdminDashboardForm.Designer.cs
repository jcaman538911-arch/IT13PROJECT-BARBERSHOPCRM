using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

partial class SuperAdminDashboardForm
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
        pnlCard1 = new Panel();
        lblCard1Title = new Label();
        lblCard1Value = new Label();
        pnlCard2 = new Panel();
        lblCard2Title = new Label();
        lblCard2Value = new Label();
        pnlCard3 = new Panel();
        lblCard3Title = new Label();
        lblCard3Value = new Label();
        pnlCard4 = new Panel();
        lblCard4Title = new Label();
        lblCard4Value = new Label();
        lblLogsHeader = new Label();
        dgvSystemLogs = new DataGridView();
        pnlCard1.SuspendLayout();
        pnlCard2.SuspendLayout();
        pnlCard3.SuspendLayout();
        pnlCard4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSystemLogs).BeginInit();
        SuspendLayout();
        // 
        // pnlCard1
        // 
        pnlCard1.BackColor = ThemeHelper.CardBackground;
        pnlCard1.BorderStyle = BorderStyle.FixedSingle;
        pnlCard1.Controls.Add(lblCard1Value);
        pnlCard1.Controls.Add(lblCard1Title);
        pnlCard1.Location = new Point(20, 20);
        pnlCard1.Name = "pnlCard1";
        pnlCard1.Size = new Size(230, 100);
        pnlCard1.TabIndex = 0;
        // 
        // lblCard1Title
        // 
        lblCard1Title.AutoSize = true;
        lblCard1Title.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCard1Title.ForeColor = ThemeHelper.TextSecondary;
        lblCard1Title.Location = new Point(15, 15);
        lblCard1Title.Name = "lblCard1Title";
        lblCard1Title.Size = new Size(97, 17);
        lblCard1Title.TabIndex = 0;
        lblCard1Title.Text = "SYSTEM USERS";
        // 
        // lblCard1Value
        // 
        lblCard1Value.AutoSize = true;
        lblCard1Value.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblCard1Value.ForeColor = ThemeHelper.PrimaryNavy;
        lblCard1Value.Location = new Point(15, 40);
        lblCard1Value.Name = "lblCard1Value";
        lblCard1Value.Size = new Size(35, 41);
        lblCard1Value.TabIndex = 1;
        lblCard1Value.Text = "3";
        // 
        // pnlCard2
        // 
        pnlCard2.BackColor = ThemeHelper.CardBackground;
        pnlCard2.BorderStyle = BorderStyle.FixedSingle;
        pnlCard2.Controls.Add(lblCard2Value);
        pnlCard2.Controls.Add(lblCard2Title);
        pnlCard2.Location = new Point(270, 20);
        pnlCard2.Name = "pnlCard2";
        pnlCard2.Size = new Size(230, 100);
        pnlCard2.TabIndex = 1;
        // 
        // lblCard2Title
        // 
        lblCard2Title.AutoSize = true;
        lblCard2Title.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCard2Title.ForeColor = ThemeHelper.TextSecondary;
        lblCard2Title.Location = new Point(15, 15);
        lblCard2Title.Name = "lblCard2Title";
        lblCard2Title.Size = new Size(125, 17);
        lblCard2Title.TabIndex = 0;
        lblCard2Title.Text = "ADMIN ACCOUNTS";
        // 
        // lblCard2Value
        // 
        lblCard2Value.AutoSize = true;
        lblCard2Value.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblCard2Value.ForeColor = ThemeHelper.PrimaryNavy;
        lblCard2Value.Location = new Point(15, 40);
        lblCard2Value.Name = "lblCard2Value";
        lblCard2Value.Size = new Size(35, 41);
        lblCard2Value.TabIndex = 1;
        lblCard2Value.Text = "1";
        // 
        // pnlCard3
        // 
        pnlCard3.BackColor = ThemeHelper.CardBackground;
        pnlCard3.BorderStyle = BorderStyle.FixedSingle;
        pnlCard3.Controls.Add(lblCard3Value);
        pnlCard3.Controls.Add(lblCard3Title);
        pnlCard3.Location = new Point(520, 20);
        pnlCard3.Name = "pnlCard3";
        pnlCard3.Size = new Size(230, 100);
        pnlCard3.TabIndex = 2;
        // 
        // lblCard3Title
        // 
        lblCard3Title.AutoSize = true;
        lblCard3Title.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCard3Title.ForeColor = ThemeHelper.TextSecondary;
        lblCard3Title.Location = new Point(15, 15);
        lblCard3Title.Name = "lblCard3Title";
        lblCard3Title.Size = new Size(117, 17);
        lblCard3Title.TabIndex = 0;
        lblCard3Title.Text = "SYSTEM STATUS";
        // 
        // lblCard3Value
        // 
        lblCard3Value.AutoSize = true;
        lblCard3Value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblCard3Value.ForeColor = ThemeHelper.SecondaryNavy;
        lblCard3Value.Location = new Point(15, 42);
        lblCard3Value.Name = "lblCard3Value";
        lblCard3Value.Size = new Size(125, 37);
        lblCard3Value.TabIndex = 1;
        lblCard3Value.Text = "Healthy";
        // 
        // pnlCard4
        // 
        pnlCard4.BackColor = ThemeHelper.CardBackground;
        pnlCard4.BorderStyle = BorderStyle.FixedSingle;
        pnlCard4.Controls.Add(lblCard4Value);
        pnlCard4.Controls.Add(lblCard4Title);
        pnlCard4.Location = new Point(770, 20);
        pnlCard4.Name = "pnlCard4";
        pnlCard4.Size = new Size(230, 100);
        pnlCard4.TabIndex = 3;
        // 
        // lblCard4Title
        // 
        lblCard4Title.AutoSize = true;
        lblCard4Title.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCard4Title.ForeColor = ThemeHelper.TextSecondary;
        lblCard4Title.Location = new Point(15, 15);
        lblCard4Title.Name = "lblCard4Title";
        lblCard4Title.Size = new Size(137, 17);
        lblCard4Title.TabIndex = 0;
        lblCard4Title.Text = "SUPPORT TICKETS";
        // 
        // lblCard4Value
        // 
        lblCard4Value.AutoSize = true;
        lblCard4Value.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblCard4Value.ForeColor = ThemeHelper.BarberRed;
        lblCard4Value.Location = new Point(15, 40);
        lblCard4Value.Name = "lblCard4Value";
        lblCard4Value.Size = new Size(35, 41);
        lblCard4Value.TabIndex = 1;
        lblCard4Value.Text = "1";
        // 
        // lblLogsHeader
        // 
        lblLogsHeader.AutoSize = true;
        lblLogsHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblLogsHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblLogsHeader.Location = new Point(20, 145);
        lblLogsHeader.Name = "lblLogsHeader";
        lblLogsHeader.Size = new Size(189, 21);
        lblLogsHeader.TabIndex = 4;
        lblLogsHeader.Text = "Recent System Audit Logs";
        // 
        // dgvSystemLogs
        // 
        dgvSystemLogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvSystemLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSystemLogs.Location = new Point(20, 175);
        dgvSystemLogs.Name = "dgvSystemLogs";
        dgvSystemLogs.Size = new Size(980, 440);
        dgvSystemLogs.TabIndex = 5;
        // 
        // SuperAdminDashboardForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvSystemLogs);
        Controls.Add(lblLogsHeader);
        Controls.Add(pnlCard4);
        Controls.Add(pnlCard3);
        Controls.Add(pnlCard2);
        Controls.Add(pnlCard1);
        FormBorderStyle = FormBorderStyle.None;
        Name = "SuperAdminDashboardForm";
        Text = "Super Admin Dashboard";
        pnlCard1.ResumeLayout(false);
        pnlCard1.PerformLayout();
        pnlCard2.ResumeLayout(false);
        pnlCard2.PerformLayout();
        pnlCard3.ResumeLayout(false);
        pnlCard3.PerformLayout();
        pnlCard4.ResumeLayout(false);
        pnlCard4.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSystemLogs).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Panel pnlCard1;
    private Label lblCard1Title;
    private Label lblCard1Value;
    private Panel pnlCard2;
    private Label lblCard2Title;
    private Label lblCard2Value;
    private Panel pnlCard3;
    private Label lblCard3Title;
    private Label lblCard3Value;
    private Panel pnlCard4;
    private Label lblCard4Title;
    private Label lblCard4Value;
    private Label lblLogsHeader;
    private DataGridView dgvSystemLogs;
}
