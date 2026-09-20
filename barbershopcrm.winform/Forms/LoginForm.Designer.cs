using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms;

partial class LoginForm
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
        pnlBackground = new Panel();
        lblBrandTitle = new Label();
        lblSubtitle = new Label();
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        btnTogglePassword = new Button();
        btnLogin = new Button();

        lblDemoAccessTrigger = new Label();

        pnlDemoPopup = new Panel();
        lblDemoHeader = new Label();
        btnCloseDemo = new Button();
        
        lblPlatformHeader = new Label();
        btnDemoSuperAdmin = new Button();

        lblCompany1Header = new Label();
        btnDemoCompany1Owner = new Button();
        btnDemoCompany1Staff = new Button();

        lblCompany2Header = new Label();
        btnDemoCompany2Owner = new Button();
        btnDemoCompany2Staff = new Button();

        lblCompany3Header = new Label();
        btnDemoCompany3Owner = new Button();
        btnDemoCompany3Staff = new Button();

        pnlBackground.SuspendLayout();
        pnlDemoPopup.SuspendLayout();
        SuspendLayout();
        // 
        // pnlBackground (Main Centered Login Card)
        // 
        pnlBackground.BackColor = Color.FromArgb(243, 235, 221);
        pnlBackground.BorderStyle = BorderStyle.FixedSingle;
        pnlBackground.Controls.Add(lblBrandTitle);
        pnlBackground.Controls.Add(lblSubtitle);
        pnlBackground.Controls.Add(lblUsername);
        pnlBackground.Controls.Add(txtUsername);
        pnlBackground.Controls.Add(lblPassword);
        pnlBackground.Controls.Add(txtPassword);
        pnlBackground.Controls.Add(btnTogglePassword);
        pnlBackground.Controls.Add(btnLogin);
        pnlBackground.Location = new Point(75, 50);
        pnlBackground.Margin = new Padding(3, 4, 3, 4);
        pnlBackground.Name = "pnlBackground";
        pnlBackground.Size = new Size(400, 350);
        pnlBackground.TabIndex = 0;
        // 
        // lblBrandTitle
        // 
        lblBrandTitle.Font = new Font("Georgia", 18F, FontStyle.Bold);
        lblBrandTitle.ForeColor = Color.FromArgb(23, 23, 23);
        lblBrandTitle.Location = new Point(15, 20);
        lblBrandTitle.Name = "lblBrandTitle";
        lblBrandTitle.Size = new Size(368, 60);
        lblBrandTitle.TabIndex = 0;
        lblBrandTitle.Text = "✂ UPPERCUT\r\nBARBER SHOP";
        lblBrandTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblSubtitle
        // 
        lblSubtitle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblSubtitle.ForeColor = Color.FromArgb(138, 131, 120);
        lblSubtitle.Location = new Point(15, 82);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(368, 22);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "CUT ABOVE THE REST • EST. 2024";
        lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblUsername.ForeColor = Color.FromArgb(23, 23, 23);
        lblUsername.Location = new Point(30, 120);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(87, 21);
        lblUsername.TabIndex = 2;
        lblUsername.Text = "Username";
        // 
        // txtUsername
        // 
        txtUsername.BackColor = Color.White;
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 10.5F);
        txtUsername.ForeColor = Color.FromArgb(23, 23, 23);
        txtUsername.Location = new Point(30, 145);
        txtUsername.Margin = new Padding(3, 4, 3, 4);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(338, 31);
        txtUsername.TabIndex = 3;
        // 
        // lblPassword
        // 
        lblPassword.AutoSize = true;
        lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPassword.ForeColor = Color.FromArgb(23, 23, 23);
        lblPassword.Location = new Point(30, 188);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(82, 21);
        lblPassword.TabIndex = 4;
        lblPassword.Text = "Password";
        // 
        // txtPassword
        // 
        txtPassword.BackColor = Color.White;
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 10.5F);
        txtPassword.ForeColor = Color.FromArgb(23, 23, 23);
        txtPassword.Location = new Point(30, 213);
        txtPassword.Margin = new Padding(3, 4, 3, 4);
        txtPassword.Name = "txtPassword";
        txtPassword.PasswordChar = '●';
        txtPassword.Size = new Size(254, 31);
        txtPassword.TabIndex = 5;
        // 
        // btnTogglePassword
        // 
        btnTogglePassword.BackColor = Color.FromArgb(23, 23, 23);
        btnTogglePassword.FlatAppearance.BorderSize = 0;
        btnTogglePassword.FlatStyle = FlatStyle.Flat;
        btnTogglePassword.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        btnTogglePassword.ForeColor = Color.FromArgb(243, 235, 221);
        btnTogglePassword.Location = new Point(288, 213);
        btnTogglePassword.Margin = new Padding(3, 4, 3, 4);
        btnTogglePassword.Name = "btnTogglePassword";
        btnTogglePassword.Size = new Size(80, 31);
        btnTogglePassword.TabIndex = 6;
        btnTogglePassword.Text = "Show";
        btnTogglePassword.UseVisualStyleBackColor = false;
        btnTogglePassword.Click += btnTogglePassword_Click;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.FromArgb(198, 161, 91);
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnLogin.ForeColor = Color.FromArgb(23, 23, 23);
        btnLogin.Location = new Point(30, 268);
        btnLogin.Margin = new Padding(3, 4, 3, 4);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(338, 48);
        btnLogin.TabIndex = 7;
        btnLogin.Text = "LOG IN";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click;
        // 
        // lblDemoAccessTrigger (Discreet Bottom-Right Control)
        // 
        lblDemoAccessTrigger.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        lblDemoAccessTrigger.AutoSize = true;
        lblDemoAccessTrigger.Cursor = Cursors.Hand;
        lblDemoAccessTrigger.Font = new Font("Segoe UI", 8.5F);
        lblDemoAccessTrigger.ForeColor = Color.FromArgb(140, 130, 120);
        lblDemoAccessTrigger.Location = new Point(440, 520);
        lblDemoAccessTrigger.Name = "lblDemoAccessTrigger";
        lblDemoAccessTrigger.Size = new Size(95, 20);
        lblDemoAccessTrigger.TabIndex = 1;
        lblDemoAccessTrigger.Text = "⚙ Demo Access";
        lblDemoAccessTrigger.Click += lblDemoAccessTrigger_Click;
        // 
        // pnlDemoPopup (Hidden Collapsible Panel)
        // 
        pnlDemoPopup.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        pnlDemoPopup.BackColor = Color.FromArgb(248, 244, 236);
        pnlDemoPopup.BorderStyle = BorderStyle.FixedSingle;
        pnlDemoPopup.Controls.Add(lblDemoHeader);
        pnlDemoPopup.Controls.Add(btnCloseDemo);
        pnlDemoPopup.Controls.Add(lblPlatformHeader);
        pnlDemoPopup.Controls.Add(btnDemoSuperAdmin);
        pnlDemoPopup.Controls.Add(lblCompany1Header);
        pnlDemoPopup.Controls.Add(btnDemoCompany1Owner);
        pnlDemoPopup.Controls.Add(btnDemoCompany1Staff);
        pnlDemoPopup.Controls.Add(lblCompany2Header);
        pnlDemoPopup.Controls.Add(btnDemoCompany2Owner);
        pnlDemoPopup.Controls.Add(btnDemoCompany2Staff);
        pnlDemoPopup.Controls.Add(lblCompany3Header);
        pnlDemoPopup.Controls.Add(btnDemoCompany3Owner);
        pnlDemoPopup.Controls.Add(btnDemoCompany3Staff);
        pnlDemoPopup.Location = new Point(230, 90);
        pnlDemoPopup.Name = "pnlDemoPopup";
        pnlDemoPopup.Size = new Size(300, 425);
        pnlDemoPopup.TabIndex = 2;
        pnlDemoPopup.Visible = false;
        // 
        // lblDemoHeader
        // 
        lblDemoHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblDemoHeader.ForeColor = Color.FromArgb(23, 23, 23);
        lblDemoHeader.Location = new Point(12, 10);
        lblDemoHeader.Name = "lblDemoHeader";
        lblDemoHeader.Size = new Size(235, 22);
        lblDemoHeader.TabIndex = 0;
        lblDemoHeader.Text = "DEMO ACCOUNTS";
        // 
        // btnCloseDemo
        // 
        btnCloseDemo.FlatAppearance.BorderSize = 0;
        btnCloseDemo.FlatStyle = FlatStyle.Flat;
        btnCloseDemo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnCloseDemo.ForeColor = Color.FromArgb(120, 110, 100);
        btnCloseDemo.Location = new Point(265, 5);
        btnCloseDemo.Name = "btnCloseDemo";
        btnCloseDemo.Size = new Size(28, 28);
        btnCloseDemo.TabIndex = 1;
        btnCloseDemo.Text = "✕";
        btnCloseDemo.UseVisualStyleBackColor = true;
        btnCloseDemo.Click += btnCloseDemo_Click;
        // 
        // lblPlatformHeader
        // 
        lblPlatformHeader.AutoSize = true;
        lblPlatformHeader.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblPlatformHeader.ForeColor = Color.FromArgb(130, 120, 110);
        lblPlatformHeader.Location = new Point(12, 38);
        lblPlatformHeader.Name = "lblPlatformHeader";
        lblPlatformHeader.Size = new Size(70, 17);
        lblPlatformHeader.TabIndex = 2;
        lblPlatformHeader.Text = "PLATFORM";
        // 
        // btnDemoSuperAdmin
        // 
        btnDemoSuperAdmin.BackColor = Color.FromArgb(40, 40, 40);
        btnDemoSuperAdmin.FlatAppearance.BorderSize = 0;
        btnDemoSuperAdmin.FlatStyle = FlatStyle.Flat;
        btnDemoSuperAdmin.Font = new Font("Segoe UI", 8.25F);
        btnDemoSuperAdmin.ForeColor = Color.FromArgb(243, 235, 221);
        btnDemoSuperAdmin.Location = new Point(12, 57);
        btnDemoSuperAdmin.Name = "btnDemoSuperAdmin";
        btnDemoSuperAdmin.Size = new Size(274, 28);
        btnDemoSuperAdmin.TabIndex = 3;
        btnDemoSuperAdmin.Text = "[ Super Admin ]";
        btnDemoSuperAdmin.TextAlign = ContentAlignment.MiddleLeft;
        btnDemoSuperAdmin.UseVisualStyleBackColor = false;
        btnDemoSuperAdmin.Click += btnDemoSuperAdmin_Click;
        // 
        // lblCompany1Header
        // 
        lblCompany1Header.AutoSize = true;
        lblCompany1Header.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblCompany1Header.ForeColor = Color.FromArgb(130, 120, 110);
        lblCompany1Header.Location = new Point(12, 95);
        lblCompany1Header.Name = "lblCompany1Header";
        lblCompany1Header.Size = new Size(82, 17);
        lblCompany1Header.TabIndex = 4;
        lblCompany1Header.Text = "TENANT 1";
        // 
        // btnDemoCompany1Owner
        // 
        btnDemoCompany1Owner.BackColor = Color.FromArgb(50, 50, 50);
        btnDemoCompany1Owner.FlatAppearance.BorderSize = 0;
        btnDemoCompany1Owner.FlatStyle = FlatStyle.Flat;
        btnDemoCompany1Owner.Font = new Font("Segoe UI", 8.25F);
        btnDemoCompany1Owner.ForeColor = Color.FromArgb(243, 235, 221);
        btnDemoCompany1Owner.Location = new Point(12, 114);
        btnDemoCompany1Owner.Name = "btnDemoCompany1Owner";
        btnDemoCompany1Owner.Size = new Size(274, 28);
        btnDemoCompany1Owner.TabIndex = 5;
        btnDemoCompany1Owner.Text = "[ Owner / Admin ]";
        btnDemoCompany1Owner.TextAlign = ContentAlignment.MiddleLeft;
        btnDemoCompany1Owner.UseVisualStyleBackColor = false;
        btnDemoCompany1Owner.Click += btnDemoCompany1Owner_Click;
        // 
        // btnDemoCompany1Staff
        // 
        btnDemoCompany1Staff.BackColor = Color.FromArgb(60, 60, 60);
        btnDemoCompany1Staff.FlatAppearance.BorderSize = 0;
        btnDemoCompany1Staff.FlatStyle = FlatStyle.Flat;
        btnDemoCompany1Staff.Font = new Font("Segoe UI", 8.25F);
        btnDemoCompany1Staff.ForeColor = Color.FromArgb(243, 235, 221);
        btnDemoCompany1Staff.Location = new Point(12, 146);
        btnDemoCompany1Staff.Name = "btnDemoCompany1Staff";
        btnDemoCompany1Staff.Size = new Size(274, 28);
        btnDemoCompany1Staff.TabIndex = 6;
        btnDemoCompany1Staff.Text = "[ Staff / Cashier ]";
        btnDemoCompany1Staff.TextAlign = ContentAlignment.MiddleLeft;
        btnDemoCompany1Staff.UseVisualStyleBackColor = false;
        btnDemoCompany1Staff.Click += btnDemoCompany1Staff_Click;
        // 
        // lblCompany2Header
        // 
        lblCompany2Header.AutoSize = true;
        lblCompany2Header.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblCompany2Header.ForeColor = Color.FromArgb(130, 120, 110);
        lblCompany2Header.Location = new Point(12, 185);
        lblCompany2Header.Name = "lblCompany2Header";
        lblCompany2Header.Size = new Size(82, 17);
        lblCompany2Header.TabIndex = 7;
        lblCompany2Header.Text = "TENANT 2";
        // 
        // btnDemoCompany2Owner
        // 
        btnDemoCompany2Owner.BackColor = Color.FromArgb(50, 50, 50);
        btnDemoCompany2Owner.FlatAppearance.BorderSize = 0;
        btnDemoCompany2Owner.FlatStyle = FlatStyle.Flat;
        btnDemoCompany2Owner.Font = new Font("Segoe UI", 8.25F);
        btnDemoCompany2Owner.ForeColor = Color.FromArgb(243, 235, 221);
        btnDemoCompany2Owner.Location = new Point(12, 204);
        btnDemoCompany2Owner.Name = "btnDemoCompany2Owner";
        btnDemoCompany2Owner.Size = new Size(274, 28);
        btnDemoCompany2Owner.TabIndex = 8;
        btnDemoCompany2Owner.Text = "[ Owner / Admin ]";
        btnDemoCompany2Owner.TextAlign = ContentAlignment.MiddleLeft;
        btnDemoCompany2Owner.UseVisualStyleBackColor = false;
        btnDemoCompany2Owner.Click += btnDemoCompany2Owner_Click;
        // 
        // btnDemoCompany2Staff
        // 
        btnDemoCompany2Staff.BackColor = Color.FromArgb(60, 60, 60);
        btnDemoCompany2Staff.FlatAppearance.BorderSize = 0;
        btnDemoCompany2Staff.FlatStyle = FlatStyle.Flat;
        btnDemoCompany2Staff.Font = new Font("Segoe UI", 8.25F);
        btnDemoCompany2Staff.ForeColor = Color.FromArgb(243, 235, 221);
        btnDemoCompany2Staff.Location = new Point(12, 236);
        btnDemoCompany2Staff.Name = "btnDemoCompany2Staff";
        btnDemoCompany2Staff.Size = new Size(274, 28);
        btnDemoCompany2Staff.TabIndex = 9;
        btnDemoCompany2Staff.Text = "[ Staff / Cashier ]";
        btnDemoCompany2Staff.TextAlign = ContentAlignment.MiddleLeft;
        btnDemoCompany2Staff.UseVisualStyleBackColor = false;
        btnDemoCompany2Staff.Click += btnDemoCompany2Staff_Click;
        // 
        // lblCompany3Header
        // 
        lblCompany3Header.AutoSize = true;
        lblCompany3Header.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblCompany3Header.ForeColor = Color.FromArgb(130, 120, 110);
        lblCompany3Header.Location = new Point(12, 275);
        lblCompany3Header.Name = "lblCompany3Header";
        lblCompany3Header.Size = new Size(82, 17);
        lblCompany3Header.TabIndex = 10;
        lblCompany3Header.Text = "TENANT 3";
        // 
        // btnDemoCompany3Owner
        // 
        btnDemoCompany3Owner.BackColor = Color.FromArgb(50, 50, 50);
        btnDemoCompany3Owner.FlatAppearance.BorderSize = 0;
        btnDemoCompany3Owner.FlatStyle = FlatStyle.Flat;
        btnDemoCompany3Owner.Font = new Font("Segoe UI", 8.25F);
        btnDemoCompany3Owner.ForeColor = Color.FromArgb(243, 235, 221);
        btnDemoCompany3Owner.Location = new Point(12, 294);
        btnDemoCompany3Owner.Name = "btnDemoCompany3Owner";
        btnDemoCompany3Owner.Size = new Size(274, 28);
        btnDemoCompany3Owner.TabIndex = 11;
        btnDemoCompany3Owner.Text = "[ Owner / Admin ]";
        btnDemoCompany3Owner.TextAlign = ContentAlignment.MiddleLeft;
        btnDemoCompany3Owner.UseVisualStyleBackColor = false;
        btnDemoCompany3Owner.Click += btnDemoCompany3Owner_Click;
        // 
        // btnDemoCompany3Staff
        // 
        btnDemoCompany3Staff.BackColor = Color.FromArgb(60, 60, 60);
        btnDemoCompany3Staff.FlatAppearance.BorderSize = 0;
        btnDemoCompany3Staff.FlatStyle = FlatStyle.Flat;
        btnDemoCompany3Staff.Font = new Font("Segoe UI", 8.25F);
        btnDemoCompany3Staff.ForeColor = Color.FromArgb(243, 235, 221);
        btnDemoCompany3Staff.Location = new Point(12, 326);
        btnDemoCompany3Staff.Name = "btnDemoCompany3Staff";
        btnDemoCompany3Staff.Size = new Size(274, 28);
        btnDemoCompany3Staff.TabIndex = 12;
        btnDemoCompany3Staff.Text = "[ Staff / Cashier ]";
        btnDemoCompany3Staff.TextAlign = ContentAlignment.MiddleLeft;
        btnDemoCompany3Staff.UseVisualStyleBackColor = false;
        btnDemoCompany3Staff.Click += btnDemoCompany3Staff_Click;
        // 
        // LoginForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(243, 235, 221);
        ClientSize = new Size(550, 600);
        Controls.Add(pnlDemoPopup);
        Controls.Add(lblDemoAccessTrigger);
        Controls.Add(pnlBackground);
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "UPPERCUT BARBER SHOP CRM - System Login";
        WindowState = FormWindowState.Maximized;
        Load += LoginForm_Load;
        Resize += LoginForm_Resize;
        pnlBackground.ResumeLayout(false);
        pnlBackground.PerformLayout();
        pnlDemoPopup.ResumeLayout(false);
        pnlDemoPopup.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Panel pnlBackground;
    private Label lblBrandTitle;
    private Label lblSubtitle;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblPassword;
    private TextBox txtPassword;
    private Button btnTogglePassword;
    private Button btnLogin;

    private Label lblDemoAccessTrigger;
    private Panel pnlDemoPopup;
    private Label lblDemoHeader;
    private Button btnCloseDemo;

    private Label lblPlatformHeader;
    private Button btnDemoSuperAdmin;

    private Label lblCompany1Header;
    private Button btnDemoCompany1Owner;
    private Button btnDemoCompany1Staff;

    private Label lblCompany2Header;
    private Button btnDemoCompany2Owner;
    private Button btnDemoCompany2Staff;

    private Label lblCompany3Header;
    private Button btnDemoCompany3Owner;
    private Button btnDemoCompany3Staff;
}
