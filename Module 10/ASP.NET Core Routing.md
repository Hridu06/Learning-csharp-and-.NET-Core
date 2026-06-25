# ASP.NET Core Routing (Easy to Advanced)

## 📌 What is Routing?

Routing is the process of mapping an incoming URL to a specific Controller and Action Method.

Without Routing:

```text
Browser URL
     ↓
ASP.NET Core
     ❌
Doesn't know which controller/action to execute
```

With Routing:

```text
URL
 ↓
Routing
 ↓
Controller
 ↓
Action
 ↓
Response
```

---

# Why Routing is Needed? ⭐⭐⭐⭐⭐

Routing helps ASP.NET Core:

- Identify Controller
- Identify Action Method
- Extract Parameters
- Process Requests
- Generate Responses

Example:

```text
/products/details/5
```

Routing decides:

```text
Controller = ProductsController

Action = Details

Id = 5
```

---

# Real World Example ⭐⭐⭐⭐⭐

URL:

```text
https://localhost:5001/products/details/10
```

Routing maps to:

```csharp
ProductsController

Details(10)
```

Action:

```csharp
public IActionResult Details(int id)
{
}
```

Here:

```text
id = 10
```

---

# 🎯 Recommended Learning Order (Most Important First)

For ASP.NET Core MVC Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | What is Routing? | Very High |
| ⭐⭐⭐⭐⭐ | Conventional Routing | Very High |
| ⭐⭐⭐⭐⭐ | Attribute Routing | Very High |
| ⭐⭐⭐⭐⭐ | Route Parameters | Very High |
| ⭐⭐⭐⭐⭐ | Route Templates | Very High |
| ⭐⭐⭐⭐⭐ | URL → Controller Mapping | Very High |
| ⭐⭐⭐⭐⭐ | Route Constraints | Very High |
| ⭐⭐⭐⭐⭐ | Optional Parameters | Very High |
| ⭐⭐⭐⭐⭐ | Default Route | Very High |
| ⭐⭐⭐⭐ | Named Routes | High |
| ⭐⭐⭐⭐ | Areas Routing | High |
| ⭐⭐⭐⭐ | Endpoint Routing | High |
| ⭐⭐⭐⭐ | Route Order | High |
| ⭐⭐⭐⭐ | URL Generation | High |
| ⭐⭐⭐ | Catch-All Routes | Medium |
| ⭐⭐⭐ | Route Data | Medium |
| ⭐⭐⭐ | Route Values | Medium |
| ⭐⭐ | Custom Route Constraints | Low |
| ⭐⭐ | Dynamic Routing | Low |

---

# How Routing Works ⭐⭐⭐⭐⭐

```text
User Request
      ↓
Routing Engine
      ↓
Controller
      ↓
Action
      ↓
Response
```

---

# Example Flow

User Request:

```text
/products/index
```

Routing:

```text
ProductsController
```

Action:

```text
Index()
```

Response:

```html
Product List
```

---

# Conventional Routing ⭐⭐⭐⭐⭐

Most common routing approach.

Defined in:

```csharp
Program.cs
```

---

# Default Route

```csharp
app.MapControllerRoute(
    name: "default",
    pattern:
    "{controller=Home}/{action=Index}/{id?}");
```

---

# Route Template Breakdown ⭐⭐⭐⭐⭐

```text
{controller=Home}
```

Default controller:

```text
HomeController
```

---

```text
{action=Index}
```

Default action:

```text
Index()
```

---

```text
{id?}
```

Optional parameter.

---

# Example URLs

### URL 1

```text
/
```

Maps to:

```csharp
HomeController

Index()
```

---

### URL 2

```text
/products
```

Maps to:

```csharp
ProductsController

Index()
```

---

### URL 3

```text
/products/details/5
```

Maps to:

```csharp
ProductsController

Details(5)
```

---

# Example Controller ⭐⭐⭐⭐⭐

```csharp
public class ProductsController
    : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Details(
        int id)
    {
        return View();
    }
}
```

---

# Attribute Routing ⭐⭐⭐⭐⭐

Routing directly on controller/action.

---

# Controller Route

```csharp
[Route("products")]
public class ProductsController
    : Controller
{
}
```

URL:

```text
/products
```

---

# Action Route

```csharp
[Route("details")]
public IActionResult Details()
{
}
```

Full URL:

```text
/products/details
```

---

# Complete Example ⭐⭐⭐⭐⭐

```csharp
[Route("products")]
public class ProductsController
    : Controller
{
    [Route("")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("details")]
    public IActionResult Details()
    {
        return View();
    }
}
```

URLs:

```text
/products

/products/details
```

---

# HTTP Method Routing ⭐⭐⭐⭐⭐

Common in Web APIs.

---

# GET Route

```csharp
[HttpGet]
public IActionResult Get()
{
}
```

---

# POST Route

```csharp
[HttpPost]
public IActionResult Create()
{
}
```

---

# Route Parameters ⭐⭐⭐⭐⭐

Most asked routing topic.

---

# Parameter Example

```csharp
[Route("products/{id}")]
```

Controller:

```csharp
public IActionResult Details(
    int id)
{
    return Content(
        $"Product {id}");
}
```

URL:

```text
/products/10
```

Output:

```text
Product 10
```

---

# Multiple Parameters ⭐⭐⭐⭐

```csharp
[Route(
"products/{category}/{id}")]
```

Action:

```csharp
public IActionResult Details(
    string category,
    int id)
{
}
```

URL:

```text
/products/laptop/10
```

---

# Optional Parameters ⭐⭐⭐⭐⭐

```csharp
[Route("products/{id?}")]
```

URLs:

```text
/products

/products/10
```

