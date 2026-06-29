# Entity Framework Core (EF Core) Fundamentals & Code First Approach (Easy to Advanced)

## Complete Guide with Interview Questions, Examples & Priority List

---

# 📌 What is Entity Framework Core (EF Core)?

Entity Framework Core (EF Core) is Microsoft's modern **Object Relational Mapper (ORM)** for .NET.

It allows developers to work with databases using **C# objects** instead of writing raw SQL queries.

Instead of writing:

```sql
SELECT *
FROM Products
```

You can write:

```csharp
var products = context.Products.ToList();
```

---

# What is ORM?

ORM stands for:

```text
Object Relational Mapper
```

ORM maps:

```text
Database Table

↓

C# Class
```

Example

Database

```text
Products Table
```

↓

C#

```csharp
public class Product
{
}
```

---

# Why Use EF Core?

Without EF Core

```text
C#

↓

ADO.NET

↓

SQL

↓

Database
```

With EF Core

```text
C#

↓

EF Core

↓

Database
```

No need to write SQL for most operations.

---

# Advantages of EF Core

- Less Code
- Faster Development
- Cross Platform
- LINQ Support
- Automatic Change Tracking
- Migrations
- Strongly Typed
- Easy CRUD Operations
- Supports Multiple Databases

---

# EF Core vs ADO.NET

| EF Core | ADO.NET |
|----------|----------|
| ORM | Raw Data Access |
| Less Code | More Code |
| Automatic Mapping | Manual Mapping |
| Easier | Faster |
| LINQ | SQL Queries |

---

# EF Core vs Dapper

| EF Core | Dapper |
|----------|---------|
| Full ORM | Micro ORM |
| Change Tracking | No Change Tracking |
| Slower | Faster |
| Rich Features | Lightweight |

---

# 🎯 Recommended Learning Order (Most Important First)

| Priority | Topic | Interview Frequency |
|-----------|------------------------------------|----------------|
| ⭐⭐⭐⭐⭐ | What is EF Core? | Very High |
| ⭐⭐⭐⭐⭐ | ORM | Very High |
| ⭐⭐⭐⭐⭐ | DbContext | Very High |
| ⭐⭐⭐⭐⭐ | DbSet | Very High |
| ⭐⭐⭐⭐⭐ | Entity Class | Very High |
| ⭐⭐⭐⭐⭐ | Code First | Very High |
| ⭐⭐⭐⭐⭐ | Connection String | Very High |
| ⭐⭐⭐⭐⭐ | Migrations | Very High |
| ⭐⭐⭐⭐⭐ | CRUD Operations | Very High |
| ⭐⭐⭐⭐⭐ | LINQ Queries | Very High |
| ⭐⭐⭐⭐⭐ | Change Tracking | Very High |
| ⭐⭐⭐⭐⭐ | SaveChanges() | Very High |
| ⭐⭐⭐⭐⭐ | Relationships | Very High |
| ⭐⭐⭐⭐⭐ | Navigation Properties | Very High |
| ⭐⭐⭐⭐ | Fluent API | High |
| ⭐⭐⭐⭐ | Data Annotations | High |
| ⭐⭐⭐⭐ | Lazy Loading | High |
| ⭐⭐⭐⭐ | Eager Loading | High |
| ⭐⭐⭐⭐ | Explicit Loading | High |
| ⭐⭐⭐⭐ | AsNoTracking() | High |
| ⭐⭐⭐⭐ | Repository Pattern | High |
| ⭐⭐⭐⭐ | Transactions | High |
| ⭐⭐⭐ | Seed Data | Medium |
| ⭐⭐⭐ | Shadow Properties | Medium |
| ⭐⭐⭐ | Global Query Filters | Medium |
| ⭐⭐ | Compiled Queries | Low |

---

# Part 1 : What is Code First?

## Three EF Core Approaches

| Approach | Description |
|----------|-------------|
| Code First | Database created from C# classes |
| Database First | C# classes generated from existing database |
| Model First | (Old EF, not supported in EF Core) |

---

# Code First Flow ⭐⭐⭐⭐⭐

```text
Create C# Classes

↓

DbContext

↓

Migration

↓

Update Database

↓

Database Created
```

---

# Example

C# Class

↓

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

↓

Migration

↓

SQL Server Table

---

# Project Structure

```text
Models/

Product.cs

ApplicationDbContext.cs

Migrations/

Program.cs

appsettings.json
```

---

# Part 2 : Installing EF Core

NuGet Packages

```text
Microsoft.EntityFrameworkCore

Microsoft.EntityFrameworkCore.SqlServer

Microsoft.EntityFrameworkCore.Tools
```

