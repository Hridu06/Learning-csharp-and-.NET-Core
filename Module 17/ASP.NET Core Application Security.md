# ASP.NET Core Application Security (Easy to Advanced)

# Model Validation Best Practices | Client-Side & Server-Side Validation | Anti-Forgery Tokens | XSS & CSRF Protection

---

# 📌 What is Application Security?

Application Security is the process of protecting a web application from attacks, unauthorized access, data theft, and vulnerabilities.

Example attacks:

- SQL Injection
- Cross-Site Scripting (XSS)
- Cross-Site Request Forgery (CSRF)
- Authentication Bypass
- Broken Authorization
- Sensitive Data Exposure

---

# Why Application Security is Important?

Without Security

```text
User

↓

Website

↓

Hackers Can

• Steal Data
• Modify Data
• Delete Data
• Access Admin Panel
```

With Security

```text
User

↓

Validation

↓

Authentication

↓

Authorization

↓

Secure Database
```

---

# Security Layers

```text
Browser

↓

Client Validation

↓

Server Validation

↓

Authentication

↓

Authorization

↓

Business Logic

↓

Database

↓

SQL Server
```

---

# 🎯 Recommended Learning Order (Most Important First)

| Priority | Topic | Interview Frequency |
|-----------|---------------------------------------|----------------|
| ⭐⭐⭐⭐⭐ | Model Validation | Very High |
| ⭐⭐⭐⭐⭐ | Client-Side Validation | Very High |
| ⭐⭐⭐⭐⭐ | Server-Side Validation | Very High |
| ⭐⭐⭐⭐⭐ | ModelState | Very High |
| ⭐⭐⭐⭐⭐ | Data Annotations | Very High |
| ⭐⭐⭐⭐⭐ | Anti-Forgery Token | Very High |
| ⭐⭐⭐⭐⭐ | ValidateAntiForgeryToken | Very High |
| ⭐⭐⭐⭐⭐ | XSS | Very High |
| ⭐⭐⭐⭐⭐ | CSRF | Very High |
| ⭐⭐⭐⭐⭐ | SQL Injection Prevention | Very High |
| ⭐⭐⭐⭐ | HTML Encoding | High |
| ⭐⭐⭐⭐ | Input Validation | High |
| ⭐⭐⭐⭐ | Output Encoding | High |
| ⭐⭐⭐⭐ | Authentication | High |
| ⭐⭐⭐⭐ | Authorization | High |
| ⭐⭐⭐⭐ | Secure Cookies | High |
| ⭐⭐⭐⭐ | HTTPS | High |
| ⭐⭐⭐⭐ | Identity | High |
| ⭐⭐⭐ | Content Security Policy | Medium |
| ⭐⭐⭐ | Security Headers | Medium |
| ⭐⭐ | SameSite Cookies | Low |

---

# Part 1 : Model Validation

## What is Model Validation?

Validation ensures that incoming user data is correct before processing or storing it.

Without validation

```text
Age = -50

Email = abc

Password = ""
```

Database becomes invalid.

---

# Validation Flow ⭐⭐⭐⭐⭐

```text
User Input

↓

Model Binding

↓

Validation

↓

ModelState

↓

Save Database
```

---

# Example Model

```csharp
public class RegisterVM
{
    [Required]
    public string Name
    {
        get;
        set;
    }

    [EmailAddress]
    public string Email
    {
        get;
        set;
    }

    [Range(18,60)]
    public int Age
    {
        get;
        set;
    }

    [StringLength(
        20,
        MinimumLength = 6)]
    public string Password
    {
        get;
        set;
    }
}
```

---

# Checking ModelState ⭐⭐⭐⭐⭐

```csharp
[HttpPost]

public IActionResult Register(
RegisterVM model)
{
    if(!ModelState.IsValid)
    {
        return View(model);
    }

    return RedirectToAction(
    "Index");
}
```

---

# Common Data Annotations ⭐⭐⭐⭐⭐

| Attribute | Purpose |
|------------|----------|
| Required | Mandatory Field |
| StringLength | Min/Max Length |
| Range | Numeric Validation |
| EmailAddress | Email Validation |
| Phone | Phone Validation |
| Url | URL Validation |
| Compare | Compare Fields |
| RegularExpression | Custom Pattern |

---

# Example

```csharp
[Required]

[StringLength(50)]

public string Name
{
    get;
    set;
}
```

---

# Best Practices for Model Validation ⭐⭐⭐⭐⭐

✅ Validate every input

✅ Never trust browser validation

✅ Always check ModelState

✅ Use ViewModels

✅ Return meaningful validation messages

---

# Part 2 : Client-Side Validation

## What is Client-Side Validation?

Validation performed inside the browser using JavaScript.

Example

```text
Required Field

Email Format

Password Length
```

---

# Flow

