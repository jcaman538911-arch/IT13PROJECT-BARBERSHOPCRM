using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

partial class AdminDashboardForm
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
        pnlCard1 = new Panel();
        lblCard1Icon = new Label();
        lblCard1Title = new Label();
        lblCard1Value = new Label();
        lblCard1Link = new Label();
        pnlCard2 = new Panel();
        lblCard2Icon = new Label();
        lblCard2Title = new Label();
        lblCard2Value = new Label();
        lblCard2Link = new Label();
        pnlCard3 = new Panel();
        lblCard3Icon = new Label();
        lblCard3Title = new Label();
        lblCard3Value = new Label();
        lblCard3Link = new Label();
        pnlCard4 = new Panel();
        lblCard4Icon = new Label();
        lblCard4Title = new Label();
        lblCard4Value = new Label();
        lblCard4Link = new Label();
        pnlCard5 = new Panel();
        lblCard5Icon = new Label();
        lblCard5Title = new Label();
        lblCard5Value = new Label();
        lblCard5Link = new Label();
        pnlCard6 = new Panel();
        lblCard6Icon = new Label();
        lblCard6Title = new Label();
        lblCard6Value = new Label();
        lblCard6Link = new Label();
        pnlActivityHeader = new Panel();
        lblRecentTitle = new Label();
        btnViewAllTransactions = new Button();
        dgvRecentTransactions = new DataGridView();
        pnlCard1.SuspendLayout();
        pnlCard2.SuspendLayout();
        pnlCard3.SuspendLayout();
        pnlCard4.SuspendLayout();
        pnlCard5.SuspendLayout();
        pnlCard6.SuspendLayout();
        pnlActivityHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvRecentTransactions).BeginInit();
        SuspendLayout();
        // 
        // pnlCard1
        // 
        pnlCard1.BackColor = ThemeHelper.WarmIvory;
        pnlCard1.BorderStyle = BorderStyle.FixedSingle;
        pnlCard1.Controls.Add(lblCard1Link);
        pnlCard1.Controls.Add(lblCard1Icon);
        pnlCard1.Controls.Add(lblCard1Value);
        pnlCard1.Controls.Add(lblCard1Title);
        pnlCard1.Location = new Point(20, 20);
        pnlCard1.Name = "pnlCard1";
        pnlCard1.Size = new Size(152, 105);
        pnlCard1.TabIndex = 0;
        // 
        // lblCard1Icon
        // 
        lblCard1Icon.BackColor = ThemeHelper.DeepCharcoal;
        lblCard1Icon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCard1Icon.ForeColor = ThemeHelper.MutedGold;
        lblCard1Icon.Location = new Point(12, 12);
        lblCard1Icon.Name = "lblCard1Icon";
        lblCard1Icon.Size = new Size(36, 36);
        lblCard1Icon.TabIndex = 2;
        lblCard1Icon.Text = "👥";
        lblCard1Icon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblCard1Title
        // 
        lblCard1Title.AutoSize = true;
        lblCard1Title.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblCard1Title.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard1Title.Location = new Point(54, 12);
        lblCard1Title.Name = "lblCard1Title";
        lblCard1Title.Size = new Size(95, 12);
        lblCard1Title.TabIndex = 0;
        lblCard1Title.Text = "CUSTOMERS TODAY";
        // 
        // lblCard1Value
        // 
        lblCard1Value.AutoSize = true;
        lblCard1Value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblCard1Value.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard1Value.Location = new Point(54, 26);
        lblCard1Value.Name = "lblCard1Value";
        lblCard1Value.Size = new Size(28, 32);
        lblCard1Value.TabIndex = 1;
        lblCard1Value.Text = "1";
        // 
        // lblCard1Link
        // 
        lblCard1Link.AutoSize = true;
        lblCard1Link.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblCard1Link.ForeColor = ThemeHelper.MutedGold;
        lblCard1Link.Location = new Point(45, 75);
        lblCard1Link.Name = "lblCard1Link";
        lblCard1Link.Size = new Size(78, 13);
        lblCard1Link.TabIndex = 3;
        lblCard1Link.Text = "View Details →";
        // 
        // pnlCard2
        // 
        pnlCard2.BackColor = ThemeHelper.WarmIvory;
        pnlCard2.BorderStyle = BorderStyle.FixedSingle;
        pnlCard2.Controls.Add(lblCard2Link);
        pnlCard2.Controls.Add(lblCard2Icon);
        pnlCard2.Controls.Add(lblCard2Value);
        pnlCard2.Controls.Add(lblCard2Title);
        pnlCard2.Location = new Point(184, 20);
        pnlCard2.Name = "pnlCard2";
        pnlCard2.Size = new Size(152, 105);
        pnlCard2.TabIndex = 1;
        // 
        // lblCard2Icon
        // 
        lblCard2Icon.BackColor = ThemeHelper.DeepCharcoal;
        lblCard2Icon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCard2Icon.ForeColor = ThemeHelper.MutedGold;
        lblCard2Icon.Location = new Point(12, 12);
        lblCard2Icon.Name = "lblCard2Icon";
        lblCard2Icon.Size = new Size(36, 36);
        lblCard2Icon.TabIndex = 2;
        lblCard2Icon.Text = "✂️";
        lblCard2Icon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblCard2Title
        // 
        lblCard2Title.AutoSize = true;
        lblCard2Title.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblCard2Title.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard2Title.Location = new Point(54, 12);
        lblCard2Title.Name = "lblCard2Title";
        lblCard2Title.Size = new Size(88, 12);
        lblCard2Title.TabIndex = 0;
        lblCard2Title.Text = "HAIRCUTS TODAY";
        // 
        // lblCard2Value
        // 
        lblCard2Value.AutoSize = true;
        lblCard2Value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblCard2Value.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard2Value.Location = new Point(54, 26);
        lblCard2Value.Name = "lblCard2Value";
        lblCard2Value.Size = new Size(28, 32);
        lblCard2Value.TabIndex = 1;
        lblCard2Value.Text = "1";
        // 
        // lblCard2Link
        // 
        lblCard2Link.AutoSize = true;
        lblCard2Link.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblCard2Link.ForeColor = ThemeHelper.MutedGold;
        lblCard2Link.Location = new Point(45, 75);
        lblCard2Link.Name = "lblCard2Link";
        lblCard2Link.Size = new Size(78, 13);
        lblCard2Link.TabIndex = 3;
        lblCard2Link.Text = "View Details →";
        // 
        // pnlCard3
        // 
        pnlCard3.BackColor = ThemeHelper.WarmIvory;
        pnlCard3.BorderStyle = BorderStyle.FixedSingle;
        pnlCard3.Controls.Add(lblCard3Link);
        pnlCard3.Controls.Add(lblCard3Icon);
        pnlCard3.Controls.Add(lblCard3Value);
        pnlCard3.Controls.Add(lblCard3Title);
        pnlCard3.Location = new Point(348, 20);
        pnlCard3.Name = "pnlCard3";
        pnlCard3.Size = new Size(156, 105);
        pnlCard3.TabIndex = 2;
        // 
        // lblCard3Icon
        // 
        lblCard3Icon.BackColor = ThemeHelper.DeepCharcoal;
        lblCard3Icon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCard3Icon.ForeColor = ThemeHelper.MutedGold;
        lblCard3Icon.Location = new Point(12, 12);
        lblCard3Icon.Name = "lblCard3Icon";
        lblCard3Icon.Size = new Size(36, 36);
        lblCard3Icon.TabIndex = 2;
        lblCard3Icon.Text = "₱";
        lblCard3Icon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblCard3Title
        // 
        lblCard3Title.AutoSize = true;
        lblCard3Title.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblCard3Title.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard3Title.Location = new Point(54, 12);
        lblCard3Title.Name = "lblCard3Title";
        lblCard3Title.Size = new Size(68, 12);
        lblCard3Title.TabIndex = 0;
        lblCard3Title.Text = "SALES TODAY";
        // 
        // lblCard3Value
        // 
        lblCard3Value.AutoSize = true;
        lblCard3Value.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblCard3Value.ForeColor = ThemeHelper.DeepBurgundy;
        lblCard3Value.Location = new Point(52, 28);
        lblCard3Value.Name = "lblCard3Value";
        lblCard3Value.Size = new Size(103, 30);
        lblCard3Value.TabIndex = 1;
        lblCard3Value.Text = "₱200.00";
        // 
        // lblCard3Link
        // 
        lblCard3Link.AutoSize = true;
        lblCard3Link.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblCard3Link.ForeColor = ThemeHelper.MutedGold;
        lblCard3Link.Location = new Point(45, 75);
        lblCard3Link.Name = "lblCard3Link";
        lblCard3Link.Size = new Size(78, 13);
        lblCard3Link.TabIndex = 3;
        lblCard3Link.Text = "View Details →";
        // 
        // pnlCard4
        // 
        pnlCard4.BackColor = ThemeHelper.WarmIvory;
        pnlCard4.BorderStyle = BorderStyle.FixedSingle;
        pnlCard4.Controls.Add(lblCard4Link);
        pnlCard4.Controls.Add(lblCard4Icon);
        pnlCard4.Controls.Add(lblCard4Value);
        pnlCard4.Controls.Add(lblCard4Title);
        pnlCard4.Location = new Point(516, 20);
        pnlCard4.Name = "pnlCard4";
        pnlCard4.Size = new Size(152, 105);
        pnlCard4.TabIndex = 3;
        // 
        // lblCard4Icon
        // 
        lblCard4Icon.BackColor = ThemeHelper.DeepCharcoal;
        lblCard4Icon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCard4Icon.ForeColor = ThemeHelper.MutedGold;
        lblCard4Icon.Location = new Point(12, 12);
        lblCard4Icon.Name = "lblCard4Icon";
        lblCard4Icon.Size = new Size(36, 36);
        lblCard4Icon.TabIndex = 2;
        lblCard4Icon.Text = "💺";
        lblCard4Icon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblCard4Title
        // 
        lblCard4Title.AutoSize = true;
        lblCard4Title.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblCard4Title.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard4Title.Location = new Point(54, 12);
        lblCard4Title.Name = "lblCard4Title";
        lblCard4Title.Size = new Size(92, 12);
        lblCard4Title.TabIndex = 0;
        lblCard4Title.Text = "BARBERS PRESENT";
        // 
        // lblCard4Value
        // 
        lblCard4Value.AutoSize = true;
        lblCard4Value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblCard4Value.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard4Value.Location = new Point(54, 26);
        lblCard4Value.Name = "lblCard4Value";
        lblCard4Value.Size = new Size(57, 32);
        lblCard4Value.TabIndex = 1;
        lblCard4Value.Text = "3 / 3";
        // 
        // lblCard4Link
        // 
        lblCard4Link.AutoSize = true;
        lblCard4Link.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblCard4Link.ForeColor = ThemeHelper.MutedGold;
        lblCard4Link.Location = new Point(45, 75);
        lblCard4Link.Name = "lblCard4Link";
        lblCard4Link.Size = new Size(78, 13);
        lblCard4Link.TabIndex = 3;
        lblCard4Link.Text = "View Details →";
        // 
        // pnlCard5
        // 
        pnlCard5.BackColor = ThemeHelper.WarmIvory;
        pnlCard5.BorderStyle = BorderStyle.FixedSingle;
        pnlCard5.Controls.Add(lblCard5Link);
        pnlCard5.Controls.Add(lblCard5Icon);
        pnlCard5.Controls.Add(lblCard5Value);
        pnlCard5.Controls.Add(lblCard5Title);
        pnlCard5.Location = new Point(680, 20);
        pnlCard5.Name = "pnlCard5";
        pnlCard5.Size = new Size(152, 105);
        pnlCard5.TabIndex = 4;
        // 
        // lblCard5Icon
        // 
        lblCard5Icon.BackColor = ThemeHelper.DeepCharcoal;
        lblCard5Icon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCard5Icon.ForeColor = ThemeHelper.MutedGold;
        lblCard5Icon.Location = new Point(12, 12);
        lblCard5Icon.Name = "lblCard5Icon";
        lblCard5Icon.Size = new Size(36, 36);
        lblCard5Icon.TabIndex = 2;
        lblCard5Icon.Text = "⭐";
        lblCard5Icon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblCard5Title
        // 
        lblCard5Title.AutoSize = true;
        lblCard5Title.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblCard5Title.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard5Title.Location = new Point(54, 12);
        lblCard5Title.Name = "lblCard5Title";
        lblCard5Title.Size = new Size(95, 12);
        lblCard5Title.TabIndex = 0;
        lblCard5Title.Text = "LOYALTY MEMBERS";
        // 
        // lblCard5Value
        // 
        lblCard5Value.AutoSize = true;
        lblCard5Value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblCard5Value.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard5Value.Location = new Point(54, 26);
        lblCard5Value.Name = "lblCard5Value";
        lblCard5Value.Size = new Size(28, 32);
        lblCard5Value.TabIndex = 1;
        lblCard5Value.Text = "3";
        // 
        // lblCard5Link
        // 
        lblCard5Link.AutoSize = true;
        lblCard5Link.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblCard5Link.ForeColor = ThemeHelper.MutedGold;
        lblCard5Link.Location = new Point(45, 75);
        lblCard5Link.Name = "lblCard5Link";
        lblCard5Link.Size = new Size(78, 13);
        lblCard5Link.TabIndex = 3;
        lblCard5Link.Text = "View Details →";
        // 
        // pnlCard6
        // 
        pnlCard6.BackColor = ThemeHelper.WarmIvory;
        pnlCard6.BorderStyle = BorderStyle.FixedSingle;
        pnlCard6.Controls.Add(lblCard6Link);
        pnlCard6.Controls.Add(lblCard6Icon);
        pnlCard6.Controls.Add(lblCard6Value);
        pnlCard6.Controls.Add(lblCard6Title);
        pnlCard6.Location = new Point(844, 20);
        pnlCard6.Name = "pnlCard6";
        pnlCard6.Size = new Size(152, 105);
        pnlCard6.TabIndex = 5;
        // 
        // lblCard6Icon
        // 
        lblCard6Icon.BackColor = ThemeHelper.DeepCharcoal;
        lblCard6Icon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCard6Icon.ForeColor = ThemeHelper.MutedGold;
        lblCard6Icon.Location = new Point(12, 12);
        lblCard6Icon.Name = "lblCard6Icon";
        lblCard6Icon.Size = new Size(36, 36);
        lblCard6Icon.TabIndex = 2;
        lblCard6Icon.Text = "🏷️";
        lblCard6Icon.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblCard6Title
        // 
        lblCard6Title.AutoSize = true;
        lblCard6Title.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        lblCard6Title.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard6Title.Location = new Point(54, 12);
        lblCard6Title.Name = "lblCard6Title";
        lblCard6Title.Size = new Size(95, 12);
        lblCard6Title.TabIndex = 0;
        lblCard6Title.Text = "PROMOTIONS USED";
        // 
        // lblCard6Value
        // 
        lblCard6Value.AutoSize = true;
        lblCard6Value.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblCard6Value.ForeColor = ThemeHelper.DeepCharcoal;
        lblCard6Value.Location = new Point(54, 26);
        lblCard6Value.Name = "lblCard6Value";
        lblCard6Value.Size = new Size(28, 32);
        lblCard6Value.TabIndex = 1;
        lblCard6Value.Text = "0";
        // 
        // lblCard6Link
        // 
        lblCard6Link.AutoSize = true;
        lblCard6Link.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
        lblCard6Link.ForeColor = ThemeHelper.MutedGold;
        lblCard6Link.Location = new Point(45, 75);
        lblCard6Link.Name = "lblCard6Link";
        lblCard6Link.Size = new Size(78, 13);
        lblCard6Link.TabIndex = 3;
        lblCard6Link.Text = "View Details →";
        // 
        // pnlActivityHeader
        // 
        pnlActivityHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlActivityHeader.BackColor = ThemeHelper.WarmIvory;
        pnlActivityHeader.Controls.Add(btnViewAllTransactions);
        pnlActivityHeader.Controls.Add(lblRecentTitle);
        pnlActivityHeader.Location = new Point(20, 140);
        pnlActivityHeader.Name = "pnlActivityHeader";
        pnlActivityHeader.Size = new Size(976, 45);
        pnlActivityHeader.TabIndex = 6;
        // 
        // lblRecentTitle
        // 
        lblRecentTitle.AutoSize = true;
        lblRecentTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblRecentTitle.ForeColor = ThemeHelper.DeepCharcoal;
        lblRecentTitle.Location = new Point(5, 10);
        lblRecentTitle.Name = "lblRecentTitle";
        lblRecentTitle.Size = new Size(240, 25);
        lblRecentTitle.TabIndex = 0;
        lblRecentTitle.Text = "🗓️ Today's Barbershop Activity";
        // 
        // btnViewAllTransactions
        // 
        btnViewAllTransactions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnViewAllTransactions.BackColor = ThemeHelper.DeepCharcoal;
        btnViewAllTransactions.FlatAppearance.BorderColor = ThemeHelper.MutedGold;
        btnViewAllTransactions.FlatAppearance.BorderSize = 1;
        btnViewAllTransactions.FlatStyle = FlatStyle.Flat;
        btnViewAllTransactions.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnViewAllTransactions.ForeColor = ThemeHelper.WarmIvory;
        btnViewAllTransactions.Location = new Point(810, 6);
        btnViewAllTransactions.Name = "btnViewAllTransactions";
        btnViewAllTransactions.Size = new Size(160, 32);
        btnViewAllTransactions.TabIndex = 1;
        btnViewAllTransactions.Text = "📜 View All Transactions";
        btnViewAllTransactions.UseVisualStyleBackColor = false;
        // 
        // dgvRecentTransactions
        // 
        dgvRecentTransactions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvRecentTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvRecentTransactions.Location = new Point(20, 190);
        dgvRecentTransactions.Name = "dgvRecentTransactions";
        dgvRecentTransactions.Size = new Size(976, 425);
        dgvRecentTransactions.TabIndex = 7;
        // 
        // AdminDashboardForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmIvory;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvRecentTransactions);
        Controls.Add(pnlActivityHeader);
        Controls.Add(pnlCard6);
        Controls.Add(pnlCard5);
        Controls.Add(pnlCard4);
        Controls.Add(pnlCard3);
        Controls.Add(pnlCard2);
        Controls.Add(pnlCard1);
        FormBorderStyle = FormBorderStyle.None;
        Name = "AdminDashboardForm";
        Text = "Admin / Owner Dashboard";
        pnlCard1.ResumeLayout(false);
        pnlCard1.PerformLayout();
        pnlCard2.ResumeLayout(false);
        pnlCard2.PerformLayout();
        pnlCard3.ResumeLayout(false);
        pnlCard3.PerformLayout();
        pnlCard4.ResumeLayout(false);
        pnlCard4.PerformLayout();
        pnlCard5.ResumeLayout(false);
        pnlCard5.PerformLayout();
        pnlCard6.ResumeLayout(false);
        pnlCard6.PerformLayout();
        pnlActivityHeader.ResumeLayout(false);
        pnlActivityHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvRecentTransactions).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlCard1;
    private Label lblCard1Icon;
    private Label lblCard1Title;
    private Label lblCard1Value;
    private Label lblCard1Link;
    private Panel pnlCard2;
    private Label lblCard2Icon;
    private Label lblCard2Title;
    private Label lblCard2Value;
    private Label lblCard2Link;
    private Panel pnlCard3;
    private Label lblCard3Icon;
    private Label lblCard3Title;
    private Label lblCard3Value;
    private Label lblCard3Link;
    private Panel pnlCard4;
    private Label lblCard4Icon;
    private Label lblCard4Title;
    private Label lblCard4Value;
    private Label lblCard4Link;
    private Panel pnlCard5;
    private Label lblCard5Icon;
    private Label lblCard5Title;
    private Label lblCard5Value;
    private Label lblCard5Link;
    private Panel pnlCard6;
    private Label lblCard6Icon;
    private Label lblCard6Title;
    private Label lblCard6Value;
    private Label lblCard6Link;
    private Panel pnlActivityHeader;
    private Label lblRecentTitle;
    private Button btnViewAllTransactions;
    private DataGridView dgvRecentTransactions;
}
