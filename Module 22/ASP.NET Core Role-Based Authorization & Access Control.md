# ASP.NET Core Role-Based Authorization & Access Control (Easy to Advanced)

## Roles | Claims | Role-Based Authorization | Securing Admin Pages | Complete Interview Guide

---

# 📌 What is Authorization?

Authorization is the process of determining **what an authenticated user is allowed to access**.

It answers:

```text
What can this user do?
```

Example

```text
User Login

↓

Authorization

↓

Can Access?

YES / NO
```

---

# Authentication vs Authorization ⭐⭐⭐⭐⭐

| Authentication | Authorization |
|----------------|---------------|
| Who are you? | What can you access? |
| Login | Permissions |
| Identity Verification | Access Control |
| Happens First | Happens After Authentication |

---

# Real World Example

```text
Admin Login

↓

Can Manage Products

YES

Customer Login

↓

Can Manage Products

NO
```

---

# Why Authorization is Important?

Without Authorization

```text
Customer

↓

Admin Panel

↓

Delete Products

↓

Security Risk
```

With Authorization

```text
Customer

↓

Admin Panel

↓

403 Forbidden
```

---

# Authorization Flow ⭐⭐⭐⭐⭐

```text
User

↓

Login

↓

Authentication

↓

Authorization

↓

Controller

↓

View
```

---

# 🎯 Recommended Learning Order (Most Important First)

| Priority | Topic | Interview Frequency |
|-----------|------------------------------------------|----------------|
| ⭐⭐⭐⭐⭐ | Authentication vs Authorization | Very High |
| ⭐⭐⭐⭐⭐ | ASP.NET Core Identity | Very High |
| ⭐⭐⭐⭐⭐ | Roles | Very High |
| ⭐⭐⭐⭐⭐ | Claims | Very High |
| ⭐⭐⭐⭐⭐ | Authorize Attribute | Very High |
| ⭐⭐⭐⭐⭐ | Role-Based Authorization | Very High |
| ⭐⭐⭐⭐⭐ | Admin Page Security | Very High |
| ⭐⭐⭐⭐⭐ | User Roles | Very High |
| ⭐⭐⭐⭐⭐ | RoleManager | Very High |
| ⭐⭐⭐⭐⭐ | UserManager | Very High |
| ⭐⭐⭐⭐⭐ | ClaimsPrincipal | Very High |
| ⭐⭐⭐⭐⭐ | IdentityUser | Very High |
| ⭐⭐⭐⭐ | Authorization Policies | High |
| ⭐⭐⭐⭐ | Claims-Based Authorization | High |
| ⭐⭐⭐⭐ | Multiple Roles | High |
| ⭐⭐⭐⭐ | Custom Policies | High |
| ⭐⭐⭐⭐ | Razor Authorization | High |
| ⭐⭐⭐⭐ | Navigation Security | High |
| ⭐⭐⭐⭐ | Identity Roles Table | High |
| ⭐⭐⭐ | Resource-Based Authorization | Medium |
| ⭐⭐⭐ | Custom Authorization Handler | Medium |
| ⭐⭐ | Dynamic Authorization | Low |

---

# Part 1 : Authentication vs Authorization

Authentication

```text
Who are you?
```

Authorization

```text
What can you access?
```

Example

```text
Login

↓

User Authenticated

↓

Can Access Admin?

↓

YES / NO
```

---

# Part 2 : Roles

## What is a Role? ⭐⭐⭐⭐⭐

A Role is a group of permissions assigned to users.

Examples

```text
Admin

Customer

Manager

Employee

Seller
```

---

# Example

```text
Admin

↓

Manage Categories

Manage Products

Manage Users

Manage Orders
```

Customer

↓

```text
View Products

Place Orders
```

---

# Role Table

```text
AspNetRoles

----------------

Id

Name

NormalizedName
```

---

# User Role Table

```text
AspNetUserRoles

----------------

UserId

RoleId
```

---

# Part 3 : Claims

## What is a Claim? ⭐⭐⭐⭐⭐

A Claim is a piece of information about the user.

Examples

```text
Name

Email

Age

Department

Country

Role
```

---

# Example Claims

```text
Name

Sabbir

Role

Admin

Email

abc@gmail.com
```

---

# ClaimsPrincipal

