# TaskFlow API

TaskFlow is a simple ASP.NET Core Web API for managing Projects and Tasks using JWT Authentication and Role-Based Authorization.

---

## Overview

The system allows users to:

- Register and login to receive a JWT token  
- Create and manage their own projects  
- Create tasks linked to projects  
- Access data based on their role (Admin or User)

---

## Technologies Used

- ASP.NET Core Web API  
- Entity Framework Core  
- SQL Server  
- ASP.NET Identity  
- JWT Authentication  
- Swagger  

---

## Authentication Flow

1. User registers or logs in  
2. API returns a JWT token  
3. Token is used in requests as:

Authorization: Bearer {token}

4. Protected endpoints require a valid token  

---

## Roles (Important)

### Admin

- Full access to all projects and tasks  
- Can create, update, delete, and view all data  

### User

- Can only manage their own projects  
- Cannot access other users’ data  

---

## Admin Account

Automatically created on application startup:

Email: admin@taskflow.com  
Password: Admin123@  
Role: Admin  

---

## API Endpoints

### Authentication

POST /api/v1/auth/register  
POST /api/v1/auth/login  

---

### Projects

POST /api/v1/projects  
GET /api/v1/projects  
GET /api/v1/projects/{id}  
PUT /api/v1/projects/{id}  
DELETE /api/v1/projects/{id}  

---

### Tasks

POST /api/v1/tasks  
GET /api/v1/tasks/project/{projectId}  
PUT /api/v1/tasks/status  
DELETE /api/v1/tasks/{id}  

---

## Database

- Users are managed using ASP.NET Identity  
- Projects are linked to Users  
- Tasks are linked to Projects  

---

## Running the Project

dotnet restore  
dotnet ef database update  
dotnet run  

---

## Swagger

http://localhost:5295/swagger
