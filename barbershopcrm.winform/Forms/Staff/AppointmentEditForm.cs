using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

/// <summary>
/// Dialog for creating or editing an appointment.
/// </summary>
public class AppointmentEditForm : Form
{
    private readonly TextBox txtCustomer = new() { ReadOnly = true };
    private readonly Button btnPickCustomer = new();
    private readonly ComboBox cmbService = new();
    private readonly ComboBox cmbBarber = new();
    private readonly DateTimePicker dtpDate = new() { Format = DateTimePickerFormat.Short };
    private readonly DateTimePicker dtpTime = new() { Format = DateTimePickerFormat.Time, ShowUpDown = true };
    private readonly TextBox txtNotes = new() { Multiline = true };
    private readonly Button btnSave = new();
    private readonly Button btnCancelDlg = new();

    public Appointment Appointment { get; }

    public AppointmentEditForm(Appointment? existing = null)
    {
        Appointment = existing ?? new Appointment();

        Text = existing == null ? "Book Appointment" : "Edit Appointment";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;
        ClientSize = new Size(420, 460);
        BackColor = ThemeHelper.WarmIvory;

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(18),
            ColumnCount = 2,
            RowCount = 8
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        for (int i = 0; i < 7; i++) layout.RowStyles.Add(new RowStyle(SizeType.Absolute, i == 5 ? 70 : 44));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        int row = 0;
        var customerPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight, WrapContents = false };
        txtCustomer.Width = 165;
        btnPickCustomer.Text = "Pick…";
        btnPickCustomer.AutoSize = true;
        ThemeHelper.ApplyPrimaryButton(btnPickCustomer);
        btnPickCustomer.Click += (s, e) =>
        {
            using var search = new CustomerSearchForm();
            if (search.ShowDialog(this) == DialogResult.OK && search.SelectedCustomer is { } c)
            {
                Appointment.CustomerId = c.Id;
                Appointment.CustomerName = c.FullName;
                txtCustomer.Text = c.FullName;
            }
        };
        customerPanel.Controls.Add(txtCustomer);
        customerPanel.Controls.Add(btnPickCustomer);
        txtCustomer.Text = Appointment.CustomerName;

        AddRow(layout, row++, "Customer", customerPanel);
        cmbService.Dock = DockStyle.Fill;
        cmbService.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbService.DataSource = SqlDataRepository.Instance.GetServices().Where(s => s.IsActive).ToList();
        cmbService.DisplayMember = "ServiceName";
        cmbService.ValueMember = "Id";
        AddRow(layout, row++, "Service", cmbService);

        cmbBarber.Dock = DockStyle.Fill;
        cmbBarber.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbBarber.DataSource = SqlDataRepository.Instance.GetBarbers();
        cmbBarber.DisplayMember = "Name";
        cmbBarber.ValueMember = "Id";
        AddRow(layout, row++, "Barber", cmbBarber);

        AddRow(layout, row++, "Date", dtpDate);
        AddRow(layout, row++, "Time", dtpTime);
        AddRow(layout, row++, "Notes", txtNotes);

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft, WrapContents = false };
        btnSave.Text = "Save Appointment";
        ThemeHelper.ApplyPrimaryButton(btnSave);
        btnSave.AutoSize = true;
        btnSave.DialogResult = DialogResult.None;
        btnSave.Click += btnSave_Click;
        btnCancelDlg.Text = "Cancel";
        btnCancelDlg.AutoSize = true;
        btnCancelDlg.DialogResult = DialogResult.Cancel;
        buttons.Controls.Add(btnSave);
        buttons.Controls.Add(btnCancelDlg);
        layout.Controls.Add(buttons, 0, row);
        layout.SetColumnSpan(buttons, 2);

        if (existing != null)
        {
            if (existing.ServiceId > 0)
                cmbService.SelectedItem = ((List<ServiceItem>)cmbService.DataSource).FirstOrDefault(s => s.Id == existing.ServiceId);
            if (existing.BarberId > 0)
                cmbBarber.SelectedItem = ((List<Employee>)cmbBarber.DataSource).FirstOrDefault(b => b.Id == existing.BarberId);
            dtpDate.Value = existing.ScheduledAt.Date;
            dtpTime.Value = DateTime.Today.Add(existing.ScheduledAt.TimeOfDay);
            txtNotes.Text = existing.Notes;
        }
        else
        {
            dtpDate.Value = DateTime.Today;
            dtpTime.Value = DateTime.Now.AddHours(1);
        }

        Controls.Add(layout);
        AcceptButton = btnSave;
        CancelButton = btnCancelDlg;
    }

    private static void AddRow(TableLayoutPanel layout, int row, string label, Control control)
    {
        var lbl = new Label
        {
            Text = label,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = ThemeHelper.BodyFont,
            ForeColor = ThemeHelper.TextPrimary
        };
        layout.Controls.Add(lbl, 0, row);
        layout.Controls.Add(control, 1, row);
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (Appointment.CustomerId <= 0)
        {
            MessageBox.Show("Please pick a registered customer.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (cmbService.SelectedItem is not ServiceItem service)
        {
            MessageBox.Show("Please select a service.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (cmbBarber.SelectedItem is not Employee barber)
        {
            MessageBox.Show("Please select a barber.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Appointment.ServiceId = service.Id;
        Appointment.ServiceName = service.ServiceName;
        Appointment.BarberId = barber.Id;
        Appointment.BarberName = barber.Name;
        Appointment.ScheduledAt = dtpDate.Value.Date + dtpTime.Value.TimeOfDay;
        Appointment.Notes = txtNotes.Text.Trim();

        DialogResult = DialogResult.OK;
        Close();
    }
}
