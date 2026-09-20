namespace barbershop.domain;

public class LoyaltyReward
{
    public int Id { get; set; }
    public string RewardName { get; set; } = string.Empty;
    public int PointsRequired { get; set; }
    public decimal DiscountAmount { get; set; }
    public bool IsActive { get; set; } = true;
}
