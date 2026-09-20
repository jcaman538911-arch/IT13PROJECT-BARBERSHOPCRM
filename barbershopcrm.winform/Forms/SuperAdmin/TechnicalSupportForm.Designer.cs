using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

partial class TechnicalSupportForm
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
        lblSubtitle = new Label();
        dgvSupportTickets = new DataGridView();
        btnResolveTicket = new Button();
        btnNewTicket = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvSupportTickets).BeginInit();
        SuspendLayout();
        // 
        // lblHeader
        // 
        lblHeader.AutoSize = true;
        lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblHeader.ForeColor = ThemeHelper.PrimaryNavy;
        lblHeader.Location = new Point(20, 20);
        lblHeader.Name = "lblHeader";
        lblHeader.Size = new Size(248, 25);
        lblHeader.TabIndex = 0;
        lblHeader.Text = "Technical Support Helpdesk";
        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 9.5F);
        lblSubtitle.ForeColor = ThemeHelper.TextSecondary;
        lblSubtitle.Location = new Point(20, 50);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(490, 17);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Super Admin assists Barbershop Owner / Staff with technical or system issues.";
        // 
        // dgvSupportTickets
        // 
        dgvSupportTickets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvSupportTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvSupportTickets.Location = new Point(20, 85);
        dgvSupportTickets.Name = "dgvSupportTickets";
        dgvSupportTickets.Size = new Size(980, 470);
        dgvSupportTickets.TabIndex = 2;
        // 
        // btnResolveTicket
        // 
        btnResolveTicket.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnResolveTicket.BackColor = ThemeHelper.SecondaryNavy;
        btnResolveTicket.FlatAppearance.BorderSize = 0;
        btnResolveTicket.FlatStyle = FlatStyle.Flat;
        btnResolveTicket.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnResolveTicket.ForeColor = Color.White;
        btnResolveTicket.Location = new Point(20, 570);
        btnResolveTicket.Name = "btnResolveTicket";
        btnResolveTicket.Size = new Size(220, 38);
        btnResolveTicket.TabIndex = 3;
        btnResolveTicket.Text = "✓ Mark Ticket Resolved";
        btnResolveTicket.UseVisualStyleBackColor = false;
        btnResolveTicket.Click += btnResolveTicket_Click;
        // 
        // btnNewTicket
        // 
        btnNewTicket.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnNewTicket.BackColor = ThemeHelper.PrimaryNavy;
        btnNewTicket.FlatAppearance.BorderSize = 0;
        btnNewTicket.FlatStyle = FlatStyle.Flat;
        btnNewTicket.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        btnNewTicket.ForeColor = Color.White;
        btnNewTicket.Location = new Point(255, 570);
        btnNewTicket.Name = "btnNewTicket";
        btnNewTicket.Size = new Size(220, 38);
        btnNewTicket.TabIndex = 4;
        btnNewTicket.Text = "➕ Log Technical Ticket";
        btnNewTicket.UseVisualStyleBackColor = false;
        btnNewTicket.Click += btnNewTicket_Click;
        // 
        // TechnicalSupportForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = ThemeHelper.WarmCanvas;
        ClientSize = new Size(1020, 640);
        Controls.Add(btnNewTicket);
        Controls.Add(btnResolveTicket);
        Controls.Add(dgvSupportTickets);
        Controls.Add(lblSubtitle);
        Controls.Add(lblHeader);
        FormBorderStyle = FormBorderStyle.None;
        Name = "TechnicalSupportForm";
        Text = "Technical Support Desk";
        ((System.ComponentModel.ISupportInitialize)dgvSupportTickets).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label lblHeader;
    private Label lblSubtitle;
    private DataGridView dgvSupportTickets;
    private Button btnResolveTicket;
    private Button btnNewTicket;
}
