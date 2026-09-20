using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class PromotionSelectionForm
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
        lblHeader = new Label();
        dgvPromotions = new DataGridView();
        btnSelect = new Button();
        btnCancel = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvPromotions).BeginInit();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblHeader.Location = new Point(20, 15);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(540, 30);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "Select Active Promotion";
        lblHeader.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dgvPromotions
        // 
        dgvPromotions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvPromotions.Location = new Point(20, 55);
        dgvPromotions.Name = "dgvPromotions";
        dgvPromotions.Size = new Size(540, 270);
        dgvPromotions.TabIndex = 1;
        // 
        // btnSelect
        // 
        btnSelect.BackColor = ThemeHelper.PrimaryNavy;
        btnSelect.FlatAppearance.BorderSize = 0;
        btnSelect.FlatStyle = FlatStyle.Flat;
        btnSelect.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSelect.ForeColor = Color.White;
        btnSelect.Location = new Point(20, 340);
        btnSelect.Name = "btnSelect";
        btnSelect.Size = new Size(250, 38);
        btnSelect.TabIndex = 2;
        btnSelect.Text = "✓ Apply Selected Promo";
        btnSelect.UseVisualStyleBackColor = false;
        btnSelect.Click += btnSelect_Click;
        // 
        // btnCancel
        // 
        btnCancel.BackColor = ThemeHelper.CardHeaderBg;
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnCancel.ForeColor = ThemeHelper.TextPrimary;
        btnCancel.Location = new Point(310, 340);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(250, 38);
        btnCancel.TabIndex = 3;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += btnCancel_Click;
        // 
        // PromotionSelectionForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.CardBackground;
        ClientSize = new Size(580, 395);
        Controls.Add(btnCancel);
        Controls.Add(btnSelect);
        Controls.Add(dgvPromotions);
        Controls.Add(lblHeader);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "PromotionSelectionForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Select Promotion Modal";
        ((System.ComponentModel.ISupportInitialize)dgvPromotions).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Label lblHeader;
    private DataGridView dgvPromotions;
    private Button btnSelect;
    private Button btnCancel;
}
