using System;
using System.Linq;
using System.Windows.Forms;
using barbershop.domain;
using barbershop.infrastructure;
using BarberShopCRM.Helpers;

namespace BarberShopCRM.Forms.SuperAdmin;

public partial class TechnicalSupportForm : Form
{
    public TechnicalSupportForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        ThemeHelper.ApplyModernGrid(dgvSupportTickets);
        LoadTickets();
    }

    private void LoadTickets()
    {
        dgvSupportTickets.DataSource = SqlDataRepository.Instance.GetSupportRequests()
            .Select(r => new
            {
                r.Id,
                r.TicketNumber,
                r.RequestedBy,
                r.Subject,
                r.Details,
                r.Priority,
                r.Status,
                r.CreatedDate
            }).ToList();
    }

    private void btnResolveTicket_Click(object sender, EventArgs e)
    {
        if (dgvSupportTickets.CurrentRow != null && dgvSupportTickets.CurrentRow.DataBoundItem != null)
        {
            dynamic item = dgvSupportTickets.CurrentRow.DataBoundItem;
            int id = item.Id;

            var ticket = SqlDataRepository.Instance.GetSupportRequests().FirstOrDefault(r => r.Id == id);
            if (ticket != null)
            {
                ticket.Status = "Resolved";
                SqlDataRepository.Instance.AddSystemLog("INFO", "Support", $"Marked ticket {ticket.TicketNumber} as Resolved.", "superadmin");
                MessageBox.Show($"Ticket {ticket.TicketNumber} marked as Resolved.", "Ticket Resolved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTickets();
            }
        }
    }

    private void btnNewTicket_Click(object sender, EventArgs e)
    {
        var newReq = new SupportRequest
        {
            RequestedBy = "Barbershop Staff",
            Subject = "Receipt Printer Calibration",
            Details = "Cashier reported minor alignment adjustment required for thermal printer.",
            Priority = "Normal",
            Status = "Open"
        };
        SqlDataRepository.Instance.AddSupportRequest(newReq);
        SqlDataRepository.Instance.AddSystemLog("INFO", "Support", $"Logged support ticket {newReq.TicketNumber}", "superadmin");
        MessageBox.Show($"New support ticket {newReq.TicketNumber} logged.", "Ticket Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadTickets();
    }
}
