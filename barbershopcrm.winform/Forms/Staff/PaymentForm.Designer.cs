using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class PaymentForm
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
        lblHeader = new Label();
        lblSubtotalLabel = new Label();
        lblSubtotalValue = new Label();
        lblDiscountLabel = new Label();
        lblDiscountValue = new Label();
        lblFinalLabel = new Label();
        lblFinalValue = new Label();
        lblPaymentMethod = new Label();
        cmbPaymentMethod = new ComboBox();
        lblAmountReceived = new Label();
        numAmountReceived = new NumericUpDown();
        lblChangeLabel = new Label();
        lblChangeValue = new Label();
        lblLoyaltyHeader = new Label();
        lblCurrentPointsLabel = new Label();
        lblCurrentPointsValue = new Label();
        numPointsToEarn = new NumericUpDown();
        lblNewBalanceLabel = new Label();
        lblNewBalanceValue = new Label();
        pnlLoyaltySection = new Panel();
        btnConfirmPayment = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)numAmountReceived).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numPointsToEarn).BeginInit();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblHeader.Location = new Point(20, 15);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(380, 30);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "Process Payment Checkout";
        lblHeader.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblSubtotalLabel
        // 
        lblSubtotalLabel.AutoSize = true;
        lblSubtotalLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblSubtotalLabel.ForeColor = ThemeHelper.TextSecondary;
        lblSubtotalLabel.Location = new Point(30, 60);
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
        lblSubtotalValue.Location = new Point(150, 60);
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
        lblDiscountLabel.Location = new Point(30, 90);
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
        lblDiscountValue.Location = new Point(150, 90);
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
        lblFinalLabel.Location = new Point(30, 125);
        lblFinalLabel.Name = "lblFinalLabel";
        lblFinalLabel.Size = new Size(106, 21);
        lblFinalLabel.TabIndex = 5;
        lblFinalLabel.Text = "Final Price:";
        // 
        // lblFinalValue
        // 
        lblFinalValue.AutoSize = true;
        lblFinalValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblFinalValue.ForeColor = ThemeHelper.PrimaryNavy;
        lblFinalValue.Location = new Point(150, 118);
        lblFinalValue.Name = "lblFinalValue";
        lblFinalValue.Size = new Size(116, 32);
        lblFinalValue.TabIndex = 6;
        lblFinalValue.Text = "₱200.00";
        // 
        // lblPaymentMethod
        // 
        lblPaymentMethod.AutoSize = true;
        lblPaymentMethod.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPaymentMethod.ForeColor = ThemeHelper.TextPrimary;
        lblPaymentMethod.Location = new Point(30, 175);
        lblPaymentMethod.Name = "lblPaymentMethod";
        lblPaymentMethod.Size = new Size(114, 17);
        lblPaymentMethod.TabIndex = 7;
        lblPaymentMethod.Text = "Payment Method";
        // 
        // cmbPaymentMethod
        // 
        cmbPaymentMethod.BackColor = Color.White;
        cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbPaymentMethod.Font = new Font("Segoe UI", 10F);
        cmbPaymentMethod.ForeColor = ThemeHelper.TextPrimary;
        cmbPaymentMethod.FormattingEnabled = true;
        cmbPaymentMethod.Location = new Point(30, 195);
        cmbPaymentMethod.Name = "cmbPaymentMethod";
        cmbPaymentMethod.Size = new Size(360, 25);
        cmbPaymentMethod.TabIndex = 8;
        // 
        // lblAmountReceived
        // 
        lblAmountReceived.AutoSize = true;
        lblAmountReceived.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblAmountReceived.ForeColor = ThemeHelper.TextPrimary;
        lblAmountReceived.Location = new Point(30, 235);
        lblAmountReceived.Name = "lblAmountReceived";
        lblAmountReceived.Size = new Size(140, 17);
        lblAmountReceived.TabIndex = 9;
        lblAmountReceived.Text = "Amount Received (₱)";
        // 
        // numAmountReceived
        // 
        numAmountReceived.BackColor = Color.White;
        numAmountReceived.DecimalPlaces = 2;
        numAmountReceived.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        numAmountReceived.ForeColor = ThemeHelper.PrimaryNavy;
        numAmountReceived.Location = new Point(30, 255);
        numAmountReceived.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        numAmountReceived.Name = "numAmountReceived";
        numAmountReceived.Size = new Size(360, 29);
        numAmountReceived.TabIndex = 10;
        numAmountReceived.ValueChanged += numAmountReceived_ValueChanged;
        // 
        // lblChangeLabel
        // 
        lblChangeLabel.AutoSize = true;
        lblChangeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblChangeLabel.ForeColor = ThemeHelper.TextPrimary;
        lblChangeLabel.Location = new Point(30, 300);
        lblChangeLabel.Name = "lblChangeLabel";
        lblChangeLabel.Size = new Size(73, 21);
        lblChangeLabel.TabIndex = 11;
        lblChangeLabel.Text = "Change:";
        // 
        // lblChangeValue
        // 
        lblChangeValue.AutoSize = true;
        lblChangeValue.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblChangeValue.ForeColor = ThemeHelper.PrimaryNavy;
        lblChangeValue.Location = new Point(150, 293);
        lblChangeValue.Name = "lblChangeValue";
        lblChangeValue.Size = new Size(87, 32);
        lblChangeValue.TabIndex = 12;
        lblChangeValue.Text = "₱0.00";
        // 
        // pnlLoyaltySection
        // 
        pnlLoyaltySection.BackColor = Color.FromArgb(45, 45, 45);
        pnlLoyaltySection.Location = new Point(30, 345);
        pnlLoyaltySection.Name = "pnlLoyaltySection";
        pnlLoyaltySection.Size = new Size(360, 110);
        pnlLoyaltySection.TabIndex = 13;
        // 
        // lblLoyaltyHeader
        // 
        lblLoyaltyHeader.AutoSize = true;
        lblLoyaltyHeader.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLoyaltyHeader.ForeColor = ThemeHelper.MutedGold;
        lblLoyaltyHeader.Location = new Point(10, 10);
        lblLoyaltyHeader.Name = "lblLoyaltyHeader";
        lblLoyaltyHeader.Size = new Size(86, 19);
        lblLoyaltyHeader.TabIndex = 14;
        lblLoyaltyHeader.Text = "LOYALTY MEMBER";
        // 
        // lblCurrentPointsLabel
        // 
        lblCurrentPointsLabel.AutoSize = true;
        lblCurrentPointsLabel.Font = new Font("Segoe UI", 8.5F);
        lblCurrentPointsLabel.ForeColor = ThemeHelper.WarmGray;
        lblCurrentPointsLabel.Location = new Point(10, 35);
        lblCurrentPointsLabel.Name = "lblCurrentPointsLabel";
        lblCurrentPointsLabel.Size = new Size(82, 15);
        lblCurrentPointsLabel.TabIndex = 15;
        lblCurrentPointsLabel.Text = "Current Points:";
        // 
        // lblCurrentPointsValue
        // 
        lblCurrentPointsValue.AutoSize = true;
        lblCurrentPointsValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCurrentPointsValue.ForeColor = ThemeHelper.WarmIvory;
        lblCurrentPointsValue.Location = new Point(100, 35);
        lblCurrentPointsValue.Name = "lblCurrentPointsValue";
        lblCurrentPointsValue.Size = new Size(12, 17);
        lblCurrentPointsValue.TabIndex = 16;
        lblCurrentPointsValue.Text = "0";
        // 

        // 
        // numPointsToEarn
        // 
        numPointsToEarn.BackColor = Color.White;
        numPointsToEarn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        numPointsToEarn.ForeColor = ThemeHelper.PrimaryNavy;
        numPointsToEarn.Location = new Point(100, 53);
        numPointsToEarn.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numPointsToEarn.Name = "numPointsToEarn";
        numPointsToEarn.Size = new Size(80, 22);
        numPointsToEarn.TabIndex = 19;
        numPointsToEarn.Value = new decimal(new int[] { 10, 0, 0, 0 });
        numPointsToEarn.ValueChanged += numPointsToEarn_ValueChanged;
        // 
        // lblNewBalanceLabel
        // 
        lblNewBalanceLabel.AutoSize = true;
        lblNewBalanceLabel.Font = new Font("Segoe UI", 8.5F);
        lblNewBalanceLabel.ForeColor = ThemeHelper.WarmGray;
        lblNewBalanceLabel.Location = new Point(10, 80);
        lblNewBalanceLabel.Name = "lblNewBalanceLabel";
        lblNewBalanceLabel.Size = new Size(91, 15);
        lblNewBalanceLabel.TabIndex = 20;
        lblNewBalanceLabel.Text = "New Balance After:";
        // 
        // lblNewBalanceValue
        // 
        lblNewBalanceValue.AutoSize = true;
        lblNewBalanceValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblNewBalanceValue.ForeColor = ThemeHelper.MutedGold;
        lblNewBalanceValue.Location = new Point(100, 80);
        lblNewBalanceValue.Name = "lblNewBalanceValue";
        lblNewBalanceValue.Size = new Size(12, 19);
        lblNewBalanceValue.TabIndex = 21;
        lblNewBalanceValue.Text = "0";
        // 
        // btnConfirmPayment
        // 
        btnConfirmPayment.BackColor = ThemeHelper.PrimaryNavy;
        btnConfirmPayment.FlatAppearance.BorderSize = 0;
        btnConfirmPayment.FlatStyle = FlatStyle.Flat;
        btnConfirmPayment.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnConfirmPayment.ForeColor = Color.White;
        btnConfirmPayment.Location = new Point(30, 465);
        btnConfirmPayment.Name = "btnConfirmPayment";
        btnConfirmPayment.Size = new Size(170, 42);
        btnConfirmPayment.TabIndex = 22;
        btnConfirmPayment.Text = "✓ Complete & Pay";
        btnConfirmPayment.UseVisualStyleBackColor = false;
        btnConfirmPayment.Click += btnConfirmPayment_Click;
        // 
        // btnCancel
        // 
        btnCancel.BackColor = ThemeHelper.CardHeaderBg;
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        btnCancel.ForeColor = ThemeHelper.TextPrimary;
        btnCancel.Location = new Point(220, 465);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(170, 42);
        btnCancel.TabIndex = 23;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += btnCancel_Click;
        // 
        // PaymentForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.CardBackground;
        ClientSize = new Size(420, 530);
        Controls.Add(btnCancel);
        Controls.Add(btnConfirmPayment);
        Controls.Add(pnlLoyaltySection);
        Controls.Add(lblChangeValue);
        Controls.Add(lblChangeLabel);
        Controls.Add(numAmountReceived);
        Controls.Add(lblAmountReceived);
        Controls.Add(cmbPaymentMethod);
        Controls.Add(lblPaymentMethod);
        Controls.Add(lblFinalValue);
        Controls.Add(lblFinalLabel);
        Controls.Add(lblDiscountValue);
        Controls.Add(lblDiscountLabel);
        Controls.Add(lblSubtotalValue);
        Controls.Add(lblSubtotalLabel);
        Controls.Add(lblHeader);
        pnlLoyaltySection.Controls.Add(lblNewBalanceValue);
        pnlLoyaltySection.Controls.Add(lblNewBalanceLabel);
        pnlLoyaltySection.Controls.Add(numPointsToEarn);
        pnlLoyaltySection.Controls.Add(lblCurrentPointsValue);
        pnlLoyaltySection.Controls.Add(lblCurrentPointsLabel);
        pnlLoyaltySection.Controls.Add(lblLoyaltyHeader);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "PaymentForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Payment Checkout";
        ((System.ComponentModel.ISupportInitialize)numAmountReceived).EndInit();
        ((System.ComponentModel.ISupportInitialize)numPointsToEarn).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblHeader;
    private Label lblSubtotalLabel;
    private Label lblSubtotalValue;
    private Label lblDiscountLabel;
    private Label lblDiscountValue;
    private Label lblFinalLabel;
    private Label lblFinalValue;
    private Label lblPaymentMethod;
    private ComboBox cmbPaymentMethod;
    private Label lblAmountReceived;
    private NumericUpDown numAmountReceived;
    private Label lblChangeLabel;
    private Label lblChangeValue;
    private Label lblLoyaltyHeader;
    private Label lblCurrentPointsLabel;
    private Label lblCurrentPointsValue;
    private NumericUpDown numPointsToEarn;
    private Label lblNewBalanceLabel;
    private Label lblNewBalanceValue;
    private Panel pnlLoyaltySection;
    private Button btnConfirmPayment;
    private Button btnCancel;
}
