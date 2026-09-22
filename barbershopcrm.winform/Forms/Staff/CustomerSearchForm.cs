using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class CustomerSearchForm : Form
{
    public Customer? SelectedCustomer { get; private set; }
    public bool IsWalkIn { get; private set; }

    public CustomerSearchForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvResults);
        LoadCustomers();
    }

    private void LoadCustomers(string query = "")
    {
        var list = SqlDataRepository.Instance.SearchCustomers(query);
        dgvResults.DataSource = list.Select(c => new
        {
            c.Id,
            c.FullName,
            c.PhoneNumber,
            Loyalty = c.IsLoyaltyMember ? "Member" : "Regular",
            c.LoyaltyPoints
        }).ToList();
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        LoadCustomers(txtSearch.Text.Trim());
    }

    private void btnSelect_Click(object sender, EventArgs e)
    {
        if (dgvResults.CurrentRow != null && dgvResults.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvResults.CurrentRow.DataBoundItem;
            int id = item.Id;
            SelectedCustomer = SqlDataRepository.Instance.GetCustomerById(id);
            IsWalkIn = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    private void btnWalkIn_Click(object sender, EventArgs e)
    {
        SelectedCustomer = null;
        IsWalkIn = true;
        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void btnNewCustomer_Click(object sender, EventArgs e)
    {
        using (var regModal = new CustomerRegistrationForm())
        {
            if (regModal.ShowDialog() == DialogResult.OK && regModal.CreatedCustomer != null)
            {
                SelectedCustomer = regModal.CreatedCustomer;
                IsWalkIn = false;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
