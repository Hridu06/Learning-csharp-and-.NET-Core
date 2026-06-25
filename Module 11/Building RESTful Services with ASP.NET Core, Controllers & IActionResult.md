# Building RESTful Services with ASP.NET Core, Controllers & IActionResult (Easy to Advanced)

---

# Part 1: REST API Fundamentals

# 📌 What is REST?

REST stands for:

```text
Representational State Transfer
```

REST is an architectural style used for building web services.

A REST API allows applications to communicate over HTTP.

Example:

```text
Mobile App
      ↓
REST API
      ↓
Database
```

---

# 📌 What is a RESTful Service?

A RESTful Service is a web service that follows REST principles.

Example:

```text
GET     /api/products
POST    /api/products
PUT     /api/products/1
DELETE  /api/products/1
```

---

# Why REST APIs Are Important?

Used in:

- Web Applications
- Mobile Apps
- React Applications
- Angular Applications
- Flutter Apps
- Microservices
- Cloud Applications

---

# 🎯 Recommended Learning Order (Most Important First)

For .NET Full Stack Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | What is REST? | Very High |
| ⭐⭐⭐⭐⭐ | Controller | Very High |
| ⭐⭐⭐⭐⭐ | ApiController Attribute | Very High |
| ⭐⭐⭐⭐⭐ | IActionResult | Very High |
| ⭐⭐⭐⭐⭐ | HTTP Methods | Very High |
| ⭐⭐⭐⭐⭐ | RESTful Endpoints | Very High |
| ⭐⭐⭐⭐⭐ | Route Attributes | Very High |
| ⭐⭐⭐⭐⭐ | GET API | Very High |
| ⭐⭐⭐⭐⭐ | POST API | Very High |
| ⭐⭐⭐⭐⭐ | PUT API | Very High |
| ⭐⭐⭐⭐⭐ | DELETE API | Very High |
| ⭐⭐⭐⭐⭐ | Model Binding | Very High |
| ⭐⭐⭐⭐⭐ | Validation | Very High |
| ⭐⭐⭐⭐⭐ | Status Codes | Very High |
| ⭐⭐⭐⭐ | ActionResult<T> | High |
| ⭐⭐⭐⭐ | DTO Pattern | High |
| ⭐⭐⭐⭐ | Dependency Injection | High |
| ⭐⭐⭐⭐ | Repository Pattern | High |
| ⭐⭐⭐⭐ | API Versioning | High |
| ⭐⭐⭐⭐ | Exception Handling | High |
| ⭐⭐⭐ | Content Negotiation | Medium |
| ⭐⭐⭐ | HATEOAS | Medium |
| ⭐⭐ | Custom Formatters | Low |

---

# REST Architecture ⭐⭐⭐⭐⭐

```text
Client
   ↓
HTTP Request
   ↓
ASP.NET Core API
   ↓
Database
   ↑
HTTP Response
   ↑
Client
```

---

# REST Principles ⭐⭐⭐⭐⭐

## 1. Client-Server

Client and server are independent.

---

## 2. Stateless

Each request contains all required information.

Server does not remember previous requests.

---

## 3. Uniform Interface

Consistent URLs and HTTP methods.

---

## 4. Resource-Based

Everything is a resource.

Example:

```text
Products
Users
Orders
Employees
```

---

# REST Resource Example ⭐⭐⭐⭐⭐

```text
/api/products
```

Represents:

```text
Products Resource
```

---

# RESTful URL Design ⭐⭐⭐⭐⭐

## Good

```text
/api/products
```

```text
/api/products/10
```

---

## Bad

```text
/api/getproducts
```

```text
/api/deleteproduct
```

---

# HTTP Methods ⭐⭐⭐⭐⭐

| Method | Purpose |
|----------|----------|
| GET | Read Data |
| POST | Create Data |
| PUT | Update Entire Resource |
| PATCH | Partial Update |
| DELETE | Delete Resource |

---

# REST CRUD Mapping ⭐⭐⭐⭐⭐

| Operation | HTTP Method |
|------------|------------|
| Create | POST |
| Read | GET |
| Update | PUT |
| Delete | DELETE |

---

# Sample Product Model

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

---

# Part 2: Controllers

# 📌 What is a Controller?

Controller handles HTTP requests and returns responses.

---

# Controller Example ⭐⭐⭐⭐⭐

