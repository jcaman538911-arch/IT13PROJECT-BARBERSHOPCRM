using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class CustomerHistoryForm
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
        pnlHeader = new Panel();
        lblCustomer = new Label();
        cmbCustomers = new ComboBox();
        btnFilter = new Button();
        lblLoyaltySummary = new Label();
        btnLoyaltyCard = new Button();
        dgvHistory = new DataGridView();
        lblLoyaltyActivityTitle = new Label();
        dgvLoyalty = new DataGridView();
        pnlHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvLoyalty).BeginInit();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlHeader.BackColor = ThemeHelper.CardBackground;
        pnlHeader.BorderStyle = BorderStyle.FixedSingle;
        pnlHeader.Controls.Add(btnLoyaltyCard);
        pnlHeader.Controls.Add(lblLoyaltySummary);
        pnlHeader.Controls.Add(btnFilter);
        pnlHeader.Controls.Add(cmbCustomers);
        pnlHeader.Controls.Add(lblCustomer);
        pnlHeader.Location = new Point(20, 20);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(980, 60);
        pnlHeader.TabIndex = 0;
        // 
        // lblCustomer
        // 
        lblCustomer.AutoSize = true;
        lblCustomer.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblCustomer.ForeColor = ThemeHelper.TextPrimary;
        lblCustomer.Location = new Point(15, 20);
        lblCustomer.Name = "lblCustomer";
        lblCustomer.Size = new Size(111, 17);
        lblCustomer.TabIndex = 0;
        lblCustomer.Text = "Select Customer:";
        // 
        // cmbCustomers
        // 
        cmbCustomers.BackColor = Color.White;
        cmbCustomers.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCustomers.Font = new Font("Segoe UI", 10F);
        cmbCustomers.ForeColor = ThemeHelper.TextPrimary;
        cmbCustomers.FormattingEnabled = true;
        cmbCustomers.Location = new Point(135, 17);
        cmbCustomers.Name = "cmbCustomers";
        cmbCustomers.Size = new Size(300, 25);
        cmbCustomers.TabIndex = 1;
        cmbCustomers.SelectedIndexChanged += cmbCustomers_SelectedIndexChanged;
        // 
        // btnFilter
        // 
        btnFilter.BackColor = ThemeHelper.PrimaryNavy;
        btnFilter.FlatAppearance.BorderSize = 0;
        btnFilter.FlatStyle = FlatStyle.Flat;
        btnFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnFilter.ForeColor = Color.White;
        btnFilter.Location = new Point(455, 15);
        btnFilter.Name = "btnFilter";
        btnFilter.Size = new Size(120, 30);
        btnFilter.TabIndex = 2;
        btnFilter.Text = "🔍 View History";
        btnFilter.UseVisualStyleBackColor = false;
        btnFilter.Click += btnFilter_Click;
        // 
        // lblLoyaltySummary
        // 
        lblLoyaltySummary.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblLoyaltySummary.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLoyaltySummary.ForeColor = ThemeHelper.PrimaryNavy;
        lblLoyaltySummary.Location = new Point(520, 12);
        lblLoyaltySummary.Name = "lblLoyaltySummary";
        lblLoyaltySummary.Size = new Size(320, 40);
        lblLoyaltySummary.TabIndex = 3;
        lblLoyaltySummary.Text = "";
        lblLoyaltySummary.TextAlign = ContentAlignment.MiddleRight;
        //
        // btnLoyaltyCard
        //
        btnLoyaltyCard.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLoyaltyCard.BackColor = ThemeHelper.MutedGold;
        btnLoyaltyCard.FlatStyle = FlatStyle.Flat;
        btnLoyaltyCard.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnLoyaltyCard.ForeColor = ThemeHelper.DeepCharcoal;
        btnLoyaltyCard.Location = new Point(850, 17);
        btnLoyaltyCard.Name = "btnLoyaltyCard";
        btnLoyaltyCard.Size = new Size(115, 30);
        btnLoyaltyCard.TabIndex = 4;
        btnLoyaltyCard.Text = "Loyalty Card";
        btnLoyaltyCard.UseVisualStyleBackColor = false;
        btnLoyaltyCard.Visible = false;
        // 
        // dgvHistory
        // 
        dgvHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvHistory.Location = new Point(20, 90);
        dgvHistory.Name = "dgvHistory";
        dgvHistory.Size = new Size(980, 320);
        dgvHistory.TabIndex = 1;
        // 
        // lblLoyaltyActivityTitle
        // 
        lblLoyaltyActivityTitle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblLoyaltyActivityTitle.AutoSize = true;
        lblLoyaltyActivityTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblLoyaltyActivityTitle.ForeColor = ThemeHelper.PrimaryNavy;
        lblLoyaltyActivityTitle.Location = new Point(20, 418);
        lblLoyaltyActivityTitle.Name = "lblLoyaltyActivityTitle";
        lblLoyaltyActivityTitle.Size = new Size(171, 19);
        lblLoyaltyActivityTitle.TabIndex = 2;
        lblLoyaltyActivityTitle.Text = "Recent Loyalty Activity";
        // 
        // dgvLoyalty
        // 
        dgvLoyalty.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvLoyalty.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvLoyalty.Location = new Point(20, 442);
        dgvLoyalty.Name = "dgvLoyalty";
        dgvLoyalty.Size = new Size(980, 178);
        dgvLoyalty.TabIndex = 3;
        // 
        // CustomerHistoryForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvLoyalty);
        Controls.Add(lblLoyaltyActivityTitle);
        Controls.Add(dgvHistory);
        Controls.Add(pnlHeader);
        FormBorderStyle = FormBorderStyle.None;
        Name = "CustomerHistoryForm";
        Text = "Customer History";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvLoyalty).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlHeader;
    private Label lblCustomer;
    private ComboBox cmbCustomers;
    private Button btnFilter;
    private Label lblLoyaltySummary;
    private Button btnLoyaltyCard;
    private DataGridView dgvHistory;
    private Label lblLoyaltyActivityTitle;
    private DataGridView dgvLoyalty;
}
