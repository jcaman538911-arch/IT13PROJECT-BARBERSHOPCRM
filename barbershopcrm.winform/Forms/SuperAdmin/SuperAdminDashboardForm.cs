using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

public partial class SuperAdminDashboardForm : Form
{
    private readonly MainForm? _mainShell;

    public SuperAdminDashboardForm(MainForm? mainShell = null)
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        _mainShell = mainShell;
        ThemeHelper.ApplyModernGrid(dgvSystemLogs);
        LoadMetricsAndLogs();
    }

    private void LoadMetricsAndLogs()
    {
        var users = SqlDataRepository.Instance.GetUsers();
        lblCard1Value.Text = users.Count.ToString();
        lblCard2Value.Text = users.Count(u => u.Role == UserRole.Admin).ToString();
        lblCard3Value.Text = "ONLINE";
        lblCard4Value.Text = SqlDataRepository.Instance.GetSupportRequests().Count(r => r.Status != "Resolved").ToString();

        dgvSystemLogs.DataSource = SqlDataRepository.Instance.GetSystemLogs()
            .Select(l => new
            {
                l.Id,
                l.Timestamp,
                l.LogLevel,
                l.Module,
                l.Message,
                l.ActionBy
            }).ToList();
    }
}
