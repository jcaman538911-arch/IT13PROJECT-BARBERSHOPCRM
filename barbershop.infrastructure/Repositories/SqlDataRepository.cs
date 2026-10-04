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

    private static T GetOrDefault<T>(SqlDataReader reader, string column, T fallback)
        => reader.IsDBNull(reader.GetOrdinal(column)) ? fallback : reader.GetFieldValue<T>(reader.GetOrdinal(column));

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
                    conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        string sql = "SELECT UserID, Username, PasswordHash, Role, FullName, AccountStatus, EmployeeID, BranchID, CreatedAt FROM Users WHERE AccountStatus = 'ACTIVE' ORDER BY UserID DESC";
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        string sql = "UPDATE Users SET AccountStatus = 'INACTIVE' WHERE UserID = @UserID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@UserID", id);
        cmd.ExecuteNonQuery();
    }

    // --- Employees ---
    public List<Employee> GetEmployees(bool includeInactive = false)
    {
        EnsureNotSuperAdmin();
        var list = new List<Employee>();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.OpenWithRetry();
        string statusFilter = includeInactive ? "" : "WHERE Status = 'ACTIVE'";
        string sql = $"SELECT EmployeeID, FirstName, LastName, ContactNumber, EmployeeType, Status, BranchID FROM Employees {statusFilter} ORDER BY EmployeeID DESC";
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        string sql = "UPDATE Employees SET Status = 'INACTIVE' WHERE EmployeeID = @EmployeeID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@EmployeeID", id);
        cmd.ExecuteNonQuery();
    }

    // --- Customers ---
    public List<Customer> GetCustomers(bool includeInactive = false)
    {
        EnsureNotSuperAdmin();
        return DbRetry.Execute(() =>
        {
            var list = new List<Customer>();
            using var conn = TenantConnectionFactory.GetConnection();
            conn.OpenWithRetry();
            string statusFilter = includeInactive ? "" : "WHERE Status = 'ACTIVE'";
            string sql = $"SELECT CustomerID, FirstName, LastName, PhoneNumber, Email, Birthday, IsLoyaltyMember, LoyaltyPoints, Status, CreatedAt FROM Customers {statusFilter} ORDER BY CustomerID DESC";
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 30;
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
        });
    }

    public Customer? GetCustomerById(int id)
    {
        return GetCustomers(includeInactive: true).Find(c => c.Id == id);
    }

    public List<Customer> SearchCustomers(string query, bool includeInactive = false)
    {
        if (string.IsNullOrWhiteSpace(query)) return GetCustomers(includeInactive);
        query = query.ToLower();
        return GetCustomers(includeInactive).FindAll(c =>
            c.FullName.StartsWith(query, StringComparison.OrdinalIgnoreCase) ||
            c.PhoneNumber.StartsWith(query) ||
            c.Email.StartsWith(query, StringComparison.OrdinalIgnoreCase) ||
            c.Id.ToString() == query ||
            $"uc-{c.Id:d6}" == query);
    }

    public void AddCustomer(Customer customer)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        string sql = "UPDATE Customers SET Status = 'INACTIVE' WHERE CustomerID = @CustomerID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CustomerID", id);
        cmd.ExecuteNonQuery();
    }

    // --- Services & Base Pricing ---
    public List<ServiceItem> GetServices()
    {
        return DbRetry.Execute(() =>
        {
            var list = new List<ServiceItem>();
            using var conn = TenantConnectionFactory.GetConnection();
            conn.OpenWithRetry();
            string sql = "SELECT ServiceID, ServiceName, Description, BasePrice, Status FROM Services WHERE Status = 'ACTIVE' ORDER BY ServiceID DESC";
            using var cmd = new SqlCommand(sql, conn);
            cmd.CommandTimeout = 30;
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
        });
    }

    public void AddService(ServiceItem service)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
         conn.OpenWithRetry();
        string sql = "SELECT PromotionID, Title, Description, DiscountType, DiscountValue, StartDate, EndDate, EligibilityRule, Status FROM Promotions WHERE Status = 'ACTIVE' ORDER BY PromotionID DESC";
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        string sql = "SELECT RewardID, RewardName, RequiredPoints, DiscountAmount, Status FROM LoyaltyRewards WHERE Status = 'ACTIVE' ORDER BY RewardID DESC";
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        string sql = @"
            SELECT 
                a.AttendanceID, 
                a.EmployeeID, 
                COALESCE(LTRIM(RTRIM(e.FirstName + ' ' + e.LastName)), a.EmployeeName) AS EmployeeName, 
                a.AttendanceDate, 
                a.TimeIn, 
                a.TimeOut, 
                a.Status, 
                a.Notes 
            FROM Attendance a
            LEFT JOIN Employees e ON a.EmployeeID = e.EmployeeID
            ORDER BY a.AttendanceDate DESC, a.AttendanceID DESC";
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

    public List<AttendanceRecord> GetTodayAttendance(DateTime? date = null)
    {
        var targetDate = date?.Date ?? DateTime.Today;
        return GetAttendanceRecords().FindAll(a => a.Date.Date == targetDate);
    }

    public void RecordAttendance(AttendanceRecord record)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.OpenWithRetry();
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
        try
        {
            using var conn = TenantConnectionFactory.GetOpenConnection();
            string sql = @"SELECT t.TransactionID, t.TransactionNumber, t.CustomerID, 
                                  COALESCE(LTRIM(RTRIM(c.FirstName + ' ' + c.LastName)), t.CustomerName) AS CustomerName, 
                                  t.StaffID, COALESCE(LTRIM(RTRIM(es.FirstName + ' ' + es.LastName)), t.StaffName) AS StaffName, 
                                  t.BarberID, COALESCE(LTRIM(RTRIM(eb.FirstName + ' ' + eb.LastName)), t.BarberName) AS BarberName, 
                                  t.ServiceID, t.ServiceName, t.Subtotal, t.DiscountAmount, t.FinalAmount, 
                                  t.PaymentMethod, t.Status, t.PromotionID, t.LoyaltyRewardID, t.PointsEarned, t.PointsRedeemed, 
                                  t.AmountReceived, t.ChangeAmount, t.TransactionDate 
                           FROM Transactions t
                           LEFT JOIN Customers c ON t.CustomerID = c.CustomerID
                           LEFT JOIN Employees es ON t.StaffID = es.EmployeeID
                           LEFT JOIN Employees eb ON t.BarberID = eb.EmployeeID
                           ORDER BY t.TransactionDate DESC";
            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Enum.TryParse<PaymentMethod>(GetOrDefault(reader, "PaymentMethod", ""), true, out var pm);
                Enum.TryParse<TransactionStatus>(GetOrDefault(reader, "Status", ""), true, out var st);

                list.Add(new Transaction
                {
                    Id = reader.GetInt32(reader.GetOrdinal("TransactionID")),
                    TransactionNumber = GetOrDefault(reader, "TransactionNumber", ""),
                    CustomerId = GetOrDefault<int?>(reader, "CustomerID", null),
                    CustomerName = GetOrDefault(reader, "CustomerName", "Walk-in Customer"),
                    StaffId = GetOrDefault(reader, "StaffID", 0),
                    StaffName = GetOrDefault(reader, "StaffName", ""),
                    BarberId = GetOrDefault(reader, "BarberID", 0),
                    BarberName = GetOrDefault(reader, "BarberName", ""),
                    ServiceId = GetOrDefault(reader, "ServiceID", 1),
                    ServiceName = GetOrDefault(reader, "ServiceName", ""),
                    Subtotal = GetOrDefault(reader, "Subtotal", 0m),
                    DiscountAmount = GetOrDefault(reader, "DiscountAmount", 0m),
                    FinalAmount = GetOrDefault(reader, "FinalAmount", 0m),
                    PaymentMethod = pm,
                    Status = st,
                    PromotionId = GetOrDefault<int?>(reader, "PromotionID", null),
                    LoyaltyRewardId = GetOrDefault<int?>(reader, "LoyaltyRewardID", null),
                    PointsEarned = GetOrDefault(reader, "PointsEarned", 0),
                    PointsRedeemed = GetOrDefault(reader, "PointsRedeemed", 0),
                    AmountReceived = GetOrDefault(reader, "AmountReceived", 0m),
                    ChangeAmount = GetOrDefault(reader, "ChangeAmount", 0m),
                    TransactionDate = GetOrDefault(reader, "TransactionDate", DateTime.Now)
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[SqlDataRepository] GetTransactions warning: {ex.Message}");
        }
        return list;
    }

    public List<Transaction> GetTodayTransactions(DateTime? date = null)
    {
        var targetDate = date?.Date ?? DateTime.Today;
        return GetTransactions().FindAll(t => t.TransactionDate.Date == targetDate);
    }

    public List<Transaction> GetCustomerTransactions(int customerId)
    {
        return GetTransactions().FindAll(t => t.CustomerId == customerId);
    }

    public string GenerateTransactionNumber()
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.OpenWithRetry();
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
    private void EnsureLoyaltySchema(SqlConnection conn)
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
                using var cmd = new SqlCommand(sql, conn);
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
        // Run one-time schema migrations on a SEPARATE connection so that any
        // error or implicit rollback from the DDL statement cannot close the
        // connection we are about to use for the real transaction below.
        try
        {
            using var schemaConn = TenantConnectionFactory.GetConnection();
            schemaConn.OpenWithRetry();
            EnsureLoyaltySchema(schemaConn);
            using var altCmd = new SqlCommand(
                "ALTER TABLE Transactions ALTER COLUMN StaffID INT NULL", schemaConn);
            altCmd.ExecuteNonQuery();
        }
        catch { /* Ignore — already applied or DDL not supported on this schema version */ }

        // Open a fresh connection for the actual transaction work.
        using var conn = TenantConnectionFactory.GetConnection();
        conn.OpenWithRetry();

        using var dbTxn = conn.BeginTransaction();
        try
        {
            SaveTransactionCore(txn, conn, dbTxn);
            dbTxn.Commit();
        }
        catch
        {
            try { dbTxn.Rollback(); } catch { /* connection may already be dead */ }
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
            cmd.Parameters.AddWithValue("@StaffID", txn.StaffId > 0 ? (object)txn.StaffId : DBNull.Value);
            cmd.Parameters.AddWithValue("@StaffName", txn.StaffName ?? "Staff");
            cmd.Parameters.AddWithValue("@BarberID", txn.BarberId > 0 ? (object)txn.BarberId : DBNull.Value);
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
                           SET Status        = @Status,
                               PaymentMethod = @PM,
                               AmountReceived= @AmtRec, 
                               ChangeAmount  = @ChangeAmt,
                               FinalAmount   = @Final,
                               DiscountAmount= @Discount, 
                               PointsEarned  = @PtsEarned,
                               PointsRedeemed= @PtsRedeemed,
                               CustomerID    = @CustID,
                               CustomerName  = @CustName,
                               BarberID      = @BarberID,
                               BarberName    = @BarberName,
                               ServiceID     = @SvcID,
                               ServiceName   = @SvcName,
                               PromotionID   = @PromoID,
                               LoyaltyRewardID = @RewardID
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
            cmd.Parameters.AddWithValue("@CustID", (object?)txn.CustomerId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CustName", txn.CustomerName ?? "Walk-in Customer");
            cmd.Parameters.AddWithValue("@BarberID", txn.BarberId > 0 ? (object)txn.BarberId : DBNull.Value);
            cmd.Parameters.AddWithValue("@BarberName", txn.BarberName ?? "Barber");
            cmd.Parameters.AddWithValue("@SvcID", (object?)txn.ServiceId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@SvcName", txn.ServiceName ?? "");
            cmd.Parameters.AddWithValue("@PromoID", (object?)txn.PromotionId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@RewardID", (object?)txn.LoyaltyRewardId ?? DBNull.Value);
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        string sql = "DELETE FROM InventoryItems WHERE InventoryItemID = @ItemID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ItemID", id);
        cmd.ExecuteNonQuery();
    }

    public void RecordStockTransaction(InventoryTransaction txn)
    {
        using var conn = TenantConnectionFactory.GetConnection();
        conn.OpenWithRetry();

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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        string sql = "SELECT SupplierID, SupplierName, ContactInformation, Status, CreatedAt, UpdatedAt FROM Suppliers WHERE Status = 'ACTIVE' ORDER BY SupplierID DESC";
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        string sql = "SELECT BranchID, BranchName, Address, ContactInformation, Status, CreatedAt, UpdatedAt FROM Branches WHERE Status = 'ACTIVE' ORDER BY BranchID DESC";
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        string sql = "UPDATE Branches SET Status = 'INACTIVE' WHERE BranchID = @BranchID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BranchID", id);
        cmd.ExecuteNonQuery();
    }

    // --- System Maintenance & Support ---
    public List<SystemLog> GetSystemLogs()
    {
        var list = new List<SystemLog>();
        try
        {
            using var conn = TenantConnectionFactory.GetMasterOpenConnection();
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
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[GetSystemLogs] Warning: {ex.Message}");
        }
        return list;
    }

    private static readonly System.Collections.Concurrent.ConcurrentQueue<SystemLog> _offlineLogs = new();

    public void AddSystemLog(string level, string module, string message, string actionBy)
    {
        var log = new SystemLog
        {
            Timestamp = DateTime.Now,
            LogLevel = level ?? "INFO",
            Module = module ?? "System",
            Message = message ?? "",
            ActionBy = actionBy ?? "System"
        };

        _offlineLogs.Enqueue(log);
        FlushOfflineLogs();
    }

    private int _isFlushingLogs = 0;

    private void FlushOfflineLogs()
    {
        if (_offlineLogs.IsEmpty) return;
        if (System.Threading.Interlocked.CompareExchange(ref _isFlushingLogs, 1, 0) != 0) return;

        System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                using var conn = TenantConnectionFactory.GetMasterOpenConnection();
                while (_offlineLogs.TryPeek(out var log))
                {
                    string sql = @"INSERT INTO SystemLogs (Timestamp, LogLevel, Module, Message, ActionBy)
                                   VALUES (@Timestamp, @Level, @Module, @Message, @ActionBy)";
                    using var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Timestamp", log.Timestamp);
                    cmd.Parameters.AddWithValue("@Level", log.LogLevel);
                    cmd.Parameters.AddWithValue("@Module", log.Module);
                    cmd.Parameters.AddWithValue("@Message", log.Message);
                    cmd.Parameters.AddWithValue("@ActionBy", log.ActionBy);
                    cmd.ExecuteNonQuery();

                    _offlineLogs.TryDequeue(out _); // Remove from queue only if successful
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FlushOfflineLogs] Could not reach DB, keeping {_offlineLogs.Count} logs in offline queue. Error: {ex.Message}");
            }
            finally
            {
                System.Threading.Interlocked.Exchange(ref _isFlushingLogs, 0);
                if (!_offlineLogs.IsEmpty)
                {
                    FlushOfflineLogs(); // trigger again if more were added while flushing
                }
            }
        });
    }

    public List<SupportRequest> GetSupportRequests()
    {
        var list = new List<SupportRequest>();
        try
        {
            using var conn = TenantConnectionFactory.GetMasterOpenConnection();
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
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[GetSupportRequests] Warning: {ex.Message}");
        }
        return list;
    }

    public void AddSupportRequest(SupportRequest req)
    {
        using var conn = TenantConnectionFactory.GetMasterOpenConnection();
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
        using var conn = TenantConnectionFactory.GetMasterOpenConnection();
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
                BEGIN
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
                    );
                END
                ELSE
                BEGIN
                    -- Upgrade a table created by an older build: add any missing
                    -- columns and backfill from legacy column names where present.
                    IF COL_LENGTH('Appointments','AppointmentNumber') IS NULL
                        ALTER TABLE Appointments ADD AppointmentNumber NVARCHAR(50) NULL;
                    IF COL_LENGTH('Appointments','CustomerID') IS NULL
                        ALTER TABLE Appointments ADD CustomerID INT NULL;
                    IF COL_LENGTH('Appointments','CustomerName') IS NULL
                        ALTER TABLE Appointments ADD CustomerName NVARCHAR(100) NULL;
                    IF COL_LENGTH('Appointments','ServiceID') IS NULL
                        ALTER TABLE Appointments ADD ServiceID INT NULL;
                    IF COL_LENGTH('Appointments','ServiceName') IS NULL
                        ALTER TABLE Appointments ADD ServiceName NVARCHAR(100) NULL;
                    IF COL_LENGTH('Appointments','BarberID') IS NULL
                        ALTER TABLE Appointments ADD BarberID INT NULL;
                    IF COL_LENGTH('Appointments','BarberName') IS NULL
                        ALTER TABLE Appointments ADD BarberName NVARCHAR(100) NULL;
                    IF COL_LENGTH('Appointments','ScheduledAt') IS NULL
                        ALTER TABLE Appointments ADD ScheduledAt DATETIME NULL;
                    IF COL_LENGTH('Appointments','Status') IS NULL
                        ALTER TABLE Appointments ADD Status NVARCHAR(20) NULL;
                    IF COL_LENGTH('Appointments','Notes') IS NULL
                        ALTER TABLE Appointments ADD Notes NVARCHAR(255) NULL;
                    IF COL_LENGTH('Appointments','CreatedDate') IS NULL
                        ALTER TABLE Appointments ADD CreatedDate DATETIME NULL;
                    IF COL_LENGTH('Appointments','AppointmentDate') IS NOT NULL
                        EXEC('UPDATE Appointments SET ScheduledAt = AppointmentDate WHERE ScheduledAt IS NULL');
                    IF COL_LENGTH('Appointments','CreatedAt') IS NOT NULL
                        EXEC('UPDATE Appointments SET CreatedDate = CreatedAt WHERE CreatedDate IS NULL');
                    EXEC('UPDATE Appointments SET AppointmentNumber = ''APT-'' + CAST(AppointmentID AS NVARCHAR(10)) WHERE AppointmentNumber IS NULL');
                    EXEC('UPDATE Appointments SET ScheduledAt = GETDATE() WHERE ScheduledAt IS NULL');
                    EXEC('UPDATE Appointments SET CreatedDate = GETDATE() WHERE CreatedDate IS NULL');
                END";
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
        conn.OpenWithRetry();
        EnsureAppointmentSchema(conn);
        string sql = @"SELECT a.AppointmentID, a.AppointmentNumber, a.CustomerID, 
                              COALESCE(LTRIM(RTRIM(c.FirstName + ' ' + c.LastName)), a.CustomerName) AS CustomerName, 
                              a.ServiceID, a.ServiceName, a.BarberID, 
                              COALESCE(LTRIM(RTRIM(e.FirstName + ' ' + e.LastName)), a.BarberName) AS BarberName, 
                              a.ScheduledAt, a.Status, a.Notes, a.CreatedDate
                       FROM Appointments a
                       LEFT JOIN Customers c ON a.CustomerID = c.CustomerID
                       LEFT JOIN Employees e ON a.BarberID = e.EmployeeID
                       ORDER BY a.ScheduledAt DESC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Appointment
            {
                Id = reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                AppointmentNumber = GetOrDefault(reader, "AppointmentNumber", ""),
                CustomerId = GetOrDefault(reader, "CustomerID", 0),
                CustomerName = GetOrDefault(reader, "CustomerName", ""),
                ServiceId = GetOrDefault(reader, "ServiceID", 0),
                ServiceName = GetOrDefault(reader, "ServiceName", ""),
                BarberId = GetOrDefault(reader, "BarberID", 0),
                BarberName = GetOrDefault(reader, "BarberName", ""),
                ScheduledAt = GetOrDefault(reader, "ScheduledAt", DateTime.Now),
                Status = Enum.TryParse(GetOrDefault(reader, "Status", ""), out AppointmentStatus st) ? st : AppointmentStatus.Scheduled,
                Notes = GetOrDefault(reader, "Notes", ""),
                CreatedDate = GetOrDefault(reader, "CreatedDate", DateTime.Now)
            });
        }
        return list;
    }

    public void AddAppointment(Appointment appointment)
    {
        EnsureNotSuperAdmin();
        using var conn = TenantConnectionFactory.GetConnection();
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
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
        conn.OpenWithRetry();
        EnsureAppointmentSchema(conn);
        try { EnsureLoyaltySchema(conn); } catch { }
        using var dbTxn = conn.BeginTransaction();
        try
        {
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

    // ==================== SUBSCRIPTION MANAGEMENT (SuperAdmin / Master DB) ====================

    public List<TenantSubscription> GetTenantSubscriptions()
    {
        var list = new List<TenantSubscription>();
        try
        {
            using var conn = TenantConnectionFactory.GetMasterOpenConnection();
            string sql = @"SELECT SubscriptionID, TenantID, CompanyName, DatabaseName, PlanName, MonthlyFee,
                                  StartDate, ExpiryDate, Status, PaymentStatus, PaymentMethod, LastPaidDate, Notes
                           FROM TenantSubscriptions ORDER BY TenantID";
            using var cmd = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new TenantSubscription
                {
                    SubscriptionID  = reader.GetInt32(reader.GetOrdinal("SubscriptionID")),
                    TenantID        = reader.GetInt32(reader.GetOrdinal("TenantID")),
                    CompanyName     = reader.GetString(reader.GetOrdinal("CompanyName")),
                    DatabaseName    = reader.GetString(reader.GetOrdinal("DatabaseName")),
                    PlanName        = reader.GetString(reader.GetOrdinal("PlanName")),
                    MonthlyFee      = reader.GetDecimal(reader.GetOrdinal("MonthlyFee")),
                    StartDate       = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                    ExpiryDate      = reader.GetDateTime(reader.GetOrdinal("ExpiryDate")),
                    Status          = reader.GetString(reader.GetOrdinal("Status")),
                    PaymentStatus   = reader.GetString(reader.GetOrdinal("PaymentStatus")),
                    PaymentMethod   = reader.GetString(reader.GetOrdinal("PaymentMethod")),
                    LastPaidDate    = reader.IsDBNull(reader.GetOrdinal("LastPaidDate")) ? null : reader.GetDateTime(reader.GetOrdinal("LastPaidDate")),
                    Notes           = reader.IsDBNull(reader.GetOrdinal("Notes")) ? "" : reader.GetString(reader.GetOrdinal("Notes"))
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[GetTenantSubscriptions] Warning: {ex.Message}");
        }
        return list;
    }

    /// <summary>
    /// SuperAdmin sets a tenant's subscription status to Active, Suspended, or Expired.
    /// When Suspended/Expired, the tenant's login will be blocked at the login screen.
    /// </summary>
    public void UpdateSubscriptionStatus(int tenantId, string status, string notes)
    {
        try
        {
            using var conn = TenantConnectionFactory.GetMasterOpenConnection();
            string sql = @"UPDATE TenantSubscriptions 
                           SET Status = @Status, Notes = @Notes
                           WHERE TenantID = @TenantID";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Notes", notes ?? "");
            cmd.Parameters.AddWithValue("@TenantID", tenantId);
            cmd.ExecuteNonQuery();

            AddSystemLog("WARN", "Subscriptions",
                $"Tenant {tenantId} subscription status changed to '{status}'. Notes: {notes}",
                "superadmin");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[UpdateSubscriptionStatus] Warning: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// SuperAdmin renews a tenant's subscription after receiving payment.
    /// Extends ExpiryDate, marks PaymentStatus = Paid, and records payment date.
    /// </summary>
    public void RenewSubscription(int tenantId, string planName, decimal monthlyFee, int monthsToAdd, string paymentMethod)
    {
        try
        {
            using var conn = TenantConnectionFactory.GetMasterOpenConnection();
            string sql = @"UPDATE TenantSubscriptions 
                           SET PlanName       = @PlanName,
                               MonthlyFee     = @MonthlyFee,
                               ExpiryDate     = DATEADD(month, @Months, 
                                                  CASE WHEN ExpiryDate < GETDATE() THEN GETDATE() ELSE ExpiryDate END),
                               Status         = 'Active',
                               PaymentStatus  = 'Paid',
                               PaymentMethod  = @PaymentMethod,
                               LastPaidDate   = GETDATE(),
                               Notes          = 'Renewed by SuperAdmin on ' + CONVERT(NVARCHAR,GETDATE(),107)
                           WHERE TenantID = @TenantID";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@PlanName", planName);
            cmd.Parameters.AddWithValue("@MonthlyFee", monthlyFee);
            cmd.Parameters.AddWithValue("@Months", monthsToAdd);
            cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
            cmd.Parameters.AddWithValue("@TenantID", tenantId);
            cmd.ExecuteNonQuery();

            AddSystemLog("INFO", "Subscriptions",
                $"Tenant {tenantId} subscription renewed — Plan: {planName}, Fee: ₱{monthlyFee:N2}, +{monthsToAdd} month(s), via {paymentMethod}.",
                "superadmin");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[RenewSubscription] Warning: {ex.Message}");
            throw;
        }
    }
    public int AddNewTenant(string companyName, string dbName, string dbPassword, string planName, decimal monthlyFee)
    {
        using var conn = TenantConnectionFactory.GetMasterOpenConnection();
        using var trans = conn.BeginTransaction();
        try
        {
            string serverStr = $"{dbName}.public.databaseasp.net";
            
            // 1. Add to Registry
            string sqlReg = "INSERT INTO TenantRegistry (TenantName, DatabaseName, ConnectionServer, Status) OUTPUT INSERTED.TenantID VALUES (@Name, @DB, @Server, 'ACTIVE')";
            using var cmdReg = new SqlCommand(sqlReg, conn, trans);
            cmdReg.Parameters.AddWithValue("@Name", companyName);
            cmdReg.Parameters.AddWithValue("@DB", dbName);
            cmdReg.Parameters.AddWithValue("@Server", serverStr);
            int newTenantId = (int)cmdReg.ExecuteScalar();

            // 2. Add to Subscriptions
            string sqlSub = @"INSERT INTO TenantSubscriptions (TenantID, CompanyName, DatabaseName, PlanName, MonthlyFee, Status, ExpiryDate, LastPaidDate) 
                              VALUES (@TID, @Name, @DB, @Plan, @Fee, 'Active', DATEADD(month, 1, GETDATE()), GETDATE())";
            using var cmdSub = new SqlCommand(sqlSub, conn, trans);
            cmdSub.Parameters.AddWithValue("@TID", newTenantId);
            cmdSub.Parameters.AddWithValue("@Name", companyName);
            cmdSub.Parameters.AddWithValue("@DB", dbName);
            cmdSub.Parameters.AddWithValue("@Plan", planName);
            cmdSub.Parameters.AddWithValue("@Fee", monthlyFee);
            cmdSub.ExecuteNonQuery();

            trans.Commit();
            AddSystemLog("INFO", "Tenants", $"Created new tenant ID {newTenantId} ({companyName}) on DB {dbName}", "superadmin");
            
            // 3. Register connection string dynamically
            string connStr = $"Server={dbName}.public.databaseasp.net; Database={dbName}; User Id={dbName}; Password={dbPassword}; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=15;";
            TenantConnectionFactory.RegisterTenantConnection(newTenantId, connStr);

            // 4. Force schema initialization (creates tables on the MonsterASP database)
            using var newTenantConn = TenantConnectionFactory.GetOpenConnection(newTenantId);

            return newTenantId;
        }
        catch
        {
            trans.Rollback();
            throw;
        }
    }
}
