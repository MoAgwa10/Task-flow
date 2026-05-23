TaskFlow API

TaskFlow is a simple ASP.NET Core Web API for managing Projects and Tasks using JWT Authentication and Role-Based Authorization.

Overview

The system allows users to:

Register and login to receive a JWT token
Create and manage their own projects
Create tasks linked to projects
Access data based on their role (Admin or User)
Technologies Used
ASP.NET Core Web API
Entity Framework Core
SQL Server
ASP.NET Identity
JWT Authentication
Swagger
Authentication Flow
User registers or logs in
API returns a JWT token
Token is used in Swagger or Postman:
Authorization: Bearer {token}
Protected endpoints require a valid token
Roles (Important)
Admin
Has full access to all projects and tasks
Can view, create, update, and delete any data in the system
User
Can only access and manage their own projects
Cannot access other users’ data
Admin Account

A default admin is created automatically when the application starts:

Email: admin@taskflow.com
Password: Admin123@
Role: Admin
API Endpoints
Auth
POST /api/v1/auth/register
POST /api/v1/auth/login
Projects
POST /api/v1/projects (Create project)
GET /api/v1/projects (Admin sees all, User sees own only)
GET /api/v1/projects/{id}
PUT /api/v1/projects/{id}
DELETE /api/v1/projects/{id}
Tasks
POST /api/v1/tasks
GET /api/v1/tasks/project/{projectId}
PUT /api/v1/tasks/status
DELETE /api/v1/tasks/{id}
Database
Users are managed using ASP.NET Identity
Projects are linked to Users
Tasks are linked to Projects
Running the Project
dotnet restore
dotnet ef database update
dotnet run
Swagger

Open in browser:

http://localhost:5295/swagger
لو عايز 👌
أعملهولك version “GitHub Premium” فيه badges وCI وstructure أقوى يخليه شكله مشروع شركة مش مشروع طالب.
