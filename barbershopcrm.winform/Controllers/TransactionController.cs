using System;
using System.Collections.Generic;
using barbershop.domain;
using barbershop.infrastructure;

namespace BarberShopCRM.Controllers;

public class TransactionController
{
    private static TransactionController? _instance;
    public static TransactionController Instance => _instance ??= new TransactionController();

    public List<Transaction> GetAllTransactions()
    {
        return SqlDataRepository.Instance.GetTransactions();
    }

    public void SaveTransaction(Transaction transaction)
    {
        SqlDataRepository.Instance.SaveTransaction(transaction);
    }
}
