using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

partial class EmployeesForm
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
        lblName = new Label();
        txtName = new TextBox();
        lblContact = new Label();
        txtContact = new TextBox();
        lblPosition = new Label();
        cmbPosition = new ComboBox();
        chkActive = new CheckBox();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        btnClear = new Button();
        pnlSearch = new Panel();
        lblSearch = new Label();
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnRefresh = new Button();
        dgvEmployees = new DataGridView();
        pnlInputs.SuspendLayout();
        pnlSearch.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvEmployees).BeginInit();
        SuspendLayout();
        // 
        // pnlInputs
        // 
        pnlInputs.BackColor = ThemeHelper.CardBackground;
        pnlInputs.BorderStyle = BorderStyle.FixedSingle;
        pnlInputs.Controls.Add(lblName);
        pnlInputs.Controls.Add(txtName);
        pnlInputs.Controls.Add(lblContact);
        pnlInputs.Controls.Add(txtContact);
        pnlInputs.Controls.Add(lblPosition);
        pnlInputs.Controls.Add(cmbPosition);
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
        // lblName
        // 
        lblName.AutoSize = true;
        lblName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblName.ForeColor = ThemeHelper.TextPrimary;
        lblName.Location = new Point(20, 20);
        lblName.Name = "lblName";
        lblName.Size = new Size(109, 17);
        lblName.TabIndex = 0;
        lblName.Text = "Employee Name";
        // 
        // txtName
        // 
        txtName.BackColor = Color.White;
        txtName.BorderStyle = BorderStyle.FixedSingle;
        txtName.Font = new Font("Segoe UI", 10F);
        txtName.ForeColor = ThemeHelper.TextPrimary;
        txtName.Location = new Point(20, 42);
        txtName.Name = "txtName";
        txtName.Size = new Size(280, 25);
        txtName.TabIndex = 1;
        // 
        // lblContact
        // 
        lblContact.AutoSize = true;
        lblContact.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblContact.ForeColor = ThemeHelper.TextPrimary;
        lblContact.Location = new Point(20, 80);
        lblContact.Name = "lblContact";
        lblContact.Size = new Size(109, 17);
        lblContact.TabIndex = 2;
        lblContact.Text = "Contact Number";
        // 
        // txtContact
        // 
        txtContact.BackColor = Color.White;
        txtContact.BorderStyle = BorderStyle.FixedSingle;
        txtContact.Font = new Font("Segoe UI", 10F);
        txtContact.ForeColor = ThemeHelper.TextPrimary;
        txtContact.Location = new Point(20, 102);
        txtContact.Name = "txtContact";
        txtContact.Size = new Size(280, 25);
        txtContact.TabIndex = 3;
        // 
        // lblPosition
        // 
        lblPosition.AutoSize = true;
        lblPosition.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPosition.ForeColor = ThemeHelper.TextPrimary;
        lblPosition.Location = new Point(20, 140);
        lblPosition.Name = "lblPosition";
        lblPosition.Size = new Size(59, 17);
        lblPosition.TabIndex = 4;
        lblPosition.Text = "Position";
        // 
        // cmbPosition
        // 
        cmbPosition.BackColor = Color.White;
        cmbPosition.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbPosition.Font = new Font("Segoe UI", 10F);
        cmbPosition.ForeColor = ThemeHelper.TextPrimary;
        cmbPosition.FormattingEnabled = true;
        cmbPosition.Location = new Point(20, 162);
        cmbPosition.Name = "cmbPosition";
        cmbPosition.Size = new Size(280, 25);
        cmbPosition.TabIndex = 5;
        // 
        // chkActive
        // 
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        chkActive.ForeColor = ThemeHelper.TextPrimary;
        chkActive.Location = new Point(20, 205);
        chkActive.Name = "chkActive";
        chkActive.Size = new Size(128, 21);
        chkActive.TabIndex = 6;
        chkActive.Text = "Active Employee";
        chkActive.UseVisualStyleBackColor = true;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = ThemeHelper.MutedGold;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnAdd.ForeColor = ThemeHelper.DeepCharcoal;
        btnAdd.Location = new Point(20, 250);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(280, 35);
        btnAdd.TabIndex = 7;
        btnAdd.Text = "➕ Add Employee";
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
        btnUpdate.Location = new Point(20, 295);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(280, 35);
        btnUpdate.TabIndex = 8;
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
        btnDelete.Location = new Point(20, 340);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(280, 35);
        btnDelete.TabIndex = 9;
        btnDelete.Text = "🗑️ Delete Employee";
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
        btnClear.Location = new Point(20, 385);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(280, 35);
        btnClear.TabIndex = 10;
        btnClear.Text = "🔄 Clear Form";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.Click += btnClear_Click;
        // 
        // pnlSearch
        // 
        pnlSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlSearch.BackColor = ThemeHelper.CardBackground;
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
        lblSearch.ForeColor = ThemeHelper.TextPrimary;
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
        txtSearch.ForeColor = ThemeHelper.TextPrimary;
        txtSearch.Location = new Point(75, 12);
        txtSearch.Name = "txtSearch";
        txtSearch.Size = new Size(300, 25);
        txtSearch.TabIndex = 1;
        // 
        // btnSearch
        // 
        btnSearch.BackColor = ThemeHelper.PrimaryNavy;
        btnSearch.FlatAppearance.BorderSize = 0;
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnSearch.ForeColor = Color.White;
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
        btnRefresh.BackColor = ThemeHelper.CardHeaderBg;
        btnRefresh.FlatAppearance.BorderSize = 0;
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnRefresh.ForeColor = ThemeHelper.TextPrimary;
        btnRefresh.Location = new Point(485, 10);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(90, 30);
        btnRefresh.TabIndex = 3;
        btnRefresh.Text = "🔄 Refresh";
        btnRefresh.UseVisualStyleBackColor = false;
        btnRefresh.Click += btnRefresh_Click;
        // 
        // dgvEmployees
        // 
        dgvEmployees.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvEmployees.Location = new Point(360, 80);
        dgvEmployees.Name = "dgvEmployees";
        dgvEmployees.Size = new Size(640, 535);
        dgvEmployees.TabIndex = 2;
        dgvEmployees.SelectionChanged += dgvEmployees_SelectionChanged;
        // 
        // EmployeesForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvEmployees);
        Controls.Add(pnlSearch);
        Controls.Add(pnlInputs);
        FormBorderStyle = FormBorderStyle.None;
        Name = "EmployeesForm";
        Text = "Employee Management";
        pnlInputs.ResumeLayout(false);
        pnlInputs.PerformLayout();
        pnlSearch.ResumeLayout(false);
        pnlSearch.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvEmployees).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlInputs;
    private Label lblName;
    private TextBox txtName;
    private Label lblContact;
    private TextBox txtContact;
    private Label lblPosition;
    private ComboBox cmbPosition;
    private CheckBox chkActive;
    private Button btnAdd;
    private Button btnUpdate;
    private Button btnDelete;
    private Button btnClear;
    private Panel pnlSearch;
    private Label lblSearch;
    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnRefresh;
    private DataGridView dgvEmployees;
}
