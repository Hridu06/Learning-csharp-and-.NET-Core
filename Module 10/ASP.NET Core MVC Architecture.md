# ASP.NET Core MVC Architecture (Easy to Advanced)

## 📌 What is MVC?

MVC stands for:

```text
M = Model
V = View
C = Controller
```

MVC is an architectural pattern that separates an application into three parts:

```text
Model      → Data & Business Logic
View       → User Interface (UI)
Controller → Handles Requests & Responses
```

---

# Why MVC?

Before MVC:

```text
UI + Business Logic + Database Logic
all mixed together
```

Problems:

- Difficult Maintenance
- Difficult Testing
- Difficult Scaling
- Code Duplication

MVC solves these issues by separating responsibilities.

---

# 🎯 Recommended Learning Order (Most Important First)

For ASP.NET Core MVC Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | What is MVC? | Very High |
| ⭐⭐⭐⭐⭐ | MVC Request Lifecycle | Very High |
| ⭐⭐⭐⭐⭐ | Model | Very High |
| ⭐⭐⭐⭐⭐ | View | Very High |
| ⭐⭐⭐⭐⭐ | Controller | Very High |
| ⭐⭐⭐⭐⭐ | Routing | Very High |
| ⭐⭐⭐⭐⭐ | IActionResult | Very High |
| ⭐⭐⭐⭐⭐ | ViewData | Very High |
| ⭐⭐⭐⭐⭐ | ViewBag | Very High |
| ⭐⭐⭐⭐⭐ | Strongly Typed View | Very High |
| ⭐⭐⭐⭐⭐ | Dependency Injection in MVC | Very High |
| ⭐⭐⭐⭐⭐ | Model Binding | Very High |
| ⭐⭐⭐⭐⭐ | Validation | Very High |
| ⭐⭐⭐⭐ | Layout Pages | High |
| ⭐⭐⭐⭐ | Partial Views | High |
| ⭐⭐⭐⭐ | TempData | High |
| ⭐⭐⭐⭐ | Action Filters | High |
| ⭐⭐⭐⭐ | Areas | High |
| ⭐⭐⭐⭐ | Tag Helpers | High |
| ⭐⭐⭐ | Custom Model Binder | Medium |
| ⭐⭐⭐ | View Components | Medium |
| ⭐⭐ | Razor Compilation | Low |

---

# MVC Architecture Diagram ⭐⭐⭐⭐⭐

```text
Browser
   ↓
Controller
   ↓
Model
   ↓
Database
   ↑
Model
   ↑
Controller
   ↑
View
   ↑
Browser
```

---

# MVC Request Lifecycle ⭐⭐⭐⭐⭐

Most Important Interview Topic

```text
User Request
      ↓
Routing
      ↓
Controller
      ↓
Action Method
      ↓
Model
      ↓
Database
      ↓
Controller
      ↓
View
      ↓
Response
```

---

# Example Flow

User visits:

```text
/products
```

Routing:

```text
ProductsController
```

Action:

```csharp
Index()
```

Gets data:

```csharp
ProductService
```

Returns:

```csharp
Index.cshtml
```

Browser receives HTML.

---

# Project Structure ⭐⭐⭐⭐⭐

```text
Controllers/
Models/
Views/
wwwroot/
Program.cs
appsettings.json
```

---

# 1. Model ⭐⭐⭐⭐⭐

## What is a Model?

Model represents:

```text
Data
Business Logic
Validation Rules
```

Example:

```csharp
public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
}
```

---

# Model Responsibilities

- Store Data
- Validation
- Business Rules
- Database Mapping

---

# 2. View ⭐⭐⭐⭐⭐

## What is a View?

View is responsible for displaying data.

Extension:

```text
.cshtml
```

Example:

```html
<h1>Products</h1>
```

---

# Razor Syntax ⭐⭐⭐⭐⭐

```csharp
@DateTime.Now
```

Output:

```html
6/24/2026
```

---

# Loop Example

```csharp
@foreach(var product in Model)
{
    <p>@product.Name</p>
}
```

---

# 3. Controller ⭐⭐⭐⭐⭐

## What is Controller?

Controller handles:

