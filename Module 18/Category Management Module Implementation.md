# Category Management Module Implementation (ASP.NET Core MVC)

## Category CRUD | Admin Area Concept | Easy to Advanced

---

# 📌 What is Category Management?

Category Management is a module that allows administrators to manage product categories.

Example Categories:

```text
Electronics

Fashion

Books

Sports

Groceries
```

In an eCommerce system, products are organized using categories.

Example

```text
Electronics
    ├── Laptop
    ├── Mobile
    ├── Monitor

Fashion
    ├── Shirt
    ├── Shoes
```

---

# Why Category Management?

Without categories:

```text
10000 Products

↓

Hard to Search

Hard to Filter

Poor User Experience
```

With categories:

```text
Electronics

↓

Laptop

↓

Dell Laptop
```

Easy navigation.

---

# Real World Architecture

```text
Admin

↓

Category Module

↓

Controller

↓

Service

↓

Repository

↓

EF Core

↓

SQL Server
```

---

# Module Features

Admin can:

- Create Category
- View Categories
- Update Category
- Delete Category
- Search Category
- Activate/Deactivate Category

---

# 🎯 Recommended Learning Order (Most Important First)

| Priority | Topic | Interview Frequency |
|-----------|-------------------------------|----------------|
| ⭐⭐⭐⭐⭐ | CRUD Operations | Very High |
| ⭐⭐⭐⭐⭐ | MVC CRUD Flow | Very High |
| ⭐⭐⭐⭐⭐ | Category Entity | Very High |
| ⭐⭐⭐⭐⭐ | DbContext | Very High |
| ⭐⭐⭐⭐⭐ | Controller | Very High |
| ⭐⭐⭐⭐⭐ | Views | Very High |
| ⭐⭐⭐⭐⭐ | Model Validation | Very High |
| ⭐⭐⭐⭐⭐ | Admin Area | Very High |
| ⭐⭐⭐⭐⭐ | Entity Relationships | Very High |
| ⭐⭐⭐⭐ | ViewModel | High |
| ⭐⭐⭐⭐ | Repository Pattern | High |
| ⭐⭐⭐⭐ | Dependency Injection | High |
| ⭐⭐⭐⭐ | TempData | High |
| ⭐⭐⭐⭐ | AntiForgery Token | High |
| ⭐⭐⭐⭐ | Pagination | High |
| ⭐⭐⭐⭐ | Search | High |
| ⭐⭐⭐⭐ | Soft Delete | High |
| ⭐⭐⭐ | Image Upload | Medium |
| ⭐⭐⭐ | Audit Fields | Medium |
| ⭐⭐⭐ | Async CRUD | Medium |

---

# Part 1 : Category Entity

```csharp
public class Category
{
    public int Id
    {
        get;
        set;
    }

    [Required]

    [StringLength(100)]
    public string Name
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    public bool IsActive
    {
        get;
        set;
    }

    public DateTime CreatedAt
    {
        get;
        set;
    }
}
```

---

# Database Table

```text
Categories

----------------------------

Id

Name

Description

IsActive

CreatedAt
```

---

# Relationship

One Category

↓

Many Products

```text
Category

↓

Products
```

---

Category

```csharp
public ICollection<Product>
Products
{
    get;
    set;
}
```

---

Product

```csharp
public int CategoryId
{
    get;
    set;
}

public Category Category
{
    get;
    set;
}
```

---

# Part 2 : DbContext

```csharp
public class AppDbContext
: DbContext
{
    public DbSet<Category>
    Categories
    {
        get;
        set;
    }
}
```

---

# Migration

```powershell
Add-Migration
AddCategory
```

Update Database

```powershell
Update-Database
```

---

# CRUD Flow ⭐⭐⭐⭐⭐

```text
Browser

↓

Controller

↓

DbContext

↓

SQL Server

↓

View
```

---

# Part 3 : Category Controller

```csharp
public class CategoriesController
: Controller
{
    private readonly
    AppDbContext context;

    public CategoriesController(
    AppDbContext context)
    {
        this.context = context;
    }
}
```

---

# Part 4 : Read Categories

```csharp
public IActionResult Index()
{
    var categories =
        context.Categories
        .ToList();

    return View(categories);
}
```

