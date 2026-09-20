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
        dgvHistory = new DataGridView();
        pnlHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlHeader.BackColor = ThemeHelper.CardBackground;
        pnlHeader.BorderStyle = BorderStyle.FixedSingle;
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
        // dgvHistory
        // 
        dgvHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvHistory.Location = new Point(20, 90);
        dgvHistory.Name = "dgvHistory";
        dgvHistory.Size = new Size(980, 530);
        dgvHistory.TabIndex = 1;
        // 
        // CustomerHistoryForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvHistory);
        Controls.Add(pnlHeader);
        FormBorderStyle = FormBorderStyle.None;
        Name = "CustomerHistoryForm";
        Text = "Customer History";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlHeader;
    private Label lblCustomer;
    private ComboBox cmbCustomers;
    private Button btnFilter;
    private DataGridView dgvHistory;
}
