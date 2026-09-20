namespace barbershop.domain;

public class Supplier
{
    public int Id { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string ContactInformation { get; set; } = string.Empty;
    public string Status { get; set; } = "ACTIVE"; // ACTIVE, INACTIVE
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
