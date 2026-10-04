using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

/// <summary>
/// Live service queue: today's transactions that are Waiting, Called, or In Service.
/// Actions depend on the selected entry's status with proper empty states.
/// </summary>
public class QueueForm : Form
{
    private readonly User _currentUser;
    public event Action? OnSwitchToNewService;
    public event Action? OnSwitchToAppointments;
    private readonly DataGridView dgvQueue = new();
    private readonly TextBox txtSearch = new();
    private readonly ComboBox cmbBarber = new();
    private readonly ComboBox cmbStatus = new();
    private readonly Button btnCall = new();
    private readonly Button btnCallCustomer = new();
    private readonly Button btnStartService = new();
    private readonly Button btnChangeBarber = new();
    private readonly Button btnCompleteService = new();
    private readonly Button btnProcessPayment = new();
    private readonly Button btnCancel = new();
    private readonly Button btnRefresh = new();
    private readonly Label lblSummary = new();
    private readonly Label lblSelectedQueue = new();
    private readonly Panel pnlHeader = new();
    private readonly Panel pnlSummary = new();
    private readonly Panel pnlFilters = new();
    private readonly Panel pnlActions = new();
    private readonly Panel pnlGridContainer = new();
    private readonly Panel pnlEmptyState = new();
    private readonly Button btnCallNextHeader = new();
    private List<Transaction> _queue = new();

    public QueueForm(User currentUser)
    {
        _currentUser = currentUser;
        Text = "Service Queue";
        BackColor = ThemeHelper.WarmIvory;

        // Create header panel
        CreateHeaderPanel();

        // Create summary panel
        CreateSummaryPanel();

        // Create filters panel
        CreateFiltersPanel();

        // Create actions panel
        CreateActionsPanel();

        // Create empty state panel
        CreateEmptyStatePanel();

        // Configure Grid Container
        pnlGridContainer.Dock = DockStyle.Fill;
        pnlGridContainer.BackColor = ThemeHelper.WarmIvory;

        // Configure DataGridView
        dgvQueue.Dock = DockStyle.Fill;
        dgvQueue.ReadOnly = true;
        dgvQueue.AllowUserToAddRows = false;
        dgvQueue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvQueue.MultiSelect = false;
        dgvQueue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvQueue.BackgroundColor = ThemeHelper.WarmIvory;
        dgvQueue.SelectionChanged += (s, e) => UpdateActionState();

        pnlGridContainer.Controls.Add(pnlEmptyState);
        pnlGridContainer.Controls.Add(dgvQueue);

        Controls.Add(pnlGridContainer);
        Controls.Add(pnlActions);
        Controls.Add(pnlFilters);
        Controls.Add(pnlSummary);
        Controls.Add(pnlHeader);
        pnlGridContainer.SendToBack();

        ThemeHelper.ApplyModernGrid(dgvQueue);
        LoadQueueAsync();
        ResponsiveLayoutHelper.Apply(this);
    }

    private void CreateHeaderPanel()
    {
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Height = 45;
        pnlHeader.BackColor = ThemeHelper.WarmIvory;
        pnlHeader.Padding = new Padding(16, 6, 16, 4);

        var titleLabel = new Label
        {
            Text = "Service Queue",
            Font = new Font("Segoe UI", 13F, FontStyle.Bold),
            ForeColor = ThemeHelper.TextPrimary,
            Location = new Point(16, 4),
            AutoSize = true
        };

        var subtitleLabel = new Label
        {
            Text = "Track customers waiting and currently being served.",
            Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
            ForeColor = ThemeHelper.TextSecondary,
            Location = new Point(16, 24),
            AutoSize = true
        };

        btnCallNextHeader.Text = "📢 Call Next Customer";
        btnCallNextHeader.Size = new Size(160, 30);
        btnCallNextHeader.Location = new Point(pnlHeader.Width - 176, 6);
        btnCallNextHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnCallNextHeader.FlatStyle = FlatStyle.Flat;
        btnCallNextHeader.FlatAppearance.BorderSize = 0;
        btnCallNextHeader.BackColor = ThemeHelper.MutedGold;
        btnCallNextHeader.ForeColor = ThemeHelper.DeepCharcoal;
        btnCallNextHeader.Cursor = Cursors.Hand;
        btnCallNextHeader.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCallNextHeader.Click += btnCall_Click;

        pnlHeader.Controls.AddRange(new Control[] { titleLabel, subtitleLabel, btnCallNextHeader });
    }

