namespace BarberShopCRM.Forms.Staff;

partial class StockTransactionForm
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
        lblItem = new Label();
        cmbItem = new ComboBox();
        lblTxnType = new Label();
        cmbTxnType = new ComboBox();
        lblQuantity = new Label();
        numQuantity = new NumericUpDown();
        lblNotes = new Label();
        txtNotes = new TextBox();
        btnRecordTransaction = new Button();
        tabContainer = new TabControl();
        tabCurrentStock = new TabPage();
        dgvCurrentStock = new DataGridView();
        tabStockHistory = new TabPage();
        dgvStockHistory = new DataGridView();

        pnlHeader.SuspendLayout();
        pnlForm.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
        tabContainer.SuspendLayout();
        tabCurrentStock.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCurrentStock).BeginInit();
        tabStockHistory.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvStockHistory).BeginInit();
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
        lblTitle.Text = "📋 Daily Inventory Tasks & Supplies";

        // pnlForm
        pnlForm.BackColor = Color.White;
        pnlForm.Dock = DockStyle.Left;
        pnlForm.Width = 350;
        pnlForm.Padding = new Padding(20);

        lblItem.Text = "Select Inventory Item:";
        lblItem.Location = new Point(20, 20);
        lblItem.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblItem.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        cmbItem.Location = new Point(20, 45);
        cmbItem.Size = new Size(300, 28);
        cmbItem.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbItem.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        lblTxnType.Text = "Transaction Type:";
        lblTxnType.Location = new Point(20, 90);
        lblTxnType.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblTxnType.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        cmbTxnType.Location = new Point(20, 115);
        cmbTxnType.Size = new Size(300, 28);
        cmbTxnType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbTxnType.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        lblQuantity.Text = "Quantity:";
        lblQuantity.Location = new Point(20, 160);
        lblQuantity.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblQuantity.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        numQuantity.Location = new Point(20, 185);
        numQuantity.Size = new Size(300, 28);
        numQuantity.Minimum = 1;
        numQuantity.Maximum = 10000;
        numQuantity.Value = 1;
        numQuantity.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        lblNotes.Text = "Notes / Reason:";
        lblNotes.Location = new Point(20, 230);
        lblNotes.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;
        lblNotes.ForeColor = BarberShopCRM.Helpers.ThemeHelper.DeepCharcoal;

        txtNotes.Location = new Point(20, 255);
        txtNotes.Size = new Size(300, 60);
        txtNotes.Multiline = true;
        txtNotes.Font = BarberShopCRM.Helpers.ThemeHelper.BodyFont;

        btnRecordTransaction.Text = "Record Stock Movement";
        btnRecordTransaction.Location = new Point(20, 335);
        btnRecordTransaction.Size = new Size(300, 42);
        BarberShopCRM.Helpers.ThemeHelper.ApplyPrimaryButton(btnRecordTransaction);
        btnRecordTransaction.Click += btnRecordTransaction_Click;

        pnlForm.Controls.AddRange(new Control[] {
            lblItem, cmbItem,
            lblTxnType, cmbTxnType,
            lblQuantity, numQuantity,
            lblNotes, txtNotes,
            btnRecordTransaction
        });

        // tabContainer
        tabContainer.Dock = DockStyle.Fill;
        tabContainer.Controls.Add(tabCurrentStock);
        tabContainer.Controls.Add(tabStockHistory);
        tabContainer.Font = BarberShopCRM.Helpers.ThemeHelper.SubtitleFont;

        tabCurrentStock.Text = "Current Stock Levels";
        tabCurrentStock.Controls.Add(dgvCurrentStock);
        dgvCurrentStock.Dock = DockStyle.Fill;

        tabStockHistory.Text = "Stock Movement Logs";
        tabStockHistory.Controls.Add(dgvStockHistory);
        dgvStockHistory.Dock = DockStyle.Fill;

        // Form
        BackColor = BarberShopCRM.Helpers.ThemeHelper.WarmIvory;
        ClientSize = new Size(980, 600);
        Controls.Add(tabContainer);
        Controls.Add(pnlForm);
        Controls.Add(pnlHeader);

        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        pnlForm.ResumeLayout(false);
        pnlForm.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
        tabContainer.ResumeLayout(false);
        tabCurrentStock.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvCurrentStock).EndInit();
        tabStockHistory.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvStockHistory).EndInit();
        ResumeLayout(false);
    }

    private Panel pnlHeader;
    private Label lblTitle;
    private Panel pnlForm;
    private Label lblItem;
    private ComboBox cmbItem;
    private Label lblTxnType;
    private ComboBox cmbTxnType;
    private Label lblQuantity;
    private NumericUpDown numQuantity;
    private Label lblNotes;
    private TextBox txtNotes;
    private Button btnRecordTransaction;
    private TabControl tabContainer;
    private TabPage tabCurrentStock;
    private DataGridView dgvCurrentStock;
    private TabPage tabStockHistory;
    private DataGridView dgvStockHistory;
}
