# 🧑‍💼 Employee Management System API

The Employee Management System API is a backend solution built with ASP.NET Core Web API to manage organizational operations related to employees, departments, attendance, payroll, and reporting.
It follows Clean Architecture principles and implements Repository & Unit of Work patterns for clean, maintainable, and scalable data access.
The API uses DTOs (Data Transfer Objects) for structured request and response handling, and JWT Authentication with role-based authorization (Admin, Manager, Employee) to ensure data security and access control.

------------------------------------------------------------
# 🧱 Tech Stack
------------------------------------------------------------
Framework:            ASP.NET Core Web API
Database:             Microsoft SQL Server
ORM:                  Entity Framework Core
Architecture:         Clean Architecture
Design Patterns:      Repository & Unit of Work
Authentication:       JWT (JSON Web Tokens)
Data Models:          DTOs (Data Transfer Objects)

------------------------------------------------------------
# 📦 Modules Overview
------------------------------------------------------------

🏢 Department Module
- Manages all organizational departments.
- Supports full CRUD operations.
- Each employee is assigned to a specific department.

👥 Employee Module
- Handles all employee-related information.
- Allows creating, updating, and deleting employee records.
- Maintains centralized employee management for HR operations.

🗓️ Leave Management Module
- Employees can submit leave requests.
- Managers can approve or reject requests.
- Simplifies leave request workflow and approval tracking.

⏰ Attendance Module
- Tracks employee check-in and check-out times.
- Generates daily and monthly attendance reports.
- Monitors punctuality and overall working hours.

💰 Payroll Module
- Automates salary calculation and monthly payroll generation.
- Provides payroll reports per employee and organization-wide.
- Reduces manual work and ensures payment accuracy.

🔐 Authentication & Roles Module
- Provides secure login and registration using JWT Authentication.
- Enforces role-based access control with three main roles:
  👑 Admin: Full access to all system modules and data.
  🧑‍💼 Manager: Approves leaves, manages teams, and reviews reports.
  👤 Employee: Manages personal data, requests leaves, views attendance and payroll.
- Ensures data confidentiality and proper system authorization.

------------------------------------------------------------
# ⚙️ Architecture Overview
------------------------------------------------------------
EmployeeManagementSystem
│
├── Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Validators
│
├── Domain
│   ├── Entities
│   └── Enums
│
├── Infrastructure
│   ├── Data
│   ├── Repositories
│   └── UnitOfWork
│
├── API
│   ├── Controllers
│   ├── Middleware
│   └── Configurations
│
└── Tests
    └── Unit & Integration Tests

------------------------------------------------------------
# 🚀 Getting Started
------------------------------------------------------------
Prerequisites:
  - .NET 8 SDK or later
  - SQL Server
  - Visual Studio or VS Code

Setup Steps:
  1. Clone the repository
     git clone https://github.com/yourusername/EmployeeManagementSystemAPI.git

  2. Navigate to the project folder
     cd EmployeeManagementSystemAPI

  3. Update your connection string inside appsettings.json

  4. Apply database migrations
     dotnet ef database update

  5. Run the project
     dotnet run
  

------------------------------------------------------------
# 📊 Future Enhancements
------------------------------------------------------------
- Email notifications for leave approvals and payroll updates.
- Dashboard with analytics and visual reports for managers and admins.
- Integration with external HR and accounting systems.
- Role-specific notifications and performance tracking.

------------------------------------------------------------
# 👨‍💻 Author
------------------------------------------------------------
Name:      Tarek Elsabbagh
Role:      Backend Developer (.NET)
Email:     tarekelsabbagh@email.com
