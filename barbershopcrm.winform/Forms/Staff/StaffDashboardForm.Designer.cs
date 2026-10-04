using System;
using System.Drawing;
using System.Windows.Forms;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.Staff;

partial class StaffDashboardForm
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
        this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
        this.pnlTop = new System.Windows.Forms.Panel();
        this.lblHeader = new System.Windows.Forms.Label();
        this.lblSubHeader = new System.Windows.Forms.Label();
        this.flpQuickActions = new System.Windows.Forms.FlowLayoutPanel();
        this.btnNewCustomer = new System.Windows.Forms.Button();
        this.btnNewService = new System.Windows.Forms.Button();
        this.btnApplyPromotion = new System.Windows.Forms.Button();
        this.btnRedeemLoyalty = new System.Windows.Forms.Button();
        this.btnEmployeeAttendance = new System.Windows.Forms.Button();
        this.tlpSummaryCards = new System.Windows.Forms.TableLayoutPanel();
        this.pnlCardCustomers = new System.Windows.Forms.Panel();
        this.lblCardCustomersValue = new System.Windows.Forms.Label();
        this.lblCardCustomersTitle = new System.Windows.Forms.Label();
        this.pnlCardAppointments = new System.Windows.Forms.Panel();
        this.lblCardAppointmentsValue = new System.Windows.Forms.Label();
        this.lblCardAppointmentsTitle = new System.Windows.Forms.Label();
        this.pnlCardWaiting = new System.Windows.Forms.Panel();
        this.lblCardWaitingValue = new System.Windows.Forms.Label();
        this.lblCardWaitingTitle = new System.Windows.Forms.Label();
        this.pnlCardCompleted = new System.Windows.Forms.Panel();
        this.lblCardCompletedValue = new System.Windows.Forms.Label();
        this.lblCardCompletedTitle = new System.Windows.Forms.Label();
        this.pnlCardSales = new System.Windows.Forms.Panel();
        this.lblCardSalesValue = new System.Windows.Forms.Label();
        this.lblCardSalesTitle = new System.Windows.Forms.Label();
        this.pnlCardBarbers = new System.Windows.Forms.Panel();
        this.lblCardBarbersValue = new System.Windows.Forms.Label();
        this.lblCardBarbersTitle = new System.Windows.Forms.Label();
        this.tlpMiddle = new System.Windows.Forms.TableLayoutPanel();
        this.pnlActivity = new System.Windows.Forms.Panel();
        this.dgvServiceActivity = new System.Windows.Forms.DataGridView();
        this.pnlEmptyState = new System.Windows.Forms.Panel();
        this.btnEmptyStartService = new System.Windows.Forms.Button();
        this.btnEmptyViewAppts = new System.Windows.Forms.Button();
        this.lblEmptyState = new System.Windows.Forms.Label();
        this.lblActivityTitle = new System.Windows.Forms.Label();
        this.pnlLiveOps = new System.Windows.Forms.Panel();
        this.lblLiveWaiting = new System.Windows.Forms.Label();
        this.lblLiveWaitingVal = new System.Windows.Forms.Label();
        this.lblLiveInService = new System.Windows.Forms.Label();
        this.lblLiveInServiceVal = new System.Windows.Forms.Label();
        this.lblLiveAppts = new System.Windows.Forms.Label();
        this.lblLiveApptsVal = new System.Windows.Forms.Label();
        this.lblLiveNextAppt = new System.Windows.Forms.Label();
        this.lblLiveNextApptVal = new System.Windows.Forms.Label();
        this.lblLiveBarbers = new System.Windows.Forms.Label();
        this.lblLiveBarbersVal = new System.Windows.Forms.Label();
        this.btnViewQueue = new System.Windows.Forms.Button();
        this.btnViewAppointments = new System.Windows.Forms.Button();
        this.lblLiveOpsTitle = new System.Windows.Forms.Label();
        this.tlpBottom = new System.Windows.Forms.TableLayoutPanel();
        this.pnlTrend = new System.Windows.Forms.Panel();
        this.pnlTrendChart = new System.Windows.Forms.Panel();
        this.lblTrendTitle = new System.Windows.Forms.Label();
        this.pnlAtAGlance = new System.Windows.Forms.Panel();
        this.lblGlance1 = new System.Windows.Forms.Label();
        this.lblGlance1Val = new System.Windows.Forms.Label();
        this.lblGlance2 = new System.Windows.Forms.Label();
        this.lblGlance2Val = new System.Windows.Forms.Label();
        this.lblGlance3 = new System.Windows.Forms.Label();
        this.lblGlance3Val = new System.Windows.Forms.Label();
        this.lblAtAGlanceTitle = new System.Windows.Forms.Label();

        this.tlpMain.SuspendLayout();
        this.pnlTop.SuspendLayout();
        this.flpQuickActions.SuspendLayout();
        this.tlpSummaryCards.SuspendLayout();
        this.pnlCardCustomers.SuspendLayout();
        this.pnlCardAppointments.SuspendLayout();
        this.pnlCardWaiting.SuspendLayout();
        this.pnlCardCompleted.SuspendLayout();
        this.pnlCardSales.SuspendLayout();
        this.pnlCardBarbers.SuspendLayout();
        this.tlpMiddle.SuspendLayout();
        this.pnlActivity.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvServiceActivity)).BeginInit();
        this.pnlEmptyState.SuspendLayout();
        this.pnlLiveOps.SuspendLayout();
        this.tlpBottom.SuspendLayout();
        this.pnlTrend.SuspendLayout();
        this.pnlAtAGlance.SuspendLayout();
        this.SuspendLayout();

        // 
        // tlpMain
        // 
        this.tlpMain.ColumnCount = 1;
        this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tlpMain.Controls.Add(this.pnlTop, 0, 0);
        this.tlpMain.Controls.Add(this.tlpSummaryCards, 0, 1);
        this.tlpMain.Controls.Add(this.tlpMiddle, 0, 2);
        this.tlpMain.Controls.Add(this.tlpBottom, 0, 3);
        this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tlpMain.Location = new System.Drawing.Point(0, 0);
        this.tlpMain.Name = "tlpMain";
        this.tlpMain.RowCount = 4;
        this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
        this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
        this.tlpMain.Size = new System.Drawing.Size(1200, 800);
        this.tlpMain.TabIndex = 0;
        this.tlpMain.BackColor = ThemeHelper.WarmCanvas;

        // 
        // pnlTop
        // 
        this.pnlTop.AutoSize = true;
        this.pnlTop.Controls.Add(this.flpQuickActions);
        this.pnlTop.Controls.Add(this.lblHeader);
        this.pnlTop.Controls.Add(this.lblSubHeader);
        this.pnlTop.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlTop.Location = new System.Drawing.Point(10, 10);
        this.pnlTop.Margin = new System.Windows.Forms.Padding(10);
        this.pnlTop.Name = "pnlTop";
        this.pnlTop.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);

        // 
        // lblHeader
        // 
        this.lblHeader.AutoSize = true;
        this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.lblHeader.ForeColor = ThemeHelper.DeepCharcoal;
        this.lblHeader.Location = new System.Drawing.Point(0, 0);
        this.lblHeader.Name = "lblHeader";
        this.lblHeader.Text = "Staff / Cashier Dashboard";

        // 
        // lblSubHeader
        // 
        this.lblSubHeader.AutoSize = true;
        this.lblSubHeader.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.lblSubHeader.ForeColor = ThemeHelper.DeepCharcoal;
        this.lblSubHeader.Location = new System.Drawing.Point(2, 32);
        this.lblSubHeader.Name = "lblSubHeader";
        this.lblSubHeader.Text = "Today's front-desk operations and service activity.";

        // 
        // flpQuickActions
        // 
        this.flpQuickActions.AutoSize = true;
        this.flpQuickActions.Controls.Add(this.btnNewCustomer);
        this.flpQuickActions.Controls.Add(this.btnNewService);
        this.flpQuickActions.Controls.Add(this.btnApplyPromotion);
        this.flpQuickActions.Controls.Add(this.btnRedeemLoyalty);
        this.flpQuickActions.Controls.Add(this.btnEmployeeAttendance);
        this.flpQuickActions.Location = new System.Drawing.Point(0, 65);
        this.flpQuickActions.Name = "flpQuickActions";
        this.flpQuickActions.WrapContents = true;

        // 
        // Quick Action Buttons
        // 
        this.btnNewCustomer.Text = "[ + New Customer ]";
        this.btnNewService.Text = "[ ✂ New Service ]";
        this.btnApplyPromotion.Text = "[ Apply Promotion ]";
        this.btnRedeemLoyalty.Text = "[ Redeem Loyalty ]";
        this.btnEmployeeAttendance.Text = "[ Employee Attendance ]";
        
        Button[] quickBtns = { this.btnNewCustomer, this.btnNewService, this.btnApplyPromotion, this.btnRedeemLoyalty, this.btnEmployeeAttendance };
        foreach (var btn in quickBtns)
        {
            btn.Size = new System.Drawing.Size(180, 40);
            btn.Margin = new System.Windows.Forms.Padding(0, 0, 10, 10);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = ThemeHelper.DeepCharcoal;
            btn.ForeColor = System.Drawing.Color.White;
            btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }
        this.btnNewService.BackColor = ThemeHelper.MutedGold; // Highlight primary action
        this.btnNewService.ForeColor = ThemeHelper.DeepCharcoal;

        // 
        // tlpSummaryCards
        // 
        this.tlpSummaryCards.ColumnCount = 6;
        for (int i=0; i<6; i++) 
            this.tlpSummaryCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66F));
        
        this.tlpSummaryCards.Controls.Add(this.pnlCardCustomers, 0, 0);
        this.tlpSummaryCards.Controls.Add(this.pnlCardAppointments, 1, 0);
        this.tlpSummaryCards.Controls.Add(this.pnlCardWaiting, 2, 0);
        this.tlpSummaryCards.Controls.Add(this.pnlCardCompleted, 3, 0);
        this.tlpSummaryCards.Controls.Add(this.pnlCardSales, 4, 0);
        this.tlpSummaryCards.Controls.Add(this.pnlCardBarbers, 5, 0);
        this.tlpSummaryCards.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tlpSummaryCards.Location = new System.Drawing.Point(10, 140);
        this.tlpSummaryCards.Margin = new System.Windows.Forms.Padding(10, 0, 10, 10);
        this.tlpSummaryCards.Name = "tlpSummaryCards";
        this.tlpSummaryCards.RowCount = 1;
        this.tlpSummaryCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tlpSummaryCards.AutoSize = true;

        // 
        // Configure Cards
        // 
        Panel[] cards = { this.pnlCardCustomers, this.pnlCardAppointments, this.pnlCardWaiting, this.pnlCardCompleted, this.pnlCardSales, this.pnlCardBarbers };
        Label[] cardTitles = { this.lblCardCustomersTitle, this.lblCardAppointmentsTitle, this.lblCardWaitingTitle, this.lblCardCompletedTitle, this.lblCardSalesTitle, this.lblCardBarbersTitle };
        Label[] cardValues = { this.lblCardCustomersValue, this.lblCardAppointmentsValue, this.lblCardWaitingValue, this.lblCardCompletedValue, this.lblCardSalesValue, this.lblCardBarbersValue };
        string[] titles = { "CUSTOMERS TODAY", "APPOINTMENTS TODAY", "WAITING IN QUEUE", "COMPLETED SERVICES", "TODAY'S SALES", "BARBERS AVAILABLE" };

        for (int i = 0; i < 6; i++)
        {
            cards[i].Dock = System.Windows.Forms.DockStyle.Fill;
            cards[i].BackColor = System.Drawing.Color.White;
            cards[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            cards[i].Margin = new System.Windows.Forms.Padding(5);
            cards[i].MinimumSize = new System.Drawing.Size(0, 80);
            cards[i].Cursor = System.Windows.Forms.Cursors.Hand;

            cardTitles[i].Text = titles[i];
            cardTitles[i].Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            cardTitles[i].ForeColor = System.Drawing.Color.DimGray;
            cardTitles[i].AutoSize = true;
            cardTitles[i].Location = new System.Drawing.Point(10, 10);
            cards[i].Controls.Add(cardTitles[i]);

            cardValues[i].Text = "-";
            cardValues[i].Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            cardValues[i].ForeColor = ThemeHelper.DeepCharcoal;
            cardValues[i].AutoSize = true;
            cardValues[i].Location = new System.Drawing.Point(10, 30);
            cards[i].Controls.Add(cardValues[i]);
        }

        // 
        // tlpMiddle
        // 
        this.tlpMiddle.ColumnCount = 2;
        this.tlpMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72F));
        this.tlpMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
        this.tlpMiddle.Controls.Add(this.pnlActivity, 0, 0);
        this.tlpMiddle.Controls.Add(this.pnlLiveOps, 1, 0);
        this.tlpMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tlpMiddle.Margin = new System.Windows.Forms.Padding(5);

        // 
        // pnlActivity
        // 
        this.pnlActivity.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlActivity.BackColor = System.Drawing.Color.White;
        this.pnlActivity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pnlActivity.Margin = new System.Windows.Forms.Padding(5);
        this.pnlActivity.Controls.Add(this.dgvServiceActivity);
        this.pnlActivity.Controls.Add(this.pnlEmptyState);
        this.pnlActivity.Controls.Add(this.lblActivityTitle);

        this.lblActivityTitle.Text = "TODAY'S SERVICE ACTIVITY";
        this.lblActivityTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblActivityTitle.BackColor = ThemeHelper.DeepCharcoal;
        this.lblActivityTitle.ForeColor = System.Drawing.Color.White;
        this.lblActivityTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblActivityTitle.Padding = new System.Windows.Forms.Padding(10);
        this.lblActivityTitle.AutoSize = false;
        this.lblActivityTitle.Height = 40;
        this.lblActivityTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.dgvServiceActivity.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvServiceActivity.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvServiceActivity.AllowUserToResizeRows = false;
        this.dgvServiceActivity.RowTemplate.Height = 35;
        this.dgvServiceActivity.BackgroundColor = System.Drawing.Color.White;
        this.dgvServiceActivity.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.dgvServiceActivity.ReadOnly = true;
        this.dgvServiceActivity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

        this.pnlEmptyState.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlEmptyState.BackColor = System.Drawing.Color.White;
        this.pnlEmptyState.Controls.Add(this.lblEmptyState);
        this.pnlEmptyState.Controls.Add(this.btnEmptyStartService);
        this.pnlEmptyState.Controls.Add(this.btnEmptyViewAppts);
        
        this.lblEmptyState.Text = "No service transactions today.\nCompleted and paid services will appear here.";
        this.lblEmptyState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lblEmptyState.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.lblEmptyState.ForeColor = System.Drawing.Color.DimGray;
        this.lblEmptyState.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblEmptyState.Height = 100;

        this.btnEmptyStartService.Text = "Start New Service";
        this.btnEmptyStartService.Size = new System.Drawing.Size(150, 35);
        this.btnEmptyStartService.Location = new System.Drawing.Point(100, 100);
        this.btnEmptyStartService.BackColor = ThemeHelper.DeepCharcoal;
        this.btnEmptyStartService.ForeColor = System.Drawing.Color.White;
        this.btnEmptyStartService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        
        this.btnEmptyViewAppts.Text = "View Appointments";
        this.btnEmptyViewAppts.Size = new System.Drawing.Size(150, 35);
        this.btnEmptyViewAppts.Location = new System.Drawing.Point(260, 100);
        this.btnEmptyViewAppts.BackColor = System.Drawing.Color.White;
        this.btnEmptyViewAppts.ForeColor = ThemeHelper.DeepCharcoal;
        this.btnEmptyViewAppts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

        // 
        // pnlLiveOps
        // 
        this.pnlLiveOps.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlLiveOps.BackColor = System.Drawing.Color.White;
        this.pnlLiveOps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pnlLiveOps.Margin = new System.Windows.Forms.Padding(5);
        
        this.lblLiveOpsTitle.Text = "LIVE OPERATIONS";
        this.lblLiveOpsTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblLiveOpsTitle.BackColor = ThemeHelper.DeepCharcoal;
        this.lblLiveOpsTitle.ForeColor = System.Drawing.Color.White;
        this.lblLiveOpsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblLiveOpsTitle.Padding = new System.Windows.Forms.Padding(10);
        this.lblLiveOpsTitle.AutoSize = false;
        this.lblLiveOpsTitle.Height = 40;
        this.lblLiveOpsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.pnlLiveOps.Controls.Add(this.lblLiveOpsTitle);
        int top = 60;
        string[] liveLabels = { "Waiting in Queue", "Currently In Service", "Appointments Today", "Next Appointment", "Barbers Present" };
        Label[] lblLive = { this.lblLiveWaiting, this.lblLiveInService, this.lblLiveAppts, this.lblLiveNextAppt, this.lblLiveBarbers };
        Label[] lblLiveVals = { this.lblLiveWaitingVal, this.lblLiveInServiceVal, this.lblLiveApptsVal, this.lblLiveNextApptVal, this.lblLiveBarbersVal };

        for(int i=0; i<5; i++)
        {
            lblLive[i].Text = liveLabels[i];
            lblLive[i].Location = new System.Drawing.Point(20, top);
            lblLive[i].Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblLive[i].AutoSize = true;
            this.pnlLiveOps.Controls.Add(lblLive[i]);

            lblLiveVals[i].Text = "-";
            lblLiveVals[i].Location = new System.Drawing.Point(200, top);
            lblLiveVals[i].Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblLiveVals[i].AutoSize = true;
            lblLiveVals[i].Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.pnlLiveOps.Controls.Add(lblLiveVals[i]);
            top += 35;
        }

        this.btnViewQueue.Text = "View Queue";
        this.btnViewQueue.Size = new System.Drawing.Size(120, 35);
        this.btnViewQueue.Location = new System.Drawing.Point(20, top + 10);
        this.btnViewQueue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.pnlLiveOps.Controls.Add(this.btnViewQueue);

        this.btnViewAppointments.Text = "View Appointments";
        this.btnViewAppointments.Size = new System.Drawing.Size(140, 35);
        this.btnViewAppointments.Location = new System.Drawing.Point(150, top + 10);
        this.btnViewAppointments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.pnlLiveOps.Controls.Add(this.btnViewAppointments);

        // 
        // tlpBottom
        // 
        this.tlpBottom.ColumnCount = 2;
        this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
        this.tlpBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
        this.tlpBottom.Controls.Add(this.pnlTrend, 0, 0);
        this.tlpBottom.Controls.Add(this.pnlAtAGlance, 1, 0);
        this.tlpBottom.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tlpBottom.Margin = new System.Windows.Forms.Padding(5);

        // 
        // pnlTrend
        // 
        this.pnlTrend.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlTrend.BackColor = System.Drawing.Color.White;
        this.pnlTrend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pnlTrend.Margin = new System.Windows.Forms.Padding(5);
        this.pnlTrend.Controls.Add(this.pnlTrendChart);
        this.pnlTrend.Controls.Add(this.lblTrendTitle);

        this.lblTrendTitle.Text = "TODAY'S SERVICE TREND (Hourly Completed)";
        this.lblTrendTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTrendTitle.BackColor = ThemeHelper.DeepCharcoal;
        this.lblTrendTitle.ForeColor = System.Drawing.Color.White;
        this.lblTrendTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblTrendTitle.Padding = new System.Windows.Forms.Padding(10);
        this.lblTrendTitle.AutoSize = false;
        this.lblTrendTitle.Height = 40;
        this.lblTrendTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.pnlTrendChart.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlTrendChart.BackColor = System.Drawing.Color.White;
        this.pnlTrendChart.Padding = new System.Windows.Forms.Padding(20);

        // 
        // pnlAtAGlance
        // 
        this.pnlAtAGlance.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlAtAGlance.BackColor = System.Drawing.Color.White;
        this.pnlAtAGlance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pnlAtAGlance.Margin = new System.Windows.Forms.Padding(5);
        
        this.lblAtAGlanceTitle.Text = "TODAY AT A GLANCE";
        this.lblAtAGlanceTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblAtAGlanceTitle.BackColor = ThemeHelper.DeepCharcoal;
        this.lblAtAGlanceTitle.ForeColor = System.Drawing.Color.White;
        this.lblAtAGlanceTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.lblAtAGlanceTitle.Padding = new System.Windows.Forms.Padding(10);
        this.lblAtAGlanceTitle.AutoSize = false;
        this.lblAtAGlanceTitle.Height = 40;
        this.lblAtAGlanceTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

        this.pnlAtAGlance.Controls.Add(this.lblAtAGlanceTitle);
        
        int topGlance = 60;
        Label[] lblGs = { this.lblGlance1, this.lblGlance2, this.lblGlance3 };
        Label[] lblGVals = { this.lblGlance1Val, this.lblGlance2Val, this.lblGlance3Val };
        string[] gTexts = { "Promotions Used:", "Loyalty Redeemed:", "Total Transactions:" };

        for(int i=0; i<3; i++)
        {
            lblGs[i].Text = gTexts[i];
            lblGs[i].Location = new System.Drawing.Point(20, topGlance);
            lblGs[i].Font = new System.Drawing.Font("Segoe UI", 9.5F);
            lblGs[i].AutoSize = true;
            this.pnlAtAGlance.Controls.Add(lblGs[i]);

            lblGVals[i].Text = "-";
            lblGVals[i].Location = new System.Drawing.Point(180, topGlance);
            lblGVals[i].Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            lblGVals[i].AutoSize = true;
            this.pnlAtAGlance.Controls.Add(lblGVals[i]);
            topGlance += 40;
        }

        this.Controls.Add(this.tlpMain);
        this.Name = "StaffDashboardForm";
        this.Text = "Staff Dashboard";
        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

        this.tlpMain.ResumeLayout(false);
        this.tlpMain.PerformLayout();
        this.pnlTop.ResumeLayout(false);
        this.pnlTop.PerformLayout();
        this.flpQuickActions.ResumeLayout(false);
        this.tlpSummaryCards.ResumeLayout(false);
        this.pnlCardCustomers.ResumeLayout(false);
        this.pnlCardCustomers.PerformLayout();
        this.pnlCardAppointments.ResumeLayout(false);
        this.pnlCardAppointments.PerformLayout();
        this.pnlCardWaiting.ResumeLayout(false);
        this.pnlCardWaiting.PerformLayout();
        this.pnlCardCompleted.ResumeLayout(false);
        this.pnlCardCompleted.PerformLayout();
        this.pnlCardSales.ResumeLayout(false);
        this.pnlCardSales.PerformLayout();
        this.pnlCardBarbers.ResumeLayout(false);
        this.pnlCardBarbers.PerformLayout();
        this.tlpMiddle.ResumeLayout(false);
        this.pnlActivity.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dgvServiceActivity)).EndInit();
        this.pnlEmptyState.ResumeLayout(false);
        this.pnlLiveOps.ResumeLayout(false);
        this.pnlLiveOps.PerformLayout();
        this.tlpBottom.ResumeLayout(false);
        this.pnlTrend.ResumeLayout(false);
        this.pnlAtAGlance.ResumeLayout(false);
        this.pnlAtAGlance.PerformLayout();
        this.ResumeLayout(false);
    }
    
    private System.Windows.Forms.TableLayoutPanel tlpMain;
    private System.Windows.Forms.Panel pnlTop;
    private System.Windows.Forms.Label lblHeader;
    private System.Windows.Forms.Label lblSubHeader;
    
    private System.Windows.Forms.FlowLayoutPanel flpQuickActions;
    private System.Windows.Forms.Button btnNewCustomer;
    private System.Windows.Forms.Button btnNewService;
    private System.Windows.Forms.Button btnApplyPromotion;
    private System.Windows.Forms.Button btnRedeemLoyalty;
    private System.Windows.Forms.Button btnEmployeeAttendance;

    private System.Windows.Forms.TableLayoutPanel tlpSummaryCards;
    private System.Windows.Forms.Panel pnlCardCustomers;
    private System.Windows.Forms.Label lblCardCustomersValue;
    private System.Windows.Forms.Label lblCardCustomersTitle;
    private System.Windows.Forms.Panel pnlCardAppointments;
    private System.Windows.Forms.Label lblCardAppointmentsValue;
    private System.Windows.Forms.Label lblCardAppointmentsTitle;
    private System.Windows.Forms.Panel pnlCardWaiting;
    private System.Windows.Forms.Label lblCardWaitingValue;
    private System.Windows.Forms.Label lblCardWaitingTitle;
    private System.Windows.Forms.Panel pnlCardCompleted;
    private System.Windows.Forms.Label lblCardCompletedValue;
    private System.Windows.Forms.Label lblCardCompletedTitle;
    private System.Windows.Forms.Panel pnlCardSales;
    private System.Windows.Forms.Label lblCardSalesValue;
    private System.Windows.Forms.Label lblCardSalesTitle;
    private System.Windows.Forms.Panel pnlCardBarbers;
    private System.Windows.Forms.Label lblCardBarbersValue;
    private System.Windows.Forms.Label lblCardBarbersTitle;

    private System.Windows.Forms.TableLayoutPanel tlpMiddle;
    private System.Windows.Forms.Panel pnlActivity;
    private System.Windows.Forms.DataGridView dgvServiceActivity;
    private System.Windows.Forms.Panel pnlEmptyState;
    private System.Windows.Forms.Button btnEmptyStartService;
    private System.Windows.Forms.Button btnEmptyViewAppts;
    private System.Windows.Forms.Label lblEmptyState;
    private System.Windows.Forms.Label lblActivityTitle;
    
    private System.Windows.Forms.Panel pnlLiveOps;
    private System.Windows.Forms.Label lblLiveWaiting;
    private System.Windows.Forms.Label lblLiveWaitingVal;
    private System.Windows.Forms.Label lblLiveInService;
    private System.Windows.Forms.Label lblLiveInServiceVal;
    private System.Windows.Forms.Label lblLiveAppts;
    private System.Windows.Forms.Label lblLiveApptsVal;
    private System.Windows.Forms.Label lblLiveNextAppt;
    private System.Windows.Forms.Label lblLiveNextApptVal;
    private System.Windows.Forms.Label lblLiveBarbers;
    private System.Windows.Forms.Label lblLiveBarbersVal;
    private System.Windows.Forms.Button btnViewQueue;
    private System.Windows.Forms.Button btnViewAppointments;
    private System.Windows.Forms.Label lblLiveOpsTitle;

    private System.Windows.Forms.TableLayoutPanel tlpBottom;
    private System.Windows.Forms.Panel pnlTrend;
    private System.Windows.Forms.Panel pnlTrendChart;
    private System.Windows.Forms.Label lblTrendTitle;
    
    private System.Windows.Forms.Panel pnlAtAGlance;
    private System.Windows.Forms.Label lblGlance1;
    private System.Windows.Forms.Label lblGlance1Val;
    private System.Windows.Forms.Label lblGlance2;
    private System.Windows.Forms.Label lblGlance2Val;
    private System.Windows.Forms.Label lblGlance3;
    private System.Windows.Forms.Label lblGlance3Val;
    private System.Windows.Forms.Label lblAtAGlanceTitle;
}
