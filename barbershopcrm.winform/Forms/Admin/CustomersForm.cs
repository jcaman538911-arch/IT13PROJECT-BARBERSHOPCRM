using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class CustomersForm : Form
{
    private int _selectedCustomerId = 0;

    public CustomersForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvCustomers);
        LoadCustomers();
    }

    private void LoadCustomers(string query = "")
    {
        var customers = SqlDataRepository.Instance.SearchCustomers(query);
        dgvCustomers.DataSource = customers.Select(c => new
        {
            c.Id,
            c.FullName,
            c.PhoneNumber,
            c.Email,
            Birthday = c.Birthday.HasValue ? c.Birthday.Value.ToString("yyyy-MM-dd") : "N/A",
            Loyalty = c.IsLoyaltyMember ? "Member" : "Regular",
            Points = c.LoyaltyPoints,
            Registered = c.CreatedDate.ToString("yyyy-MM-dd")
        }).ToList();
    }

    private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvCustomers.CurrentRow != null && dgvCustomers.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvCustomers.CurrentRow.DataBoundItem;
            _selectedCustomerId = item.Id;

            var cust = SqlDataRepository.Instance.GetCustomerById(_selectedCustomerId);
            if (cust != null)
            {
                txtFullName.Text = cust.FullName;
                txtPhone.Text = cust.PhoneNumber;
                txtEmail.Text = cust.Email;
                dtpBirthday.Value = cust.Birthday ?? DateTime.Today;
                chkLoyaltyMember.Checked = cust.IsLoyaltyMember;
                numLoyaltyPoints.Value = cust.LoyaltyPoints;
            }
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtFullName.Text))
        {
            MessageBox.Show("Customer Full Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var cust = new Customer
        {
            FullName = txtFullName.Text.Trim(),
            PhoneNumber = txtPhone.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            Birthday = dtpBirthday.Value,
            IsLoyaltyMember = chkLoyaltyMember.Checked,
            LoyaltyPoints = (int)numLoyaltyPoints.Value
        };

        SqlDataRepository.Instance.AddCustomer(cust);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Customers", $"Registered customer '{cust.FullName}'", "user");
        MessageBox.Show("Customer registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadCustomers();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (_selectedCustomerId == 0)
        {
            MessageBox.Show("Please select a customer from the table to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtFullName.Text))
        {
            MessageBox.Show("Customer Full Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var cust = new Customer
        {
            Id = _selectedCustomerId,
            FullName = txtFullName.Text.Trim(),
            PhoneNumber = txtPhone.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            Birthday = dtpBirthday.Value,
            IsLoyaltyMember = chkLoyaltyMember.Checked,
            LoyaltyPoints = (int)numLoyaltyPoints.Value
        };

        SqlDataRepository.Instance.UpdateCustomer(cust);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Customers", $"Updated customer ID {_selectedCustomerId} '{cust.FullName}'", "user");
        MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ClearForm();
        LoadCustomers();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_selectedCustomerId == 0)
        {
            MessageBox.Show("Please select a customer to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show($"Are you sure you want to delete customer ID {_selectedCustomerId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result == DialogResult.Yes)
        {
            SqlDataRepository.Instance.DeleteCustomer(_selectedCustomerId);
            SqlDataRepository.Instance.AddSystemLog("WARN", "Customers", $"Deleted customer ID {_selectedCustomerId}", "user");
            MessageBox.Show("Customer deleted successfully!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadCustomers();
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearForm();
    }

    private void ClearForm()
    {
        _selectedCustomerId = 0;
        txtFullName.Clear();
        txtPhone.Clear();
        txtEmail.Clear();
        dtpBirthday.Value = DateTime.Today;
        chkLoyaltyMember.Checked = false;
        numLoyaltyPoints.Value = 0;
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        LoadCustomers(txtSearch.Text.Trim());
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        LoadCustomers();
    }
}
