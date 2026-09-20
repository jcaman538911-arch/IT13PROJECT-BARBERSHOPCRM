using System;
using System.Drawing;
using System.Windows.Forms;
using barbershop.infrastructure;
using barbershop.domain;

namespace BarberShopCRM.Forms;

public partial class LoginForm : Form
{
    // Production Toggle: Set to false to hide Demo Access completely for production release
    private const bool EnableDemoAccessFeature = true;

    public User? LoggedInUser { get; private set; }

    public LoginForm()
    {
        InitializeComponent();
    }

    private void LoginForm_Load(object? sender, EventArgs e)
    {
        lblDemoAccessTrigger.Visible = EnableDemoAccessFeature;
        pnlDemoPopup.Visible = false;
        PositionLayoutControls();
    }

    private void LoginForm_Resize(object? sender, EventArgs e)
    {
        PositionLayoutControls();
    }

    private void PositionLayoutControls()
    {
        if (this.ClientSize.Width <= 0 || this.ClientSize.Height <= 0) return;

        // Center the Main Login Card in the middle of the window
        if (pnlBackground != null)
        {
            pnlBackground.Left = Math.Max(10, (this.ClientSize.Width - pnlBackground.Width) / 2);
            pnlBackground.Top = Math.Max(10, (this.ClientSize.Height - pnlBackground.Height) / 2);
        }

        // Position Demo Access trigger at bottom-right corner
        if (lblDemoAccessTrigger != null)
        {
            lblDemoAccessTrigger.Left = Math.Max(10, this.ClientSize.Width - lblDemoAccessTrigger.Width - 30);
            lblDemoAccessTrigger.Top = Math.Max(10, this.ClientSize.Height - lblDemoAccessTrigger.Height - 25);
        }

        // Position Demo Popup right above Demo Access trigger in bottom-right corner
        if (pnlDemoPopup != null && lblDemoAccessTrigger != null)
        {
            pnlDemoPopup.Left = Math.Max(10, this.ClientSize.Width - pnlDemoPopup.Width - 30);
            pnlDemoPopup.Top = Math.Max(10, lblDemoAccessTrigger.Top - pnlDemoPopup.Height - 10);
        }
    }

    private void lblDemoAccessTrigger_Click(object? sender, EventArgs e)
    {
        pnlDemoPopup.Visible = !pnlDemoPopup.Visible;
        if (pnlDemoPopup.Visible)
        {
            pnlDemoPopup.BringToFront();
        }
    }

    private void btnCloseDemo_Click(object? sender, EventArgs e)
    {
        pnlDemoPopup.Visible = false;
    }

    private void FillCredentials(string username, string password)
    {
        txtUsername.Text = username;
        txtPassword.Text = password;
        pnlDemoPopup.Visible = false;
        btnLogin.Focus();
    }

    // --- Demo Quick Test Account Selection ---

    private void btnDemoSuperAdmin_Click(object sender, EventArgs e)
    {
        FillCredentials("superadmin", "admin123");
    }

    private void btnDemoCompany1Owner_Click(object sender, EventArgs e)
    {
        FillCredentials("owner1", "owner123");
    }

    private void btnDemoCompany1Staff_Click(object sender, EventArgs e)
    {
        FillCredentials("staff1", "staff123");
    }

    private void btnDemoCompany2Owner_Click(object sender, EventArgs e)
    {
        FillCredentials("owner2", "owner123");
    }

    private void btnDemoCompany2Staff_Click(object sender, EventArgs e)
    {
        FillCredentials("staff2", "staff123");
    }

    private void btnDemoCompany3Owner_Click(object sender, EventArgs e)
    {
        FillCredentials("owner3", "owner123");
    }

    private void btnDemoCompany3Staff_Click(object sender, EventArgs e)
    {
        FillCredentials("staff3", "staff123");
    }

    // --- Core Password & Login Logic ---

    private void btnTogglePassword_Click(object sender, EventArgs e)
    {
        if (txtPassword.PasswordChar == '●')
        {
            txtPassword.PasswordChar = '\0';
            btnTogglePassword.Text = "Hide";
        }
        else
        {
            txtPassword.PasswordChar = '●';
            btnTogglePassword.Text = "Show";
        }
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text.Trim();
        string password = txtPassword.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var user = SqlDataRepository.Instance.Authenticate(username, password);
        if (user == null)
        {
            MessageBox.Show("Invalid username or password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        SqlDataRepository.Instance.AddSystemLog("INFO", "Auth", $"User '{user.Username}' logged in successfully as {user.Role}.", user.Username);

        LoggedInUser = user;
        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}
