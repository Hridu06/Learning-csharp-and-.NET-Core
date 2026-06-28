# ASP.NET Core Razor Views, Razor Syntax, Strongly Typed Views, ViewData, ViewBag & HTML Helpers (Easy to Advanced)

---

# 📌 What is Razor?

Razor is the View Engine used in ASP.NET Core MVC.

It allows you to write:

- HTML
- C#
- Razor Syntax

inside the same file.

Extension:

```text
.cshtml
```

---

# Example

```html
<h1>Hello</h1>

<p>@DateTime.Now</p>
```

Output:

```html
Hello

6/25/2026
```

---

# What is a Razor View?

A Razor View is an HTML page that contains Razor syntax and is responsible for generating the UI.

Example:

```text
Views/
    Home/
        Index.cshtml
```

---

# MVC Flow

```text
Browser
      ↓
Controller
      ↓
Model
      ↓
View (.cshtml)
      ↓
HTML Response
```

---

# Why Razor?

- Dynamic HTML
- Clean Syntax
- Easy Data Binding
- IntelliSense Support
- Secure (Automatic HTML Encoding)

---

# 🎯 Recommended Learning Order (Most Important First)

| Priority | Topic | Interview Frequency |
|-----------|----------------------------|----------------|
| ⭐⭐⭐⭐⭐ | Razor Basics | Very High |
| ⭐⭐⭐⭐⭐ | Razor Syntax | Very High |
| ⭐⭐⭐⭐⭐ | Strongly Typed Views | Very High |
| ⭐⭐⭐⭐⭐ | @model Directive | Very High |
| ⭐⭐⭐⭐⭐ | ViewData | Very High |
| ⭐⭐⭐⭐⭐ | ViewBag | Very High |
| ⭐⭐⭐⭐⭐ | HTML Helpers | Very High |
| ⭐⭐⭐⭐⭐ | Model Binding with Views | Very High |
| ⭐⭐⭐⭐ | Layout Pages | High |
| ⭐⭐⭐⭐ | Partial Views | High |
| ⭐⭐⭐⭐ | Tag Helpers | High |
| ⭐⭐⭐⭐ | TempData | High |
| ⭐⭐⭐⭐ | Sections | High |
| ⭐⭐⭐⭐ | _ViewImports.cshtml | High |
| ⭐⭐⭐⭐ | _ViewStart.cshtml | High |
| ⭐⭐⭐ | Display Templates | Medium |
| ⭐⭐⭐ | Editor Templates | Medium |
| ⭐⭐⭐ | View Components | Medium |
| ⭐⭐ | Custom HTML Helpers | Low |

---

# Part 1 : Razor Basics

# Razor Syntax ⭐⭐⭐⭐⭐

Razor syntax begins with:

```csharp
@
```

Example:

```html
<p>@DateTime.Now</p>
```

---

# Razor Expression

```csharp
@Model.Name
```

---

# Razor Code Block

```csharp
@{
    var name = "Sabbir";
}
```

Display:

```html
<p>@name</p>
```

Output

```text
Sabbir
```

---

# Variables

```csharp
@{
    int age = 25;
}

<p>@age</p>
```

Output

```text
25
```

---

# If Statement ⭐⭐⭐⭐⭐

```csharp
@if(Model.Price > 500)
{
    <p>Expensive</p>
}
else
{
    <p>Cheap</p>
}
```

---

# Switch Statement

```csharp
@switch(Model.Status)
{
    case "Active":
        <p>Active</p>
        break;

    default:
        <p>Unknown</p>
        break;
}
```

---

# For Loop ⭐⭐⭐⭐⭐

```csharp
@for(int i=1;i<=5;i++)
{
    <p>@i</p>
}
```

Output

```text
1
2
3
4
5
```

---

# Foreach Loop ⭐⭐⭐⭐⭐

```csharp
@foreach(var item in Model)
{
    <p>@item.Name</p>
}
```

---

# Comments

```csharp
@* Razor Comment *@
```

Not rendered in HTML.

---

# Part 2 : Strongly Typed Views

# What is Strongly Typed View? ⭐⭐⭐⭐⭐

A strongly typed view receives a specific model object from the controller.

Benefits

- IntelliSense
- Compile-Time Checking
- Type Safety

---

# Product Model

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

# Controller

```csharp
public IActionResult Index()
{
    Product product = new Product
    {
        Id = 1,
        Name = "Laptop",
        Price = 80000
    };

    return View(product);
}
```

---

# View

```csharp
@model Product

<h2>@Model.Name</h2>

<p>@Model.Price</p>
```

Output

```text
Laptop

80000
```

---

# List Model

Controller

```csharp
public IActionResult Index()
{
    List<Product> products =
        new()
        {
            new Product
            {
                Id = 1,
                Name = "Laptop"
            },

            new Product
            {
                Id = 2,
                Name = "Phone"
            }
        };

    return View(products);
}
```

---

View

```csharp
@model List<Product>

@foreach(var item in Model)
{
    <p>@item.Name</p>
}
```