Current logged-in user.

```csharp
User
```

---

# Read Claim

```csharp
User.Identity.Name
```

---

# Get Role

```csharp
User.IsInRole(
"Admin");
```

---

# Get Claim

```csharp
User.FindFirst(
ClaimTypes.Email)?
.Value;
```

---

# Roles vs Claims ⭐⭐⭐⭐⭐

| Roles | Claims |
|--------|---------|
| Group of Permissions | User Information |
| Admin | Email |
| Customer | Department |
| Manager | Age |
| Used for Access | Used for Identity |

---

# Part 4 : Identity Setup

Program.cs

```csharp
builder.Services

.AddIdentity<
IdentityUser,
IdentityRole>()

.AddEntityFrameworkStores<
AppDbContext>();
```

---

# Identity Tables

```text
AspNetUsers

AspNetRoles

AspNetUserRoles

AspNetUserClaims

AspNetRoleClaims
```

---

# Part 5 : Creating Roles ⭐⭐⭐⭐⭐

Inject

```csharp
RoleManager<
IdentityRole>
```

---

Example

```csharp
if(!await roleManager
.RoleExistsAsync(
"Admin"))
{
    await roleManager
    .CreateAsync(

    new IdentityRole(
    "Admin"));
}
```

---

# Create Customer Role

```csharp
await roleManager

.CreateAsync(

new IdentityRole(
"Customer"));
```

---

# Part 6 : Assign Role to User ⭐⭐⭐⭐⭐

Inject

```csharp
UserManager<
IdentityUser>
```

---

Assign

```csharp
await userManager

.AddToRoleAsync(

user,

"Admin");
```

---

Check Role

```csharp
await userManager

.IsInRoleAsync(

user,

"Admin");
```

---

# Get User Roles

```csharp
await userManager

.GetRolesAsync(user);
```

---

# Part 7 : Authorize Attribute ⭐⭐⭐⭐⭐

Protect Entire Controller

```csharp
[Authorize]

public class ProductController
: Controller
{
}
```

Only logged-in users can access.

---

# Protect Action

```csharp
[Authorize]

public IActionResult Create()
{
}
```

---

# Role-Based Authorization ⭐⭐⭐⭐⭐

Only Admin

```csharp
[Authorize(
Roles="Admin")]
```

---

Admin or Manager

```csharp
[Authorize(
Roles="Admin,Manager")]
```

---

Multiple Attributes

```csharp
[Authorize]

[Authorize(
Roles="Admin")]
```

---

# Allow Anonymous

```csharp
[AllowAnonymous]

public IActionResult Index()
{
}
```

Anyone can access.

---

# Part 8 : Securing Admin Pages ⭐⭐⭐⭐⭐

Controller

```csharp
[Area("Admin")]

[Authorize(
Roles="Admin")]

public class ProductsController
: Controller
{
}
```

---

Result

```text
Customer

↓

Admin Panel

↓

403 Forbidden
```

---

# Navigation Security

Show menu only to Admin.

```csharp
@if(User.IsInRole(
"Admin"))
{
<a>

Admin Panel

</a>
}
```

---

# Hide Admin Button

```csharp
@if(User.IsInRole(
"Admin"))
{
<button>

Delete

</button>
}
```

---

# Part 9 : Claims-Based Authorization

Policy

Program.cs

```csharp
builder.Services

.AddAuthorization(
options=>
{
options.AddPolicy(

"AdultOnly",

policy=>

policy.RequireClaim(
"Age","18"));
});
```

---

Use

```csharp
[Authorize(
Policy="AdultOnly")]
```

---

# Part 10 : Custom Policies ⭐⭐⭐⭐

Require Department

```csharp
policy.RequireClaim(

"Department",

"IT");
```

---

Require Country

```csharp
policy.RequireClaim(

"Country",

"Bangladesh");
```

---

# Part 11 : Authorization Policies

```csharp
builder.Services

.AddAuthorization(
options=>
{
options.AddPolicy(

"AdminOnly",

policy=>

policy.RequireRole(
"Admin"));
});
```

---

Use

```csharp
[Authorize(
Policy="AdminOnly")]
```

---

# Part 12 : Razor Authorization

Show UI

```csharp
@if(User.Identity
.IsAuthenticated)
{
<p>

Welcome

</p>
}
```