```text
User

↓

Browser Validation

↓

Valid

↓

Submit Form
```

---

# Example

Model

```csharp
[Required]

[EmailAddress]

public string Email
{
    get;
    set;
}
```

View

```html
<input asp-for="Email"/>

<span
asp-validation-for="Email">
</span>
```

---

# Validation Scripts

```html
<partial
name="_ValidationScriptsPartial"/>
```

Enables jQuery validation.

---

# Advantages

- Faster
- Better User Experience
- Less Server Requests

---

# Limitations

❌ Can be disabled

❌ Can be bypassed

Never rely only on client validation.

---

# Part 3 : Server-Side Validation

## What is Server-Side Validation?

Validation performed on the server after the request is received.

---

# Flow ⭐⭐⭐⭐⭐

```text
Browser

↓

Server

↓

Validation

↓

Database
```

---

# Example

```csharp
if(!ModelState.IsValid)
{
    return View(model);
}
```

---

# Why Server Validation Is Mandatory?

A malicious user can send requests without using your form.

Example

```text
Postman

curl

Custom Scripts
```

These bypass browser validation.

---

# Client vs Server Validation ⭐⭐⭐⭐⭐

| Client Side | Server Side |
|-------------|-------------|
| Browser | Server |
| Faster | Secure |
| Can be bypassed | Cannot be bypassed |
| Better UX | Mandatory |

---

# Best Practice

Use BOTH

```text
Client Validation

+

Server Validation
```

---

# Part 4 : Anti-Forgery Token (CSRF Protection)

## What is CSRF?

CSRF stands for:

```text
Cross-Site Request Forgery
```

A CSRF attack tricks a logged-in user into sending an unwanted request.

---

# Example Attack

Victim logs into:

```text
Bank Website
```

Attacker sends hidden request:

```text
Transfer Money
```

Browser automatically sends cookies.

Request succeeds.

---

# How Anti-Forgery Works ⭐⭐⭐⭐⭐

```text
Server

↓

Generates Token

↓

Stores Token in Cookie

↓

Adds Token to Form

↓

User Submits Form

↓

Server Compares Tokens

↓

Valid → Continue

Invalid → Reject
```

---

# Anti-Forgery Token

View

```html
<form asp-action="Create">

@Html.AntiForgeryToken()

</form>
```

---

# MVC Form Tag Helper

Usually generated automatically.

```html
<form asp-action="Create">

</form>
```

---

# Controller

```csharp
[HttpPost]

[ValidateAntiForgeryToken]

public IActionResult Create(
Product model)
{
}
```

---

# Ignore Validation

Only when necessary.

```csharp
[IgnoreAntiforgeryToken]
```

Avoid unless required.

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Protect all POST actions

✅ Protect PUT actions

✅ Protect DELETE actions

✅ Use ValidateAntiForgeryToken

---

# Part 5 : XSS (Cross-Site Scripting)

## What is XSS?

XSS allows attackers to inject malicious JavaScript into a web page.

---

# Example Attack

User enters:

```html
<script>

alert("Hacked")

</script>
```

If displayed without encoding:

```html
<script>

alert("Hacked")

</script>
```

JavaScript executes.

---

# Types of XSS

- Stored XSS
- Reflected XSS
- DOM-based XSS

---

# Stored XSS

Stored inside database.

Example

```text
Comment

↓

Database

↓

Another User Opens Page

↓

Script Executes
```

---

# Reflected XSS

Comes from URL.

---

# DOM-Based XSS

Uses JavaScript to inject HTML.

---

# Preventing XSS ⭐⭐⭐⭐⭐

ASP.NET Core automatically HTML encodes Razor output.

Example

```csharp
@Model.Name
```

User input

```html
<script>

alert(1)

</script>
```

Rendered safely as text:

```html
&lt;script&gt;

alert(1)

&lt;/script&gt;
```

---

# Dangerous Code

Never use:

```csharp
@Html.Raw(Model.Name)
```

unless the content is trusted.

---

# Good

```csharp
@Model.Name
```

---

# HTML Encoding

Always encode user input.

ASP.NET Core does this automatically.

---

# Part 6 : SQL Injection

## Attack

Wrong

```csharp
string sql =

"SELECT *

FROM Users

WHERE Name='"

+ name +

"'";
```

---

# Prevention ⭐⭐⭐⭐⭐

Use Parameterized Queries.

```csharp
command.Parameters.AddWithValue(
"@Name",
name);
```

---

# EF Core

Safe

```csharp
context.Users

.Where(x=>x.Name==name)
```

---

# Part 7 : Secure Authentication

Use:

- ASP.NET Core Identity
- Password Hashing
- Secure Cookies

Never store:

```text
Plain Password
```

---

# Part 8 : Authorization

Authentication

```text
Who are you?
```

Authorization

```text
What can you access?
```

---

# Example

```csharp
[Authorize]
```

