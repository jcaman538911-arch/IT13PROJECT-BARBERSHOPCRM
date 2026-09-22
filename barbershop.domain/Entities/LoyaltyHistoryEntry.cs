namespace barbershop.domain;

public class LoyaltyHistoryEntry
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int? TransactionId { get; set; }
    public int PointsEarned { get; set; }
    public int PointsRedeemed { get; set; }
    public string ActivityType { get; set; } = "EARNED";
    public int PreviousBalance { get; set; }
    public int NewBalance { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public string RecordedBy { get; set; } = string.Empty;
}