---

View

```csharp
@model List<Category>

<table>

@foreach(var item in Model)
{
<tr>

<td>@item.Name</td>

</tr>
}

</table>
```

---

# Part 5 : Create Category

GET

```csharp
public IActionResult Create()
{
    return View();
}
```

---

POST

```csharp
[HttpPost]

[ValidateAntiForgeryToken]

public IActionResult Create(
Category category)
{
    if(ModelState.IsValid)
    {
        context.Categories.Add(category);

        context.SaveChanges();

        return RedirectToAction(
        "Index");
    }

    return View(category);
}
```

---

View

```html
<form asp-action="Create">

<input asp-for="Name"/>

<textarea
asp-for="Description">
</textarea>

<button>

Save

</button>

</form>
```

---

# Part 6 : Edit Category

GET

```csharp
public IActionResult Edit(
int id)
{
    var category =
    context.Categories.Find(id);

    return View(category);
}
```

---

POST

```csharp
[HttpPost]

public IActionResult Edit(
Category category)
{
    context.Categories.Update(
    category);

    context.SaveChanges();

    return RedirectToAction(
    "Index");
}
```

---

# Part 7 : Delete Category

GET

```csharp
public IActionResult Delete(
int id)
{
    var category =
    context.Categories.Find(id);

    return View(category);
}
```

---

POST

```csharp
[HttpPost]

public IActionResult DeleteConfirmed(
int id)
{
    var category =
    context.Categories.Find(id);

    context.Categories.Remove(category);

    context.SaveChanges();

    return RedirectToAction(
    "Index");
}
```

---

# CRUD Summary

| Operation | HTTP |
|------------|------|
| Read | GET |
| Create | GET + POST |
| Edit | GET + POST |
| Delete | GET + POST |

---

# Part 8 : Model Validation ⭐⭐⭐⭐⭐

```csharp
public class Category
{
    [Required]

    [StringLength(100)]

    public string Name
    {
        get;
        set;
    }
}
```

Controller

```csharp
if(!ModelState.IsValid)
{
    return View(model);
}
```

---

# Validation Message

```html
<span
asp-validation-for="Name">
</span>
```

---

# Validation Summary

```html
<div
asp-validation-summary="All">
</div>
```

---

# Part 9 : TempData ⭐⭐⭐⭐

Controller

```csharp
TempData["Success"] =
"Category Saved";
```

View

```html
@if(TempData["Success"] != null)
{
<p>

@TempData["Success"]

</p>
}
```

---

# Part 10 : Admin Area

## What is Area?

Area divides a large application into modules.

Example

```text
Admin

Customer

Vendor
```

Each has its own

- Controllers
- Views
- Models

---

# Folder Structure

```text
Areas

    Admin

        Controllers

        Views

        Models

Home

Controllers
```

---

# Area Controller

```csharp
[Area("Admin")]

public class CategoriesController
: Controller
{
}
```

---

# URL

```text
/Admin/Categories
```

---

# Area Route

Program.cs

```csharp
app.MapControllerRoute(

name:"areas",

pattern:
"{area:exists}/{controller=Home}/{action=Index}/{id?}");
```

---

# Why Use Areas?

Large Projects

Example

```text
Admin

Employee

Accounts

Inventory

Sales

HRM
```

Each becomes independent.

---

# Part 11 : Search

```csharp
public IActionResult Index(
string search)
{
    var data =
    context.Categories

    .Where(x=>

    x.Name.Contains(search))

    .ToList();

    return View(data);
}
```

---

# Part 12 : Pagination

Instead of

```text
10000 Categories
```

Show

```text
10 Per Page
```

---

# Part 13 : Soft Delete ⭐⭐⭐⭐

Instead of deleting

```text
DELETE
```

Update

```text
IsDeleted = true
```

Model

```csharp
public bool IsDeleted
{
    get;
    set;
}
```

Query

```csharp
context.Categories

.Where(x=>

!x.IsDeleted)
```

---

# Part 14 : Async CRUD

```csharp
await context.Categories

.ToListAsync();
```

---

Insert

```csharp
await context.SaveChangesAsync();
```

---

# Part 15 : Repository Pattern

