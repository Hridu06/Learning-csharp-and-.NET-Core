# ASP.NET Core Model Binding & Model Validation (Easy to Advanced)

## 📌 What is a Model?

A Model is a C# class that represents application data and business rules.

Example:

```csharp
public class Product
{
    public int Id { get; set; }

    public string Name
    {
        get;
        set;
    }

    public decimal Price
    {
        get;
        set;
    }
}
```

Model represents:

```text
Product Data
```

---

# Why Models Are Important?

Models are used for:

- Forms
- APIs
- Database Operations
- Validation
- Data Transfer

---

# 🎯 Recommended Learning Order (Most Important First)

For ASP.NET Core MVC & Web API Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | What is a Model? | Very High |
| ⭐⭐⭐⭐⭐ | Model Binding | Very High |
| ⭐⭐⭐⭐⭐ | Data Annotations | Very High |
| ⭐⭐⭐⭐⭐ | ModelState | Very High |
| ⭐⭐⭐⭐⭐ | Server-Side Validation | Very High |
| ⭐⭐⭐⭐⭐ | [Required] | Very High |
| ⭐⭐⭐⭐⭐ | [Range] | Very High |
| ⭐⭐⭐⭐⭐ | [StringLength] | Very High |
| ⭐⭐⭐⭐⭐ | [EmailAddress] | Very High |
| ⭐⭐⭐⭐⭐ | [ApiController] Validation | Very High |
| ⭐⭐⭐⭐ | [FromBody] | High |
| ⭐⭐⭐⭐ | [FromRoute] | High |
| ⭐⭐⭐⭐ | [FromQuery] | High |
| ⭐⭐⭐⭐ | Custom Validation | High |
| ⭐⭐⭐⭐ | ValidationSummary | High |
| ⭐⭐⭐⭐ | Action Parameters | High |
| ⭐⭐⭐⭐ | Binding Sources | High |
| ⭐⭐⭐ | IValidatableObject | Medium |
| ⭐⭐⭐ | Custom Validation Attributes | Medium |
| ⭐⭐ | Custom Model Binder | Low |
| ⭐⭐ | Model Binder Providers | Low |

---

# Part 1: Models

# What is a Model? ⭐⭐⭐⭐⭐

Model is a class that represents application data.

Example:

```csharp
public class Employee
{
    public int Id { get; set; }

    public string Name
    {
        get;
        set;
    }

    public decimal Salary
    {
        get;
        set;
    }
}
```

---

# Model Responsibilities ⭐⭐⭐⭐⭐

- Store Data
- Validation Rules
- Business Rules
- Data Transfer

---

# Example Model

```csharp
public class Student
{
    public int Id { get; set; }

    public string Name
    {
        get;
        set;
    }

    public int Age
    {
        get;
        set;
    }
}
```

---

# Part 2: Model Binding

# 📌 What is Model Binding?

Model Binding automatically converts incoming HTTP request data into C# objects.

---

# Without Model Binding

```csharp
string name =
    Request.Form["Name"];

string email =
    Request.Form["Email"];
```

Manual work.

---

# With Model Binding

```csharp
public IActionResult Create(
    Student student)
{
}
```

ASP.NET automatically fills the object.

---

# How Model Binding Works ⭐⭐⭐⭐⭐

```text
HTTP Request
       ↓
Route Values
Query String
Form Data
JSON Body
       ↓
Model Binder
       ↓
C# Object
```

---

# Example Form Data

```html
<input name="Name" />
<input name="Age" />
```

User enters:

```text
Name = Sabbir

Age = 25
```

---

# Model

```csharp
public class Student
{
    public string Name
    {
        get;
        set;
    }

    public int Age
    {
        get;
        set;
    }
}
```

---

# Controller

```csharp
[HttpPost]
public IActionResult Create(
    Student student)
{
    return View();
}
```

ASP.NET creates:

```csharp
Student
{
    Name = "Sabbir",
    Age = 25
}
```

Automatically.

---

# Model Binding Sources ⭐⭐⭐⭐⭐

ASP.NET Core can bind from:

| Source | Example |
|----------|----------|
| Route | /products/1 |
| Query String | ?id=1 |
| Form Data | HTML Form |
| Request Body | JSON |
| Headers | Request Header |

