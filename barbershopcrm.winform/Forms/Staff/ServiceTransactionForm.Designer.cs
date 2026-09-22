using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class ServiceTransactionForm
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
        pnlWorkflow = new Panel();
        lblCustomerHeader = new Label();
        lblCustomerName = new Label();
        btnSelectCustomer = new Button();
        lblServiceHeader = new Label();
        cmbService = new ComboBox();
        lblBarberHeader = new Label();
        cmbBarber = new ComboBox();
        lblPriceHeader = new Label();
        lblBasePriceValue = new Label();
        lblPromotionHeader = new Label();
        cmbPromotion = new ComboBox();
        lblLoyaltyRewardHeader = new Label();
        cmbLoyaltyReward = new ComboBox();
        lblLoyaltyInfo = new Label();
        pnlSummary = new Panel();
        lblSummaryTitle = new Label();
        lblSubtotalLabel = new Label();
        lblSubtotalValue = new Label();
        lblDiscountLabel = new Label();
        lblDiscountValue = new Label();
        lblFinalLabel = new Label();
        lblFinalValue = new Label();
        btnProcessPayment = new Button();
        btnSaveDraft = new Button();
        dgvActiveTransactions = new DataGridView();
        lblActiveTitle = new Label();
        pnlWorkflow.SuspendLayout();
        pnlSummary.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvActiveTransactions).BeginInit();
        SuspendLayout();
        // 
        // pnlWorkflow
        // 
        pnlWorkflow.BackColor = ThemeHelper.CardBackground;
        pnlWorkflow.BorderStyle = BorderStyle.FixedSingle;
        pnlWorkflow.Controls.Add(lblCustomerHeader);
        pnlWorkflow.Controls.Add(lblCustomerName);
        pnlWorkflow.Controls.Add(btnSelectCustomer);
        pnlWorkflow.Controls.Add(lblServiceHeader);
        pnlWorkflow.Controls.Add(cmbService);
        pnlWorkflow.Controls.Add(lblBarberHeader);
        pnlWorkflow.Controls.Add(cmbBarber);
        pnlWorkflow.Controls.Add(lblPriceHeader);
        pnlWorkflow.Controls.Add(lblBasePriceValue);
        pnlWorkflow.Controls.Add(lblPromotionHeader);
        pnlWorkflow.Controls.Add(cmbPromotion);
        pnlWorkflow.Controls.Add(lblLoyaltyRewardHeader);
        pnlWorkflow.Controls.Add(cmbLoyaltyReward);
        pnlWorkflow.Controls.Add(lblLoyaltyInfo);
        pnlWorkflow.Location = new Point(20, 20);
        pnlWorkflow.Name = "pnlWorkflow";
        pnlWorkflow.Size = new Size(340, 595);
        pnlWorkflow.TabIndex = 0;
        // 
        // lblCustomerHeader
        // 
        lblCustomerHeader.AutoSize = true;
        lblCustomerHeader.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustomerHeader.ForeColor = ThemeHelper.TextPrimary;
        lblCustomerHeader.Location = new Point(15, 15);
        lblCustomerHeader.Name = "lblCustomerHeader";
        lblCustomerHeader.Size = new Size(116, 17);
        lblCustomerHeader.TabIndex = 0;
        lblCustomerHeader.Text = "1. Customer Selection";
        // 
        // lblCustomerName
        // 
        lblCustomerName.BackColor = ThemeHelper.CardHeaderBg;
        lblCustomerName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblCustomerName.ForeColor = ThemeHelper.PrimaryNavy;
        lblCustomerName.Location = new Point(15, 35);
        lblCustomerName.Name = "lblCustomerName";
        lblCustomerName.Size = new Size(190, 28);
        lblCustomerName.TabIndex = 1;
        lblCustomerName.Text = "Walk-in Customer";
        lblCustomerName.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnSelectCustomer
        // 
        btnSelectCustomer.BackColor = ThemeHelper.PrimaryNavy;
        btnSelectCustomer.FlatAppearance.BorderSize = 0;
        btnSelectCustomer.FlatStyle = FlatStyle.Flat;
        btnSelectCustomer.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        btnSelectCustomer.ForeColor = Color.White;
        btnSelectCustomer.Location = new Point(210, 35);
        btnSelectCustomer.Name = "btnSelectCustomer";
        btnSelectCustomer.Size = new Size(115, 28);
        btnSelectCustomer.TabIndex = 2;
        btnSelectCustomer.Text = "🔍 Find/Reg";
        btnSelectCustomer.UseVisualStyleBackColor = false;
        btnSelectCustomer.Click += btnSelectCustomer_Click;
        // 
        // lblServiceHeader
        // 
        lblServiceHeader.AutoSize = true;
        lblServiceHeader.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblServiceHeader.ForeColor = ThemeHelper.TextPrimary;
        lblServiceHeader.Location = new Point(15, 75);
        lblServiceHeader.Name = "lblServiceHeader";
        lblServiceHeader.Size = new Size(130, 17);
        lblServiceHeader.TabIndex = 3;
        lblServiceHeader.Text = "2. Haircut Service";
        // 
        // cmbService
        // 
        cmbService.BackColor = Color.White;
        cmbService.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbService.Font = new Font("Segoe UI", 10F);
        cmbService.ForeColor = ThemeHelper.TextPrimary;
        cmbService.FormattingEnabled = true;
        cmbService.Location = new Point(15, 95);
        cmbService.Name = "cmbService";
        cmbService.Size = new Size(310, 25);
        cmbService.TabIndex = 4;
        cmbService.SelectedIndexChanged += cmbService_SelectedIndexChanged;
        // 
        // lblBarberHeader
        // 
        lblBarberHeader.AutoSize = true;
        lblBarberHeader.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblBarberHeader.ForeColor = ThemeHelper.TextPrimary;
        lblBarberHeader.Location = new Point(15, 135);
        lblBarberHeader.Name = "lblBarberHeader";
        lblBarberHeader.Size = new Size(174, 17);
        lblBarberHeader.TabIndex = 5;
        lblBarberHeader.Text = "3. Verbally Requested Barber";
        // 
        // cmbBarber
        // 
        cmbBarber.BackColor = Color.White;
        cmbBarber.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbBarber.Font = new Font("Segoe UI", 10F);
        cmbBarber.ForeColor = ThemeHelper.TextPrimary;
        cmbBarber.FormattingEnabled = true;
        cmbBarber.Location = new Point(15, 155);
        cmbBarber.Name = "cmbBarber";
        cmbBarber.Size = new Size(310, 25);
        cmbBarber.TabIndex = 6;
        // 
        // lblPriceHeader
        // 
        lblPriceHeader.AutoSize = true;
        lblPriceHeader.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPriceHeader.ForeColor = ThemeHelper.TextPrimary;
        lblPriceHeader.Location = new Point(15, 195);
        lblPriceHeader.Name = "lblPriceHeader";
        lblPriceHeader.Size = new Size(71, 17);
        lblPriceHeader.TabIndex = 7;
        lblPriceHeader.Text = "Base Price";
        // 
        // lblBasePriceValue
        // 
        lblBasePriceValue.AutoSize = true;
        lblBasePriceValue.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblBasePriceValue.ForeColor = ThemeHelper.PrimaryNavy;
        lblBasePriceValue.Location = new Point(15, 215);
        lblBasePriceValue.Name = "lblBasePriceValue";
        lblBasePriceValue.Size = new Size(103, 30);
        lblBasePriceValue.TabIndex = 8;
        lblBasePriceValue.Text = "₱200.00";
        // 
        // lblPromotionHeader
        // 
        lblPromotionHeader.AutoSize = true;
        lblPromotionHeader.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPromotionHeader.ForeColor = ThemeHelper.TextPrimary;
        lblPromotionHeader.Location = new Point(15, 260);
        lblPromotionHeader.Name = "lblPromotionHeader";
        lblPromotionHeader.Size = new Size(128, 17);
        lblPromotionHeader.TabIndex = 9;
        lblPromotionHeader.Text = "4. Apply Promotion";
        // 
        // cmbPromotion
        // 
        cmbPromotion.BackColor = Color.White;
        cmbPromotion.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbPromotion.Font = new Font("Segoe UI", 9.5F);
        cmbPromotion.ForeColor = ThemeHelper.TextPrimary;
        cmbPromotion.FormattingEnabled = true;
        cmbPromotion.Location = new Point(15, 280);
        cmbPromotion.Name = "cmbPromotion";
        cmbPromotion.Size = new Size(310, 23);
        cmbPromotion.TabIndex = 10;
        cmbPromotion.SelectedIndexChanged += cmbPromotion_SelectedIndexChanged;
        // 
        // lblLoyaltyRewardHeader
        // 
        lblLoyaltyRewardHeader.AutoSize = true;
        lblLoyaltyRewardHeader.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblLoyaltyRewardHeader.ForeColor = ThemeHelper.TextPrimary;
        lblLoyaltyRewardHeader.Location = new Point(15, 320);
        lblLoyaltyRewardHeader.Name = "lblLoyaltyRewardHeader";
        lblLoyaltyRewardHeader.Size = new Size(160, 17);
        lblLoyaltyRewardHeader.TabIndex = 11;
        lblLoyaltyRewardHeader.Text = "5. Redeem Loyalty Reward";
        // 
        // cmbLoyaltyReward
        // 
        cmbLoyaltyReward.BackColor = Color.White;
        cmbLoyaltyReward.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbLoyaltyReward.Font = new Font("Segoe UI", 9.5F);
        cmbLoyaltyReward.ForeColor = ThemeHelper.TextPrimary;
        cmbLoyaltyReward.FormattingEnabled = true;
        cmbLoyaltyReward.Location = new Point(15, 340);
        cmbLoyaltyReward.Name = "cmbLoyaltyReward";
        cmbLoyaltyReward.Size = new Size(310, 23);
        cmbLoyaltyReward.TabIndex = 12;
        cmbLoyaltyReward.SelectedIndexChanged += cmbLoyaltyReward_SelectedIndexChanged;
        // 
        // lblLoyaltyInfo
        // 
        lblLoyaltyInfo.BackColor = ThemeHelper.CardHeaderBg;
        lblLoyaltyInfo.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
        lblLoyaltyInfo.ForeColor = ThemeHelper.TextSecondary;
        lblLoyaltyInfo.Location = new Point(15, 375);
        lblLoyaltyInfo.Name = "lblLoyaltyInfo";
        lblLoyaltyInfo.Padding = new Padding(6, 4, 6, 4);
        lblLoyaltyInfo.Size = new Size(310, 55);
        lblLoyaltyInfo.TabIndex = 13;
        lblLoyaltyInfo.Text = "Register customer to start earning loyalty points.";
        // 
        // pnlSummary
        // 
        pnlSummary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlSummary.BackColor = ThemeHelper.CardBackground;
        pnlSummary.BorderStyle = BorderStyle.FixedSingle;
        pnlSummary.Controls.Add(btnSaveDraft);
        pnlSummary.Controls.Add(btnProcessPayment);
        pnlSummary.Controls.Add(lblFinalValue);
        pnlSummary.Controls.Add(lblFinalLabel);
        pnlSummary.Controls.Add(lblDiscountValue);
        pnlSummary.Controls.Add(lblDiscountLabel);
        pnlSummary.Controls.Add(lblSubtotalValue);
        pnlSummary.Controls.Add(lblSubtotalLabel);
        pnlSummary.Controls.Add(lblSummaryTitle);
        pnlSummary.Location = new Point(380, 20);
        pnlSummary.Name = "pnlSummary";
        pnlSummary.Size = new Size(620, 180);
        pnlSummary.TabIndex = 1;
        // 
        // lblSummaryTitle
        // 
        lblSummaryTitle.AutoSize = true;
        lblSummaryTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblSummaryTitle.ForeColor = ThemeHelper.PrimaryNavy;
        lblSummaryTitle.Location = new Point(15, 12);
        lblSummaryTitle.Name = "lblSummaryTitle";
        lblSummaryTitle.Size = new Size(168, 21);
        lblSummaryTitle.TabIndex = 0;
        lblSummaryTitle.Text = "Transaction Summary";
        // 
        // lblSubtotalLabel
        // 
        lblSubtotalLabel.AutoSize = true;
        lblSubtotalLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblSubtotalLabel.ForeColor = ThemeHelper.TextSecondary;
        lblSubtotalLabel.Location = new Point(15, 45);
        lblSubtotalLabel.Name = "lblSubtotalLabel";
        lblSubtotalLabel.Size = new Size(64, 17);
        lblSubtotalLabel.TabIndex = 1;
        lblSubtotalLabel.Text = "Subtotal:";
        // 
        // lblSubtotalValue
        // 
        lblSubtotalValue.AutoSize = true;
        lblSubtotalValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblSubtotalValue.ForeColor = ThemeHelper.TextPrimary;
        lblSubtotalValue.Location = new Point(100, 45);
        lblSubtotalValue.Name = "lblSubtotalValue";
        lblSubtotalValue.Size = new Size(62, 19);
        lblSubtotalValue.TabIndex = 2;
        lblSubtotalValue.Text = "₱200.00";
        // 
        // lblDiscountLabel
        // 
        lblDiscountLabel.AutoSize = true;
        lblDiscountLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDiscountLabel.ForeColor = ThemeHelper.TextSecondary;
        lblDiscountLabel.Location = new Point(15, 75);
        lblDiscountLabel.Name = "lblDiscountLabel";
        lblDiscountLabel.Size = new Size(67, 17);
        lblDiscountLabel.TabIndex = 3;
        lblDiscountLabel.Text = "Discount:";
        // 
        // lblDiscountValue
        // 
        lblDiscountValue.AutoSize = true;
        lblDiscountValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblDiscountValue.ForeColor = ThemeHelper.BarberRed;
        lblDiscountValue.Location = new Point(100, 75);
        lblDiscountValue.Name = "lblDiscountValue";
        lblDiscountValue.Size = new Size(58, 19);
        lblDiscountValue.TabIndex = 4;
        lblDiscountValue.Text = "- ₱0.00";
        // 
        // lblFinalLabel
        // 
        lblFinalLabel.AutoSize = true;
        lblFinalLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblFinalLabel.ForeColor = ThemeHelper.PrimaryNavy;
        lblFinalLabel.Location = new Point(15, 110);
        lblFinalLabel.Name = "lblFinalLabel";
        lblFinalLabel.Size = new Size(106, 21);
        lblFinalLabel.TabIndex = 5;
        lblFinalLabel.Text = "Final Amount:";
        // 
        // lblFinalValue
        // 
        lblFinalValue.AutoSize = true;
        lblFinalValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblFinalValue.ForeColor = ThemeHelper.PrimaryNavy;
        lblFinalValue.Location = new Point(130, 100);
        lblFinalValue.Name = "lblFinalValue";
        lblFinalValue.Size = new Size(116, 37);
        lblFinalValue.TabIndex = 6;
        lblFinalValue.Text = "₱200.00";
        // 
        // btnProcessPayment
        // 
        btnProcessPayment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnProcessPayment.BackColor = ThemeHelper.PrimaryNavy;
        btnProcessPayment.FlatAppearance.BorderSize = 0;
        btnProcessPayment.FlatStyle = FlatStyle.Flat;
        btnProcessPayment.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnProcessPayment.ForeColor = Color.White;
        btnProcessPayment.Location = new Point(390, 100);
        btnProcessPayment.Name = "btnProcessPayment";
        btnProcessPayment.Size = new Size(215, 45);
        btnProcessPayment.TabIndex = 7;
        btnProcessPayment.Text = "💳 Process Payment";
        btnProcessPayment.UseVisualStyleBackColor = false;
        btnProcessPayment.Click += btnProcessPayment_Click;
        // 
        // btnSaveDraft
        // 
        btnSaveDraft.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSaveDraft.BackColor = ThemeHelper.SecondaryNavy;
        btnSaveDraft.FlatAppearance.BorderSize = 0;
        btnSaveDraft.FlatStyle = FlatStyle.Flat;
        btnSaveDraft.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnSaveDraft.ForeColor = Color.White;
        btnSaveDraft.Location = new Point(390, 45);
        btnSaveDraft.Name = "btnSaveDraft";
        btnSaveDraft.Size = new Size(215, 35);
        btnSaveDraft.TabIndex = 8;
        btnSaveDraft.Text = "📋 Book Service (In Service)";
        btnSaveDraft.UseVisualStyleBackColor = false;
        btnSaveDraft.Click += btnSaveDraft_Click;
        // 
        // dgvActiveTransactions
        // 
        dgvActiveTransactions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvActiveTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvActiveTransactions.Location = new Point(380, 240);
        dgvActiveTransactions.Name = "dgvActiveTransactions";
        dgvActiveTransactions.Size = new Size(620, 375);
        dgvActiveTransactions.TabIndex = 2;
        dgvActiveTransactions.SelectionChanged += dgvActiveTransactions_SelectionChanged;
        // 
        // lblActiveTitle
        // 
        lblActiveTitle.AutoSize = true;
        lblActiveTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblActiveTitle.ForeColor = ThemeHelper.PrimaryNavy;
        lblActiveTitle.Location = new Point(380, 210);
        lblActiveTitle.Name = "lblActiveTitle";
        lblActiveTitle.Size = new Size(239, 20);
        lblActiveTitle.TabIndex = 3;
        lblActiveTitle.Text = "Active Haircut Queue & Receipts";
        // 
        // ServiceTransactionForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(lblActiveTitle);
        Controls.Add(dgvActiveTransactions);
        Controls.Add(pnlSummary);
        Controls.Add(pnlWorkflow);
        FormBorderStyle = FormBorderStyle.None;
        Name = "ServiceTransactionForm";
        Text = "New Service Transaction";
        pnlWorkflow.ResumeLayout(false);
        pnlWorkflow.PerformLayout();
        pnlSummary.ResumeLayout(false);
        pnlSummary.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvActiveTransactions).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Panel pnlWorkflow;
    private Label lblCustomerHeader;
    private Label lblCustomerName;
    private Button btnSelectCustomer;
    private Label lblServiceHeader;
    private ComboBox cmbService;
    private Label lblBarberHeader;
    private ComboBox cmbBarber;
    private Label lblPriceHeader;
    private Label lblBasePriceValue;
    private Label lblPromotionHeader;
    private ComboBox cmbPromotion;
    private Label lblLoyaltyRewardHeader;
    private ComboBox cmbLoyaltyReward;
    private Label lblLoyaltyInfo;
    private Panel pnlSummary;
    private Label lblSummaryTitle;
    private Label lblSubtotalLabel;
    private Label lblSubtotalValue;
    private Label lblDiscountLabel;
    private Label lblDiscountValue;
    private Label lblFinalLabel;
    private Label lblFinalValue;
    private Button btnProcessPayment;
    private Button btnSaveDraft;
    private DataGridView dgvActiveTransactions;
    private Label lblActiveTitle;
}
