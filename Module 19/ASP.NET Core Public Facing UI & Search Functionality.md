# ASP.NET Core Public Facing UI & Search Functionality (Easy to Advanced)

## Product Listing | Search Functionality | UI Design | Filtering | Pagination | Complete Interview Guide

---

# 📌 What is Public Facing UI?

Public Facing UI is the part of a website that is accessible to every visitor without logging in.

Examples:

- Home Page
- Product Listing
- Product Details
- Category List
- Search Products
- Contact Page
- About Page

Unlike the Admin Panel, the public UI is designed for customers or visitors.

---

# Example

```text
Customer

↓

Home Page

↓

Products

↓

Search

↓

Product Details

↓

Add to Cart
```

---

# Public UI vs Admin Panel

| Public UI | Admin Panel |
|------------|-------------|
| Customer Facing | Administrator Facing |
| Product List | Product Management |
| Product Details | Product CRUD |
| Search Products | Manage Products |
| View Categories | Manage Categories |
| No Login Required (Usually) | Login Required |

---

# Public Product Module Flow

```text
Visitor

↓

Home Page

↓

Product List

↓

Search

↓

Filter

↓

Product Details

↓

Add to Cart
```

---

# 🎯 Recommended Learning Order (Most Important First)

| Priority | Topic | Interview Frequency |
|-----------|--------------------------------------|----------------|
| ⭐⭐⭐⭐⭐ | Product Listing | Very High |
| ⭐⭐⭐⭐⭐ | Search Functionality | Very High |
| ⭐⭐⭐⭐⭐ | LINQ Search | Very High |
| ⭐⭐⭐⭐⭐ | Product Details | Very High |
| ⭐⭐⭐⭐⭐ | MVC Flow | Very High |
| ⭐⭐⭐⭐⭐ | ViewModel | Very High |
| ⭐⭐⭐⭐⭐ | Pagination | Very High |
| ⭐⭐⭐⭐⭐ | Category Filter | Very High |
| ⭐⭐⭐⭐⭐ | Search + Filter | Very High |
| ⭐⭐⭐⭐ | Sorting | High |
| ⭐⭐⭐⭐ | Responsive UI | High |
| ⭐⭐⭐⭐ | Bootstrap Cards | High |
| ⭐⭐⭐⭐ | Partial Views | High |
| ⭐⭐⭐⭐ | Async Queries | High |
| ⭐⭐⭐⭐ | Include() | High |
| ⭐⭐⭐ | AJAX Search | Medium |
| ⭐⭐⭐ | Live Search | Medium |
| ⭐⭐⭐ | Debouncing | Medium |
| ⭐⭐ | Full-Text Search | Low |

---

# Part 1 : Product Entity

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

    public string ImagePath
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

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

# Product ViewModel

```csharp
public class ProductVM
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

    public string ImagePath
    {
        get;
        set;
    }

    public string CategoryName
    {
        get;
        set;
    }
}
```

---

# Part 2 : Product Listing

## Home Controller

```csharp
public class HomeController
: Controller
{
    private readonly
    AppDbContext context;

    public HomeController(
    AppDbContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var products =
        context.Products

        .Include(x=>x.Category)

        .ToList();

        return View(products);
    }
}
```

---

# View

```csharp
@model List<Product>

@foreach(var item in Model)
{
<div>

<h3>

@item.Name

</h3>

<p>

@item.Price

</p>

</div>
}
```

---

# Bootstrap Product Card

```html
<div class="card">

<img

src="~/images/@item.ImagePath"

class="card-img-top"/>

<div class="card-body">

<h5>

@item.Name

</h5>

<p>

$@item.Price

</p>

<a

asp-action="Details"

asp-route-id="@item.Id"

class="btn btn-primary">

Details

</a>

</div>

</div>
```

---

# Product Grid

```html
<div class="row">

@foreach(var item in Model)
{
<div class="col-md-4">

Product Card

</div>
}

</div>
```