---

# [FromRoute] ⭐⭐⭐⭐

Reads from URL.

---

# URL

```text
/products/10
```

Controller:

```csharp
public IActionResult Details(
    [FromRoute] int id)
{
    return Ok(id);
}
```

Result:

```text
10
```

---

# [FromQuery] ⭐⭐⭐⭐

Reads query string.

---

# URL

```text
/products?id=10
```

Controller:

```csharp
public IActionResult Search(
    [FromQuery] int id)
{
    return Ok(id);
}
```

---

# [FromBody] ⭐⭐⭐⭐

Reads JSON request body.

Request:

```json
{
  "name":"Laptop",
  "price":1000
}
```

Controller:

```csharp
public IActionResult Create(
    [FromBody] Product product)
{
}
```

---

# [FromForm] ⭐⭐⭐

Reads form data.

```csharp
public IActionResult Create(
    [FromForm] Product product)
{
}
```

---

# [FromHeader] ⭐⭐⭐

Reads header values.

```csharp
public IActionResult Get(
 [FromHeader] string apiKey)
{
}
```

---

# Simple Type Binding ⭐⭐⭐⭐⭐

```csharp
public IActionResult Get(
    int id)
{
}
```

---

# Complex Type Binding ⭐⭐⭐⭐⭐

```csharp
public IActionResult Create(
    Product product)
{
}
```

---

# Nested Model Binding ⭐⭐⭐⭐

```csharp
public class Address
{
    public string City
    {
        get;
        set;
    }
}

public class Employee
{
    public string Name
    {
        get;
        set;
    }

    public Address Address
    {
        get;
        set;
    }
}
```

---

# Part 3: Model Validation

# 📌 What is Validation?

Validation ensures incoming data is correct.

---

# Why Validation?

Prevent:

```text
Invalid Data
Missing Data
Incorrect Data
```

---

# Example

❌ Invalid

```json
{
  "name":"",
  "age":-10
}
```

---

# Valid

```json
{
  "name":"Sabbir",
  "age":25
}
```

---

# Data Annotations ⭐⭐⭐⭐⭐

Validation attributes applied to model properties.

---

# [Required] ⭐⭐⭐⭐⭐

Mandatory field.

```csharp
[Required]
public string Name
{
    get;
    set;
}
```

---

# Example

```csharp
public class Student
{
    [Required]
    public string Name
    {
        get;
        set;
    }
}
```

---

# [StringLength] ⭐⭐⭐⭐⭐

Limits string length.

```csharp
[StringLength(50)]
public string Name
{
    get;
    set;
}
```

---

# Minimum & Maximum Length

```csharp
[StringLength(
    50,
    MinimumLength = 3)]
```

---

# [MaxLength] ⭐⭐⭐⭐

```csharp
[MaxLength(100)]
```

---

# [MinLength] ⭐⭐⭐⭐

```csharp
[MinLength(3)]
```

---

# [Range] ⭐⭐⭐⭐⭐

Numeric validation.

```csharp
[Range(1,100)]
public int Age
{
    get;
    set;
}
```

---

# [EmailAddress] ⭐⭐⭐⭐⭐

```csharp
[EmailAddress]
public string Email
{
    get;
    set;
}
```

Valid:

```text
abc@gmail.com
```

---

# [Phone] ⭐⭐⭐⭐

```csharp
[Phone]
public string Phone
{
    get;
    set;
}
```

---

# [Url] ⭐⭐⭐

```csharp
[Url]
public string Website
{
    get;
    set;
}
```

---

# [RegularExpression] ⭐⭐⭐⭐

```csharp
[RegularExpression(
    @"^[A-Za-z]+$")]
```

Only letters.

---

# [Compare] ⭐⭐⭐⭐

Password confirmation.

```csharp
[Compare("Password")]
public string ConfirmPassword
{
    get;
    set;
}
```

---

# Complete Validation Example ⭐⭐⭐⭐⭐

```csharp
public class User
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

# ModelState ⭐⭐⭐⭐⭐

Stores validation results.

---

# Validation Check

```csharp
if(!ModelState.IsValid)
{
    return View(model);
}
```

---

# Example

```csharp
[HttpPost]
public IActionResult Create(
    User model)
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

