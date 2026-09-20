using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

partial class BusinessReportsForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        pnlCategory = new Panel();
        btnSalesReport = new Button();
        btnBarberReport = new Button();
        btnCustomerReport = new Button();
        btnLoyaltyReport = new Button();
        btnPromotionReport = new Button();
        pnlSummaryCards = new Panel();
        lblMetric1Title = new Label();
        lblMetric1Value = new Label();
        lblMetric2Title = new Label();
        lblMetric2Value = new Label();
        lblMetric3Title = new Label();
        lblMetric3Value = new Label();
        dgvReportDetails = new DataGridView();
        pnlCategory.SuspendLayout();
        pnlSummaryCards.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvReportDetails).BeginInit();
        SuspendLayout();
        // 
        // pnlCategory
        // 
        pnlCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlCategory.BackColor = ThemeHelper.CardBackground;
        pnlCategory.BorderStyle = BorderStyle.FixedSingle;
        pnlCategory.Controls.Add(btnPromotionReport);
        pnlCategory.Controls.Add(btnLoyaltyReport);
        pnlCategory.Controls.Add(btnCustomerReport);
        pnlCategory.Controls.Add(btnBarberReport);
        pnlCategory.Controls.Add(btnSalesReport);
        pnlCategory.Location = new Point(20, 20);
        pnlCategory.Name = "pnlCategory";
        pnlCategory.Size = new Size(980, 50);
        pnlCategory.TabIndex = 0;
        // 
        // btnSalesReport
        // 
        btnSalesReport.BackColor = ThemeHelper.PrimaryNavy;
        btnSalesReport.FlatAppearance.BorderSize = 0;
        btnSalesReport.FlatStyle = FlatStyle.Flat;
        btnSalesReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnSalesReport.ForeColor = Color.White;
        btnSalesReport.Location = new Point(10, 8);
        btnSalesReport.Name = "btnSalesReport";
        btnSalesReport.Size = new Size(160, 34);
        btnSalesReport.TabIndex = 0;
        btnSalesReport.Text = "💵 Sales Report";
        btnSalesReport.UseVisualStyleBackColor = false;
        btnSalesReport.Click += btnSalesReport_Click;
        // 
        // btnBarberReport
        // 
        btnBarberReport.BackColor = ThemeHelper.CardHeaderBg;
        btnBarberReport.FlatAppearance.BorderSize = 0;
        btnBarberReport.FlatStyle = FlatStyle.Flat;
        btnBarberReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnBarberReport.ForeColor = ThemeHelper.TextPrimary;
        btnBarberReport.Location = new Point(180, 8);
        btnBarberReport.Name = "btnBarberReport";
        btnBarberReport.Size = new Size(160, 34);
        btnBarberReport.TabIndex = 1;
        btnBarberReport.Text = "💈 Barber Performance";
        btnBarberReport.UseVisualStyleBackColor = false;
        btnBarberReport.Click += btnBarberReport_Click;
        // 
        // btnCustomerReport
        // 
        btnCustomerReport.BackColor = ThemeHelper.CardHeaderBg;
        btnCustomerReport.FlatAppearance.BorderSize = 0;
        btnCustomerReport.FlatStyle = FlatStyle.Flat;
        btnCustomerReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnCustomerReport.ForeColor = ThemeHelper.TextPrimary;
        btnCustomerReport.Location = new Point(350, 8);
        btnCustomerReport.Name = "btnCustomerReport";
        btnCustomerReport.Size = new Size(160, 34);
        btnCustomerReport.TabIndex = 2;
        btnCustomerReport.Text = "👥 Customer Report";
        btnCustomerReport.UseVisualStyleBackColor = false;
        btnCustomerReport.Click += btnCustomerReport_Click;
        // 
        // btnLoyaltyReport
        // 
        btnLoyaltyReport.BackColor = ThemeHelper.CardHeaderBg;
        btnLoyaltyReport.FlatAppearance.BorderSize = 0;
        btnLoyaltyReport.FlatStyle = FlatStyle.Flat;
        btnLoyaltyReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnLoyaltyReport.ForeColor = ThemeHelper.TextPrimary;
        btnLoyaltyReport.Location = new Point(520, 8);
        btnLoyaltyReport.Name = "btnLoyaltyReport";
        btnLoyaltyReport.Size = new Size(160, 34);
        btnLoyaltyReport.TabIndex = 3;
        btnLoyaltyReport.Text = "⭐ Loyalty & Rewards";
        btnLoyaltyReport.UseVisualStyleBackColor = false;
        btnLoyaltyReport.Click += btnLoyaltyReport_Click;
        // 
        // btnPromotionReport
        // 
        btnPromotionReport.BackColor = ThemeHelper.CardHeaderBg;
        btnPromotionReport.FlatAppearance.BorderSize = 0;
        btnPromotionReport.FlatStyle = FlatStyle.Flat;
        btnPromotionReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnPromotionReport.ForeColor = ThemeHelper.TextPrimary;
        btnPromotionReport.Location = new Point(690, 8);
        btnPromotionReport.Name = "btnPromotionReport";
        btnPromotionReport.Size = new Size(160, 34);
        btnPromotionReport.TabIndex = 4;
        btnPromotionReport.Text = "🏷️ Promotion Impact";
        btnPromotionReport.UseVisualStyleBackColor = false;
        btnPromotionReport.Click += btnPromotionReport_Click;
        // 
        // pnlSummaryCards
        // 
        pnlSummaryCards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlSummaryCards.BackColor = ThemeHelper.CardBackground;
        pnlSummaryCards.BorderStyle = BorderStyle.FixedSingle;
        pnlSummaryCards.Controls.Add(lblMetric1Title);
        pnlSummaryCards.Controls.Add(lblMetric1Value);
        pnlSummaryCards.Controls.Add(lblMetric2Title);
        pnlSummaryCards.Controls.Add(lblMetric2Value);
        pnlSummaryCards.Controls.Add(lblMetric3Title);
        pnlSummaryCards.Controls.Add(lblMetric3Value);
        pnlSummaryCards.Location = new Point(20, 80);
        pnlSummaryCards.Name = "pnlSummaryCards";
        pnlSummaryCards.Size = new Size(980, 80);
        pnlSummaryCards.TabIndex = 1;
        // 
        // lblMetric1Title
        // 
        lblMetric1Title.AutoSize = true;
        lblMetric1Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMetric1Title.ForeColor = ThemeHelper.TextSecondary;
        lblMetric1Title.Location = new Point(20, 15);
        lblMetric1Title.Name = "lblMetric1Title";
        lblMetric1Title.Size = new Size(74, 15);
        lblMetric1Title.TabIndex = 0;
        lblMetric1Title.Text = "TOTAL SALES";
        // 
        // lblMetric1Value
        // 
        lblMetric1Value.AutoSize = true;
        lblMetric1Value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblMetric1Value.ForeColor = ThemeHelper.PrimaryNavy;
        lblMetric1Value.Location = new Point(20, 35);
        lblMetric1Value.Name = "lblMetric1Value";
        lblMetric1Value.Size = new Size(103, 32);
        lblMetric1Value.TabIndex = 1;
        lblMetric1Value.Text = "₱370.00";
        // 
        // lblMetric2Title
        // 
        lblMetric2Title.AutoSize = true;
        lblMetric2Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMetric2Title.ForeColor = ThemeHelper.TextSecondary;
        lblMetric2Title.Location = new Point(350, 15);
        lblMetric2Title.Name = "lblMetric2Title";
        lblMetric2Title.Size = new Size(105, 15);
        lblMetric2Title.TabIndex = 2;
        lblMetric2Title.Text = "TOTAL HAIRCUTS";
        // 
        // lblMetric2Value
        // 
        lblMetric2Value.AutoSize = true;
        lblMetric2Value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblMetric2Value.ForeColor = ThemeHelper.PrimaryNavy;
        lblMetric2Value.Location = new Point(350, 35);
        lblMetric2Value.Name = "lblMetric2Value";
        lblMetric2Value.Size = new Size(28, 32);
        lblMetric2Value.TabIndex = 3;
        lblMetric2Value.Text = "2";
        // 
        // lblMetric3Title
        // 
        lblMetric3Title.AutoSize = true;
        lblMetric3Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblMetric3Title.ForeColor = ThemeHelper.TextSecondary;
        lblMetric3Title.Location = new Point(680, 15);
        lblMetric3Title.Name = "lblMetric3Title";
        lblMetric3Title.Size = new Size(116, 15);
        lblMetric3Title.TabIndex = 4;
        lblMetric3Title.Text = "DISCOUNTS GIVEN";
        // 
        // lblMetric3Value
        // 
        lblMetric3Value.AutoSize = true;
        lblMetric3Value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblMetric3Value.ForeColor = ThemeHelper.BarberRed;
        lblMetric3Value.Location = new Point(680, 35);
        lblMetric3Value.Name = "lblMetric3Value";
        lblMetric3Value.Size = new Size(90, 32);
        lblMetric3Value.TabIndex = 5;
        lblMetric3Value.Text = "₱30.00";
        // 
        // dgvReportDetails
        // 
        dgvReportDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvReportDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvReportDetails.Location = new Point(20, 175);
        dgvReportDetails.Name = "dgvReportDetails";
        dgvReportDetails.Size = new Size(980, 445);
        dgvReportDetails.TabIndex = 2;
        // 
        // BusinessReportsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvReportDetails);
        Controls.Add(pnlSummaryCards);
        Controls.Add(pnlCategory);
        FormBorderStyle = FormBorderStyle.None;
        Name = "BusinessReportsForm";
        Text = "Business Reports";
        pnlCategory.ResumeLayout(false);
        pnlSummaryCards.ResumeLayout(false);
        pnlSummaryCards.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvReportDetails).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlCategory;
    private Button btnSalesReport;
    private Button btnBarberReport;
    private Button btnCustomerReport;
    private Button btnLoyaltyReport;
    private Button btnPromotionReport;
    private Panel pnlSummaryCards;
    private Label lblMetric1Title;
    private Label lblMetric1Value;
    private Label lblMetric2Title;
    private Label lblMetric2Value;
    private Label lblMetric3Title;
    private Label lblMetric3Value;
    private DataGridView dgvReportDetails;
}
