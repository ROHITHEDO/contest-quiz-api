
# Contest Quiz API

This project is a Quiz Contest Backend built using ASP.NET Core Web API with JWT Authentication and Role Based Authorization.

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication

---

# Setup Instructions

### 1 Clone Repository

git clone https://github.com/yourusername/contest-quiz-api.git

### 2 Open Project

Open in Visual Studio

### 3 Configure Database

Update connection string in:

appsettings.json

  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ContestDB;Trusted_Connection=True;TrustServerCertificate=True"
  },

  Run migrations:
    update-database

  Run Application
    dotnet run

  API will run on
    https://localhost:Port

