using System;
using System.Drawing;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;
using BarberShopCRM.Forms.SuperAdmin;
using BarberShopCRM.Forms.Admin;
using BarberShopCRM.Forms.Staff;

namespace BarberShopCRM.Forms;

public partial class MainForm : Form
{
    public User CurrentUser { get; }
    private Form? _activeForm = null;
    private Button? _activeNavButton = null;

    public MainForm(User user)
    {
        InitializeComponent();
        this.WindowState = FormWindowState.Maximized;
        CurrentUser = user;
        this.Shown += MainForm_Shown;
        SetupUserInterface();
    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {
        // Click first navigation button once main window handle is created and shown
        if (flpNavMenu.Controls.Count > 0 && flpNavMenu.Controls[0] is Button defaultBtn)
        {
            defaultBtn.PerformClick();
        }
    }

    private void SetupUserInterface()
    {
        string companyInfo = string.IsNullOrWhiteSpace(CurrentUser.CompanyName) ? "System Master" : CurrentUser.CompanyName;
        lblUserBadge.Text = $"👤 {CurrentUser.FullName} ({CurrentUser.Role}) | 🏢 {companyInfo}";
        this.Text = $"BarberShop CRM - {companyInfo}";
        UpdateClock();
        BuildNavigationMenu();
    }

    private void timerClock_Tick(object sender, EventArgs e)
    {
        UpdateClock();
    }

    private void UpdateClock()
    {
        lblClock.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");
    }

    private void BuildNavigationMenu()
    {
        flpNavMenu.Controls.Clear();

        switch (CurrentUser.Role)
        {
            case UserRole.SuperAdmin:
                AddNavButton("Dashboard", () => OpenChildForm(new SuperAdminDashboardForm(this), "Super Admin Dashboard"), icon: "👥");
                AddNavButton("System Users", () => OpenChildForm(new SystemUsersForm(), "System Users Management"), icon: "👤");
                AddNavButton("Admin Accounts", () => OpenChildForm(new AdminAccountsForm(), "Admin Accounts Manager"), icon: "🔑");
                AddNavButton("System Access", () => OpenChildForm(new SystemAccessForm(), "System Access & Security"), icon: "🔒");
                AddNavButton("Maintenance", () => OpenChildForm(new SystemMaintenanceForm(), "System Maintenance"), icon: "⚡");
                AddNavButton("Updates", () => OpenChildForm(new SystemUpdatesForm(), "System Updates & Patches"), icon: "🔄");
                AddNavButton("Technical Support", () => OpenChildForm(new TechnicalSupportForm(), "Technical Support Desk"), icon: "💬");
                break;

            case UserRole.Admin:
                AddNavButton("Dashboard", () => OpenChildForm(new AdminDashboardForm(this), "Admin / Owner Dashboard"), icon: "👥");
                AddNavButton("Employees", () => OpenChildForm(new EmployeesForm(), "Employee Management (Barbers & Staff)"), icon: "👥");
                AddNavButton("Customers", () => OpenChildForm(new CustomersForm(), "Customer Management"), icon: "👥");
                AddNavButton("Services & Pricing", () => OpenChildForm(new ServicesPricingForm(), "Services & Base Pricing"), icon: "✂️");
                AddNavButton("Promotions", () => OpenChildForm(new PromotionsForm(CurrentUser.Role), "Promotions Management"), icon: "🏷️");
                AddNavButton("Loyalty & Rewards", () => OpenChildForm(new LoyaltyRewardsForm(CurrentUser.Role), "Loyalty & Rewards Program"), icon: "🎁");
                AddNavButton("Branches", () => OpenChildForm(new BranchesManagementForm(), "Branch Management"), icon: "🏢");
                AddNavButton("Inventory", () => OpenChildForm(new InventoryManagementForm(), "Inventory Management"), icon: "📦");
                AddNavButton("Suppliers", () => OpenChildForm(new SuppliersManagementForm(), "Supplier Management"), icon: "🚚");
                AddNavButton("Barber Attendance", () => OpenChildForm(new BarberAttendanceManagementForm(), "Barber Attendance Management"), icon: "📅");
                AddNavButton("Business Reports", () => OpenChildForm(new BusinessReportsForm(), "Business & Financial Reports"), icon: "📊");
                break;

            case UserRole.Staff:
                AddNavButton("Dashboard", () => OpenChildForm(new StaffDashboardForm(this), "Staff / Cashier Dashboard"), icon: "👥");
                AddNavButton("Customers", () => OpenChildForm(new CustomersForm(), "Customer Search & Registration"), icon: "👥");
                AddNavButton("New Transaction", () => OpenChildForm(new ServiceTransactionForm(CurrentUser), "New Service Transaction"), icon: "✂️");
                AddNavButton("Promotions", () => OpenChildForm(new PromotionsForm(CurrentUser.Role), "Active Promotions"), icon: "🏷️");
                AddNavButton("Loyalty & Rewards", () => OpenChildForm(new LoyaltyRewardsForm(CurrentUser.Role), "Loyalty Rewards"), icon: "🎁");
                AddNavButton("Customer History", () => OpenChildForm(new CustomerHistoryForm(), "Customer Service History"), icon: "📋");
                AddNavButton("Daily Transactions", () => OpenChildForm(new DailyTransactionsForm(), "Daily Sales Transactions"), icon: "💵");
                AddNavButton("Inventory Tasks", () => OpenChildForm(new StockTransactionForm(CurrentUser), "Daily Inventory Tasks"), icon: "📦");
                AddNavButton("Barber Attendance", () => OpenChildForm(new RecordBarberAttendanceForm(), "Record Barber Attendance"), icon: "📅");
                break;
        }

        // Add Logout menu item in Deep Burgundy
        AddNavButton("Logout", PerformLogout, icon: "↳", isDanger: true);
    }

    private void AddNavButton(string text, Action onClick, string icon = "", bool isDanger = false)
    {
        Button btn = new Button
        {
            Text = $"  {icon}  {text}".TrimStart(),
            Size = new Size(240, 42),
            Margin = new Padding(0, 0, 0, 8),
            FlatStyle = FlatStyle.Flat,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = isDanger ? ThemeHelper.DeepBurgundy : ThemeHelper.WarmIvory,
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand
        };
        btn.FlatAppearance.BorderSize = 0;

        btn.MouseEnter += (s, e) =>
        {
            if (btn != _activeNavButton && !isDanger)
            {
                btn.BackColor = Color.FromArgb(40, 40, 40);
            }
        };

        btn.MouseLeave += (s, e) =>
        {
            if (btn != _activeNavButton && !isDanger)
            {
                btn.BackColor = Color.Transparent;
            }
        };

        btn.Click += (s, e) =>
        {
            HighlightButton(btn, isDanger);
            onClick.Invoke();
        };

        flpNavMenu.Controls.Add(btn);
    }

    private void HighlightButton(Button btn, bool isDanger)
    {
        if (_activeNavButton != null && !_activeNavButton.IsDisposed)
        {
            _activeNavButton.BackColor = Color.Transparent;
            _activeNavButton.ForeColor = _activeNavButton.Tag is bool danger && danger ? ThemeHelper.DeepBurgundy : ThemeHelper.WarmIvory;
        }

        _activeNavButton = btn;
        if (!isDanger)
        {
            _activeNavButton.BackColor = ThemeHelper.MutedGold;
            _activeNavButton.ForeColor = ThemeHelper.DeepCharcoal;
        }
        else
        {
            _activeNavButton.BackColor = ThemeHelper.DeepBurgundy;
            _activeNavButton.ForeColor = ThemeHelper.WarmIvory;
        }
    }

    public bool IsLoggingOut { get; private set; } = false;

    public void OpenChildForm(Form childForm, string pageTitle)
    {
        try
        {
            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
            }

            _activeForm = childForm;
            lblPageTitle.Text = pageTitle;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlMainContent.Controls.Clear();
            pnlMainContent.Controls.Add(childForm);
            pnlMainContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error displaying page '{pageTitle}': {ex.Message}", "View Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void NavigateTo(string menuName)
    {
        foreach (Control ctrl in flpNavMenu.Controls)
        {
            if (ctrl is Button btn && btn.Text.Trim().Equals(menuName.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                btn.PerformClick();
                break;
            }
        }
    }

    private void PerformLogout()
    {
        var result = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
            IsLoggingOut = true;
            SqlDataRepository.Instance.AddSystemLog("INFO", "Auth", $"User '{CurrentUser.Username}' logged out.", CurrentUser.Username);
            this.Close();
        }
    }
}
