using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

/// <summary>
/// Full-window appointments table with search/date/status filters and
/// Check In / Edit / Cancel actions. Cancelled and completed appointments
/// stay in history but are hidden from the default "Active" view.
/// </summary>
public class AppointmentsForm : Form
{
    private readonly User _currentUser;
    private readonly DataGridView dgvAppointments = new();
    private readonly TextBox txtSearch = new();
    private readonly DateTimePicker dtpDate = new() { Format = DateTimePickerFormat.Short };
    private readonly ComboBox cmbStatus = new();
    private readonly CheckBox chkAllDates = new();
    private readonly Button btnNew = new();
    private readonly Button btnCheckIn = new();
    private readonly Button btnEdit = new();
    private readonly Button btnCancelAppt = new();
    private List<Appointment> _appointments = new();

    public AppointmentsForm(User currentUser)
    {
        _currentUser = currentUser;
        Text = "Appointments";
        BackColor = ThemeHelper.WarmIvory;

        var toolbar = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 56,
            Padding = new Padding(16, 12, 16, 8),
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            BackColor = ThemeHelper.WarmIvory
        };

        txtSearch.Width = 180;
        txtSearch.PlaceholderText = "Search customer…";
        txtSearch.Margin = new Padding(0, 4, 8, 0);
        txtSearch.TextChanged += (s, e) => RefreshGrid();

        dtpDate.Width = 110;
        dtpDate.Margin = new Padding(0, 4, 8, 0);
        dtpDate.ValueChanged += (s, e) => RefreshGrid();

        chkAllDates.Text = "All dates";
        chkAllDates.AutoSize = true;
        chkAllDates.Margin = new Padding(0, 8, 8, 0);
        chkAllDates.ForeColor = ThemeHelper.TextPrimary;
        chkAllDates.CheckedChanged += (s, e) => RefreshGrid();

        cmbStatus.Width = 120;
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStatus.Margin = new Padding(0, 4, 8, 0);
        cmbStatus.Items.AddRange(new object[] { "Active", "All", "Scheduled", "Checked In", "Completed", "Cancelled" });
        cmbStatus.SelectedIndex = 0;
        cmbStatus.SelectedIndexChanged += (s, e) => RefreshGrid();

        ConfigureButton(btnNew, "+ Book Appointment", btnNew_Click);
        ConfigureButton(btnCheckIn, "Check In", btnCheckIn_Click);
        ConfigureButton(btnEdit, "Edit", btnEdit_Click);
        ConfigureButton(btnCancelAppt, "Cancel Appt", btnCancelAppt_Click, danger: true);

        toolbar.Controls.AddRange(new Control[] { txtSearch, dtpDate, chkAllDates, cmbStatus, btnNew, btnCheckIn, btnEdit, btnCancelAppt });

        dgvAppointments.Dock = DockStyle.Fill;
        dgvAppointments.ReadOnly = true;
        dgvAppointments.AllowUserToAddRows = false;
        dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvAppointments.MultiSelect = false;
        dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvAppointments.BackgroundColor = ThemeHelper.WarmIvory;
        dgvAppointments.SelectionChanged += (s, e) => UpdateActionState();

        Controls.Add(dgvAppointments);
        Controls.Add(toolbar);
        ThemeHelper.ApplyModernGrid(dgvAppointments);
        LoadAppointments();
        ResponsiveLayoutHelper.Apply(this);
    }

    private void ConfigureButton(Button btn, string text, EventHandler onClick, bool danger = false)
    {
        btn.Text = text;
        btn.AutoSize = true;
        btn.MinimumSize = new Size(110, 34);
        btn.Margin = new Padding(8, 0, 0, 0);
        ThemeHelper.ApplyPrimaryButton(btn);
        if (danger)
        {
            btn.BackColor = ThemeHelper.DeepBurgundy;
            btn.ForeColor = ThemeHelper.WarmIvory;
        }
        btn.Click += onClick;
    }

    /// <summary>Re-reads appointments (called when the Service Desk tab regains focus).</summary>
    public void Reload() => LoadAppointments();

    private void LoadAppointments()
    {
        try
        {
            _appointments = SqlDataRepository.Instance.GetAppointments();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not load appointments: {ex.Message}", "Appointments", MessageBoxButtons.OK, MessageBoxIcon.Error);
            _appointments = new List<Appointment>();
        }
        RefreshGrid();
    }

    private void RefreshGrid()
    {
        string q = txtSearch.Text.Trim();
        string status = cmbStatus.SelectedItem?.ToString() ?? "Active";

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

        if (q.Length > 0)
            query = query.Where(a => a.CustomerName.Contains(q, StringComparison.OrdinalIgnoreCase)
                                  || a.ServiceName.Contains(q, StringComparison.OrdinalIgnoreCase)
                                  || a.BarberName.Contains(q, StringComparison.OrdinalIgnoreCase)
                                  || a.AppointmentNumber.Contains(q, StringComparison.OrdinalIgnoreCase));

        dgvAppointments.DataSource = query
            .OrderBy(a => a.ScheduledAt)
            .Select(a => new
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

        UpdateActionState();
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
        btnCheckIn.Enabled = a?.Status == AppointmentStatus.Scheduled;
        btnEdit.Enabled = a?.Status is AppointmentStatus.Scheduled or AppointmentStatus.CheckedIn;
        btnCancelAppt.Enabled = a?.Status is AppointmentStatus.Scheduled or AppointmentStatus.CheckedIn;
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
}
