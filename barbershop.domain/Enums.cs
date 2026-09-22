namespace barbershop.domain;

public enum UserRole
{
    SuperAdmin,
    Admin,
    Staff
}

public enum EmployeePosition
{
    Barber,
    Staff
}

public enum AttendanceStatus
{
    Present,
    Absent,
    Late,
    Leave
}

public enum PaymentMethod
{
    Cash,
    GCash,
    BankTransfer
}

public enum TransactionStatus
{
    InService,
    Completed,
    Cancelled,
    Waiting,
    Called
}

public enum AppointmentStatus
{
    Scheduled,
    CheckedIn,
    Completed,
    Cancelled
}