# ModelState Errors ⭐⭐⭐⭐

```csharp
foreach(var error
    in ModelState.Values)
{
}
```

---

# Validation in API ⭐⭐⭐⭐⭐

```csharp
[ApiController]
```

Automatically validates models.

---

# Example

```csharp
[ApiController]
public class ProductsController
{
}
```

If validation fails:

```http
400 Bad Request
```

Automatically.

---

# ValidationSummary ⭐⭐⭐⭐

MVC View.

```html
<div asp-validation-summary="All">
</div>
```

Shows validation messages.

---

# Validation Message

```html
<span asp-validation-for="Name">
</span>
```

---

# Custom Validation ⭐⭐⭐⭐

Create custom validation logic.

---

# Example

```csharp
public class User
{
    [Required]

    public string Name
    {
        get;
        set;
    }

    public bool IsValid()
    {
        return Name != "Admin";
    }
}
```

---

# IValidatableObject ⭐⭐⭐

Advanced validation.

```csharp
public class User
    : IValidatableObject
{
    public IEnumerable<
        ValidationResult>
        Validate(
            ValidationContext context)
    {
        if(Age < 18)
        {
            yield return
            new ValidationResult(
                "Age must be 18");
        }
    }
}
```

---

# Custom Validation Attribute ⭐⭐⭐

```csharp
public class AdultAttribute
    : ValidationAttribute
{
}
```

Used for reusable validation.

---

# Validation Flow ⭐⭐⭐⭐⭐

```text
Request
   ↓
Model Binding
   ↓
Validation
   ↓
ModelState
   ↓
Controller Action
```

---

# Real-World Registration Example ⭐⭐⭐⭐⭐

Model:

```csharp
public class RegisterDto
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

Controller:

```csharp
[HttpPost]
public IActionResult Register(
    RegisterDto model)
{
    if(!ModelState.IsValid)
    {
        return BadRequest(
            ModelState);
    }

    return Ok();
}
```

---

# Common Mistakes

## Forgetting ModelState Check

❌ Wrong

```csharp
Save(model);
```

Without validation.

---

## Missing Validation Attributes

❌ Wrong

```csharp
public string Email
{
    get;
    set;
}
```

No validation.

---

## Trusting Client Validation Only

❌ Wrong

Always validate on server.

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Always validate on server

✅ Use DTOs

✅ Use Data Annotations

✅ Check ModelState

✅ Return meaningful errors

✅ Use [ApiController]

✅ Use custom validation when needed

---

# 🎯 Most Asked Interview Questions

## Q1. What is Model Binding?

Automatically converts request data into C# objects.

---

## Q2. What is Model Validation?

Process of verifying data correctness.

---

## Q3. What is ModelState?

Stores validation results.

---

## Q4. Difference Between [FromBody] and [FromQuery]?

| FromBody | FromQuery |
|-----------|-----------|
| JSON Body | Query String |

---

## Q5. What is [Required] Attribute?

Makes property mandatory.

---

## Q6. What is Data Annotation?

Validation attribute applied to model properties.

---

## Q7. What Happens If ModelState Is Invalid?

Validation errors exist.

---

## Q8. What Does [ApiController] Do?

Automatically validates models and returns 400 responses.

---

## Q9. Difference Between Client-Side and Server-Side Validation?

| Client Side | Server Side |
|------------|-------------|
| Browser | Server |
| Faster | More Secure |

---

## Q10. Why Is Server-Side Validation Important?

Client-side validation can be bypassed.

---

## Q11. What Is IValidatableObject?

Interface for custom model validation.

---

## Q12. Why Use DTO Instead of Entity?

Security and separation of concerns.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Models
- [x] Model Binding
- [x] Binding Sources
- [x] FromBody
- [x] FromRoute
- [x] FromQuery
- [x] Data Annotations
- [x] Required
- [x] StringLength
- [x] Range
- [x] EmailAddress
- [x] Compare
- [x] Validation
- [x] Server-Side Validation
- [x] ModelState
- [x] ApiController Validation
- [x] DTO Validation
- [x] Custom Validation

Mastering these topics will help you answer **95%+ of ASP.NET Core Model Binding & Validation interview questions** and build secure, production-ready MVC and Web API applications.