using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

partial class SystemUpdatesForm
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
        pnlVersionInfo = new Panel();
        lblVersionTitle = new Label();
        lblVersion = new Label();
        lblFramework = new Label();
        btnCheckUpdates = new Button();
        txtPatchNotes = new TextBox();
        lblPatchHeader = new Label();
        pnlVersionInfo.SuspendLayout();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.AutoSize = true;
        lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblHeader.Location = new Point(20, 20);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(278, 25);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "System Updates & Patch Manager";
        // 
        // pnlVersionInfo
        // 
        pnlVersionInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlVersionInfo.BackColor = ThemeHelper.CardBackground;
        pnlVersionInfo.BorderStyle = BorderStyle.FixedSingle;
        pnlVersionInfo.Controls.Add(btnCheckUpdates);
        pnlVersionInfo.Controls.Add(lblFramework);
        pnlVersionInfo.Controls.Add(lblVersion);
        pnlVersionInfo.Controls.Add(lblVersionTitle);
        pnlVersionInfo.Location = new Point(20, 60);
        pnlVersionInfo.Name = "pnlVersionInfo";
        pnlVersionInfo.Size = new Size(980, 110);
        pnlVersionInfo.TabIndex = 1;
        // 
        // lblVersionTitle
        // 
        lblVersionTitle.AutoSize = true;
        lblVersionTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblVersionTitle.ForeColor = ThemeHelper.PrimaryNavy;
        lblVersionTitle.Location = new Point(20, 18);
        lblVersionTitle.Name = "lblVersionTitle";
        lblVersionTitle.Size = new Size(234, 21);
        lblVersionTitle.TabIndex = 0;
        lblVersionTitle.Text = "Barber Shop CRM Desktop v1.0";
        // 
        // lblVersion
        // 
        lblVersion.AutoSize = true;
        lblVersion.Font = new Font("Segoe UI", 9.5F);
        lblVersion.ForeColor = ThemeHelper.BarberRed;
        lblVersion.Location = new Point(20, 45);
        lblVersion.Name = "lblVersion";
        lblVersion.Size = new Size(219, 17);
        lblVersion.TabIndex = 1;
        lblVersion.Text = "Build Target: C# .NET 8 Windows Forms";
        // 
        // lblFramework
        // 
        lblFramework.AutoSize = true;
        lblFramework.Font = new Font("Segoe UI", 9F);
        lblFramework.ForeColor = ThemeHelper.TextSecondary;
        lblFramework.Location = new Point(20, 68);
        lblFramework.Name = "lblFramework";
        lblFramework.Size = new Size(330, 15);
        lblFramework.TabIndex = 2;
        lblFramework.Text = "Visual Studio 2022 WinForms Designer Compatible Architecture";
        // 
        // btnCheckUpdates
        // 
        btnCheckUpdates.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCheckUpdates.BackColor = ThemeHelper.PrimaryNavy;
        btnCheckUpdates.FlatAppearance.BorderSize = 0;
        btnCheckUpdates.FlatStyle = FlatStyle.Flat;
        btnCheckUpdates.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnCheckUpdates.ForeColor = Color.White;
        btnCheckUpdates.Location = new Point(780, 35);
        btnCheckUpdates.Name = "btnCheckUpdates";
        btnCheckUpdates.Size = new Size(180, 40);
        btnCheckUpdates.TabIndex = 3;
        btnCheckUpdates.Text = "🔄 Check for Updates";
        btnCheckUpdates.UseVisualStyleBackColor = false;
        btnCheckUpdates.Click += btnCheckUpdates_Click;
        // 
        // lblPatchHeader
        // 
        lblPatchHeader.AutoSize = true;
        lblPatchHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblPatchHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblPatchHeader.Location = new Point(20, 185);
        lblPatchHeader.Name = "lblPatchHeader";
        lblPatchHeader.Size = new Size(167, 20);
        lblPatchHeader.TabIndex = 2;
        lblPatchHeader.Text = "System Release Notes";
        // 
        // txtPatchNotes
        // 
        txtPatchNotes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtPatchNotes.BackColor = ThemeHelper.CardBackground;
        txtPatchNotes.BorderStyle = BorderStyle.FixedSingle;
        txtPatchNotes.Font = new Font("Segoe UI", 9.5F);
        txtPatchNotes.ForeColor = ThemeHelper.TextPrimary;
        txtPatchNotes.Location = new Point(20, 215);
        txtPatchNotes.Multiline = true;
        txtPatchNotes.Name = "txtPatchNotes";
        txtPatchNotes.ReadOnly = true;
        txtPatchNotes.ScrollBars = ScrollBars.Vertical;
        txtPatchNotes.Size = new Size(980, 395);
        txtPatchNotes.TabIndex = 3;
        txtPatchNotes.Text = "=== RELEASE NOTES v1.0.0 ===\r\n- Initial release of Barber Shop CRM Desktop Application.\r\n- C# .NET 8 WinForms native desktop architecture.\r\n- Complete Visual Studio 2022 Designer compatibility.\r\n- Role-based separation for Super Admin, Admin/Owner, and Staff/Cashier.\r\n- Native ₱200 base haircut pricing logic with walk-in customer support.\r\n- Promotions & Loyalty Rewards redemption engine.\r\n- Barber attendance tracking and daily transaction sales audit.\r\n- Business analytics and report generation.";
        // 
        // SystemUpdatesForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(txtPatchNotes);
        Controls.Add(lblPatchHeader);
        Controls.Add(pnlVersionInfo);
        Controls.Add(lblHeader);
        FormBorderStyle = FormBorderStyle.None;
        Name = "SystemUpdatesForm";
        Text = "System Updates";
        pnlVersionInfo.ResumeLayout(false);
        pnlVersionInfo.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblHeader;
    private Panel pnlVersionInfo;
    private Label lblVersionTitle;
    private Label lblVersion;
    private Label lblFramework;
    private Button btnCheckUpdates;
    private Label lblPatchHeader;
    private TextBox txtPatchNotes;
}
