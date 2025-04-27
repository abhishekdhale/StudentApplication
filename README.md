# Student Management System

A web API application for managing students and courses with authentication.

## Setup Instructions

1. Prerequisites:
   - Visual Studio 2022
   - .NET 9.0 SDK
   - SQL Server (or SQL Server Express)

2. Database Setup:
   - Open the solution in Visual Studio
   - Open Package Manager Console (Tools > NuGet Package Manager > Package Manager Console)
   - Run the following commands:
     ```
     Add-Migration InitialCreate
     Update-Database
     ```
   - The application will use the connection string configured in appsettings.json

3. Running the Application:
   - Open the solution in Visual Studio
   - Press F5 or click the "Start" button to run the application
   - The application will start and Swagger UI will open in your default browser

## Assumptions

1. Authentication is implemented using JWT tokens
2. Course data is pre-populated in the database
3. Student-Course relationship is many-to-many
4. API endpoints are protected and require authentication

## Testing Instructions

1. First, obtain a JWT token by calling the Login API:
   - Open Swagger UI (available at /swagger when running the application)
   - Use the `/api/auth/login` endpoint with the following credentials:
     ```
     {
       "username": "admin",
       "password": "admin123"
     }
     ```

2. Use the received token in the Authorization header for subsequent API calls:
   - Click the "Authorize" button in Swagger UI
   - Enter the token in the format: `Bearer <your-token>`

3. Test the following endpoints:
   - POST /api/students - Create a new student
   - POST /api/students/{id}/courses - Assign courses to a student
   - GET /api/students - List all students with their courses

## Features Implemented

- [x] Add Student API
- [x] Assign Student to Courses
- [x] List Students with Course Information
- [x] Authentication System
- [x] Error Handling
- [x] Database Schema
- [x] API Documentation (Swagger)

## Database Schema

```sql
CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Phone NVARCHAR(20) NOT NULL
);

CREATE TABLE Courses (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500)
);

CREATE TABLE StudentCourses (
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    PRIMARY KEY (StudentId, CourseId),
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);
``` 
