using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class RecordBarberAttendanceForm
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
        lblBarber = new Label();
        cmbBarber = new ComboBox();
        lblDate = new Label();
        dtpDate = new DateTimePicker();
        lblTimeIn = new Label();
        dtpTimeIn = new DateTimePicker();
        lblTimeOut = new Label();
        dtpTimeOut = new DateTimePicker();
        lblStatus = new Label();
        cmbStatus = new ComboBox();
        lblNotes = new Label();
        txtNotes = new TextBox();
        btnSaveAttendance = new Button();
        dgvTodayAttendance = new DataGridView();
        pnlInputs.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTodayAttendance).BeginInit();
        SuspendLayout();
        // 
        // pnlInputs
        // 
        pnlInputs.BackColor = ThemeHelper.CardBackground;
        pnlInputs.BorderStyle = BorderStyle.FixedSingle;
        pnlInputs.Controls.Add(lblBarber);
        pnlInputs.Controls.Add(cmbBarber);
        pnlInputs.Controls.Add(lblDate);
        pnlInputs.Controls.Add(dtpDate);
        pnlInputs.Controls.Add(lblTimeIn);
        pnlInputs.Controls.Add(dtpTimeIn);
        pnlInputs.Controls.Add(lblTimeOut);
        pnlInputs.Controls.Add(dtpTimeOut);
        pnlInputs.Controls.Add(lblStatus);
        pnlInputs.Controls.Add(cmbStatus);
        pnlInputs.Controls.Add(lblNotes);
        pnlInputs.Controls.Add(txtNotes);
        pnlInputs.Controls.Add(btnSaveAttendance);
        pnlInputs.Location = new Point(20, 20);
        pnlInputs.Name = "pnlInputs";
        pnlInputs.Size = new Size(320, 595);
        pnlInputs.TabIndex = 0;
        // 
        // lblBarber
        // 
        lblBarber.AutoSize = true;
        lblBarber.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblBarber.ForeColor = ThemeHelper.TextPrimary;
        lblBarber.Location = new Point(20, 20);
        lblBarber.Name = "lblBarber";
        lblBarber.Size = new Size(88, 17);
        lblBarber.TabIndex = 0;
        lblBarber.Text = "Select Barber";
        // 
        // cmbBarber
        // 
        cmbBarber.BackColor = Color.White;
        cmbBarber.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbBarber.Font = new Font("Segoe UI", 10F);
        cmbBarber.ForeColor = ThemeHelper.TextPrimary;
        cmbBarber.FormattingEnabled = true;
        cmbBarber.Location = new Point(20, 42);
        cmbBarber.Name = "cmbBarber";
        cmbBarber.Size = new Size(280, 25);
        cmbBarber.TabIndex = 1;
        // 
        // lblDate
        // 
        lblDate.AutoSize = true;
        lblDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblDate.ForeColor = ThemeHelper.TextPrimary;
        lblDate.Location = new Point(20, 80);
        lblDate.Name = "lblDate";
        lblDate.Size = new Size(114, 17);
        lblDate.TabIndex = 2;
        lblDate.Text = "Attendance Date";
        // 
        // dtpDate
        // 
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(20, 102);
        dtpDate.Name = "dtpDate";
        dtpDate.Size = new Size(280, 23);
        dtpDate.TabIndex = 3;
        // 
        // lblTimeIn
        // 
        lblTimeIn.AutoSize = true;
        lblTimeIn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblTimeIn.ForeColor = ThemeHelper.TextPrimary;
        lblTimeIn.Location = new Point(20, 140);
        lblTimeIn.Name = "lblTimeIn";
        lblTimeIn.Size = new Size(56, 17);
        lblTimeIn.TabIndex = 4;
        lblTimeIn.Text = "Time In";
        // 
        // dtpTimeIn
        // 
        dtpTimeIn.CustomFormat = "hh:mm tt";
        dtpTimeIn.Format = DateTimePickerFormat.Custom;
        dtpTimeIn.Location = new Point(20, 162);
        dtpTimeIn.Name = "dtpTimeIn";
        dtpTimeIn.ShowUpDown = true;
        dtpTimeIn.Size = new Size(280, 23);
        dtpTimeIn.TabIndex = 5;
        // 
        // lblTimeOut
        // 
        lblTimeOut.AutoSize = true;
        lblTimeOut.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblTimeOut.ForeColor = ThemeHelper.TextPrimary;
        lblTimeOut.Location = new Point(20, 200);
        lblTimeOut.Name = "lblTimeOut";
        lblTimeOut.Size = new Size(67, 17);
        lblTimeOut.TabIndex = 6;
        lblTimeOut.Text = "Time Out";
        // 
        // dtpTimeOut
        // 
        dtpTimeOut.CustomFormat = "hh:mm tt";
        dtpTimeOut.Format = DateTimePickerFormat.Custom;
        dtpTimeOut.Location = new Point(20, 222);
        dtpTimeOut.Name = "dtpTimeOut";
        dtpTimeOut.ShowUpDown = true;
        dtpTimeOut.Size = new Size(280, 23);
        dtpTimeOut.TabIndex = 7;
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblStatus.ForeColor = ThemeHelper.TextPrimary;
        lblStatus.Location = new Point(20, 260);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(122, 17);
        lblStatus.TabIndex = 8;
        lblStatus.Text = "Attendance Status";
        // 
        // cmbStatus
        // 
        cmbStatus.BackColor = Color.White;
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStatus.Font = new Font("Segoe UI", 10F);
        cmbStatus.ForeColor = ThemeHelper.TextPrimary;
        cmbStatus.FormattingEnabled = true;
        cmbStatus.Location = new Point(20, 282);
        cmbStatus.Name = "cmbStatus";
        cmbStatus.Size = new Size(280, 25);
        cmbStatus.TabIndex = 9;
        // 
        // lblNotes
        // 
        lblNotes.AutoSize = true;
        lblNotes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblNotes.ForeColor = ThemeHelper.TextPrimary;
        lblNotes.Location = new Point(20, 320);
        lblNotes.Name = "lblNotes";
        lblNotes.Size = new Size(95, 17);
        lblNotes.TabIndex = 10;
        lblNotes.Text = "Notes / Remarks";
        // 
        // txtNotes
        // 
        txtNotes.BackColor = Color.White;
        txtNotes.BorderStyle = BorderStyle.FixedSingle;
        txtNotes.Font = new Font("Segoe UI", 10F);
        txtNotes.ForeColor = ThemeHelper.TextPrimary;
        txtNotes.Location = new Point(20, 342);
        txtNotes.Multiline = true;
        txtNotes.Name = "txtNotes";
        txtNotes.Size = new Size(280, 60);
        txtNotes.TabIndex = 11;
        // 
        // btnSaveAttendance
        // 
        btnSaveAttendance.BackColor = ThemeHelper.PrimaryNavy;
        btnSaveAttendance.FlatAppearance.BorderSize = 0;
        btnSaveAttendance.FlatStyle = FlatStyle.Flat;
        btnSaveAttendance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSaveAttendance.ForeColor = Color.White;
        btnSaveAttendance.Location = new Point(20, 420);
        btnSaveAttendance.Name = "btnSaveAttendance";
        btnSaveAttendance.Size = new Size(280, 40);
        btnSaveAttendance.TabIndex = 12;
        btnSaveAttendance.Text = "💾 Record Attendance";
        btnSaveAttendance.UseVisualStyleBackColor = false;
        btnSaveAttendance.Click += btnSaveAttendance_Click;
        // 
        // dgvTodayAttendance
        // 
        dgvTodayAttendance.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvTodayAttendance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvTodayAttendance.Location = new Point(360, 20);
        dgvTodayAttendance.Name = "dgvTodayAttendance";
        dgvTodayAttendance.Size = new Size(640, 595);
        dgvTodayAttendance.TabIndex = 1;
        // 
        // RecordBarberAttendanceForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(dgvTodayAttendance);
        Controls.Add(pnlInputs);
        FormBorderStyle = FormBorderStyle.None;
        Name = "RecordBarberAttendanceForm";
        Text = "Record Barber Attendance";
        pnlInputs.ResumeLayout(false);
        pnlInputs.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTodayAttendance).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlInputs;
    private Label lblBarber;
    private ComboBox cmbBarber;
    private Label lblDate;
    private DateTimePicker dtpDate;
    private Label lblTimeIn;
    private DateTimePicker dtpTimeIn;
    private Label lblTimeOut;
    private DateTimePicker dtpTimeOut;
    private Label lblStatus;
    private ComboBox cmbStatus;
    private Label lblNotes;
    private TextBox txtNotes;
    private Button btnSaveAttendance;
    private DataGridView dgvTodayAttendance;
}
