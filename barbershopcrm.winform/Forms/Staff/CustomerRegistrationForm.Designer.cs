using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class CustomerRegistrationForm
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
        lblFullName = new Label();
        txtFullName = new TextBox();
        lblPhone = new Label();
        txtPhone = new TextBox();
        lblEmail = new Label();
        txtEmail = new TextBox();
        lblBirthday = new Label();
        dtpBirthday = new DateTimePicker();
        chkLoyaltyMember = new CheckBox();
        btnSave = new Button();
        btnCancel = new Button();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
        lblHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblHeader.Location = new Point(20, 15);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(360, 30);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "Register Customer";
        lblHeader.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblFullName
        // 
        lblFullName.AutoSize = true;
        lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblFullName.ForeColor = ThemeHelper.TextPrimary;
        lblFullName.Location = new Point(30, 60);
        lblFullName.Name = "lblFullName";
        lblFullName.Size = new Size(71, 17);
        lblFullName.TabIndex = 1;
        lblFullName.Text = "Full Name";
        // 
        // txtFullName
        // 
        txtFullName.BackColor = Color.White;
        txtFullName.BorderStyle = BorderStyle.FixedSingle;
        txtFullName.Font = new Font("Segoe UI", 10F);
        txtFullName.ForeColor = ThemeHelper.TextPrimary;
        txtFullName.Location = new Point(30, 80);
        txtFullName.Name = "txtFullName";
        txtFullName.Size = new Size(340, 25);
        txtFullName.TabIndex = 2;
        // 
        // lblPhone
        // 
        lblPhone.AutoSize = true;
        lblPhone.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblPhone.ForeColor = ThemeHelper.TextPrimary;
        lblPhone.Location = new Point(30, 115);
        lblPhone.Name = "lblPhone";
        lblPhone.Size = new Size(101, 17);
        lblPhone.TabIndex = 3;
        lblPhone.Text = "Phone Number";
        // 
        // txtPhone
        // 
        txtPhone.BackColor = Color.White;
        txtPhone.BorderStyle = BorderStyle.FixedSingle;
        txtPhone.Font = new Font("Segoe UI", 10F);
        txtPhone.ForeColor = ThemeHelper.TextPrimary;
        txtPhone.Location = new Point(30, 135);
        txtPhone.Name = "txtPhone";
        txtPhone.Size = new Size(340, 25);
        txtPhone.TabIndex = 4;
        // 
        // lblEmail
        // 
        lblEmail.AutoSize = true;
        lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblEmail.ForeColor = ThemeHelper.TextPrimary;
        lblEmail.Location = new Point(30, 170);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(95, 17);
        lblEmail.TabIndex = 5;
        lblEmail.Text = "Email Address";
        // 
        // txtEmail
        // 
        txtEmail.BackColor = Color.White;
        txtEmail.BorderStyle = BorderStyle.FixedSingle;
        txtEmail.Font = new Font("Segoe UI", 10F);
        txtEmail.ForeColor = ThemeHelper.TextPrimary;
        txtEmail.Location = new Point(30, 190);
        txtEmail.Name = "txtEmail";
        txtEmail.Size = new Size(340, 25);
        txtEmail.TabIndex = 6;
        // 
        // lblBirthday
        // 
        lblBirthday.AutoSize = true;
        lblBirthday.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblBirthday.ForeColor = ThemeHelper.TextPrimary;
        lblBirthday.Location = new Point(30, 225);
        lblBirthday.Name = "lblBirthday";
        lblBirthday.Size = new Size(60, 17);
        lblBirthday.TabIndex = 7;
        lblBirthday.Text = "Birthday";
        // 
        // dtpBirthday
        // 
        dtpBirthday.Format = DateTimePickerFormat.Short;
        dtpBirthday.Location = new Point(30, 245);
        dtpBirthday.Name = "dtpBirthday";
        dtpBirthday.Size = new Size(340, 23);
        dtpBirthday.TabIndex = 8;
        // 
        // chkLoyaltyMember
        // 
        chkLoyaltyMember.AutoSize = true;
        chkLoyaltyMember.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        chkLoyaltyMember.ForeColor = ThemeHelper.PrimaryNavy;
        chkLoyaltyMember.Location = new Point(30, 280);
        chkLoyaltyMember.Name = "chkLoyaltyMember";
        chkLoyaltyMember.Size = new Size(183, 21);
        chkLoyaltyMember.TabIndex = 9;
        chkLoyaltyMember.Text = "Join Loyalty Rewards (+0)";
        chkLoyaltyMember.UseVisualStyleBackColor = true;
        // 
        // btnSave
        // 
        btnSave.BackColor = ThemeHelper.PrimaryNavy;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.ForeColor = Color.White;
        btnSave.Location = new Point(30, 320);
        btnSave.Name = "btnSave";
        btnSave.Size = new Size(160, 38);
        btnSave.TabIndex = 10;
        btnSave.Text = "✓ Save Customer";
        btnSave.UseVisualStyleBackColor = false;
        btnSave.Click += btnSave_Click;
        // 
        // btnCancel
        // 
        btnCancel.BackColor = ThemeHelper.CardHeaderBg;
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnCancel.ForeColor = ThemeHelper.TextPrimary;
        btnCancel.Location = new Point(210, 320);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new Size(160, 38);
        btnCancel.TabIndex = 11;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;
        btnCancel.Click += btnCancel_Click;
        // 
        // CustomerRegistrationForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.CardBackground;
        ClientSize = new Size(400, 380);
        Controls.Add(btnCancel);
        Controls.Add(btnSave);
        Controls.Add(chkLoyaltyMember);
        Controls.Add(dtpBirthday);
        Controls.Add(lblBirthday);
        Controls.Add(txtEmail);
        Controls.Add(lblEmail);
        Controls.Add(txtPhone);
        Controls.Add(lblPhone);
        Controls.Add(txtFullName);
        Controls.Add(lblFullName);
        Controls.Add(lblHeader);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "CustomerRegistrationForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Register Customer Modal";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblHeader;
    private Label lblFullName;
    private TextBox txtFullName;
    private Label lblPhone;
    private TextBox txtPhone;
    private Label lblEmail;
    private TextBox txtEmail;
    private Label lblBirthday;
    private DateTimePicker dtpBirthday;
    private CheckBox chkLoyaltyMember;
    private Button btnSave;
    private Button btnCancel;
}