    private void CreateSummaryPanel()
    {
        pnlSummary.Dock = DockStyle.Top;
        pnlSummary.Height = 46;
        pnlSummary.BackColor = Color.FromArgb(248, 244, 236);
        pnlSummary.Padding = new Padding(16, 2, 16, 2);

        var summaryLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 1,
            BackColor = Color.FromArgb(248, 244, 236)
        };

        for (int i = 0; i < 4; i++)
        {
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
        }

        var lblWaiting = CreateSummaryLabel("WAITING", "0");
        var lblInService = CreateSummaryLabel("IN SERVICE", "0");
        var lblCompleted = CreateSummaryLabel("COMPLETED TODAY", "0");
        var lblActiveQueue = CreateSummaryLabel("ACTIVE QUEUE", "0");

        summaryLayout.Controls.Add(lblWaiting, 0, 0);
        summaryLayout.Controls.Add(lblInService, 1, 0);
        summaryLayout.Controls.Add(lblCompleted, 2, 0);
        summaryLayout.Controls.Add(lblActiveQueue, 3, 0);

        pnlSummary.Controls.Add(summaryLayout);
        pnlSummary.Tag = new[] { lblWaiting, lblInService, lblCompleted, lblActiveQueue };
    }

    private Panel CreateSummaryLabel(string title, string count)
    {
        var panel = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(248, 244, 236),
            Padding = new Padding(8, 2, 8, 2)
        };

        var titleLabel = new Label
        {
            Text = title,
            Font = new Font("Segoe UI", 8F, FontStyle.Bold),
            ForeColor = ThemeHelper.WarmGray,
            Location = new Point(8, 3),
            AutoSize = true
        };

        var countLabel = new Label
        {
            Text = count,
            Name = "countLabel",
            Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
            ForeColor = ThemeHelper.MutedGold,
            Location = new Point(8, 18),
            AutoSize = true
        };

        panel.Controls.AddRange(new Control[] { titleLabel, countLabel });
        return panel;
    }

    private void CreateFiltersPanel()
    {
        pnlFilters.Dock = DockStyle.Top;
        pnlFilters.Height = 44;
        pnlFilters.BackColor = ThemeHelper.WarmIvory;
        pnlFilters.Padding = new Padding(16, 6, 16, 6);

        var flowFilters = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            BackColor = ThemeHelper.WarmIvory
        };

        var searchLabel = new Label
        {
            Text = "Search:",
            Font = ThemeHelper.BodyFont,
            ForeColor = ThemeHelper.TextSecondary,
            AutoSize = true,
            Margin = new Padding(0, 6, 4, 0)
        };

        txtSearch.Width = 160;
        txtSearch.PlaceholderText = "Customer name...";
        txtSearch.Margin = new Padding(0, 2, 16, 0);
        txtSearch.TextChanged += (s, e) => RefreshGrid();

        var barberLabel = new Label
        {
            Text = "Barber:",
            Font = ThemeHelper.BodyFont,
            ForeColor = ThemeHelper.TextSecondary,
            AutoSize = true,
            Margin = new Padding(0, 6, 4, 0)
        };

        cmbBarber.Width = 140;
        cmbBarber.DropDownStyle = ComboBoxStyle.DropDown;
        cmbBarber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        cmbBarber.AutoCompleteSource = AutoCompleteSource.ListItems;
        cmbBarber.Margin = new Padding(0, 2, 16, 0);
        LoadBarbers();
        cmbBarber.SelectedIndexChanged += (s, e) => RefreshGrid();

        var statusLabel = new Label
        {
            Text = "Status:",
            Font = ThemeHelper.BodyFont,
            ForeColor = ThemeHelper.TextSecondary,
            AutoSize = true,
            Margin = new Padding(0, 6, 4, 0)
        };

        cmbStatus.Width = 110;
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDown;
        cmbStatus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        cmbStatus.AutoCompleteSource = AutoCompleteSource.ListItems;
        cmbStatus.Margin = new Padding(0, 2, 16, 0);
        cmbStatus.Items.AddRange(new object[] { "All", "Waiting", "Called", "In Service" });
        cmbStatus.SelectedIndex = 0;
        cmbStatus.SelectedIndexChanged += (s, e) => RefreshGrid();

        btnRefresh.Text = "↻ Refresh";
        btnRefresh.Size = new Size(85, 28);
        btnRefresh.Margin = new Padding(0, 2, 0, 0);
        btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.FlatAppearance.BorderSize = 0;
        btnRefresh.BackColor = ThemeHelper.DeepCharcoal;
        btnRefresh.ForeColor = ThemeHelper.WarmIvory;
        btnRefresh.Cursor = Cursors.Hand;
        btnRefresh.Click += (s, e) => LoadQueueAsync();

        flowFilters.Controls.AddRange(new Control[] { searchLabel, txtSearch, barberLabel, cmbBarber, statusLabel, cmbStatus, btnRefresh });
        pnlFilters.Controls.Add(flowFilters);
    }

    private void CreateActionsPanel()
    {
        pnlActions.Dock = DockStyle.Top;
        pnlActions.Height = 44;
        pnlActions.BackColor = ThemeHelper.WarmIvory;
        pnlActions.Padding = new Padding(16, 4, 16, 4);

        lblSelectedQueue.Text = "No queue entry selected";
        lblSelectedQueue.Font = ThemeHelper.BodyFont;
        lblSelectedQueue.ForeColor = ThemeHelper.TextSecondary;
        lblSelectedQueue.Location = new Point(16, 12);
        lblSelectedQueue.AutoSize = true;

        ConfigureContextualButton(btnCall, "Call Next", btnCall_Click);
        ConfigureContextualButton(btnCallCustomer, "Call Customer", btnCallCustomer_Click);
        ConfigureContextualButton(btnStartService, "Start Service", btnStartService_Click);
        ConfigureContextualButton(btnChangeBarber, "Change Barber", btnChangeBarber_Click);
        ConfigureContextualButton(btnCompleteService, "Complete Service", btnCompleteService_Click);
        ConfigureContextualButton(btnProcessPayment, "Process Payment", btnProcessPayment_Click);
        ConfigureContextualButton(btnCancel, "Cancel Queue Entry", btnCancel_Click, danger: true);

        var flowActions = new FlowLayoutPanel
        {
            Dock = DockStyle.Right,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            AutoSize = true,
            BackColor = ThemeHelper.WarmIvory
        };

        flowActions.Controls.AddRange(new Control[] {
            btnCall, btnCallCustomer, btnStartService, btnChangeBarber, btnCompleteService, btnProcessPayment, btnCancel
        });

        pnlActions.Controls.Add(lblSelectedQueue);
        pnlActions.Controls.Add(flowActions);
    }

    private void ConfigureContextualButton(Button btn, string text, EventHandler onClick, bool danger = false)
    {
        btn.Text = text;
        btn.AutoSize = true;
        btn.MinimumSize = new Size(90, 30);
        btn.Padding = new Padding(8, 2, 8, 2);
        btn.Margin = new Padding(3, 0, 3, 0);
        btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.Cursor = Cursors.Hand;
        btn.Enabled = false; // Initially disabled until selection

        if (danger)
        {
            btn.BackColor = ThemeHelper.DeepBurgundy;
            btn.ForeColor = ThemeHelper.WarmIvory;
        }
        else
        {
            btn.BackColor = ThemeHelper.MutedGold;
            btn.ForeColor = ThemeHelper.DeepCharcoal;
        }

        btn.Click += onClick;
    }

    private void LoadBarbers()
    {
        try
        {
            var employees = SqlDataRepository.Instance.GetEmployees()
                .Where(e => e.Position == EmployeePosition.Barber && e.IsActive)
                .ToList();

            cmbBarber.Items.Clear();
            cmbBarber.Items.Add("All Barbers");
            foreach (var emp in employees)
            {
                cmbBarber.Items.Add(emp.Name);
            }
            cmbBarber.SelectedIndex = 0;
        }
        catch
        {
            cmbBarber.Items.Clear();
            cmbBarber.Items.Add("All Barbers");
            cmbBarber.SelectedIndex = 0;
        }
    }

    /// <summary>Re-reads the queue (called when the Service Desk tab regains focus).</summary>
    public void Reload() => LoadQueueAsync();

    private async void LoadQueueAsync()
    {
        _queue = await Task.Run(() => SqlDataRepository.Instance.GetTodayTransactions()
            .Where(t => t.Status is TransactionStatus.Waiting or TransactionStatus.Called or TransactionStatus.InService)
            .OrderBy(t => t.TransactionDate)
            .ToList());

        await RefreshSummaryAsync();
        RefreshGrid();
    }

    private async Task RefreshSummaryAsync()
    {
        try
        {
            var todayTxns = await Task.Run(() => SqlDataRepository.Instance.GetTodayTransactions());
            int waiting = todayTxns.Count(t => t.Status == TransactionStatus.Waiting || t.Status == TransactionStatus.Called);
            int inService = todayTxns.Count(t => t.Status == TransactionStatus.InService);
            int completedToday = todayTxns.Count(t => t.Status == TransactionStatus.Completed);
            int activeQueue = waiting + inService;

            if (pnlSummary.Tag is Panel[] labels && labels.Length == 4)
            {
                UpdateSummaryValue(labels[0], waiting.ToString());
                UpdateSummaryValue(labels[1], inService.ToString());
                UpdateSummaryValue(labels[2], completedToday.ToString());
                UpdateSummaryValue(labels[3], activeQueue.ToString());
            }
        }
        catch { }
    }

    private void UpdateSummaryValue(Panel card, string value)
    {
        foreach (Control ctrl in card.Controls)
        {
            if (ctrl.Name == "countLabel")
            {
                ctrl.Text = value;
            }
        }
    }

    private void RefreshGrid()
    {
        string search = txtSearch.Text.Trim();
        string barber = cmbBarber.SelectedItem?.ToString() ?? "All Barbers";
        string status = cmbStatus.SelectedItem?.ToString() ?? "All";

        IEnumerable<Transaction> query = _queue;

        if (barber != "All Barbers")
            query = query.Where(t => t.BarberName == barber);

        if (status != "All")
        {
            query = status switch
            {
                "Waiting" => query.Where(t => t.Status == TransactionStatus.Waiting),
                "Called" => query.Where(t => t.Status == TransactionStatus.Called),
                "In Service" => query.Where(t => t.Status == TransactionStatus.InService),
                _ => query
            };
        }

        if (search.Length > 0)
            query = query.Where(t => t.CustomerName.StartsWith(search, StringComparison.OrdinalIgnoreCase)
                                  || t.ServiceName.StartsWith(search, StringComparison.OrdinalIgnoreCase)
                                  || t.TransactionNumber.StartsWith(search, StringComparison.OrdinalIgnoreCase));

        var results = query.OrderBy(t => t.TransactionDate).ToList();

        if (results.Count == 0)
        {
            ShowEmptyState();
        }
        else
        {
            pnlEmptyState.Visible = false;
            dgvQueue.Visible = true;
            dgvQueue.DataSource = results.Select(t => new
            {
                t.Id,
                QueueNo = t.TransactionNumber,
                t.CustomerName,
                t.ServiceName,
                t.BarberName,
                Waiting = FormatWaiting(t.TransactionDate),
                Status = t.Status == TransactionStatus.InService ? "In Service" : t.Status.ToString()
            }).ToList();
        }

        UpdateActionState();
    }

    private void CreateEmptyStatePanel()
    {
        pnlEmptyState.Dock = DockStyle.Fill;
        pnlEmptyState.BackColor = ThemeHelper.WarmIvory;
        pnlEmptyState.Visible = false;

        var emptyLabel = new Label
        {
            Text = "No customers in queue",
            Font = new Font("Segoe UI", 13F, FontStyle.Bold),
            ForeColor = ThemeHelper.TextPrimary,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 35
        };

        var emptySubLabel = new Label
        {
            Text = "There are currently no customers waiting or being served.",
            Font = ThemeHelper.BodyFont,
            ForeColor = ThemeHelper.TextSecondary,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 30
        };

        var emptyActionsPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.LeftToRight,
            Dock = DockStyle.Top,
            Height = 40,
            Padding = new Padding(16, 4, 0, 0),
            BackColor = ThemeHelper.WarmIvory
        };

        var btnStartNew = new Button
        {
            Text = "Start New Service",
            Size = new Size(140, 32),
            Margin = new Padding(0, 0, 8, 0),
            BackColor = ThemeHelper.MutedGold,
            ForeColor = ThemeHelper.DeepCharcoal,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            Cursor = Cursors.Hand
        };
        btnStartNew.Click += (s, e) => OnSwitchToNewService?.Invoke();

        var btnViewAppointments = new Button
        {
            Text = "View Appointments",
            Size = new Size(140, 32),
            BackColor = ThemeHelper.DeepCharcoal,
            ForeColor = ThemeHelper.WarmIvory,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            Cursor = Cursors.Hand
        };
        btnViewAppointments.Click += (s, e) => OnSwitchToAppointments?.Invoke();

        emptyActionsPanel.Controls.AddRange(new Control[] { btnStartNew, btnViewAppointments });
        pnlEmptyState.Controls.Add(emptyActionsPanel);
        pnlEmptyState.Controls.Add(emptySubLabel);
        pnlEmptyState.Controls.Add(emptyLabel);
    }

    private void ShowEmptyState()
    {
        dgvQueue.Visible = false;
        pnlEmptyState.Visible = true;
    }

    private static string FormatWaiting(DateTime since)
    {
        var span = DateTime.Now - since;
        return span.TotalMinutes < 1 ? "just now" : $"{(int)span.TotalMinutes} min";
    }

    private Transaction? SelectedEntry()
    {
        if (dgvQueue.CurrentRow?.DataBoundItem == null) return null;
        int id = (int)((dynamic)dgvQueue.CurrentRow.DataBoundItem).Id;
        return _queue.FirstOrDefault(t => t.Id == id);
    }

    private void UpdateActionState()
    {
        var t = SelectedEntry();

        if (t == null)
        {
            lblSelectedQueue.Text = "No queue entry selected";
            btnCall.Enabled = true; // Always enabled to call next
            btnCallCustomer.Enabled = false;
            btnStartService.Enabled = false;
            btnChangeBarber.Enabled = false;
            btnCompleteService.Enabled = false;
            btnProcessPayment.Enabled = false;
            btnCancel.Enabled = false;
            return;
        }

        // Show selected queue entry summary
        lblSelectedQueue.Text = $"{t.CustomerName} • {t.ServiceName} • {t.TransactionNumber} • {t.BarberName} • {t.Status}";

        // Contextual actions based on status
        btnCall.Enabled = false; // Disabled when specific entry selected
        btnCallCustomer.Enabled = t.Status == TransactionStatus.Waiting;
        btnStartService.Enabled = t.Status is TransactionStatus.Waiting or TransactionStatus.Called;
        btnChangeBarber.Enabled = t.Status is TransactionStatus.Waiting or TransactionStatus.Called;
        btnCompleteService.Enabled = t.Status == TransactionStatus.InService;
        btnProcessPayment.Enabled = t.Status is TransactionStatus.InService or TransactionStatus.Completed;
        btnCancel.Enabled = t.Status is TransactionStatus.Waiting or TransactionStatus.Called or TransactionStatus.InService;
    }

    private void SetStatus(Transaction txn, TransactionStatus status)
    {
        txn.Status = status;
        SqlDataRepository.Instance.SaveTransaction(txn);
        LoadQueueAsync();
    }

    private void btnCall_Click(object? sender, EventArgs e)
    {
        // Call next waiting customer
        var nextWaiting = _queue.FirstOrDefault(t => t.Status == TransactionStatus.Waiting);
        if (nextWaiting != null)
        {
            SetStatus(nextWaiting, TransactionStatus.Called);
            MessageBox.Show($"Called {nextWaiting.CustomerName} for service.", "Called", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("No customers waiting to be called.", "No Waiting Customers", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnCallCustomer_Click(object? sender, EventArgs e)
    {
        if (SelectedEntry() is { } t) SetStatus(t, TransactionStatus.Called);
    }

    private void btnStartService_Click(object? sender, EventArgs e)
    {
        if (SelectedEntry() is { } t) SetStatus(t, TransactionStatus.InService);
    }

    private void btnChangeBarber_Click(object? sender, EventArgs e)
    {
        if (SelectedEntry() is not { } t) return;

        var barbers = SqlDataRepository.Instance.GetEmployees()
            .Where(emp => emp.Position == EmployeePosition.Barber && emp.IsActive)
            .ToList();

        if (barbers.Count == 0)
        {
            MessageBox.Show("No active barbers available to reassign.", "Change Barber", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var dialog = new Form
        {
            Text = "Change Barber Assignment",
            Size = new Size(360, 200),
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            BackColor = ThemeHelper.WarmIvory
        };

        var lblPrompt = new Label
        {
            Text = $"Reassign barber for {t.CustomerName} ({t.TransactionNumber}):",
            Font = ThemeHelper.BodyFont,
            ForeColor = ThemeHelper.TextPrimary,
            Location = new Point(20, 16),
            Size = new Size(310, 36)
        };

        var cmbNewBarber = new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDown,
            AutoCompleteMode = AutoCompleteMode.SuggestAppend,
            AutoCompleteSource = AutoCompleteSource.ListItems,
            Location = new Point(20, 56),
            Size = new Size(300, 28),
            Font = ThemeHelper.BodyFont
        };

        foreach (var b in barbers)
        {
            cmbNewBarber.Items.Add(b);
        }
        cmbNewBarber.DisplayMember = "Name";

        var selectedIndex = barbers.FindIndex(b => b.Id == t.BarberId);
        cmbNewBarber.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;

        var btnConfirm = new Button
        {
            Text = "Assign Barber",
            DialogResult = DialogResult.OK,
            Location = new Point(110, 106),
            Size = new Size(120, 32),
            BackColor = ThemeHelper.MutedGold,
            ForeColor = ThemeHelper.DeepCharcoal,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
        btnConfirm.FlatAppearance.BorderSize = 0;

        var btnCancelDlg = new Button
        {
            Text = "Cancel",
            DialogResult = DialogResult.Cancel,
            Location = new Point(240, 106),
            Size = new Size(80, 32),
            BackColor = ThemeHelper.DeepCharcoal,
            ForeColor = ThemeHelper.WarmIvory,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
        };
        btnCancelDlg.FlatAppearance.BorderSize = 0;

        dialog.Controls.AddRange(new Control[] { lblPrompt, cmbNewBarber, btnConfirm, btnCancelDlg });

        if (dialog.ShowDialog(this) == DialogResult.OK && cmbNewBarber.SelectedItem is Employee newBarber)
        {
            t.BarberId = newBarber.Id;
            t.BarberName = newBarber.Name;
            SqlDataRepository.Instance.SaveTransaction(t);
            SqlDataRepository.Instance.AddSystemLog("INFO", "Queue",
                $"Reassigned queue entry '{t.TransactionNumber}' to Barber '{newBarber.Name}'",
                _currentUser.Username);
            LoadQueueAsync();
            MessageBox.Show($"Barber updated to {newBarber.Name} for {t.CustomerName}.", "Barber Reassigned", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void btnCompleteService_Click(object? sender, EventArgs e)
    {
        if (SelectedEntry() is { } t)
        {
            if (MessageBox.Show($"Complete service for {t.CustomerName}?", "Complete Service",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SetStatus(t, TransactionStatus.Completed);
            }
        }
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        if (SelectedEntry() is { } t &&
            MessageBox.Show($"Cancel queue entry {t.TransactionNumber} for {t.CustomerName}?", "Cancel",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            SetStatus(t, TransactionStatus.Cancelled);
        }
    }

    private void btnProcessPayment_Click(object? sender, EventArgs e)
    {
        if (SelectedEntry() is not { } t) return;
        if (t.CustomerId.HasValue && SqlDataRepository.Instance.GetCustomerById(t.CustomerId.Value) is { IsLoyaltyMember: true })
            t.PointsEarned = 10;
        using var payModal = new PaymentForm(t);
        if (payModal.ShowDialog(this) == DialogResult.OK)
        {
            SqlDataRepository.Instance.SaveTransaction(payModal.CompletedTransaction);
            SqlDataRepository.Instance.AddSystemLog("INFO", "Payments",
                $"Processed payment for '{payModal.CompletedTransaction.TransactionNumber}' (₱{payModal.CompletedTransaction.FinalAmount:N2})",
                _currentUser.Username);
            LoadQueueAsync();
        }
    }
}
