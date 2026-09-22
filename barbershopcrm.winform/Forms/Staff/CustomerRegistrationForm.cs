using BarberShopCRM.Helpers;
using System;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;

namespace BarberShopCRM.Forms.Staff;

public partial class CustomerRegistrationForm : Form
{
    public Customer? CreatedCustomer { get; private set; }

    public CustomerRegistrationForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFullName.Text))
        {
            MessageBox.Show("Full Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var cust = new Customer
        {
            FullName = txtFullName.Text.Trim(),
            PhoneNumber = txtPhone.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            Birthday = dtpBirthday.Value,
            IsLoyaltyMember = chkLoyaltyMember.Checked,
            LoyaltyPoints = 0
        };

        SqlDataRepository.Instance.AddCustomer(cust);
        CreatedCustomer = cust;
        SqlDataRepository.Instance.AddSystemLog("INFO", "Customers", $"Registered new customer '{cust.FullName}' via POS.", "cashier");
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}
