namespace barbershop.domain;

public class Branch
{
    public int Id { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactInformation { get; set; } = string.Empty;
    public string Status { get; set; } = "ACTIVE"; // ACTIVE, INACTIVE
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class BranchUser
{
    public int Id { get; set; }
    public int BranchId { get; set; }
    public int UserId { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Status { get; set; } = "ACTIVE";
}
