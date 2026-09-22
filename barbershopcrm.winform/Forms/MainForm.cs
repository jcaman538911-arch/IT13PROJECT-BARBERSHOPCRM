using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
    private const int SidebarExpandedWidth = 274;
    private const int SidebarCollapsedWidth = 62;
    private const int IconColumnWidth = 24;
    private const int NavRowHeight = 40;

    public User CurrentUser { get; }
    private Form? _activeForm = null;
    private NavItemButton? _activeNavButton = null;

    private readonly List<NavEntry> _navEntries = new();
    private bool _sidebarCollapsed = false;
    private readonly ToolTip _navTooltip = new();

    /// <summary>A navigable destination, used for rendering, quick-jump search and badges.</summary>
    private sealed record NavEntry(string Section, string Title, string Icon, Action Open, Func<int>? Badge = null, bool IsDanger = false)
    {
        public int BadgeValue { get; set; }
    }

    /// <summary>Nav row that paints its own gold left indicator when selected.</summary>
    private sealed class NavItemButton : Button
    {
        public NavEntry Entry { get; init; } = null!;
        public bool Selected { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Selected)
            {
                using var brush = new SolidBrush(ThemeHelper.MutedGold);
                e.Graphics.FillRectangle(brush, 0, 0, 4, Height);
            }
        }
    }

    public MainForm(User user)
    {
        InitializeComponent();
        this.WindowState = FormWindowState.Maximized;
        CurrentUser = user;
        this.Shown += MainForm_Shown;
        this.KeyPreview = true;
        this.KeyDown += MainForm_KeyDown;
        SetupUserInterface();
    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {
        // Open Dashboard (always the first entry) once the window handle exists.
        if (flpNavMenu.Controls.OfType<NavItemButton>().FirstOrDefault() is { } first)
        {
            first.PerformClick();
        }
    }

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.K)
        {
            txtNavSearch.Focus();
            txtNavSearch.SelectAll();
            e.Handled = true;
        }
    }

    private void SetupUserInterface()
    {
        string companyInfo = string.IsNullOrWhiteSpace(CurrentUser.CompanyName) ? "System Master" : CurrentUser.CompanyName;
        lblUserBadge.Text = $"👤 {CurrentUser.FullName} ({CurrentUser.Role}) | 🏢 {companyInfo}";
        this.Text = $"BarberShop CRM - {companyInfo}";
        UpdateClock();

        lblSidebarHeader.Click += (s, e) => NavigateTo("Dashboard");
        btnToggleSidebar.Click += (s, e) => ToggleSidebar();
        txtNavSearch.TextChanged += (s, e) => RenderNavigation();

        BuildNavigationEntries();
        RenderNavigation();
    }

    private DateTime _lastBadgeRefresh = DateTime.MinValue;

    private void timerClock_Tick(object sender, EventArgs e)
    {
        UpdateClock();
        if (DateTime.Now - _lastBadgeRefresh > TimeSpan.FromSeconds(60))
        {
            _lastBadgeRefresh = DateTime.Now;
            RefreshBadges();
        }
    }

    private void UpdateClock()
    {
        lblClock.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");
    }

    // ==================== NAVIGATION MODEL ====================

    private void BuildNavigationEntries()
    {
        _navEntries.Clear();

        switch (CurrentUser.Role)
        {
            case UserRole.SuperAdmin:
                Add("", "Dashboard", "🏠", () => OpenChildForm(new SuperAdminDashboardForm(this), "Super Admin Dashboard"));
                Add("ACCOUNTS", "System Users", "👤", () => OpenChildForm(new SystemUsersForm(), "System Users Management"));
                Add("ACCOUNTS", "Admin Accounts", "🔑", () => OpenChildForm(new AdminAccountsForm(), "Admin Accounts Manager"));
                Add("SYSTEM", "System Access", "🔒", () => OpenChildForm(new SystemAccessForm(), "System Access & Security"));
                Add("SYSTEM", "Maintenance", "⚡", () => OpenChildForm(new SystemMaintenanceForm(), "System Maintenance"));
                Add("SYSTEM", "Updates", "🔄", () => OpenChildForm(new SystemUpdatesForm(), "System Updates & Patches"));
                Add("SUPPORT", "Technical Support", "💬", () => OpenChildForm(new TechnicalSupportForm(), "Technical Support Desk"));
                break;

            case UserRole.Admin:
                Add("", "Dashboard", "🏠", () => OpenChildForm(new AdminDashboardForm(this), "Admin / Owner Dashboard"));
                Add("PEOPLE", "Employees", "🧑‍🔧", () => OpenChildForm(new EmployeesForm(), "Employee Management (Barbers & Staff)"));
                Add("PEOPLE", "Customers", "👥", () => OpenChildForm(new CustomersForm(), "Customer Management"));
                Add("PEOPLE", "Employee Attendance", "📅", () => OpenChildForm(new BarberAttendanceManagementForm(), "Employee Attendance Management"));
                Add("SERVICES & BENEFITS", "Services & Pricing", "✂️", () => OpenChildForm(new ServicesPricingForm(), "Services & Base Pricing"));
                Add("SERVICES & BENEFITS", "Promotions", "🏷️", () => OpenChildForm(new PromotionsForm(CurrentUser.Role), "Promotions Management"));
                Add("SERVICES & BENEFITS", "Loyalty Rewards", "🎁", () => OpenChildForm(new LoyaltyRewardsForm(CurrentUser.Role), "Loyalty & Rewards Program"));
                Add("SHOP OPERATIONS", "Inventory", "📦", () => OpenChildForm(new InventoryManagementForm(), "Inventory Management"),
                    badge: LowStockCount);
                Add("SHOP OPERATIONS", "Suppliers", "🚚", () => OpenChildForm(new SuppliersManagementForm(), "Supplier Management"));
                Add("SHOP OPERATIONS", "Branches", "🏢", () => OpenChildForm(new BranchesManagementForm(), "Branch Management"));
                Add("INSIGHTS", "Business Reports", "📊", () => OpenChildForm(new BusinessReportsForm(), "Business & Financial Reports"));
                break;

            case UserRole.Staff:
                Add("", "Dashboard", "🏠", () => OpenChildForm(new StaffDashboardForm(this), "Staff / Cashier Dashboard"));
                Add("DAILY OPERATIONS", "Customer Service Desk", "✂️",
                    () => OpenChildForm(new CustomerServiceDeskForm(CurrentUser), "Customer Service Desk"),
                    badge: CustomerServiceDeskForm.WaitingCount);
                Add("DAILY OPERATIONS", "Daily Transactions", "💵", () => OpenChildForm(new DailyTransactionsForm(), "Daily Sales Transactions"));
                Add("CUSTOMERS", "Customers", "👥", () => OpenChildForm(new CustomersForm(), "Customers - Register, Find & Profiles"));
                Add("CUSTOMERS", "Customer History", "📋", () => OpenChildForm(new CustomerHistoryForm(), "Customer Service History"));
                Add("CUSTOMERS", "Customer Concerns", "💬", () => OpenChildForm(new CustomerConcernsForm(CurrentUser), "Customer Concerns"),
                    badge: UnresolvedConcernCount);
                Add("CUSTOMER BENEFITS", "Promotions", "🏷️", () => OpenChildForm(new PromotionsForm(CurrentUser.Role), "Active Promotions"));
                Add("CUSTOMER BENEFITS", "Loyalty Rewards", "🎁", () => OpenChildForm(new LoyaltyRewardsForm(CurrentUser.Role), "Loyalty Rewards & Redemption"));
                Add("SHOP OPERATIONS", "Inventory Availability", "📦", () => OpenChildForm(new StockTransactionForm(CurrentUser), "Inventory Availability & Tasks"),
                    badge: LowStockCount);
                Add("SHOP OPERATIONS", "Employee Attendance", "📅", () => OpenChildForm(new RecordBarberAttendanceForm(), "Record Employee Attendance"));
                Add("SHOP OPERATIONS", "Branch Locations", "🏢", () => OpenChildForm(new BranchesManagementForm(), "Branch Locations"));
                break;
        }

        _navEntries.Add(new NavEntry("", "Logout", "↳", PerformLogout, IsDanger: true));
    }

    private void Add(string section, string title, string icon, Action open, Func<int>? badge = null)
        => _navEntries.Add(new NavEntry(section, title, icon, open, badge));

    private static int LowStockCount()
    {
        try
        {
            return SqlDataRepository.Instance.GetInventoryItems().Count(i => i.Quantity <= i.MinimumStockLevel);
        }
        catch
        {
            return 0;
        }
    }

    private static int UnresolvedConcernCount()
    {
        try
        {
            return SqlDataRepository.Instance.GetSupportRequests()
                .Count(c => !c.Status.Equals("Resolved", StringComparison.OrdinalIgnoreCase));
        }
        catch
        {
            return 0;
        }
    }

    // ==================== NAVIGATION RENDERING ====================

    private void RenderNavigation()
    {
        string query = _sidebarCollapsed ? string.Empty : txtNavSearch.Text.Trim();
        var matches = _navEntries
            .Where(entry => query.Length == 0
                            || entry.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || entry.Section.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();

        flpNavMenu.SuspendLayout();
        flpNavMenu.Controls.Clear();

        string? currentSection = null;
        foreach (var entry in matches)
        {
            bool sectionChanged = entry.Section != currentSection;
            if (sectionChanged)
            {
                currentSection = entry.Section;
                if (flpNavMenu.Controls.Count > 0) flpNavMenu.Controls.Add(BuildSeparator());
                if (!_sidebarCollapsed && query.Length == 0 && entry.Section.Length > 0)
                    flpNavMenu.Controls.Add(BuildSectionHeader(entry.Section));
            }

            var button = BuildNavButton(entry);
            flpNavMenu.Controls.Add(button);
            if (_activeNavButton?.Entry.Title == entry.Title) SelectButton(button);
        }

        if (matches.Count == 0)
        {
            flpNavMenu.Controls.Add(new Label
            {
                Text = "No modules match your search.",
                ForeColor = ThemeHelper.WarmGray,
                Font = ThemeHelper.SmallFont,
                Size = new Size(SidebarExpandedWidth - 40, 30),
                Margin = new Padding(4, 8, 0, 0)
            });
        }

        flpNavMenu.ResumeLayout();
    }

    private Label BuildSectionHeader(string text) => new()
    {
        Text = text,
        Size = new Size(SidebarExpandedWidth - 44, 26),
        Margin = new Padding(4, 6, 0, 2),
        Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
        ForeColor = ThemeHelper.WarmGray,
        TextAlign = ContentAlignment.BottomLeft
    };

    private Panel BuildSeparator() => new()
    {
        Size = new Size(_sidebarCollapsed ? SidebarCollapsedWidth - 24 : SidebarExpandedWidth - 44, 1),
        Margin = new Padding(4, 8, 0, 6),
        BackColor = Color.FromArgb(48, 48, 48)
    };

    private NavItemButton BuildNavButton(NavEntry entry)
    {
        var btn = new NavItemButton
        {
            Entry = entry,
            Size = new Size(_sidebarCollapsed ? SidebarCollapsedWidth - 24 : SidebarExpandedWidth - 44, NavRowHeight),
            Margin = new Padding(0, 0, 0, 2),
            FlatStyle = FlatStyle.Flat,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular),
            ForeColor = entry.IsDanger ? ThemeHelper.DeepBurgundy : ThemeHelper.WarmIvory,
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand,
            // Uniform icon column keeps every label starting at the same x position.
            Padding = new Padding(12, 0, 0, 0),
            Text = _sidebarCollapsed ? entry.Icon : $"{entry.Icon}".PadRight(2) + new string(' ', 2) + entry.Title
        };
        RenderBadge(btn);
        btn.FlatAppearance.BorderSize = 0;
        if (_sidebarCollapsed)
        {
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Padding = new Padding(0);
            _navTooltip.SetToolTip(btn, entry.Title);
        }

        btn.MouseEnter += (s, e) => { if (!btn.Selected) btn.BackColor = Color.FromArgb(38, 38, 38); };
        btn.MouseLeave += (s, e) => { if (!btn.Selected) btn.BackColor = Color.Transparent; };
        btn.Click += (s, e) =>
        {
            if (!entry.IsDanger) SelectButton(btn);
            entry.Open.Invoke();
        };
        return btn;
    }

    private void SelectButton(NavItemButton btn)
    {
        if (_activeNavButton != null && !_activeNavButton.IsDisposed)
        {
            _activeNavButton.Selected = false;
            _activeNavButton.BackColor = Color.Transparent;
            _activeNavButton.ForeColor = ThemeHelper.WarmIvory;
            _activeNavButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);
            _activeNavButton.Invalidate();
        }

        _activeNavButton = btn;
        btn.Selected = true;
        btn.BackColor = Color.FromArgb(38, 38, 38);
        btn.ForeColor = ThemeHelper.MutedGold;
        btn.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        btn.Invalidate();
    }

    /// <summary>Recomputes live operational counts for rows that declare a badge.
    /// Text is always rendered from the cached value so re-renders stay cheap.</summary>
    private void RefreshBadges()
    {
        foreach (var entry in _navEntries.Where(e => e.Badge != null))
        {
            entry.BadgeValue = entry.Badge!.Invoke();
        }
        foreach (var btn in flpNavMenu.Controls.OfType<NavItemButton>())
        {
            if (btn.Entry.Badge == null) continue;
            RenderBadge(btn);
        }
    }

    private void RenderBadge(NavItemButton btn)
    {
        string label = _sidebarCollapsed
            ? btn.Entry.Icon
            : $"{btn.Entry.Icon}".PadRight(2) + new string(' ', 2) + btn.Entry.Title
              + (btn.Entry.BadgeValue > 0 ? $"   ● {btn.Entry.BadgeValue}" : string.Empty);
        if (btn.Text != label) btn.Text = label;
    }

    private void ToggleSidebar()
    {
        _sidebarCollapsed = !_sidebarCollapsed;
        pnlSidebar.Width = _sidebarCollapsed ? SidebarCollapsedWidth : SidebarExpandedWidth;
        lblSidebarHeader.Text = _sidebarCollapsed ? "✂" : "✂ UPPERCUT BARBER SHOP";
        lblSidebarHeader.Font = _sidebarCollapsed
            ? new Font("Georgia", 13F, FontStyle.Bold)
            : new Font("Georgia", 10.5F, FontStyle.Bold);
        txtNavSearch.Visible = !_sidebarCollapsed;
        btnToggleSidebar.Text = _sidebarCollapsed ? "»" : "«";
        RenderNavigation();
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
        var target = flpNavMenu.Controls.OfType<NavItemButton>()
            .FirstOrDefault(b => b.Entry.Title.Equals(menuName.Trim(), StringComparison.OrdinalIgnoreCase));
        target?.PerformClick();
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
