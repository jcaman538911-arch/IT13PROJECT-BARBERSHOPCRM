using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class DailyTransactionsForm
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
        lblSearch = new Label();
        txtSearch = new TextBox();
        lblDate = new Label();
        dtpDate = new DateTimePicker();
        btnFilter = new Button();
        btnReset = new Button();
        dgvTransactions = new DataGridView();
        pnlHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTransactions).BeginInit();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlHeader.BackColor = ThemeHelper.CardBackground;
        pnlHeader.BorderStyle = BorderStyle.FixedSingle;
        pnlHeader.Controls.Add(lblSearch);
        pnlHeader.Controls.Add(txtSearch);
        pnlHeader.Controls.Add(lblDate);
        pnlHeader.Controls.Add(dtpDate);
        pnlHeader.Controls.Add(btnFilter);
        pnlHeader.Controls.Add(btnReset);
        pnlHeader.Location = new Point(20, 20);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(980, 60);
        pnlHeader.TabIndex = 0;
        // 
        // lblSearch
        // 
        lblSearch.AutoSize = true;
        lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblSearch.ForeColor = ThemeHelper.TextPrimary;
        lblSearch.Location = new Point(15, 20);
        lblSearch.Name = "lblSearch";
        lblSearch.Size = new Size(52, 17);
        lblSearch.TabIndex = 0;
        lblSearch.Text = "Search:";
        // 
        // txtSearch
        // 
        txtSearch.BackColor = Color.White;
        txtSearch.BorderStyle = BorderStyle.FixedSingle;
        txtSearch.Font = new Font("Segoe UI", 9.5F);
        txtSearch.ForeColor = ThemeHelper.TextPrimary;
        txtSearch.Location = new Point(70, 18);
        txtSearch.Name = "txtSearch";
        txtSearch.Size = new Size(200, 24);
        txtSearch.TabIndex = 1;
        // 
        // lblDate
        // 
        lblDate.AutoSize = true;
        lblDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDate.ForeColor = ThemeHelper.TextPrimary;
        lblDate.Location = new Point(290, 20);
        lblDate.Name = "lblDate";
        lblDate.Size = new Size(41, 17);
        lblDate.TabIndex = 2;
        lblDate.Text = "Date:";
        // 
        // dtpDate
        // 
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(335, 18);
        dtpDate.Name = "dtpDate";
        dtpDate.Size = new Size(130, 23);
        dtpDate.TabIndex = 3;
        // 
        // btnFilter
        // 
        btnFilter.BackColor = ThemeHelper.PrimaryNavy;
        btnFilter.FlatAppearance.BorderSize = 0;
        btnFilter.FlatStyle = FlatStyle.Flat;
        btnFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnFilter.ForeColor = Color.White;
        btnFilter.Location = new Point(480, 15);
        btnFilter.Name = "btnFilter";
        btnFilter.Size = new Size(100, 30);
        btnFilter.TabIndex = 4;
        btnFilter.Text = "🔍 Search";
        btnFilter.UseVisualStyleBackColor = false;
        btnFilter.Click += btnFilter_Click;
        // 
        // btnReset
        // 
        btnReset.BackColor = ThemeHelper.CardHeaderBg;
        btnReset.FlatAppearance.BorderSize = 0;
        btnReset.FlatStyle = FlatStyle.Flat;
        btnReset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnReset.ForeColor = ThemeHelper.TextPrimary;
        btnReset.Location = new Point(590, 15);
        btnReset.Name = "btnReset";
        btnReset.Size = new Size(100, 30);
        btnReset.TabIndex = 5;
        btnReset.Text = "🔄 Reset";
        btnReset.UseVisualStyleBackColor = false;
        btnReset.Click += btnReset_Click;
        // 
        // dgvTransactions
        // 
        dgvTransactions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvTransactions.Location = new Point(20, 90);
        dgvTransactions.Name = "dgvTransactions";
        dgvTransactions.Size = new Size(980, 530);
        dgvTransactions.TabIndex = 1;
        // 
        // DailyTransactionsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvTransactions);
        Controls.Add(pnlHeader);
        FormBorderStyle = FormBorderStyle.None;
        Name = "DailyTransactionsForm";
        Text = "Daily Transactions Log";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTransactions).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlHeader;
    private Label lblSearch;
    private TextBox txtSearch;
    private Label lblDate;
    private DateTimePicker dtpDate;
    private Button btnFilter;
    private Button btnReset;
    private DataGridView dgvTransactions;
}
