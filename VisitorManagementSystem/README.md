# Visitor Management System

A complete ASP.NET Core MVC (.NET 8) application for managing visitor check-ins and check-outs.

## Features

- **Dashboard**: Real-time statistics showing total visitors today, visitors inside, and checked-out visitors
- **Employee Master**: CRUD operations for employee management
- **Visitor Master**: CRUD operations for visitor management  
- **Visit Management**: Register visits, check-in/check-out functionality, visit history

## Technology Stack

- **Framework**: ASP.NET Core MVC (.NET 8)
- **Database**: SQL Server
- **ORM**: Entity Framework Core (Code First)
- **Frontend**: Bootstrap 5, jQuery, JavaScript
- **Pattern**: Repository Pattern

## Project Structure

```
VisitorManagementSystem/
├── Controllers/
│   ├── DashboardController.cs
│   ├── EmployeeController.cs
│   ├── VisitorController.cs
│   └── VisitController.cs
├── Models/
│   ├── Employee.cs
│   ├── Visitor.cs
│   └── Visit.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── SQLScript.sql
├── Repositories/
│   ├── IEmployeeRepository.cs
│   ├── EmployeeRepository.cs
│   ├── IVisitorRepository.cs
│   ├── VisitorRepository.cs
│   ├── IVisitRepository.cs
│   └── VisitRepository.cs
├── Views/
│   ├── Dashboard/
│   ├── Employee/
│   ├── Visitor/
│   ├── Visit/
│   ├── Home/
│   └── Shared/
├── wwwroot/
│   ├── js/
│   │   ├── employee.js
│   │   ├── visitor.js
│   │   ├── visit.js
│   │   └── site.js
│   └── css/
│       └── site.css
├── Program.cs
├── appsettings.json
└── VisitorManagementSystem.csproj
```

## Database Setup

### Option 1: Using SQL Script
Run the provided SQL script `Data/SQLScript.sql` in SQL Server Management Studio to create the database with sample data.

### Option 2: Using EF Core Migrations
```bash
cd VisitorManagementSystem
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Configuration

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=VisitorManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## Running the Application

1. **Restore packages:**
   ```bash
   dotnet restore
   ```

2. **Build the project:**
   ```bash
   dotnet build
   ```

3. **Run migrations (if using EF Core):**
   ```bash
   dotnet ef database update
   ```

4. **Run the application:**
   ```bash
   dotnet run
   ```

5. **Access the application:**
   - Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

## Database Schema

### Visitors Table
- VisitorId (PK)
- VisitorName
- CompanyName
- MobileNo
- Email
- IDProof

### Employees Table
- EmployeeId (PK)
- EmployeeName
- Department
- Designation
- MobileNo
- Email
- IsActive

### Visits Table
- VisitId (PK)
- VisitorId (FK -> Visitors)
- EmployeeId (FK -> Employees)
- Purpose
- VisitDate
- CheckInTime
- CheckOutTime
- Status (CheckedIn/CheckedOut)

## API Endpoints

### Employee
- GET `/Employee/GetEmployees` - Get all employees
- GET `/Employee/GetActiveEmployees` - Get active employees only

### Visitor
- GET `/Visitor/GetVisitors` - Get all visitors

### Visit
- GET `/Visit/GetTodayVisits` - Get today's visits
- GET `/Visit/GetVisitorsInside` - Get visitors currently inside
- GET `/Visit/GetRecentVisits?count=5` - Get recent visits

## Key Features

1. **Real-time Dashboard**: Shows live statistics of visitor activity
2. **Modal-based CRUD**: Add/Edit employees and visitors using Bootstrap modals
3. **jQuery AJAX**: Smooth asynchronous operations without page reloads
4. **Form Validation**: Client-side and server-side validation
5. **Responsive Design**: Bootstrap 5 ensures mobile-friendly UI
6. **Repository Pattern**: Clean separation of concerns for maintainability

## Sample Data

The SQL script includes sample data for:
- 5 Visitors
- 6 Employees (5 active, 1 inactive)
- 5 Visits (mix of checked-in and checked-out)

## Security Notes

- CSRF protection enabled via anti-forgery tokens
- Input validation on both client and server side
- Foreign key constraints prevent orphaned records

## License

This project is created for demonstration purposes.
