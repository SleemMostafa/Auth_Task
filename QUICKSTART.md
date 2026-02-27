# Quick Start Guide

## Steps to Run the Application

### 1. Install .NET SDK
Make sure you have .NET 8.0 SDK installed:
```bash
dotnet --version
```

If not installed, download from: https://dotnet.microsoft.com/download/dotnet/8.0

### 2. Navigate to Project Directory
```bash
cd "e:\SelfStydy\Entishar\Auth_Task\Auth_Task"
```

### 3. Restore Packages
```bash
dotnet restore
```

### 4. Install EF Core Tools (if not already installed)
```bash
dotnet tool install --global dotnet-ef
```

### 5. Create Database Migration
```bash
dotnet ef migrations add InitialCreate
```

### 6. Apply Migration to Create Database
```bash
dotnet ef database update
```

This creates `usermanagement.db` with a seeded admin user.

### 7. Run the Application
```bash
dotnet run
```

Or press F5 in Visual Studio/Rider.

### 8. Access the Application
Open your browser and navigate to:
- HTTPS: https://localhost:5001
- HTTP: http://localhost:5000

### 9. Login
Use default credentials:
- **Username**: `admin`
- **Password**: `admin123`

## Common Commands

### View Database
```bash
# Install DB Browser for SQLite or use:
sqlite3 usermanagement.db
.tables
SELECT * FROM Users;
```

### Reset Database
```bash
# Delete database file
Remove-Item usermanagement.db

# Recreate
dotnet ef database update
```

### Add New Migration
```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

### Build Project
```bash
dotnet build
```

### Run in Watch Mode (auto-reload on changes)
```bash
dotnet watch run
```

## Troubleshooting

If .NET SDK is not recognized:
1. Verify installation: Control Panel → Programs → Microsoft .NET SDK
2. Check PATH environment variable includes .NET SDK
3. Restart terminal/IDE after installation

If migration commands fail:
1. Ensure you're in the correct directory (Auth_Task/Auth_Task)
2. Install EF tools: `dotnet tool install --global dotnet-ef`
3. Add EF Design package if needed: `dotnet add package Microsoft.EntityFrameworkCore.Design`

## What Was Created

✅ Complete authentication system with login/logout
✅ User CRUD operations (Create, Read, Update, Delete)
✅ SQLite database with Entity Framework Core
✅ Protected routes requiring authentication
✅ Bootstrap UI with responsive design
✅ Form validation on client and server side
✅ Session-based authentication
✅ Seeded admin user for testing

Enjoy your User Management System! 🚀
