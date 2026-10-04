using System;
using System.Collections.Generic;
using System.Linq;
using barbershop.domain;
using barbershop.infrastructure;

namespace BarberShopCRM.Controllers;

public class DashboardController
{
    private static DashboardController? _instance;
    public static DashboardController Instance => _instance ??= new DashboardController();

    public DashboardMetrics GetMetrics(DateTime date)
    {
        var txns = SqlDataRepository.Instance.GetTodayTransactions(date);
        var att = SqlDataRepository.Instance.GetTodayAttendance(date);
        var barbers = SqlDataRepository.Instance.GetBarbers();
        var allCustomers = SqlDataRepository.Instance.GetCustomers();

        return new DashboardMetrics
        {
            CustomersToday = txns.Select(t => t.CustomerName).Distinct().Count(),
            CompletedTransactions = txns.Count(t => t.Status == TransactionStatus.Completed),
            TotalSales = txns.Where(t => t.Status == TransactionStatus.Completed).Sum(t => t.FinalAmount),
            PresentBarbers = att.Count(a => a.Status == AttendanceStatus.Present),
            TotalBarbers = barbers.Count,
            LoyaltyMembers = allCustomers.Count(c => c.IsLoyaltyMember),
            PromotionsUsed = txns.Count(t => t.PromotionId.HasValue),
            RecentTransactions = txns.OrderByDescending(t => t.TransactionDate).ToList()
        };
    }
}

public class DashboardMetrics
{
    public int CustomersToday { get; set; }
    public int CompletedTransactions { get; set; }
    public decimal TotalSales { get; set; }
    public int PresentBarbers { get; set; }
    public int TotalBarbers { get; set; }
    public int LoyaltyMembers { get; set; }
    public int PromotionsUsed { get; set; }
    public List<Transaction> RecentTransactions { get; set; } = new();
}
