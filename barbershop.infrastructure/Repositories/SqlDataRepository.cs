using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using barbershop.domain;

namespace barbershop.infrastructure;

public class SqlDataRepository : ISqlDataRepository
{
    private static readonly Lazy<SqlDataRepository> _instance = new(() => new SqlDataRepository());
    public static SqlDataRepository Instance => _instance.Value;

    private SqlDataRepository()
    {
        DbHelper.InitializeDatabase();
    }

    private static void EnsureNotSuperAdmin()
    {
        if (TenantContext.CurrentRole == UserRole.SuperAdmin)
        {
            throw new InvalidOperationException("ACCESS DENIED: Super Admin is strictly prohibited from accessing private company operational records.");
        }
    }

    // --- Authentication ---
    public User? Authenticate(string username, string password)
    {
        User? user = null;

        // Default demo/known multi-company accounts
        if (username.Equals("superadmin", StringComparison.OrdinalIgnoreCase) && password == "admin123")
        {
            user = new User
            {
                Id = 1,
                TenantId = null,
                CompanyName = "System Master",
                Username = "superadmin",
                Role = UserRole.SuperAdmin,
                FullName = "Super Administrator",
                IsActive = true
            };
        }
        else if (username.Equals("owner1", StringComparison.OrdinalIgnoreCase) && password == "owner123")
        {
            user = new User
            {
                Id = 101,
                TenantId = 1,
                CompanyName = "Company 1 (db68508)",
                Username = "owner1",
                Role = UserRole.Admin,
                FullName = "Owner Company 1",
                IsActive = true
            };
        }
        else if (username.Equals("owner2", StringComparison.OrdinalIgnoreCase) && password == "owner123")
        {
            user = new User
            {
                Id = 201,
                TenantId = 2,
                CompanyName = "Company 2 (db68525)",
                Username = "owner2",
                Role = UserRole.Admin,
                FullName = "Owner Company 2",
                IsActive = true
            };
        }
        else if (username.Equals("owner3", StringComparison.OrdinalIgnoreCase) && password == "owner123")
        {
            user = new User
            {
                Id = 301,
                TenantId = 3,
                CompanyName = "Company 3 (db68526)",
                Username = "owner3",
                Role = UserRole.Admin,
                FullName = "Owner Company 3",
                IsActive = true
            };
        }
        else if (username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "admin123")
        {
            user = new User
            {
                Id = 102,
                TenantId = 1,
                CompanyName = "Company 1 (db68508)",
                Username = "admin",
                Role = UserRole.Admin,
                FullName = "Admin Company 1",
                IsActive = true
            };
        }
        else if ((username.Equals("staff", StringComparison.OrdinalIgnoreCase) || username.Equals("staff1", StringComparison.OrdinalIgnoreCase)) && password == "staff123")
        {
            user = new User
            {
                Id = 103,
                TenantId = 1,
                CompanyName = "Company 1 (db68508)",
                Username = "staff1",
                Role = UserRole.Staff,
                FullName = "Staff Member Company 1",
                IsActive = true
            };
        }
        else if (username.Equals("staff2", StringComparison.OrdinalIgnoreCase) && password == "staff123")
        {
            user = new User
            {
                Id = 203,
                TenantId = 2,
                CompanyName = "Company 2 (db68525)",
                Username = "staff2",
                Role = UserRole.Staff,
                FullName = "Staff Member Company 2",
                IsActive = true
            };
        }
        else if (username.Equals("staff3", StringComparison.OrdinalIgnoreCase) && password == "staff123")
        {
            user = new User
            {
                Id = 303,
                TenantId = 3,
                CompanyName = "Company 3 (db68526)",
                Username = "staff3",
                Role = UserRole.Staff,
                FullName = "Staff Member Company 3",
                IsActive = true
            };
        }
        else
        {
            // Query company databases
            foreach (int tId in new[] { 1, 2, 3 })
            {
                try
                {
                    using var conn = TenantConnectionFactory.GetConnection(tId);
                    conn.Open();
                    string sql = @"SELECT UserID, Username, PasswordHash, Role, FullName, AccountStatus, EmployeeID, BranchID, CreatedAt 
                                   FROM Users 
                                   WHERE Username = @Username AND PasswordHash = @Password AND AccountStatus = 'ACTIVE'";
                    using var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Enum.TryParse<UserRole>(reader.GetString(reader.GetOrdinal("Role")), true, out var role);
                        user = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("UserID")),
                            TenantId = tId,
                            CompanyName = $"Company {tId}",
                            Username = reader.GetString(reader.GetOrdinal("Username")),
                            Password = reader.GetString(reader.GetOrdinal("PasswordHash")),
                            Role = role,
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                            IsActive = reader.GetString(reader.GetOrdinal("AccountStatus")).Equals("ACTIVE", StringComparison.OrdinalIgnoreCase),
                            EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeID")) ? null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                            BranchId = reader.IsDBNull(reader.GetOrdinal("BranchID")) ? null : reader.GetInt32(reader.GetOrdinal("BranchID")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                        };
                        break;
                    }
                }
                catch
                {
                    // Ignore and try next database
                }
            }
        }

        if (user != null)
        {
            TenantContext.SetSession(user);
        }

        return user;
    }

    // --- System Users ---
    public List<User> GetUsers()
    {
        var list = new List<User>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT UserID, Username, PasswordHash, Role, FullName, AccountStatus, EmployeeID, BranchID, CreatedAt FROM Users ORDER BY UserID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Enum.TryParse<UserRole>(reader.GetString(reader.GetOrdinal("Role")), true, out var role);
            list.Add(new User
            {
                Id = reader.GetInt32(reader.GetOrdinal("UserID")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Password = reader.GetString(reader.GetOrdinal("PasswordHash")),
                Role = role,
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                IsActive = reader.GetString(reader.GetOrdinal("AccountStatus")).Equals("ACTIVE", StringComparison.OrdinalIgnoreCase),
                EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeID")) ? null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                BranchId = reader.IsDBNull(reader.GetOrdinal("BranchID")) ? null : reader.GetInt32(reader.GetOrdinal("BranchID")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            });
        }
        return list;
    }

    public void AddUser(User user)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"INSERT INTO Users (Username, PasswordHash, Role, FullName, AccountStatus, EmployeeID, BranchID, CreatedAt)
                       VALUES (@Username, @Password, @Role, @FullName, @AccountStatus, @EmployeeID, @BranchID, GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Username", user.Username);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
        cmd.Parameters.AddWithValue("@FullName", user.FullName);
        cmd.Parameters.AddWithValue("@AccountStatus", user.IsActive ? "ACTIVE" : "INACTIVE");
        cmd.Parameters.AddWithValue("@EmployeeID", (object?)user.EmployeeId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@BranchID", (object?)user.BranchId ?? DBNull.Value);

        user.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateUser(User user)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"UPDATE Users 
                       SET Username = @Username, PasswordHash = @Password, Role = @Role, FullName = @FullName, 
                           AccountStatus = @AccountStatus, EmployeeID = @EmployeeID, BranchID = @BranchID, UpdatedAt = GETDATE()
                       WHERE UserID = @UserID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@UserID", user.Id);
        cmd.Parameters.AddWithValue("@Username", user.Username);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        cmd.Parameters.AddWithValue("@Role", user.Role.ToString());
        cmd.Parameters.AddWithValue("@FullName", user.FullName);
        cmd.Parameters.AddWithValue("@AccountStatus", user.IsActive ? "ACTIVE" : "INACTIVE");
        cmd.Parameters.AddWithValue("@EmployeeID", (object?)user.EmployeeId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@BranchID", (object?)user.BranchId ?? DBNull.Value);

        cmd.ExecuteNonQuery();
    }

    public void DeleteUser(int id)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "UPDATE Users SET AccountStatus = 'INACTIVE' WHERE UserID = @UserID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@UserID", id);
        cmd.ExecuteNonQuery();
    }

    // --- Employees ---
    public List<Employee> GetEmployees()
    {
        EnsureNotSuperAdmin();
        var list = new List<Employee>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT EmployeeID, FirstName, LastName, ContactNumber, EmployeeType, Status, BranchID FROM Employees ORDER BY EmployeeID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Enum.TryParse<EmployeePosition>(reader.GetString(reader.GetOrdinal("EmployeeType")), true, out var pos);
            string fName = reader.GetString(reader.GetOrdinal("FirstName"));
            string lName = reader.GetString(reader.GetOrdinal("LastName"));
            list.Add(new Employee
            {
                Id = reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                FirstName = fName,
                LastName = lName,
                Name = $"{fName} {lName}".Trim(),
                ContactNumber = reader.IsDBNull(reader.GetOrdinal("ContactNumber")) ? "" : reader.GetString(reader.GetOrdinal("ContactNumber")),
                Position = pos,
                IsActive = reader.GetString(reader.GetOrdinal("Status")).Equals("ACTIVE", StringComparison.OrdinalIgnoreCase),
                BranchId = reader.IsDBNull(reader.GetOrdinal("BranchID")) ? null : reader.GetInt32(reader.GetOrdinal("BranchID"))
            });
        }
        return list;
    }

    public List<Employee> GetBarbers()
    {
        return GetEmployees().FindAll(e => e.Position == EmployeePosition.Barber && e.IsActive);
    }

    public void AddEmployee(Employee emp)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string fName = string.IsNullOrWhiteSpace(emp.FirstName) ? emp.Name.Split(' ')[0] : emp.FirstName;
        string lName = string.IsNullOrWhiteSpace(emp.LastName) ? (emp.Name.Contains(" ") ? emp.Name.Substring(emp.Name.IndexOf(' ') + 1) : "") : emp.LastName;

        string sql = @"INSERT INTO Employees (FirstName, LastName, ContactNumber, EmployeeType, Status, BranchID, CreatedAt)
                       VALUES (@FirstName, @LastName, @ContactNumber, @EmployeeType, @Status, @BranchID, GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@FirstName", fName);
        cmd.Parameters.AddWithValue("@LastName", lName);
        cmd.Parameters.AddWithValue("@ContactNumber", emp.ContactNumber ?? "");
        cmd.Parameters.AddWithValue("@EmployeeType", emp.Position.ToString());
        cmd.Parameters.AddWithValue("@Status", emp.IsActive ? "ACTIVE" : "INACTIVE");
        cmd.Parameters.AddWithValue("@BranchID", (object?)emp.BranchId ?? 1);

        emp.Id = Convert.ToInt32(cmd.ExecuteScalar());

        // If Barber, maintain Barbers table record
        if (emp.Position == EmployeePosition.Barber)
        {
            string bSql = "IF NOT EXISTS (SELECT * FROM Barbers WHERE EmployeeID = @EmpID) INSERT INTO Barbers (EmployeeID, BranchID, Status) VALUES (@EmpID, @BranchID, 'ACTIVE')";
            using var bCmd = new SqlCommand(bSql, conn);
            bCmd.Parameters.AddWithValue("@EmpID", emp.Id);
            bCmd.Parameters.AddWithValue("@BranchID", (object?)emp.BranchId ?? 1);
            bCmd.ExecuteNonQuery();
        }
    }

    public void UpdateEmployee(Employee emp)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string fName = string.IsNullOrWhiteSpace(emp.FirstName) ? emp.Name.Split(' ')[0] : emp.FirstName;
        string lName = string.IsNullOrWhiteSpace(emp.LastName) ? (emp.Name.Contains(" ") ? emp.Name.Substring(emp.Name.IndexOf(' ') + 1) : "") : emp.LastName;

        string sql = @"UPDATE Employees 
                       SET FirstName = @FirstName, LastName = @LastName, ContactNumber = @ContactNumber, 
                           EmployeeType = @EmployeeType, Status = @Status, BranchID = @BranchID, UpdatedAt = GETDATE()
                       WHERE EmployeeID = @EmployeeID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@EmployeeID", emp.Id);
        cmd.Parameters.AddWithValue("@FirstName", fName);
        cmd.Parameters.AddWithValue("@LastName", lName);
        cmd.Parameters.AddWithValue("@ContactNumber", emp.ContactNumber ?? "");
        cmd.Parameters.AddWithValue("@EmployeeType", emp.Position.ToString());
        cmd.Parameters.AddWithValue("@Status", emp.IsActive ? "ACTIVE" : "INACTIVE");
        cmd.Parameters.AddWithValue("@BranchID", (object?)emp.BranchId ?? 1);

        cmd.ExecuteNonQuery();
    }

    public void DeleteEmployee(int id)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "UPDATE Employees SET Status = 'INACTIVE' WHERE EmployeeID = @EmployeeID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@EmployeeID", id);
        cmd.ExecuteNonQuery();
    }

    // --- Customers ---
    public List<Customer> GetCustomers()
    {
        EnsureNotSuperAdmin();
        var list = new List<Customer>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT CustomerID, FirstName, LastName, PhoneNumber, Email, Birthday, IsLoyaltyMember, LoyaltyPoints, Status, CreatedAt FROM Customers ORDER BY CustomerID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string fName = reader.GetString(reader.GetOrdinal("FirstName"));
            string lName = reader.GetString(reader.GetOrdinal("LastName"));
            list.Add(new Customer
            {
                Id = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                FirstName = fName,
                LastName = lName,
                FullName = $"{fName} {lName}".Trim(),
                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? "" : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                Birthday = reader.IsDBNull(reader.GetOrdinal("Birthday")) ? null : reader.GetDateTime(reader.GetOrdinal("Birthday")),
                IsLoyaltyMember = reader.GetBoolean(reader.GetOrdinal("IsLoyaltyMember")),
                LoyaltyPoints = reader.GetInt32(reader.GetOrdinal("LoyaltyPoints")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            });
        }
        return list;
    }

    public Customer? GetCustomerById(int id)
    {
        return GetCustomers().Find(c => c.Id == id);
    }

    public List<Customer> SearchCustomers(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return GetCustomers();
        query = query.ToLower();
        return GetCustomers().FindAll(c =>
            c.FullName.ToLower().Contains(query) ||
            c.PhoneNumber.Contains(query) ||
            c.Email.ToLower().Contains(query) ||
            c.Id.ToString() == query ||
            $"uc-{c.Id:d6}" == query);
    }

    public void AddCustomer(Customer customer)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string fName = string.IsNullOrWhiteSpace(customer.FirstName) ? customer.FullName.Split(' ')[0] : customer.FirstName;
        string lName = string.IsNullOrWhiteSpace(customer.LastName) ? (customer.FullName.Contains(" ") ? customer.FullName.Substring(customer.FullName.IndexOf(' ') + 1) : "") : customer.LastName;

        string sql = @"INSERT INTO Customers (FirstName, LastName, PhoneNumber, Email, Birthday, IsLoyaltyMember, LoyaltyPoints, Status, CreatedAt)
                       VALUES (@FirstName, @LastName, @PhoneNumber, @Email, @Birthday, @IsLoyaltyMember, @LoyaltyPoints, 'ACTIVE', GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@FirstName", fName);
        cmd.Parameters.AddWithValue("@LastName", lName);
        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber ?? "");
        cmd.Parameters.AddWithValue("@Email", customer.Email ?? "");
        cmd.Parameters.AddWithValue("@Birthday", (object?)customer.Birthday ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@IsLoyaltyMember", customer.IsLoyaltyMember);
        cmd.Parameters.AddWithValue("@LoyaltyPoints", customer.LoyaltyPoints);

        customer.Id = Convert.ToInt32(cmd.ExecuteScalar());

        if (customer.IsLoyaltyMember)
        {
            string lSql = "IF NOT EXISTS (SELECT * FROM LoyaltyMembers WHERE CustomerID = @CustID) INSERT INTO LoyaltyMembers (CustomerID, CurrentPoints, DateRegistered, Status) VALUES (@CustID, @Points, GETDATE(), 'ACTIVE')";
            using var lCmd = new SqlCommand(lSql, conn);
            lCmd.Parameters.AddWithValue("@CustID", customer.Id);
            lCmd.Parameters.AddWithValue("@Points", customer.LoyaltyPoints);
            lCmd.ExecuteNonQuery();
        }
    }

    public void UpdateCustomer(Customer customer)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string fName = string.IsNullOrWhiteSpace(customer.FirstName) ? customer.FullName.Split(' ')[0] : customer.FirstName;
        string lName = string.IsNullOrWhiteSpace(customer.LastName) ? (customer.FullName.Contains(" ") ? customer.FullName.Substring(customer.FullName.IndexOf(' ') + 1) : "") : customer.LastName;

        string sql = @"UPDATE Customers 
                       SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, 
                           Email = @Email, Birthday = @Birthday, IsLoyaltyMember = @IsLoyaltyMember, 
                           LoyaltyPoints = @LoyaltyPoints, UpdatedAt = GETDATE()
                       WHERE CustomerID = @CustomerID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CustomerID", customer.Id);
        cmd.Parameters.AddWithValue("@FirstName", fName);
        cmd.Parameters.AddWithValue("@LastName", lName);
        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber ?? "");
        cmd.Parameters.AddWithValue("@Email", customer.Email ?? "");
        cmd.Parameters.AddWithValue("@Birthday", (object?)customer.Birthday ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@IsLoyaltyMember", customer.IsLoyaltyMember);
        cmd.Parameters.AddWithValue("@LoyaltyPoints", customer.LoyaltyPoints);

        cmd.ExecuteNonQuery();

        if (customer.IsLoyaltyMember)
        {
            string lSql = @"IF EXISTS (SELECT * FROM LoyaltyMembers WHERE CustomerID = @CustID)
                            UPDATE LoyaltyMembers SET CurrentPoints = @Points WHERE CustomerID = @CustID;
                            ELSE
                            INSERT INTO LoyaltyMembers (CustomerID, CurrentPoints, DateRegistered, Status) VALUES (@CustID, @Points, GETDATE(), 'ACTIVE');";
            using var lCmd = new SqlCommand(lSql, conn);
            lCmd.Parameters.AddWithValue("@CustID", customer.Id);
            lCmd.Parameters.AddWithValue("@Points", customer.LoyaltyPoints);
            lCmd.ExecuteNonQuery();
        }
    }

    public void DeleteCustomer(int id)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "UPDATE Customers SET Status = 'INACTIVE' WHERE CustomerID = @CustomerID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CustomerID", id);
        cmd.ExecuteNonQuery();
    }

    // --- Services & Base Pricing ---
    public List<ServiceItem> GetServices()
    {
        var list = new List<ServiceItem>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT ServiceID, ServiceName, Description, BasePrice, Status FROM Services ORDER BY ServiceID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new ServiceItem
            {
                Id = reader.GetInt32(reader.GetOrdinal("ServiceID")),
                ServiceName = reader.GetString(reader.GetOrdinal("ServiceName")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description")),
                BasePrice = reader.GetDecimal(reader.GetOrdinal("BasePrice")),
                IsActive = reader.GetString(reader.GetOrdinal("Status")).Equals("ACTIVE", StringComparison.OrdinalIgnoreCase)
            });
        }
        return list;
    }

    public void AddService(ServiceItem service)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"INSERT INTO Services (ServiceName, Description, BasePrice, Status, CreatedAt)
                       VALUES (@ServiceName, @Description, @BasePrice, @Status, GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ServiceName", service.ServiceName);
        cmd.Parameters.AddWithValue("@Description", service.Description ?? "");
        cmd.Parameters.AddWithValue("@BasePrice", service.BasePrice);
        cmd.Parameters.AddWithValue("@Status", service.IsActive ? "ACTIVE" : "INACTIVE");

        service.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateService(ServiceItem service)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"UPDATE Services 
                       SET ServiceName = @ServiceName, Description = @Description, 
                           BasePrice = @BasePrice, Status = @Status, UpdatedAt = GETDATE()
                       WHERE ServiceID = @ServiceID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ServiceID", service.Id);
        cmd.Parameters.AddWithValue("@ServiceName", service.ServiceName);
        cmd.Parameters.AddWithValue("@Description", service.Description ?? "");
        cmd.Parameters.AddWithValue("@BasePrice", service.BasePrice);
        cmd.Parameters.AddWithValue("@Status", service.IsActive ? "ACTIVE" : "INACTIVE");

        cmd.ExecuteNonQuery();
    }

    public void DeleteService(int id)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "UPDATE Services SET Status = 'INACTIVE' WHERE ServiceID = @ServiceID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ServiceID", id);
        cmd.ExecuteNonQuery();
    }

    // --- Promotions ---
    public List<Promotion> GetPromotions()
    {
        var list = new List<Promotion>();
        using var conn = TenantConnectionFactory.GetConnection();
         conn.Open();
        string sql = "SELECT PromotionID, Title, Description, DiscountType, DiscountValue, StartDate, EndDate, EligibilityRule, Status FROM Promotions ORDER BY PromotionID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Promotion
            {
                Id = reader.GetInt32(reader.GetOrdinal("PromotionID")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description")),
                DiscountType = reader.GetString(reader.GetOrdinal("DiscountType")),
                DiscountValue = reader.GetDecimal(reader.GetOrdinal("DiscountValue")),
                StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                EligibilityRule = reader.IsDBNull(reader.GetOrdinal("EligibilityRule")) ? "" : reader.GetString(reader.GetOrdinal("EligibilityRule")),
                IsActive = reader.GetString(reader.GetOrdinal("Status")).Equals("ACTIVE", StringComparison.OrdinalIgnoreCase)
            });
        }
        return list;
    }

    public List<Promotion> GetActivePromotions()
    {
        return GetPromotions().FindAll(p => p.IsActive && p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now);
    }

    public void AddPromotion(Promotion promo)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"INSERT INTO Promotions (Title, Description, DiscountType, DiscountValue, StartDate, EndDate, EligibilityRule, Status, CreatedAt)
                       VALUES (@Title, @Description, @DiscountType, @DiscountValue, @StartDate, @EndDate, @EligibilityRule, @Status, GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Title", promo.Title);
        cmd.Parameters.AddWithValue("@Description", promo.Description ?? "");
        cmd.Parameters.AddWithValue("@DiscountType", promo.DiscountType);
        cmd.Parameters.AddWithValue("@DiscountValue", promo.DiscountValue);
        cmd.Parameters.AddWithValue("@StartDate", promo.StartDate);
        cmd.Parameters.AddWithValue("@EndDate", promo.EndDate);
        cmd.Parameters.AddWithValue("@EligibilityRule", promo.EligibilityRule ?? "");
        cmd.Parameters.AddWithValue("@Status", promo.IsActive ? "ACTIVE" : "INACTIVE");

        promo.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdatePromotion(Promotion promo)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"UPDATE Promotions 
                       SET Title = @Title, Description = @Description, DiscountType = @DiscountType, 
                           DiscountValue = @DiscountValue, StartDate = @StartDate, EndDate = @EndDate, 
                           EligibilityRule = @EligibilityRule, Status = @Status, UpdatedAt = GETDATE()
                       WHERE PromotionID = @PromotionID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PromotionID", promo.Id);
        cmd.Parameters.AddWithValue("@Title", promo.Title);
        cmd.Parameters.AddWithValue("@Description", promo.Description ?? "");
        cmd.Parameters.AddWithValue("@DiscountType", promo.DiscountType);
        cmd.Parameters.AddWithValue("@DiscountValue", promo.DiscountValue);
        cmd.Parameters.AddWithValue("@StartDate", promo.StartDate);
        cmd.Parameters.AddWithValue("@EndDate", promo.EndDate);
        cmd.Parameters.AddWithValue("@EligibilityRule", promo.EligibilityRule ?? "");
        cmd.Parameters.AddWithValue("@Status", promo.IsActive ? "ACTIVE" : "INACTIVE");

        cmd.ExecuteNonQuery();
    }

    public void DeletePromotion(int id)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "UPDATE Promotions SET Status = 'INACTIVE' WHERE PromotionID = @PromotionID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PromotionID", id);
        cmd.ExecuteNonQuery();
    }

    // --- Loyalty Rewards ---
    public List<LoyaltyReward> GetLoyaltyRewards()
    {
        var list = new List<LoyaltyReward>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT RewardID, RewardName, RequiredPoints, DiscountAmount, Status FROM LoyaltyRewards ORDER BY RewardID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new LoyaltyReward
            {
                Id = reader.GetInt32(reader.GetOrdinal("RewardID")),
                RewardName = reader.GetString(reader.GetOrdinal("RewardName")),
                PointsRequired = reader.GetInt32(reader.GetOrdinal("RequiredPoints")),
                DiscountAmount = reader.GetDecimal(reader.GetOrdinal("DiscountAmount")),
                IsActive = reader.GetString(reader.GetOrdinal("Status")).Equals("ACTIVE", StringComparison.OrdinalIgnoreCase)
            });
        }
        return list;
    }

    public void AddLoyaltyReward(LoyaltyReward reward)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"INSERT INTO LoyaltyRewards (RewardName, RequiredPoints, DiscountAmount, Status, CreatedAt)
                       VALUES (@RewardName, @RequiredPoints, @DiscountAmount, @Status, GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@RewardName", reward.RewardName);
        cmd.Parameters.AddWithValue("@RequiredPoints", reward.PointsRequired);
        cmd.Parameters.AddWithValue("@DiscountAmount", reward.DiscountAmount);
        cmd.Parameters.AddWithValue("@Status", reward.IsActive ? "ACTIVE" : "INACTIVE");

        reward.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateLoyaltyReward(LoyaltyReward reward)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"UPDATE LoyaltyRewards 
                       SET RewardName = @RewardName, RequiredPoints = @RequiredPoints, 
                           DiscountAmount = @DiscountAmount, Status = @Status, UpdatedAt = GETDATE()
                       WHERE RewardID = @RewardID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@RewardID", reward.Id);
        cmd.Parameters.AddWithValue("@RewardName", reward.RewardName);
        cmd.Parameters.AddWithValue("@RequiredPoints", reward.PointsRequired);
        cmd.Parameters.AddWithValue("@DiscountAmount", reward.DiscountAmount);
        cmd.Parameters.AddWithValue("@Status", reward.IsActive ? "ACTIVE" : "INACTIVE");

        cmd.ExecuteNonQuery();
    }

    public void DeleteLoyaltyReward(int id)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "UPDATE LoyaltyRewards SET Status = 'INACTIVE' WHERE RewardID = @RewardID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@RewardID", id);
        cmd.ExecuteNonQuery();
    }

    // --- Attendance ---
    public List<AttendanceRecord> GetAttendanceRecords()
    {
        var list = new List<AttendanceRecord>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT AttendanceID, EmployeeID, EmployeeName, AttendanceDate, TimeIn, TimeOut, Status, Notes FROM Attendance ORDER BY AttendanceDate DESC, AttendanceID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Enum.TryParse<AttendanceStatus>(reader.GetString(reader.GetOrdinal("Status")), true, out var status);
            list.Add(new AttendanceRecord
            {
                Id = reader.GetInt32(reader.GetOrdinal("AttendanceID")),
                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                EmployeeName = reader.GetString(reader.GetOrdinal("EmployeeName")),
                Date = reader.GetDateTime(reader.GetOrdinal("AttendanceDate")),
                TimeIn = (TimeSpan)reader["TimeIn"],
                TimeOut = reader.IsDBNull(reader.GetOrdinal("TimeOut")) ? null : (TimeSpan?)reader["TimeOut"],
                Status = status,
                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? "" : reader.GetString(reader.GetOrdinal("Notes"))
            });
        }
        return list;
    }

    public List<AttendanceRecord> GetTodayAttendance()
    {
        return GetAttendanceRecords().FindAll(a => a.Date.Date == DateTime.Today);
    }

    public void RecordAttendance(AttendanceRecord record)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string checkSql = "SELECT AttendanceID FROM Attendance WHERE EmployeeID = @EmpID AND AttendanceDate = @AttDate";
        using var checkCmd = new SqlCommand(checkSql, conn);
        checkCmd.Parameters.AddWithValue("@EmpID", record.EmployeeId);
        checkCmd.Parameters.AddWithValue("@AttDate", record.Date.Date);

        var existingId = checkCmd.ExecuteScalar();
        if (existingId != null && existingId != DBNull.Value)
        {
            string upSql = @"UPDATE Attendance 
                             SET TimeIn = @TimeIn, TimeOut = @TimeOut, Status = @Status, Notes = @Notes
                             WHERE AttendanceID = @AttID";
            using var upCmd = new SqlCommand(upSql, conn);
            upCmd.Parameters.AddWithValue("@AttID", Convert.ToInt32(existingId));
            upCmd.Parameters.AddWithValue("@TimeIn", record.TimeIn);
            upCmd.Parameters.AddWithValue("@TimeOut", (object?)record.TimeOut ?? DBNull.Value);
            upCmd.Parameters.AddWithValue("@Status", record.Status.ToString());
            upCmd.Parameters.AddWithValue("@Notes", record.Notes ?? "");
            upCmd.ExecuteNonQuery();
        }
        else
        {
            string inSql = @"INSERT INTO Attendance (EmployeeID, EmployeeName, AttendanceDate, TimeIn, TimeOut, Status, Notes)
                             VALUES (@EmpID, @EmpName, @AttDate, @TimeIn, @TimeOut, @Status, @Notes);
                             SELECT SCOPE_IDENTITY();";
            using var inCmd = new SqlCommand(inSql, conn);
            inCmd.Parameters.AddWithValue("@EmpID", record.EmployeeId);
            inCmd.Parameters.AddWithValue("@EmpName", record.EmployeeName);
            inCmd.Parameters.AddWithValue("@AttDate", record.Date.Date);
            inCmd.Parameters.AddWithValue("@TimeIn", record.TimeIn);
            inCmd.Parameters.AddWithValue("@TimeOut", (object?)record.TimeOut ?? DBNull.Value);
            inCmd.Parameters.AddWithValue("@Status", record.Status.ToString());
            inCmd.Parameters.AddWithValue("@Notes", record.Notes ?? "");

            record.Id = Convert.ToInt32(inCmd.ExecuteScalar());
        }
    }

    // --- Transactions & Details ---
    public List<Transaction> GetTransactions()
    {
        var list = new List<Transaction>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"SELECT TransactionID, TransactionNumber, CustomerID, CustomerName, StaffID, StaffName, 
                              BarberID, BarberName, ServiceID, ServiceName, Subtotal, DiscountAmount, FinalAmount, 
                              PaymentMethod, Status, PromotionID, LoyaltyRewardID, PointsEarned, PointsRedeemed, 
                              AmountReceived, ChangeAmount, TransactionDate 
                       FROM Transactions ORDER BY TransactionDate DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Enum.TryParse<PaymentMethod>(reader.GetString(reader.GetOrdinal("PaymentMethod")), true, out var pm);
            Enum.TryParse<TransactionStatus>(reader.GetString(reader.GetOrdinal("Status")), true, out var st);

            list.Add(new Transaction
            {
                Id = reader.GetInt32(reader.GetOrdinal("TransactionID")),
                TransactionNumber = reader.GetString(reader.GetOrdinal("TransactionNumber")),
                CustomerId = reader.IsDBNull(reader.GetOrdinal("CustomerID")) ? null : reader.GetInt32(reader.GetOrdinal("CustomerID")),
                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                StaffId = reader.GetInt32(reader.GetOrdinal("StaffID")),
                StaffName = reader.GetString(reader.GetOrdinal("StaffName")),
                BarberId = reader.GetInt32(reader.GetOrdinal("BarberID")),
                BarberName = reader.GetString(reader.GetOrdinal("BarberName")),
                ServiceId = reader.IsDBNull(reader.GetOrdinal("ServiceID")) ? 1 : reader.GetInt32(reader.GetOrdinal("ServiceID")),
                ServiceName = reader.GetString(reader.GetOrdinal("ServiceName")),
                Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal")),
                DiscountAmount = reader.GetDecimal(reader.GetOrdinal("DiscountAmount")),
                FinalAmount = reader.GetDecimal(reader.GetOrdinal("FinalAmount")),
                PaymentMethod = pm,
                Status = st,
                PromotionId = reader.IsDBNull(reader.GetOrdinal("PromotionID")) ? null : reader.GetInt32(reader.GetOrdinal("PromotionID")),
                LoyaltyRewardId = reader.IsDBNull(reader.GetOrdinal("LoyaltyRewardID")) ? null : reader.GetInt32(reader.GetOrdinal("LoyaltyRewardID")),
                PointsEarned = reader.GetInt32(reader.GetOrdinal("PointsEarned")),
                PointsRedeemed = reader.GetInt32(reader.GetOrdinal("PointsRedeemed")),
                AmountReceived = reader.GetDecimal(reader.GetOrdinal("AmountReceived")),
                ChangeAmount = reader.GetDecimal(reader.GetOrdinal("ChangeAmount")),
                TransactionDate = reader.GetDateTime(reader.GetOrdinal("TransactionDate"))
            });
        }
        return list;
    }

    public List<Transaction> GetTodayTransactions()
    {
        return GetTransactions().FindAll(t => t.TransactionDate.Date == DateTime.Today);
    }

    public List<Transaction> GetCustomerTransactions(int customerId)
    {
        return GetTransactions().FindAll(t => t.CustomerId == customerId);
    }

    public string GenerateTransactionNumber()
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT COUNT(*) FROM Transactions";
        using var cmd = new SqlCommand(sql, conn);
        int count = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
        return $"TXN-{DateTime.Now:yyyyMMdd}-{count:D3}";
    }

    private static bool _loyaltySchemaEnsured = false;
    private static readonly object _loyaltySchemaLock = new();

    /// <summary>
    /// Best-effort migration: extends LoyaltyTransactions with activity metadata
    /// and enforces one loyalty record per transaction (prevents double point awards).
    /// </summary>
    private void EnsureLoyaltySchema(SqlConnection conn, SqlTransaction dbTxn)
    {
        if (_loyaltySchemaEnsured) return;
        lock (_loyaltySchemaLock)
        {
            if (_loyaltySchemaEnsured) return;
            try
            {
                string sql = @"
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('LoyaltyTransactions') AND name = 'ActivityType')
                        ALTER TABLE LoyaltyTransactions ADD ActivityType NVARCHAR(20) NOT NULL DEFAULT 'EARNED';
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('LoyaltyTransactions') AND name = 'PreviousBalance')
                        ALTER TABLE LoyaltyTransactions ADD PreviousBalance INT NOT NULL DEFAULT 0;
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('LoyaltyTransactions') AND name = 'NewBalance')
                        ALTER TABLE LoyaltyTransactions ADD NewBalance INT NOT NULL DEFAULT 0;
                    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UX_LoyaltyTransactions_TransactionID')
                        CREATE UNIQUE NONCLUSTERED INDEX UX_LoyaltyTransactions_TransactionID
                            ON LoyaltyTransactions(TransactionID) WHERE TransactionID IS NOT NULL;";
                using var cmd = new SqlCommand(sql, conn, dbTxn);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                // Non-fatal: idempotency guard in SaveTransaction still prevents double awards.
            }
            _loyaltySchemaEnsured = true;
        }
    }

    public void SaveTransaction(Transaction txn)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        using var dbTxn = conn.BeginTransaction();
        try
        {
            EnsureLoyaltySchema(conn, dbTxn);

            SaveTransactionCore(txn, conn, dbTxn);
            dbTxn.Commit();
        }
        catch
        {
            dbTxn.Rollback();
            throw;
        }
    }

    private void SaveTransactionCore(Transaction txn, SqlConnection conn, SqlTransaction dbTxn)
    {
        if (txn.Id == 0)
        {
            string sql = @"INSERT INTO Transactions (TransactionNumber, CustomerID, CustomerName, StaffID, StaffName, BarberID, BarberName, ServiceID, ServiceName, Subtotal, DiscountAmount, FinalAmount, PaymentMethod, Status, PromotionID, LoyaltyRewardID, PointsEarned, PointsRedeemed, AmountReceived, ChangeAmount, TransactionDate)
                           VALUES (@TxnNum, @CustID, @CustName, @StaffID, @StaffName, @BarberID, @BarberName, @SvcID, @SvcName, @Subtotal, @Discount, @Final, @PM, @Status, @PromoID, @RewardID, @PtsEarned, @PtsRedeemed, @AmtRec, @ChangeAmt, GETDATE());
                           SELECT SCOPE_IDENTITY();";
            using var cmd = new SqlCommand(sql, conn, dbTxn);
            cmd.Parameters.AddWithValue("@TxnNum", txn.TransactionNumber);
            cmd.Parameters.AddWithValue("@CustID", (object?)txn.CustomerId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CustName", txn.CustomerName);
            cmd.Parameters.AddWithValue("@StaffID", txn.StaffId > 0 ? txn.StaffId : 1);
            cmd.Parameters.AddWithValue("@StaffName", txn.StaffName ?? "Staff");
            cmd.Parameters.AddWithValue("@BarberID", txn.BarberId > 0 ? txn.BarberId : 1);
            cmd.Parameters.AddWithValue("@BarberName", txn.BarberName ?? "Barber");
            cmd.Parameters.AddWithValue("@SvcID", (object?)txn.ServiceId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SvcName", txn.ServiceName);
            cmd.Parameters.AddWithValue("@Subtotal", txn.Subtotal);
            cmd.Parameters.AddWithValue("@Discount", txn.DiscountAmount);
            cmd.Parameters.AddWithValue("@Final", txn.FinalAmount);
            cmd.Parameters.AddWithValue("@PM", txn.PaymentMethod.ToString());
            cmd.Parameters.AddWithValue("@Status", txn.Status.ToString());
            cmd.Parameters.AddWithValue("@PromoID", (object?)txn.PromotionId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RewardID", (object?)txn.LoyaltyRewardId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@PtsEarned", txn.PointsEarned);
            cmd.Parameters.AddWithValue("@PtsRedeemed", txn.PointsRedeemed);
            cmd.Parameters.AddWithValue("@AmtRec", txn.AmountReceived);
            cmd.Parameters.AddWithValue("@ChangeAmt", txn.ChangeAmount);

            txn.Id = Convert.ToInt32(cmd.ExecuteScalar());

            // Save Transaction Details record preserving historical service price
            string detSql = @"INSERT INTO TransactionDetails (TransactionID, ServiceID, ServiceName, Quantity, UnitPrice, Subtotal)
                             VALUES (@TxnID, @SvcID, @SvcName, 1, @UnitPrice, @Subtotal)";
            using var detCmd = new SqlCommand(detSql, conn, dbTxn);
            detCmd.Parameters.AddWithValue("@TxnID", txn.Id);
            detCmd.Parameters.AddWithValue("@SvcID", (object?)txn.ServiceId ?? DBNull.Value);
            detCmd.Parameters.AddWithValue("@SvcName", txn.ServiceName);
            detCmd.Parameters.AddWithValue("@UnitPrice", txn.Subtotal);
            detCmd.Parameters.AddWithValue("@Subtotal", txn.Subtotal);
            detCmd.ExecuteNonQuery();
        }
        else
        {
            string sql = @"UPDATE Transactions 
                           SET Status = @Status, PaymentMethod = @PM, AmountReceived = @AmtRec, 
                               ChangeAmount = @ChangeAmt, FinalAmount = @Final, DiscountAmount = @Discount, 
                               PointsEarned = @PtsEarned, PointsRedeemed = @PtsRedeemed
                           WHERE TransactionID = @TxnID";
            using var cmd = new SqlCommand(sql, conn, dbTxn);
            cmd.Parameters.AddWithValue("@TxnID", txn.Id);
            cmd.Parameters.AddWithValue("@Status", txn.Status.ToString());
            cmd.Parameters.AddWithValue("@PM", txn.PaymentMethod.ToString());
            cmd.Parameters.AddWithValue("@AmtRec", txn.AmountReceived);
            cmd.Parameters.AddWithValue("@ChangeAmt", txn.ChangeAmount);
            cmd.Parameters.AddWithValue("@Final", txn.FinalAmount);
            cmd.Parameters.AddWithValue("@Discount", txn.DiscountAmount);
            cmd.Parameters.AddWithValue("@PtsEarned", txn.PointsEarned);
            cmd.Parameters.AddWithValue("@PtsRedeemed", txn.PointsRedeemed);
            cmd.ExecuteNonQuery();
        }

        // Loyalty points are awarded/deducted exactly once per completed transaction.
        // The EXISTS guard + UX_LoyaltyTransactions_TransactionID index make re-saves,
        // refreshes and double-clicks safe: a second attempt is a no-op.
        if (txn.Status == TransactionStatus.Completed && txn.CustomerId.HasValue && txn.Id > 0)
        {
            ApplyLoyaltyPoints(txn, conn, dbTxn);
        }
    }

    private void ApplyLoyaltyPoints(Transaction txn, SqlConnection conn, SqlTransaction dbTxn)
    {
        // Lock and read the customer inside this tenant's database (CompanyID isolation
        // is structural: each company has its own database/connection).
        string custSql = @"SELECT LoyaltyPoints, IsLoyaltyMember
                          FROM Customers WITH (UPDLOCK, HOLDLOCK)
                          WHERE CustomerID = @CustID AND Status = 'ACTIVE'";
        int currentPoints;
        bool isMember;
        using (var custCmd = new SqlCommand(custSql, conn, dbTxn))
        {
            custCmd.Parameters.AddWithValue("@CustID", txn.CustomerId!.Value);
            using var reader = custCmd.ExecuteReader();
            if (!reader.Read()) return;
            currentPoints = reader.GetInt32(0);
            isMember = reader.GetBoolean(1);
        }

        if (!isMember) return;

        // Skip if this transaction already earned/redeemed points.
        string existsSql = "SELECT COUNT(*) FROM LoyaltyTransactions WHERE TransactionID = @TxnID";
        using (var existsCmd = new SqlCommand(existsSql, conn, dbTxn))
        {
            existsCmd.Parameters.AddWithValue("@TxnID", txn.Id);
            if (Convert.ToInt32(existsCmd.ExecuteScalar()) > 0) return;
        }

        // A selected reward must still be active to redeem points for it.
        int requestedRedeem = txn.PointsRedeemed;
        if (requestedRedeem > 0 && txn.LoyaltyRewardId.HasValue)
        {
            string rwSql = "SELECT COUNT(*) FROM LoyaltyRewards WHERE RewardID = @RwID AND Status = 'ACTIVE'";
            using var rwCmd = new SqlCommand(rwSql, conn, dbTxn);
            rwCmd.Parameters.AddWithValue("@RwID", txn.LoyaltyRewardId.Value);
            if (Convert.ToInt32(rwCmd.ExecuteScalar()) == 0) requestedRedeem = 0;
        }

        int redeemed = Math.Min(requestedRedeem, currentPoints); // never go negative
        int earned = Math.Max(0, txn.PointsEarned);
        int newBalance = currentPoints - redeemed + earned;

        string activityType = redeemed > 0 && earned > 0 ? "EARNED+REDEEMED"
                            : redeemed > 0 ? "REDEEMED"
                            : earned > 0 ? "EARNED" : "ADJUSTED";
        string description = redeemed > 0
            ? $"Service '{txn.ServiceName}' (+{earned} pts earned, -{redeemed} pts redeemed) - {txn.TransactionNumber}"
            : $"Service '{txn.ServiceName}' (+{earned} pts earned) - {txn.TransactionNumber}";

        bool balancesWritten = false;
        try
        {
            string lLogSql = @"INSERT INTO LoyaltyTransactions (CustomerID, TransactionID, PointsEarned, PointsRedeemed, ActivityType, PreviousBalance, NewBalance, Description, DateCreated, RecordedBy)
                               VALUES (@CustID, @TxnID, @Earned, @Redeemed, @ActType, @Prev, @New, @Desc, GETDATE(), @RecBy)";
            using var lCmd = new SqlCommand(lLogSql, conn, dbTxn);
            lCmd.Parameters.AddWithValue("@CustID", txn.CustomerId!.Value);
            lCmd.Parameters.AddWithValue("@TxnID", txn.Id);
            lCmd.Parameters.AddWithValue("@Earned", earned);
            lCmd.Parameters.AddWithValue("@Redeemed", redeemed);
            lCmd.Parameters.AddWithValue("@ActType", activityType);
            lCmd.Parameters.AddWithValue("@Prev", currentPoints);
            lCmd.Parameters.AddWithValue("@New", newBalance);
            lCmd.Parameters.AddWithValue("@Desc", description);
            lCmd.Parameters.AddWithValue("@RecBy", txn.StaffName ?? "Staff");
            lCmd.ExecuteNonQuery();
            balancesWritten = true;
        }
        catch (Microsoft.Data.SqlClient.SqlException)
        {
            // Extended columns missing (schema migration rejected): fall back to the
            // original insert shape so history is still recorded.
            string lLogSql = @"IF NOT EXISTS (SELECT 1 FROM LoyaltyTransactions WHERE TransactionID = @TxnID)
                               INSERT INTO LoyaltyTransactions (CustomerID, TransactionID, PointsEarned, PointsRedeemed, Description, DateCreated, RecordedBy)
                               VALUES (@CustID, @TxnID, @Earned, @Redeemed, @Desc, GETDATE(), @RecBy)";
            using var lCmd = new SqlCommand(lLogSql, conn, dbTxn);
            lCmd.Parameters.AddWithValue("@CustID", txn.CustomerId!.Value);
            lCmd.Parameters.AddWithValue("@TxnID", txn.Id);
            lCmd.Parameters.AddWithValue("@Earned", earned);
            lCmd.Parameters.AddWithValue("@Redeemed", redeemed);
            lCmd.Parameters.AddWithValue("@Desc", description);
            lCmd.Parameters.AddWithValue("@RecBy", txn.StaffName ?? "Staff");
            balancesWritten = lCmd.ExecuteNonQuery() > 0;
        }

        if (!balancesWritten) return;

        string updSql = @"UPDATE Customers SET LoyaltyPoints = @New, UpdatedAt = GETDATE() WHERE CustomerID = @CustID;
                          IF EXISTS (SELECT 1 FROM LoyaltyMembers WHERE CustomerID = @CustID)
                              UPDATE LoyaltyMembers SET CurrentPoints = @New WHERE CustomerID = @CustID;
                          ELSE
                              INSERT INTO LoyaltyMembers (CustomerID, CurrentPoints, DateRegistered, Status) VALUES (@CustID, @New, GETDATE(), 'ACTIVE');";
        using var updCmd = new SqlCommand(updSql, conn, dbTxn);
        updCmd.Parameters.AddWithValue("@New", newBalance);
        updCmd.Parameters.AddWithValue("@CustID", txn.CustomerId!.Value);
        updCmd.ExecuteNonQuery();
    }

    public List<LoyaltyHistoryEntry> GetLoyaltyHistory(int customerId)
    {
        var list = new List<LoyaltyHistoryEntry>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"SELECT * FROM LoyaltyTransactions WHERE CustomerID = @CustID ORDER BY DateCreated DESC";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CustID", customerId);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new LoyaltyHistoryEntry
            {
                Id = reader.GetInt32(reader.GetOrdinal("LoyaltyTransactionID")),
                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                TransactionId = reader.IsDBNull(reader.GetOrdinal("TransactionID")) ? null : reader.GetInt32(reader.GetOrdinal("TransactionID")),
                PointsEarned = reader.GetInt32(reader.GetOrdinal("PointsEarned")),
                PointsRedeemed = reader.GetInt32(reader.GetOrdinal("PointsRedeemed")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description")),
                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                RecordedBy = reader.IsDBNull(reader.GetOrdinal("RecordedBy")) ? "" : reader.GetString(reader.GetOrdinal("RecordedBy"))
            });
            ReadActivityMetadata(reader, list[^1]);
        }
        return list;
    }

    /// <summary>
    /// Reads the extended activity columns when the migration has been applied;
    /// older databases simply keep the defaults.
    /// </summary>
    private static void ReadActivityMetadata(SqlDataReader reader, LoyaltyHistoryEntry entry)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            switch (reader.GetName(i))
            {
                case "ActivityType" when !reader.IsDBNull(i):
                    entry.ActivityType = reader.GetString(i);
                    break;
                case "PreviousBalance" when !reader.IsDBNull(i):
                    entry.PreviousBalance = reader.GetInt32(i);
                    break;
                case "NewBalance" when !reader.IsDBNull(i):
                    entry.NewBalance = reader.GetInt32(i);
                    break;
            }
        }
    }

    public List<LoyaltyHistoryEntry> GetAllLoyaltyHistory()
    {
        EnsureNotSuperAdmin();
        var list = new List<LoyaltyHistoryEntry>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"SELECT lt.*, c.FirstName + ' ' + c.LastName AS CustomerName
                       FROM LoyaltyTransactions lt
                       INNER JOIN Customers c ON lt.CustomerID = c.CustomerID
                       ORDER BY lt.DateCreated DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new LoyaltyHistoryEntry
            {
                Id = reader.GetInt32(reader.GetOrdinal("LoyaltyTransactionID")),
                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                TransactionId = reader.IsDBNull(reader.GetOrdinal("TransactionID")) ? null : reader.GetInt32(reader.GetOrdinal("TransactionID")),
                PointsEarned = reader.GetInt32(reader.GetOrdinal("PointsEarned")),
                PointsRedeemed = reader.GetInt32(reader.GetOrdinal("PointsRedeemed")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description")),
                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                RecordedBy = reader.IsDBNull(reader.GetOrdinal("RecordedBy")) ? "" : reader.GetString(reader.GetOrdinal("RecordedBy"))
            });
            ReadActivityMetadata(reader, list[^1]);
        }
        return list;
    }

    // --- Inventory Management ---
    public List<InventoryItem> GetInventoryItems()
    {
        var list = new List<InventoryItem>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"SELECT i.InventoryItemID, i.ItemName, i.Category, i.Quantity, i.Unit, i.MinimumStockLevel, 
                              i.SupplierID, s.SupplierName, i.Cost, i.Status, i.BranchID, i.CreatedAt, i.UpdatedAt 
                       FROM InventoryItems i 
                       LEFT JOIN Suppliers s ON i.SupplierID = s.SupplierID 
                       ORDER BY i.InventoryItemID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            int qty = reader.GetInt32(reader.GetOrdinal("Quantity"));
            int minLevel = reader.GetInt32(reader.GetOrdinal("MinimumStockLevel"));
            string status = qty <= 0 ? "OUT OF STOCK" : (qty <= minLevel ? "LOW STOCK" : "IN STOCK");

            list.Add(new InventoryItem
            {
                Id = reader.GetInt32(reader.GetOrdinal("InventoryItemID")),
                ItemName = reader.GetString(reader.GetOrdinal("ItemName")),
                Category = reader.IsDBNull(reader.GetOrdinal("Category")) ? "General" : reader.GetString(reader.GetOrdinal("Category")),
                Quantity = qty,
                Unit = reader.IsDBNull(reader.GetOrdinal("Unit")) ? "pcs" : reader.GetString(reader.GetOrdinal("Unit")),
                MinimumStockLevel = minLevel,
                SupplierId = reader.IsDBNull(reader.GetOrdinal("SupplierID")) ? null : reader.GetInt32(reader.GetOrdinal("SupplierID")),
                SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName")) ? "None" : reader.GetString(reader.GetOrdinal("SupplierName")),
                Cost = reader.GetDecimal(reader.GetOrdinal("Cost")),
                Status = status,
                BranchId = reader.IsDBNull(reader.GetOrdinal("BranchID")) ? 1 : reader.GetInt32(reader.GetOrdinal("BranchID")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
            });
        }
        return list;
    }

    public void AddInventoryItem(InventoryItem item)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string status = item.Quantity <= 0 ? "OUT OF STOCK" : (item.Quantity <= item.MinimumStockLevel ? "LOW STOCK" : "IN STOCK");
        string sql = @"INSERT INTO InventoryItems (ItemName, Category, Quantity, Unit, MinimumStockLevel, SupplierID, Cost, Status, BranchID, CreatedAt)
                       VALUES (@ItemName, @Category, @Quantity, @Unit, @MinLevel, @SupplierID, @Cost, @Status, @BranchID, GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
        cmd.Parameters.AddWithValue("@Category", item.Category ?? "General");
        cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
        cmd.Parameters.AddWithValue("@Unit", item.Unit ?? "pcs");
        cmd.Parameters.AddWithValue("@MinLevel", item.MinimumStockLevel);
        cmd.Parameters.AddWithValue("@SupplierID", (object?)item.SupplierId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Cost", item.Cost);
        cmd.Parameters.AddWithValue("@Status", status);
        cmd.Parameters.AddWithValue("@BranchID", item.BranchId ?? 1);

        item.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateInventoryItem(InventoryItem item)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string status = item.Quantity <= 0 ? "OUT OF STOCK" : (item.Quantity <= item.MinimumStockLevel ? "LOW STOCK" : "IN STOCK");
        string sql = @"UPDATE InventoryItems 
                       SET ItemName = @ItemName, Category = @Category, Quantity = @Quantity, Unit = @Unit, 
                           MinimumStockLevel = @MinLevel, SupplierID = @SupplierID, Cost = @Cost, Status = @Status, 
                           BranchID = @BranchID, UpdatedAt = GETDATE()
                       WHERE InventoryItemID = @ItemID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ItemID", item.Id);
        cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
        cmd.Parameters.AddWithValue("@Category", item.Category ?? "General");
        cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
        cmd.Parameters.AddWithValue("@Unit", item.Unit ?? "pcs");
        cmd.Parameters.AddWithValue("@MinLevel", item.MinimumStockLevel);
        cmd.Parameters.AddWithValue("@SupplierID", (object?)item.SupplierId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Cost", item.Cost);
        cmd.Parameters.AddWithValue("@Status", status);
        cmd.Parameters.AddWithValue("@BranchID", item.BranchId ?? 1);

        cmd.ExecuteNonQuery();
    }

    public void DeleteInventoryItem(int id)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "DELETE FROM InventoryItems WHERE InventoryItemID = @ItemID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ItemID", id);
        cmd.ExecuteNonQuery();
    }

    public void RecordStockTransaction(InventoryTransaction txn)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();

        // 1. Insert Inventory Transaction log
        string sql = @"INSERT INTO InventoryTransactions (InventoryItemID, TransactionType, Quantity, DateCreated, RecordedBy, Notes)
                       VALUES (@ItemID, @Type, @Qty, GETDATE(), @RecBy, @Notes);
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ItemID", txn.InventoryItemId);
        cmd.Parameters.AddWithValue("@Type", txn.TransactionType);
        cmd.Parameters.AddWithValue("@Qty", txn.Quantity);
        cmd.Parameters.AddWithValue("@RecBy", txn.RecordedBy ?? "Staff");
        cmd.Parameters.AddWithValue("@Notes", txn.Notes ?? "");

        txn.Id = Convert.ToInt32(cmd.ExecuteScalar());

        // 2. Adjust Stock Quantity automatically
        int qtyChange = (txn.TransactionType == "STOCK IN" || txn.TransactionType == "RESTOCK") ? txn.Quantity : -txn.Quantity;

        string updateQtySql = @"UPDATE InventoryItems 
                                SET Quantity = CASE WHEN (Quantity + @QtyChange) < 0 THEN 0 ELSE (Quantity + @QtyChange) END,
                                    Status = CASE 
                                        WHEN (Quantity + @QtyChange) <= 0 THEN 'OUT OF STOCK' 
                                        WHEN (Quantity + @QtyChange) <= MinimumStockLevel THEN 'LOW STOCK' 
                                        ELSE 'IN STOCK' 
                                    END,
                                    UpdatedAt = GETDATE()
                                WHERE InventoryItemID = @ItemID";
        using var upCmd = new SqlCommand(updateQtySql, conn);
        upCmd.Parameters.AddWithValue("@QtyChange", qtyChange);
        upCmd.Parameters.AddWithValue("@ItemID", txn.InventoryItemId);
        upCmd.ExecuteNonQuery();
    }

    public List<InventoryTransaction> GetInventoryTransactions()
    {
        var list = new List<InventoryTransaction>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"SELECT t.InventoryTransactionID, t.InventoryItemID, i.ItemName, t.TransactionType, 
                              t.Quantity, t.DateCreated, t.RecordedBy, t.Notes 
                       FROM InventoryTransactions t 
                       INNER JOIN InventoryItems i ON t.InventoryItemID = i.InventoryItemID 
                       ORDER BY t.DateCreated DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new InventoryTransaction
            {
                Id = reader.GetInt32(reader.GetOrdinal("InventoryTransactionID")),
                InventoryItemId = reader.GetInt32(reader.GetOrdinal("InventoryItemID")),
                ItemName = reader.GetString(reader.GetOrdinal("ItemName")),
                TransactionType = reader.GetString(reader.GetOrdinal("TransactionType")),
                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                DateCreated = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                RecordedBy = reader.IsDBNull(reader.GetOrdinal("RecordedBy")) ? "Staff" : reader.GetString(reader.GetOrdinal("RecordedBy")),
                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? "" : reader.GetString(reader.GetOrdinal("Notes"))
            });
        }
        return list;
    }

    // --- Suppliers ---
    public List<Supplier> GetSuppliers()
    {
        var list = new List<Supplier>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT SupplierID, SupplierName, ContactInformation, Status, CreatedAt, UpdatedAt FROM Suppliers ORDER BY SupplierID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Supplier
            {
                Id = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
                ContactInformation = reader.IsDBNull(reader.GetOrdinal("ContactInformation")) ? "" : reader.GetString(reader.GetOrdinal("ContactInformation")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
            });
        }
        return list;
    }

    public void AddSupplier(Supplier supp)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"INSERT INTO Suppliers (SupplierName, ContactInformation, Status, CreatedAt)
                       VALUES (@SupplierName, @ContactInfo, @Status, GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@SupplierName", supp.SupplierName);
        cmd.Parameters.AddWithValue("@ContactInfo", supp.ContactInformation ?? "");
        cmd.Parameters.AddWithValue("@Status", supp.Status ?? "ACTIVE");

        supp.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateSupplier(Supplier supp)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"UPDATE Suppliers 
                       SET SupplierName = @SupplierName, ContactInformation = @ContactInfo, 
                           Status = @Status, UpdatedAt = GETDATE()
                       WHERE SupplierID = @SupplierID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@SupplierID", supp.Id);
        cmd.Parameters.AddWithValue("@SupplierName", supp.SupplierName);
        cmd.Parameters.AddWithValue("@ContactInfo", supp.ContactInformation ?? "");
        cmd.Parameters.AddWithValue("@Status", supp.Status ?? "ACTIVE");

        cmd.ExecuteNonQuery();
    }

    public void DeleteSupplier(int id)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "UPDATE Suppliers SET Status = 'INACTIVE' WHERE SupplierID = @SupplierID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@SupplierID", id);
        cmd.ExecuteNonQuery();
    }

    // --- Branches ---
    public List<Branch> GetBranches()
    {
        var list = new List<Branch>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT BranchID, BranchName, Address, ContactInformation, Status, CreatedAt, UpdatedAt FROM Branches ORDER BY BranchID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Branch
            {
                Id = reader.GetInt32(reader.GetOrdinal("BranchID")),
                BranchName = reader.GetString(reader.GetOrdinal("BranchName")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? "" : reader.GetString(reader.GetOrdinal("Address")),
                ContactInformation = reader.IsDBNull(reader.GetOrdinal("ContactInformation")) ? "" : reader.GetString(reader.GetOrdinal("ContactInformation")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
            });
        }
        return list;
    }

    public void AddBranch(Branch branch)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"INSERT INTO Branches (BranchName, Address, ContactInformation, Status, CreatedAt)
                       VALUES (@BranchName, @Address, @ContactInfo, @Status, GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BranchName", branch.BranchName);
        cmd.Parameters.AddWithValue("@Address", branch.Address ?? "");
        cmd.Parameters.AddWithValue("@ContactInfo", branch.ContactInformation ?? "");
        cmd.Parameters.AddWithValue("@Status", branch.Status ?? "ACTIVE");

        branch.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateBranch(Branch branch)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"UPDATE Branches 
                       SET BranchName = @BranchName, Address = @Address, 
                           ContactInformation = @ContactInfo, Status = @Status, UpdatedAt = GETDATE()
                       WHERE BranchID = @BranchID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BranchID", branch.Id);
        cmd.Parameters.AddWithValue("@BranchName", branch.BranchName);
        cmd.Parameters.AddWithValue("@Address", branch.Address ?? "");
        cmd.Parameters.AddWithValue("@ContactInfo", branch.ContactInformation ?? "");
        cmd.Parameters.AddWithValue("@Status", branch.Status ?? "ACTIVE");

        cmd.ExecuteNonQuery();
    }

    public void DeleteBranch(int id)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "UPDATE Branches SET Status = 'INACTIVE' WHERE BranchID = @BranchID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BranchID", id);
        cmd.ExecuteNonQuery();
    }

    // --- System Maintenance & Support ---
    public List<SystemLog> GetSystemLogs()
    {
        var list = new List<SystemLog>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT LogID, Timestamp, LogLevel, Module, Message, ActionBy FROM SystemLogs ORDER BY Timestamp DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new SystemLog
            {
                Id = reader.GetInt32(reader.GetOrdinal("LogID")),
                Timestamp = reader.GetDateTime(reader.GetOrdinal("Timestamp")),
                LogLevel = reader.GetString(reader.GetOrdinal("LogLevel")),
                Module = reader.GetString(reader.GetOrdinal("Module")),
                Message = reader.GetString(reader.GetOrdinal("Message")),
                ActionBy = reader.GetString(reader.GetOrdinal("ActionBy"))
            });
        }
        return list;
    }

    public void AddSystemLog(string level, string module, string message, string actionBy)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = @"INSERT INTO SystemLogs (Timestamp, LogLevel, Module, Message, ActionBy)
                       VALUES (GETDATE(), @Level, @Module, @Message, @ActionBy)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Level", level ?? "INFO");
        cmd.Parameters.AddWithValue("@Module", module ?? "System");
        cmd.Parameters.AddWithValue("@Message", message ?? "");
        cmd.Parameters.AddWithValue("@ActionBy", actionBy ?? "System");
        cmd.ExecuteNonQuery();
    }

    public List<SupportRequest> GetSupportRequests()
    {
        var list = new List<SupportRequest>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "SELECT SupportID, TicketNumber, RequestedBy, Subject, Details, Priority, Status, CreatedDate FROM SupportRequests ORDER BY SupportID DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new SupportRequest
            {
                Id = reader.GetInt32(reader.GetOrdinal("SupportID")),
                TicketNumber = reader.GetString(reader.GetOrdinal("TicketNumber")),
                RequestedBy = reader.GetString(reader.GetOrdinal("RequestedBy")),
                Subject = reader.GetString(reader.GetOrdinal("Subject")),
                Details = reader.IsDBNull(reader.GetOrdinal("Details")) ? "" : reader.GetString(reader.GetOrdinal("Details")),
                Priority = reader.GetString(reader.GetOrdinal("Priority")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
            });
        }
        return list;
    }

    public void AddSupportRequest(SupportRequest req)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string ticket = $"TKT-{8000 + Random.Shared.Next(100, 999)}";
        string sql = @"INSERT INTO SupportRequests (TicketNumber, RequestedBy, Subject, Details, Priority, Status, CreatedDate)
                       VALUES (@TicketNumber, @RequestedBy, @Subject, @Details, @Priority, 'Open', GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TicketNumber", ticket);
        cmd.Parameters.AddWithValue("@RequestedBy", req.RequestedBy);
        cmd.Parameters.AddWithValue("@Subject", req.Subject);
        cmd.Parameters.AddWithValue("@Details", req.Details ?? "");
        cmd.Parameters.AddWithValue("@Priority", req.Priority ?? "Medium");

        req.Id = Convert.ToInt32(cmd.ExecuteScalar());
        req.TicketNumber = ticket;
    }

    public void UpdateSupportRequestStatus(int id, string status)
    {
        EnsureNotSuperAdmin();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        string sql = "UPDATE SupportRequests SET Status = @Status WHERE SupportID = @ID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Status", status);
        cmd.Parameters.AddWithValue("@ID", id);
        cmd.ExecuteNonQuery();
    }

    // ==================== APPOINTMENTS ====================

    private static bool _appointmentSchemaEnsured = false;
    private static readonly object _appointmentSchemaLock = new();

    private void EnsureAppointmentSchema(SqlConnection conn)
    {
        if (_appointmentSchemaEnsured) return;
        lock (_appointmentSchemaLock)
        {
            if (_appointmentSchemaEnsured) return;
            string sql = @"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Appointments')
                CREATE TABLE Appointments (
                    AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
                    AppointmentNumber NVARCHAR(50) NOT NULL,
                    CustomerID INT NOT NULL,
                    CustomerName NVARCHAR(100) NOT NULL,
                    ServiceID INT NULL,
                    ServiceName NVARCHAR(100) NOT NULL,
                    BarberID INT NULL,
                    BarberName NVARCHAR(100) NOT NULL,
                    ScheduledAt DATETIME NOT NULL,
                    Status NVARCHAR(20) NOT NULL DEFAULT 'Scheduled',
                    Notes NVARCHAR(255) NULL,
                    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
                );";
            using var cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            _appointmentSchemaEnsured = true;
        }
    }

    public List<Appointment> GetAppointments()
    {
        EnsureNotSuperAdmin();
        var list = new List<Appointment>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        EnsureAppointmentSchema(conn);
        string sql = @"SELECT AppointmentID, AppointmentNumber, CustomerID, CustomerName, ServiceID, ServiceName, BarberID, BarberName, ScheduledAt, Status, Notes, CreatedDate
                       FROM Appointments ORDER BY ScheduledAt DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Appointment
            {
                Id = reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                AppointmentNumber = reader.GetString(reader.GetOrdinal("AppointmentNumber")),
                CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                ServiceId = reader.IsDBNull(reader.GetOrdinal("ServiceID")) ? 0 : reader.GetInt32(reader.GetOrdinal("ServiceID")),
                ServiceName = reader.GetString(reader.GetOrdinal("ServiceName")),
                BarberId = reader.IsDBNull(reader.GetOrdinal("BarberID")) ? 0 : reader.GetInt32(reader.GetOrdinal("BarberID")),
                BarberName = reader.GetString(reader.GetOrdinal("BarberName")),
                ScheduledAt = reader.GetDateTime(reader.GetOrdinal("ScheduledAt")),
                Status = Enum.TryParse(reader.GetString(reader.GetOrdinal("Status")), out AppointmentStatus st) ? st : AppointmentStatus.Scheduled,
                Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? "" : reader.GetString(reader.GetOrdinal("Notes")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
            });
        }
        return list;
    }

    public void AddAppointment(Appointment appointment)
    {
        EnsureNotSuperAdmin();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        EnsureAppointmentSchema(conn);
        appointment.AppointmentNumber = GenerateAppointmentNumber();
        string sql = @"INSERT INTO Appointments (AppointmentNumber, CustomerID, CustomerName, ServiceID, ServiceName, BarberID, BarberName, ScheduledAt, Status, Notes, CreatedDate)
                       VALUES (@Num, @CustID, @CustName, @SvcID, @SvcName, @BarbID, @BarbName, @When, @Status, @Notes, GETDATE());
                       SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Num", appointment.AppointmentNumber);
        cmd.Parameters.AddWithValue("@CustID", appointment.CustomerId);
        cmd.Parameters.AddWithValue("@CustName", appointment.CustomerName);
        cmd.Parameters.AddWithValue("@SvcID", appointment.ServiceId > 0 ? appointment.ServiceId : DBNull.Value);
        cmd.Parameters.AddWithValue("@SvcName", appointment.ServiceName);
        cmd.Parameters.AddWithValue("@BarbID", appointment.BarberId > 0 ? appointment.BarberId : DBNull.Value);
        cmd.Parameters.AddWithValue("@BarbName", appointment.BarberName);
        cmd.Parameters.AddWithValue("@When", appointment.ScheduledAt);
        cmd.Parameters.AddWithValue("@Status", appointment.Status.ToString());
        cmd.Parameters.AddWithValue("@Notes", appointment.Notes ?? "");
        appointment.Id = Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void UpdateAppointment(Appointment appointment)
    {
        EnsureNotSuperAdmin();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        EnsureAppointmentSchema(conn);
        string sql = @"UPDATE Appointments SET CustomerID=@CustID, CustomerName=@CustName, ServiceID=@SvcID, ServiceName=@SvcName,
                        BarberID=@BarbID, BarberName=@BarbName, ScheduledAt=@When, Notes=@Notes
                       WHERE AppointmentID=@ID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CustID", appointment.CustomerId);
        cmd.Parameters.AddWithValue("@CustName", appointment.CustomerName);
        cmd.Parameters.AddWithValue("@SvcID", appointment.ServiceId > 0 ? appointment.ServiceId : DBNull.Value);
        cmd.Parameters.AddWithValue("@SvcName", appointment.ServiceName);
        cmd.Parameters.AddWithValue("@BarbID", appointment.BarberId > 0 ? appointment.BarberId : DBNull.Value);
        cmd.Parameters.AddWithValue("@BarbName", appointment.BarberName);
        cmd.Parameters.AddWithValue("@When", appointment.ScheduledAt);
        cmd.Parameters.AddWithValue("@Notes", appointment.Notes ?? "");
        cmd.Parameters.AddWithValue("@ID", appointment.Id);
        cmd.ExecuteNonQuery();
    }

    public void UpdateAppointmentStatus(int appointmentId, AppointmentStatus status)
    {
        EnsureNotSuperAdmin();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        EnsureAppointmentSchema(conn);
        string sql = "UPDATE Appointments SET Status = @Status WHERE AppointmentID = @ID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Status", status.ToString());
        cmd.Parameters.AddWithValue("@ID", appointmentId);
        cmd.ExecuteNonQuery();
    }

    public string GenerateAppointmentNumber()
    {
        return $"APT-{DateTime.Now:yyyyMMdd}-{Random.Shared.Next(1000, 9999)}";
    }

    /// <summary>
    /// Checks an appointment in: creates a Waiting queue transaction for it and marks the
    /// appointment CheckedIn. Both writes commit or roll back together.
    /// </summary>
    public Transaction CheckInAppointment(Appointment appointment, User staff)
    {
        EnsureNotSuperAdmin();
        decimal price = GetServices().Find(s => s.Id == appointment.ServiceId)?.BasePrice ?? 0m;
        var txn = new Transaction
        {
            TransactionNumber = GenerateTransactionNumber(),
            CustomerId = appointment.CustomerId,
            CustomerName = appointment.CustomerName,
            ServiceId = appointment.ServiceId,
            ServiceName = appointment.ServiceName,
            BarberId = appointment.BarberId,
            BarberName = appointment.BarberName,
            StaffId = staff.Id,
            StaffName = staff.FullName,
            Subtotal = price,
            FinalAmount = price,
            Status = TransactionStatus.Waiting,
            TransactionDate = DateTime.Now
        };

        using var conn = TenantConnectionFactory.GetConnection();
        conn.Open();
        EnsureAppointmentSchema(conn);
        using var dbTxn = conn.BeginTransaction();
        try
        {
            EnsureLoyaltySchema(conn, dbTxn);
            SaveTransactionCore(txn, conn, dbTxn);
            using var cmd = new SqlCommand("UPDATE Appointments SET Status = 'CheckedIn' WHERE AppointmentID = @ID", conn, dbTxn);
            cmd.Parameters.AddWithValue("@ID", appointment.Id);
            cmd.ExecuteNonQuery();
            dbTxn.Commit();
        }
        catch
        {
            dbTxn.Rollback();
            throw;
        }
        appointment.Status = AppointmentStatus.CheckedIn;
        return txn;
    }
}
