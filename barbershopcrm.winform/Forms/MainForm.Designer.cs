using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms;

partial class MainForm
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
        components = new System.ComponentModel.Container();
        pnlSidebar = new Panel();
        flpNavMenu = new FlowLayoutPanel();
        lblSidebarHeader = new Label();
        pnlHeader = new Panel();
        lblPageTitle = new Label();
        lblUserBadge = new Label();
        lblClock = new Label();
        pnlMainContent = new Panel();
        timerClock = new System.Windows.Forms.Timer(components);
        pnlSidebar.SuspendLayout();
        pnlHeader.SuspendLayout();
        SuspendLayout();
        // 
        // pnlSidebar
        // 
        pnlSidebar.BackColor = ThemeHelper.DeepCharcoal;
        pnlSidebar.Controls.Add(flpNavMenu);
        pnlSidebar.Controls.Add(lblSidebarHeader);
        pnlSidebar.Dock = DockStyle.Left;
        pnlSidebar.Location = new Point(0, 0);
        pnlSidebar.Margin = new Padding(3, 4, 3, 4);
        pnlSidebar.Name = "pnlSidebar";
        pnlSidebar.Size = new Size(274, 960);
        pnlSidebar.TabIndex = 0;
        // 
        // flpNavMenu
        // 
        flpNavMenu.AutoScroll = true;
        flpNavMenu.Dock = DockStyle.Fill;
        flpNavMenu.FlowDirection = FlowDirection.TopDown;
        flpNavMenu.Location = new Point(0, 110);
        flpNavMenu.Margin = new Padding(3, 4, 3, 4);
        flpNavMenu.Name = "flpNavMenu";
        flpNavMenu.Padding = new Padding(12, 15, 12, 15);
        flpNavMenu.Size = new Size(274, 850);
        flpNavMenu.TabIndex = 1;
        flpNavMenu.WrapContents = false;
        // 
        // lblSidebarHeader
        // 
        lblSidebarHeader.BackColor = ThemeHelper.DeepCharcoal;
        lblSidebarHeader.Dock = DockStyle.Top;
        lblSidebarHeader.Font = new Font("Georgia", 11F, FontStyle.Bold);
        lblSidebarHeader.ForeColor = ThemeHelper.WarmIvory;
        lblSidebarHeader.Location = new Point(0, 0);
        lblSidebarHeader.Name = "lblSidebarHeader";
        lblSidebarHeader.Size = new Size(274, 110);
        lblSidebarHeader.TabIndex = 0;
        lblSidebarHeader.Text = "✂ UPPERCUT\r\nBARBER SHOP\r\nEST. 2024";
        lblSidebarHeader.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = ThemeHelper.DeepCharcoal;
        pnlHeader.Controls.Add(lblPageTitle);
        pnlHeader.Controls.Add(lblUserBadge);
        pnlHeader.Controls.Add(lblClock);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(274, 0);
        pnlHeader.Margin = new Padding(3, 4, 3, 4);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(1189, 87);
        pnlHeader.TabIndex = 1;
        // 
        // lblPageTitle
        // 
        lblPageTitle.AutoSize = true;
        lblPageTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblPageTitle.ForeColor = ThemeHelper.WarmIvory;
        lblPageTitle.Location = new Point(23, 22);
        lblPageTitle.Name = "lblPageTitle";
        lblPageTitle.Size = new Size(230, 25);
        lblPageTitle.TabIndex = 0;
        lblPageTitle.Text = "Admin / Owner Dashboard";
        // 
        // lblUserBadge
        // 
        lblUserBadge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblUserBadge.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblUserBadge.ForeColor = ThemeHelper.WarmIvory;
        lblUserBadge.Location = new Point(680, 24);
        lblUserBadge.Name = "lblUserBadge";
        lblUserBadge.Size = new Size(300, 33);
        lblUserBadge.TabIndex = 1;
        lblUserBadge.Text = "👤 Barbershop Owner / Admin";
        lblUserBadge.TextAlign = ContentAlignment.MiddleRight;
        // 
        // lblClock
        // 
        lblClock.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblClock.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        lblClock.ForeColor = ThemeHelper.MutedGold;
        lblClock.Location = new Point(990, 24);
        lblClock.Name = "lblClock";
        lblClock.Size = new Size(180, 33);
        lblClock.TabIndex = 2;
        lblClock.Text = "📅 2026-08-27 Wednesday";
        lblClock.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // pnlMainContent
        // 
        pnlMainContent.BackColor = ThemeHelper.WarmIvory;
        pnlMainContent.Dock = DockStyle.Fill;
        pnlMainContent.Location = new Point(274, 87);
        pnlMainContent.Margin = new Padding(3, 4, 3, 4);
        pnlMainContent.Name = "pnlMainContent";
        pnlMainContent.Size = new Size(1189, 873);
        pnlMainContent.TabIndex = 2;
        // 
        // timerClock
        // 
        timerClock.Enabled = true;
        timerClock.Interval = 1000;
        timerClock.Tick += timerClock_Tick;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmIvory;
        ClientSize = new Size(1463, 960);
        Controls.Add(pnlMainContent);
        Controls.Add(pnlHeader);
        Controls.Add(pnlSidebar);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(1168, 784);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "UPPERCUT BARBER SHOP CRM";
        WindowState = FormWindowState.Maximized;
        pnlSidebar.ResumeLayout(false);
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlSidebar;
    private Label lblSidebarHeader;
    private FlowLayoutPanel flpNavMenu;
    private Panel pnlHeader;
    private Label lblPageTitle;
    private Label lblUserBadge;
    private Label lblClock;
    private Panel pnlMainContent;
    private System.Windows.Forms.Timer timerClock;
}
