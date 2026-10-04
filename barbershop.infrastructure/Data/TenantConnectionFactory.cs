using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;
using barbershop.domain;

namespace barbershop.infrastructure;

public static class SqlConnectionExtensions
{
    // Transport-level SQL error numbers that mean the pooled connection is dead.
    private static readonly System.Collections.Generic.HashSet<int> _transportErrors
        = new() { 19, 20, 233, -2, 10053, 10054, 10060, 64 };

    /// <summary>
    /// Opens the connection with retry logic.
    /// On transport-level errors the broken connection pool is cleared before the next attempt
    /// so SQL Client won't hand back the same dead socket again.
    /// NOTE: Do NOT call this from the UI thread for long-running queries — use Task.Run() instead.
    /// </summary>
    public static void OpenWithRetry(this SqlConnection conn, int maxRetries = 3, int delayMs = 500)
    {
        int attempts = 0;
        while (true)
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
                conn.Open();
                return;
            }
            catch (SqlException ex)
            {
                attempts++;
                // Clear the pool if the connection is physically broken so the next
                // attempt gets a fresh socket instead of the same dead one.
                if (_transportErrors.Contains(ex.Number))
                    SqlConnection.ClearPool(conn);

                if (attempts >= maxRetries) throw;
                System.Threading.Thread.Sleep(delayMs);
            }
        }
    }

    /// <summary>
    /// Async-safe version of OpenWithRetry. Use this on background threads / Task.Run blocks.
    /// </summary>
    public static async System.Threading.Tasks.Task OpenWithRetryAsync(
        this SqlConnection conn, int maxRetries = 3, int delayMs = 500)
    {
        int attempts = 0;
        while (true)
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
                await conn.OpenAsync();
                return;
            }
            catch (SqlException ex)
            {
                attempts++;
                if (_transportErrors.Contains(ex.Number))
                    SqlConnection.ClearPool(conn);

                if (attempts >= maxRetries) throw;
                await System.Threading.Tasks.Task.Delay(delayMs);
            }
        }
    }
}

/// <summary>
/// Wraps any DB operation so that transport-level errors (error 19 / physical
/// connection not usable) cause the connection pool to be cleared and the
/// entire operation to be retried with a fresh connection.
/// Usage:  var list = DbRetry.Execute(() => { ... return list; });
/// </summary>
public static class DbRetry
{
    private static readonly System.Collections.Generic.HashSet<int> _transportErrors
        = new() { 19, 20, 233, -2, 10053, 10054, 10060, 64 };

    public static T Execute<T>(Func<T> operation, int maxRetries = 3, int delayMs = 500)
    {
        int attempts = 0;
        while (true)
        {
            try
            {
                return operation();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                if (!_transportErrors.Contains(ex.Number) || attempts >= maxRetries)
                {
                    throw; // Not a transport error, or max retries reached. Let it crash.
                }

                attempts++;
                // Wipe every broken socket in the pool so the next attempt
                // gets a fresh physical connection instead of the dead one.
                Microsoft.Data.SqlClient.SqlConnection.ClearAllPools();
                System.Threading.Thread.Sleep(delayMs);
            }
        }
    }

    public static async System.Threading.Tasks.Task<T> ExecuteAsync<T>(
        Func<System.Threading.Tasks.Task<T>> operation, int maxRetries = 3, int delayMs = 500)
    {
        int attempts = 0;
        while (true)
        {
            try
            {
                return await operation();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                if (!_transportErrors.Contains(ex.Number) || attempts >= maxRetries)
                {
                    throw;
                }

                attempts++;
                Microsoft.Data.SqlClient.SqlConnection.ClearAllPools();
                await System.Threading.Tasks.Task.Delay(delayMs);
            }
        }
    }
}

public static class TenantConnectionFactory
{
    private static readonly Dictionary<int, string> _tenantConnectionStrings = new()
    {
        { 1, @"Server=db68508.public.databaseasp.net; Database=db68508; User Id=db68508; Password=caman314031; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=60;" },
        { 2, @"Server=db68525.public.databaseasp.net; Database=db68525; User Id=db68525; Password=caman314032; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=60;" },
        { 3, @"Server=db68526.public.databaseasp.net; Database=db68526; User Id=db68526; Password=caman314033; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=60;" }
    };

    // Cloud Master DB — dedicated to SuperAdmin: stores SystemLogs, SupportRequests, SystemUsers, TenantRegistry
    private static readonly string _masterCloudConnectionString = @"Server=db70240.public.databaseasp.net; Database=db70240; User Id=db70240; Password=Fr8=m9+N5#Gb; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=60;";

