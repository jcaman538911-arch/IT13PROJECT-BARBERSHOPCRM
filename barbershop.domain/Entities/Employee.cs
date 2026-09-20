namespace barbershop.domain;

public class Employee
{
    public int Id { get; set; }
    private string _name = string.Empty;
    public string Name
    {
        get => string.IsNullOrWhiteSpace(_name) ? $"{FirstName} {LastName}".Trim() : _name;
        set => _name = value;
    }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public EmployeePosition Position { get; set; }
    public bool IsActive { get; set; } = true;
    public int? BranchId { get; set; }
}
