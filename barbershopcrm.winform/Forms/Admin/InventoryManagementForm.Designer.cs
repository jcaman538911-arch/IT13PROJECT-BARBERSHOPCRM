namespace BarberShopCRM.Forms.Admin;

partial class InventoryManagementForm
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
        lblItemName = new Label();
        txtItemName = new TextBox();
        lblCategory = new Label();
        txtCategory = new TextBox();
        lblQuantity = new Label();
        numQuantity = new NumericUpDown();
        lblUnit = new Label();
        txtUnit = new TextBox();
        lblMinStock = new Label();
        numMinStock = new NumericUpDown();
        lblSupplier = new Label();
        cmbSupplier = new ComboBox();
        lblCost = new Label();
        numCost = new NumericUpDown();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        btnClear = new Button();
        dgvInventory = new DataGridView();

        pnlHeader.SuspendLayout();
        pnlForm.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numMinStock).BeginInit();
        ((System.ComponentModel.ISupportInitialize)numCost).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvInventory).BeginInit();
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
        lblTitle.Text = "📦 Inventory Management & Oversight";

        // pnlForm
        pnlForm.BackColor = Color.White;
        pnlForm.Dock = DockStyle.Left;
        pnlForm.Width = 360;
        pnlForm.Padding = new Padding(15);

        lblItemName.Text = "Item Name:";
        lblItemName.Location = new Point(15, 15);
        lblItemName.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblItemName.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        txtItemName.Location = new Point(15, 38);
        txtItemName.Size = new Size(320, 26);
        txtItemName.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        lblCategory.Text = "Category:";
        lblCategory.Location = new Point(15, 70);
        lblCategory.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblCategory.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        txtCategory.Location = new Point(15, 93);
        txtCategory.Size = new Size(320, 26);
        txtCategory.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;
        txtCategory.Text = "General";

        lblQuantity.Text = "Current Quantity:";
        lblQuantity.Location = new Point(15, 125);
        lblQuantity.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblQuantity.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        numQuantity.Location = new Point(15, 148);
        numQuantity.Size = new Size(150, 26);
        numQuantity.Maximum = 100000;
        numQuantity.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        lblUnit.Text = "Unit (e.g. pcs, bottles):";
        lblUnit.Location = new Point(180, 125);
        lblUnit.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblUnit.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        txtUnit.Location = new Point(180, 148);
        txtUnit.Size = new Size(155, 26);
        txtUnit.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;
        txtUnit.Text = "pcs";

        lblMinStock.Text = "Min. Stock Threshold:";
        lblMinStock.Location = new Point(15, 180);
        lblMinStock.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblMinStock.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        numMinStock.Location = new Point(15, 203);
        numMinStock.Size = new Size(150, 26);
        numMinStock.Maximum = 1000;
        numMinStock.Value = 5;
        numMinStock.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        lblCost.Text = "Unit Cost (₱):";
        lblCost.Location = new Point(180, 180);
        lblCost.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblCost.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        numCost.Location = new Point(180, 203);
        numCost.Size = new Size(155, 26);
        numCost.Maximum = 100000;
        numCost.DecimalPlaces = 2;
        numCost.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        lblSupplier.Text = "Supplier:";
        lblSupplier.Location = new Point(15, 235);
        lblSupplier.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblSupplier.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        cmbSupplier.Location = new Point(15, 258);
        cmbSupplier.Size = new Size(320, 26);
        cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbSupplier.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        btnAdd.Text = "Add Item";
        btnAdd.Location = new Point(15, 305);
        btnAdd.Size = new Size(150, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplyPrimaryButton(btnAdd);
        btnAdd.Click += btnAdd_Click;

        btnUpdate.Text = "Update";
        btnUpdate.Location = new Point(180, 305);
        btnUpdate.Size = new Size(155, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplySecondaryButton(btnUpdate);
        btnUpdate.Click += btnUpdate_Click;

        btnDelete.Text = "Delete";
        btnDelete.Location = new Point(15, 355);
        btnDelete.Size = new Size(150, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplyDestructiveButton(btnDelete);
        btnDelete.Click += btnDelete_Click;

        btnClear.Text = "Clear";
        btnClear.Location = new Point(180, 355);
        btnClear.Size = new Size(155, 38);
        BarberShopCRM.Helpers.ThemeHelper.ApplySecondaryButton(btnClear);
        btnClear.Click += btnClear_Click;

        pnlForm.Controls.AddRange(new Control[] {
            lblItemName, txtItemName,
            lblCategory, txtCategory,
            lblQuantity, numQuantity,
            lblUnit, txtUnit,
            lblMinStock, numMinStock,
            lblCost, numCost,
            lblSupplier, cmbSupplier,
            btnAdd, btnUpdate, btnDelete, btnClear
        });

        // dgvInventory
        dgvInventory.Dock = DockStyle.Fill;
        dgvInventory.SelectionChanged += dgvInventory_SelectionChanged;

        // Form
        BackColor = BarberShopCRM.Helpers.ThemeHelper.WarmIvory;
        ClientSize = new Size(1000, 620);
        Controls.Add(dgvInventory);
        Controls.Add(pnlForm);
        Controls.Add(pnlHeader);

        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlForm.ResumeLayout(false);
        pnlForm.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
        ((System.ComponentModel.ISupportInitialize)numMinStock).EndInit();
        ((System.ComponentModel.ISupportInitialize)numCost).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvInventory).EndInit();
        ResumeLayout(false);
    }

    private Panel pnlHeader;
    private Label lblTitle;
    private Panel pnlForm;
    private Label lblItemName;
    private TextBox txtItemName;
    private Label lblCategory;
    private TextBox txtCategory;
    private Label lblQuantity;
    private NumericUpDown numQuantity;
    private Label lblUnit;
    private TextBox txtUnit;
    private Label lblMinStock;
    private NumericUpDown numMinStock;
    private Label lblSupplier;
    private ComboBox cmbSupplier;
    private Label lblCost;
    private NumericUpDown numCost;
    private Button btnAdd;
    private Button btnUpdate;
    private Button btnDelete;
    private Button btnClear;
    private DataGridView dgvInventory;
}
