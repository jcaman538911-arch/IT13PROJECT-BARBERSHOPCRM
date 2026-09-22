using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class BusinessReportsForm : Form
{
    private Button? _activeCategoryBtn = null;

    public BusinessReportsForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvReportDetails);
        HighlightTab(btnSalesReport);
        LoadSalesReport();
    }

    private void HighlightTab(Button btn)
    {
        if (_activeCategoryBtn != null)
        {
            _activeCategoryBtn.BackColor = ThemeHelper.CardHeaderBg;
            _activeCategoryBtn.ForeColor = ThemeHelper.TextPrimary;
        }

        _activeCategoryBtn = btn;
        _activeCategoryBtn.BackColor = ThemeHelper.PrimaryNavy;
        _activeCategoryBtn.ForeColor = Color.White;
    }

    private void btnSalesReport_Click(object sender, EventArgs e)
    {
        HighlightTab(btnSalesReport);
        LoadSalesReport();
    }

    private void btnBarberReport_Click(object sender, EventArgs e)
    {
        HighlightTab(btnBarberReport);
        LoadBarberReport();
    }

    private void btnCustomerReport_Click(object sender, EventArgs e)
    {
        HighlightTab(btnCustomerReport);
        LoadCustomerReport();
    }

    private void btnLoyaltyReport_Click(object sender, EventArgs e)
    {
        HighlightTab(btnLoyaltyReport);
        LoadLoyaltyReport();
    }

    private void btnPromotionReport_Click(object sender, EventArgs e)
    {
        HighlightTab(btnPromotionReport);
        LoadPromotionReport();
    }

    private void LoadSalesReport()
    {
        var txns = SqlDataRepository.Instance.GetTransactions().Where(t => t.Status == TransactionStatus.Completed).ToList();
        decimal totalSales = txns.Sum(t => t.FinalAmount);
        int totalHaircuts = txns.Count;
        decimal totalDiscounts = txns.Sum(t => t.DiscountAmount);

        lblMetric1Title.Text = "TOTAL REVENUE";
        lblMetric1Value.Text = $"₱{totalSales:N2}";

        lblMetric2Title.Text = "COMPLETED HAIRCUTS";
        lblMetric2Value.Text = totalHaircuts.ToString();

        lblMetric3Title.Text = "DISCOUNTS GIVEN";
        lblMetric3Value.Text = $"₱{totalDiscounts:N2}";

        dgvReportDetails.DataSource = txns.Select(t => new
        {
            t.TransactionNumber,
            Date = t.TransactionDate.ToString("yyyy-MM-dd HH:mm"),
            Customer = t.CustomerName,
            Service = t.ServiceName,
            Barber = t.BarberName,
            Cashier = t.StaffName,
            Subtotal = $"₱{t.Subtotal:N2}",
            Discount = $"₱{t.DiscountAmount:N2}",
            FinalAmount = $"₱{t.FinalAmount:N2}",
            Payment = t.PaymentMethod.ToString()
        }).ToList();
    }

    private void LoadBarberReport()
    {
        var txns = SqlDataRepository.Instance.GetTransactions().Where(t => t.Status == TransactionStatus.Completed).ToList();
        var barbers = SqlDataRepository.Instance.GetBarbers();
        var att = SqlDataRepository.Instance.GetAttendanceRecords();

        int activeBarbers = barbers.Count;
        int totalHaircuts = txns.Count;
        int presentBarbersToday = att.Count(a => a.Date.Date == DateTime.Today && a.Status == AttendanceStatus.Present);

        lblMetric1Title.Text = "ACTIVE BARBERS";
        lblMetric1Value.Text = activeBarbers.ToString();

        lblMetric2Title.Text = "HAIRCUTS COMPLETED";
        lblMetric2Value.Text = totalHaircuts.ToString();

        lblMetric3Title.Text = "PRESENT TODAY";
        lblMetric3Value.Text = $"{presentBarbersToday} / {activeBarbers}";

        var barberStats = barbers.Select(b =>
        {
            var bTxns = txns.Where(t => t.BarberId == b.Id).ToList();
            int haircutsCount = bTxns.Count;
            decimal revenueGenerated = bTxns.Sum(t => t.FinalAmount);
            int attendanceCount = att.Count(a => a.EmployeeId == b.Id && a.Status == AttendanceStatus.Present);

            return new
            {
                BarberId = b.Id,
                BarberName = b.Name,
                Contact = b.ContactNumber,
                HaircutsCompleted = haircutsCount,
                TotalRevenueGenerated = $"₱{revenueGenerated:N2}",
                DaysPresent = attendanceCount
            };
        }).ToList();

        dgvReportDetails.DataSource = barberStats;
    }

    private void LoadCustomerReport()
    {
        var customers = SqlDataRepository.Instance.GetCustomers();
        int totalCust = customers.Count;
        int loyaltyCount = customers.Count(c => c.IsLoyaltyMember);
        int regularCount = totalCust - loyaltyCount;

        lblMetric1Title.Text = "TOTAL CUSTOMERS";
        lblMetric1Value.Text = totalCust.ToString();

        lblMetric2Title.Text = "LOYALTY MEMBERS";
        lblMetric2Value.Text = loyaltyCount.ToString();

        lblMetric3Title.Text = "REGULAR CUSTOMERS";
        lblMetric3Value.Text = regularCount.ToString();

        dgvReportDetails.DataSource = customers.Select(c => new
        {
            c.Id,
            c.FullName,
            c.PhoneNumber,
            c.Email,
            Birthday = c.Birthday.HasValue ? c.Birthday.Value.ToString("yyyy-MM-dd") : "N/A",
            LoyaltyStatus = c.IsLoyaltyMember ? "Member" : "Non-Member",
            LoyaltyPoints = c.LoyaltyPoints,
            FirstVisited = c.CreatedDate.ToString("yyyy-MM-dd")
        }).ToList();
    }

    private void LoadLoyaltyReport()
    {
        var customers = SqlDataRepository.Instance.GetCustomers().Where(c => c.IsLoyaltyMember).ToList();
        var rewards = SqlDataRepository.Instance.GetLoyaltyRewards();
        var txns = SqlDataRepository.Instance.GetTransactions();

        int totalMembers = customers.Count;
        int totalPointsEarned = txns.Sum(t => t.PointsEarned) + customers.Sum(c => c.LoyaltyPoints);
        int totalPointsRedeemed = txns.Sum(t => t.PointsRedeemed);

        lblMetric1Title.Text = "LOYALTY MEMBERS";
        lblMetric1Value.Text = totalMembers.ToString();

        lblMetric2Title.Text = "TOTAL POINTS EARNED";
        lblMetric2Value.Text = totalPointsEarned.ToString();

        lblMetric3Title.Text = "POINTS REDEEMED";
        lblMetric3Value.Text = totalPointsRedeemed.ToString();

        dgvReportDetails.DataSource = customers.Select(c => new
        {
            c.Id,
            MemberName = c.FullName,
            c.PhoneNumber,
            CurrentPointsBalance = c.LoyaltyPoints,
            RewardEligibility = rewards.Where(r => r.PointsRequired <= c.LoyaltyPoints && r.IsActive).OrderByDescending(r => r.PointsRequired).FirstOrDefault()?.RewardName ?? "Accumulating Points",
            MemberSince = c.CreatedDate.ToString("yyyy-MM-dd")
        }).ToList();
    }

    private void LoadPromotionReport()
    {
        var promos = SqlDataRepository.Instance.GetPromotions();
        var txns = SqlDataRepository.Instance.GetTransactions().Where(t => t.PromotionId.HasValue && t.Status == TransactionStatus.Completed).ToList();

        int activePromos = promos.Count(p => p.IsActive);
        int promosUsed = txns.Count;
        decimal totalDiscounted = txns.Sum(t => t.DiscountAmount);

        lblMetric1Title.Text = "ACTIVE PROMOTIONS";
        lblMetric1Value.Text = activePromos.ToString();

        lblMetric2Title.Text = "TIMES REDEEMED";
        lblMetric2Value.Text = promosUsed.ToString();

        lblMetric3Title.Text = "PROMO DISCOUNTS GIVEN";
        lblMetric3Value.Text = $"₱{totalDiscounted:N2}";

        dgvReportDetails.DataSource = promos.Select(p =>
        {
            var pTxns = txns.Where(t => t.PromotionId == p.Id).ToList();
            return new
            {
                p.Id,
                Promotion = p.Title,
                p.DiscountType,
                Value = p.DiscountType == "Percentage" ? $"{p.DiscountValue}% OFF" : $"₱{p.DiscountValue:N2} OFF",
                TimesUsed = pTxns.Count,
                TotalDiscountGiven = $"₱{pTxns.Sum(t => t.DiscountAmount):N2}",
                Status = p.IsActive ? "Active" : "Disabled"
            };
        }).ToList();
    }

    public void LoadInventoryReport()
    {
        var items = SqlDataRepository.Instance.GetInventoryItems();
        var txns = SqlDataRepository.Instance.GetInventoryTransactions();

        int totalItems = items.Count;
        int lowStockCount = items.Count(i => i.Status == "LOW STOCK");
        int outOfStockCount = items.Count(i => i.Status == "OUT OF STOCK");

        lblMetric1Title.Text = "TOTAL CATALOG ITEMS";
        lblMetric1Value.Text = totalItems.ToString();

        lblMetric2Title.Text = "LOW STOCK ALERTS";
        lblMetric2Value.Text = lowStockCount.ToString();

        lblMetric3Title.Text = "OUT OF STOCK";
        lblMetric3Value.Text = outOfStockCount.ToString();

        dgvReportDetails.DataSource = items.Select(i => new
        {
            i.Id,
            i.ItemName,
            i.Category,
            CurrentQty = $"{i.Quantity} {i.Unit}",
            i.MinimumStockLevel,
            UnitCost = $"₱{i.Cost:N2}",
            TotalAssetValue = $"₱{(i.Quantity * i.Cost):N2}",
            i.SupplierName,
            i.Status
        }).ToList();
    }

    public void LoadBranchReport()
    {
        var branches = SqlDataRepository.Instance.GetBranches();
        var txns = SqlDataRepository.Instance.GetTransactions().Where(t => t.Status == TransactionStatus.Completed).ToList();
        var employees = SqlDataRepository.Instance.GetEmployees();

        lblMetric1Title.Text = "TOTAL BRANCHES";
        lblMetric1Value.Text = branches.Count.ToString();

        lblMetric2Title.Text = "TOTAL EMPLOYEES";
        lblMetric2Value.Text = employees.Count.ToString();

        lblMetric3Title.Text = "OVERALL REVENUE";
        lblMetric3Value.Text = $"₱{txns.Sum(t => t.FinalAmount):N2}";

        dgvReportDetails.DataSource = branches.Select(b => new
        {
            b.Id,
            b.BranchName,
            b.Address,
            b.ContactInformation,
            AssignedStaff = employees.Count(e => e.BranchId == b.Id),
            CompletedTransactions = txns.Count,
            TotalRevenue = $"₱{txns.Sum(t => t.FinalAmount):N2}",
            b.Status
        }).ToList();
    }
}
