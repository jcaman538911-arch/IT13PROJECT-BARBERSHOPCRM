namespace barbershop.domain;

public static class TenantContext
{
    public static int? CurrentTenantId { get; private set; }
    public static UserRole? CurrentRole { get; private set; }
    public static string CurrentUsername { get; private set; } = string.Empty;
    public static string CurrentCompanyName { get; private set; } = string.Empty;

    public static void SetSession(User user)
    {
        CurrentTenantId = user.TenantId;
        CurrentRole = user.Role;
        CurrentUsername = user.Username;
        CurrentCompanyName = user.CompanyName;
    }

    public static void ClearSession()
    {
        CurrentTenantId = null;
        CurrentRole = null;
        CurrentUsername = string.Empty;
        CurrentCompanyName = string.Empty;
    }
}