    private static readonly string _localConnectionString = @"Server=(localdb)\MSSQLLocalDB; Database=BarberShopCRM_Local; Integrated Security=True; Encrypt=False; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=5;";

    private static bool _masterCloudAvailable = true;

    private static readonly HashSet<int> _failedTenants = new();
    private static readonly HashSet<string> _initializedConnections = new();
    private static readonly object _lock = new();

    public static string GetConnectionString(int? tenantId = null)
    {
        int targetTenant = tenantId ?? TenantContext.CurrentTenantId ?? 1;

        lock (_lock)
        {
            if (!_failedTenants.Contains(targetTenant) && _tenantConnectionStrings.TryGetValue(targetTenant, out var connStr))
            {
                return connStr;
            }
        }

        return _localConnectionString;
    }

    public static void RegisterTenantConnection(int tenantId, string connectionString)
    {
        lock (_lock)
        {
            _tenantConnectionStrings[tenantId] = connectionString;
            _failedTenants.Remove(tenantId);
        }
    }

    public static void MarkTenantFailed(int? tenantId = null)
    {
        int targetTenant = tenantId ?? TenantContext.CurrentTenantId ?? 1;
        lock (_lock)
        {
            _failedTenants.Add(targetTenant);
        }
    }

    public static SqlConnection GetConnection(int? tenantId = null)
    {
        int targetTenant = tenantId ?? TenantContext.CurrentTenantId ?? 1;
        string connStr = GetConnectionString(targetTenant);
        
        // Return a connection instance. If opening it fails, caller or repo can mark it failed.
        return new SqlConnection(connStr);
    }

    public static SqlConnection GetOpenConnection(int? tenantId = null)
    {
        int targetTenant = tenantId ?? TenantContext.CurrentTenantId ?? 1;
        string connStr = GetConnectionString(targetTenant);

        var conn = new SqlConnection(connStr);
        try
        {
            conn.OpenWithRetry();            // clears pool on transport errors automatically
            EnsureTablesExistOnConnection(conn, targetTenant);
            return conn;
        }
        catch (Exception ex)
        {
            // Clear pool so next caller gets a fresh socket, not the same dead one.
            SqlConnection.ClearPool(conn);
            conn.Dispose();

            if (connStr != _localConnectionString)
            {
                MarkTenantFailed(targetTenant);
                System.Diagnostics.Debug.WriteLine(
                    $"[TenantConnectionFactory] Remote DB failed for tenant {targetTenant}. " +
                    $"Falling back to LocalDB. Details: {ex.Message}");

                var localConn = new SqlConnection(_localConnectionString);
                localConn.Open();
                EnsureTablesExistOnConnection(localConn, targetTenant);
                return localConn;
            }
            throw;
        }
    }

    /// <summary>
    /// Creates a SqlCommand with a 30-second timeout so no query blocks the app indefinitely.
    /// Use this instead of new SqlCommand(...) throughout the repository.
    /// </summary>
    public static SqlCommand CreateTimedCommand(string sql, SqlConnection conn, SqlTransaction? txn = null)
    {
        var cmd = txn != null
            ? new SqlCommand(sql, conn, txn)
            : new SqlCommand(sql, conn);
        cmd.CommandTimeout = 30;   // 30-second hard limit per query
        return cmd;
    }

