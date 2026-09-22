using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class CustomerHistoryForm : Form
{
    private Customer? _selectedCustomer;

    public CustomerHistoryForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvHistory);
        ThemeHelper.ApplyModernGrid(dgvLoyalty);
        btnLoyaltyCard.Click += btnLoyaltyCard_Click;
        PopulateCustomers();
    }

    private void btnLoyaltyCard_Click(object? sender, EventArgs e)
    {
        if (_selectedCustomer is { IsLoyaltyMember: true } member)
        {
            using var card = new LoyaltyCardForm(member);
            card.ShowDialog(this);
        }
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

        LoadLoyaltyInfo(customerId);
    }

    private void LoadLoyaltyInfo(int customerId)
    {
        var customer = SqlDataRepository.Instance.GetCustomerById(customerId);
        _selectedCustomer = customer;
        if (customer == null)
        {
            lblLoyaltySummary.Text = "";
            dgvLoyalty.DataSource = null;
            btnLoyaltyCard.Visible = false;
            return;
        }

        int rewardCount = customer.IsLoyaltyMember
            ? SqlDataRepository.Instance.GetLoyaltyRewards().Count(r => r.IsActive && customer.LoyaltyPoints >= r.PointsRequired)
            : 0;
        lblLoyaltySummary.Text = customer.IsLoyaltyMember
            ? $"Loyalty Member {LoyaltyCardForm.FormatMemberId(customer.Id)}  |  Points: {customer.LoyaltyPoints}  |  {rewardCount} reward(s) available"
            : "Not a Loyalty Member";
        btnLoyaltyCard.Visible = customer.IsLoyaltyMember;

        var loyalty = SqlDataRepository.Instance.GetLoyaltyHistory(customerId);
        dgvLoyalty.DataSource = loyalty.Select(l => new
        {
            Date = l.DateCreated.ToString("yyyy-MM-dd HH:mm"),
            Activity = l.PointsEarned > 0 && l.PointsRedeemed > 0 ? "EARNED+REDEEMED" : l.PointsEarned > 0 ? "EARNED" : "REDEEMED",
            Points = l.PointsEarned > 0 ? $"+{l.PointsEarned}" : $"-{l.PointsRedeemed}",
            BalanceAfter = l.NewBalance,
            l.Description,
            l.RecordedBy
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
