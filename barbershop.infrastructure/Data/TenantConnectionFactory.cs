using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;
using barbershop.domain;

namespace barbershop.infrastructure;

public static class TenantConnectionFactory
{
    private static readonly Dictionary<int, string> _tenantConnectionStrings = new()
    {
        { 1, @"Server=db68508.public.databaseasp.net; Database=db68508; User Id=db68508; Password=caman314031; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=5;" },
        { 2, @"Server=db68525.public.databaseasp.net; Database=db68525; User Id=db68525; Password=caman314032; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=5;" },
        { 3, @"Server=db68526.public.databaseasp.net; Database=db68526; User Id=db68526; Password=caman314033; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=5;" }
    };

    private static readonly string _localConnectionString = @"Server=(localdb)\MSSQLLocalDB; Database=BarberShopCRM_Local; Integrated Security=True; Encrypt=False; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=3;";

    private static readonly HashSet<int> _failedTenants = new();

    public static string GetConnectionString(int? tenantId = null)
    {
        int targetTenant = tenantId ?? TenantContext.CurrentTenantId ?? 1;

        if (!_failedTenants.Contains(targetTenant) && _tenantConnectionStrings.TryGetValue(targetTenant, out var connStr))
        {
            return connStr;
        }

        return _localConnectionString;
    }

    public static SqlConnection GetConnection(int? tenantId = null)
    {
        string connStr = GetConnectionString(tenantId);
        return new SqlConnection(connStr);
    }

    public static string MasterConnectionString => _localConnectionString;

    public static SqlConnection GetMasterConnection()
    {
        return new SqlConnection(_localConnectionString);
    }

    /// <summary>
    /// Non-blocking initialization of tenant databases.
    /// </summary>
    public static void InitializeAllTenantDatabases()
    {
        System.Threading.Tasks.Task.Run(() =>
        {
            string sqlScript = LoadDatabaseScript();

            foreach (var kvp in _tenantConnectionStrings)
            {
                int tenantId = kvp.Key;
                string connStr = kvp.Value;

                try
                {
                    using var conn = new SqlConnection(connStr);
                    conn.Open();

                    using var checkCmd = conn.CreateCommand();
                    checkCmd.CommandText = "SELECT COUNT(*) FROM sys.tables WHERE name = 'Branches'";
                    int tableCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (tableCount == 0 && !string.IsNullOrWhiteSpace(sqlScript))
                    {
                        ExecuteSqlScriptOnConnection(conn, sqlScript);
                        SeedCompanyData(conn, tenantId);
                    }
                    else if (tableCount > 0)
                    {
                        EnsureCompanySeedUser(conn, tenantId);
                    }
                }
                catch (Exception ex)
                {
                    _failedTenants.Add(tenantId);
                    System.Diagnostics.Debug.WriteLine($"Tenant {tenantId} remote initialization warning: {ex.Message}");
                    EnsureLocalFallbackDatabase(sqlScript, tenantId);
                }
            }
        });
    }

    private static void EnsureLocalFallbackDatabase(string sqlScript, int tenantId)
    {
        try
        {
            // Connect to Master to ensure BarberShopCRM_Local DB exists
            string masterStr = @"Server=(localdb)\MSSQLLocalDB; Database=master; Integrated Security=True; Encrypt=False; TrustServerCertificate=True; Connect Timeout=3;";
            using (var masterConn = new SqlConnection(masterStr))
            {
                masterConn.Open();
                using var createDbCmd = masterConn.CreateCommand();
                createDbCmd.CommandText = "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BarberShopCRM_Local') CREATE DATABASE BarberShopCRM_Local;";
                createDbCmd.ExecuteNonQuery();
            }

            using var localConn = new SqlConnection(_localConnectionString);
            localConn.Open();

            using var checkCmd = localConn.CreateCommand();
            checkCmd.CommandText = "SELECT COUNT(*) FROM sys.tables WHERE name = 'Branches'";
            int tableCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (tableCount == 0 && !string.IsNullOrWhiteSpace(sqlScript))
            {
                ExecuteSqlScriptOnConnection(localConn, sqlScript);
                SeedCompanyData(localConn, tenantId);
            }
            else
            {
                EnsureCompanySeedUser(localConn, tenantId);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"LocalDB Fallback initialization warning: {ex.Message}");
        }
    }

