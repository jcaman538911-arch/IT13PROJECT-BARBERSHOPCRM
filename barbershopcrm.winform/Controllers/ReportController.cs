using System;
using System.Collections.Generic;
using barbershop.domain;
using barbershop.infrastructure;

namespace BarberShopCRM.Controllers;

public class ReportController
{
    private static ReportController? _instance;
    public static ReportController Instance => _instance ??= new ReportController();

    public List<Transaction> GenerateDateRangeReport(DateTime startDate, DateTime endDate)
    {
        var allTxns = SqlDataRepository.Instance.GetTransactions();
        return allTxns.FindAll(t => t.TransactionDate.Date >= startDate.Date && t.TransactionDate.Date <= endDate.Date);
    }
    
    public List<SystemLog> GetSystemLogs()
    {
        return SqlDataRepository.Instance.GetSystemLogs();
    }
}
