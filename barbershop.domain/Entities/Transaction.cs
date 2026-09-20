namespace barbershop.domain;

public class Transaction
{
    public int Id { get; set; }
    public string TransactionNumber { get; set; } = string.Empty;
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; } = "Walk-in Customer";
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = "Haircut";
    public int BarberId { get; set; }
    public string BarberName { get; set; } = string.Empty;
    public int StaffId { get; set; }
    public string StaffName { get; set; } = string.Empty;
    public decimal Subtotal { get; set; } = 200.00m;
    public decimal DiscountAmount { get; set; } = 0.00m;
    public decimal FinalAmount { get; set; } = 200.00m;
    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
    public TransactionStatus Status { get; set; } = TransactionStatus.InService;
    public int? PromotionId { get; set; }
    public string? AppliedPromotionName { get; set; }
    public int? LoyaltyRewardId { get; set; }
    public string? AppliedRewardName { get; set; }
    public int PointsEarned { get; set; }
    public int PointsRedeemed { get; set; }
    public decimal AmountReceived { get; set; }
    public decimal ChangeAmount { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.Now;
}
