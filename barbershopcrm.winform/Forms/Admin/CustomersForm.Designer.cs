using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

partial class CustomersForm
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
        lblFullName = new Label();
        txtFullName = new TextBox();
        lblPhone = new Label();
        txtPhone = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblBirthday = new Label();
        dtpBirthday = new DateTimePicker();
        chkLoyaltyMember = new CheckBox();
        lblLoyaltyPoints = new Label();
        numLoyaltyPoints = new NumericUpDown();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        btnClear = new Button();
        pnlSearch = new Panel();
        lblSearch = new Label();
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnRefresh = new Button();
        dgvCustomers = new DataGridView();
        pnlInputs.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numLoyaltyPoints).BeginInit();
        pnlSearch.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
        SuspendLayout();
        // 
        // pnlInputs
        // 
        pnlInputs.BackColor = ThemeHelper.WarmIvory;
        pnlInputs.BorderStyle = BorderStyle.FixedSingle;
        pnlInputs.Controls.Add(lblFullName);
        pnlInputs.Controls.Add(txtFullName);
        pnlInputs.Controls.Add(lblPhone);
        pnlInputs.Controls.Add(txtPhone);
        pnlInputs.Controls.Add(lblEmail);
        pnlInputs.Controls.Add(txtEmail);
        pnlInputs.Controls.Add(lblBirthday);
        pnlInputs.Controls.Add(dtpBirthday);
        pnlInputs.Controls.Add(chkLoyaltyMember);
        pnlInputs.Controls.Add(lblLoyaltyPoints);
        pnlInputs.Controls.Add(numLoyaltyPoints);
        pnlInputs.Controls.Add(btnAdd);
        pnlInputs.Controls.Add(btnUpdate);
        pnlInputs.Controls.Add(btnDelete);
        pnlInputs.Controls.Add(btnClear);
        pnlInputs.Location = new Point(20, 20);
        pnlInputs.Name = "pnlInputs";
        pnlInputs.Size = new Size(320, 595);
        pnlInputs.TabIndex = 0;
        // 
        // lblFullName
        // 
        lblFullName.AutoSize = true;
        lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblFullName.ForeColor = ThemeHelper.DeepCharcoal;
        lblFullName.Location = new Point(20, 15);
        lblFullName.Name = "lblFullName";
        lblFullName.Size = new Size(71, 17);
        lblFullName.TabIndex = 0;
        lblFullName.Text = "Full Name";
        // 
        // txtFullName
        // 
        txtFullName.BackColor = Color.White;
        txtFullName.BorderStyle = BorderStyle.FixedSingle;
        txtFullName.Font = new Font("Segoe UI", 10F);
        txtFullName.ForeColor = ThemeHelper.DeepCharcoal;
        txtFullName.Location = new Point(20, 35);
        txtFullName.Name = "txtFullName";
        txtFullName.Size = new Size(280, 25);
        txtFullName.TabIndex = 1;
        // 
        // lblPhone
        // 
        lblPhone.AutoSize = true;
        lblPhone.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPhone.ForeColor = ThemeHelper.DeepCharcoal;
        lblPhone.Location = new Point(20, 70);
        lblPhone.Name = "lblPhone";
        lblPhone.Size = new Size(101, 17);
        lblPhone.TabIndex = 2;
        lblPhone.Text = "Phone Number";
        // 
        // txtPhone
        // 
        txtPhone.BackColor = Color.White;
        txtPhone.BorderStyle = BorderStyle.FixedSingle;
        txtPhone.Font = new Font("Segoe UI", 10F);
        txtPhone.ForeColor = ThemeHelper.DeepCharcoal;
        txtPhone.Location = new Point(20, 90);
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new Size(280, 25);
        txtPhone.TabIndex = 3;
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblEmail.ForeColor = ThemeHelper.DeepCharcoal;
        lblEmail.Location = new Point(20, 125);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(95, 17);
        lblEmail.TabIndex = 4;
        lblEmail.Text = "Email Address";
        // 
        // txtEmail
        // 
        txtEmail.BackColor = Color.White;
        txtEmail.BorderStyle = BorderStyle.FixedSingle;
        txtEmail.Font = new Font("Segoe UI", 10F);
        txtEmail.ForeColor = ThemeHelper.DeepCharcoal;
        txtEmail.Location = new Point(20, 145);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(280, 25);
        txtEmail.TabIndex = 5;
        // 
        // lblBirthday
        // 
        lblBirthday.AutoSize = true;
        lblBirthday.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblBirthday.ForeColor = ThemeHelper.DeepCharcoal;
        lblBirthday.Location = new Point(20, 180);
        lblBirthday.Name = "lblBirthday";
        lblBirthday.Size = new Size(60, 17);
        lblBirthday.TabIndex = 6;
        lblBirthday.Text = "Birthday";
        // 
        // dtpBirthday
        // 
        dtpBirthday.Format = DateTimePickerFormat.Short;
        dtpBirthday.Location = new Point(20, 200);
        dtpBirthday.Name = "dtpBirthday";
        dtpBirthday.Size = new Size(280, 23);
        dtpBirthday.TabIndex = 7;
        // 
        // chkLoyaltyMember
        // 
        chkLoyaltyMember.AutoSize = true;
        chkLoyaltyMember.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        chkLoyaltyMember.ForeColor = ThemeHelper.DeepCharcoal;
        chkLoyaltyMember.Location = new Point(20, 235);
        chkLoyaltyMember.Name = "chkLoyaltyMember";
        chkLoyaltyMember.Size = new Size(168, 21);
        chkLoyaltyMember.TabIndex = 8;
        chkLoyaltyMember.Text = "Register Loyalty Member";
        chkLoyaltyMember.UseVisualStyleBackColor = true;
        // 
        // lblLoyaltyPoints
        // 
        lblLoyaltyPoints.AutoSize = true;
        lblLoyaltyPoints.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblLoyaltyPoints.ForeColor = ThemeHelper.DeepCharcoal;
        lblLoyaltyPoints.Location = new Point(20, 265);
        lblLoyaltyPoints.Name = "lblLoyaltyPoints";
        lblLoyaltyPoints.Size = new Size(95, 17);
        lblLoyaltyPoints.TabIndex = 9;
        lblLoyaltyPoints.Text = "Loyalty Points";
        // 
        // numLoyaltyPoints
        // 
        numLoyaltyPoints.BackColor = Color.White;
        numLoyaltyPoints.Font = new Font("Segoe UI", 10F);
        numLoyaltyPoints.ForeColor = ThemeHelper.DeepCharcoal;
        numLoyaltyPoints.Location = new Point(20, 285);
        numLoyaltyPoints.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        numLoyaltyPoints.Name = "numLoyaltyPoints";
        numLoyaltyPoints.Size = new Size(280, 25);
        numLoyaltyPoints.TabIndex = 10;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = ThemeHelper.MutedGold;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnAdd.ForeColor = ThemeHelper.DeepCharcoal;
        btnAdd.Location = new Point(20, 330);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(280, 35);
        btnAdd.TabIndex = 11;
        btnAdd.Text = "➕ Register Customer";
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
        btnUpdate.Location = new Point(20, 375);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(280, 35);
        btnUpdate.TabIndex = 12;
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
        btnDelete.Location = new Point(20, 420);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(280, 35);
        btnDelete.TabIndex = 13;
        btnDelete.Text = "🗑️ Delete Customer";
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
        btnClear.Location = new Point(20, 465);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(280, 35);
        btnClear.TabIndex = 14;
        btnClear.Text = "🔄 Clear Form";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.Click += btnClear_Click;
        // 
        // pnlSearch
        // 
        pnlSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlSearch.BackColor = ThemeHelper.WarmIvory;
        pnlSearch.BorderStyle = BorderStyle.FixedSingle;
        pnlSearch.Controls.Add(lblSearch);
        pnlSearch.Controls.Add(txtSearch);
        pnlSearch.Controls.Add(btnSearch);
        pnlSearch.Controls.Add(btnRefresh);
        pnlSearch.Location = new Point(360, 20);
        pnlSearch.Name = "pnlSearch";
        pnlSearch.Size = new Size(640, 50);
        pnlSearch.TabIndex = 1;
        // 
        // lblSearch
        // 
        lblSearch.AutoSize = true;
        lblSearch.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblSearch.ForeColor = ThemeHelper.DeepCharcoal;
        lblSearch.Location = new Point(15, 16);
        lblSearch.Name = "lblSearch";
        lblSearch.Size = new Size(52, 17);
        lblSearch.TabIndex = 0;
        lblSearch.Text = "Search:";
        // 
        // txtSearch
        // 
        txtSearch.BackColor = Color.White;
        txtSearch.BorderStyle = BorderStyle.FixedSingle;
        txtSearch.Font = new Font("Segoe UI", 10F);
        txtSearch.ForeColor = ThemeHelper.DeepCharcoal;
        txtSearch.Location = new Point(75, 12);
        txtSearch.Name = "txtSearch";
        txtSearch.Size = new Size(300, 25);
        txtSearch.TabIndex = 1;
        // 
        // btnSearch
        // 
        btnSearch.BackColor = ThemeHelper.DeepCharcoal;
        btnSearch.FlatAppearance.BorderSize = 0;
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSearch.ForeColor = ThemeHelper.WarmIvory;
        btnSearch.Location = new Point(385, 10);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(90, 30);
        btnSearch.TabIndex = 2;
        btnSearch.Text = "🔍 Search";
        btnSearch.UseVisualStyleBackColor = false;
        btnSearch.Click += btnSearch_Click;
        // 
        // btnRefresh
        // 
        btnRefresh.BackColor = ThemeHelper.WarmIvory;
        btnRefresh.FlatAppearance.BorderSize = 0;
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnRefresh.ForeColor = ThemeHelper.DeepCharcoal;
        btnRefresh.Location = new Point(485, 10);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(90, 30);
        btnRefresh.TabIndex = 3;
        btnRefresh.Text = "🔄 Refresh";
        btnRefresh.UseVisualStyleBackColor = false;
        btnRefresh.Click += btnRefresh_Click;
        // 
        // dgvCustomers
        // 
        dgvCustomers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvCustomers.Location = new Point(360, 80);
        dgvCustomers.Name = "dgvCustomers";
        dgvCustomers.Size = new Size(640, 535);
        dgvCustomers.TabIndex = 2;
        dgvCustomers.SelectionChanged += dgvCustomers_SelectionChanged;
        // 
        // CustomersForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvCustomers);
        Controls.Add(pnlSearch);
        Controls.Add(pnlInputs);
        FormBorderStyle = FormBorderStyle.None;
        Name = "CustomersForm";
        Text = "Customer Management";
        pnlInputs.ResumeLayout(false);
        pnlInputs.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numLoyaltyPoints).EndInit();
        pnlSearch.ResumeLayout(false);
        pnlSearch.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlInputs;
    private Label lblFullName;
    private TextBox txtFullName;
    private Label lblPhone;
    private TextBox txtPhone;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblBirthday;
    private DateTimePicker dtpBirthday;
    private CheckBox chkLoyaltyMember;
    private Label lblLoyaltyPoints;
    private NumericUpDown numLoyaltyPoints;
    private Button btnAdd;
    private Button btnUpdate;
    private Button btnDelete;
    private Button btnClear;
    private Panel pnlSearch;
    private Label lblSearch;
    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnRefresh;
    private DataGridView dgvCustomers;
}