---

# Package Manager

```powershell
Install-Package
Microsoft.EntityFrameworkCore.SqlServer
```

---

# CLI

```bash
dotnet add package
Microsoft.EntityFrameworkCore.SqlServer
```

---

# Part 3 : Entity Class

Example

```csharp
public class Product
{
    public int Id
    {
        get;
        set;
    }

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

This becomes:

```text
Products Table
```

---

# Part 4 : DbContext ⭐⭐⭐⭐⭐

## What is DbContext?

The bridge between your application and the database.

Responsibilities

- Database Connection
- Change Tracking
- CRUD Operations
- LINQ Queries
- Transactions

---

# Example

```csharp
public class AppDbContext
: DbContext
{
    public AppDbContext(
DbContextOptions<AppDbContext>
options)
: base(options)
{
}

public DbSet<Product>
Products
{
get;
set;
}
}
```

---

# What is DbSet? ⭐⭐⭐⭐⭐

Represents a database table.

```csharp
public DbSet<Product>
Products
{
get;
set;
}
```

Equivalent to

```text
Products Table
```

---

# Part 5 : Connection String

appsettings.json

```json
{
  "ConnectionStrings":
  {
    "DefaultConnection":
    "Server=(localdb)\\MSSQLLocalDB;
    Database=ShopDB;
    Trusted_Connection=True;
    TrustServerCertificate=True;"
  }
}
```

---

# Register DbContext

Program.cs

```csharp
builder.Services
.AddDbContext<AppDbContext>(
options =>
options.UseSqlServer(
builder.Configuration
.GetConnectionString(
"DefaultConnection")));
```

---

# Part 6 : Migrations ⭐⭐⭐⭐⭐

## What is Migration?

Migration tracks database schema changes.

Example

```text
Add Product Class

↓

Migration

↓

Database Updated
```

---

# Create Migration

Package Manager

```powershell
Add-Migration
InitialCreate
```

CLI

```bash
dotnet ef migrations add InitialCreate
```

---

# Update Database

Package Manager

```powershell
Update-Database
```

CLI

```bash
dotnet ef database update
```

---

# Remove Migration

```powershell
Remove-Migration
```

---

# Migration Folder

```text
Migrations/

20260601_InitialCreate.cs

ApplicationDbContextModelSnapshot.cs
```

---

# Part 7 : CRUD Operations ⭐⭐⭐⭐⭐

# Insert

```csharp
Product product =
new Product
{
Name="Laptop",
Price=80000
};

context.Products.Add(product);

context.SaveChanges();
```

---

# Read

```csharp
var products =
context.Products.ToList();
```

---

# Find By Id

```csharp
var product =
context.Products.Find(1);
```

---

# Update

```csharp
var product =
context.Products.Find(1);

product.Price = 90000;

context.SaveChanges();
```

---

# Delete

```csharp
var product =
context.Products.Find(1);

context.Products.Remove(product);

context.SaveChanges();
```

---

# SaveChanges() ⭐⭐⭐⭐⭐

Persists changes to database.

Without

```csharp
SaveChanges();
```

Nothing is saved.

---

# Async Version

```csharp
await context.SaveChangesAsync();
```

---

# Part 8 : LINQ Queries ⭐⭐⭐⭐⭐

Get All

```csharp
context.Products.ToList();
```

---

First

```csharp
context.Products.First();
```

---

FirstOrDefault

```csharp
context.Products
.FirstOrDefault(
x=>x.Id==1);
```

---

Where

```csharp
context.Products
.Where(
x=>x.Price>5000);
```

---

OrderBy

```csharp
context.Products
.OrderBy(
x=>x.Price);
```

---

OrderByDescending

```csharp
context.Products
.OrderByDescending(
x=>x.Price);
```

---

Count

```csharp
context.Products.Count();
```

---

Any

```csharp
context.Products
.Any(
x=>x.Price>5000);
```

---

# Part 9 : Relationships ⭐⭐⭐⭐⭐

One Category

↓

Many Products

---

Category

```csharp
public class Category
{
public int Id
{
get;
set;
}

public ICollection<Product>
Products
{
get;
set;
}
}
```

---

Product

```csharp
public class Product
{
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
}
```

---

# Navigation Properties ⭐⭐⭐⭐⭐

```csharp
public Category Category
{
get;
set;
}
```

Collection

```csharp
public ICollection<Product>
Products
{
get;
set;
}
```

---

# Part 10 : Data Annotations ⭐⭐⭐⭐

```csharp
[Required]

[StringLength(100)]

[Range(1,100)]

[Key]

