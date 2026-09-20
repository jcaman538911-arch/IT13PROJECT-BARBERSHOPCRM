# Uppercut Barber Shop CRM - Tenant Accounts & Database Guide

This document contains full instructions and credentials for managing Tenant accounts, Application Login credentials, and SQL Server Management Studio (SSMS) database connections.

---

## 🔑 1. Application UI Login Accounts (Desktop CRM & Web API)

Use these accounts to log into the **BarberShop CRM Application** or **ASP.NET Core API**:

| Tenant / Company | Role / Title | Username | Password | Access Rights |
| :--- | :--- | :--- | :--- | :--- |
| **Tenant 1** (Company 1) | Owner / Admin | `owner1` | `owner123` | Full Admin access to Tenant 1 (`db68508`) |
| **Tenant 1** (Company 1) | Staff / Cashier | `staff1` | `staff123` | POS Cashier & Appointment access to Tenant 1 |
| **Tenant 2** (Company 2) | Owner / Admin | `owner2` | `owner123` | Full Admin access to Tenant 2 (`db68525`) |
| **Tenant 2** (Company 2) | Staff / Cashier | `staff2` | `staff123` | POS Cashier & Appointment access to Tenant 2 |
| **Tenant 3** (Company 3) | Owner / Admin | `owner3` | `owner123` | Full Admin access to Tenant 3 (`db68526`) |
| **Tenant 3** (Company 3) | Staff / Cashier | `staff3` | `staff123` | POS Cashier & Appointment access to Tenant 3 |
| **System Master** | Super Admin | `superadmin` | `admin123` | Multi-tenant system management |

---

## 🗄️ 2. MonsterASP Remote Database Credentials (SSMS Connections)

Use these credentials when connecting **SQL Server Management Studio (SSMS)** or external database tools to your live cloud databases on **MonsterASP**:

### Tenant 1 Database
- **Server Name**: `db68508.public.databaseasp.net`
- **Database Name**: `db68508`
- **User ID**: `db68508`
- **Password**: `caman314031`
- **Full Connection String**:
  ```text
  Server=db68508.public.databaseasp.net; Database=db68508; User Id=db68508; Password=caman314031; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=5;
  ```

### Tenant 2 Database
- **Server Name**: `db68525.public.databaseasp.net`
- **Database Name**: `db68525`
- **User ID**: `db68525`
- **Password**: `caman314032`
- **Full Connection String**:
  ```text
  Server=db68525.public.databaseasp.net; Database=db68525; User Id=db68525; Password=caman314032; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=5;
  ```

### Tenant 3 Database
- **Server Name**: `db68526.public.databaseasp.net`
- **Database Name**: `db68526`
- **User ID**: `db68526`
- **Password**: `caman314033`
- **Full Connection String**:
  ```text
  Server=db68526.public.databaseasp.net; Database=db68526; User Id=db68526; Password=caman314033; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=5;
  ```

### Default Fallback Connection String
- **Full Connection String**:
  ```text
  Server=db68508.public.databaseasp.net; Database=db68508; User Id=db68508; Password=caman314031; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Connect Timeout=5;
  ```

---

## 💻 3. Step-by-Step Guide: Connecting SSMS to MonsterASP

1. Open **SQL Server Management Studio (SSMS)**.
2. In the **Connect to Server** dialog:
   - **Server Name**: Enter `db68508.public.databaseasp.net` *(or db68525/db68526)*.
   - **Authentication**: Select **SQL Server Authentication**.
   - **User Name**: Enter `db68508` *(or db68525/db68526)*.
   - **Password**: Enter `caman314031` *(or caman314032/caman314033)*.
   - **Encrypt**: Select **Optional** (or check `☑ Trust Server Certificate`).
3. Click **Connect**.

### 💡 How to Connect Multiple Tenants Side-by-Side (Without Closing SSMS)
You can connect Tenant 1, Tenant 2, and Tenant 3 **at the same time** in SSMS:
1. At the top of the **Object Explorer** panel (top-left), click **Connect** $\rightarrow$ **Database Engine...** (or click the plug icon 🔌 with the green arrow).
2. Type the credentials for **Tenant 2** (`db68525.public.databaseasp.net`) or **Tenant 3** (`db68526.public.databaseasp.net`) and click **Connect**.
3. All tenant databases will now appear **together side-by-side** in your Object Explorer list!

---

## 🔍 5. How to View Tables & Data Rows in SSMS

Once connected to your database in **SQL Server Management Studio (SSMS)**:

1. In the **Object Explorer** panel on the left side:
   - Click **`+` Databases**
   - Click **`+` `db68508`** *(or `db68525` / `db68526`)*
   - Click **`+` Tables**
2. You will see all your system database tables:
   - `dbo.Users` *(Owner & Staff user logins)*
   - `dbo.Customers` *(Customer list & loyalty info)*
   - `dbo.Employees` *(Barbers & staff profiles)*
   - `dbo.Services` *(Service list & prices)*
   - `dbo.Transactions` *(POS sales records)*
   - `dbo.Appointments` *(Appointment bookings)*
   - `dbo.Branches` *(Company branch settings)*
3. **To view data inside any table**:
   - **Right-click** on the table name (e.g., `dbo.Customers`).
   - Click **Select Top 1000 Rows**.
   - SSMS will display all the saved records in the results grid below!
