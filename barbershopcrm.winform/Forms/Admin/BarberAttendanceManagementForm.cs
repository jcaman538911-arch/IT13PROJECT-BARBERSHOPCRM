using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class BarberAttendanceManagementForm : Form
{
    public BarberAttendanceManagementForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvAttendance);
        cmbStatusFilter.SelectedIndex = 0;
        LoadAttendance();
    }

    private void LoadAttendance(bool useFilters = false)
    {
        var records = SqlDataRepository.Instance.GetAttendanceRecords();

        if (useFilters)
        {
            records = records.Where(r => r.Date.Date == dtpDateFilter.Value.Date).ToList();

            if (cmbStatusFilter.SelectedIndex > 0 && cmbStatusFilter.SelectedItem != null && Enum.TryParse<AttendanceStatus>(cmbStatusFilter.SelectedItem.ToString(), out var status))
            {
                records = records.Where(r => r.Status == status).ToList();
            }
        }

        dgvAttendance.DataSource = records.Select(r => new
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

    private void btnFilter_Click(object sender, EventArgs e)
    {
        LoadAttendance(useFilters: true);
    }

    private void btnResetFilter_Click(object sender, EventArgs e)
    {
        cmbStatusFilter.SelectedIndex = 0;
        dtpDateFilter.Value = DateTime.Today;
        LoadAttendance(useFilters: false);
    }
}
