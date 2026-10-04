using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
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
        SetupEventHandlers();
        LoadDashboardMetrics();
    }

    private void SetupEventHandlers()
    {
        // Card 1: Customers Today
        AttachCardClick(pnlCard1, lblCard1Title, lblCard1Value, lblCard1Icon, lblCard1Link, "Customers");
        
        // Card 2: Haircuts Today (Transactions)
        AttachCardClick(pnlCard2, lblCard2Title, lblCard2Value, lblCard2Icon, lblCard2Link, "Daily Transactions");
        
        // Card 3: Sales Today
        AttachCardClick(pnlCard3, lblCard3Title, lblCard3Value, lblCard3Icon, lblCard3Link, "Business Reports");
        
        // Card 4: Barbers Present
        AttachCardClick(pnlCard4, lblCard4Title, lblCard4Value, lblCard4Icon, lblCard4Link, "Employee Attendance");
        
        // Card 5: Loyalty Members
        AttachCardClick(pnlCard5, lblCard5Title, lblCard5Value, lblCard5Icon, lblCard5Link, "Loyalty Rewards");
        
        // Card 6: Promotions Used
        AttachCardClick(pnlCard6, lblCard6Title, lblCard6Value, lblCard6Icon, lblCard6Link, "Promotions");
    }

    private void AttachCardClick(Panel pnl, Label title, Label val, Label icon, Label link, string route)
    {
        var handler = new EventHandler((s, e) => _mainShell?.NavigateTo(route));
        pnl.Cursor = Cursors.Hand;
        link.Cursor = Cursors.Hand;
        pnl.Click += handler;
        title.Click += handler;
        val.Click += handler;
        icon.Click += handler;
        link.Click += handler;
    }

    private async void LoadDashboardMetrics(DateTime? date = null)
    {
        var targetDate = date ?? DateTime.Today;
        
        var (todayTxns, allCustomers, todayAtt, barbers) = await Task.Run(() => 
        {
            return (
                SqlDataRepository.Instance.GetTodayTransactions(targetDate),
                SqlDataRepository.Instance.GetCustomers(),
                SqlDataRepository.Instance.GetTodayAttendance(targetDate),
                SqlDataRepository.Instance.GetBarbers()
            );
        });


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

        btnViewAllTransactions.Click -= BtnViewAllTransactions_Click;
        btnViewAllTransactions.Click += BtnViewAllTransactions_Click;
    }

    private void BtnViewAllTransactions_Click(object? sender, EventArgs e)
    {
        _mainShell?.NavigateTo("Business Reports");
    }

    private void dtpDashboardDate_ValueChanged(object sender, EventArgs e)
    {
        LoadDashboardMetrics(dtpDashboardDate.Value);
    }
}
