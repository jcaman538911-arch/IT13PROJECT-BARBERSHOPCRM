using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Admin;

partial class BarberAttendanceManagementForm
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
        pnlHeader = new Panel();
        lblFilterStatus = new Label();
        cmbStatusFilter = new ComboBox();
        lblDateFilter = new Label();
        dtpDateFilter = new DateTimePicker();
        btnFilter = new Button();
        btnResetFilter = new Button();
        dgvAttendance = new DataGridView();
        pnlHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvAttendance).BeginInit();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlHeader.BackColor = ThemeHelper.CardBackground;
        pnlHeader.BorderStyle = BorderStyle.FixedSingle;
        pnlHeader.Controls.Add(lblFilterStatus);
        pnlHeader.Controls.Add(cmbStatusFilter);
        pnlHeader.Controls.Add(lblDateFilter);
        pnlHeader.Controls.Add(dtpDateFilter);
        pnlHeader.Controls.Add(btnFilter);
        pnlHeader.Controls.Add(btnResetFilter);
        pnlHeader.Location = new Point(20, 20);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(980, 60);
        pnlHeader.TabIndex = 0;
        // 
        // lblFilterStatus
        // 
        lblFilterStatus.AutoSize = true;
        lblFilterStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblFilterStatus.ForeColor = ThemeHelper.TextPrimary;
        lblFilterStatus.Location = new Point(15, 20);
        lblFilterStatus.Name = "lblFilterStatus";
        lblFilterStatus.Size = new Size(49, 17);
        lblFilterStatus.TabIndex = 0;
        lblFilterStatus.Text = "Status:";
        // 
        // cmbStatusFilter
        // 
        cmbStatusFilter.BackColor = Color.White;
        cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStatusFilter.Font = new Font("Segoe UI", 9.5F);
        cmbStatusFilter.ForeColor = ThemeHelper.TextPrimary;
        cmbStatusFilter.FormattingEnabled = true;
        cmbStatusFilter.Items.AddRange(new object[] { "All Statuses", "Present", "Absent", "Late", "Leave" });
        cmbStatusFilter.Location = new Point(70, 17);
        cmbStatusFilter.Name = "cmbStatusFilter";
        cmbStatusFilter.Size = new Size(130, 23);
        cmbStatusFilter.TabIndex = 1;
        // 
        // lblDateFilter
        // 
        lblDateFilter.AutoSize = true;
        lblDateFilter.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDateFilter.ForeColor = ThemeHelper.TextPrimary;
        lblDateFilter.Location = new Point(220, 20);
        lblDateFilter.Name = "lblDateFilter";
        lblDateFilter.Size = new Size(41, 17);
        lblDateFilter.TabIndex = 2;
        lblDateFilter.Text = "Date:";
        // 
        // dtpDateFilter
        // 
        dtpDateFilter.Format = DateTimePickerFormat.Short;
        dtpDateFilter.Location = new Point(265, 17);
        dtpDateFilter.Name = "dtpDateFilter";
        dtpDateFilter.Size = new Size(130, 23);
        dtpDateFilter.TabIndex = 3;
        // 
        // btnFilter
        // 
        btnFilter.BackColor = ThemeHelper.PrimaryNavy;
        btnFilter.FlatAppearance.BorderSize = 0;
        btnFilter.FlatStyle = FlatStyle.Flat;
        btnFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnFilter.ForeColor = Color.White;
        btnFilter.Location = new Point(415, 14);
        btnFilter.Name = "btnFilter";
        btnFilter.Size = new Size(100, 30);
        btnFilter.TabIndex = 4;
        btnFilter.Text = "🔍 Apply Filter";
        btnFilter.UseVisualStyleBackColor = false;
        btnFilter.Click += btnFilter_Click;
        // 
        // btnResetFilter
        // 
        btnResetFilter.BackColor = ThemeHelper.CardHeaderBg;
        btnResetFilter.FlatAppearance.BorderSize = 0;
        btnResetFilter.FlatStyle = FlatStyle.Flat;
        btnResetFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        btnResetFilter.ForeColor = ThemeHelper.TextPrimary;
        btnResetFilter.Location = new Point(525, 14);
        btnResetFilter.Name = "btnResetFilter";
        btnResetFilter.Size = new Size(100, 30);
        btnResetFilter.TabIndex = 5;
        btnResetFilter.Text = "🔄 Reset";
        btnResetFilter.UseVisualStyleBackColor = false;
        btnResetFilter.Click += btnResetFilter_Click;
        // 
        // dgvAttendance
        // 
        dgvAttendance.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvAttendance.Location = new Point(20, 90);
        dgvAttendance.Name = "dgvAttendance";
        dgvAttendance.Size = new Size(980, 530);
        dgvAttendance.TabIndex = 1;
        // 
        // BarberAttendanceManagementForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvAttendance);
        Controls.Add(pnlHeader);
        FormBorderStyle = FormBorderStyle.None;
        Name = "BarberAttendanceManagementForm";
        Text = "Barber Attendance Management";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvAttendance).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlHeader;
    private Label lblFilterStatus;
    private ComboBox cmbStatusFilter;
    private Label lblDateFilter;
    private DateTimePicker dtpDateFilter;
    private Button btnFilter;
    private Button btnResetFilter;
    private DataGridView dgvAttendance;
}
