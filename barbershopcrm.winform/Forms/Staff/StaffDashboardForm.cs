using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

public partial class StaffDashboardForm : Form
{
    private readonly MainForm? _mainShell;
    private List<Transaction> _todayTransactions = new();

    public StaffDashboardForm(MainForm? mainShell = null)
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        _mainShell = mainShell;
        ThemeHelper.ApplyModernGrid(dgvServiceActivity);
        SetupEventHandlers();
        LoadDashboardData();
    }

    private void SetupEventHandlers()
    {
        // Quick Actions
        btnNewCustomer.Click += (s, e) => _mainShell?.NavigateTo("Customers");
        btnNewService.Click += (s, e) => _mainShell?.NavigateTo("New Transaction");
        btnApplyPromotion.Click += (s, e) => _mainShell?.NavigateTo("Promotions");
        btnRedeemLoyalty.Click += (s, e) => _mainShell?.NavigateTo("Loyalty & Rewards");
        btnEmployeeAttendance.Click += (s, e) => _mainShell?.NavigateTo("Employee Attendance");

        // Summary Card Navigation
        pnlCardCustomers.Click += (s, e) => _mainShell?.NavigateTo("Customers");
        pnlCardAppointments.Click += (s, e) => _mainShell?.NavigateTo("Customer Service");
        pnlCardWaiting.Click += (s, e) => _mainShell?.NavigateTo("Customer Service");
        pnlCardCompleted.Click += (s, e) => _mainShell?.NavigateTo("Daily Transactions");
        pnlCardSales.Click += (s, e) => _mainShell?.NavigateTo("Daily Transactions");
        pnlCardBarbers.Click += (s, e) => _mainShell?.NavigateTo("Employee Attendance");

        // Live Ops Navigation
        btnViewQueue.Click += (s, e) => _mainShell?.NavigateTo("Customer Service");
        btnViewAppointments.Click += (s, e) => _mainShell?.NavigateTo("Customer Service");

        // Empty State Navigation
        btnEmptyStartService.Click += (s, e) => _mainShell?.NavigateTo("New Transaction");
        btnEmptyViewAppts.Click += (s, e) => _mainShell?.NavigateTo("Customer Service");

        // Custom Chart Drawing
        pnlTrendChart.Paint += PnlTrendChart_Paint;
        pnlTrendChart.Resize += (s, e) => pnlTrendChart.Invalidate();
    }

    public async void LoadDashboardData()
    {
        // 1. Fetch Data
        var today = DateTime.Today;
        
        var (todayTxns, appointments, attendance, barbers) = await Task.Run(() => 
        {
            return (
                SqlDataRepository.Instance.GetTodayTransactions(today),
                SqlDataRepository.Instance.GetAppointments().FindAll(a => a.ScheduledAt.Date == today),
                SqlDataRepository.Instance.GetTodayAttendance(today),
                SqlDataRepository.Instance.GetBarbers()
            );
        });

        _todayTransactions = todayTxns;

        // 2. Summary Cards
        lblCardCustomersValue.Text = _todayTransactions.Select(t => t.CustomerName).Distinct().Count().ToString();
        lblCardAppointmentsValue.Text = appointments.Count.ToString();
        
        // Wait list (simulated by transactions not completed/paid)
        int waiting = _todayTransactions.Count(t => t.Status == TransactionStatus.Waiting);
        lblCardWaitingValue.Text = waiting.ToString();
        
        int completed = _todayTransactions.Count(t => t.Status == TransactionStatus.Completed);
        lblCardCompletedValue.Text = completed.ToString();
        
        decimal sales = _todayTransactions.Where(t => t.Status == TransactionStatus.Completed).Sum(t => t.FinalAmount);
        lblCardSalesValue.Text = $"₱{sales:N2}";

        int presentBarbers = attendance.Count(a => a.Status == AttendanceStatus.Present);
        lblCardBarbersValue.Text = $"{presentBarbers} / {barbers.Count}";

        // 3. Grid Activity
        var completedTxns = _todayTransactions.Where(t => t.Status == TransactionStatus.Completed).ToList();
        if (completedTxns.Count > 0)
        {
            pnlEmptyState.Visible = false;
            dgvServiceActivity.Visible = true;
            dgvServiceActivity.DataSource = completedTxns.Select(t => new
            {
                Time = t.TransactionDate.ToString("HH:mm"),
                Customer = t.CustomerName,
                Service = t.ServiceName,
                Barber = t.BarberName,
                Total = $"₱{t.FinalAmount:N2}",
                Method = t.PaymentMethod.ToString(),
                t.Status
            }).ToList();
        }
        else
        {
            dgvServiceActivity.Visible = false;
            pnlEmptyState.Visible = true;
            pnlEmptyState.BringToFront();
        }

        // 4. Live Operations
        lblLiveWaitingVal.Text = waiting.ToString();
        int inService = _todayTransactions.Count(t => t.Status == TransactionStatus.InService);
        lblLiveInServiceVal.Text = inService.ToString();
        lblLiveApptsVal.Text = appointments.Count.ToString();
        
        var nextAppt = appointments.Where(a => a.ScheduledAt >= DateTime.Now).OrderBy(a => a.ScheduledAt).FirstOrDefault();
        lblLiveNextApptVal.Text = nextAppt != null ? nextAppt.ScheduledAt.ToString("HH:mm") : "None";
        lblLiveBarbersVal.Text = $"{presentBarbers} / {barbers.Count}";

        // 5. At A Glance
        int promos = _todayTransactions.Count(t => t.PromotionId.HasValue);
        lblGlance1Val.Text = promos.ToString();
        
        int loyalty = _todayTransactions.Count(t => t.DiscountAmount > 0 && !t.PromotionId.HasValue); // simplistic check
        lblGlance2Val.Text = loyalty.ToString();
        
        lblGlance3Val.Text = _todayTransactions.Count.ToString();

        // 6. Trend Chart
        pnlTrendChart.Invalidate();
    }

    private void PnlTrendChart_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        g.Clear(Color.White);

        if (_todayTransactions.Count == 0)
        {
            var font = new Font("Segoe UI", 10);
            g.DrawString("No data yet for today.", font, Brushes.Gray, 10, 10);
            return;
        }

        var completed = _todayTransactions.Where(t => t.Status == TransactionStatus.Completed).ToList();
        if (completed.Count == 0) return;

        // Group by hour
        var hourlyData = completed.GroupBy(t => t.TransactionDate.Hour)
                                  .OrderBy(g => g.Key)
                                  .ToDictionary(g => g.Key, g => g.Count());

        int startHour = 8;
        int endHour = 18;
        int barCount = endHour - startHour + 1;
        
        float width = pnlTrendChart.Width;
        float height = pnlTrendChart.Height;
        float marginX = 40;
        float marginY = 30;

        float chartW = width - (marginX * 2);
        float chartH = height - (marginY * 2);
        
        // Draw Axes
        using var pen = new Pen(Color.LightGray, 1);
        g.DrawLine(pen, marginX, height - marginY, width - marginX, height - marginY);

        int maxVal = hourlyData.Values.Count > 0 ? hourlyData.Values.Max() : 5;
        if (maxVal < 5) maxVal = 5;

        float barSpacing = chartW / barCount;
        float barWidth = Math.Min(30, barSpacing * 0.6f);

        using var barBrush = new SolidBrush(ThemeHelper.MutedGold);
        using var textBrush = new SolidBrush(ThemeHelper.DeepCharcoal);
        var textFont = new Font("Segoe UI", 8);

        for (int i = 0; i < barCount; i++)
        {
            int hour = startHour + i;
            int count = hourlyData.ContainsKey(hour) ? hourlyData[hour] : 0;

            float barH = (count / (float)maxVal) * chartH;
            float x = marginX + (i * barSpacing) + (barSpacing / 2) - (barWidth / 2);
            float y = height - marginY - barH;

            if (count > 0)
            {
                g.FillRectangle(barBrush, x, y, barWidth, barH);
                g.DrawString(count.ToString(), textFont, textBrush, x + (barWidth / 2) - 5, y - 15);
            }

            // Labels
            string timeLabel = hour <= 12 ? $"{hour}A" : $"{hour - 12}P";
            g.DrawString(timeLabel, textFont, textBrush, x + (barWidth / 2) - 10, height - marginY + 5);
        }
    }
}
