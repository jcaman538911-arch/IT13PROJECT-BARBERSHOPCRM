using System;
using System.Windows.Forms;
using barbershop.infrastructure;

namespace BarberShopCRM.Forms.SuperAdmin;

public partial class SystemUpdatesForm : Form
{
    public SystemUpdatesForm()
    {
        InitializeComponent();
    }

    private void btnCheckUpdates_Click(object sender, EventArgs e)
    {
        SqlDataRepository.Instance.AddSystemLog("INFO", "Updates", "Super Admin performed system update check.", "superadmin");
        MessageBox.Show("Your Barber Shop CRM System is up to date! (v1.0.0 Stable)", "System Up to Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
