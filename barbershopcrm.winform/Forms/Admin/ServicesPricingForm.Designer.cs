using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

partial class ServicesPricingForm
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
        lblServiceName = new Label();
        txtServiceName = new TextBox();
        lblDescription = new Label();
        txtDescription = new TextBox();
        lblBasePrice = new Label();
        numBasePrice = new NumericUpDown();
        chkActive = new CheckBox();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        btnClear = new Button();
        dgvServices = new DataGridView();
        pnlInputs.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numBasePrice).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvServices).BeginInit();
        SuspendLayout();
        // 
        // pnlInputs
        // 
        pnlInputs.BackColor = ThemeHelper.CardBackground;
        pnlInputs.BorderStyle = BorderStyle.FixedSingle;
        pnlInputs.Controls.Add(lblServiceName);
        pnlInputs.Controls.Add(txtServiceName);
        pnlInputs.Controls.Add(lblDescription);
        pnlInputs.Controls.Add(txtDescription);
        pnlInputs.Controls.Add(lblBasePrice);
        pnlInputs.Controls.Add(numBasePrice);
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
        // lblServiceName
        // 
        lblServiceName.AutoSize = true;
        lblServiceName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblServiceName.ForeColor = ThemeHelper.TextPrimary;
        lblServiceName.Location = new Point(20, 20);
        lblServiceName.Name = "lblServiceName";
        lblServiceName.Size = new Size(93, 17);
        lblServiceName.TabIndex = 0;
        lblServiceName.Text = "Service Name";
        // 
        // txtServiceName
        // 
        txtServiceName.BackColor = Color.White;
        txtServiceName.BorderStyle = BorderStyle.FixedSingle;
        txtServiceName.Font = new Font("Segoe UI", 10F);
        txtServiceName.ForeColor = ThemeHelper.TextPrimary;
        txtServiceName.Location = new Point(20, 42);
        txtServiceName.Name = "txtServiceName";
        txtServiceName.Size = new Size(280, 25);
        txtServiceName.TabIndex = 1;
        // 
        // lblDescription
        // 
        lblDescription.AutoSize = true;
        lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDescription.ForeColor = ThemeHelper.TextPrimary;
        lblDescription.Location = new Point(20, 80);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new Size(79, 17);
        lblDescription.TabIndex = 2;
        lblDescription.Text = "Description";
        // 
        // txtDescription
        // 
        txtDescription.BackColor = Color.White;
        txtDescription.BorderStyle = BorderStyle.FixedSingle;
        txtDescription.Font = new Font("Segoe UI", 10F);
        txtDescription.ForeColor = ThemeHelper.TextPrimary;
        txtDescription.Location = new Point(20, 102);
        txtDescription.Multiline = true;
        txtDescription.Name = "txtDescription";
        txtDescription.Size = new Size(280, 60);
        txtDescription.TabIndex = 3;
        // 
        // lblBasePrice
        // 
        lblBasePrice.AutoSize = true;
        lblBasePrice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblBasePrice.ForeColor = ThemeHelper.PrimaryNavy;
        lblBasePrice.Location = new Point(20, 175);
        lblBasePrice.Name = "lblBasePrice";
        lblBasePrice.Size = new Size(106, 17);
        lblBasePrice.TabIndex = 4;
        lblBasePrice.Text = "Base Price (₱)";
        // 
        // numBasePrice
        // 
        numBasePrice.BackColor = Color.White;
        numBasePrice.DecimalPlaces = 2;
        numBasePrice.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        numBasePrice.ForeColor = ThemeHelper.PrimaryNavy;
        numBasePrice.Location = new Point(20, 197);
        numBasePrice.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        numBasePrice.Name = "numBasePrice";
        numBasePrice.Size = new Size(280, 27);
        numBasePrice.TabIndex = 5;
        numBasePrice.Value = new decimal(new int[] { 200, 0, 0, 0 });
        // 
        // chkActive
        // 
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        chkActive.ForeColor = ThemeHelper.TextPrimary;
        chkActive.Location = new Point(20, 240);
        chkActive.Name = "chkActive";
        chkActive.Size = new Size(113, 21);
        chkActive.TabIndex = 6;
        chkActive.Text = "Active Service";
        chkActive.UseVisualStyleBackColor = true;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = ThemeHelper.MutedGold;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnAdd.ForeColor = ThemeHelper.DeepCharcoal;
        btnAdd.Location = new Point(20, 285);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(280, 35);
        btnAdd.TabIndex = 7;
        btnAdd.Text = "➕ Add Service";
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
        btnUpdate.Location = new Point(20, 330);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(280, 35);
        btnUpdate.TabIndex = 8;
        btnUpdate.Text = "✏️ Change Price / Update";
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
        btnDelete.Location = new Point(20, 375);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(280, 35);
        btnDelete.TabIndex = 9;
        btnDelete.Text = "🗑️ Delete Service";
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
        btnClear.Location = new Point(20, 420);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(280, 35);
        btnClear.TabIndex = 10;
        btnClear.Text = "🔄 Clear Form";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.Click += btnClear_Click;
        // 
        // dgvServices
        // 
        dgvServices.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvServices.Location = new Point(360, 20);
        dgvServices.Name = "dgvServices";
        dgvServices.Size = new Size(640, 595);
        dgvServices.TabIndex = 1;
        dgvServices.SelectionChanged += dgvServices_SelectionChanged;
        // 
        // ServicesPricingForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvServices);
        Controls.Add(pnlInputs);
        FormBorderStyle = FormBorderStyle.None;
        Name = "ServicesPricingForm";
        Text = "Services & Base Pricing";
        pnlInputs.ResumeLayout(false);
        pnlInputs.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numBasePrice).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvServices).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlInputs;
    private Label lblServiceName;
    private TextBox txtServiceName;
    private Label lblDescription;
    private TextBox txtDescription;
    private Label lblBasePrice;
    private NumericUpDown numBasePrice;
    private CheckBox chkActive;
    private Button btnAdd;
    private Button btnUpdate;
    private Button btnDelete;
    private Button btnClear;
    private DataGridView dgvServices;
}
