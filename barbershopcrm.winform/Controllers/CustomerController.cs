using System.Collections.Generic;
using barbershop.domain;
using barbershop.infrastructure;

namespace BarberShopCRM.Controllers;

public class CustomerController
{
    private static CustomerController? _instance;
    public static CustomerController Instance => _instance ??= new CustomerController();

    public List<Customer> GetAllCustomers()
    {
        return SqlDataRepository.Instance.GetCustomers();
    }


}
