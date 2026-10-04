using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

public partial class BusinessReportsForm : Form
{
    private Button? _activeCategoryBtn = null;
    private readonly DateTimePicker dtpFromDate = new();
    private readonly DateTimePicker dtpToDate = new();
    private readonly CheckBox chkAllDates = new();
    private readonly Button btnExportCsv = new();
    private readonly Button btnSyncData = new();
    private readonly Panel pnlFilterBar = new();

    public BusinessReportsForm()
    {
        InitializeComponent();
        CreateFilterBarPanel();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvReportDetails);
        HighlightTab(btnSalesReport);
        LoadSalesReport();
    }

    private void CreateFilterBarPanel()
    {
        pnlFilterBar.Height = 55;
        pnlFilterBar.BackColor = ThemeHelper.WarmIvory;
        pnlFilterBar.Padding = new Padding(15, 5, 15, 5);
        pnlFilterBar.Location = new Point(20, 100);
        pnlFilterBar.Width = pnlCategory.Width;
        pnlFilterBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        var flow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            BackColor = ThemeHelper.WarmIvory
        };

        var lblFrom = new Label { Text = "From:", AutoSize = true, Margin = new Padding(0, 8, 4, 0), Font = ThemeHelper.BodyFont, ForeColor = ThemeHelper.TextSecondary };
        dtpFromDate.Format = DateTimePickerFormat.Short;
        dtpFromDate.Width = 110;
        dtpFromDate.Margin = new Padding(0, 5, 0, 0);
        dtpFromDate.Value = DateTime.Today.AddDays(-30);
        dtpFromDate.ValueChanged += (s, e) => { chkAllDates.Checked = false; ReloadCurrentTab(); };

        var lblTo = new Label { Text = "To:", AutoSize = true, Margin = new Padding(12, 8, 4, 0), Font = ThemeHelper.BodyFont, ForeColor = ThemeHelper.TextSecondary };
        dtpToDate.Format = DateTimePickerFormat.Short;
        dtpToDate.Width = 110;
        dtpToDate.Margin = new Padding(0, 5, 0, 0);
        dtpToDate.Value = DateTime.Today;
        dtpToDate.ValueChanged += (s, e) => { chkAllDates.Checked = false; ReloadCurrentTab(); };

        chkAllDates.Text = "All Dates";
        chkAllDates.AutoSize = true;
        chkAllDates.Margin = new Padding(12, 6, 12, 0);
        chkAllDates.Font = ThemeHelper.BodyFont;
        chkAllDates.ForeColor = ThemeHelper.TextPrimary;
        chkAllDates.CheckedChanged += (s, e) => ReloadCurrentTab();

        var btnToday = CreatePresetButton("Today", (s, e) => { chkAllDates.Checked = false; dtpFromDate.Value = DateTime.Today; dtpToDate.Value = DateTime.Today; });
        btnToday.Margin = new Padding(2, 3, 4, 0);
        
        var btnThisMonth = CreatePresetButton("This Month", (s, e) => { chkAllDates.Checked = false; dtpFromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); dtpToDate.Value = DateTime.Today; });
        btnThisMonth.Width = 90;
        btnThisMonth.Margin = new Padding(2, 3, 4, 0);

        btnExportCsv.Text = "📥 Export CSV";
        btnExportCsv.Size = new Size(115, 28);
        btnExportCsv.Margin = new Padding(16, 2, 6, 0);
        btnExportCsv.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnExportCsv.FlatStyle = FlatStyle.Flat;
        btnExportCsv.FlatAppearance.BorderSize = 0;
        btnExportCsv.BackColor = ThemeHelper.DeepCharcoal;
        btnExportCsv.ForeColor = ThemeHelper.MutedGold;
        btnExportCsv.Cursor = Cursors.Hand;
        btnExportCsv.Click += btnExportCsv_Click;

        btnSyncData.Text = "🔄 Cloud Sync";
        btnSyncData.Size = new Size(115, 28);
        btnSyncData.Margin = new Padding(4, 2, 0, 0);
        btnSyncData.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSyncData.FlatStyle = FlatStyle.Flat;
        btnSyncData.FlatAppearance.BorderSize = 0;
        btnSyncData.BackColor = ThemeHelper.MutedGold;
        btnSyncData.ForeColor = ThemeHelper.DeepCharcoal;
        btnSyncData.Cursor = Cursors.Hand;
        btnSyncData.Click += btnSyncData_Click;

        flow.Controls.AddRange(new Control[] { lblFrom, dtpFromDate, lblTo, dtpToDate, chkAllDates, btnToday, btnThisMonth, btnExportCsv, btnSyncData });
        pnlFilterBar.Controls.Add(flow);
        Controls.Add(pnlFilterBar);

        // Adjust top offsets
        pnlSummaryCards.Location = new Point(20, 175);
        pnlSummaryCards.Height = 100;
        pnlSummaryCards.BackColor = ThemeHelper.WarmIvory;
        pnlSummaryCards.BorderStyle = BorderStyle.None;

        var tlp = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1, BackColor = ThemeHelper.WarmIvory };
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        tlp.Controls.Add(CreateKpiCard(lblMetric1Title, lblMetric1Value, 0), 0, 0);
        tlp.Controls.Add(CreateKpiCard(lblMetric2Title, lblMetric2Value, 1), 1, 0);
        tlp.Controls.Add(CreateKpiCard(lblMetric3Title, lblMetric3Value, 2), 2, 0);

        pnlSummaryCards.Controls.Clear();
        pnlSummaryCards.Controls.Add(tlp);

        dgvReportDetails.Location = new Point(20, 295);
        dgvReportDetails.Height = Height - 315;
    }

    private Panel CreateKpiCard(Label title, Label val, int index)
    {
        var pnl = new Panel { Dock = DockStyle.Fill, Margin = new Padding(index == 0 ? 0 : 10, 2, index == 2 ? 0 : 10, 6), BackColor = ThemeHelper.WarmIvory, BorderStyle = BorderStyle.FixedSingle };
        title.Location = new Point(15, 12);
        title.ForeColor = ThemeHelper.DeepCharcoal;
        title.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        val.Location = new Point(15, 32);
        val.ForeColor = ThemeHelper.DeepBurgundy;
        val.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        pnl.Controls.Add(title);
        pnl.Controls.Add(val);
        return pnl;
    }

    private Button CreatePresetButton(string text, EventHandler onClick)
    {
        var btn = new Button
        {
            Text = text,
            Size = new Size(80, 26),
            Margin = new Padding(2, 2, 4, 0),
            Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
            FlatStyle = FlatStyle.Flat,
            FlatAppearance = { BorderSize = 0 },
            BackColor = ThemeHelper.WarmIvory,
            ForeColor = ThemeHelper.TextPrimary,
            Cursor = Cursors.Hand
        };
        btn.Click += onClick;
        return btn;
    }

    private void ReloadCurrentTab()
    {
        if (_activeCategoryBtn == btnSalesReport) LoadSalesReport();
        else if (_activeCategoryBtn == btnBarberReport) LoadBarberReport();
        else if (_activeCategoryBtn == btnCustomerReport) LoadCustomerReport();
        else if (_activeCategoryBtn == btnLoyaltyReport) LoadLoyaltyReport();
        else if (_activeCategoryBtn == btnPromotionReport) LoadPromotionReport();
    }

    private List<Transaction> GetFilteredTransactions()
    {
        var txns = SqlDataRepository.Instance.GetTransactions().Where(t => t.Status == TransactionStatus.Completed);
        if (chkAllDates.Checked) return txns.ToList();

        DateTime from = dtpFromDate.Value.Date;
        DateTime to = dtpToDate.Value.Date.AddDays(1).AddTicks(-1);
        return txns.Where(t => t.TransactionDate >= from && t.TransactionDate <= to).ToList();
    }

    private void btnExportCsv_Click(object? sender, EventArgs e)
    {
        try
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "CSV File (*.csv)|*.csv|All Files (*.*)|*.*",
                FileName = $"Report_Export_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var sb = new System.Text.StringBuilder();

                var headers = dgvReportDetails.Columns.Cast<DataGridViewColumn>().Select(c => $"\"{c.HeaderText}\"");
                sb.AppendLine(string.Join(",", headers));

                foreach (DataGridViewRow row in dgvReportDetails.Rows)
                {
                    if (row.IsNewRow) continue;
                    var cells = row.Cells.Cast<DataGridViewCell>().Select(c => $"\"{c.Value?.ToString()?.Replace("\"", "\"\"")}\"");
                    sb.AppendLine(string.Join(",", cells));
                }

                System.IO.File.WriteAllText(sfd.FileName, sb.ToString());
                MessageBox.Show($"Report successfully exported to:\n{sfd.FileName}", "Export Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Export error: {ex.Message}", "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnSyncData_Click(object? sender, EventArgs e)
    {
        try
        {
            int syncedCount = TenantConnectionFactory.SyncLocalToCloud();
            if (syncedCount > 0)
            {
                MessageBox.Show($"Cloud Sync Completed!\nSuccessfully synchronized {syncedCount} offline record(s) to Cloud database.", "Cloud Sync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadCurrentTab();
            }
            else
            {
                MessageBox.Show("All database records are already synchronized with the Cloud database.", "Cloud Sync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Cloud Sync Warning: {ex.Message}", "Cloud Sync", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
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

    private async void LoadSalesReport()
    {
        bool isAllDates = chkAllDates.Checked;
        DateTime from = dtpFromDate.Value.Date;
        DateTime to = dtpToDate.Value.Date.AddDays(1).AddTicks(-1);

        var (txns, totalSales, totalHaircuts, totalDiscounts) = await Task.Run(() => 
        {
            var allTxns = SqlDataRepository.Instance.GetTransactions().Where(t => t.Status == TransactionStatus.Completed);
            var filtered = isAllDates ? allTxns.ToList() : allTxns.Where(t => t.TransactionDate >= from && t.TransactionDate <= to).ToList();
            
            return (
                filtered,
                filtered.Sum(t => t.FinalAmount),
                filtered.Count,
                filtered.Sum(t => t.DiscountAmount)
            );
        });

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

    private async void LoadBarberReport()
    {
        bool isAllDates = chkAllDates.Checked;
        DateTime from = dtpFromDate.Value.Date;
        DateTime to = dtpToDate.Value.Date.AddDays(1).AddTicks(-1);

        var (txns, barbers, att, activeBarbers, totalHaircuts, presentBarbersToday) = await Task.Run(() => 
        {
            var allTxns = SqlDataRepository.Instance.GetTransactions().Where(t => t.Status == TransactionStatus.Completed);
            var filtered = isAllDates ? allTxns.ToList() : allTxns.Where(t => t.TransactionDate >= from && t.TransactionDate <= to).ToList();
            var bList = SqlDataRepository.Instance.GetBarbers();
            var aList = SqlDataRepository.Instance.GetAttendanceRecords();
            
            return (
                filtered,
                bList,
                aList,
                bList.Count,
                filtered.Count,
                aList.Count(a => a.Date.Date == DateTime.Today && a.Status == AttendanceStatus.Present)
            );
        });

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

    private async void LoadCustomerReport()
    {
        var (customers, totalCust, loyaltyCount, regularCount) = await Task.Run(() => 
        {
            var custs = SqlDataRepository.Instance.GetCustomers();
            int tCount = custs.Count;
            int lCount = custs.Count(c => c.IsLoyaltyMember);
            return (custs, tCount, lCount, tCount - lCount);
        });

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

    private async void LoadLoyaltyReport()
    {
        var (customers, rewards, txns, totalMembers, totalPointsEarned, totalPointsRedeemed) = await Task.Run(() => 
        {
            var custs = SqlDataRepository.Instance.GetCustomers().Where(c => c.IsLoyaltyMember).ToList();
            var rwds = SqlDataRepository.Instance.GetLoyaltyRewards();
            var allTxns = SqlDataRepository.Instance.GetTransactions();
            
            return (
                custs,
                rwds,
                allTxns,
                custs.Count,
                allTxns.Sum(t => t.PointsEarned) + custs.Sum(c => c.LoyaltyPoints),
                allTxns.Sum(t => t.PointsRedeemed)
            );
        });

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

    private async void LoadPromotionReport()
    {
        var (promos, txns, activePromos, promosUsed, totalDiscounted) = await Task.Run(() => 
        {
            var pList = SqlDataRepository.Instance.GetPromotions();
            var tList = SqlDataRepository.Instance.GetTransactions().Where(t => t.PromotionId.HasValue && t.Status == TransactionStatus.Completed).ToList();
            
            return (
                pList,
                tList,
                pList.Count(p => p.IsActive),
                tList.Count,
                tList.Sum(t => t.DiscountAmount)
            );
        });

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
