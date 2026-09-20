using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

public partial class AdminAccountsForm : Form
{
    public AdminAccountsForm()
    {
        InitializeComponent();
        ThemeHelper.ApplyModernGrid(dgvAdminAccounts);
        LoadAdminAccounts();
    }

    private void LoadAdminAccounts()
    {
        var admins = SqlDataRepository.Instance.GetUsers().Where(u => u.Role == UserRole.Admin).ToList();
        dgvAdminAccounts.DataSource = admins.Select(a => new
        {
            a.Id,
            a.Username,
            a.FullName,
            Status = a.IsActive ? "Active" : "Disabled",
            a.CreatedDate
        }).ToList();
    }

    private void btnResetPassword_Click(object sender, EventArgs e)
    {
        if (dgvAdminAccounts.CurrentRow != null && dgvAdminAccounts.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvAdminAccounts.CurrentRow.DataBoundItem;
            int id = item.Id;

            var user = SqlDataRepository.Instance.GetUsers().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Password = "admin123";
                SqlDataRepository.Instance.UpdateUser(user);
                SqlDataRepository.Instance.AddSystemLog("WARN", "AdminAccounts", $"Reset password for Admin '{user.Username}'", "superadmin");
                MessageBox.Show($"Password for '{user.Username}' has been reset to default 'admin123'.", "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAdminAccounts();
            }
        }
    }

    private void btnToggleStatus_Click(object sender, EventArgs e)
    {
        if (dgvAdminAccounts.CurrentRow != null && dgvAdminAccounts.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvAdminAccounts.CurrentRow.DataBoundItem;
            int id = item.Id;

            var user = SqlDataRepository.Instance.GetUsers().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                SqlDataRepository.Instance.UpdateUser(user);
                SqlDataRepository.Instance.AddSystemLog("INFO", "AdminAccounts", $"Toggled active state for Admin '{user.Username}' to {user.IsActive}", "superadmin");
                MessageBox.Show($"Admin '{user.Username}' status changed to {(user.IsActive ? "Active" : "Disabled")}.", "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAdminAccounts();
            }
        }
    }
}
