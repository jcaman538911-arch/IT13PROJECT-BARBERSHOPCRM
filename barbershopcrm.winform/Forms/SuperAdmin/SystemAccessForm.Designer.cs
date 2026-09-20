using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

partial class SystemAccessForm
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
        pnlRules = new Panel();
        lblRoleRules = new Label();
        txtRules = new TextBox();
        btnSaveAccessRules = new Button();
        dgvAccessLogs = new DataGridView();
        lblLogTitle = new Label();
        pnlRules.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvAccessLogs).BeginInit();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.AutoSize = true;
        lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblHeader.Location = new Point(20, 20);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(307, 25);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "System Access & Security Control";
        // 
        // pnlRules
        // 
        pnlRules.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlRules.BackColor = ThemeHelper.CardBackground;
        pnlRules.BorderStyle = BorderStyle.FixedSingle;
        pnlRules.Controls.Add(btnSaveAccessRules);
        pnlRules.Controls.Add(txtRules);
        pnlRules.Controls.Add(lblRoleRules);
        pnlRules.Location = new Point(20, 60);
        pnlRules.Name = "pnlRules";
        pnlRules.Size = new Size(980, 160);
        pnlRules.TabIndex = 1;
        // 
        // lblRoleRules
        // 
        lblRoleRules.AutoSize = true;
        lblRoleRules.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblRoleRules.ForeColor = ThemeHelper.PrimaryNavy;
        lblRoleRules.Location = new Point(15, 12);
        lblRoleRules.Name = "lblRoleRules";
        lblRoleRules.Size = new Size(205, 19);
        lblRoleRules.TabIndex = 0;
        lblRoleRules.Text = "Role Separation Directives:";
        // 
        // txtRules
        // 
        txtRules.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtRules.BackColor = ThemeHelper.CardHeaderBg;
        txtRules.BorderStyle = BorderStyle.FixedSingle;
        txtRules.Font = new Font("Consolas", 9.5F);
        txtRules.ForeColor = ThemeHelper.TextPrimary;
        txtRules.Location = new Point(15, 38);
        txtRules.Multiline = true;
        txtRules.Name = "txtRules";
        txtRules.ReadOnly = true;
        txtRules.ScrollBars = ScrollBars.Vertical;
        txtRules.Size = new Size(950, 75);
        txtRules.TabIndex = 1;
        txtRules.Text = "SUPER ADMIN: System Users, Admin Accounts, System Access, Maintenance, Updates, Technical Support ONLY.\r\nADMIN / OWNER: Business Management (Employees, Customers, Services, Promotions, Loyalty, Attendance, Reports).\r\nSTAFF / CASHIER: Daily Operations (Customers, Transactions, Payments, Promotions, Loyalty, History, Attendance).\r\nBARBER / CUSTOMER: NO SYSTEM LOGINS.";
        // 
        // btnSaveAccessRules
        // 
        btnSaveAccessRules.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnSaveAccessRules.BackColor = ThemeHelper.PrimaryNavy;
        btnSaveAccessRules.FlatAppearance.BorderSize = 0;
        btnSaveAccessRules.FlatStyle = FlatStyle.Flat;
        btnSaveAccessRules.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSaveAccessRules.ForeColor = Color.White;
        btnSaveAccessRules.Location = new Point(785, 120);
        btnSaveAccessRules.Name = "btnSaveAccessRules";
        btnSaveAccessRules.Size = new Size(180, 30);
        btnSaveAccessRules.TabIndex = 2;
        btnSaveAccessRules.Text = "🔒 Lock Security Policies";
        btnSaveAccessRules.UseVisualStyleBackColor = false;
        btnSaveAccessRules.Click += btnSaveAccessRules_Click;
        // 
        // dgvAccessLogs
        // 
        dgvAccessLogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvAccessLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvAccessLogs.Location = new Point(20, 260);
        dgvAccessLogs.Name = "dgvAccessLogs";
        dgvAccessLogs.Size = new Size(980, 360);
        dgvAccessLogs.TabIndex = 2;
        // 
        // lblLogTitle
        // 
        lblLogTitle.AutoSize = true;
        lblLogTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblLogTitle.ForeColor = ThemeHelper.PrimaryNavy;
        lblLogTitle.Location = new Point(20, 233);
        lblLogTitle.Name = "lblLogTitle";
        lblLogTitle.Size = new Size(207, 20);
        lblLogTitle.TabIndex = 3;
        lblLogTitle.Text = "System Authentication Audit";
        // 
        // SystemAccessForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(lblLogTitle);
        Controls.Add(dgvAccessLogs);
        Controls.Add(pnlRules);
        Controls.Add(lblHeader);
        FormBorderStyle = FormBorderStyle.None;
        Name = "SystemAccessForm";
        Text = "System Access Control";
        pnlRules.ResumeLayout(false);
        pnlRules.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvAccessLogs).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblHeader;
    private Panel pnlRules;
    private Label lblRoleRules;
    private TextBox txtRules;
    private Button btnSaveAccessRules;
    private DataGridView dgvAccessLogs;
    private Label lblLogTitle;
}
