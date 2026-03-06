
# Contest Quiz API

This project is a Quiz Contest Backend built using ASP.NET Core Web API with JWT Authentication and Role Based Authorization.

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication

## Postman Collection

Import the Postman collection to test the APIs.

Location:

Postman/contest-quiz-api.postman_collection.json

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



  ### 4  API Endpoints
Register

POST /api/auth/register

Body

{
 "name":"Rohith",
 "email":"rohith@gmail.com",
 "password":"123456"
}
Login

POST /api/auth/login

Returns JWT Token.

Add Question (Admin / VIP)

POST /api/contest/add-question

Authorization: Bearer Token

Add Option (Admin / VIP)

POST /api/contest/add-option

Submit Answers

POST /api/contest/submit/{contestId}

Leaderboard

GET /api/contest/leaderboard/{contestId}


 ### 5 Features

JWT Authentication

Role Based Authorization

Contest Participation

Score Calculation

Leaderboard

  Run Application
    dotnet run

  API will run on
    https://localhost:Port