Output

```text
Laptop

Phone
```

---

# Strongly Typed vs Weakly Typed

| Strongly Typed | Weakly Typed |
|---------------|-------------|
| @model | ViewBag/ViewData |
| IntelliSense | No IntelliSense |
| Compile-Time Checking | Runtime Errors |
| Recommended | Small Data Only |

---

# Part 3 : ViewData

# What is ViewData? ⭐⭐⭐⭐⭐

ViewData passes data from Controller to View.

Internally it is:

```text
Dictionary<string, object>
```

---

# Controller

```csharp
ViewData["Title"] =
    "Product List";
```

---

# View

```html
<h2>@ViewData["Title"]</h2>
```

Output

```text
Product List
```

---

# Multiple ViewData

```csharp
ViewData["Name"] = "Sabbir";

ViewData["Age"] = 25;
```

View

```html
<p>@ViewData["Name"]</p>

<p>@ViewData["Age"]</p>
```

---

# Type Casting

```csharp
string name =
    ViewData["Name"] as string;
```

---

# Part 4 : ViewBag

# What is ViewBag? ⭐⭐⭐⭐⭐

ViewBag is a dynamic object used to pass data.

---

Controller

```csharp
ViewBag.Name = "Sabbir";

ViewBag.Age = 25;
```

---

View

```html
<p>@ViewBag.Name</p>

<p>@ViewBag.Age</p>
```

Output

```text
Sabbir

25
```

---

# ViewBag vs ViewData ⭐⭐⭐⭐⭐

| ViewBag | ViewData |
|----------|----------|
| Dynamic | Dictionary |
| Easy Syntax | Key Based |
| No Casting | Casting Often Needed |
| Runtime Checking | Runtime Checking |

---

# ViewBag vs Strongly Typed View

| ViewBag | Strongly Typed |
|----------|---------------|
| Dynamic | Type Safe |
| Small Data | Main Data |
| Runtime Errors | Compile-Time Checking |

---

# When to Use

Use Strongly Typed View for:

```text
Main Page Data
```

Use ViewBag/ViewData for:

```text
Page Title
Dropdown Lists
Messages
```

---

# Part 5 : TempData

# What is TempData? ⭐⭐⭐⭐

Stores data between two requests.

Usually after Redirect.

Controller

```csharp
TempData["Success"] =
    "Saved Successfully";
```

---

View

```html
<p>@TempData["Success"]</p>
```

---

# Comparison

| ViewData | ViewBag | TempData |
|----------|---------|----------|
| Same Request | Same Request | Next Request |
| Dictionary | Dynamic | Session Based |

---

# Part 6 : HTML Helpers

# What are HTML Helpers? ⭐⭐⭐⭐⭐

HTML Helpers generate HTML elements from C# code.

---

# TextBox

```csharp
@Html.TextBox("Name")
```

Output

```html
<input type="text" name="Name" />
```

---

# TextBoxFor ⭐⭐⭐⭐⭐

```csharp
@Html.TextBoxFor(
    x => x.Name)
```

Output

```html
<input
name="Name"
value="Laptop" />
```

---

# LabelFor

```csharp
@Html.LabelFor(
    x => x.Name)
```

Output

```html
<label>Name</label>
```

---

# DisplayFor

```csharp
@Html.DisplayFor(
    x => x.Name)
```

Displays model value.

---

# EditorFor ⭐⭐⭐⭐

```csharp
@Html.EditorFor(
    x => x.Name)
```

Creates input automatically.

---

# PasswordFor

```csharp
@Html.PasswordFor(
    x => x.Password)
```

---

# TextAreaFor

```csharp
@Html.TextAreaFor(
    x => x.Description)
```

---

# HiddenFor

```csharp
@Html.HiddenFor(
    x => x.Id)
```

---

# CheckBoxFor

```csharp
@Html.CheckBoxFor(
    x => x.IsActive)
```

---

# RadioButtonFor

```csharp
@Html.RadioButtonFor(
    x => x.Gender,
    "Male")
```

---

# DropDownListFor ⭐⭐⭐⭐

```csharp
@Html.DropDownListFor(
    x => x.CategoryId,
    Model.Categories)
```

---

# ValidationMessageFor ⭐⭐⭐⭐⭐

```csharp
@Html.ValidationMessageFor(
    x => x.Name)
```

Displays validation error.

---

# ValidationSummary ⭐⭐⭐⭐

```csharp
@Html.ValidationSummary()
```

Displays all validation errors.

---

# BeginForm ⭐⭐⭐⭐⭐

```csharp
@using(Html.BeginForm())
{
}
```

Generates

```html
<form></form>
```

---

# AntiForgery Token ⭐⭐⭐⭐⭐

```csharp
@Html.AntiForgeryToken()
```

Protects against:

```text
CSRF Attack
```

Controller

```csharp
[ValidateAntiForgeryToken]
```

---

# HTML Helpers vs Tag Helpers ⭐⭐⭐⭐

