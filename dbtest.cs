using System;
using Microsoft.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connStr = "Server=db68508.public.databaseasp.net; Database=db68508; User Id=db68508; Password=caman314031; Encrypt=True; TrustServerCertificate=True; Connect Timeout=15;";
        try {
            using (var conn = new SqlConnection(connStr)) {
                conn.Open();
                Console.WriteLine("SUCCESS! Connected to MonsterASP.");
            }
        }
        catch (Exception ex) {
            Console.WriteLine("FAIL: " + ex.Message);
        }
    }
}
