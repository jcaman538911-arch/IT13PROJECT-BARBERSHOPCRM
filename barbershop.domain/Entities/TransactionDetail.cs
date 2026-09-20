namespace barbershop.domain;

public class LoyaltyTransaction
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int? TransactionId { get; set; }
    public int PointsEarned { get; set; }
    public int PointsRedeemed { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public string RecordedBy { get; set; } = string.Empty;
}

public class TransactionDetail
{
    public int Id { get; set; }
    public int TransactionId { get; set; }
    public int? ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}
