namespace barbershop.domain;

public class SupportRequest
{
    public int Id { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public string RequestedBy { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public string Priority { get; set; } = "Medium";
    public string Status { get; set; } = "Open"; // Open, In Progress, Resolved
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
