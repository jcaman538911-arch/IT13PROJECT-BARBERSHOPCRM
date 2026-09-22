namespace barbershop.domain;

public class Appointment
{
    public int Id { get; set; }
    public string AppointmentNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public int BarberId { get; set; }
    public string BarberName { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; } = DateTime.Now;
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
