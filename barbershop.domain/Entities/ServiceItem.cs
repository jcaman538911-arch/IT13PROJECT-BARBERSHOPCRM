namespace barbershop.domain;

public class ServiceItem
{
    public int Id { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal BasePrice { get; set; } = 200.00m;
    public bool IsActive { get; set; } = true;
}
