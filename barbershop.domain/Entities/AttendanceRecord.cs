namespace barbershop.domain;

public class AttendanceRecord
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Today;
    public TimeSpan TimeIn { get; set; } = new TimeSpan(9, 0, 0);
    public TimeSpan? TimeOut { get; set; } = new TimeSpan(18, 0, 0);
    public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
    public string Notes { get; set; } = string.Empty;
}
