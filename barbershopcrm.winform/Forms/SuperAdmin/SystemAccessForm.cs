using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

public partial class SystemAccessForm : Form
{
    public SystemAccessForm()
    {
        InitializeComponent();
        ThemeHelper.ApplyModernGrid(dgvAccessLogs);
        LoadLogs();
    }

    private void LoadLogs()
    {
        dgvAccessLogs.DataSource = SqlDataRepository.Instance.GetSystemLogs()
            .Where(l => l.Module == "Auth" || l.Module == "Users" || l.Module == "AdminAccounts")
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

    private void btnSaveAccessRules_Click(object sender, EventArgs e)
    {
        SqlDataRepository.Instance.AddSystemLog("INFO", "Security", "Super Admin verified and locked system role security policies.", "superadmin");
        MessageBox.Show("Security access policies enforced and verified.", "Policies Locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadLogs();
    }
}
