using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

partial class AdminAccountsForm
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
        lblInfo = new Label();
        dgvAdminAccounts = new DataGridView();
        btnResetPassword = new Button();
        btnToggleStatus = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvAdminAccounts).BeginInit();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.AutoSize = true;
        lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblHeader.Location = new Point(20, 20);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(270, 25);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "Barbershop Owner / Admin Accounts";
        // 
        // lblInfo
        // 
        lblInfo.AutoSize = true;
        lblInfo.Font = new Font("Segoe UI", 9.5F);
        lblInfo.ForeColor = ThemeHelper.TextSecondary;
        lblInfo.Location = new Point(20, 50);
        lblInfo.Name = "lblInfo";
        lblInfo.Size = new Size(540, 17);
        lblInfo.TabIndex = 1;
        lblInfo.Text = "Super Admin manages login access for the Barbershop Owner/Admin. Business data is strictly hidden.";
        // 
        // dgvAdminAccounts
        // 
        dgvAdminAccounts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvAdminAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvAdminAccounts.Location = new Point(20, 85);
        dgvAdminAccounts.Name = "dgvAdminAccounts";
        dgvAdminAccounts.Size = new Size(980, 470);
        dgvAdminAccounts.TabIndex = 2;
        // 
        // btnResetPassword
        // 
        btnResetPassword.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnResetPassword.BackColor = ThemeHelper.PrimaryNavy;
        btnResetPassword.FlatAppearance.BorderSize = 0;
        btnResetPassword.FlatStyle = FlatStyle.Flat;
        btnResetPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnResetPassword.ForeColor = Color.White;
        btnResetPassword.Location = new Point(20, 570);
        btnResetPassword.Name = "btnResetPassword";
        btnResetPassword.Size = new Size(200, 38);
        btnResetPassword.TabIndex = 3;
        btnResetPassword.Text = "🔑 Reset Admin Password";
        btnResetPassword.UseVisualStyleBackColor = false;
        btnResetPassword.Click += btnResetPassword_Click;
        // 
        // btnToggleStatus
        // 
        btnToggleStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnToggleStatus.BackColor = ThemeHelper.CardHeaderBg;
        btnToggleStatus.FlatAppearance.BorderSize = 0;
        btnToggleStatus.FlatStyle = FlatStyle.Flat;
        btnToggleStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnToggleStatus.ForeColor = ThemeHelper.TextPrimary;
        btnToggleStatus.Location = new Point(235, 570);
        btnToggleStatus.Name = "btnToggleStatus";
        btnToggleStatus.Size = new Size(200, 38);
        btnToggleStatus.TabIndex = 4;
        btnToggleStatus.Text = "⚡ Toggle Active Status";
        btnToggleStatus.UseVisualStyleBackColor = false;
        btnToggleStatus.Click += btnToggleStatus_Click;
        // 
        // AdminAccountsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(btnToggleStatus);
        Controls.Add(btnResetPassword);
        Controls.Add(dgvAdminAccounts);
        Controls.Add(lblInfo);
        Controls.Add(lblHeader);
        FormBorderStyle = FormBorderStyle.None;
        Name = "AdminAccountsForm";
        Text = "Admin Accounts Manager";
        ((System.ComponentModel.ISupportInitialize)dgvAdminAccounts).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblHeader;
    private Label lblInfo;
    private DataGridView dgvAdminAccounts;
    private Button btnResetPassword;
    private Button btnToggleStatus;
}