---

# Part 3 : Product Details

Controller

```csharp
public IActionResult Details(
int id)
{
    var product =
    context.Products

    .Include(x=>x.Category)

    .FirstOrDefault(
    x=>x.Id==id);

    return View(product);
}
```

---

# View

```html
<h2>

@Model.Name

</h2>

<img

src="~/images/@Model.ImagePath"/>

<p>

@Model.Description

</p>

<h3>

$@Model.Price

</h3>
```

---

# Part 4 : Search Functionality ⭐⭐⭐⭐⭐

## What is Search?

Search allows users to find products quickly.

Example

```text
Search

↓

Laptop

↓

Dell Laptop

HP Laptop

Lenovo Laptop
```

---

# Search Flow

```text
User Types

↓

Search Box

↓

Controller

↓

LINQ

↓

Filtered Products

↓

View
```

---

# Search Form

```html
<form

asp-action="Index"

method="get">

<input

type="text"

name="search"

placeholder="Search Product"/>

<button>

Search

</button>

</form>
```

---

# Controller

```csharp
public IActionResult Index(
string search)
{
    var products =
    context.Products

    .Include(x=>x.Category)

    .AsQueryable();

    if(!string.IsNullOrEmpty(search))
    {
        products =
        products.Where(

        x=>x.Name

        .Contains(search));
    }

    return View(

    products.ToList());
}
```

---

# Generated SQL

```sql
SELECT *

FROM Products

WHERE Name LIKE '%Laptop%'
```

---

# Search Ignore Case

```csharp
products.Where(

x=>x.Name

.ToLower()

.Contains(

search.ToLower()));
```

---

# Search Multiple Fields

```csharp
products.Where(

x=>x.Name.Contains(search)

||

x.Description.Contains(search)

||

x.Category.Name.Contains(search));
```

---

# Part 5 : Category Filter

Dropdown

```html
<select

name="categoryId">

<option>

All

</option>

</select>
```

---

Controller

```csharp
if(categoryId>0)
{
    products =
    products.Where(

    x=>x.CategoryId

    ==categoryId);
}
```

---

# Search + Category Filter ⭐⭐⭐⭐⭐

```csharp
if(!string.IsNullOrEmpty(search))
{
    products =
    products.Where(

    x=>x.Name

    .Contains(search));
}

if(categoryId>0)
{
    products =
    products.Where(

    x=>x.CategoryId

    ==categoryId);
}
```

---

# Part 6 : Sorting

Price Low to High

```csharp
products =
products.OrderBy(

x=>x.Price);
```

---

Price High to Low

```csharp
products =
products.OrderByDescending(

x=>x.Price);
```

---

Alphabetical

```csharp
products =
products.OrderBy(

x=>x.Name);
```

---

# Part 7 : Pagination

Without Pagination

```text
10000 Products
```

Very Slow

---

With Pagination

```text
10 Products

↓

Next

↓

Next
```

---

Example

```csharp
products

.Skip((page-1)*10)

.Take(10);
```

---

# Part 8 : Async Search

```csharp
await context.Products

.ToListAsync();
```

---

Async Search

```csharp
await products

.ToListAsync();
```

---

# Part 9 : Partial View

Create

```text
_ProductCard.cshtml
```

---

Use

```html
<partial

name="_ProductCard"

model="item"/>
```

Reusable UI.

---

# Part 10 : Live Search (AJAX)

Flow

```text
Typing

↓

AJAX

↓

Controller

↓

JSON

↓

Update Products
```

---

Benefits

- Faster
- Better UX
- No Page Refresh

---

# Part 11 : Responsive UI

Bootstrap

```html
<div class="col-lg-3

col-md-4

col-sm-6">
```

Works on

- Desktop
- Tablet
- Mobile

---

# Part 12 : Best UI Practices

Product Card

