using System.Collections.Generic;
using barbershop.domain;
using barbershop.infrastructure;

namespace BarberShopCRM.Controllers;

public class EmployeeController
{
    private static EmployeeController? _instance;
    public static EmployeeController Instance => _instance ??= new EmployeeController();

    public List<Employee> GetAllEmployees()
    {
        return SqlDataRepository.Instance.GetBarbers();
    }



    public List<AttendanceRecord> GetAttendanceRecords()
    {
        return SqlDataRepository.Instance.GetAttendanceRecords();
    }

    public void RecordAttendance(AttendanceRecord record)
    {
        SqlDataRepository.Instance.RecordAttendance(record);
    }
}
