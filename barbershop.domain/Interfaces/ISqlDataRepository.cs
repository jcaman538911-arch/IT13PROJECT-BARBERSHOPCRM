namespace barbershop.domain;

public interface ISqlDataRepository
{
    // Authentication
    User? Authenticate(string username, string password);

    // System Users
    List<User> GetUsers();
    void AddUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int id);

    // Employees & Barbers
    List<Employee> GetEmployees();
    List<Employee> GetBarbers();
    void AddEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    void DeleteEmployee(int id);

    // Customers
    List<Customer> GetCustomers();
    Customer? GetCustomerById(int id);
    List<Customer> SearchCustomers(string query);
    void AddCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
    void DeleteCustomer(int id);

    // Services
    List<ServiceItem> GetServices();
    void AddService(ServiceItem service);
    void UpdateService(ServiceItem service);
    void DeleteService(int id);

    // Promotions
    List<Promotion> GetPromotions();
    List<Promotion> GetActivePromotions();
    void AddPromotion(Promotion promo);
    void UpdatePromotion(Promotion promo);
    void DeletePromotion(int id);

    // Loyalty Rewards
    List<LoyaltyReward> GetLoyaltyRewards();
    void AddLoyaltyReward(LoyaltyReward reward);
    void UpdateLoyaltyReward(LoyaltyReward reward);
    void DeleteLoyaltyReward(int id);

    // Attendance
    List<AttendanceRecord> GetAttendanceRecords();
    List<AttendanceRecord> GetTodayAttendance();
    void RecordAttendance(AttendanceRecord record);

    // Transactions
    List<Transaction> GetTransactions();
    List<Transaction> GetTodayTransactions();
    List<Transaction> GetCustomerTransactions(int customerId);
    string GenerateTransactionNumber();
    void SaveTransaction(Transaction txn);
    List<LoyaltyHistoryEntry> GetLoyaltyHistory(int customerId);
    List<LoyaltyHistoryEntry> GetAllLoyaltyHistory();

    // Appointments
    List<Appointment> GetAppointments();
    void AddAppointment(Appointment appointment);
    void UpdateAppointment(Appointment appointment);
    void UpdateAppointmentStatus(int appointmentId, AppointmentStatus status);
    Transaction CheckInAppointment(Appointment appointment, User staff);
    string GenerateAppointmentNumber();

    // Inventory
    List<InventoryItem> GetInventoryItems();
    void AddInventoryItem(InventoryItem item);
    void UpdateInventoryItem(InventoryItem item);
    void DeleteInventoryItem(int id);
    void RecordStockTransaction(InventoryTransaction txn);
    List<InventoryTransaction> GetInventoryTransactions();

    // Suppliers
    List<Supplier> GetSuppliers();
    void AddSupplier(Supplier supp);
    void UpdateSupplier(Supplier supp);
    void DeleteSupplier(int id);

    // Branches
    List<Branch> GetBranches();
    void AddBranch(Branch branch);
    void UpdateBranch(Branch branch);
    void DeleteBranch(int id);

    // System Logs & Support
    List<SystemLog> GetSystemLogs();
    void AddSystemLog(string level, string module, string message, string actionBy);
    List<SupportRequest> GetSupportRequests();
    void AddSupportRequest(SupportRequest req);
    void UpdateSupportRequestStatus(int id, string status);
}
