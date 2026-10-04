using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

/// <summary>
/// Full-window appointments table with improved header, filters, and contextual actions.
/// Cancelled and completed appointments stay in history but are hidden from the default "Active" view.
/// </summary>
public class AppointmentsForm : Form
{
    private readonly User _currentUser;
    public event Action? OnSwitchToQueue;
    private readonly DataGridView dgvAppointments = new();
    private readonly TextBox txtSearch = new();
    private readonly DateTimePicker dtpDate = new() { Format = DateTimePickerFormat.Short };
    private readonly ComboBox cmbStatus = new();
    private readonly ComboBox cmbBarber = new();
    private readonly CheckBox chkAllDates = new();
    private readonly Button btnNew = new();
    private readonly Button btnCheckIn = new();
    private readonly Button btnEdit = new();
    private readonly Button btnCancelAppt = new();
    private readonly Button btnViewQueue = new();
    private readonly Button btnViewTransaction = new();
    private readonly Label lblSelectedAppointment = new();
    private readonly Panel pnlHeader = new();
    private readonly Panel pnlSummary = new();
    private readonly Panel pnlFilters = new();
    private readonly Panel pnlActions = new();
    private readonly Panel pnlGridContainer = new();
    private readonly Panel pnlEmptyState = new();
    private readonly Button btnQuickToday = new();
    private readonly Button btnQuickUpcoming = new();
    private readonly Button btnQuickCompleted = new();
    private readonly Button btnQuickCancelled = new();
    private readonly Button btnQuickReset = new();
    private List<Appointment> _appointments = new();

    public AppointmentsForm(User currentUser)
    {
        _currentUser = currentUser;
        Text = "Appointments";
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
        dgvAppointments.Dock = DockStyle.Fill;
        dgvAppointments.ReadOnly = true;
        dgvAppointments.AllowUserToAddRows = false;
        dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvAppointments.MultiSelect = false;
        dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvAppointments.BackgroundColor = ThemeHelper.WarmIvory;
        dgvAppointments.SelectionChanged += (s, e) => UpdateActionState();

        pnlGridContainer.Controls.Add(pnlEmptyState);
        pnlGridContainer.Controls.Add(dgvAppointments);

        Controls.Add(pnlGridContainer);
        Controls.Add(pnlActions);
        Controls.Add(pnlFilters);
        Controls.Add(pnlSummary);
        Controls.Add(pnlHeader);
        pnlGridContainer.SendToBack();

        ThemeHelper.ApplyModernGrid(dgvAppointments);
        LoadAppointments();
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
            Text = "Customer Appointments",
            Font = new Font("Segoe UI", 13F, FontStyle.Bold),
            ForeColor = ThemeHelper.TextPrimary,
            Location = new Point(16, 4),
            AutoSize = true
        };

        var subtitleLabel = new Label
        {
            Text = "Manage scheduled customers and check-ins.",
            Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
            ForeColor = ThemeHelper.TextSecondary,
            Location = new Point(16, 24),
            AutoSize = true
        };

        btnNew.Text = "+ Book Appointment";
        btnNew.Size = new Size(140, 30);
        btnNew.Location = new Point(pnlHeader.Width - 156, 6);
        btnNew.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnNew.FlatStyle = FlatStyle.Flat;
        btnNew.FlatAppearance.BorderSize = 0;
        btnNew.BackColor = ThemeHelper.MutedGold;
        btnNew.ForeColor = ThemeHelper.DeepCharcoal;
        btnNew.Cursor = Cursors.Hand;
        btnNew.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnNew.Click += btnNew_Click;

        pnlHeader.Controls.AddRange(new Control[] { titleLabel, subtitleLabel, btnNew });
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

        var lblToday = CreateSummaryLabel("TODAY", "0");
        var lblConfirmed = CreateSummaryLabel("CONFIRMED", "0");
        var lblCheckedIn = CreateSummaryLabel("CHECKED IN", "0");
        var lblCancelled = CreateSummaryLabel("CANCELLED", "0");

