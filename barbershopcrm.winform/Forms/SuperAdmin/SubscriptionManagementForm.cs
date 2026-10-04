using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

public partial class SubscriptionManagementForm : Form
{
    private DataGridView dgvSubscriptions = null!;
    private Panel pnlActions = null!;
    private Button btnRefresh = null!, btnAddTenant = null!, btnRenew = null!, btnSuspend = null!, btnActivate = null!, btnExpire = null!;
    private Label lblHeader = null!, lblStatus = null!;
    private Panel pnlSummary = null!;

    // Summary cards
    private Label lblActiveCount = null!, lblExpiredCount = null!, lblSuspendedCount = null!, lblRevenue = null!;

    private TenantSubscription? _selected = null;

    public SubscriptionManagementForm()
    {
        BuildLayout();
        ResponsiveLayoutHelper.Apply(this);
        LoadSubscriptions();
    }

    private void BuildLayout()
    {
        this.Text = "Tenant Subscription Management";
        this.BackColor = ThemeHelper.WarmCanvas;
        this.FormBorderStyle = FormBorderStyle.None;
        this.Size = new Size(1020, 680);

        // Header
        lblHeader = new Label
        {
            Text = "🔑  Tenant Subscription Management",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold),
            ForeColor = ThemeHelper.PrimaryNavy,
            AutoSize = true,
            Location = new Point(20, 18)
        };
        this.Controls.Add(lblHeader);

        // Summary panel
        pnlSummary = new Panel
        {
            Location = new Point(20, 55),
            Size = new Size(980, 90),
            BackColor = Color.Transparent
        };
        this.Controls.Add(pnlSummary);

        AddSummaryCard(pnlSummary, 0,   "ACTIVE TENANTS",    Color.FromArgb(39, 174, 96),  out lblActiveCount);
        AddSummaryCard(pnlSummary, 245, "EXPIRED",           Color.FromArgb(231, 76, 60),  out lblExpiredCount);
        AddSummaryCard(pnlSummary, 490, "SUSPENDED",         Color.FromArgb(230, 126, 34), out lblSuspendedCount);
        AddSummaryCard(pnlSummary, 735, "MONTHLY REVENUE",   Color.FromArgb(52, 152, 219), out lblRevenue);

        // Subscriptions grid
        dgvSubscriptions = new DataGridView
        {
            Location = new Point(20, 160),
            Size = new Size(980, 400),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AllowUserToAddRows = false
        };
        ThemeHelper.ApplyModernGrid(dgvSubscriptions);
        dgvSubscriptions.SelectionChanged += DgvSubscriptions_SelectionChanged;
        this.Controls.Add(dgvSubscriptions);

        // Action buttons panel
        pnlActions = new Panel
        {
            Location = new Point(20, 575),
            Size = new Size(980, 50),
            Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            BackColor = Color.Transparent
        };
        this.Controls.Add(pnlActions);

        btnRefresh   = MakeButton("🔄  Refresh",          ThemeHelper.SecondaryNavy,       0);
        btnAddTenant = MakeButton("➕  Add Tenant",       ThemeHelper.PrimaryGold,       160);
        btnActivate  = MakeButton("✅  Activate",         Color.FromArgb(39, 174, 96),   320);
        btnRenew     = MakeButton("💳  Renew",            Color.FromArgb(52, 152, 219),  480);
        btnSuspend   = MakeButton("🚫  Suspend",          Color.FromArgb(230, 126, 34),  640);
        btnExpire    = MakeButton("⏰  Mark Expired",     Color.FromArgb(231, 76, 60),   800);

        pnlActions.Controls.AddRange(new Control[] { btnRefresh, btnAddTenant, btnActivate, btnRenew, btnSuspend, btnExpire });

        btnActivate.Enabled = btnRenew.Enabled = btnSuspend.Enabled = btnExpire.Enabled = false;

        btnRefresh.Click    += (s, e) => LoadSubscriptions();
        btnAddTenant.Click  += BtnAddTenant_Click;
        btnActivate.Click   += BtnActivate_Click;
        btnRenew.Click      += BtnRenew_Click;
        btnSuspend.Click    += BtnSuspend_Click;
        btnExpire.Click     += BtnExpire_Click;

