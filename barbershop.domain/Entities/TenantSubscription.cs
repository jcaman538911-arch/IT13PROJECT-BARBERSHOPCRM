namespace barbershop.domain;

/// <summary>
/// Represents a tenant's subscription to the BarberShop CRM SaaS system.
/// Managed exclusively by SuperAdmin via the Master DB.
/// </summary>
public class TenantSubscription
{
    public int SubscriptionID { get; set; }
    public int TenantID { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>Plan tier: Basic, Standard, Premium</summary>
    public string PlanName { get; set; } = "Basic";

    /// <summary>Monthly fee in PHP</summary>
    public decimal MonthlyFee { get; set; } = 999.00m;

    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime ExpiryDate { get; set; } = DateTime.Today.AddMonths(1);

    /// <summary>Active, Expired, Suspended</summary>
    public string Status { get; set; } = "Active";

    /// <summary>Paid, Unpaid, Overdue</summary>
    public string PaymentStatus { get; set; } = "Paid";

    public string PaymentMethod { get; set; } = "GCash";
    public DateTime? LastPaidDate { get; set; }
    public string Notes { get; set; } = string.Empty;

    // Computed helpers
    public bool IsActive => Status == "Active" && ExpiryDate >= DateTime.Today;
    public int DaysUntilExpiry => (ExpiryDate - DateTime.Today).Days;
    public bool IsExpiringSoon => IsActive && DaysUntilExpiry <= 7;
}
