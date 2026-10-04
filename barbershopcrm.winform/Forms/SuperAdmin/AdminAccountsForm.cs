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
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvAdminAccounts);
        LoadAdminAccounts();
    }

    private int _selectedTenantId = 0;

    private void LoadAdminAccounts()
    {
        var tenants = SqlDataRepository.Instance.GetTenantSubscriptions();
        dgvAdminAccounts.DataSource = tenants.Select(t => new
        {
            TenantID = t.TenantID,
            Company = t.CompanyName,
            Database = t.DatabaseName,
            AdminUser = "admin", // Standard admin username for tenants
            SubscriptionPlan = t.PlanName,
            SubscriptionStatus = t.Status,
            NextBillingDate = t.ExpiryDate.ToString("yyyy-MM-dd")
        }).ToList();
    }

    private int GetCurrentRowId()
    {
        if (dgvAdminAccounts.CurrentRow?.DataBoundItem == null) return _selectedTenantId;
        dynamic item = dgvAdminAccounts.CurrentRow.DataBoundItem;
        return item.TenantID;
    }

    private void btnResetPassword_Click(object sender, EventArgs e)
    {
        int tenantId = GetCurrentRowId();
        if (tenantId == 0)
        {
            MessageBox.Show("Please select a tenant to reset their admin password.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var tenant = SqlDataRepository.Instance.GetTenantSubscriptions().FirstOrDefault(t => t.TenantID == tenantId);
        if (tenant == null) return;

        var result = MessageBox.Show($"Are you sure you want to reset the admin password for '{tenant.CompanyName}' to 'admin123'?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result != DialogResult.Yes) return;

        try
        {
            string tenantConnString = TenantConnectionFactory.GetConnectionString(tenantId);
            using var conn = new Microsoft.Data.SqlClient.SqlConnection(tenantConnString);
            conn.Open();
            using var cmd = new Microsoft.Data.SqlClient.SqlCommand("UPDATE Users SET Password = 'admin123' WHERE Role = 1", conn); // Role 1 = Admin
            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                SqlDataRepository.Instance.AddSystemLog("WARN", "AdminAccounts", $"Reset admin password for Tenant {tenantId} ({tenant.CompanyName})", "superadmin");
                MessageBox.Show($"Password for '{tenant.CompanyName}' admin has been reset to 'admin123'.", "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Could not find an Admin user in the database for {tenant.CompanyName}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to connect to tenant database to reset password: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnToggleStatus_Click(object sender, EventArgs e)
    {
        MessageBox.Show("To suspend or activate a tenant, please go to the 'Subscriptions' form.", "Use Subscriptions Form", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
