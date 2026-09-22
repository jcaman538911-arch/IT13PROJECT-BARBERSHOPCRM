using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class DailyTransactionsForm : Form
{
    public DailyTransactionsForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvTransactions);
        LoadTransactions();
    }

    private void LoadTransactions(string query = "", bool filterByDate = false)
    {
        var txns = SqlDataRepository.Instance.GetTransactions();

        if (filterByDate)
        {
            txns = txns.Where(t => t.TransactionDate.Date == dtpDate.Value.Date).ToList();
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            query = query.ToLower();
            txns = txns.Where(t =>
                t.TransactionNumber.ToLower().Contains(query) ||
                t.CustomerName.ToLower().Contains(query) ||
                t.BarberName.ToLower().Contains(query)).ToList();
        }

        dgvTransactions.DataSource = txns.Select(t => new
        {
            t.TransactionNumber,
            Customer = t.CustomerName,
            Service = t.ServiceName,
            Barber = t.BarberName,
            Price = $"₱{t.Subtotal:N2}",
            Discount = $"₱{t.DiscountAmount:N2}",
            FinalAmount = $"₱{t.FinalAmount:N2}",
            Payment = t.PaymentMethod.ToString(),
            t.Status,
            DateTime = t.TransactionDate.ToString("yyyy-MM-dd HH:mm")
        }).ToList();
    }

    private void btnFilter_Click(object sender, EventArgs e)
    {
        LoadTransactions(txtSearch.Text.Trim(), filterByDate: true);
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        dtpDate.Value = DateTime.Today;
        LoadTransactions();
    }
}
