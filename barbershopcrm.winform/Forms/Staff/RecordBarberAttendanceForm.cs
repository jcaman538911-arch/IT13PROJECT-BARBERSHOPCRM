using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class RecordBarberAttendanceForm : Form
{
    public RecordBarberAttendanceForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvTodayAttendance);
        PopulateDropdowns();
        LoadTodayAttendance();
    }

    private void PopulateDropdowns()
    {
        var barbers = SqlDataRepository.Instance.GetBarbers();
        cmbBarber.DataSource = barbers;
        cmbBarber.DisplayMember = "Name";
        cmbBarber.ValueMember = "Id";

        cmbStatus.DataSource = Enum.GetValues(typeof(AttendanceStatus));
    }

    private void LoadTodayAttendance()
    {
        var records = SqlDataRepository.Instance.GetTodayAttendance();
        dgvTodayAttendance.DataSource = records.Select(r => new
        {
            r.Id,
            Barber = r.EmployeeName,
            Date = r.Date.ToString("yyyy-MM-dd"),
            TimeIn = DateTime.Today.Add(r.TimeIn).ToString("hh:mm tt"),
            TimeOut = r.TimeOut.HasValue ? DateTime.Today.Add(r.TimeOut.Value).ToString("hh:mm tt") : "In Service",
            Status = r.Status.ToString(),
            r.Notes
        }).ToList();
    }

    private void btnSaveAttendance_Click(object sender, EventArgs e)
    {
        if (cmbBarber.SelectedItem is not Employee barber)
        {
            MessageBox.Show("Please select a barber.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var rec = new AttendanceRecord
        {
            EmployeeId = barber.Id,
            EmployeeName = barber.Name,
            Date = dtpDate.Value.Date,
            TimeIn = dtpTimeIn.Value.TimeOfDay,
            TimeOut = dtpTimeOut.Value.TimeOfDay,
            Status = (AttendanceStatus)cmbStatus.SelectedItem!,
            Notes = txtNotes.Text.Trim()
        };

        SqlDataRepository.Instance.RecordAttendance(rec);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Attendance", $"Recorded attendance for Barber '{barber.Name}' ({rec.Status})", "cashier");
        MessageBox.Show($"Attendance recorded for Barber {barber.Name} ({rec.Status}).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtNotes.Clear();
        LoadTodayAttendance();
    }
}