Both valid.

---

# Default Route Values ⭐⭐⭐⭐

```csharp
[Route("products/{id=1}")]
```

URL:

```text
/products
```

Result:

```text
id = 1
```

---

# Route Constraints ⭐⭐⭐⭐⭐

Restrict parameter types.

---

# Integer Constraint

```csharp
[Route("products/{id:int}")]
```

Valid:

```text
/products/10
```

Invalid:

```text
/products/abc
```

---

# String Length Constraint

```csharp
[Route(
"products/{name:minlength(3)}")]
```

---

# Range Constraint

```csharp
[Route(
"products/{id:range(1,100)}")]
```

---

# Common Constraints ⭐⭐⭐⭐⭐

| Constraint | Example |
|------------|----------|
| int | {id:int} |
| bool | {value:bool} |
| datetime | {date:datetime} |
| guid | {id:guid} |
| minlength | minlength(3) |
| maxlength | maxlength(10) |
| range | range(1,100) |

---

# Route Data ⭐⭐⭐

Contains route values.

```csharp
var id =
    RouteData.Values["id"];
```

---

# Query String vs Route Parameter ⭐⭐⭐⭐⭐

## Route Parameter

```text
/products/10
```

Action:

```csharp
public IActionResult Details(
    int id)
{
}
```

---

## Query String

```text
/products?id=10
```

Action:

```csharp
public IActionResult Details(
    int id)
{
}
```

---

# Query String vs Route Parameter

| Route Parameter | Query String |
|----------------|--------------|
| SEO Friendly | Flexible |
| Cleaner URLs | Multiple Filters |

---

# Endpoint Routing ⭐⭐⭐⭐

Introduced in ASP.NET Core.

```csharp
app.MapControllers();
```

or

```csharp
app.MapControllerRoute();
```

---

# Named Routes ⭐⭐⭐⭐

```csharp
[Route(
"products/{id}",
Name = "ProductDetails")]
```

Generate URLs later.

---

# URL Generation ⭐⭐⭐⭐

```csharp
Url.RouteUrl(
    "ProductDetails",
    new { id = 10 });
```

Output:

```text
/products/10
```

---

# Areas Routing ⭐⭐⭐⭐

Large application organization.

Structure:

```text
Areas
 └── Admin
      └── Controllers
```

---

# Area Route

```csharp
[Area("Admin")]
```

URL:

```text
/Admin/Users
```

---

# Catch-All Route ⭐⭐⭐

Captures everything.

```csharp
[Route("{*path}")]
```

URL:

```text
/products/mobile/apple
```

Captured as:

```text
path
```

---

# Route Order ⭐⭐⭐⭐

Routing evaluates in order.

Specific routes first.

---

# Wrong

```csharp
/products/{id}

/products/details
```

---

# Correct

```csharp
/products/details

/products/{id}
```

---

# MVC Routing Example ⭐⭐⭐⭐⭐

Program.cs

```csharp
app.MapControllerRoute(
    name: "default",
    pattern:
    "{controller=Home}/{action=Index}/{id?}");
```

Controller:

```csharp
public class HomeController
    : Controller
{
    public IActionResult Index()
    {
        return Content(
            "Home");
    }
}
```

URL:

```text
/
```

Output:

```text
Home
```

---

# API Routing Example ⭐⭐⭐⭐⭐

```csharp
[ApiController]
[Route("api/products")]
public class ProductsController
    : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Get(
        int id)
    {
        return Ok(id);
    }
}
```

URL:

```text
/api/products/5
```

Response:

```json
5
```

---

# Common Routing Mistakes

## Missing Route Attribute

❌

```csharp
[HttpGet]
```

Without controller route.

---

## Wrong Parameter Type

```csharp
/products/abc
```

For:

```csharp
{id:int}
```

Returns:

```text
404
```

---

## Wrong Route Order

More generic route placed first.

---

# Routing Best Practices ⭐⭐⭐⭐⭐

✅ Use attribute routing for APIs

✅ Use conventional routing for MVC

✅ Use route constraints

✅ Use meaningful URLs

✅ Keep URLs RESTful

✅ Avoid deeply nested routes

---

# 🎯 Most Asked Interview Questions

## Q1. What is Routing?

Routing maps URL to controller and action.

---

## Q2. Why Is Routing Needed?

To determine which controller/action handles request.

---

## Q3. Difference Between Conventional and Attribute Routing?

| Conventional | Attribute |
|-------------|------------|
| Centralized | Controller Based |
| Program.cs | Attributes |

---

## Q4. What is Route Parameter?

Value passed through URL.

```text
/products/10
```

---

## Q5. What is Optional Parameter?

```csharp
{id?}
```

---

## Q6. What is Route Constraint?

Restricts parameter type.

```csharp
{id:int}
```

---

## Q7. Difference Between Query String and Route Parameter?

```text
/products/10

/products?id=10
```

---

## Q8. What is Endpoint Routing?

Modern routing system in ASP.NET Core.

---

## Q9. What is Named Route?

Route with unique name used for URL generation.

---

## Q10. Which Routing Is Preferred for APIs?

```text
Attribute Routing
```

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Routing Basics
- [x] Conventional Routing
- [x] Attribute Routing
- [x] Route Templates
- [x] Route Parameters
- [x] Optional Parameters
- [x] Route Constraints
- [x] Default Routes
- [x] Query String
- [x] Endpoint Routing
- [x] URL Generation
- [x] Named Routes
- [x] Areas Routing
- [x] Route Order
- [x] API Routing

Mastering these topics will help you answer **95%+ of ASP.NET Core Routing interview questions** and implement routing correctly in MVC and Web API applications.