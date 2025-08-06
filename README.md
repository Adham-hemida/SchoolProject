
  <h1>📚 SchoolProject API</h1>
  <p>🔧 A Clean Architecture ASP.NET Core Web API for managing students, teachers, departments, and assignments</p>


---

## 🚀 Tech Stack

- ASP.NET Core 9
- Database : SQL Server
- Entity Framework Core
- MediatR (CQRS Pattern) – (Used in Student Module)
- Clean Architecture
- Unit of Work
- Generic Repository
- Mapster (Object Mapping)
- JWT Authentication + Refresh Tokens
- Hangfire (Background Jobs)
- Rate Limiting
- Health Check
-Caching: Hybrid Cache 
-Documentation: Scalar

---

## 📦 Project Overview

A comprehensive school management system that provides:
- Authentication & Authorization
- Student & Teacher Management
- Department & Subject Management
- Assignments with file attachments
- Student submissions
- Role & Claims-based Permission System

> ✅ A responsible admin is assumed to be generating email accounts for both **teachers** and **students** before they can log in.

---

## 🔐 Authentication & User Management

### **Auth Endpoints** (`/Auth`)

| Method | Endpoint                     | Description             |
| ------ | ---------------------------- | ----------------------- |
| POST   | `/Auth`                      | Login                   |
| POST   | `/Auth/refresh`              | Refresh JWT token       |
| PUT    | `/Auth/revoke-refresh-token` | Revoke refresh token    |
| POST   | `/Auth/forget-password`      | Start password reset    |
| POST   | `/Auth/reset-password`       | Complete password reset |

### **Profile Endpoints** (`/Accounts`)

| Method | Endpoint              | Description              |
| ------ | --------------------- | ------------------------ |
| GET    | `/Accounts`                 | Get current user profile |
| PUT    | `/Accounts/change-password` | Change password          |

---

## 👥 Admin User Management (`/api/User`)

| Method | Endpoint                       | Description         |
| ------ | ------------------------------ | ------------------- |
| GET    | `/api/User`                    | Get user      |
| POST   | `/api/User`                    | Create new user     |
| PUT    | `/api/User/{id}`               | Update user         |
| PUT    | `/api/User/UnLock/{id}`        | Unlock user account |
| POST   | `/api/Users/teacher/{teacherId}/assign-user`        | Assign user to teacher |
| POST   | `/api/Users/teacher/create-with-user`        |Create teacher with user account|
| POST   | `/api/Users/student/{studentId}/assign-user`        |Assign user to student|
| POST   | `/api/Users/student/create-with-user`        |Create student with user account|

---

## 📘 Core Entities

### 👨‍🎓 Students (`/api/Department/{DepartmentId}/Students`)

| Method | Endpoint                               | Description           |
| ------ | -------------------------------------- | --------------------- |
| GET    | `/api/Department/{DepartmentId}/Students`                    | List All students         |
| POST   | `/api/Department/{DepartmentId}/Students`                    | Add student           |
| GET    | `/api/Department/{DepartmentId}/Students/{id}`               | Get student by ID     |
| PUT    | `/api/Department/{DepartmentId}/Students/{id}`               | Update student        |
| PUT    | `/api/Department/{DepartmentId}/Students/{id}/toggleStatus` | Assign to department |
| PUT    | `/api/Department/{DepartmentId}/Students/{id}/assign-to-department` | Toggle student status |

> **Note:** CQRS with MediatR is applied in the **Student Module**.

---

### 👨‍🏫 Teachers (`/api/Teacher`)

| Method | Endpoint                          | Description       |
| ------ | --------------------------------- | ----------------- |
| GET    | `/api/Teacher`                    | List teachers     |
| POST   | `/api/Teacher`                    | Add teacher       |
| GET    | `/api/Teacher/{id}`               | Get teacher by ID |
| PUT    | `/api/Teacher/{id}`               | Update teacher    |
| PUT    | `/api/Teacher/toggle-status/{id}` | Enable/Disable Teacher   |
| PUT    | `/api/Teacher/{teacherId}/assign-subject/{id}` | Assign subject to teacher  |

---

### 🗂️ Assignments (`/api/Assignment`)

| Method | Endpoint                             | Description              |
| ------ | ------------------------------------ | ------------------------ |
| POST    | `/api/Assignment/{teacherId}`                    | Teacher Create assignment   |
| GET   | `/api/Assignment/{assignmentId}`                    |Get assignment by ID       |
| GET    | `/api/Assignment/{assignmentId}/submissions`               | View submissions for assignment     |
| PUT    | `/api/Assignment/subject/{subjectId}/assignment/{assignmentId}`               | Update assignment        |
| PUT    | `/api/Assignment/{assignmentId}/toggleStatus` | Toggle assignment status |

---

### 🧾 Assignment File Uploads (`/api/files`)

| Method | Endpoint                 | Description           |
| ------ | ------------------------ | --------------------- |
| POST   | `/api/FileAttachments/assignment/{assignmentId}/upload`             |Upload teacher files to assignment  |
| POST   | `/api/FileAttachments/assignment/{assignmentId}/upload-assignment`    |Upload student submission |
|  GET    | `/api/FileAttachments/assignment/{assignmentId}/subject/{subjectId}/download/{fileId}`       | Download File of Assignment    |
| GET    | `/api/FileAttachments/download/{fileId}`        |Download student submission         |

---

## 🏢 Departments 
🏛 Departments (/api/Department)

| Method | Endpoint                             | Description              |
| ------ | ------------------------------------ | ------------------------ |
| GET    | `/api/Department`                    | Get all departments  |
| GET   | `/api/Department/{id}`                    | Get department by ID      |
| POST   | `/api/Department`               | Create new department     |
| PUT    | `/api/Department/{id}`               | Update department     |
| PUT    | `/api/Department/{id}/toggleStatus` | Enable/Disable department |

---

📚 Department Subjects (/api/DepartmentSubjects)
| Method | Endpoint                             | Description              |
| ------ | ------------------------------------ | ------------------------ |
| GET    | `/api/DepartmentSubjects`                    |Get all subjects for all departments  |
| GET   | `/api/DepartmentSubjects/{id}`                    | Get subjects for a specific department      |

---

🏗️ Architecture
✅ clean archeticture
✅ ** CQRS with MediatR is applied in the **Student Module**.
✅ Generic Repository + Unit of Work
✅ Result Pattern for consistent responses
✅ Separation of Concerns using SOLID principles
---

## 🛠️ Features Implemented

- ✅ Claims-Based Permissions (No Roles)
- ✅ File Upload & Download (Assignment & Submissions)
- ✅ JWT with Refresh Token
- ✅ Email Confirmation & Password Reset
- ✅ Pagination & Filtering
- ✅ Unit of Work Pattern (Most Services)
- ✅ Hangfire Dashboard (Optional)
- ✅ Hybrid Caching 

