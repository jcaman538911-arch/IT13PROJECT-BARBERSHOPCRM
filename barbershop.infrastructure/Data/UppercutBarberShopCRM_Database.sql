-- ============================================================
-- UPPERCUT BARBER SHOP CRM - DATABASE DDL & SEED SCRIPT
-- TECHNOLOGY: MICROSOFT SQL SERVER / SSMS
-- DATABASE NAME: UppercutBarberShopCRM
-- ============================================================

-- (Database creation skipped for MonsterASP)
-- (USE statement skipped)

-- 1. BRANCHES TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Branches')
BEGIN
    CREATE TABLE Branches (
        BranchID INT IDENTITY(1,1) PRIMARY KEY,
        BranchName NVARCHAR(100) NOT NULL,
        Address NVARCHAR(255) NULL,
        ContactInformation NVARCHAR(100) NULL,
        Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 2. SUPPLIERS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Suppliers')
BEGIN
    CREATE TABLE Suppliers (
        SupplierID INT IDENTITY(1,1) PRIMARY KEY,
        SupplierName NVARCHAR(100) NOT NULL,
        ContactInformation NVARCHAR(200) NULL,
        Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 3. EMPLOYEES TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
    CREATE TABLE Employees (
        EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        ContactNumber NVARCHAR(50) NULL,
        EmployeeType NVARCHAR(20) NOT NULL, -- Barber, Staff, Admin
        Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
        BranchID INT NULL FOREIGN KEY REFERENCES Branches(BranchID),
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 4. USERS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        PasswordHash NVARCHAR(255) NOT NULL,
        Role NVARCHAR(20) NOT NULL, -- SuperAdmin, Admin, Staff
        FullName NVARCHAR(100) NOT NULL,
        AccountStatus NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
        EmployeeID INT NULL FOREIGN KEY REFERENCES Employees(EmployeeID),
        BranchID INT NULL FOREIGN KEY REFERENCES Branches(BranchID),
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 5. CUSTOMERS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customers')
BEGIN
    CREATE TABLE Customers (
        CustomerID INT IDENTITY(1,1) PRIMARY KEY,
        FirstName NVARCHAR(50) NOT NULL,
        LastName NVARCHAR(50) NOT NULL,
        PhoneNumber NVARCHAR(50) NULL,
        Email NVARCHAR(100) NULL,
        Birthday DATE NULL,
        IsLoyaltyMember BIT NOT NULL DEFAULT 0,
        LoyaltyPoints INT NOT NULL DEFAULT 0,
        Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 6. SERVICES TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Services')
BEGIN
    CREATE TABLE Services (
        ServiceID INT IDENTITY(1,1) PRIMARY KEY,
        ServiceName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(255) NULL,
        BasePrice DECIMAL(18,2) NOT NULL DEFAULT 200.00,
        Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 7. BARBERS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Barbers')
BEGIN
    CREATE TABLE Barbers (
        BarberID INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeID INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Employees(EmployeeID),
        BranchID INT NULL FOREIGN KEY REFERENCES Branches(BranchID),
        Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE'
    );
END
GO

-- 8. PROMOTIONS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Promotions')
BEGIN
    CREATE TABLE Promotions (
        PromotionID INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(100) NOT NULL,
        Description NVARCHAR(255) NULL,
        DiscountType NVARCHAR(20) NOT NULL DEFAULT 'Percentage', -- Percentage, FixedAmount
        DiscountValue DECIMAL(18,2) NOT NULL DEFAULT 0.00,
        StartDate DATETIME NOT NULL DEFAULT GETDATE(),
        EndDate DATETIME NOT NULL DEFAULT DATEADD(day, 30, GETDATE()),
        EligibilityRule NVARCHAR(100) NULL,
        Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 9. LOYALTY REWARDS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LoyaltyRewards')
BEGIN
    CREATE TABLE LoyaltyRewards (
        RewardID INT IDENTITY(1,1) PRIMARY KEY,
        RewardName NVARCHAR(100) NOT NULL,
        RequiredPoints INT NOT NULL,
        DiscountAmount DECIMAL(18,2) NOT NULL,
        Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 10. LOYALTY MEMBERS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LoyaltyMembers')
BEGIN
    CREATE TABLE LoyaltyMembers (
        LoyaltyMemberID INT IDENTITY(1,1) PRIMARY KEY,
        CustomerID INT NOT NULL UNIQUE FOREIGN KEY REFERENCES Customers(CustomerID),
        CurrentPoints INT NOT NULL DEFAULT 0,
        DateRegistered DATETIME NOT NULL DEFAULT GETDATE(),
        Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE'
    );
END
GO

-- 11. TRANSACTIONS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Transactions')
BEGIN
    CREATE TABLE Transactions (
        TransactionID INT IDENTITY(1,1) PRIMARY KEY,
        TransactionNumber NVARCHAR(50) NOT NULL UNIQUE,
        CustomerID INT NULL FOREIGN KEY REFERENCES Customers(CustomerID),
        CustomerName NVARCHAR(100) NOT NULL DEFAULT 'Walk-in Customer',
        StaffID INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeID),
        StaffName NVARCHAR(100) NOT NULL,
        BarberID INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeID),
        BarberName NVARCHAR(100) NOT NULL,
        ServiceID INT NULL FOREIGN KEY REFERENCES Services(ServiceID),
        ServiceName NVARCHAR(100) NOT NULL DEFAULT 'Haircut',
        BranchID INT NULL FOREIGN KEY REFERENCES Branches(BranchID),
        Subtotal DECIMAL(18,2) NOT NULL DEFAULT 200.00,
        DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0.00,
        FinalAmount DECIMAL(18,2) NOT NULL DEFAULT 200.00,
        PaymentMethod NVARCHAR(50) NOT NULL DEFAULT 'Cash',
        Status NVARCHAR(20) NOT NULL DEFAULT 'COMPLETED',
        PromotionID INT NULL FOREIGN KEY REFERENCES Promotions(PromotionID),
        LoyaltyRewardID INT NULL FOREIGN KEY REFERENCES LoyaltyRewards(RewardID),
        PointsEarned INT NOT NULL DEFAULT 0,
        PointsRedeemed INT NOT NULL DEFAULT 0,
        AmountReceived DECIMAL(18,2) NOT NULL DEFAULT 0.00,
        ChangeAmount DECIMAL(18,2) NOT NULL DEFAULT 0.00,
        TransactionDate DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 12. TRANSACTION DETAILS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TransactionDetails')
BEGIN
    CREATE TABLE TransactionDetails (
        TransactionDetailID INT IDENTITY(1,1) PRIMARY KEY,
        TransactionID INT NOT NULL FOREIGN KEY REFERENCES Transactions(TransactionID) ON DELETE CASCADE,
        ServiceID INT NULL FOREIGN KEY REFERENCES Services(ServiceID),
        ServiceName NVARCHAR(100) NOT NULL,
        Quantity INT NOT NULL DEFAULT 1,
        UnitPrice DECIMAL(18,2) NOT NULL,
        Subtotal DECIMAL(18,2) NOT NULL
    );
END
GO

-- 13. LOYALTY TRANSACTIONS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LoyaltyTransactions')
BEGIN
    CREATE TABLE LoyaltyTransactions (
        LoyaltyTransactionID INT IDENTITY(1,1) PRIMARY KEY,
        CustomerID INT NOT NULL FOREIGN KEY REFERENCES Customers(CustomerID),
        TransactionID INT NULL FOREIGN KEY REFERENCES Transactions(TransactionID),
        PointsEarned INT NOT NULL DEFAULT 0,
        PointsRedeemed INT NOT NULL DEFAULT 0,
        ActivityType NVARCHAR(20) NOT NULL DEFAULT 'EARNED', -- EARNED, REDEEMED, ADJUSTED
        PreviousBalance INT NOT NULL DEFAULT 0,
        NewBalance INT NOT NULL DEFAULT 0,
        Description NVARCHAR(255) NULL,
        DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
        RecordedBy NVARCHAR(100) NULL
    );
END
GO

-- One loyalty record per transaction: prevents duplicate point awards.
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UX_LoyaltyTransactions_TransactionID')
BEGIN
    CREATE UNIQUE NONCLUSTERED INDEX UX_LoyaltyTransactions_TransactionID
        ON LoyaltyTransactions(TransactionID) WHERE TransactionID IS NOT NULL;
END
GO

-- 14. ATTENDANCE TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Attendance')
BEGIN
    CREATE TABLE Attendance (
        AttendanceID INT IDENTITY(1,1) PRIMARY KEY,
        EmployeeID INT NOT NULL FOREIGN KEY REFERENCES Employees(EmployeeID),
        EmployeeName NVARCHAR(100) NOT NULL,
        AttendanceDate DATE NOT NULL,
        TimeIn TIME NOT NULL,
        TimeOut TIME NULL,
        Status NVARCHAR(20) NOT NULL DEFAULT 'PRESENT',
        Notes NVARCHAR(255) NULL,
        RecordedBy NVARCHAR(100) NULL
    );
END
GO

-- 15. INVENTORY ITEMS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'InventoryItems')
BEGIN
    CREATE TABLE InventoryItems (
        InventoryItemID INT IDENTITY(1,1) PRIMARY KEY,
        ItemName NVARCHAR(100) NOT NULL,
        Category NVARCHAR(50) NULL DEFAULT 'General',
        Quantity INT NOT NULL DEFAULT 0,
        Unit NVARCHAR(20) NOT NULL DEFAULT 'pcs',
        MinimumStockLevel INT NOT NULL DEFAULT 5,
        SupplierID INT NULL FOREIGN KEY REFERENCES Suppliers(SupplierID),
        Cost DECIMAL(18,2) NOT NULL DEFAULT 0.00,
        Status NVARCHAR(20) NOT NULL DEFAULT 'IN STOCK', -- IN STOCK, LOW STOCK, OUT OF STOCK
        BranchID INT NULL FOREIGN KEY REFERENCES Branches(BranchID),
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 16. INVENTORY TRANSACTIONS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'InventoryTransactions')
BEGIN
    CREATE TABLE InventoryTransactions (
        InventoryTransactionID INT IDENTITY(1,1) PRIMARY KEY,
        InventoryItemID INT NOT NULL FOREIGN KEY REFERENCES InventoryItems(InventoryItemID) ON DELETE CASCADE,
        TransactionType NVARCHAR(50) NOT NULL, -- STOCK IN, STOCK OUT, USED SUPPLY, RESTOCK
        Quantity INT NOT NULL,
        DateCreated DATETIME NOT NULL DEFAULT GETDATE(),
        RecordedBy NVARCHAR(100) NULL,
        Notes NVARCHAR(255) NULL
    );
END
GO

-- 17. BRANCH USERS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'BranchUsers')
BEGIN
    CREATE TABLE BranchUsers (
        BranchUserID INT IDENTITY(1,1) PRIMARY KEY,
        BranchID INT NOT NULL FOREIGN KEY REFERENCES Branches(BranchID),
        UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
        Role NVARCHAR(50) NULL,
        Status NVARCHAR(20) NOT NULL DEFAULT 'ACTIVE'
    );
END
GO

-- 18. SYSTEM LOGS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SystemLogs')
BEGIN
    CREATE TABLE SystemLogs (
        LogID INT IDENTITY(1,1) PRIMARY KEY,
        Timestamp DATETIME NOT NULL DEFAULT GETDATE(),
        LogLevel NVARCHAR(20) NOT NULL DEFAULT 'INFO',
        Module NVARCHAR(50) NOT NULL DEFAULT 'System',
        Message NVARCHAR(MAX) NOT NULL,
        ActionBy NVARCHAR(100) NOT NULL DEFAULT 'System'
    );
END
GO

-- 19. SUPPORT REQUESTS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SupportRequests')
BEGIN
    CREATE TABLE SupportRequests (
        SupportID INT IDENTITY(1,1) PRIMARY KEY,
        TicketNumber NVARCHAR(50) NOT NULL UNIQUE,
        RequestedBy NVARCHAR(100) NOT NULL,
        Subject NVARCHAR(200) NOT NULL,
        Details NVARCHAR(MAX) NULL,
        Priority NVARCHAR(20) NOT NULL DEFAULT 'Medium',
        Status NVARCHAR(20) NOT NULL DEFAULT 'Open',
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 20. APPOINTMENTS TABLE
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Appointments')
BEGIN
    CREATE TABLE Appointments (
        AppointmentID INT IDENTITY(1,1) PRIMARY KEY,
        AppointmentNumber NVARCHAR(50) NOT NULL,
        CustomerID INT NOT NULL,
        CustomerName NVARCHAR(100) NOT NULL,
        ServiceID INT NULL,
        ServiceName NVARCHAR(100) NOT NULL,
        BarberID INT NULL,
        BarberName NVARCHAR(100) NOT NULL,
        ScheduledAt DATETIME NOT NULL,
        Status NVARCHAR(20) NOT NULL DEFAULT 'Scheduled',
        Notes NVARCHAR(255) NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO

-- ============================================================
-- INITIAL REFERENCE & SEED DATA
-- ============================================================

-- 1. SEED DEFAULT BRANCH
IF NOT EXISTS (SELECT * FROM Branches WHERE BranchName = 'Uppercut Main Branch')
BEGIN
    INSERT INTO Branches (BranchName, Address, ContactInformation, Status)
    VALUES ('Uppercut Main Branch', '123 Main Street, Downtown', '0917-123-4567', 'ACTIVE');
END
GO

-- 2. SEED DEFAULT SUPPLIER
IF NOT EXISTS (SELECT * FROM Suppliers WHERE SupplierName = 'Barber Supplies Co.')
BEGIN
    INSERT INTO Suppliers (SupplierName, ContactInformation, Status)
    VALUES ('Barber Supplies Co.', '0918-987-6543 / orders@barbersupplies.ph', 'ACTIVE');
END
GO

-- 3. SEED EMPLOYEES
IF NOT EXISTS (SELECT * FROM Employees WHERE FirstName = 'John' AND LastName = 'Doe')
BEGIN
    INSERT INTO Employees (FirstName, LastName, ContactNumber, EmployeeType, Status, BranchID) VALUES 
    ('John', 'Doe', '09171234567', 'Barber', 'ACTIVE', 1),
    ('Mark', 'Smith', '09181234568', 'Barber', 'ACTIVE', 1),
    ('David', 'Johnson', '09191234569', 'Barber', 'ACTIVE', 1),
    ('Maria', 'Santos', '09201234570', 'Staff', 'ACTIVE', 1),
    ('Anna', 'Reyes', '09211234571', 'Staff', 'ACTIVE', 1),
    ('System', 'Admin', '09000000000', 'Admin', 'ACTIVE', 1);
END
GO

-- 4. SEED USERS
IF NOT EXISTS (SELECT * FROM Users WHERE Username = 'superadmin')
BEGIN
    INSERT INTO Users (Username, PasswordHash, Role, FullName, AccountStatus, BranchID) VALUES
    ('superadmin', 'admin123', 'SuperAdmin', 'System Super Administrator', 'ACTIVE', 1),
    ('admin', 'admin123', 'Admin', 'Barbershop Owner / Admin', 'ACTIVE', 1),
    ('staff', 'staff123', 'Staff', 'Maria Cashier', 'ACTIVE', 1);
END
GO

-- 5. SEED CUSTOMERS & LOYALTY MEMBERS
IF NOT EXISTS (SELECT * FROM Customers WHERE FirstName = 'Michael' AND LastName = 'Santos')
BEGIN
    INSERT INTO Customers (FirstName, LastName, PhoneNumber, Email, Birthday, IsLoyaltyMember, LoyaltyPoints, Status) VALUES
    ('Michael', 'Santos', '09159998881', 'michael@example.com', '1995-08-26', 1, 40, 'ACTIVE'),
    ('James', 'Cruz', '09159998882', 'james@example.com', '1990-03-15', 1, 110, 'ACTIVE'),
    ('Kevin', 'Reyes', '09159998883', 'kevin@example.com', '1998-11-20', 0, 0, 'ACTIVE'),
    ('Sarah', 'Garcia', '09159998884', 'sarah@example.com', '2001-05-10', 1, 60, 'ACTIVE');

    INSERT INTO LoyaltyMembers (CustomerID, CurrentPoints, DateRegistered, Status) VALUES
    (1, 40, GETDATE(), 'ACTIVE'),
    (2, 110, GETDATE(), 'ACTIVE'),
    (4, 60, GETDATE(), 'ACTIVE');
END
GO

-- 6. SEED SERVICES (Base Haircut = ₱200)
IF NOT EXISTS (SELECT * FROM Services WHERE ServiceName = 'Haircut')
BEGIN
    INSERT INTO Services (ServiceName, Description, BasePrice, Status) VALUES
    ('Haircut', 'Standard Barber Haircut (Fade, Undercut, Buzz Cut, etc.)', 200.00, 'ACTIVE'),
    ('Beard Trim & Shape', 'Beard trimming and line shaping', 150.00, 'ACTIVE'),
    ('Haircut + Wash & Style', 'Complete haircut package with hair wash and pomade styling', 300.00, 'ACTIVE');
END
GO

-- 7. SEED PROMOTIONS
IF NOT EXISTS (SELECT * FROM Promotions WHERE Title = '20% OFF New Customers')
BEGIN
    INSERT INTO Promotions (Title, Description, DiscountType, DiscountValue, StartDate, EndDate, EligibilityRule, Status) VALUES
    ('20% OFF New Customers', 'First time customer discount', 'Percentage', 20.00, GETDATE(), DATEADD(day, 60, GETDATE()), 'NewCustomer', 'ACTIVE'),
    ('₱50 OFF Birthday Month', 'Celebration discount during birthday month', 'FixedAmount', 50.00, GETDATE(), DATEADD(day, 60, GETDATE()), 'Birthday', 'ACTIVE'),
    ('₱30 OFF Friend Referral', 'Discount for referred customers', 'FixedAmount', 30.00, GETDATE(), DATEADD(day, 60, GETDATE()), 'Referral', 'ACTIVE');
END
GO

-- 8. SEED LOYALTY REWARDS
IF NOT EXISTS (SELECT * FROM LoyaltyRewards WHERE RewardName = '50 points = ₱30 OFF')
BEGIN
    INSERT INTO LoyaltyRewards (RewardName, RequiredPoints, DiscountAmount, Status) VALUES
    ('50 points = ₱30 OFF', 50, 30.00, 'ACTIVE'),
    ('100 points = ₱80 OFF', 100, 80.00, 'ACTIVE'),
    ('150 points = Free Haircut', 150, 200.00, 'ACTIVE');
END
GO

-- 9. SEED INVENTORY ITEMS
IF NOT EXISTS (SELECT * FROM InventoryItems WHERE ItemName = 'Shaving Gel 500ml')
BEGIN
    INSERT INTO InventoryItems (ItemName, Category, Quantity, Unit, MinimumStockLevel, SupplierID, Cost, Status, BranchID) VALUES
    ('Shaving Gel 500ml', 'Grooming', 15, 'bottles', 5, 1, 250.00, 'IN STOCK', 1),
    ('Hair Pomade Matte 100g', 'Styling', 25, 'tubs', 8, 1, 350.00, 'IN STOCK', 1),
    ('Disposal Razor Blades', 'Sanitation', 3, 'boxes', 10, 1, 120.00, 'LOW STOCK', 1),
    ('Barber Neck Paper Wraps', 'Sanitation', 0, 'rolls', 5, 1, 80.00, 'OUT OF STOCK', 1);
END
GO

PRINT 'Uppercut Barber Shop CRM SQL Server database script completed successfully.';
GO
