namespace barbershop.domain;

public class Customer
{
    public int Id { get; set; }
    private string _fullName = string.Empty;
    public string FullName
    {
        get => string.IsNullOrWhiteSpace(_fullName) ? $"{FirstName} {LastName}".Trim() : _fullName;
        set => _fullName = value;
    }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime? Birthday { get; set; }
    public bool IsLoyaltyMember { get; set; }
    public int LoyaltyPoints { get; set; }
    public string Status { get; set; } = "ACTIVE";
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