Admin Only

```csharp
[Authorize(
Roles="Admin")]
```

---

# Part 9 : HTTPS

Always redirect HTTP → HTTPS

Program.cs

```csharp
app.UseHttpsRedirection();
```

---

# Secure Cookies

```csharp
options.Cookie.HttpOnly = true;

options.Cookie.SecurePolicy =
CookieSecurePolicy.Always;
```

---

# SameSite Cookie

```csharp
options.Cookie.SameSite =
SameSiteMode.Strict;
```

Helps reduce CSRF risk.

---

# Part 10 : Content Security Policy (CSP)

Restricts which scripts and resources the browser can load.

Example Header

```text
Content-Security-Policy

default-src 'self'
```

Helps prevent XSS.

---

# Security Headers

Common Headers

```text
X-Content-Type-Options

X-Frame-Options

Referrer-Policy

Content-Security-Policy
```

---

# Security Flow ⭐⭐⭐⭐⭐

```text
User

↓

Client Validation

↓

Server Validation

↓

AntiForgery Check

↓

Authentication

↓

Authorization

↓

Business Logic

↓

Database
```

---

# Real World Registration Example

Model

```csharp
public class RegisterVM
{
    [Required]

    [EmailAddress]

    public string Email
    {
        get;
        set;
    }

    [StringLength(
        20,
        MinimumLength=6)]

    public string Password
    {
        get;
        set;
    }
}
```

---

View

```html
<form asp-action="Register">

<input asp-for="Email"/>

<input asp-for="Password"/>

<span
asp-validation-for="Email">
</span>

<button>

Register

</button>

</form>

<partial
name="_ValidationScriptsPartial"/>
```

---

Controller

```csharp
[HttpPost]

[ValidateAntiForgeryToken]

public IActionResult Register(
RegisterVM model)
{
    if(!ModelState.IsValid)
    {
        return View(model);
    }

    return RedirectToAction(
    "Index");
}
```

---

# Common Mistakes

❌ Trusting client-side validation only

❌ Not checking ModelState

❌ Missing AntiForgery token

❌ Using Html.Raw() with user input

❌ Building SQL with string concatenation

❌ Not using HTTPS

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Validate on both client and server

✅ Always check ModelState

✅ Use Data Annotations

✅ Use AntiForgeryToken

✅ Use HTTPS

✅ Encode output

✅ Use Parameterized Queries

✅ Use ASP.NET Core Identity

✅ Apply Authorization

---

# 🎯 Most Asked Interview Questions

## Q1. What is Model Validation?

Ensures incoming data is correct before processing or saving.

---

## Q2. Why Is Server-Side Validation Mandatory?

Because client-side validation can be bypassed using tools like Postman or custom HTTP requests.

---

## Q3. Difference Between Client-Side and Server-Side Validation?

| Client Side | Server Side |
|-------------|-------------|
| Browser | Server |
| Faster | More Secure |
| Can be bypassed | Cannot be bypassed |

---

## Q4. What Is CSRF?

Cross-Site Request Forgery—an attack where a user's browser is tricked into sending an unwanted authenticated request.

---

## Q5. How Do You Prevent CSRF?

- `@Html.AntiForgeryToken()`
- `[ValidateAntiForgeryToken]`
- SameSite cookies
- Secure authentication

---

## Q6. What Is XSS?

Cross-Site Scripting—an attack where malicious JavaScript is injected into a web page.

---

## Q7. How Does ASP.NET Core Prevent XSS?

By automatically HTML encoding Razor output.

---

## Q8. Why Is `Html.Raw()` Dangerous?

It disables HTML encoding and may allow XSS if the content comes from users.

---

## Q9. How Do You Prevent SQL Injection?

Use parameterized queries or EF Core LINQ queries.

---

## Q10. What Is the Difference Between Authentication and Authorization?

| Authentication | Authorization |
|---------------|---------------|
| Who are you? | What are you allowed to access? |

---

## Q11. Why Use HTTPS?

To encrypt communication between the client and the server.

---

## Q12. Why Use Anti-Forgery Tokens?

To verify that form submissions originate from your application and protect against CSRF attacks.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Model Validation
- [x] Data Annotations
- [x] ModelState
- [x] Client-Side Validation
- [x] Server-Side Validation
- [x] Validation Scripts
- [x] Anti-Forgery Token
- [x] ValidateAntiForgeryToken
- [x] CSRF
- [x] XSS
- [x] HTML Encoding
- [x] Html.Raw()
- [x] SQL Injection Prevention
- [x] Parameterized Queries
- [x] Authentication
- [x] Authorization
- [x] HTTPS
- [x] Secure Cookies
- [x] SameSite Cookies
- [x] Content Security Policy (CSP)

Mastering these topics will help you answer **95%+ of ASP.NET Core Application Security interview questions** and build secure, production-ready web applications.