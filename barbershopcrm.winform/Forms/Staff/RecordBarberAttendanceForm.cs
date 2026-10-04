using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class RecordBarberAttendanceForm : Form
{
    // Typed list so the combo binding and cast both work correctly
    private List<Employee> _employees = new();

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
        // Load ALL employees (both Barbers and Staff)
        _employees = SqlDataRepository.Instance.GetEmployees(includeInactive: true);

        // Bind the typed list directly so SelectedItem is an Employee object
        cmbBarber.DataSource    = _employees;
        cmbBarber.DisplayMember = "Name";
        cmbBarber.ValueMember   = "Id";

        cmbStatus.DataSource = Enum.GetValues(typeof(AttendanceStatus));
    }

    private void LoadTodayAttendance()
    {
        var records = SqlDataRepository.Instance.GetTodayAttendance();
        dgvTodayAttendance.DataSource = records.Select(r => new
        {
            r.Id,
            Employee    = r.EmployeeName,
            Date        = r.Date.ToString("yyyy-MM-dd"),
            TimeIn      = DateTime.Today.Add(r.TimeIn).ToString("hh:mm tt"),
            TimeOut     = r.TimeOut.HasValue
                              ? DateTime.Today.Add(r.TimeOut.Value).ToString("hh:mm tt")
                              : "Still In",
            Status      = r.Status.ToString(),
            r.Notes
        }).ToList();
    }

    private void btnSaveAttendance_Click(object sender, EventArgs e)
    {
        // SelectedItem is now a typed Employee — cast will succeed
        if (cmbBarber.SelectedItem is not Employee emp)
        {
            MessageBox.Show("Please select an employee.",
                "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var role = emp.Position == EmployeePosition.Barber ? "Barber" : "Staff";

        var rec = new AttendanceRecord
        {
            EmployeeId   = emp.Id,
            EmployeeName = emp.Name,
            Date         = dtpDate.Value.Date,
            TimeIn       = dtpTimeIn.Value.TimeOfDay,
            TimeOut      = dtpTimeOut.Value.TimeOfDay,
            Status       = (AttendanceStatus)cmbStatus.SelectedItem!,
            Notes        = txtNotes.Text.Trim()
        };

        SqlDataRepository.Instance.RecordAttendance(rec);
        SqlDataRepository.Instance.AddSystemLog(
            "INFO", "Attendance",
            $"Recorded attendance for {role} '{emp.Name}' ({rec.Status})",
            "cashier");
        MessageBox.Show(
            $"Attendance recorded for {role} {emp.Name} ({rec.Status}).",
            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        txtNotes.Clear();
        LoadTodayAttendance();
    }
}