```text
Requests
Business Logic Calls
Response Generation
```

Example:

```csharp
public class ProductsController
    : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
```

---

# Controller Naming Convention ⭐⭐⭐⭐⭐

```text
ProductsController
```

URL:

```text
/products
```

---

# Action Methods ⭐⭐⭐⭐⭐

```csharp
public IActionResult Index()
{
    return View();
}
```

Action:

```text
Index
```

---

# Routing ⭐⭐⭐⭐⭐

Maps URL to controller.

---

# Default Route

```csharp
app.MapControllerRoute(
    name: "default",
    pattern:
    "{controller=Home}/{action=Index}/{id?}");
```

---

# Example

URL:

```text
/products/details/5
```

Maps to:

```csharp
ProductsController

Details(5)
```

---

# Attribute Routing ⭐⭐⭐⭐

```csharp
[Route("products")]
public class ProductsController
    : Controller
{
}
```

---

# IActionResult ⭐⭐⭐⭐⭐

Represents action result.

---

# Common Results

| Result | Purpose |
|----------|----------|
| View() | Return View |
| Json() | Return JSON |
| Redirect() | Redirect URL |
| RedirectToAction() | Redirect Action |
| NotFound() | 404 |
| BadRequest() | 400 |
| Ok() | 200 |

---

# View Result

```csharp
return View();
```

---

# Json Result

```csharp
return Json(product);
```

---

# Redirect Result

```csharp
return RedirectToAction(
    "Index");
```

---

# Strongly Typed View ⭐⭐⭐⭐⭐

Best practice.

Controller:

```csharp
public IActionResult Index()
{
    Product product =
        new Product
        {
            Name = "Laptop"
        };

    return View(product);
}
```

View:

```csharp
@model Product

<h1>@Model.Name</h1>
```

Output:

```html
Laptop
```

---

# ViewData ⭐⭐⭐⭐⭐

Pass data from controller to view.

Controller:

```csharp
ViewData["Title"] =
    "Product List";
```

View:

```csharp
<h1>@ViewData["Title"]</h1>
```

---

# ViewBag ⭐⭐⭐⭐⭐

Dynamic object.

Controller:

```csharp
ViewBag.Title =
    "Products";
```

View:

```csharp
<h1>@ViewBag.Title</h1>
```

---

# ViewBag vs ViewData ⭐⭐⭐⭐

| ViewBag | ViewData |
|----------|-----------|
| Dynamic | Dictionary |
| Easier | Older |

---

# TempData ⭐⭐⭐⭐

Stores data between requests.

```csharp
TempData["Message"] =
    "Saved";
```

Used after redirects.

---

# Model Binding ⭐⭐⭐⭐⭐

Automatically maps request data to objects.

---

# Example

Form:

```html
<input name="Name" />
```

Controller:

```csharp
[HttpPost]
public IActionResult Create(
    Product product)
{
    return View();
}
```

ASP.NET automatically fills object.

---

# Validation ⭐⭐⭐⭐⭐

Most asked MVC topic.

---

# Data Annotations

```csharp
public class Product
{
    [Required]
    public string Name
    {
        get;
        set;
    }

    [Range(1,1000)]
    public decimal Price
    {
        get;
        set;
    }
}
```

---

# ModelState Validation

```csharp
if(!ModelState.IsValid)
{
    return View(model);
}
```

---

# Dependency Injection in MVC ⭐⭐⭐⭐⭐

Register:

```csharp
builder.Services
.AddScoped<
    IProductService,
    ProductService>();
```

Inject:

```csharp
public class ProductsController
{
    private readonly
        IProductService service;

    public ProductsController(
        IProductService service)
    {
        this.service = service;
    }
}
```

---

# Layout Pages ⭐⭐⭐⭐

Master template.

```text
_Layout.cshtml
```

Contains:

```html
Header
Navbar
Footer
```

Shared across pages.

---

# RenderBody()

```html
@RenderBody()
```

Displays page content.

---

# Partial Views ⭐⭐⭐⭐

Reusable UI pieces.

```text
_ProductCard.cshtml
```

Usage:

```csharp
<partial name="_ProductCard" />
```

---

