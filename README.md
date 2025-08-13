
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
| PUT    | `/api/Department/{DepartmentId}/Students/{id}/toggleStatus` | Toggle student status |
| PUT    | `/api/Department/{DepartmentId}/Students/{id}/assign-to-department` | Assign student to department |

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

### 📘 Subjects (`/api/Subjects`)

| Method | Endpoint                                                                 | Description                                     |
|--------|--------------------------------------------------------------------------|-------------------------------------------------|
| GET    | `/api/Subjects/{id}`                                                     | Get subject by ID                               |
| GET    | `/api/Subjects`                                                          | List all subjects                               |
| POST   | `/api/Subjects`                                                          | Create new subject                              |
| PUT    | `/api/Subjects/{id}`                                                     | Update subject                                  |
| PUT    | `/api/Subjects/{id}/toggleStatus`                                        | Toggle subject status                           |
| POST   | `/api/Subjects/department/{departmentId}/subject/{id}/add-subject-to-department` | Add subject to department (with isMandatory)    |
| PUT    | `/api/Subjects/department/{departmentId}/subject/{id}/toggleStatus-departmentSubject` | Toggle status of subject in department         |
| POST   | `/api/Subjects/student/{studentId}/subject/{id}/add-subject-to-student`  | Assign subject to student                       |
| PUT    | `/api/Subjects/department/{departmentID}/student/{studentId}/subject/{id}/toggleStatus-studentSubject` | Toggle subject status for student in department |

---

### 🧮 Student Grades (`/api/StudentSubjects`)

| Method | Endpoint                                                                 | Description                            |
|--------|--------------------------------------------------------------------------|----------------------------------------|
| POST   | `/api/StudentSubjects/student/{studentId}/subject/{id}/add-grade-to-student` | Add grade for a student in a subject   |
| GET    | `/api/StudentSubjects/{studentId}/get-grades-of-students`                | Get all subjects and grades for student|

---
### 🛡️ Roles Management (`/api/Roles`)

| Method | Endpoint                                | Description                          |
|--------|------------------------------------------|--------------------------------------|
| GET    | `/api/Roles?includeDisabled={bool}`      | Get all roles (with optional filter) |
| GET    | `/api/Roles/{id}`                        | Get role by ID                        |
| POST   | `/api/Roles`                             | Create a new role                     |
| PUT    | `/api/Roles/{id}`                        | Update role by ID                     |
| PUT    | `/api/Roles/{id}/toggle-status`          | Toggle the status of a role          |

---

🏗️ Architecture
✅ clean archeticture
✅ ** CQRS with MediatR is applied in the **Student Module**.
✅ Generic Repository + Unit of Work
✅ Result Pattern for consistent responses
✅ Separation of Concerns using SOLID principles
---

Key Features
-🔒 User and Role Management: Leveraged JWT for secure authentication and authorization, allowing for seamless and secure access control.

-🚨 Exception Handling: Integrated centralized exception handling to manage errors gracefully, significantly enhancing the user experience.

-⚠️ Error Handling with Result Pattern: Employed a result pattern for structured error handling, providing clear and actionable feedback to users.

-🚦CORS (Cross-Origin Resource Sharing): a security feature implemented by web browsers to prevent web pages from making requests to a different domain than the one that served the web page.

-🔄 Mapster: Utilized for efficient object mapping between models, improving data handling and reducing boilerplate code.

-✅ Fluent Validation: Ensured data integrity by effectively validating inputs, leading to user-friendly error messages.

-🔑 Account Management: Implemented features for user account management, including change password and reset password functionalities.

-🚦 Rate Limiting: Controlled the number of requests to prevent abuse, ensuring fair usage across all users.

-🛠️ Background Jobs: Used Hangfire for managing background tasks like sending confirmation emails and processing password resets seamlessly.

-🗃️ Hybrid Caching: Optimized performance with caching for frequently accessed data, significantly improving response times.

-📧 Email Confirmation: Managed user email confirmations, password changes, and resets seamlessly to enhance security.

-🔍 Health Checks: Incorporated health checks to monitor the system’s status and performance, ensuring reliability and uptime.

-✅ Generic Repository for all module

-✅ Applying CQRS with MediatR is applied in the **Student Module
