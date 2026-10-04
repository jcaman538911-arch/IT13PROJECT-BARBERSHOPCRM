using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
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

    private async void LoadMetricsAndLogs()
    {
        var (users, supportCount, logs) = await Task.Run(() => 
        {
            return (
                SqlDataRepository.Instance.GetUsers(),
                SqlDataRepository.Instance.GetSupportRequests().Count(r => r.Status != "Resolved"),
                SqlDataRepository.Instance.GetSystemLogs()
            );
        });

        lblCard1Value.Text = users.Count.ToString();
        lblCard2Value.Text = users.Count(u => u.Role == UserRole.Admin).ToString();
        lblCard3Value.Text = "ONLINE";
        lblCard4Value.Text = supportCount.ToString();

        dgvSystemLogs.DataSource = logs
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