# Tag Helpers ⭐⭐⭐⭐

Server-side HTML helpers.

Example:

```html
<input asp-for="Name" />
```

Generates proper HTML.

---

# Areas ⭐⭐⭐⭐

Used for large applications.

Structure:

```text
Areas/
    Admin/
        Controllers/
        Views/
```

---

# Action Filters ⭐⭐⭐⭐

Execute before/after action.

Example:

```csharp
public class LogFilter
    : ActionFilterAttribute
{
}
```

Used for:

- Logging
- Validation
- Authorization

---

# View Components ⭐⭐⭐

Reusable components.

Example:

```text
Shopping Cart Widget
Notification Widget
```

---

# Real-World MVC CRUD Example ⭐⭐⭐⭐⭐

## Model

```csharp
public class Product
{
    public int Id { get; set; }

    public string Name
    {
        get;
        set;
    }
}
```

---

## Controller

```csharp
public class ProductsController
    : Controller
{
    public IActionResult Index()
    {
        List<Product> products =
            new()
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop"
                }
            };

        return View(products);
    }
}
```

---

## View

```csharp
@model List<Product>

@foreach(var product in Model)
{
    <p>@product.Name</p>
}
```

Output:

```html
Laptop
```

---

# MVC vs Web API ⭐⭐⭐⭐⭐

| MVC | Web API |
|------|---------|
| Returns HTML | Returns JSON |
| Uses Views | No Views |
| Browser UI | Mobile/SPA Backend |

---

# MVC vs Razor Pages ⭐⭐⭐⭐

| MVC | Razor Pages |
|------|-------------|
| Controller + View | Page Focused |
| Larger Apps | Simpler Apps |

---

# Common Mistakes

## Returning View Without Model

❌ Wrong

```csharp
return View();
```

When view expects:

```csharp
@model Product
```

---

## Not Checking ModelState

❌ Wrong

```csharp
Save(model);
```

Without validation.

---

## Business Logic Inside Controller

❌ Wrong

```csharp
public IActionResult Save()
{
    // 200 lines
}
```

Use Services.

---

# MVC Best Practices ⭐⭐⭐⭐⭐

✅ Thin Controllers

✅ Business Logic in Services

✅ Use Dependency Injection

✅ Use Strongly Typed Views

✅ Validate Models

✅ Use ViewModels

✅ Use Partial Views

✅ Follow SOLID Principles

---

# 🎯 Most Asked Interview Questions

## Q1. What is MVC?

Architectural pattern:

```text
Model
View
Controller
```

---

## Q2. What is the Responsibility of Model?

Stores data and business rules.

---

## Q3. What is the Responsibility of View?

Displays UI.

---

## Q4. What is the Responsibility of Controller?

Handles requests and responses.

---

## Q5. Explain MVC Request Lifecycle.

```text
Request
→ Routing
→ Controller
→ Model
→ View
→ Response
```

---

## Q6. Difference Between ViewBag and ViewData?

| ViewBag | ViewData |
|----------|-----------|
| Dynamic | Dictionary |

---

## Q7. What is Model Binding?

Automatically maps request data to C# objects.

---

## Q8. What is ModelState?

Stores validation state.

---

## Q9. What is IActionResult?

Represents action response.

---

## Q10. What is Dependency Injection in MVC?

Providing services through constructor injection.

---

## Q11. Difference Between MVC and Web API?

| MVC | Web API |
|------|---------|
| HTML | JSON |

---

## Q12. Why Use Strongly Typed Views?

Compile-time checking and IntelliSense support.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] MVC Architecture
- [x] MVC Request Lifecycle
- [x] Model
- [x] View
- [x] Controller
- [x] Routing
- [x] IActionResult
- [x] Strongly Typed Views
- [x] ViewData
- [x] ViewBag
- [x] TempData
- [x] Model Binding
- [x] Validation
- [x] ModelState
- [x] Dependency Injection
- [x] Layout Pages
- [x] Partial Views
- [x] Tag Helpers
- [x] Action Filters
- [x] Areas

Mastering these topics will help you answer **95%+ of ASP.NET Core MVC interview questions** and build complete MVC applications in real-world .NET projects.