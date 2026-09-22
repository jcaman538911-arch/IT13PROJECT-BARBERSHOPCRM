using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class StaffDashboardForm : Form
{
    private readonly MainForm? _mainShell;

    public StaffDashboardForm(MainForm? mainShell = null)
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        _mainShell = mainShell;
        ThemeHelper.ApplyModernGrid(dgvTodayQueue);
        LoadMetrics();
    }

    private void LoadMetrics()
    {
        var todayTxns = SqlDataRepository.Instance.GetTodayTransactions();
        var att = SqlDataRepository.Instance.GetTodayAttendance();
        var barbers = SqlDataRepository.Instance.GetBarbers();

        lblCard1Value.Text = todayTxns.Select(t => t.CustomerName).Distinct().Count().ToString();
        lblCard2Value.Text = todayTxns.Count(t => t.Status == TransactionStatus.Completed).ToString();
        
        decimal salesToday = todayTxns.Where(t => t.Status == TransactionStatus.Completed).Sum(t => t.FinalAmount);
        lblCard3Value.Text = $"₱{salesToday:N2}";

        int presentBarbers = att.Count(a => a.Status == AttendanceStatus.Present);
        lblCard4Value.Text = $"{presentBarbers} / {barbers.Count}";

        dgvTodayQueue.DataSource = todayTxns.Select(t => new
        {
            t.TransactionNumber,
            t.CustomerName,
            t.ServiceName,
            t.BarberName,
            Price = $"₱{t.Subtotal:N2}",
            Discount = $"₱{t.DiscountAmount:N2}",
            FinalAmount = $"₱{t.FinalAmount:N2}",
            t.Status,
            Time = t.TransactionDate.ToString("HH:mm")
        }).ToList();
    }

    private void btnNewCustomer_Click(object sender, EventArgs e)
    {
        _mainShell?.NavigateTo("Customers");
    }

    private void btnNewTransaction_Click(object sender, EventArgs e)
    {
        _mainShell?.NavigateTo("New Transaction");
    }

    private void btnApplyPromotion_Click(object sender, EventArgs e)
    {
        _mainShell?.NavigateTo("Promotions");
    }

    private void btnRedeemLoyalty_Click(object sender, EventArgs e)
    {
        _mainShell?.NavigateTo("Loyalty & Rewards");
    }

    private void btnRecordAttendance_Click(object sender, EventArgs e)
    {
        _mainShell?.NavigateTo("Barber Attendance");
    }
}
