namespace barbershop.domain;

public class User
{
    public int Id { get; set; }
    public int? TenantId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public string FullName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string AccountStatus => IsActive ? "ACTIVE" : "INACTIVE";
    public int? EmployeeId { get; set; }
    public int? BranchId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
