using System;
using Microsoft.Data.SqlClient;

namespace barbershop.infrastructure;

public static class DbHelper
{

    public static string ConnectionString => TenantConnectionFactory.GetConnectionString();

    public static SqlConnection GetConnection()
    {
        return TenantConnectionFactory.GetConnection();
    }

    public static void InitializeDatabase()
    {
        try
        {
            TenantConnectionFactory.InitializeAllTenantDatabases();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Database Initialization Exception: {ex.Message}");
        }
    }
}