        summaryLayout.Controls.Add(lblToday, 0, 0);
        summaryLayout.Controls.Add(lblConfirmed, 1, 0);
        summaryLayout.Controls.Add(lblCheckedIn, 2, 0);
        summaryLayout.Controls.Add(lblCancelled, 3, 0);

        pnlSummary.Controls.Add(summaryLayout);
        pnlSummary.Tag = new[] { lblToday, lblConfirmed, lblCheckedIn, lblCancelled };
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

        txtSearch.Width = 150;
        txtSearch.PlaceholderText = "Customer name/phone...";
        txtSearch.Margin = new Padding(0, 2, 12, 0);
        txtSearch.TextChanged += (s, e) => RefreshGrid();

        var dateLabel = new Label
        {
            Text = "Date:",
            Font = ThemeHelper.BodyFont,
            ForeColor = ThemeHelper.TextSecondary,
            AutoSize = true,
            Margin = new Padding(0, 6, 4, 0)
        };

        dtpDate.Width = 110;
        dtpDate.Margin = new Padding(0, 2, 4, 0);
        dtpDate.ValueChanged += (s, e) => RefreshGrid();

        chkAllDates.Text = "All Dates";
        chkAllDates.AutoSize = true;
        chkAllDates.Margin = new Padding(0, 6, 12, 0);
        chkAllDates.ForeColor = ThemeHelper.TextPrimary;
        chkAllDates.CheckedChanged += (s, e) => RefreshGrid();

        var statusLabel = new Label
        {
            Text = "Status:",
            Font = ThemeHelper.BodyFont,
            ForeColor = ThemeHelper.TextSecondary,
            AutoSize = true,
            Margin = new Padding(0, 6, 4, 0)
        };

        cmbStatus.Width = 100;
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDown;
        cmbStatus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        cmbStatus.AutoCompleteSource = AutoCompleteSource.ListItems;
        cmbStatus.Margin = new Padding(0, 2, 12, 0);
        cmbStatus.Items.AddRange(new object[] { "Active", "All", "Scheduled", "Checked In", "Completed", "Cancelled" });
        cmbStatus.SelectedIndex = 0;
        cmbStatus.SelectedIndexChanged += (s, e) => RefreshGrid();

        var barberLabel = new Label
        {
            Text = "Barber:",
            Font = ThemeHelper.BodyFont,
            ForeColor = ThemeHelper.TextSecondary,
            AutoSize = true,
            Margin = new Padding(0, 6, 4, 0)
        };

        cmbBarber.Width = 120;
        cmbBarber.DropDownStyle = ComboBoxStyle.DropDown;
        cmbBarber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        cmbBarber.AutoCompleteSource = AutoCompleteSource.ListItems;
        cmbBarber.Margin = new Padding(0, 2, 12, 0);
        LoadBarbers();
        cmbBarber.SelectedIndexChanged += (s, e) => RefreshGrid();

        ConfigureQuickFilterButton(btnQuickToday, "Today", () => ApplyQuickFilter("today"));
        ConfigureQuickFilterButton(btnQuickUpcoming, "Upcoming", () => ApplyQuickFilter("upcoming"));
        ConfigureQuickFilterButton(btnQuickCompleted, "Completed", () => ApplyQuickFilter("completed"));
        ConfigureQuickFilterButton(btnQuickCancelled, "Cancelled", () => ApplyQuickFilter("cancelled"));
        ConfigureQuickFilterButton(btnQuickReset, "Reset", () => ApplyQuickFilter("reset"), isReset: true);

        btnQuickToday.Margin = new Padding(2, 2, 2, 0);
        btnQuickUpcoming.Margin = new Padding(2, 2, 2, 0);
        btnQuickCompleted.Margin = new Padding(2, 2, 2, 0);
        btnQuickCancelled.Margin = new Padding(2, 2, 2, 0);
        btnQuickReset.Margin = new Padding(2, 2, 0, 0);

        flowFilters.Controls.AddRange(new Control[] {
            searchLabel, txtSearch, dateLabel, dtpDate, chkAllDates,
            statusLabel, cmbStatus, barberLabel, cmbBarber,
            btnQuickToday, btnQuickUpcoming, btnQuickCompleted, btnQuickCancelled, btnQuickReset
        });

