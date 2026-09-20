using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

partial class SystemUsersForm
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
        lblUsername = new Label();
        txtUsername = new TextBox();
        lblPassword = new Label();
        txtPassword = new TextBox();
        lblFullName = new Label();
        txtFullName = new TextBox();
        lblRole = new Label();
        cmbRole = new ComboBox();
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
        dgvUsers = new DataGridView();
        pnlInputs.SuspendLayout();
        pnlSearch.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
        SuspendLayout();
        // 
        // pnlInputs
        // 
        pnlInputs.BackColor = ThemeHelper.CardBackground;
        pnlInputs.BorderStyle = BorderStyle.FixedSingle;
        pnlInputs.Controls.Add(lblUsername);
        pnlInputs.Controls.Add(txtUsername);
        pnlInputs.Controls.Add(lblPassword);
        pnlInputs.Controls.Add(txtPassword);
        pnlInputs.Controls.Add(lblFullName);
        pnlInputs.Controls.Add(txtFullName);
        pnlInputs.Controls.Add(lblRole);
        pnlInputs.Controls.Add(cmbRole);
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
        // lblUsername
        // 
        lblUsername.AutoSize = true;
        lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblUsername.ForeColor = ThemeHelper.TextPrimary;
        lblUsername.Location = new Point(20, 20);
        lblUsername.Name = "lblUsername";
        lblUsername.Size = new Size(69, 17);
        lblUsername.TabIndex = 0;
        lblUsername.Text = "Username";
        // 
        // txtUsername
        // 
        txtUsername.BackColor = Color.White;
        txtUsername.BorderStyle = BorderStyle.FixedSingle;
        txtUsername.Font = new Font("Segoe UI", 10F);
        txtUsername.ForeColor = ThemeHelper.TextPrimary;
        txtUsername.Location = new Point(20, 42);
        txtUsername.Name = "txtUsername";
        txtUsername.Size = new Size(280, 25);
        txtUsername.TabIndex = 1;
        // 
        // lblPassword
        // 
        lblPassword.AutoSize = true;
        lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPassword.ForeColor = ThemeHelper.TextPrimary;
        lblPassword.Location = new Point(20, 80);
        lblPassword.Name = "lblPassword";
        lblPassword.Size = new Size(66, 17);
        lblPassword.TabIndex = 2;
        lblPassword.Text = "Password";
        // 
        // txtPassword
        // 
        txtPassword.BackColor = Color.White;
        txtPassword.BorderStyle = BorderStyle.FixedSingle;
        txtPassword.Font = new Font("Segoe UI", 10F);
        txtPassword.ForeColor = ThemeHelper.TextPrimary;
        txtPassword.Location = new Point(20, 102);
        txtPassword.Name = "txtPassword";
        txtPassword.Size = new Size(280, 25);
        txtPassword.TabIndex = 3;
        // 
        // lblFullName
        // 
        lblFullName.AutoSize = true;
        lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblFullName.ForeColor = ThemeHelper.TextPrimary;
        lblFullName.Location = new Point(20, 140);
        lblFullName.Name = "lblFullName";
        lblFullName.Size = new Size(71, 17);
        lblFullName.TabIndex = 4;
        lblFullName.Text = "Full Name";
        // 
        // txtFullName
        // 
        txtFullName.BackColor = Color.White;
        txtFullName.BorderStyle = BorderStyle.FixedSingle;
        txtFullName.Font = new Font("Segoe UI", 10F);
        txtFullName.ForeColor = ThemeHelper.TextPrimary;
        txtFullName.Location = new Point(20, 162);
        txtFullName.Name = "txtFullName";
        txtFullName.Size = new Size(280, 25);
        txtFullName.TabIndex = 5;
        // 
        // lblRole
        // 
        lblRole.AutoSize = true;
        lblRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblRole.ForeColor = ThemeHelper.TextPrimary;
        lblRole.Location = new Point(20, 200);
        lblRole.Name = "lblRole";
        lblRole.Size = new Size(81, 17);
        lblRole.TabIndex = 6;
        lblRole.Text = "System Role";
        // 
        // cmbRole
        // 
        cmbRole.BackColor = Color.White;
        cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbRole.Font = new Font("Segoe UI", 10F);
        cmbRole.ForeColor = ThemeHelper.TextPrimary;
        cmbRole.FormattingEnabled = true;
        cmbRole.Location = new Point(20, 222);
        cmbRole.Name = "cmbRole";
        cmbRole.Size = new Size(280, 25);
        cmbRole.TabIndex = 7;
        // 
        // chkActive
        // 
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        chkActive.ForeColor = ThemeHelper.TextPrimary;
        chkActive.Location = new Point(20, 265);
        chkActive.Name = "chkActive";
        chkActive.Size = new Size(116, 21);
        chkActive.TabIndex = 8;
        chkActive.Text = "Account Active";
        chkActive.UseVisualStyleBackColor = true;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = ThemeHelper.MutedGold;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnAdd.ForeColor = ThemeHelper.DeepCharcoal;
        btnAdd.Location = new Point(20, 310);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(280, 35);
        btnAdd.TabIndex = 9;
        btnAdd.Text = "➕ Add System User";
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
        btnUpdate.Location = new Point(20, 355);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(280, 35);
        btnUpdate.TabIndex = 10;
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
        btnDelete.Location = new Point(20, 400);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(280, 35);
        btnDelete.TabIndex = 11;
        btnDelete.Text = "🗑️ Delete User";
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
        btnClear.Location = new Point(20, 445);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(280, 35);
        btnClear.TabIndex = 12;
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
        // dgvUsers
        // 
        dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvUsers.Location = new Point(360, 80);
        dgvUsers.Name = "dgvUsers";
        dgvUsers.Size = new Size(640, 535);
        dgvUsers.TabIndex = 2;
        dgvUsers.SelectionChanged += dgvUsers_SelectionChanged;
        // 
        // SystemUsersForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvUsers);
        Controls.Add(pnlSearch);
        Controls.Add(pnlInputs);
        FormBorderStyle = FormBorderStyle.None;
        Name = "SystemUsersForm";
        Text = "System Users Management";
        pnlInputs.ResumeLayout(false);
        pnlInputs.PerformLayout();
        pnlSearch.ResumeLayout(false);
        pnlSearch.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlInputs;
    private Label lblUsername;
    private TextBox txtUsername;
    private Label lblPassword;
    private TextBox txtPassword;
    private Label lblFullName;
    private TextBox txtFullName;
    private Label lblRole;
    private ComboBox cmbRole;
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
    private DataGridView dgvUsers;
}
