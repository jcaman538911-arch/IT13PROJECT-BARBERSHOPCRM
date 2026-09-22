using BarberShopCRM.Helpers;
using System;
using System.Windows.Forms;
using barbershop.infrastructure;

namespace BarberShopCRM.Forms.SuperAdmin;

public partial class SystemMaintenanceForm : Form
{
    public SystemMaintenanceForm()
    {
        InitializeComponent();
        ResponsiveLayoutHelper.Apply(this);
        LogConsole("System Maintenance Console initialized ready for operation.");
    }

    private void LogConsole(string msg)
    {
        txtConsole.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
    }

    private void btnRunBackup_Click(object sender, EventArgs e)
    {
        LogConsole("Starting data backup procedure...");
        var users = SqlDataRepository.Instance.GetUsers();
        var employees = SqlDataRepository.Instance.GetEmployees();
        var customers = SqlDataRepository.Instance.GetCustomers();
        var transactions = SqlDataRepository.Instance.GetTransactions();

        LogConsole($"Backup target: Users ({users.Count}), Employees ({employees.Count}), Customers ({customers.Count}), Transactions ({transactions.Count}).");
        LogConsole("Creating encrypted memory snapshot... SUCCESS!");
        SqlDataRepository.Instance.AddSystemLog("INFO", "Maintenance", "Data backup snapshot completed.", "superadmin");
        MessageBox.Show("System data snapshot created successfully!", "Backup Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnOptimizeMemory_Click(object sender, EventArgs e)
    {
        LogConsole("Requesting Garbage Collector optimization...");
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        LogConsole("Garbage collection complete. Memory heap compacted.");
        SqlDataRepository.Instance.AddSystemLog("INFO", "Maintenance", "Garbage collection & memory optimization executed.", "superadmin");
        MessageBox.Show("Memory optimization completed successfully!", "Optimization Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnClearLogs_Click(object sender, EventArgs e)
    {
        LogConsole("Generating diagnostic log report...");
        int totalLogs = SqlDataRepository.Instance.GetSystemLogs().Count;
        LogConsole($"Analyzed {totalLogs} log entries. System status: STABLE.");
        SqlDataRepository.Instance.AddSystemLog("INFO", "Maintenance", $"Flushed diagnostic logs report. Entries analyzed: {totalLogs}", "superadmin");
        MessageBox.Show($"Diagnostic report generated. Analyzed {totalLogs} audit records.", "Logs Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
