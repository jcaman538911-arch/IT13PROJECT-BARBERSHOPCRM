using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class AdminDashboardForm : Form
{
    private readonly MainForm? _mainShell;

    public AdminDashboardForm(MainForm? mainShell = null)
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        _mainShell = mainShell;
        ThemeHelper.ApplyModernGrid(dgvRecentTransactions);
        LoadDashboardMetrics();
    }

    private void LoadDashboardMetrics()
    {
        var todayTxns = SqlDataRepository.Instance.GetTodayTransactions();
        var allCustomers = SqlDataRepository.Instance.GetCustomers();
        var todayAtt = SqlDataRepository.Instance.GetTodayAttendance();
        var barbers = SqlDataRepository.Instance.GetBarbers();

        lblCard1Value.Text = todayTxns.Select(t => t.CustomerName).Distinct().Count().ToString();
        lblCard2Value.Text = todayTxns.Count(t => t.Status == TransactionStatus.Completed).ToString();
        
        decimal salesToday = todayTxns.Where(t => t.Status == TransactionStatus.Completed).Sum(t => t.FinalAmount);
        lblCard3Value.Text = $"₱{salesToday:N2}";

        int presentBarbers = todayAtt.Count(a => a.Status == AttendanceStatus.Present);
        lblCard4Value.Text = $"{presentBarbers} / {barbers.Count}";

        lblCard5Value.Text = allCustomers.Count(c => c.IsLoyaltyMember).ToString();
        lblCard6Value.Text = todayTxns.Count(t => t.PromotionId.HasValue).ToString();

        dgvRecentTransactions.DataSource = todayTxns.Select(t => new
        {
            t.TransactionNumber,
            t.CustomerName,
            t.ServiceName,
            t.BarberName,
            Price = $"₱{t.Subtotal:N2}",
            Discount = $"₱{t.DiscountAmount:N2}",
            Total = $"₱{t.FinalAmount:N2}",
            Method = t.PaymentMethod.ToString(),
            t.Status,
            Time = t.TransactionDate.ToString("HH:mm")
        }).ToList();

        btnViewAllTransactions.Click += (s, e) => _mainShell?.NavigateTo("Business Reports");
    }
}
