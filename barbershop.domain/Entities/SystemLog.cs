namespace barbershop.domain;

public class SystemLog
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string LogLevel { get; set; } = "INFO";
    public string Module { get; set; } = "System";
    public string Message { get; set; } = string.Empty;
    public string ActionBy { get; set; } = "System";
}
