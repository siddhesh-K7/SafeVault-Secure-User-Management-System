# 🛡️ SafeVault – Secure User Management System

## 📖 Project Description
SafeVault is a secure web application built for the Coursera capstone project to demonstrate advanced secure coding practices. The system provides robust user management APIs while addressing and mitigating critical vulnerabilities such as SQL injection, Cross-Site Scripting (XSS), and Broken Access Control.

---

## 🔒 Security Features Implemented

1. **Input Validation**: Centralized server-side validation using Data Annotations (e.g., Regex for usernames/passwords) ensuring malicious or malformed input fails fast.
2. **SQL Injection Prevention**: All queries strictly use Entity Framework Core LINQ to safely parameterize database interactions. Raw SQL strings are entirely avoided.
3. **Secure Password Hashing**: Utilizes `BCrypt.Net-Next` to securely salt and hash passwords. Plain-text passwords are never stored.
4. **JWT Authentication**: Configured securely with issuer/audience validation and secure expiration handling.
5. **Role-Based Access Control (RBAC)**: Implemented `Admin` and `User` roles. Users can only access their own profiles, while Admins have global access to management endpoints.
6. **XSS Protection**: HTML encoding is strictly enforced (via `HttpUtility.HtmlEncode`) on stored/returned data.
7. **Security Headers & Exception Handling**: Global middleware seamlessly handles unexpected exceptions to prevent stack trace leaks and enforces strict HTTP security headers (e.g., `Content-Security-Policy`, `X-Frame-Options`).

---

## 📂 Project Structure

- `Controllers/`: API endpoints (`AuthController`, `UsersController`)
- `Services/`: Business logic, security validations, and EF Core interactions
- `Models/` & `DTOs/`: Core data entities and request/response validation schemas
- `Middleware/`: Security header injection and global exception masking
- `Data/`: EF Core `DbContext` implementation
- `Tests/`: Comprehensive suite of automated security and integration tests

---

## 🔑 Authentication & Authorization
- **Authentication**: Clients authenticate by passing `UsernameOrEmail` and `Password` to `POST /api/auth/login`. On success, a JWT is returned to authenticate subsequent requests.
- **Authorization**:
  - `User`: Permitted to view and update only their own profile details.
  - `Admin`: Full access to `GET /api/users`, `DELETE /api/users/{id}`, and other management routes.

---

## 🐞 Vulnerabilities Identified and Fixes Applied

| Vulnerability Type | Risk | Applied Fix / Mitigation |
|---|---|---|
| **SQL Injection** | Unauthorized database access, data exposure | Strict use of EF Core parameterized LINQ queries. |
| **Cross-Site Scripting (XSS)** | Malicious script execution in client browsers | Strict input validation + `HttpUtility.HtmlEncode` on display properties. |
| **Weak Input Validation** | Ingestion of invalid or malicious data | Server-side validation via strict Data Annotations. |
| **Missing Authorization** | Horizontal/Vertical Privilege Escalation | Explicit `[Authorize]` attributes and RBAC role checks. |
| **Plain-text Passwords** | Credential exposure | Strong, salted password hashing using BCrypt. |
| **Information Leakage** | Sensitive data exposure via stack traces | Global `SecurityExceptionMiddleware` to return sanitized errors. |

---

## 🤖 How Microsoft Copilot Assisted

- **Secure Coding**: Copilot assisted in generating strict Regular Expression patterns for Data Annotations to validate passwords and usernames safely.
- **Authentication and Authorization**: Copilot guided the setup of JWT bearer tokens and helped scaffold the `ClaimsPrincipal` validation for checking `User` vs `Admin` roles.
- **Debugging**: Copilot identified a risk of stack-trace leakage and suggested implementing global exception-handling middleware.
- **Security Testing**: Copilot generated boilerplate xUnit tests targeting specific injection vectors (SQLi and XSS) to ensure the API accurately rejects malicious payloads.
- **Final Review**: Copilot helped review the HTTP response headers to ensure best-practice security headers were included in every response.

---

## ⚙️ Setup and Execution Instructions

### Prerequisites
- .NET 8.0 SDK

### Running the Application
```bash
dotnet restore
dotnet build
dotnet run
```
Once running, the Swagger UI is accessible at `http://localhost:<port>/swagger/index.html`.

### Running the Security Tests
The `Tests/` directory contains automated unit tests verifying that vulnerabilities like SQLi and XSS are mitigated.
```bash
dotnet test
```

---

## ✅ 30-Point Rubric Checklist

- [x] **5 points** – Public GitHub repository created
- [x] **5 points** – Copilot used to generate secure input validation and SQL injection prevention
- [x] **5 points** – Authentication, authorization, and RBAC implemented with Copilot
- [x] **5 points** – SQL injection and XSS vulnerabilities identified and resolved
- [x] **5 points** – Security tests generated and executed
- [x] **5 points** – Vulnerabilities, fixes, and Copilot assistance summarized
