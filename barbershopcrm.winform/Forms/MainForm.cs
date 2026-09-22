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
        _activeNavButton = null;

        switch (CurrentUser.Role)
        {
            case UserRole.SuperAdmin:
                AddNavButton("Dashboard", () => OpenChildForm(new SuperAdminDashboardForm(this), "Super Admin Dashboard"), icon: "👥");
                AddNavGroup("Accounts", "👤",
                    ("System Users", "👤", () => OpenChildForm(new SystemUsersForm(), "System Users Management")),
                    ("Admin Accounts", "🔑", () => OpenChildForm(new AdminAccountsForm(), "Admin Accounts Manager")));
                AddNavGroup("System", "⚡",
                    ("System Access", "🔒", () => OpenChildForm(new SystemAccessForm(), "System Access & Security")),
                    ("Maintenance", "⚡", () => OpenChildForm(new SystemMaintenanceForm(), "System Maintenance")),
                    ("Updates", "🔄", () => OpenChildForm(new SystemUpdatesForm(), "System Updates & Patches")));
                AddNavButton("Technical Support", () => OpenChildForm(new TechnicalSupportForm(), "Technical Support Desk"), icon: "💬");
                break;

            case UserRole.Admin:
                AddNavButton("Dashboard", () => OpenChildForm(new AdminDashboardForm(this), "Admin / Owner Dashboard"), icon: "👥");
                AddNavGroup("Customers", "👥",
                    ("Customer Directory", "👥", () => OpenChildForm(new CustomersForm(), "Customer Management")),
                    ("Transaction History", "📋", () => OpenChildForm(new CustomerHistoryForm(), "Customer Service History")));
                AddNavGroup("Employees", "🧑‍🔧",
                    ("Employee Management", "👥", () => OpenChildForm(new EmployeesForm(), "Employee Management (Barbers & Staff)")),
                    ("Barber Attendance", "📅", () => OpenChildForm(new BarberAttendanceManagementForm(), "Barber Attendance Management")));
                AddNavGroup("Catalog", "✂️",
                    ("Services & Pricing", "✂️", () => OpenChildForm(new ServicesPricingForm(), "Services & Base Pricing")),
                    ("Promotions", "🏷️", () => OpenChildForm(new PromotionsForm(CurrentUser.Role), "Promotions Management")),
                    ("Loyalty & Rewards", "🎁", () => OpenChildForm(new LoyaltyRewardsForm(CurrentUser.Role), "Loyalty & Rewards Program")));
                AddNavGroup("Inventory", "📦",
                    ("Inventory", "📦", () => OpenChildForm(new InventoryManagementForm(), "Inventory Management")),
                    ("Suppliers", "🚚", () => OpenChildForm(new SuppliersManagementForm(), "Supplier Management")),
                    ("Branches", "🏢", () => OpenChildForm(new BranchesManagementForm(), "Branch Management")));
                AddNavButton("Business Reports", () => OpenChildForm(new BusinessReportsForm(), "Business & Financial Reports"), icon: "📊");
                break;

            case UserRole.Staff:
                AddNavButton("Dashboard", () => OpenChildForm(new StaffDashboardForm(this), "Staff / Cashier Dashboard"), icon: "👥");
                AddNavGroup("Customers", "👥",
                    ("Register Customer", "➕", () => { using var regModal = new CustomerRegistrationForm(); regModal.ShowDialog(this); }),
                    ("Customer Directory", "👥", () => OpenChildForm(new CustomersForm(), "Customer Search & Registration")),
                    ("Transaction History", "📋", () => OpenChildForm(new CustomerHistoryForm(), "Customer Service History")));
                AddNavGroup("Transactions", "✂️",
                    ("New Transaction", "✂️", () => OpenChildForm(new ServiceTransactionForm(CurrentUser), "New Service Transaction")),
                    ("Daily Transactions", "💵", () => OpenChildForm(new DailyTransactionsForm(), "Daily Sales Transactions")));
                AddNavGroup("Promotions", "🏷️",
                    ("View Promotions", "🏷️", () => OpenChildForm(new PromotionsForm(CurrentUser.Role), "Active Promotions")),
                    ("Redeem Rewards", "🎁", () => OpenChildForm(new LoyaltyRewardsForm(CurrentUser.Role), "Loyalty Rewards & Redemption")));
                AddNavGroup("Inventory", "📦",
                    ("Inventory Tasks", "📦", () => OpenChildForm(new StockTransactionForm(CurrentUser), "Daily Inventory Tasks")),
                    ("Branch Locations", "🏢", () => OpenChildForm(new BranchesManagementForm(), "Branch Locations")),
                    ("Record Attendance", "📅", () => OpenChildForm(new RecordBarberAttendanceForm(), "Record Barber Attendance")));
                break;
        }

        // Add Logout menu item in Deep Burgundy
        AddNavButton("Logout", PerformLogout, icon: "↳", isDanger: true);
    }

    private void AddNavGroup(string title, string icon, params (string text, string icon, Action onClick)[] items)
    {
        var children = items.Select(i => MakeChildButton(i.text, i.icon, i.onClick)).ToList();
        bool expanded = true;

        Button header = new Button
        {
            Text = $"  ▾  {icon}  {title}",
            Size = new Size(240, 42),
            Margin = new Padding(0, 0, 0, 0),
            FlatStyle = FlatStyle.Flat,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = ThemeHelper.WarmIvory,
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand
        };
        header.FlatAppearance.BorderSize = 0;
        header.MouseEnter += (s, e) => header.BackColor = Color.FromArgb(40, 40, 40);
        header.MouseLeave += (s, e) => header.BackColor = Color.Transparent;
        header.Click += (s, e) =>
        {
            expanded = !expanded;
            header.Text = $"  {(expanded ? "▾" : "▸")}  {icon}  {title}";
            int index = flpNavMenu.Controls.GetChildIndex(header);
            if (expanded)
            {
                for (int k = 0; k < children.Count; k++)
                {
                    flpNavMenu.Controls.Add(children[k]);
                    flpNavMenu.Controls.SetChildIndex(children[k], index + 1 + k);
                }
            }
            else
            {
                foreach (var child in children) flpNavMenu.Controls.Remove(child);
            }
        };

        flpNavMenu.Controls.Add(header);
        foreach (var child in children) flpNavMenu.Controls.Add(child);
    }

    private Button MakeChildButton(string text, string icon, Action onClick)
    {
        Button btn = new Button
        {
            Text = $"       {icon}  {text}",
            Size = new Size(240, 36),
            Margin = new Padding(0, 0, 0, 0),
            FlatStyle = FlatStyle.Flat,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
            ForeColor = ThemeHelper.WarmIvory,
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand
        };
        btn.FlatAppearance.BorderSize = 0;
        btn.MouseEnter += (s, e) =>
        {
            if (btn != _activeNavButton) btn.BackColor = Color.FromArgb(40, 40, 40);
        };
        btn.MouseLeave += (s, e) =>
        {
            if (btn != _activeNavButton) btn.BackColor = Color.Transparent;
        };
        btn.Click += (s, e) =>
        {
            HighlightButton(btn, isDanger: false);
            onClick.Invoke();
        };
        return btn;
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
