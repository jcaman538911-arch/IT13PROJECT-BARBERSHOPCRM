using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class CustomerHistoryForm : Form
{
    public CustomerHistoryForm()
    {
        InitializeComponent();
        ThemeHelper.ApplyModernGrid(dgvHistory);
        PopulateCustomers();
    }

    private void PopulateCustomers()
    {
        var customers = SqlDataRepository.Instance.GetCustomers();
        cmbCustomers.DataSource = customers;
        cmbCustomers.DisplayMember = "FullName";
        cmbCustomers.ValueMember = "Id";

        if (customers.Any())
        {
            LoadHistory(customers[0].Id);
        }
    }

    private void LoadHistory(int customerId)
    {
        var history = SqlDataRepository.Instance.GetCustomerTransactions(customerId);
        dgvHistory.DataSource = history.Select(h => new
        {
            Date = h.TransactionDate.ToString("yyyy-MM-dd HH:mm"),
            Service = h.ServiceName,
            Barber = h.BarberName,
            Price = $"₱{h.Subtotal:N2}",
            Discount = $"₱{h.DiscountAmount:N2}",
            FinalPaid = $"₱{h.FinalAmount:N2}",
            h.PaymentMethod,
            PaymentStatus = h.Status.ToString()
        }).ToList();
    }

    private void cmbCustomers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbCustomers.SelectedValue is int custId)
        {
            LoadHistory(custId);
        }
    }

    private void btnFilter_Click(object sender, EventArgs e)
    {
        if (cmbCustomers.SelectedValue is int custId)
        {
            LoadHistory(custId);
        }
    }
}
