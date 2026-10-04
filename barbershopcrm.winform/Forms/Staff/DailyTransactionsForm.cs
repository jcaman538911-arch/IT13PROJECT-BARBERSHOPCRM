using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
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

    private async void LoadTransactions(string query = "", bool filterByDate = false)
    {
        var targetDate = filterByDate ? dtpDate.Value.Date : (DateTime?)null;
        
        var txns = await Task.Run(() => 
        {
            var data = SqlDataRepository.Instance.GetTransactions();
            if (targetDate.HasValue)
            {
                data = data.Where(t => t.TransactionDate.Date == targetDate.Value).ToList();
            }
            if (!string.IsNullOrWhiteSpace(query))
            {
                var q = query.ToLower();
                data = data.Where(t =>
                    t.TransactionNumber.StartsWith(q, StringComparison.OrdinalIgnoreCase) ||
                    t.CustomerName.StartsWith(q, StringComparison.OrdinalIgnoreCase) ||
                    t.BarberName.StartsWith(q, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            return data;
        });


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

    private void dtpDate_ValueChanged(object? sender, EventArgs e)
    {
        LoadTransactions(txtSearch.Text.Trim(), filterByDate: true);
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        dtpDate.ValueChanged -= dtpDate_ValueChanged; // prevent triggering twice
        dtpDate.Value = DateTime.Today;
        dtpDate.ValueChanged += dtpDate_ValueChanged;
        LoadTransactions();
    }
}