```csharp
public class ProductsController
    : ControllerBase
{
}
```

---

# API Controller ⭐⭐⭐⭐⭐

```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsController
    : ControllerBase
{
}
```

---

# What Does [ApiController] Do?

Automatically:

✅ Validates ModelState

✅ Returns 400 Responses

✅ Improves Parameter Binding

✅ Better API Behavior

---

# Route Attribute ⭐⭐⭐⭐⭐

```csharp
[Route("api/products")]
```

API URL:

```text
/api/products
```

---

# ControllerBase vs Controller ⭐⭐⭐⭐⭐

| ControllerBase | Controller |
|---------------|------------|
| APIs | MVC Views |
| No View Support | Supports Views |
| Lightweight | Full MVC |

---

# Part 3: IActionResult

# 📌 What is IActionResult?

Represents the result of an action method.

---

# Example

```csharp
public IActionResult Get()
{
    return Ok();
}
```

---

# Why Use IActionResult?

Allows returning:

- JSON
- Status Codes
- Files
- Redirects
- Errors

---

# Common IActionResult Types ⭐⭐⭐⭐⭐

| Method | Status Code |
|----------|-------------|
| Ok() | 200 |
| Created() | 201 |
| NoContent() | 204 |
| BadRequest() | 400 |
| Unauthorized() | 401 |
| Forbid() | 403 |
| NotFound() | 404 |
| StatusCode() | Custom |

---

# Ok() ⭐⭐⭐⭐⭐

```csharp
return Ok(products);
```

Response:

```http
200 OK
```

---

# NotFound() ⭐⭐⭐⭐⭐

```csharp
return NotFound();
```

Response:

```http
404 Not Found
```

---

# BadRequest() ⭐⭐⭐⭐⭐

```csharp
return BadRequest(
    "Invalid Data");
```

Response:

```http
400 Bad Request
```

---

# CreatedAtAction() ⭐⭐⭐⭐⭐

Used after POST.

```csharp
return CreatedAtAction(
    nameof(GetById),
    new { id = product.Id },
    product);
```

Response:

```http
201 Created
```

---

# ActionResult<T> ⭐⭐⭐⭐

Recommended for APIs.

```csharp
public ActionResult<Product>
GetById(int id)
{
}
```

Benefits:

- Strongly Typed
- Better Swagger Support

---

# Part 4: Building CRUD REST APIs

# GET All Products ⭐⭐⭐⭐⭐

```csharp
[HttpGet]
public IActionResult Get()
{
    return Ok(products);
}
```

URL:

```text
GET /api/products
```

---

# GET Product By Id ⭐⭐⭐⭐⭐

```csharp
[HttpGet("{id}")]
public IActionResult GetById(
    int id)
{
    var product =
        products.FirstOrDefault(
            x => x.Id == id);

    if(product == null)
        return NotFound();

    return Ok(product);
}
```

---

# POST Product ⭐⭐⭐⭐⭐

```csharp
[HttpPost]
public IActionResult Create(
    Product product)
{
    products.Add(product);

    return CreatedAtAction(
        nameof(GetById),
        new { id = product.Id },
        product);
}
```

---

# PUT Product ⭐⭐⭐⭐⭐

```csharp
[HttpPut("{id}")]
public IActionResult Update(
    int id,
    Product product)
{
    return NoContent();
}
```

Response:

```http
204 No Content
```

---

# DELETE Product ⭐⭐⭐⭐⭐

```csharp
[HttpDelete("{id}")]
public IActionResult Delete(
    int id)
{
    return NoContent();
}
```

---

# PATCH API ⭐⭐⭐⭐

Partial update.

```csharp
[HttpPatch("{id}")]
```

Used for updating specific fields.

---

# Part 5: Model Binding

# 📌 What is Model Binding?

Automatically maps incoming request data to C# objects.

---

# Example

Request:

```json
{
  "name":"Laptop",
  "price":500
}
```

Controller:

```csharp
public IActionResult Create(
    Product product)
{
}
```

ASP.NET Core automatically creates Product object.

---

# [FromBody] ⭐⭐⭐⭐

```csharp
public IActionResult Create(
    [FromBody] Product product)
{
}
```

Reads data from request body.

---

# [FromRoute] ⭐⭐⭐⭐

