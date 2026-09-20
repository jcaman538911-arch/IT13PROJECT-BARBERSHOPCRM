using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

partial class LoyaltyRewardsForm
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
        pnlInputs = new Panel();
        lblRewardName = new Label();
        txtRewardName = new TextBox();
        lblPointsRequired = new Label();
        numPointsRequired = new NumericUpDown();
        lblDiscountAmount = new Label();
        numDiscountAmount = new NumericUpDown();
        chkActive = new CheckBox();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        btnClear = new Button();
        lblRuleInfo = new Label();
        dgvLoyaltyRewards = new DataGridView();
        pnlInputs.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numPointsRequired).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numDiscountAmount).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvLoyaltyRewards).BeginInit();
        SuspendLayout();
        // 
        // pnlInputs
        // 
        pnlInputs.BackColor = ThemeHelper.CardBackground;
        pnlInputs.BorderStyle = BorderStyle.FixedSingle;
        pnlInputs.Controls.Add(lblRuleInfo);
        pnlInputs.Controls.Add(lblRewardName);
        pnlInputs.Controls.Add(txtRewardName);
        pnlInputs.Controls.Add(lblPointsRequired);
        pnlInputs.Controls.Add(numPointsRequired);
        pnlInputs.Controls.Add(lblDiscountAmount);
        pnlInputs.Controls.Add(numDiscountAmount);
        pnlInputs.Controls.Add(chkActive);
        pnlInputs.Controls.Add(btnAdd);
        pnlInputs.Controls.Add(btnUpdate);
        pnlInputs.Controls.Add(btnDelete);
        pnlInputs.Controls.Add(btnClear);
        pnlInputs.Location = new Point(20, 20);
        pnlInputs.Name = "pnlInputs";
        pnlInputs.Size = new Size(320, 595);
        pnlInputs.TabIndex = 0;
        // 
        // lblRuleInfo
        // 
        lblRuleInfo.BackColor = ThemeHelper.CardHeaderBg;
        lblRuleInfo.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        lblRuleInfo.ForeColor = ThemeHelper.PrimaryNavy;
        lblRuleInfo.Location = new Point(15, 12);
        lblRuleInfo.Name = "lblRuleInfo";
        lblRuleInfo.Size = new Size(290, 30);
        lblRuleInfo.TabIndex = 0;
        lblRuleInfo.Text = "★ POINT RULE: 1 Completed Haircut = +10 Points";
        lblRuleInfo.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblRewardName
        // 
        lblRewardName.AutoSize = true;
        lblRewardName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblRewardName.ForeColor = ThemeHelper.TextPrimary;
        lblRewardName.Location = new Point(15, 55);
        lblRewardName.Name = "lblRewardName";
        lblRewardName.Size = new Size(95, 17);
        lblRewardName.TabIndex = 1;
        lblRewardName.Text = "Reward Name";
        // 
        // txtRewardName
        // 
        txtRewardName.BackColor = Color.White;
        txtRewardName.BorderStyle = BorderStyle.FixedSingle;
        txtRewardName.Font = new Font("Segoe UI", 10F);
        txtRewardName.ForeColor = ThemeHelper.TextPrimary;
        txtRewardName.Location = new Point(15, 75);
        txtRewardName.Name = "txtRewardName";
        txtRewardName.Size = new Size(290, 25);
        txtRewardName.TabIndex = 2;
        // 
        // lblPointsRequired
        // 
        lblPointsRequired.AutoSize = true;
        lblPointsRequired.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPointsRequired.ForeColor = ThemeHelper.TextPrimary;
        lblPointsRequired.Location = new Point(15, 110);
        lblPointsRequired.Name = "lblPointsRequired";
        lblPointsRequired.Size = new Size(107, 17);
        lblPointsRequired.TabIndex = 3;
        lblPointsRequired.Text = "Points Required";
        // 
        // numPointsRequired
        // 
        numPointsRequired.BackColor = Color.White;
        numPointsRequired.Font = new Font("Segoe UI", 10F);
        numPointsRequired.ForeColor = ThemeHelper.TextPrimary;
        numPointsRequired.Location = new Point(15, 130);
        numPointsRequired.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        numPointsRequired.Name = "numPointsRequired";
        numPointsRequired.Size = new Size(290, 25);
        numPointsRequired.TabIndex = 4;
        // 
        // lblDiscountAmount
        // 
        lblDiscountAmount.AutoSize = true;
        lblDiscountAmount.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDiscountAmount.ForeColor = ThemeHelper.PrimaryNavy;
        lblDiscountAmount.Location = new Point(15, 165);
        lblDiscountAmount.Name = "lblDiscountAmount";
        lblDiscountAmount.Size = new Size(140, 17);
        lblDiscountAmount.TabIndex = 5;
        lblDiscountAmount.Text = "Discount Amount (₱)";
        // 
        // numDiscountAmount
        // 
        numDiscountAmount.BackColor = Color.White;
        numDiscountAmount.DecimalPlaces = 2;
        numDiscountAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        numDiscountAmount.ForeColor = ThemeHelper.PrimaryNavy;
        numDiscountAmount.Location = new Point(15, 185);
        numDiscountAmount.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numDiscountAmount.Name = "numDiscountAmount";
        numDiscountAmount.Size = new Size(290, 25);
        numDiscountAmount.TabIndex = 6;
        // 
        // chkActive
        // 
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        chkActive.ForeColor = ThemeHelper.TextPrimary;
        chkActive.Location = new Point(15, 225);
        chkActive.Name = "chkActive";
        chkActive.Size = new Size(116, 21);
        chkActive.TabIndex = 7;
        chkActive.Text = "Reward Active";
        chkActive.UseVisualStyleBackColor = true;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = ThemeHelper.MutedGold;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnAdd.ForeColor = ThemeHelper.DeepCharcoal;
        btnAdd.Location = new Point(15, 265);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(290, 35);
        btnAdd.TabIndex = 8;
        btnAdd.Text = "➕ Add Reward Tier";
        btnAdd.UseVisualStyleBackColor = false;
        btnAdd.Click += btnAdd_Click;
        // 
        // btnUpdate
        // 
        btnUpdate.BackColor = ThemeHelper.DeepCharcoal;
        btnUpdate.FlatAppearance.BorderSize = 0;
        btnUpdate.FlatStyle = FlatStyle.Flat;
        btnUpdate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnUpdate.ForeColor = ThemeHelper.WarmIvory;
        btnUpdate.Location = new Point(15, 310);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(290, 35);
        btnUpdate.TabIndex = 9;
        btnUpdate.Text = "✏️ Update Selected";
        btnUpdate.UseVisualStyleBackColor = false;
        btnUpdate.Click += btnUpdate_Click;
        // 
        // btnDelete
        // 
        btnDelete.BackColor = ThemeHelper.DeepBurgundy;
        btnDelete.FlatAppearance.BorderSize = 0;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnDelete.ForeColor = ThemeHelper.WarmIvory;
        btnDelete.Location = new Point(15, 355);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(290, 35);
        btnDelete.TabIndex = 10;
        btnDelete.Text = "🗑️ Delete Reward";
        btnDelete.UseVisualStyleBackColor = false;
        btnDelete.Click += btnDelete_Click;
        // 
        // btnClear
        // 
        btnClear.BackColor = ThemeHelper.WarmIvory;
        btnClear.FlatAppearance.BorderSize = 0;
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnClear.ForeColor = ThemeHelper.DeepCharcoal;
        btnClear.Location = new Point(15, 400);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(290, 35);
        btnClear.TabIndex = 11;
        btnClear.Text = "🔄 Clear Form";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.Click += btnClear_Click;
        // 
        // dgvLoyaltyRewards
        // 
        dgvLoyaltyRewards.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvLoyaltyRewards.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvLoyaltyRewards.Location = new Point(360, 20);
        dgvLoyaltyRewards.Name = "dgvLoyaltyRewards";
        dgvLoyaltyRewards.Size = new Size(640, 595);
        dgvLoyaltyRewards.TabIndex = 1;
        dgvLoyaltyRewards.SelectionChanged += dgvLoyaltyRewards_SelectionChanged;
        // 
        // LoyaltyRewardsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvLoyaltyRewards);
        Controls.Add(pnlInputs);
        FormBorderStyle = FormBorderStyle.None;
        Name = "LoyaltyRewardsForm";
        Text = "Loyalty & Rewards Program";
        pnlInputs.ResumeLayout(false);
        pnlInputs.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numPointsRequired).EndInit();
        ((System.ComponentModel.ISupportInitialize)numDiscountAmount).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvLoyaltyRewards).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlInputs;
    private Label lblRuleInfo;
    private Label lblRewardName;
    private TextBox txtRewardName;
    private Label lblPointsRequired;
    private NumericUpDown numPointsRequired;
    private Label lblDiscountAmount;
    private NumericUpDown numDiscountAmount;
    private CheckBox chkActive;
    private Button btnAdd;
    private Button btnUpdate;
    private Button btnDelete;
    private Button btnClear;
    private DataGridView dgvLoyaltyRewards;
}