    private static string LoadDatabaseScript()
    {
        try
        {
            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UppercutBarberShopCRM_Database.sql");
            if (!File.Exists(scriptPath))
            {
                scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "UppercutBarberShopCRM_Database.sql");
            }

            if (File.Exists(scriptPath))
            {
                string sqlScript = File.ReadAllText(scriptPath);
                return sqlScript.Replace("USE UppercutBarberShopCRM;", "");
            }
        }
        catch
        {
            // Ignore if file missing
        }
        return string.Empty;
    }

    private static void ExecuteSqlScriptOnConnection(SqlConnection conn, string scriptContent)
    {
        var batches = scriptContent.Split(new[] { "\nGO\r\n", "\nGO\n", "\r\nGO\r\n", "\r\nGO\n" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var batch in batches)
        {
            if (string.IsNullOrWhiteSpace(batch)) continue;
            using var cmd = conn.CreateCommand();
            cmd.CommandText = batch;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                // Ignore batch warning
            }
        }
    }

    private static void SeedCompanyData(SqlConnection conn, int tenantId)
    {
        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = $@"
            IF NOT EXISTS (SELECT * FROM Branches WHERE BranchName LIKE '%Company {tenantId}%')
            INSERT INTO Branches (BranchName, Address, ContactInformation, Status) 
            VALUES ('Uppercut Barber Shop - Company {tenantId}', 'City Branch {tenantId}', '555-010{tenantId}', 'ACTIVE');

            IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'owner{tenantId}')
            INSERT INTO Users (Username, PasswordHash, Role, FullName, AccountStatus) 
            VALUES ('owner{tenantId}', 'owner123', 'Admin', 'Owner Company {tenantId}', 'ACTIVE');

            IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'staff{tenantId}')
            INSERT INTO Users (Username, PasswordHash, Role, FullName, AccountStatus) 
            VALUES ('staff{tenantId}', 'staff123', 'Staff', 'Staff Cashier Company {tenantId}', 'ACTIVE');

            IF NOT EXISTS (SELECT * FROM Customers WHERE FirstName = 'Customer{tenantId}')
            INSERT INTO Customers (FirstName, LastName, PhoneNumber, Email, IsLoyaltyMember, LoyaltyPoints, Status)
            VALUES ('Customer{tenantId}', 'Sample', '0917-000-000{tenantId}', 'customer{tenantId}@mail.com', 1, 100, 'ACTIVE');

            IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName LIKE '%Company {tenantId}%')
            INSERT INTO Services (ServiceName, Description, BasePrice, Status)
            VALUES ('Signature Cut - Company {tenantId}', 'Specialty haircut for Company {tenantId}', {250 + tenantId * 50}, 'ACTIVE');
            ";
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Seed data warning for Tenant {tenantId}: {ex.Message}");
        }
    }

    private static void EnsureCompanySeedUser(SqlConnection conn, int tenantId)
    {
        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = $@"
            IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'owner{tenantId}')
            INSERT INTO Users (Username, PasswordHash, Role, FullName, AccountStatus) 
            VALUES ('owner{tenantId}', 'owner123', 'Admin', 'Owner Company {tenantId}', 'ACTIVE');

            IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'staff{tenantId}')
            INSERT INTO Users (Username, PasswordHash, Role, FullName, AccountStatus) 
            VALUES ('staff{tenantId}', 'staff123', 'Staff', 'Staff Cashier Company {tenantId}', 'ACTIVE');
            ";
            cmd.ExecuteNonQuery();
        }
        catch
        {
            // Ignore if record present
        }
    }
}