        pnlFilters.Controls.Add(flowFilters);
    }

    private void ConfigureQuickFilterButton(Button btn, string text, Action onClick, bool isReset = false)
    {
        btn.Text = text;
        btn.Font = new Font("Segoe UI", 8.5f);
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 1;
        btn.FlatAppearance.BorderColor = ThemeHelper.WarmGray;
        btn.Cursor = Cursors.Hand;

        if (isReset)
        {
            btn.BackColor = ThemeHelper.WarmIvory;
            btn.ForeColor = ThemeHelper.TextSecondary;
        }
        else
        {
            btn.BackColor = Color.FromArgb(248, 244, 236);
            btn.ForeColor = ThemeHelper.TextPrimary;
        }

        btn.Click += (s, e) => onClick();
    }

    private void ApplyQuickFilter(string filterType)
    {
        switch (filterType.ToLower())
        {
            case "today":
                dtpDate.Value = DateTime.Today;
                chkAllDates.Checked = false;
                cmbStatus.SelectedIndex = 0; // Active
                break;
            case "upcoming":
                dtpDate.Value = DateTime.Today;
                chkAllDates.Checked = false;
                cmbStatus.SelectedIndex = 0; // Active
                break;
            case "completed":
                chkAllDates.Checked = true;
                cmbStatus.SelectedIndex = cmbStatus.Items.IndexOf("Completed");
                if (cmbStatus.SelectedIndex == -1) cmbStatus.SelectedIndex = 0;
                break;
            case "cancelled":
                chkAllDates.Checked = true;
                cmbStatus.SelectedIndex = cmbStatus.Items.IndexOf("Cancelled");
                if (cmbStatus.SelectedIndex == -1) cmbStatus.SelectedIndex = 0;
                break;
            case "reset":
                dtpDate.Value = DateTime.Today;
                chkAllDates.Checked = false;
                cmbStatus.SelectedIndex = 0; // Active
                cmbBarber.SelectedIndex = 0; // All Barbers
                txtSearch.Clear();
                break;
        }
        RefreshGrid();
    }

    private void CreateActionsPanel()
    {
        pnlActions.Dock = DockStyle.Top;
        pnlActions.Height = 44;
        pnlActions.BackColor = ThemeHelper.WarmIvory;
        pnlActions.Padding = new Padding(16, 4, 16, 4);

        lblSelectedAppointment.Text = "No appointment selected";
        lblSelectedAppointment.Font = ThemeHelper.BodyFont;
        lblSelectedAppointment.ForeColor = ThemeHelper.TextSecondary;
        lblSelectedAppointment.Location = new Point(16, 12);
        lblSelectedAppointment.AutoSize = true;

        ConfigureContextualButton(btnCheckIn, "Check In", btnCheckIn_Click);
        ConfigureContextualButton(btnEdit, "Edit Appointment", btnEdit_Click);
        ConfigureContextualButton(btnCancelAppt, "Cancel Appointment", btnCancelAppt_Click, danger: true);
        ConfigureContextualButton(btnViewQueue, "View Queue", btnViewQueue_Click);
        ConfigureContextualButton(btnViewTransaction, "View Transaction", btnViewTransaction_Click);

        var flowActions = new FlowLayoutPanel
        {
            Dock = DockStyle.Right,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            AutoSize = true,
            BackColor = ThemeHelper.WarmIvory
        };

        flowActions.Controls.AddRange(new Control[] {
            btnCheckIn, btnEdit, btnCancelAppt, btnViewQueue, btnViewTransaction
        });

        pnlActions.Controls.Add(lblSelectedAppointment);
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

    /// <summary>Re-reads appointments (called when the Service Desk tab regains focus).</summary>
    public void Reload() => LoadAppointments();

    private void LoadAppointments()
    {
        try
        {
            _appointments = SqlDataRepository.Instance.GetAppointments();
            UpdateSummaryCounts();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not load appointments: {ex.Message}", "Appointments", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _appointments = new List<Appointment>();
        }
        RefreshGrid();
    }

    private void UpdateSummaryCounts()
    {
        if (pnlSummary.Tag is not Panel[] summaryLabels) return;

        var today = DateTime.Today;
        int todayCount = _appointments.Count(a => a.ScheduledAt.Date == today);
        int confirmedCount = _appointments.Count(a => a.Status == AppointmentStatus.Scheduled && a.ScheduledAt.Date == today);
        int checkedInCount = _appointments.Count(a => a.Status == AppointmentStatus.CheckedIn && a.ScheduledAt.Date == today);
        int cancelledCount = _appointments.Count(a => a.Status == AppointmentStatus.Cancelled && a.ScheduledAt.Date == today);

        foreach (var panel in summaryLabels)
        {
            var countLabel = panel.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "countLabel");
            if (countLabel != null)
            {
                if (panel.Controls[0].Text == "TODAY") countLabel.Text = todayCount.ToString();
                if (panel.Controls[0].Text == "CONFIRMED") countLabel.Text = confirmedCount.ToString();
                if (panel.Controls[0].Text == "CHECKED IN") countLabel.Text = checkedInCount.ToString();
                if (panel.Controls[0].Text == "CANCELLED") countLabel.Text = cancelledCount.ToString();
            }
        }
    }

    private void RefreshGrid()
    {
        string q = txtSearch.Text.Trim();
        string status = cmbStatus.SelectedItem?.ToString() ?? "Active";
        string barber = cmbBarber.SelectedItem?.ToString() ?? "All Barbers";

        IEnumerable<Appointment> query = _appointments;

        if (!chkAllDates.Checked)
            query = query.Where(a => a.ScheduledAt.Date == dtpDate.Value.Date);

        query = status switch
        {
            "Active" => query.Where(a => a.Status is AppointmentStatus.Scheduled or AppointmentStatus.CheckedIn),
            "All" => query,
            "Checked In" => query.Where(a => a.Status == AppointmentStatus.CheckedIn),
            var s => query.Where(a => a.Status.ToString() == s.Replace(" ", ""))
        };

        if (barber != "All Barbers")
            query = query.Where(a => a.BarberName == barber);

        if (q.Length > 0)
            query = query.Where(a => a.CustomerName.StartsWith(q, StringComparison.OrdinalIgnoreCase)
                                  || a.ServiceName.StartsWith(q, StringComparison.OrdinalIgnoreCase)
                                  || a.BarberName.StartsWith(q, StringComparison.OrdinalIgnoreCase)
                                  || a.AppointmentNumber.StartsWith(q, StringComparison.OrdinalIgnoreCase));

        var results = query.OrderBy(a => a.ScheduledAt).ToList();

        if (results.Count == 0)
        {
            ShowEmptyState();
        }
        else
        {
            pnlEmptyState.Visible = false;
            dgvAppointments.Visible = true;
            dgvAppointments.DataSource = results.Select(a => new
            {
                a.Id,
                a.AppointmentNumber,
                Time = a.ScheduledAt.ToString("MMM d, h:mm tt"),
                a.CustomerName,
                a.ServiceName,
                a.BarberName,
                Status = a.Status == AppointmentStatus.CheckedIn ? "Checked In" : a.Status.ToString(),
                a.Notes
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
            Text = "No appointments scheduled",
            Font = new Font("Segoe UI", 13F, FontStyle.Bold),
            ForeColor = ThemeHelper.TextPrimary,
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 35
        };

        var emptySubLabel = new Label
        {
            Text = "There are no appointments matching the selected criteria.",
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

        var btnBookNew = new Button
        {
            Text = "+ Book Appointment",
            Size = new Size(150, 32),
            Margin = new Padding(0, 0, 8, 0),
            BackColor = ThemeHelper.MutedGold,
            ForeColor = ThemeHelper.DeepCharcoal,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            Cursor = Cursors.Hand
        };
        btnBookNew.Click += btnNew_Click;

        var btnViewUpcoming = new Button
        {
            Text = "View Upcoming",
            Size = new Size(130, 32),
            BackColor = ThemeHelper.DeepCharcoal,
            ForeColor = ThemeHelper.WarmIvory,
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            Cursor = Cursors.Hand
        };
        btnViewUpcoming.Click += (s, e) =>
        {
            cmbStatus.SelectedIndex = 0; // Active
            chkAllDates.Checked = false;
            dtpDate.Value = DateTime.Today;
            RefreshGrid();
        };

        emptyActionsPanel.Controls.AddRange(new Control[] { btnBookNew, btnViewUpcoming });
        pnlEmptyState.Controls.Add(emptyActionsPanel);
        pnlEmptyState.Controls.Add(emptySubLabel);
        pnlEmptyState.Controls.Add(emptyLabel);
    }

    private void ShowEmptyState()
    {
        dgvAppointments.Visible = false;
        pnlEmptyState.Visible = true;
    }

    private Appointment? Selected()
    {
        if (dgvAppointments.CurrentRow?.DataBoundItem == null) return null;
        int id = (int)((dynamic)dgvAppointments.CurrentRow.DataBoundItem).Id;
        return _appointments.FirstOrDefault(a => a.Id == id);
    }

    private void UpdateActionState()
    {
        var a = Selected();

        if (a == null)
        {
            lblSelectedAppointment.Text = "No appointment selected";
            btnCheckIn.Enabled = false;
            btnEdit.Enabled = false;
            btnCancelAppt.Enabled = false;
            btnViewQueue.Enabled = false;
            btnViewTransaction.Enabled = false;
            return;
        }

        // Show selected appointment summary
        lblSelectedAppointment.Text = $"{a.CustomerName} • {a.ServiceName} • {a.ScheduledAt:MMM d, h:mm tt} • {a.BarberName} • {a.Status}";

        // Contextual actions based on status
        btnCheckIn.Enabled = a.Status == AppointmentStatus.Scheduled;
        btnEdit.Enabled = a.Status is AppointmentStatus.Scheduled or AppointmentStatus.CheckedIn;
        btnCancelAppt.Enabled = a.Status is AppointmentStatus.Scheduled or AppointmentStatus.CheckedIn;
        btnViewQueue.Enabled = a.Status == AppointmentStatus.CheckedIn;
        btnViewTransaction.Enabled = a.Status == AppointmentStatus.Completed;
    }

    private void btnNew_Click(object? sender, EventArgs e)
    {
        using var dlg = new AppointmentEditForm();
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            SqlDataRepository.Instance.AddAppointment(dlg.Appointment);
            SqlDataRepository.Instance.AddSystemLog("INFO", "Appointments",
                $"Booked appointment '{dlg.Appointment.AppointmentNumber}' for '{dlg.Appointment.CustomerName}' at {dlg.Appointment.ScheduledAt:g}",
                _currentUser.Username);
            LoadAppointments();
        }
    }

    private void btnCheckIn_Click(object? sender, EventArgs e)
    {
        if (Selected() is not { } a) return;
        try
        {
            var txn = SqlDataRepository.Instance.CheckInAppointment(a, _currentUser);
            MessageBox.Show($"{a.CustomerName} checked in and added to the queue as {txn.TransactionNumber}.",
                "Checked In", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAppointments();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Check-in failed: {ex.Message}", "Check In", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnEdit_Click(object? sender, EventArgs e)
    {
        if (Selected() is not { } a) return;
        using var dlg = new AppointmentEditForm(a);
        if (dlg.ShowDialog(this) == DialogResult.OK)
        {
            SqlDataRepository.Instance.UpdateAppointment(a);
            LoadAppointments();
        }
    }

    private void btnCancelAppt_Click(object? sender, EventArgs e)
    {
        if (Selected() is not { } a) return;
        if (MessageBox.Show($"Cancel appointment {a.AppointmentNumber} for {a.CustomerName}?\nIt will be kept in history.",
                "Cancel Appointment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            SqlDataRepository.Instance.UpdateAppointmentStatus(a.Id, AppointmentStatus.Cancelled);
            LoadAppointments();
        }
    }

    private void btnViewQueue_Click(object? sender, EventArgs e)
    {
        // Navigate to Queue tab
        OnSwitchToQueue?.Invoke();
    }

    private void btnViewTransaction_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("This would show the transaction details for this completed appointment.", "View Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}