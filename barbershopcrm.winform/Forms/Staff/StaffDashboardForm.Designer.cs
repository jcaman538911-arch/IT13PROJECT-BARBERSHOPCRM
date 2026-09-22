using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class StaffDashboardForm
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
        pnlQuickActions = new Panel();
        lblQuickTitle = new Label();
        btnNewCustomer = new Button();
        btnNewTransaction = new Button();
        btnApplyPromotion = new Button();
        btnRedeemLoyalty = new Button();
        btnRecordAttendance = new Button();
        pnlCard1 = new Panel();
        lblCard1Title = new Label();
        lblCard1Value = new Label();
        pnlCard2 = new Panel();
        lblCard2Title = new Label();
        lblCard2Value = new Label();
        pnlCard3 = new Panel();
        lblCard3Title = new Label();
        lblCard3Value = new Label();
        pnlCard4 = new Panel();
        lblCard4Title = new Label();
        lblCard4Value = new Label();
        lblQueueTitle = new Label();
        dgvTodayQueue = new DataGridView();
        pnlQuickActions.SuspendLayout();
        pnlCard1.SuspendLayout();
        pnlCard2.SuspendLayout();
        pnlCard3.SuspendLayout();
        pnlCard4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTodayQueue).BeginInit();
        SuspendLayout();
        // 
        // pnlQuickActions
        // 
        pnlQuickActions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlQuickActions.BackColor = ThemeHelper.CardBackground;
        pnlQuickActions.BorderStyle = BorderStyle.FixedSingle;
        pnlQuickActions.Controls.Add(btnRecordAttendance);
        pnlQuickActions.Controls.Add(btnRedeemLoyalty);
        pnlQuickActions.Controls.Add(btnApplyPromotion);
        pnlQuickActions.Controls.Add(btnNewTransaction);
        pnlQuickActions.Controls.Add(btnNewCustomer);
        pnlQuickActions.Controls.Add(lblQuickTitle);
        pnlQuickActions.Location = new Point(20, 20);
        pnlQuickActions.Name = "pnlQuickActions";
        pnlQuickActions.Size = new Size(980, 80);
        pnlQuickActions.TabIndex = 0;
        // 
        // lblQuickTitle
        // 
        lblQuickTitle.AutoSize = true;
        lblQuickTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblQuickTitle.ForeColor = ThemeHelper.PrimaryNavy;
        lblQuickTitle.Location = new Point(15, 10);
        lblQuickTitle.Name = "lblQuickTitle";
        lblQuickTitle.Size = new Size(181, 17);
        lblQuickTitle.TabIndex = 0;
        lblQuickTitle.Text = "⚡ QUICK CASHIER ACTIONS";
        // 
        // btnNewCustomer
        // 
        btnNewCustomer.BackColor = ThemeHelper.SecondaryNavy;
        btnNewCustomer.FlatAppearance.BorderSize = 0;
        btnNewCustomer.FlatStyle = FlatStyle.Flat;
        btnNewCustomer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnNewCustomer.ForeColor = Color.White;
        btnNewCustomer.Location = new Point(15, 34);
        btnNewCustomer.Name = "btnNewCustomer";
        btnNewCustomer.Size = new Size(170, 35);
        btnNewCustomer.TabIndex = 1;
        btnNewCustomer.Text = "👤 New Customer";
        btnNewCustomer.UseVisualStyleBackColor = false;
        btnNewCustomer.Click += btnNewCustomer_Click;
        // 
        // btnNewTransaction
        // 
        btnNewTransaction.BackColor = ThemeHelper.PrimaryNavy;
        btnNewTransaction.FlatAppearance.BorderSize = 0;
        btnNewTransaction.FlatStyle = FlatStyle.Flat;
        btnNewTransaction.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnNewTransaction.ForeColor = Color.White;
        btnNewTransaction.Location = new Point(195, 34);
        btnNewTransaction.Name = "btnNewTransaction";
        btnNewTransaction.Size = new Size(180, 35);
        btnNewTransaction.TabIndex = 2;
        btnNewTransaction.Text = "✂️ New Transaction POS";
        btnNewTransaction.UseVisualStyleBackColor = false;
        btnNewTransaction.Click += btnNewTransaction_Click;
        // 
        // btnApplyPromotion
        // 
        btnApplyPromotion.BackColor = ThemeHelper.CardHeaderBg;
        btnApplyPromotion.FlatAppearance.BorderSize = 0;
        btnApplyPromotion.FlatStyle = FlatStyle.Flat;
        btnApplyPromotion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnApplyPromotion.ForeColor = ThemeHelper.TextPrimary;
        btnApplyPromotion.Location = new Point(385, 34);
        btnApplyPromotion.Name = "btnApplyPromotion";
        btnApplyPromotion.Size = new Size(170, 35);
        btnApplyPromotion.TabIndex = 3;
        btnApplyPromotion.Text = "🏷️ Apply Promotion";
        btnApplyPromotion.UseVisualStyleBackColor = false;
        btnApplyPromotion.Click += btnApplyPromotion_Click;
        // 
        // btnRedeemLoyalty
        // 
        btnRedeemLoyalty.BackColor = ThemeHelper.CardHeaderBg;
        btnRedeemLoyalty.FlatAppearance.BorderSize = 0;
        btnRedeemLoyalty.FlatStyle = FlatStyle.Flat;
        btnRedeemLoyalty.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnRedeemLoyalty.ForeColor = ThemeHelper.TextPrimary;
        btnRedeemLoyalty.Location = new Point(565, 34);
        btnRedeemLoyalty.Name = "btnRedeemLoyalty";
        btnRedeemLoyalty.Size = new Size(170, 35);
        btnRedeemLoyalty.TabIndex = 4;
        btnRedeemLoyalty.Text = "⭐ Redeem Loyalty";
        btnRedeemLoyalty.UseVisualStyleBackColor = false;
        btnRedeemLoyalty.Click += btnRedeemLoyalty_Click;
        // 
        // btnRecordAttendance
        // 
        btnRecordAttendance.BackColor = ThemeHelper.SecondaryNavy;
        btnRecordAttendance.FlatAppearance.BorderSize = 0;
        btnRecordAttendance.FlatStyle = FlatStyle.Flat;
        btnRecordAttendance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnRecordAttendance.ForeColor = Color.White;
        btnRecordAttendance.Location = new Point(745, 34);
        btnRecordAttendance.Name = "btnRecordAttendance";
        btnRecordAttendance.Size = new Size(215, 35);
        btnRecordAttendance.TabIndex = 5;
        btnRecordAttendance.Text = "📋 Record Employee Attendance";
        btnRecordAttendance.UseVisualStyleBackColor = false;
        btnRecordAttendance.Click += btnRecordAttendance_Click;
        // 
        // pnlCard1
        // 
        pnlCard1.BackColor = ThemeHelper.CardBackground;
        pnlCard1.BorderStyle = BorderStyle.FixedSingle;
        pnlCard1.Controls.Add(lblCard1Value);
        pnlCard1.Controls.Add(lblCard1Title);
        pnlCard1.Location = new Point(20, 115);
        pnlCard1.Name = "pnlCard1";
        pnlCard1.Size = new Size(230, 90);
        pnlCard1.TabIndex = 1;
        // 
        // lblCard1Title
        // 
        lblCard1Title.AutoSize = true;
        lblCard1Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblCard1Title.ForeColor = ThemeHelper.TextSecondary;
        lblCard1Title.Location = new Point(15, 12);
        lblCard1Title.Name = "lblCard1Title";
        lblCard1Title.Size = new Size(119, 15);
        lblCard1Title.TabIndex = 0;
        lblCard1Title.Text = "CUSTOMERS TODAY";
        // 
        // lblCard1Value
        // 
        lblCard1Value.AutoSize = true;
        lblCard1Value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblCard1Value.ForeColor = ThemeHelper.PrimaryNavy;
        lblCard1Value.Location = new Point(15, 35);
        lblCard1Value.Name = "lblCard1Value";
        lblCard1Value.Size = new Size(33, 37);
        lblCard1Value.TabIndex = 1;
        lblCard1Value.Text = "2";
        // 
        // pnlCard2
        // 
        pnlCard2.BackColor = ThemeHelper.CardBackground;
        pnlCard2.BorderStyle = BorderStyle.FixedSingle;
        pnlCard2.Controls.Add(lblCard2Value);
        pnlCard2.Controls.Add(lblCard2Title);
        pnlCard2.Location = new Point(270, 115);
        pnlCard2.Name = "pnlCard2";
        pnlCard2.Size = new Size(230, 90);
        pnlCard2.TabIndex = 2;
        // 
        // lblCard2Title
        // 
        lblCard2Title.AutoSize = true;
        lblCard2Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblCard2Title.ForeColor = ThemeHelper.TextSecondary;
        lblCard2Title.Location = new Point(15, 12);
        lblCard2Title.Name = "lblCard2Title";
        lblCard2Title.Size = new Size(137, 15);
        lblCard2Title.TabIndex = 0;
        lblCard2Title.Text = "COMPLETED HAIRCUTS";
        // 
        // lblCard2Value
        // 
        lblCard2Value.AutoSize = true;
        lblCard2Value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblCard2Value.ForeColor = ThemeHelper.PrimaryNavy;
        lblCard2Value.Location = new Point(15, 35);
        lblCard2Value.Name = "lblCard2Value";
        lblCard2Value.Size = new Size(33, 37);
        lblCard2Value.TabIndex = 1;
        lblCard2Value.Text = "2";
        // 
        // pnlCard3
        // 
        pnlCard3.BackColor = ThemeHelper.CardBackground;
        pnlCard3.BorderStyle = BorderStyle.FixedSingle;
        pnlCard3.Controls.Add(lblCard3Value);
        pnlCard3.Controls.Add(lblCard3Title);
        pnlCard3.Location = new Point(520, 115);
        pnlCard3.Name = "pnlCard3";
        pnlCard3.Size = new Size(230, 90);
        pnlCard3.TabIndex = 3;
        // 
        // lblCard3Title
        // 
        lblCard3Title.AutoSize = true;
        lblCard3Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblCard3Title.ForeColor = ThemeHelper.TextSecondary;
        lblCard3Title.Location = new Point(15, 12);
        lblCard3Title.Name = "lblCard3Title";
        lblCard3Title.Size = new Size(96, 15);
        lblCard3Title.TabIndex = 0;
        lblCard3Title.Text = "TODAY'S SALES";
        // 
        // lblCard3Value
        // 
        lblCard3Value.AutoSize = true;
        lblCard3Value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblCard3Value.ForeColor = ThemeHelper.BarberRed;
        lblCard3Value.Location = new Point(15, 37);
        lblCard3Value.Name = "lblCard3Value";
        lblCard3Value.Size = new Size(116, 32);
        lblCard3Value.TabIndex = 1;
        lblCard3Value.Text = "₱370.00";
        // 
        // pnlCard4
        // 
        pnlCard4.BackColor = ThemeHelper.CardBackground;
        pnlCard4.BorderStyle = BorderStyle.FixedSingle;
        pnlCard4.Controls.Add(lblCard4Value);
        pnlCard4.Controls.Add(lblCard4Title);
        pnlCard4.Location = new Point(770, 115);
        pnlCard4.Name = "pnlCard4";
        pnlCard4.Size = new Size(230, 90);
        pnlCard4.TabIndex = 4;
        // 
        // lblCard4Title
        // 
        lblCard4Title.AutoSize = true;
        lblCard4Title.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblCard4Title.ForeColor = ThemeHelper.TextSecondary;
        lblCard4Title.Location = new Point(15, 12);
        lblCard4Title.Name = "lblCard4Title";
        lblCard4Title.Size = new Size(111, 15);
        lblCard4Title.TabIndex = 0;
        lblCard4Title.Text = "BARBERS PRESENT";
        // 
        // lblCard4Value
        // 
        lblCard4Value.AutoSize = true;
        lblCard4Value.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblCard4Value.ForeColor = ThemeHelper.SecondaryNavy;
        lblCard4Value.Location = new Point(15, 35);
        lblCard4Value.Name = "lblCard4Value";
        lblCard4Value.Size = new Size(71, 37);
        lblCard4Value.TabIndex = 1;
        lblCard4Value.Text = "3 / 3";
        // 
        // lblQueueTitle
        // 
        lblQueueTitle.AutoSize = true;
        lblQueueTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblQueueTitle.ForeColor = ThemeHelper.PrimaryNavy;
        lblQueueTitle.Location = new Point(20, 220);
        lblQueueTitle.Name = "lblQueueTitle";
        lblQueueTitle.Size = new Size(227, 21);
        lblQueueTitle.TabIndex = 5;
        lblQueueTitle.Text = "Today's Service Transactions";
        // 
        // dgvTodayQueue
        // 
        dgvTodayQueue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvTodayQueue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvTodayQueue.Location = new Point(20, 250);
        dgvTodayQueue.Name = "dgvTodayQueue";
        dgvTodayQueue.Size = new Size(980, 365);
        dgvTodayQueue.TabIndex = 6;
        // 
        // StaffDashboardForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvTodayQueue);
        Controls.Add(lblQueueTitle);
        Controls.Add(pnlCard4);
        Controls.Add(pnlCard3);
        Controls.Add(pnlCard2);
        Controls.Add(pnlCard1);
        Controls.Add(pnlQuickActions);
        FormBorderStyle = FormBorderStyle.None;
        Name = "StaffDashboardForm";
        Text = "Staff / Cashier Dashboard";
        pnlQuickActions.ResumeLayout(false);
        pnlQuickActions.PerformLayout();
        pnlCard1.ResumeLayout(false);
        pnlCard1.PerformLayout();
        pnlCard2.ResumeLayout(false);
        pnlCard2.PerformLayout();
        pnlCard3.ResumeLayout(false);
        pnlCard3.PerformLayout();
        pnlCard4.ResumeLayout(false);
        pnlCard4.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTodayQueue).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Panel pnlQuickActions;
    private Label lblQuickTitle;
    private Button btnNewCustomer;
    private Button btnNewTransaction;
    private Button btnApplyPromotion;
    private Button btnRedeemLoyalty;
    private Button btnRecordAttendance;
    private Panel pnlCard1;
    private Label lblCard1Title;
    private Label lblCard1Value;
    private Panel pnlCard2;
    private Label lblCard2Title;
    private Label lblCard2Value;
    private Panel pnlCard3;
    private Label lblCard3Title;
    private Label lblCard3Value;
    private Panel pnlCard4;
    private Label lblCard4Title;
    private Label lblCard4Value;
    private Label lblQueueTitle;
    private DataGridView dgvTodayQueue;
}
