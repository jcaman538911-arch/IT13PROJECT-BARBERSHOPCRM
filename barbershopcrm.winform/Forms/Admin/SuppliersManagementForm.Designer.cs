namespace BarberShopCRM.Forms.Admin;

partial class SuppliersManagementForm
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
        lblSupplierName = new Label();
        txtSupplierName = new TextBox();
        lblContactInfo = new Label();
        txtContactInfo = new TextBox();
        chkIsActive = new CheckBox();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDeactivate = new Button();
        btnClear = new Button();
        dgvSuppliers = new DataGridView();

        pnlHeader.SuspendLayout();
        pnlForm.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSuppliers).BeginInit();
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
        lblTitle.Text = "🚚 Supplier Management";

        // pnlForm
        pnlForm.BackColor = Color.White;
        pnlForm.Dock = DockStyle.Left;
        pnlForm.Width = 350;
        pnlForm.Padding = new Padding(20);

        lblSupplierName.Text = "Supplier Name:";
        lblSupplierName.Location = new Point(20, 20);
        lblSupplierName.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblSupplierName.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        txtSupplierName.Location = new Point(20, 45);
        txtSupplierName.Size = new Size(300, 28);
        txtSupplierName.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        lblContactInfo.Text = "Contact Information:";
        lblContactInfo.Location = new Point(20, 85);
        lblContactInfo.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblContactInfo.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        txtContactInfo.Location = new Point(20, 110);
        txtContactInfo.Size = new Size(300, 28);
        txtContactInfo.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        chkIsActive.Text = "Active Supplier";
        chkIsActive.Location = new Point(20, 160);
        chkIsActive.Checked = true;
        chkIsActive.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        btnAdd.Text = "Add Supplier";
        btnAdd.Location = new Point(20, 200);
        btnAdd.Size = new Size(140, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplyPrimaryButton(btnAdd);
        btnAdd.Click += btnAdd_Click;

        btnUpdate.Text = "Update";
        btnUpdate.Location = new Point(180, 200);
        btnUpdate.Size = new Size(140, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplySecondaryButton(btnUpdate);
        btnUpdate.Click += btnUpdate_Click;

        btnDeactivate.Text = "Deactivate";
        btnDeactivate.Location = new Point(20, 250);
        btnDeactivate.Size = new Size(140, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplyDestructiveButton(btnDeactivate);
        btnDeactivate.Click += btnDeactivate_Click;

        btnClear.Text = "Clear";
        btnClear.Location = new Point(180, 250);
        btnClear.Size = new Size(140, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplySecondaryButton(btnClear);
        btnClear.Click += btnClear_Click;

        pnlForm.Controls.AddRange(new Control[] {
            lblSupplierName, txtSupplierName,
            lblContactInfo, txtContactInfo,
            chkIsActive, btnAdd, btnUpdate, btnDeactivate, btnClear
        });

        // dgvSuppliers
        dgvSuppliers.Dock = DockStyle.Fill;
        dgvSuppliers.SelectionChanged += dgvSuppliers_SelectionChanged;

        // Form
        BackColor = BarberShopCRM.Helpers.ThemeHelper.WarmIvory;
        ClientSize = new Size(950, 600);
        Controls.Add(dgvSuppliers);
        Controls.Add(pnlForm);
        Controls.Add(pnlHeader);

        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlForm.ResumeLayout(false);
        pnlForm.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvSuppliers).EndInit();
        ResumeLayout(false);
    }

    private Panel pnlHeader;
    private Label lblTitle;
    private Panel pnlForm;
    private Label lblSupplierName;
    private TextBox txtSupplierName;
    private Label lblContactInfo;
    private TextBox txtContactInfo;
    private CheckBox chkIsActive;
    private Button btnAdd;
    private Button btnUpdate;
    private Button btnDeactivate;
    private Button btnClear;
    private DataGridView dgvSuppliers;
}
