namespace BarberShopCRM.Forms.Admin;

partial class BranchesManagementForm
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

    private void InitializeComponent()
    {
        pnlHeader = new Panel();
        lblTitle = new Label();
        pnlForm = new Panel();
        lblBranchName = new Label();
        txtBranchName = new TextBox();
        lblAddress = new Label();
        txtAddress = new TextBox();
        lblContactInfo = new Label();
        txtContactInfo = new TextBox();
        chkIsActive = new CheckBox();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDeactivate = new Button();
        btnClear = new Button();
        dgvBranches = new DataGridView();

        pnlHeader.SuspendLayout();
        pnlForm.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvBranches).BeginInit();
        SuspendLayout();

        // pnlHeader
        pnlHeader.BackColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;
        pnlHeader.Controls.Add(lblTitle);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Height = 60;

        lblTitle.AutoSize = true;
        lblTitle.Font = BarberShopCRM.Helpers.ThemeHelper.HeaderFont;
        lblTitle.ForeColor = BarberShopCRM.Helpers.ThemeHelper.WarmIvory;
        lblTitle.Location = new Point(20, 15);
        lblTitle.Text = "🏢 Branch Management";

        // pnlForm
        pnlForm.BackColor = Color.White;
        pnlForm.Dock = DockStyle.Left;
        pnlForm.Width = 350;
        pnlForm.Padding = new Padding(20);

        lblBranchName.Text = "Branch Name:";
        lblBranchName.Location = new Point(20, 20);
        lblBranchName.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblBranchName.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        txtBranchName.Location = new Point(20, 45);
        txtBranchName.Size = new Size(300, 28);
        txtBranchName.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        lblAddress.Text = "Address:";
        lblAddress.Location = new Point(20, 85);
        lblAddress.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblAddress.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        txtAddress.Location = new Point(20, 110);
        txtAddress.Size = new Size(300, 28);
        txtAddress.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        lblContactInfo.Text = "Contact Information:";
        lblContactInfo.Location = new Point(20, 150);
        lblContactInfo.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblContactInfo.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        txtContactInfo.Location = new Point(20, 175);
        txtContactInfo.Size = new Size(300, 28);
        txtContactInfo.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        chkIsActive.Text = "Active Branch";
        chkIsActive.Location = new Point(20, 220);
        chkIsActive.Checked = true;
        chkIsActive.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        btnAdd.Text = "Add Branch";
        btnAdd.Location = new Point(20, 260);
        btnAdd.Size = new Size(140, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplyPrimaryButton(btnAdd);
        btnAdd.Click += btnAdd_Click;

        btnUpdate.Text = "Update";
        btnUpdate.Location = new Point(180, 260);
        btnUpdate.Size = new Size(140, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplySecondaryButton(btnUpdate);
        btnUpdate.Click += btnUpdate_Click;

        btnDeactivate.Text = "Deactivate";
        btnDeactivate.Location = new Point(20, 310);
        btnDeactivate.Size = new Size(140, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplyDestructiveButton(btnDeactivate);
        btnDeactivate.Click += btnDeactivate_Click;

        btnClear.Text = "Clear";
        btnClear.Location = new Point(180, 310);
        btnClear.Size = new Size(140, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplySecondaryButton(btnClear);
        btnClear.Click += btnClear_Click;

        pnlForm.Controls.AddRange(new Control[] {
            lblBranchName, txtBranchName,
            lblAddress, txtAddress,
            lblContactInfo, txtContactInfo,
            chkIsActive, btnAdd, btnUpdate, btnDeactivate, btnClear
        });

        // dgvBranches
        dgvBranches.Dock = DockStyle.Fill;
        dgvBranches.SelectionChanged += dgvBranches_SelectionChanged;

        // Form
        BackColor = BarberShopCRM.Helpers.ThemeHelper.WarmIvory;
        ClientSize = new Size(950, 600);
        Controls.Add(dgvBranches);
        Controls.Add(pnlForm);
        Controls.Add(pnlHeader);

        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlForm.ResumeLayout(false);
        pnlForm.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvBranches).EndInit();
        ResumeLayout(false);
    }

    private Panel pnlHeader;
    private Label lblTitle;
    private Panel pnlForm;
    private Label lblBranchName;
    private TextBox txtBranchName;
    private Label lblAddress;
    private TextBox txtAddress;
    private Label lblContactInfo;
    private TextBox txtContactInfo;
    private CheckBox chkIsActive;
    private Button btnAdd;
    private Button btnUpdate;
    private Button btnDeactivate;
    private Button btnClear;
    private DataGridView dgvBranches;
}
