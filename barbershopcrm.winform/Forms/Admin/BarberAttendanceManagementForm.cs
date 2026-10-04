using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
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

    private async void LoadAttendance(bool useFilters = false)
    {
        var filterDate = useFilters ? dtpDateFilter.Value.Date : (DateTime?)null;
        var filterStatusStr = useFilters && cmbStatusFilter.SelectedIndex > 0 ? cmbStatusFilter.SelectedItem?.ToString() : null;

        var records = await Task.Run(() => 
        {
            var data = SqlDataRepository.Instance.GetAttendanceRecords();
            if (useFilters)
            {
                data = data.Where(r => r.Date.Date == filterDate).ToList();

                if (filterStatusStr != null && Enum.TryParse<AttendanceStatus>(filterStatusStr, out var status))
                {
                    data = data.Where(r => r.Status == status).ToList();
                }
            }
            return data;
        });


        dgvAttendance.DataSource = records.Select(r => new
        {
            r.Id,
            Name = r.EmployeeName,
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