        // Status bar
        lblStatus = new Label
        {
            Text = "Select a tenant row to manage their subscription.",
            Font = new Font("Segoe UI", 9F),
            ForeColor = ThemeHelper.TextSecondary,
            AutoSize = true,
            Location = new Point(20, 632)
        };
        this.Controls.Add(lblStatus);
    }

    private void BtnAddTenant_Click(object? sender, EventArgs e)
    {
        using var dialog = new AddTenantDialog();
        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            LoadSubscriptions();
        }
    }

    private void AddSummaryCard(Panel parent, int x, string title, Color accent, out Label valueLabel)
    {
        var card = new Panel
        {
            Location = new Point(x, 0),
            Size = new Size(230, 82),
            BackColor = ThemeHelper.CardBackground,
            BorderStyle = BorderStyle.FixedSingle
        };

        var lblTitle = new Label
        {
            Text = title,
            Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
            ForeColor = ThemeHelper.TextSecondary,
            AutoSize = true,
            Location = new Point(12, 10)
        };

        var bar = new Panel { Location = new Point(0, 0), Size = new Size(4, 82), BackColor = accent };

        valueLabel = new Label
        {
            Text = "—",
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            ForeColor = accent,
            AutoSize = true,
            Location = new Point(12, 35)
        };

        card.Controls.AddRange(new Control[] { bar, lblTitle, valueLabel });
        parent.Controls.Add(card);
    }

    private Button MakeButton(string text, Color color, int x) => new Button
    {
        Text = text,
        Location = new Point(x, 8),
        Size = new Size(150, 36),
        BackColor = color,
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
        Cursor = Cursors.Hand,
        FlatAppearance = { BorderSize = 0 }
    };

    private void LoadSubscriptions()
    {
        var subs = SqlDataRepository.Instance.GetTenantSubscriptions();

        dgvSubscriptions.DataSource = subs.Select(s => new
        {
            s.TenantID,
            Company         = s.CompanyName,
            Database        = s.DatabaseName,
            Plan            = s.PlanName,
            Fee             = $"₱{s.MonthlyFee:N2}/mo",
            Expiry          = s.ExpiryDate.ToString("MMM dd, yyyy"),
            DaysLeft        = s.DaysUntilExpiry > 0 ? $"{s.DaysUntilExpiry} days" : "EXPIRED",
            Status          = s.Status,
            PaymentStatus   = s.PaymentStatus,
            PaymentMethod   = s.PaymentMethod,
            LastPaid        = s.LastPaidDate.HasValue ? s.LastPaidDate.Value.ToString("MMM dd, yyyy") : "—",
        }).ToList();

        // Color-code rows
        foreach (DataGridViewRow row in dgvSubscriptions.Rows)
        {
            string status = row.Cells["Status"].Value?.ToString() ?? "";
            row.DefaultCellStyle.BackColor = status switch
            {
                "Suspended" => Color.FromArgb(255, 235, 230),
                "Expired"   => Color.FromArgb(255, 245, 230),
                _           => Color.White
            };
        }

        // Update summary cards
        lblActiveCount.Text    = subs.Count(s => s.IsActive).ToString();
        lblExpiredCount.Text   = subs.Count(s => s.Status == "Expired").ToString();
        lblSuspendedCount.Text = subs.Count(s => s.Status == "Suspended").ToString();
        lblRevenue.Text        = $"₱{subs.Where(s => s.IsActive).Sum(s => s.MonthlyFee):N0}";

        _selected = null;
        lblStatus.Text = $"Loaded {subs.Count} tenant subscription(s).";
        btnActivate.Enabled = btnRenew.Enabled = btnSuspend.Enabled = btnExpire.Enabled = false;
    }

    private void DgvSubscriptions_SelectionChanged(object? sender, EventArgs e)
    {
        if (dgvSubscriptions.CurrentRow?.DataBoundItem == null) return;

        dynamic item = dgvSubscriptions.CurrentRow.DataBoundItem;
        int tenantId = item.TenantID;
        var subs = SqlDataRepository.Instance.GetTenantSubscriptions();
        _selected = subs.FirstOrDefault(s => s.TenantID == tenantId);

        if (_selected != null)
        {
            lblStatus.Text = $"Selected: {_selected.CompanyName}  |  Plan: {_selected.PlanName}  |  Status: {_selected.Status}  |  Expires: {_selected.ExpiryDate:MMM dd, yyyy}";
            btnActivate.Enabled = btnRenew.Enabled = btnSuspend.Enabled = btnExpire.Enabled = true;
        }
    }

    private void BtnActivate_Click(object? sender, EventArgs e)
    {
        if (_selected == null) return;
        var result = MessageBox.Show(
            $"Restore ACTIVE access for:\n{_selected.CompanyName}?\n\nThis will re-enable their system login immediately.",
            "Confirm Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result != DialogResult.Yes) return;

        SqlDataRepository.Instance.UpdateSubscriptionStatus(_selected.TenantID, "Active", "Manually activated by SuperAdmin.");
        MessageBox.Show("Tenant access has been RESTORED successfully.", "Activated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadSubscriptions();
    }

    private void BtnRenew_Click(object? sender, EventArgs e)
    {
        if (_selected == null) return;

        using var dlg = new RenewSubscriptionDialog(_selected);
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            SqlDataRepository.Instance.RenewSubscription(
                _selected.TenantID,
                dlg.SelectedPlan,
                dlg.SelectedFee,
                dlg.MonthsToAdd,
                dlg.SelectedPaymentMethod);
            MessageBox.Show(
                $"✅ Subscription renewed for {_selected.CompanyName}!\n\n" +
                $"Plan: {dlg.SelectedPlan}\nFee: ₱{dlg.SelectedFee:N2}/mo\nExtended by: {dlg.MonthsToAdd} month(s)\nPayment: {dlg.SelectedPaymentMethod}",
                "Renewal Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadSubscriptions();
        }
    }

    private void BtnSuspend_Click(object? sender, EventArgs e)
    {
        if (_selected == null) return;

        // Inline input dialog — no Microsoft.VisualBasic dependency needed
        string reason = string.Empty;
        using var dlg = new Form
        {
            Text = "Suspend Tenant Access",
            Size = new Size(420, 180),
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false, MinimizeBox = false,
            BackColor = ThemeHelper.WarmCanvas
        };
        var lbl = new Label { Text = $"Reason for suspending:\n{_selected.CompanyName}", AutoSize = true, Location = new Point(15, 15), Font = new Font("Segoe UI", 9F) };
        var txt = new TextBox { Location = new Point(15, 60), Size = new Size(375, 24), Text = "Unpaid subscription fee." };
        var btnOk  = new Button { Text = "Suspend", Location = new Point(15, 100), Size = new Size(100, 32), BackColor = Color.FromArgb(230, 126, 34), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.OK };
        var btnCnl = new Button { Text = "Cancel",  Location = new Point(130, 100), Size = new Size(80, 32),  BackColor = ThemeHelper.TextSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DialogResult = DialogResult.Cancel };
        btnOk.FlatAppearance.BorderSize = btnCnl.FlatAppearance.BorderSize = 0;
        dlg.AcceptButton = btnOk; dlg.CancelButton = btnCnl;
        dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCnl });

        if (dlg.ShowDialog(this) != DialogResult.OK) return;
        reason = txt.Text.Trim();
        if (string.IsNullOrWhiteSpace(reason)) return;

        SqlDataRepository.Instance.UpdateSubscriptionStatus(_selected.TenantID, "Suspended", reason);
        MessageBox.Show($"Access for {_selected.CompanyName} has been SUSPENDED.\nTenant users will be blocked from logging in.", "Suspended", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        LoadSubscriptions();
    }

    private void BtnExpire_Click(object? sender, EventArgs e)
    {
        if (_selected == null) return;
        var result = MessageBox.Show(
            $"Mark subscription as EXPIRED for:\n{_selected.CompanyName}?\n\nThis simulates a subscription that was not renewed on time.",
            "Confirm Expiry", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result != DialogResult.Yes) return;

        SqlDataRepository.Instance.UpdateSubscriptionStatus(_selected.TenantID, "Expired", "Subscription expired — awaiting renewal payment.");
        MessageBox.Show($"Subscription for {_selected.CompanyName} has been marked EXPIRED.", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadSubscriptions();
    }
}

// ─── Renewal Dialog ────────────────────────────────────────────────────────────
public class RenewSubscriptionDialog : Form
{
    public string SelectedPlan { get; private set; } = "Basic";
    public decimal SelectedFee { get; private set; } = 999m;
    public int MonthsToAdd { get; private set; } = 1;
    public string SelectedPaymentMethod { get; private set; } = "GCash";

    private ComboBox cmbPlan = null!, cmbPayment = null!;
    private NumericUpDown nudMonths = null!;
    private Label lblFeeDisplay = null!;

    private readonly decimal[] _fees = { 999m, 1499m, 1999m };
    private readonly string[] _plans = { "Basic", "Standard", "Premium" };

    public RenewSubscriptionDialog(TenantSubscription sub)
    {
        this.Text = $"Renew Subscription — {sub.CompanyName}";
        this.Size = new Size(420, 420);   // ✅ Tall enough for all controls + buttons
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false; this.MinimizeBox = false;
        this.BackColor = ThemeHelper.WarmCanvas;

        int y = 20;
        AddLabel("Subscription Plan:", y); y += 28;

        cmbPlan = new ComboBox { Location = new Point(20, y), Size = new Size(360, 26), DropDownStyle = ComboBoxStyle.DropDownList };
        cmbPlan.Items.AddRange(new object[] { "Basic — ₱999/mo", "Standard — ₱1,499/mo", "Premium — ₱1,999/mo" });
        int planIdx = Array.IndexOf(_plans, sub.PlanName);
        cmbPlan.SelectedIndex = planIdx >= 0 ? planIdx : 0;
        cmbPlan.SelectedIndexChanged += (s, e) =>
        {
            SelectedFee = _fees[cmbPlan.SelectedIndex];
            SelectedPlan = _plans[cmbPlan.SelectedIndex];
            UpdateFeeDisplay();
        };
        this.Controls.Add(cmbPlan); y += 40;

        lblFeeDisplay = new Label { Location = new Point(20, y), Size = new Size(360, 30), Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.FromArgb(52, 152, 219) };
        this.Controls.Add(lblFeeDisplay); y += 42;

        AddLabel("Extend by (months):", y); y += 28;
        nudMonths = new NumericUpDown { Location = new Point(20, y), Size = new Size(100, 26), Minimum = 1, Maximum = 12, Value = 1, Font = new Font("Segoe UI", 10F) };
        this.Controls.Add(nudMonths); y += 42;

        AddLabel("Payment Method:", y); y += 28;
        cmbPayment = new ComboBox { Location = new Point(20, y), Size = new Size(200, 26), DropDownStyle = ComboBoxStyle.DropDownList };
        cmbPayment.Items.AddRange(new object[] { "GCash", "Bank Transfer", "Cash", "Maya" });
        cmbPayment.SelectedIndex = 0;
        this.Controls.Add(cmbPayment); y += 48;

        var btnOk = new Button { Text = "✅  Confirm Renewal", Location = new Point(20, y), Size = new Size(160, 36), BackColor = Color.FromArgb(39, 174, 96), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        btnOk.FlatAppearance.BorderSize = 0;
        btnOk.Click += (s, e) =>
        {
            SelectedPlan = _plans[cmbPlan.SelectedIndex];
            SelectedFee = _fees[cmbPlan.SelectedIndex];
            MonthsToAdd = (int)nudMonths.Value;
            SelectedPaymentMethod = cmbPayment.SelectedItem?.ToString() ?? "GCash";
            this.DialogResult = DialogResult.OK;
            this.Close();
        };
        this.Controls.Add(btnOk);

        var btnCancel = new Button { Text = "Cancel", Location = new Point(200, y), Size = new Size(100, 36), BackColor = ThemeHelper.TextSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        this.Controls.Add(btnCancel);

        // Init
        SelectedFee = _fees[cmbPlan.SelectedIndex];
        SelectedPlan = _plans[cmbPlan.SelectedIndex];
        UpdateFeeDisplay();
    }

    private void AddLabel(string text, int y)
    {
        this.Controls.Add(new Label { Text = text, Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = ThemeHelper.PrimaryNavy });
    }

    private void UpdateFeeDisplay()
    {
        lblFeeDisplay.Text = $"Monthly Fee: ₱{SelectedFee:N2}";
    }
}