    private static void EnsureTablesExistOnConnection(SqlConnection conn, int targetTenant)
    {
        string key = $"{conn.Database}_{targetTenant}";
        lock (_lock)
        {
            if (_initializedConnections.Contains(key)) return;

            try
            {
                EnsureCoreTablesCreated(conn);

            string script = LoadDatabaseScript();
            if (!string.IsNullOrWhiteSpace(script))
            {
                ExecuteSqlScriptOnConnection(conn, script);
            }

            SeedCompanyData(conn, targetTenant);

                _initializedConnections.Add(key);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EnsureTablesExist warning: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Connection string for the Cloud Master DB (db70240) used exclusively by SuperAdmin.
    /// Falls back to LocalDB if the cloud Master DB is unreachable.
    /// </summary>
    public static string MasterConnectionString => _masterCloudAvailable ? _masterCloudConnectionString : _localConnectionString;

    public static SqlConnection GetMasterConnection()
    {
        return new SqlConnection(MasterConnectionString);
    }

    /// <summary>
    /// Opens and returns a connection to the Cloud Master DB.
    /// Ensures all required Master DB tables exist, then returns the open connection.
    /// Falls back to LocalDB if the cloud is unreachable.
    /// </summary>
    public static SqlConnection GetMasterOpenConnection()
    {
        string connStr = _masterCloudAvailable ? _masterCloudConnectionString : _localConnectionString;
        var conn = new SqlConnection(connStr);
        try
        {
            conn.Open();
            EnsureMasterTablesExist(conn);
            return conn;
        }
        catch (Exception ex)
        {
            conn.Dispose();
            if (_masterCloudAvailable && connStr == _masterCloudConnectionString)
            {
                lock (_lock) { _masterCloudAvailable = false; }
                System.Diagnostics.Debug.WriteLine($"[TenantConnectionFactory] Master Cloud DB unreachable, falling back to LocalDB. Details: {ex.Message}");
                var localConn = new SqlConnection(_localConnectionString);
                localConn.Open();
                EnsureMasterTablesExist(localConn);
                return localConn;
            }
            throw;
        }
    }

    /// <summary>
    /// Initializes the Master DB schema (SystemLogs, SupportRequests, SystemUsers, TenantRegistry) on startup.
    /// </summary>
    public static void InitializeMasterDatabase()
    {
        System.Threading.Tasks.Task.Run(() =>
        {
            try
            {
                using var conn = GetMasterOpenConnection();
                System.Diagnostics.Debug.WriteLine("[MasterDB] Master database initialized successfully.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[MasterDB] Master database initialization warning: {ex.Message}");
            }
        });
    }

    private static readonly HashSet<string> _masterInitialized = new();

    private static void EnsureMasterTablesExist(SqlConnection conn)
    {
        string key = conn.Database;
        lock (_lock)
        {
            if (_masterInitialized.Contains(key)) return;

            try
            {
                string ddl = @"
                -- Tenant Registry: maps TenantId -> DB info
            IF OBJECT_ID('dbo.TenantRegistry', 'U') IS NULL
            BEGIN
                CREATE TABLE TenantRegistry (
                    TenantID INT IDENTITY(1,1) PRIMARY KEY,
                    TenantName NVARCHAR(100) NOT NULL,
                    DatabaseName NVARCHAR(100) NOT NULL,
                    ConnectionServer NVARCHAR(200) NOT NULL,
                    Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
                    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
                );
                INSERT INTO TenantRegistry (TenantName, DatabaseName, ConnectionServer, Status)
                VALUES
                    ('Company 1 - Uppercut Barber Shop', 'db68508', 'db68508.public.databaseasp.net', 'ACTIVE'),
                    ('Company 2 - Uppercut Barber Shop', 'db68525', 'db68525.public.databaseasp.net', 'ACTIVE'),
                    ('Company 3 - Uppercut Barber Shop', 'db68526', 'db68526.public.databaseasp.net', 'ACTIVE');
            END;

            -- System Users table (SuperAdmin-managed accounts)
            IF OBJECT_ID('dbo.Users', 'U') IS NULL
            BEGIN
                CREATE TABLE Users (
                    UserID INT IDENTITY(1,1) PRIMARY KEY,
                    Username NVARCHAR(50) NOT NULL UNIQUE,
                    PasswordHash NVARCHAR(255) NOT NULL,
                    Role NVARCHAR(20) NOT NULL,
                    FullName NVARCHAR(100) NOT NULL,
                    AccountStatus NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
                    EmployeeID INT NULL,
                    BranchID INT NULL,
                    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
                );
                -- Seed the SuperAdmin account
                INSERT INTO Users (Username, PasswordHash, Role, FullName, AccountStatus)
                VALUES ('superadmin', 'admin123', 'SuperAdmin', 'Super Administrator', 'ACTIVE');
            END;

            -- Global System Audit Logs
            IF OBJECT_ID('dbo.SystemLogs', 'U') IS NULL
            BEGIN
                CREATE TABLE SystemLogs (
                    LogID INT IDENTITY(1,1) PRIMARY KEY,
                    Timestamp DATETIME NOT NULL DEFAULT GETDATE(),
                    LogLevel NVARCHAR(20) NOT NULL DEFAULT 'INFO',
                    Module NVARCHAR(50) NOT NULL,
                    Message NVARCHAR(MAX) NOT NULL,
                    ActionBy NVARCHAR(100) NOT NULL
                );
            END;

            -- Technical Support Requests
            IF OBJECT_ID('dbo.SupportRequests', 'U') IS NULL
            BEGIN
                CREATE TABLE SupportRequests (
                    SupportID INT IDENTITY(1,1) PRIMARY KEY,
                    TicketNumber NVARCHAR(50) NOT NULL UNIQUE,
                    RequestedBy NVARCHAR(100) NOT NULL,
                    Subject NVARCHAR(200) NOT NULL,
                    Details NVARCHAR(MAX) NULL,
                    Priority NVARCHAR(20) NOT NULL DEFAULT 'Medium',
                    Status NVARCHAR(20) NOT NULL DEFAULT 'Open',
                    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
                );
            END;

            -- Tenant Subscriptions: SuperAdmin manages subscriptions & access control
            IF OBJECT_ID('dbo.TenantSubscriptions', 'U') IS NULL
            BEGIN
                CREATE TABLE TenantSubscriptions (
                    SubscriptionID INT IDENTITY(1,1) PRIMARY KEY,
                    TenantID INT NOT NULL UNIQUE,
                    CompanyName NVARCHAR(100) NOT NULL,
                    DatabaseName NVARCHAR(50) NOT NULL,
                    PlanName NVARCHAR(50) NOT NULL DEFAULT 'Basic',
                    MonthlyFee DECIMAL(18,2) NOT NULL DEFAULT 999.00,
                    StartDate DATETIME NOT NULL DEFAULT GETDATE(),
                    ExpiryDate DATETIME NOT NULL DEFAULT DATEADD(month, 1, GETDATE()),
                    Status NVARCHAR(20) NOT NULL DEFAULT 'Active',
                    PaymentStatus NVARCHAR(20) NOT NULL DEFAULT 'Paid',
                    PaymentMethod NVARCHAR(50) NOT NULL DEFAULT 'GCash',
                    LastPaidDate DATETIME NULL,
                    Notes NVARCHAR(500) NULL
                );
                -- Seed subscriptions for the 3 demo tenants
                INSERT INTO TenantSubscriptions (TenantID, CompanyName, DatabaseName, PlanName, MonthlyFee, StartDate, ExpiryDate, Status, PaymentStatus, PaymentMethod, LastPaidDate)
                VALUES
                    (1, 'Company 1 - Uppercut Barber Shop', 'db68508', 'Standard', 1499.00, DATEADD(month,-1,GETDATE()), DATEADD(month,1,GETDATE()), 'Active', 'Paid', 'GCash', GETDATE()),
                    (2, 'Company 2 - Uppercut Barber Shop', 'db68525', 'Basic',    999.00,  DATEADD(month,-1,GETDATE()), DATEADD(month,1,GETDATE()), 'Active', 'Paid', 'GCash', GETDATE()),
                    (3, 'Company 3 - Uppercut Barber Shop', 'db68526', 'Premium',  1999.00, DATEADD(month,-1,GETDATE()), DATEADD(month,1,GETDATE()), 'Active', 'Paid', 'GCash', GETDATE());
            END;
            ";

            using var cmd = conn.CreateCommand();
            cmd.CommandText = ddl;
            cmd.ExecuteNonQuery();

            _masterInitialized.Add(key);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[EnsureMasterTablesExist] Warning: {ex.Message}");
        }
    }
    }

    /// <summary>
    /// Checks the Master DB to see if a given tenant's subscription is Active.
    /// Called at login to enforce subscription access control.
    /// Returns true if active or if Master DB is unreachable (fail-open for demo resilience).
    /// </summary>
    public static (bool IsActive, string Status, DateTime ExpiryDate) CheckTenantSubscription(int tenantId)
    {
        try
        {
            using var conn = new SqlConnection(_masterCloudAvailable ? _masterCloudConnectionString : _localConnectionString);
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Status, ExpiryDate FROM TenantSubscriptions WHERE TenantID = @TenantID";
            cmd.Parameters.AddWithValue("@TenantID", tenantId);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string status = reader.GetString(0);
                DateTime expiry = reader.GetDateTime(1);
                bool active = status == "Active" && expiry >= DateTime.Today;
                return (active, status, expiry);
            }
            // No subscription record found — default to active (fail-open)
            return (true, "Active", DateTime.Today.AddMonths(1));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[CheckTenantSubscription] Warning (fail-open): {ex.Message}");
            return (true, "Active", DateTime.Today.AddMonths(1)); // Fail-open if DB unreachable
        }
    }

    /// <summary>
    /// Synchronizes offline LocalDB transactions up to the remote Cloud tenant database when online.
    /// </summary>
    public static int SyncLocalToCloud(int? tenantId = null)
    {
        int targetTenant = tenantId ?? TenantContext.CurrentTenantId ?? 1;
        if (!_tenantConnectionStrings.TryGetValue(targetTenant, out var remoteStr))
        {
            return 0;
        }

        int syncedRecords = 0;
        try
        {
            using var remoteConn = new SqlConnection(remoteStr);
            remoteConn.Open();

            // Remote Cloud DB is reachable! Remove from failed tenants list
            lock (_lock)
            {
                _failedTenants.Remove(targetTenant);
            }

            using var localConn = new SqlConnection(_localConnectionString);
            localConn.Open();

            using var checkTableCmd = localConn.CreateCommand();
            checkTableCmd.CommandText = "SELECT COUNT(*) FROM sys.tables WHERE name = 'Transactions'";
            if (Convert.ToInt32(checkTableCmd.ExecuteScalar()) == 0) return 0;

            // Fetch LocalDB transactions
            using var getLocalTxnsCmd = localConn.CreateCommand();
            getLocalTxnsCmd.CommandText = "SELECT TransactionNumber, CustomerName, StaffID, StaffName, BarberID, BarberName, ServiceName, Subtotal, DiscountAmount, FinalAmount, PaymentMethod, Status, PointsEarned, PointsRedeemed, AmountReceived, ChangeAmount, TransactionDate FROM Transactions";
            using var reader = getLocalTxnsCmd.ExecuteReader();

            var localTxns = new List<(string TxnNum, string CustName, int StaffId, string StaffName, int BarberId, string BarberName, string ServiceName, decimal Subtotal, decimal Discount, decimal FinalAmt, string Payment, string Status, int PtsEarned, int PtsRedeemed, decimal AmtRec, decimal ChangeAmt, DateTime TxnDate)>();

            while (reader.Read())
            {
                localTxns.Add((
                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    reader.GetString(3),
                    reader.GetInt32(4),
                    reader.GetString(5),
                    reader.GetString(6),
                    reader.GetDecimal(7),
                    reader.GetDecimal(8),
                    reader.GetDecimal(9),
                    reader.GetString(10),
                    reader.GetString(11),
                    reader.GetInt32(12),
                    reader.GetInt32(13),
                    reader.GetDecimal(14),
                    reader.GetDecimal(15),
                    reader.GetDateTime(16)
                ));
            }
            reader.Close();

            foreach (var t in localTxns)
            {
                using var checkRemoteCmd = remoteConn.CreateCommand();
                checkRemoteCmd.CommandText = "SELECT COUNT(*) FROM Transactions WHERE TransactionNumber = @Num";
                checkRemoteCmd.Parameters.AddWithValue("@Num", t.TxnNum);
                int count = Convert.ToInt32(checkRemoteCmd.ExecuteScalar());

                if (count == 0)
                {
                    using var insertCmd = remoteConn.CreateCommand();
                    insertCmd.CommandText = @"INSERT INTO Transactions 
                        (TransactionNumber, CustomerName, StaffID, StaffName, BarberID, BarberName, ServiceName, Subtotal, DiscountAmount, FinalAmount, PaymentMethod, Status, PointsEarned, PointsRedeemed, AmountReceived, ChangeAmount, TransactionDate)
                        VALUES (@Num, @Cust, @StaffId, @StaffName, @BarberId, @BarberName, @Svc, @Sub, @Disc, @Final, @Pay, @Status, @Earned, @Redeemed, @Rec, @Change, @Date)";
                    insertCmd.Parameters.AddWithValue("@Num", t.TxnNum);
                    insertCmd.Parameters.AddWithValue("@Cust", t.CustName);
                    insertCmd.Parameters.AddWithValue("@StaffId", t.StaffId);
                    insertCmd.Parameters.AddWithValue("@StaffName", t.StaffName);
                    insertCmd.Parameters.AddWithValue("@BarberId", t.BarberId);
                    insertCmd.Parameters.AddWithValue("@BarberName", t.BarberName);
                    insertCmd.Parameters.AddWithValue("@Svc", t.ServiceName);
                    insertCmd.Parameters.AddWithValue("@Sub", t.Subtotal);
                    insertCmd.Parameters.AddWithValue("@Disc", t.Discount);
                    insertCmd.Parameters.AddWithValue("@Final", t.FinalAmt);
                    insertCmd.Parameters.AddWithValue("@Pay", t.Payment);
                    insertCmd.Parameters.AddWithValue("@Status", t.Status);
                    insertCmd.Parameters.AddWithValue("@Earned", t.PtsEarned);
                    insertCmd.Parameters.AddWithValue("@Redeemed", t.PtsRedeemed);
                    insertCmd.Parameters.AddWithValue("@Rec", t.AmtRec);
                    insertCmd.Parameters.AddWithValue("@Change", t.ChangeAmt);
                    insertCmd.Parameters.AddWithValue("@Date", t.TxnDate);
                    insertCmd.ExecuteNonQuery();
                    syncedRecords++;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[SyncEngine] Cloud sync warning: {ex.Message}");
        }

        return syncedRecords;
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
                    checkCmd.CommandText = "SELECT COUNT(*) FROM sys.tables WHERE name = 'Transactions'";
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
            checkCmd.CommandText = "SELECT COUNT(*) FROM sys.tables WHERE name = 'Transactions'";
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

    private static void EnsureCoreTablesCreated(SqlConnection conn)
    {
        string ddl = @"
        IF OBJECT_ID('dbo.Branches', 'U') IS NULL
        BEGIN
            CREATE TABLE Branches (
                BranchID INT IDENTITY(1,1) PRIMARY KEY,
                BranchName NVARCHAR(100) NOT NULL,
                Address NVARCHAR(255) NULL,
                ContactInformation NVARCHAR(100) NULL,
                Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
                CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
            );
        END;

        IF OBJECT_ID('dbo.Suppliers', 'U') IS NULL
        BEGIN
            CREATE TABLE Suppliers (
                SupplierID INT IDENTITY(1,1) PRIMARY KEY,
                SupplierName NVARCHAR(100) NOT NULL,
                ContactInformation NVARCHAR(200) NULL,
                Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
                CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
            );
        END;

        IF OBJECT_ID('dbo.Employees', 'U') IS NULL
        BEGIN
            CREATE TABLE Employees (
                EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
                FirstName NVARCHAR(50) NOT NULL,
                LastName NVARCHAR(50) NOT NULL,
                ContactNumber NVARCHAR(50) NULL,
                EmployeeType NVARCHAR(20) NOT NULL,
                Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
                BranchID INT NULL,
                CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
            );
        END;

        IF OBJECT_ID('dbo.Users', 'U') IS NULL
        BEGIN
            CREATE TABLE Users (
                UserID INT IDENTITY(1,1) PRIMARY KEY,
                Username NVARCHAR(50) NOT NULL UNIQUE,
                PasswordHash NVARCHAR(255) NOT NULL,
                Role NVARCHAR(20) NOT NULL,
                FullName NVARCHAR(100) NOT NULL,
                AccountStatus NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
                EmployeeID INT NULL,
                BranchID INT NULL,
                CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
            );
        END;

        IF OBJECT_ID('dbo.Customers', 'U') IS NULL
        BEGIN
            CREATE TABLE Customers (
                CustomerID INT IDENTITY(1,1) PRIMARY KEY,
                FirstName NVARCHAR(50) NOT NULL,
                LastName NVARCHAR(50) NOT NULL,
                PhoneNumber NVARCHAR(50) NULL,
                Email NVARCHAR(100) NULL,
                Birthday DATE NULL,
                IsLoyaltyMember BIT NOT NULL DEFAULT 0,
                LoyaltyPoints INT NOT NULL DEFAULT 0,
                Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
                CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
            );
        END;

        IF OBJECT_ID('dbo.Services', 'U') IS NULL
        BEGIN
            CREATE TABLE Services (
                ServiceID INT IDENTITY(1,1) PRIMARY KEY,
                ServiceName NVARCHAR(100) NOT NULL,
                Description NVARCHAR(255) NULL,
                BasePrice DECIMAL(18,2) NOT NULL DEFAULT 200.00,
                Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
                CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
            );
        END;

        IF OBJECT_ID('dbo.Promotions', 'U') IS NULL
        BEGIN
            CREATE TABLE Promotions (
                PromotionID INT IDENTITY(1,1) PRIMARY KEY,
                Title NVARCHAR(100) NOT NULL,
                Description NVARCHAR(255) NULL,
                DiscountType NVARCHAR(20) NOT NULL DEFAULT 'Percentage',
                DiscountValue DECIMAL(18,2) NOT NULL DEFAULT 0.00,
                StartDate DATETIME NOT NULL DEFAULT GETDATE(),
                EndDate DATETIME NOT NULL DEFAULT DATEADD(day, 30, GETDATE()),
                EligibilityRule NVARCHAR(100) NULL,
                Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
                CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
            );
        END;

        IF OBJECT_ID('dbo.LoyaltyRewards', 'U') IS NULL
        BEGIN
            CREATE TABLE LoyaltyRewards (
                RewardID INT IDENTITY(1,1) PRIMARY KEY,
                RewardName NVARCHAR(100) NOT NULL,
                RequiredPoints INT NOT NULL,
                DiscountAmount DECIMAL(18,2) NOT NULL,
                Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
                CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
            );
        END;

        IF OBJECT_ID('dbo.Transactions', 'U') IS NULL
        BEGIN
            CREATE TABLE Transactions (
                TransactionID INT IDENTITY(1,1) PRIMARY KEY,
                TransactionNumber NVARCHAR(50) NOT NULL UNIQUE,
                CustomerID INT NULL,
                CustomerName NVARCHAR(100) NOT NULL DEFAULT 'Walk-in Customer',
                StaffID INT NULL,
                StaffName NVARCHAR(100) NOT NULL,
                BarberID INT NOT NULL,
                BarberName NVARCHAR(100) NOT NULL,
                ServiceID INT NULL,
                ServiceName NVARCHAR(100) NOT NULL DEFAULT 'Haircut',
                BranchID INT NULL,
                Subtotal DECIMAL(18,2) NOT NULL DEFAULT 200.00,
                DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0.00,
                FinalAmount DECIMAL(18,2) NOT NULL DEFAULT 200.00,
                PaymentMethod NVARCHAR(50) NOT NULL DEFAULT 'Cash',
                Status NVARCHAR(20) NOT NULL DEFAULT 'COMPLETED',
                PromotionID INT NULL,
                LoyaltyRewardID INT NULL,
                PointsEarned INT NOT NULL DEFAULT 0,
                PointsRedeemed INT NOT NULL DEFAULT 0,
                AmountReceived DECIMAL(18,2) NOT NULL DEFAULT 0.00,
                ChangeAmount DECIMAL(18,2) NOT NULL DEFAULT 0.00,
                TransactionDate DATETIME NOT NULL DEFAULT GETDATE()
            );
        END;

        IF OBJECT_ID('dbo.Appointments', 'U') IS NULL
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
        END;

        IF OBJECT_ID('dbo.InventoryItems', 'U') IS NULL
        BEGIN
            CREATE TABLE InventoryItems (
                InventoryItemID INT IDENTITY(1,1) PRIMARY KEY,
                ItemName NVARCHAR(100) NOT NULL,
                Category NVARCHAR(50) NULL DEFAULT 'General',
                Quantity INT NOT NULL DEFAULT 0,
                Unit NVARCHAR(20) NOT NULL DEFAULT 'pcs',
                MinimumStockLevel INT NOT NULL DEFAULT 5,
                SupplierID INT NULL,
                Cost DECIMAL(18,2) NOT NULL DEFAULT 0.00,
                Status NVARCHAR(20) NOT NULL DEFAULT 'IN STOCK',
                BranchID INT NULL,
                CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
            );
        END;

        IF OBJECT_ID('dbo.CustomerConcerns', 'U') IS NULL
        BEGIN
            CREATE TABLE CustomerConcerns (
                ConcernID INT IDENTITY(1,1) PRIMARY KEY,
                TicketNumber NVARCHAR(50) NOT NULL UNIQUE,
                CustomerName NVARCHAR(100) NOT NULL,
                ContactNumber NVARCHAR(50) NULL,
                Category NVARCHAR(50) NOT NULL DEFAULT 'General',
                Subject NVARCHAR(200) NOT NULL,
                Details NVARCHAR(MAX) NULL,
                Priority NVARCHAR(20) NOT NULL DEFAULT 'Medium',
                Status NVARCHAR(20) NOT NULL DEFAULT 'Open',
                ResolutionNotes NVARCHAR(MAX) NULL,
                CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
                ResolvedAt DATETIME NULL
            );
        END;
        ";

        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = ddl;
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"EnsureCoreTablesCreated warning: {ex.Message}");
        }
    }

    private static string LoadDatabaseScript()
    {
        try
        {
            string[] candidates = new[]
            {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UppercutBarberShopCRM_Database.sql"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "UppercutBarberShopCRM_Database.sql"),
                Path.Combine(Directory.GetCurrentDirectory(), "UppercutBarberShopCRM_Database.sql"),
                Path.Combine(Directory.GetCurrentDirectory(), "barbershop.infrastructure", "Data", "UppercutBarberShopCRM_Database.sql")
            };

            foreach (var candidate in candidates)
            {
                if (File.Exists(candidate))
                {
                    return File.ReadAllText(candidate).Replace("USE UppercutBarberShopCRM;", "");
                }
            }

            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            for (int i = 0; i < 5 && dir != null; i++)
            {
                string path1 = Path.Combine(dir.FullName, "UppercutBarberShopCRM_Database.sql");
                if (File.Exists(path1)) return File.ReadAllText(path1).Replace("USE UppercutBarberShopCRM;", "");

                string path2 = Path.Combine(dir.FullName, "barbershop.infrastructure", "Data", "UppercutBarberShopCRM_Database.sql");
                if (File.Exists(path2)) return File.ReadAllText(path2).Replace("USE UppercutBarberShopCRM;", "");

                dir = dir.Parent;
            }
        }
        catch
        {
            // Fallback to empty string
        }
        return string.Empty;
    }

    private static void ExecuteSqlScriptOnConnection(SqlConnection conn, string scriptContent)
    {
        var batches = System.Text.RegularExpressions.Regex.Split(
            scriptContent,
            @"^\s*GO\s*$",
            System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline
        );

        foreach (var batch in batches)
        {
            string trimmedBatch = batch.Trim();
            if (string.IsNullOrWhiteSpace(trimmedBatch)) continue;

            using var cmd = conn.CreateCommand();
            cmd.CommandText = trimmedBatch;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ScriptExecution] Batch warning: {ex.Message}");
            }
        }
    }

    private static void SeedCompanyData(SqlConnection conn, int tenantId)
    {
        try
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = $@"
            IF OBJECT_ID('dbo.Branches', 'U') IS NOT NULL AND NOT EXISTS (SELECT * FROM Branches WHERE BranchName LIKE '%Company {tenantId}%')
                INSERT INTO Branches (BranchName, Address, ContactInformation, Status) 
                VALUES ('Uppercut Barber Shop - Company {tenantId}', 'City Branch {tenantId}', '555-010{tenantId}', 'ACTIVE');

            IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL AND NOT EXISTS (SELECT * FROM Users WHERE Username = 'owner{tenantId}')
                INSERT INTO Users (Username, PasswordHash, Role, FullName, AccountStatus) 
                VALUES ('owner{tenantId}', 'owner123', 'Admin', 'Owner Company {tenantId}', 'ACTIVE');

            IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL AND NOT EXISTS (SELECT * FROM Users WHERE Username = 'staff{tenantId}')
                INSERT INTO Users (Username, PasswordHash, Role, FullName, AccountStatus) 
                VALUES ('staff{tenantId}', 'staff123', 'Staff', 'Staff Cashier Company {tenantId}', 'ACTIVE');

            IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL AND NOT EXISTS (SELECT * FROM Customers WHERE FirstName = 'Customer{tenantId}')
                INSERT INTO Customers (FirstName, LastName, PhoneNumber, Email, IsLoyaltyMember, LoyaltyPoints, Status)
                VALUES ('Customer{tenantId}', 'Sample', '0917-000-000{tenantId}', 'customer{tenantId}@mail.com', 1, 100, 'ACTIVE');

            IF OBJECT_ID('dbo.Services', 'U') IS NOT NULL AND NOT EXISTS (SELECT * FROM Services WHERE ServiceName LIKE '%Company {tenantId}%')
                INSERT INTO Services (ServiceName, Description, BasePrice, Status)
                VALUES ('Signature Cut - Company {tenantId}', 'Specialty haircut for Company {tenantId}', {250 + tenantId * 50}, 'ACTIVE');

            IF OBJECT_ID('dbo.Transactions', 'U') IS NOT NULL AND NOT EXISTS (SELECT * FROM Transactions WHERE TransactionNumber LIKE '%TXN-{tenantId}-%')
                INSERT INTO Transactions (TransactionNumber, CustomerName, StaffID, StaffName, BarberID, BarberName, ServiceName, Subtotal, DiscountAmount, FinalAmount, PaymentMethod, Status, PointsEarned, PointsRedeemed, AmountReceived, ChangeAmount, TransactionDate)
                VALUES ('TXN-{tenantId}-001', 'John Sample', 1, 'Staff Cashier', 1, 'David Barber', 'Signature Cut', 300.00, 0.00, 300.00, 'Cash', 'COMPLETED', 10, 0, 500.00, 200.00, GETDATE());
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
            IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL AND NOT EXISTS (SELECT * FROM Users WHERE Username = 'owner{tenantId}')
                INSERT INTO Users (Username, PasswordHash, Role, FullName, AccountStatus) 
                VALUES ('owner{tenantId}', 'owner123', 'Admin', 'Owner Company {tenantId}', 'ACTIVE');

            IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL AND NOT EXISTS (SELECT * FROM Users WHERE Username = 'staff{tenantId}')
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