```text
Image

↓

Product Name

↓

Category

↓

Price

↓

Details Button
```

---

# Product Card Example

```html
<div class="card shadow">

<img

src="~/images/@item.ImagePath"/>

<div class="card-body">

<h5>

@item.Name

</h5>

<p>

@item.Category.Name

</p>

<h4>

$@item.Price

</h4>

<a

class="btn btn-success">

View Details

</a>

</div>

</div>
```

---

# Complete Product Listing Flow ⭐⭐⭐⭐⭐

```text
Visitor

↓

Open Home Page

↓

Controller

↓

EF Core

↓

Products

↓

Search

↓

Filter

↓

Pagination

↓

View
```

---

# Folder Structure

```text
Controllers

HomeController.cs

Models

Product.cs

ViewModels

ProductVM.cs

Views

Home

Index.cshtml

Details.cshtml

Shared

_ProductCard.cshtml

wwwroot

images
```

---

# Common Mistakes

❌ Searching after calling `ToList()`

```csharp
context.Products

.ToList()

.Where(...)
```

Always filter **before** `ToList()`.

---

❌ Forgetting `Include()`

Category becomes null.

---

❌ Using synchronous queries

Prefer:

```csharp
await ToListAsync();
```

---

❌ Searching only by Name

Search across

- Name
- Description
- Category

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Use ViewModels

✅ Use Async Queries

✅ Use Partial Views

✅ Search before `ToList()`

✅ Use Pagination

✅ Use Category Filter

✅ Use Responsive UI

✅ Use Bootstrap Cards

✅ Optimize Images

---

# 🎯 Most Asked Interview Questions

## Q1. What is a Public Facing UI?

The part of the application accessible to customers or visitors, such as the product list and product details pages.

---

## Q2. How Do You Display Products?

Retrieve products from the database using EF Core and pass them to a Razor View.

---

## Q3. How Do You Search Products?

Use LINQ with `Where()` and `Contains()`.

Example:

```csharp
products.Where(
x=>x.Name.Contains(search));
```

---

## Q4. Why Use `AsQueryable()`?

It allows building the query dynamically before executing it.

---

## Q5. Why Use `Include()`?

To eagerly load related data such as the product's category.

---

## Q6. Difference Between `FirstOrDefault()` and `SingleOrDefault()`?

| FirstOrDefault() | SingleOrDefault() |
|------------------|-------------------|
| Returns first match | Expects exactly one match |
| Doesn't fail if multiple matches exist | Throws an exception if multiple matches exist |

---

## Q7. Why Use Pagination?

Improves performance and reduces the amount of data displayed at one time.

---

## Q8. Why Search Before `ToList()`?

Because filtering in the database is much faster than filtering in memory.

---

## Q9. Why Use Partial Views?

To reuse UI components such as product cards across multiple pages.

---

## Q10. How Can Search Be Improved?

- Category filter
- Sorting
- Pagination
- AJAX live search
- Full-text search

---

## Q11. Why Use Async Database Queries?

To avoid blocking threads and improve application scalability.

---

## Q12. What Is the MVC Flow for Product Search?

```text
Browser

↓

Controller

↓

EF Core

↓

SQL Server

↓

View
```

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Public Facing UI
- [x] Product Listing
- [x] Product Details
- [x] Product Cards
- [x] Bootstrap Grid
- [x] MVC Flow
- [x] Search Functionality
- [x] LINQ Search
- [x] `Contains()`
- [x] `AsQueryable()`
- [x] `Include()`
- [x] Search by Multiple Fields
- [x] Category Filter
- [x] Sorting
- [x] Pagination
- [x] Async Queries
- [x] Partial Views
- [x] Responsive UI
- [x] Image Display
- [x] ViewModels

Mastering these topics will help you answer **95%+ of ASP.NET Core Public UI, Product Listing, and Search Functionality interview questions** and build a responsive, production-ready eCommerce front-end.