[ForeignKey("Category")]
```

---

# Fluent API ⭐⭐⭐⭐

```csharp
protected override
void OnModelCreating(
ModelBuilder builder)
{
builder.Entity<Product>()
.Property(x=>x.Name)
.HasMaxLength(100);
}
```

---

# Part 11 : Loading Data

# Lazy Loading

Loads when accessed.

---

# Eager Loading ⭐⭐⭐⭐

```csharp
context.Products

.Include(
x=>x.Category)

.ToList();
```

---

# Explicit Loading

```csharp
context.Entry(product)

.Reference(
x=>x.Category)

.Load();
```

---

# Part 12 : Change Tracking ⭐⭐⭐⭐⭐

EF automatically tracks changes.

```csharp
product.Price=90000;

context.SaveChanges();
```

Only changed values are updated.

---

# Disable Tracking

```csharp
context.Products

.AsNoTracking()

.ToList();
```

Better performance for read-only queries.

---

# Part 13 : Transactions

```csharp
using var transaction =
context.Database
.BeginTransaction();
```

Commit

```csharp
transaction.Commit();
```

Rollback

```csharp
transaction.Rollback();
```

---

# Part 14 : Seed Data ⭐⭐⭐

```csharp
builder.Entity<Category>()

.HasData(

new Category
{
Id=1,
Name="Electronics"
});
```

---

# EF Core Lifecycle ⭐⭐⭐⭐⭐

```text
Create Entity

↓

DbContext

↓

Migration

↓

Database

↓

CRUD

↓

SaveChanges()
```

---

# Real World Example ⭐⭐⭐⭐⭐

Model

```csharp
public class Product
{
public int Id
{
get;
set;
}

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

DbContext

```csharp
public class AppDbContext
: DbContext
{
public DbSet<Product>
Products
{
get;
set;
}
}
```

---

Insert

```csharp
context.Products.Add(

new Product
{
Name="Laptop",
Price=80000
});

context.SaveChanges();
```

---

Read

```csharp
var products =
context.Products.ToList();
```

---

# Common Mistakes

## Forgetting SaveChanges()

Nothing saved.

---

## Forgetting Migration

Database never updated.

---

## Missing DbSet

Table not created.

---

## Not Registering DbContext

Application crashes.

---

## Using Tracking for Read-Only Queries

Use

```csharp
AsNoTracking()
```

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Use Code First

✅ Use Migrations

✅ Use Async Methods

✅ Use LINQ

✅ Use Navigation Properties

✅ Use DTOs

✅ Use AsNoTracking() for read-only queries

✅ Use Dependency Injection

---

# 🎯 Most Asked Interview Questions

## Q1. What is EF Core?

Entity Framework Core is Microsoft's ORM for .NET.

---

## Q2. What is ORM?

Object Relational Mapper.

Maps database tables to C# classes.

---

## Q3. What is Code First?

Database is generated from C# classes.

---

## Q4. What is DbContext?

Bridge between application and database.

---

## Q5. What is DbSet?

Represents a database table.

---

## Q6. What is Migration?

Tracks schema changes and updates the database.

---

## Q7. Difference Between Add() and SaveChanges()?

| Add() | SaveChanges() |
|--------|---------------|
| Tracks Entity | Saves to Database |

---

## Q8. What are Navigation Properties?

Properties representing relationships between entities.

---

## Q9. Difference Between Lazy Loading and Eager Loading?

| Lazy | Eager |
|-------|--------|
| Loads later | Loads immediately |

---

## Q10. What is AsNoTracking()?

Disables change tracking for better read performance.

---

## Q11. Difference Between Data Annotations and Fluent API?

| Data Annotations | Fluent API |
|------------------|------------|
| Attributes | Code Configuration |

---

## Q12. Why Use EF Core Instead of ADO.NET?

Less code, automatic mapping, LINQ support, migrations, and higher developer productivity.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] EF Core Basics
- [x] ORM
- [x] Code First
- [x] Entity Class
- [x] DbContext
- [x] DbSet
- [x] Connection String
- [x] Registering DbContext
- [x] Migrations
- [x] Update Database
- [x] CRUD Operations
- [x] SaveChanges()
- [x] LINQ Queries
- [x] Relationships
- [x] Navigation Properties
- [x] Data Annotations
- [x] Fluent API
- [x] Lazy Loading
- [x] Eager Loading
- [x] Explicit Loading
- [x] Change Tracking
- [x] AsNoTracking()
- [x] Transactions
- [x] Seed Data

Mastering these topics will help you answer **95%+ of Entity Framework Core and Code First interview questions** and build scalable, production-ready ASP.NET Core applications.