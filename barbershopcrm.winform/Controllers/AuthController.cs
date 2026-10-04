using System;
using System.Linq;
using barbershop.domain;
using barbershop.infrastructure;

namespace BarberShopCRM.Controllers;

public class AuthController
{
    private static AuthController? _instance;
    public static AuthController Instance => _instance ??= new AuthController();

    public User? Authenticate(string username, string password)
    {
        var users = SqlDataRepository.Instance.GetUsers();
        var user = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (user != null && user.Password == password)
        {
            return user;
        }
        return null;
    }
}
