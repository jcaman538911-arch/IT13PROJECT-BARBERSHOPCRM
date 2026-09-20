using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

partial class PromotionsForm
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
        lblTitle = new Label();
        txtTitle = new TextBox();
        lblDescription = new Label();
        txtDescription = new TextBox();
        lblDiscountType = new Label();
        cmbDiscountType = new ComboBox();
        lblDiscountValue = new Label();
        numDiscountValue = new NumericUpDown();
        lblEligibility = new Label();
        txtEligibility = new TextBox();
        lblValidity = new Label();
        dtpStartDate = new DateTimePicker();
        dtpEndDate = new DateTimePicker();
        chkActive = new CheckBox();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        btnClear = new Button();
        dgvPromotions = new DataGridView();
        pnlInputs.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numDiscountValue).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvPromotions).BeginInit();
        SuspendLayout();
        // 
        // pnlInputs
        // 
        pnlInputs.BackColor = ThemeHelper.CardBackground;
        pnlInputs.BorderStyle = BorderStyle.FixedSingle;
        pnlInputs.Controls.Add(lblTitle);
        pnlInputs.Controls.Add(txtTitle);
        pnlInputs.Controls.Add(lblDescription);
        pnlInputs.Controls.Add(txtDescription);
        pnlInputs.Controls.Add(lblDiscountType);
        pnlInputs.Controls.Add(cmbDiscountType);
        pnlInputs.Controls.Add(lblDiscountValue);
        pnlInputs.Controls.Add(numDiscountValue);
        pnlInputs.Controls.Add(lblEligibility);
        pnlInputs.Controls.Add(txtEligibility);
        pnlInputs.Controls.Add(lblValidity);
        pnlInputs.Controls.Add(dtpStartDate);
        pnlInputs.Controls.Add(dtpEndDate);
        pnlInputs.Controls.Add(chkActive);
        pnlInputs.Controls.Add(btnAdd);
        pnlInputs.Controls.Add(btnUpdate);
        pnlInputs.Controls.Add(btnDelete);
        pnlInputs.Controls.Add(btnClear);
        pnlInputs.Location = new Point(20, 20);
        pnlInputs.Name = "pnlInputs";
        pnlInputs.Size = new Size(330, 595);
        pnlInputs.TabIndex = 0;
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblTitle.ForeColor = ThemeHelper.TextPrimary;
        lblTitle.Location = new Point(15, 12);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(106, 17);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Promotion Title";
        // 
        // txtTitle
        // 
        txtTitle.BackColor = Color.White;
        txtTitle.BorderStyle = BorderStyle.FixedSingle;
        txtTitle.Font = new Font("Segoe UI", 9.5F);
        txtTitle.ForeColor = ThemeHelper.TextPrimary;
        txtTitle.Location = new Point(15, 30);
        txtTitle.Name = "txtTitle";
        txtTitle.Size = new Size(300, 24);
        txtTitle.TabIndex = 1;
        // 
        // lblDescription
        // 
        lblDescription.AutoSize = true;
        lblDescription.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDescription.ForeColor = ThemeHelper.TextPrimary;
        lblDescription.Location = new Point(15, 60);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new Size(79, 17);
        lblDescription.TabIndex = 2;
        lblDescription.Text = "Description";
        // 
        // txtDescription
        // 
        txtDescription.BackColor = Color.White;
        txtDescription.BorderStyle = BorderStyle.FixedSingle;
        txtDescription.Font = new Font("Segoe UI", 9.5F);
        txtDescription.ForeColor = ThemeHelper.TextPrimary;
        txtDescription.Location = new Point(15, 78);
        txtDescription.Name = "txtDescription";
        txtDescription.Size = new Size(300, 24);
        txtDescription.TabIndex = 3;
        // 
        // lblDiscountType
        // 
        lblDiscountType.AutoSize = true;
        lblDiscountType.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDiscountType.ForeColor = ThemeHelper.TextPrimary;
        lblDiscountType.Location = new Point(15, 108);
        lblDiscountType.Name = "lblDiscountType";
        lblDiscountType.Size = new Size(96, 17);
        lblDiscountType.TabIndex = 4;
        lblDiscountType.Text = "Discount Type";
        // 
        // cmbDiscountType
        // 
        cmbDiscountType.BackColor = Color.White;
        cmbDiscountType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbDiscountType.Font = new Font("Segoe UI", 9.5F);
        cmbDiscountType.ForeColor = ThemeHelper.TextPrimary;
        cmbDiscountType.FormattingEnabled = true;
        cmbDiscountType.Items.AddRange(new object[] { "Percentage", "FixedAmount" });
        cmbDiscountType.Location = new Point(15, 126);
        cmbDiscountType.Name = "cmbDiscountType";
        cmbDiscountType.Size = new Size(140, 23);
        cmbDiscountType.TabIndex = 5;
        // 
        // lblDiscountValue
        // 
        lblDiscountValue.AutoSize = true;
        lblDiscountValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDiscountValue.ForeColor = ThemeHelper.PrimaryNavy;
        lblDiscountValue.Location = new Point(170, 108);
        lblDiscountValue.Name = "lblDiscountValue";
        lblDiscountValue.Size = new Size(100, 17);
        lblDiscountValue.TabIndex = 6;
        lblDiscountValue.Text = "Discount Value";
        // 
        // numDiscountValue
        // 
        numDiscountValue.BackColor = Color.White;
        numDiscountValue.DecimalPlaces = 2;
        numDiscountValue.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        numDiscountValue.ForeColor = ThemeHelper.PrimaryNavy;
        numDiscountValue.Location = new Point(170, 126);
        numDiscountValue.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
        numDiscountValue.Name = "numDiscountValue";
        numDiscountValue.Size = new Size(145, 24);
        numDiscountValue.TabIndex = 7;
        // 
        // lblEligibility
        // 
        lblEligibility.AutoSize = true;
        lblEligibility.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblEligibility.ForeColor = ThemeHelper.TextPrimary;
        lblEligibility.Location = new Point(15, 156);
        lblEligibility.Name = "lblEligibility";
        lblEligibility.Size = new Size(107, 17);
        lblEligibility.TabIndex = 8;
        lblEligibility.Text = "Eligibility Code";
        // 
        // txtEligibility
        // 
        txtEligibility.BackColor = Color.White;
        txtEligibility.BorderStyle = BorderStyle.FixedSingle;
        txtEligibility.Font = new Font("Segoe UI", 9.5F);
        txtEligibility.ForeColor = ThemeHelper.TextPrimary;
        txtEligibility.Location = new Point(15, 174);
        txtEligibility.Name = "txtEligibility";
        txtEligibility.Size = new Size(300, 24);
        txtEligibility.TabIndex = 9;
        // 
        // lblValidity
        // 
        lblValidity.AutoSize = true;
        lblValidity.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblValidity.ForeColor = ThemeHelper.TextPrimary;
        lblValidity.Location = new Point(15, 204);
        lblValidity.Name = "lblValidity";
        lblValidity.Size = new Size(130, 17);
        lblValidity.TabIndex = 10;
        lblValidity.Text = "Validity Period";
        // 
        // dtpStartDate
        // 
        dtpStartDate.Format = DateTimePickerFormat.Short;
        dtpStartDate.Location = new Point(15, 224);
        dtpStartDate.Name = "dtpStartDate";
        dtpStartDate.Size = new Size(140, 23);
        dtpStartDate.TabIndex = 11;
        // 
        // dtpEndDate
        // 
        dtpEndDate.Format = DateTimePickerFormat.Short;
        dtpEndDate.Location = new Point(170, 224);
        dtpEndDate.Name = "dtpEndDate";
        dtpEndDate.Size = new Size(145, 23);
        dtpEndDate.TabIndex = 12;
        // 
        // chkActive
        // 
        chkActive.AutoSize = true;
        chkActive.Checked = true;
        chkActive.CheckState = CheckState.Checked;
        chkActive.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        chkActive.ForeColor = ThemeHelper.TextPrimary;
        chkActive.Location = new Point(15, 255);
        chkActive.Name = "chkActive";
        chkActive.Size = new Size(137, 21);
        chkActive.TabIndex = 13;
        chkActive.Text = "Enable Promotion";
        chkActive.UseVisualStyleBackColor = true;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = ThemeHelper.MutedGold;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnAdd.ForeColor = ThemeHelper.DeepCharcoal;
        btnAdd.Location = new Point(15, 290);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(300, 35);
        btnAdd.TabIndex = 14;
        btnAdd.Text = "➕ Create Promotion";
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
        btnUpdate.Location = new Point(15, 335);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(300, 35);
        btnUpdate.TabIndex = 15;
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
        btnDelete.Location = new Point(15, 380);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(300, 35);
        btnDelete.TabIndex = 16;
        btnDelete.Text = "🗑️ Delete Promotion";
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
        btnClear.Location = new Point(15, 425);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(300, 35);
        btnClear.TabIndex = 17;
        btnClear.Text = "🔄 Clear Form";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.Click += btnClear_Click;
        // 
        // dgvPromotions
        // 
        dgvPromotions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvPromotions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvPromotions.Location = new Point(370, 20);
        dgvPromotions.Name = "dgvPromotions";
        dgvPromotions.Size = new Size(630, 595);
        dgvPromotions.TabIndex = 1;
        dgvPromotions.SelectionChanged += dgvPromotions_SelectionChanged;
        // 
        // PromotionsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvPromotions);
        Controls.Add(pnlInputs);
        FormBorderStyle = FormBorderStyle.None;
        Name = "PromotionsForm";
        Text = "Promotions Management";
        pnlInputs.ResumeLayout(false);
        pnlInputs.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numDiscountValue).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvPromotions).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlInputs;
    private Label lblTitle;
    private TextBox txtTitle;
    private Label lblDescription;
    private TextBox txtDescription;
    private Label lblDiscountType;
    private ComboBox cmbDiscountType;
    private Label lblDiscountValue;
    private NumericUpDown numDiscountValue;
    private Label lblEligibility;
    private TextBox txtEligibility;
    private Label lblValidity;
    private DateTimePicker dtpStartDate;
    private DateTimePicker dtpEndDate;
    private CheckBox chkActive;
    private Button btnAdd;
    private Button btnUpdate;
    private Button btnDelete;
    private Button btnClear;
    private DataGridView dgvPromotions;
}