Interface

```csharp
public interface
ICategoryRepository
{
    Task<List<Category>>
    GetAllAsync();
}
```

---

Implementation

```csharp
public class CategoryRepository
: ICategoryRepository
{
}
```

---

# Part 16 : ViewModel

Never expose Entity directly.

Create

```csharp
CategoryCreateVM
```

Example

```csharp
public class CategoryCreateVM
{
    public string Name
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }
}
```

---

# Complete MVC Flow ⭐⭐⭐⭐⭐

```text
Admin Clicks

↓

Category/Create

↓

Controller

↓

Validation

↓

DbContext

↓

SaveChanges()

↓

SQL Server

↓

Redirect

↓

Index View
```

---

# Real World Folder Structure

```text
Areas

└── Admin

    ├── Controllers

    │      CategoriesController.cs

    ├── Views

    │      Categories

    │          Index.cshtml

    │          Create.cshtml

    │          Edit.cshtml

    │          Delete.cshtml

    ├── ViewModels

    ├── Services

    └── Repositories
```

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Use Areas for Admin Modules

✅ Use ViewModels

✅ Validate ModelState

✅ Use Async CRUD

✅ Use Dependency Injection

✅ Use Repository Pattern

✅ Use Soft Delete

✅ Use TempData for Success Messages

✅ Protect Forms with AntiForgeryToken

---

# Common Mistakes

❌ Using Entity directly in Views

❌ Forgetting ModelState validation

❌ Forgetting SaveChanges()

❌ Using synchronous database calls

❌ Hardcoding SQL inside Controller

❌ No authorization for Admin pages

---

# 🎯 Most Asked Interview Questions

## Q1. What is CRUD?

| Letter | Meaning |
|---------|---------|
| C | Create |
| R | Read |
| U | Update |
| D | Delete |

---

## Q2. Why Do We Need Category Management?

To organize products, improve search, filtering, and maintain a scalable product catalog.

---

## Q3. What is an Admin Area?

A feature in ASP.NET Core MVC that organizes large applications into separate functional modules such as Admin, Customer, and Vendor.

---

## Q4. How Do You Create an Area?

1. Create an `Areas` folder.
2. Add a subfolder (e.g., `Admin`).
3. Add Controllers and Views.
4. Decorate controllers with:

```csharp
[Area("Admin")]
```

5. Configure area routing in `Program.cs`.

---

## Q5. Why Use Areas?

- Better project organization
- Separation of concerns
- Easier maintenance
- Scalable architecture

---

## Q6. Difference Between Soft Delete and Hard Delete?

| Soft Delete | Hard Delete |
|--------------|-------------|
| Marks record as deleted | Removes record permanently |
| Data can be restored | Data is lost permanently |

---

## Q7. Why Use ViewModels Instead of Entities?

- Prevent over-posting attacks
- Hide sensitive fields
- Improve security
- Better separation of UI and database models

---

## Q8. Why Check `ModelState.IsValid`?

To ensure only valid data is saved to the database.

---

## Q9. Why Use Async CRUD?

- Better scalability
- Non-blocking database operations
- Improved application performance

---

## Q10. What Is the Relationship Between Category and Product?

One-to-Many

```text
One Category

↓

Many Products
```

---

## Q11. What Is `TempData` Used For?

To pass data (such as success messages) between two requests, typically after a redirect.

---

## Q12. Why Use the Repository Pattern?

To separate data access logic from business logic, making the application easier to test and maintain.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Category CRUD
- [x] MVC CRUD Flow
- [x] Category Entity
- [x] DbContext
- [x] DbSet
- [x] CRUD Controllers
- [x] CRUD Views
- [x] Model Validation
- [x] ModelState
- [x] Admin Areas
- [x] Area Routing
- [x] ViewModels
- [x] Repository Pattern
- [x] Dependency Injection
- [x] TempData
- [x] AntiForgeryToken
- [x] Search
- [x] Pagination
- [x] Soft Delete
- [x] Async CRUD
- [x] One-to-Many Relationship (Category → Products)

Mastering these topics will help you answer **95%+ of Category Management Module and ASP.NET Core MVC CRUD interview questions** and implement a complete, production-ready Admin module.