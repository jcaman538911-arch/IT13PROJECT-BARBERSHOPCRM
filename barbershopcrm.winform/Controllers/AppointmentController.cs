using System.Collections.Generic;
using barbershop.domain;
using barbershop.infrastructure;

namespace BarberShopCRM.Controllers;

public class AppointmentController
{
    private static AppointmentController? _instance;
    public static AppointmentController Instance => _instance ??= new AppointmentController();

    public List<Appointment> GetAllAppointments()
    {
        return SqlDataRepository.Instance.GetAppointments();
    }


}
