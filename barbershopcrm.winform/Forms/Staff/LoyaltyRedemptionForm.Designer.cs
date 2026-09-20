using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class LoyaltyRedemptionForm
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
        lblCustomerPointsInfo = new Label();
        dgvRewards = new DataGridView();
        btnRedeem = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvRewards).BeginInit();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblHeader.Location = new Point(20, 15);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(540, 30);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "Redeem Loyalty Reward";
        lblHeader.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblCustomerPointsInfo
        // 
        lblCustomerPointsInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblCustomerPointsInfo.ForeColor = ThemeHelper.TextPrimary;
        lblCustomerPointsInfo.Location = new Point(20, 48);
        lblCustomerPointsInfo.Name = "lblCustomerPointsInfo";
        lblCustomerPointsInfo.Size = new Size(540, 25);
        lblCustomerPointsInfo.TabIndex = 1;
        lblCustomerPointsInfo.Text = "Customer: Michael Santos | Current Balance: 40 Points";
        lblCustomerPointsInfo.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dgvRewards
        // 
        dgvRewards.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvRewards.Location = new Point(20, 80);
        dgvRewards.Name = "dgvRewards";
        dgvRewards.Size = new Size(540, 240);
        dgvRewards.TabIndex = 2;
        // 
        // btnRedeem
        // 
        btnRedeem.BackColor = ThemeHelper.PrimaryNavy;
        btnRedeem.FlatAppearance.BorderSize = 0;
        btnRedeem.FlatStyle = FlatStyle.Flat;
        btnRedeem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnRedeem.ForeColor = Color.White;
        btnRedeem.Location = new Point(20, 335);
        btnRedeem.Name = "btnRedeem";
        btnRedeem.Size = new Size(250, 38);
        btnRedeem.TabIndex = 3;
        btnRedeem.Text = "★ Redeem Reward";
        btnRedeem.UseVisualStyleBackColor = false;
        btnRedeem.Click += btnRedeem_Click;
        // 
        // btnCancel
        // 
        btnCancel.BackColor = ThemeHelper.CardHeaderBg;
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnCancel.ForeColor = ThemeHelper.TextPrimary;
        btnCancel.Location = new Point(310, 335);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(250, 38);
        btnCancel.TabIndex = 4;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += btnCancel_Click;
        // 
        // LoyaltyRedemptionForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.CardBackground;
        ClientSize = new Size(580, 395);
        Controls.Add(btnCancel);
        Controls.Add(btnRedeem);
        Controls.Add(dgvRewards);
        Controls.Add(lblCustomerPointsInfo);
        Controls.Add(lblHeader);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "LoyaltyRedemptionForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Loyalty Redemption Modal";
        ((System.ComponentModel.ISupportInitialize)dgvRewards).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label lblHeader;
    private Label lblCustomerPointsInfo;
    private DataGridView dgvRewards;
    private Button btnRedeem;
    private Button btnCancel;
}