```csharp
public IActionResult Get(
    [FromRoute] int id)
{
}
```

---

# [FromQuery] ⭐⭐⭐⭐

```csharp
public IActionResult Search(
    [FromQuery] string name)
{
}
```

URL:

```text
/api/products?name=laptop
```

---

# Part 6: Validation

# Data Annotations ⭐⭐⭐⭐⭐

```csharp
public class Product
{
    [Required]
    public string Name
    {
        get;
        set;
    }

    [Range(1,10000)]
    public decimal Price
    {
        get;
        set;
    }
}
```

---

# ModelState Validation ⭐⭐⭐⭐⭐

```csharp
if(!ModelState.IsValid)
{
    return BadRequest(
        ModelState);
}
```

---

# Part 7: Dependency Injection

# Register Service ⭐⭐⭐⭐⭐

```csharp
builder.Services.AddScoped<
    IProductService,
    ProductService>();
```

---

# Inject Service ⭐⭐⭐⭐⭐

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

# Part 8: DTO Pattern

# 📌 What is DTO?

DTO:

```text
Data Transfer Object
```

Used to transfer data safely.

---

# Product Entity

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

---

# Product DTO

```csharp
public class ProductDto
{
    public string Name
    {
        get;
        set;
    }
}
```

---

# Why DTO?

✅ Security

✅ Smaller Payload

✅ Better API Design

---

# Part 9: Exception Handling

# Global Exception Handling ⭐⭐⭐⭐

```csharp
app.UseExceptionHandler();
```

---

# Try Catch Example

```csharp
try
{
}
catch(Exception ex)
{
    return StatusCode(
        500,
        ex.Message);
}
```

---

# Part 10: API Versioning

```text
/api/v1/products

/api/v2/products
```

Used when API evolves.

---

# REST API Best Practices ⭐⭐⭐⭐⭐

✅ Use nouns

```text
/products
```

---

❌ Avoid verbs

```text
/getproducts
```

---

✅ Use proper status codes

```text
200
201
400
404
500
```

---

✅ Use DTOs

---

✅ Validate requests

---

✅ Use dependency injection

---

✅ Return meaningful responses

---

# Real-World Product API ⭐⭐⭐⭐⭐

```csharp
[ApiController]
[Route("api/products")]
public class ProductsController
    : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(
        int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Create(
        Product product)
    {
        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(
        int id,
        Product product)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(
        int id)
    {
        return NoContent();
    }
}
```

---

# 🎯 Most Asked Interview Questions

## Q1. What is REST?

Architectural style for building web services.

---

## Q2. What is a RESTful API?

API that follows REST principles.

---

## Q3. What is Controller?

Class that handles HTTP requests and responses.

---

## Q4. Difference Between Controller and ControllerBase?

| Controller | ControllerBase |
|------------|---------------|
| MVC Views | APIs |
| Full MVC | Lightweight |

---

## Q5. What is IActionResult?

Represents action method result.

---

## Q6. Difference Between IActionResult and ActionResult<T>?

| IActionResult | ActionResult<T> |
|--------------|----------------|
| Generic Response | Strongly Typed |

---

## Q7. Why Use [ApiController]?

Automatic validation and API behavior improvements.

---

## Q8. Difference Between PUT and PATCH?

| PUT | PATCH |
|------|------|
| Full Update | Partial Update |

---

## Q9. What is DTO?

Data Transfer Object used for transferring data safely.

---

## Q10. Most Common IActionResult Methods?

- Ok()
- CreatedAtAction()
- NotFound()
- BadRequest()
- NoContent()

---

## Q11. Why Use Dependency Injection?

Loose coupling and better testing.

---

## Q12. Why Use DTO Instead of Entity?

Security and better API design.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] REST Basics
- [x] REST Principles
- [x] HTTP Methods
- [x] RESTful URL Design
- [x] Controllers
- [x] ControllerBase
- [x] ApiController
- [x] IActionResult
- [x] ActionResult<T>
- [x] GET APIs
- [x] POST APIs
- [x] PUT APIs
- [x] DELETE APIs
- [x] Model Binding
- [x] Validation
- [x] DTO Pattern
- [x] Dependency Injection
- [x] Status Codes
- [x] Exception Handling

Mastering these topics will help you answer **95%+ of ASP.NET Core REST API interview questions** and build production-ready APIs.