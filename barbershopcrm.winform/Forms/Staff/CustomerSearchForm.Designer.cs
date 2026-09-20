using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class CustomerSearchForm
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
        lblSearch = new Label();
        txtSearch = new TextBox();
        btnSearch = new Button();
        dgvResults = new DataGridView();
        btnSelect = new Button();
        btnWalkIn = new Button();
        btnNewCustomer = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblHeader.Location = new Point(20, 15);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(560, 30);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "Search & Select Customer";
        lblHeader.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblSearch
        // 
        lblSearch.AutoSize = true;
        lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblSearch.ForeColor = ThemeHelper.TextPrimary;
        lblSearch.Location = new Point(20, 55);
        lblSearch.Name = "lblSearch";
        lblSearch.Size = new Size(52, 17);
        lblSearch.TabIndex = 1;
        lblSearch.Text = "Search:";
        // 
        // txtSearch
        // 
        txtSearch.BackColor = Color.White;
        txtSearch.BorderStyle = BorderStyle.FixedSingle;
        txtSearch.Font = new Font("Segoe UI", 10F);
        txtSearch.ForeColor = ThemeHelper.TextPrimary;
        txtSearch.Location = new Point(80, 52);
        txtSearch.Name = "txtSearch";
        txtSearch.Size = new Size(380, 25);
        txtSearch.TabIndex = 2;
        // 
        // btnSearch
        // 
        btnSearch.BackColor = ThemeHelper.PrimaryNavy;
        btnSearch.FlatAppearance.BorderSize = 0;
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSearch.ForeColor = Color.White;
        btnSearch.Location = new Point(470, 50);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(110, 28);
        btnSearch.TabIndex = 3;
        btnSearch.Text = "🔍 Search";
        btnSearch.UseVisualStyleBackColor = false;
        btnSearch.Click += btnSearch_Click;
        // 
        // dgvResults
        // 
        dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvResults.Location = new Point(20, 90);
        dgvResults.Name = "dgvResults";
        dgvResults.Size = new Size(560, 260);
        dgvResults.TabIndex = 4;
        // 
        // btnSelect
        // 
        btnSelect.BackColor = ThemeHelper.PrimaryNavy;
        btnSelect.FlatAppearance.BorderSize = 0;
        btnSelect.FlatStyle = FlatStyle.Flat;
        btnSelect.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnSelect.ForeColor = Color.White;
        btnSelect.Location = new Point(20, 365);
        btnSelect.Name = "btnSelect";
        btnSelect.Size = new Size(180, 38);
        btnSelect.TabIndex = 5;
        btnSelect.Text = "✓ Select Customer";
        btnSelect.UseVisualStyleBackColor = false;
        btnSelect.Click += btnSelect_Click;
        // 
        // btnWalkIn
        // 
        btnWalkIn.BackColor = ThemeHelper.SecondaryNavy;
        btnWalkIn.FlatAppearance.BorderSize = 0;
        btnWalkIn.FlatStyle = FlatStyle.Flat;
        btnWalkIn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnWalkIn.ForeColor = Color.White;
        btnWalkIn.Location = new Point(210, 365);
        btnWalkIn.Name = "btnWalkIn";
        btnWalkIn.Size = new Size(180, 38);
        btnWalkIn.TabIndex = 6;
        btnWalkIn.Text = "🚶 Walk-in Customer";
        btnWalkIn.UseVisualStyleBackColor = false;
        btnWalkIn.Click += btnWalkIn_Click;
        // 
        // btnNewCustomer
        // 
        btnNewCustomer.BackColor = ThemeHelper.CardHeaderBg;
        btnNewCustomer.FlatAppearance.BorderSize = 0;
        btnNewCustomer.FlatStyle = FlatStyle.Flat;
        btnNewCustomer.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnNewCustomer.ForeColor = ThemeHelper.TextPrimary;
        btnNewCustomer.Location = new Point(400, 365);
        btnNewCustomer.Name = "btnNewCustomer";
        btnNewCustomer.Size = new Size(180, 38);
        btnNewCustomer.TabIndex = 7;
        btnNewCustomer.Text = "➕ Register New";
        btnNewCustomer.UseVisualStyleBackColor = false;
        btnNewCustomer.Click += btnNewCustomer_Click;
        // 
        // CustomerSearchForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.CardBackground;
        ClientSize = new Size(600, 420);
        Controls.Add(btnNewCustomer);
        Controls.Add(btnWalkIn);
        Controls.Add(btnSelect);
        Controls.Add(dgvResults);
        Controls.Add(btnSearch);
        Controls.Add(txtSearch);
        Controls.Add(lblSearch);
        Controls.Add(lblHeader);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "CustomerSearchForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Search Customer Modal";
        ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblHeader;
    private Label lblSearch;
    private TextBox txtSearch;
    private Button btnSearch;
    private DataGridView dgvResults;
    private Button btnSelect;
    private Button btnWalkIn;
    private Button btnNewCustomer;
}