| HTML Helpers | Tag Helpers |
|--------------|-------------|
| C# Methods | HTML Attributes |
| Older | Modern |
| @Html.TextBoxFor() | asp-for |

---

Example

HTML Helper

```csharp
@Html.TextBoxFor(
    x => x.Name)
```

Tag Helper

```html
<input asp-for="Name" />
```

Tag Helpers are recommended in ASP.NET Core.

---

# Layout Pages ⭐⭐⭐⭐

Shared page template.

```text
_Layout.cshtml
```

Contains

- Header
- Navbar
- Footer

---

# RenderBody

```html
@RenderBody()
```

Displays page content.

---

# Partial Views ⭐⭐⭐⭐

Reusable UI.

```text
_ProductCard.cshtml
```

Use

```html
<partial
name="_ProductCard" />
```

---

# _ViewImports.cshtml ⭐⭐⭐⭐

Imports namespaces and Tag Helpers.

Example

```csharp
@using MyProject.Models

@addTagHelper *,
Microsoft.AspNetCore.Mvc.TagHelpers
```

---

# _ViewStart.cshtml ⭐⭐⭐⭐

Defines default layout.

```csharp
@{
    Layout = "_Layout";
}
```

---

# View Components ⭐⭐⭐

Reusable components.

Examples

- Shopping Cart
- Notifications
- Sidebar

---

# Real World Example ⭐⭐⭐⭐⭐

## Model

```csharp
public class Product
{
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

## Controller

```csharp
public IActionResult Index()
{
    Product product =
        new Product
        {
            Name = "Laptop",
            Price = 80000
        };

    ViewBag.Title = "Products";

    return View(product);
}
```

---

## View

```csharp
@model Product

<h2>@ViewBag.Title</h2>

<p>@Model.Name</p>

<p>@Model.Price</p>

@Html.TextBoxFor(
    x => x.Name)
```

---

# Common Mistakes

## Forgetting @model

❌ Wrong

```csharp
@Model.Name
```

Without

```csharp
@model Product
```

---

## Using ViewBag for Main Data

❌ Wrong

```csharp
ViewBag.Product
```

Use Strongly Typed View instead.

---

## Forgetting AntiForgery Token

Always use

```csharp
@Html.AntiForgeryToken()
```

for forms.

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Use Strongly Typed Views

✅ Use ViewModel instead of Entity

✅ Use ViewBag/ViewData only for small data

✅ Prefer Tag Helpers in ASP.NET Core

✅ Use Validation Helpers

✅ Use Partial Views for reusable UI

✅ Use Layout Pages

---

# 🎯 Most Asked Interview Questions

## Q1. What is Razor?

Razor is the View Engine of ASP.NET Core MVC.

---

## Q2. What is a Razor View?

A `.cshtml` file that combines HTML and C# to generate dynamic UI.

---

## Q3. What is Strongly Typed View?

A view associated with a specific model using:

```csharp
@model Product
```

---

## Q4. Difference Between ViewBag and ViewData?

| ViewBag | ViewData |
|----------|----------|
| Dynamic | Dictionary |
| Easy Syntax | Key Based |

---

## Q5. Difference Between ViewBag and TempData?

| ViewBag | TempData |
|----------|----------|
| Same Request | Next Request |

---

## Q6. Difference Between ViewData and TempData?

| ViewData | TempData |
|----------|----------|
| Current Request | Redirect / Next Request |

---

## Q7. What are HTML Helpers?

Methods that generate HTML controls from C#.

Example:

```csharp
@Html.TextBoxFor()
```

---

## Q8. Difference Between HTML Helpers and Tag Helpers?

| HTML Helpers | Tag Helpers |
|--------------|-------------|
| C# Methods | HTML Attributes |
| Older | Recommended |

---

## Q9. What is @model?

Associates a model with a Razor View.

---

## Q10. Why Use Strongly Typed Views?

- IntelliSense
- Compile-Time Checking
- Type Safety
- Easier Maintenance

---

## Q11. What is _Layout.cshtml?

Shared layout page for common UI.

---

## Q12. Why Use @Html.AntiForgeryToken()?

Protects forms from **CSRF (Cross-Site Request Forgery)** attacks.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Razor Basics
- [x] Razor Syntax
- [x] Code Blocks
- [x] Loops & Conditions
- [x] Strongly Typed Views
- [x] @model Directive
- [x] ViewData
- [x] ViewBag
- [x] TempData
- [x] HTML Helpers
- [x] TextBoxFor
- [x] LabelFor
- [x] ValidationMessageFor
- [x] ValidationSummary
- [x] BeginForm
- [x] AntiForgeryToken
- [x] Layout Pages
- [x] Partial Views
- [x] Tag Helpers
- [x] _ViewImports.cshtml
- [x] _ViewStart.cshtml

Mastering these topics will help you answer **95%+ of Razor Views interview questions** and build dynamic, maintainable ASP.NET Core MVC applications.