---

Show Admin UI

```csharp
@if(User.IsInRole(
"Admin"))
{
<a>

Dashboard

</a>
}
```

---

# Part 13 : Claims Example

Add Claim

```csharp
await userManager

.AddClaimAsync(

user,

new Claim(

ClaimTypes.Country,

"Bangladesh"));
```

---

Read Claim

```csharp
User.FindFirst(

ClaimTypes.Country)?
.Value;
```

---

# Part 14 : Secure CRUD Module

Controller

```csharp
[Authorize(
Roles="Admin")]

public class CategoriesController
: Controller
{
}
```

Create

```csharp
[Authorize(
Roles="Admin")]
```

Delete

```csharp
[Authorize(
Roles="Admin")]
```

Edit

```csharp
[Authorize(
Roles="Admin")]
```

---

# Real World Role Structure

```text
Admin

↓

Manage Everything

Manager

↓

Orders

Employees

Customer

↓

Shopping

Order History
```

---

# Authorization Flow ⭐⭐⭐⭐⭐

```text
Login

↓

Identity

↓

User Role

↓

Authorize

↓

Controller

↓

Action

↓

View
```

---

# Folder Structure

```text
Areas

Admin

Controllers

ProductsController

CategoriesController

Views

Identity

Login

Register

Program.cs
```

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Use ASP.NET Core Identity

✅ Create Roles at Startup

✅ Use Role-Based Authorization

✅ Secure Entire Admin Area

✅ Use Claims for User Information

✅ Hide Unauthorized UI

✅ Never Trust Client-Side Checks

✅ Use Authorization Policies for Complex Rules

---

# Common Mistakes

❌ Checking roles only in the View

Always protect the Controller.

---

❌ Hardcoding user names

Use Roles instead.

---

❌ Giving every user Admin role

Use least privilege.

---

❌ Using Authentication without Authorization

Logged-in users should still have restricted access.

---

# 🎯 Most Asked Interview Questions

## Q1. Difference Between Authentication and Authorization?

| Authentication | Authorization |
|----------------|---------------|
| Verifies identity | Controls access |
| Login | Permissions |

---

## Q2. What Is a Role?

A group of permissions assigned to users (e.g., Admin, Customer, Manager).

---

## Q3. What Is a Claim?

A key-value piece of information about a user, such as Email, Department, or Country.

---

## Q4. Difference Between Roles and Claims?

| Roles | Claims |
|--------|---------|
| Access Control | User Information |
| Admin | Email |
| Customer | Country |

---

## Q5. What Is `[Authorize]`?

An attribute that restricts access to authenticated users.

---

## Q6. How Do You Restrict Access to Admin Users?

```csharp
[Authorize(Roles="Admin")]
```

---

## Q7. What Is `[AllowAnonymous]`?

Allows access without authentication.

---

## Q8. What Is `UserManager`?

A service used to manage users, passwords, claims, and roles.

---

## Q9. What Is `RoleManager`?

A service used to create, update, delete, and manage roles.

---

## Q10. How Do You Check If a User Is in a Role?

```csharp
User.IsInRole("Admin")
```

or

```csharp
await userManager.IsInRoleAsync(user, "Admin");
```

---

## Q11. What Is a Policy in ASP.NET Core?

A reusable authorization rule based on roles, claims, or custom requirements.

---

## Q12. How Do You Secure an Admin Area?

- Require authentication
- Restrict access with roles
- Apply `[Authorize(Roles="Admin")]`
- Hide unauthorized UI
- Protect controller actions

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Authentication
- [x] Authorization
- [x] ASP.NET Core Identity
- [x] IdentityUser
- [x] IdentityRole
- [x] Roles
- [x] Claims
- [x] ClaimsPrincipal
- [x] UserManager
- [x] RoleManager
- [x] Authorize Attribute
- [x] AllowAnonymous
- [x] Role-Based Authorization
- [x] Claims-Based Authorization
- [x] Authorization Policies
- [x] Securing Admin Pages
- [x] Navigation Security
- [x] Admin Area Protection
- [x] Identity Database Tables

Mastering these topics will help you answer **95%+ of ASP.NET Core Role-Based Authorization, Identity, and Access Control interview questions** and build secure, enterprise-grade web applications.