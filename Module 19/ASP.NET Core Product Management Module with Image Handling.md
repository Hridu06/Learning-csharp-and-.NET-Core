# ASP.NET Core Product Management Module with Image Handling (Easy to Advanced)

## Product CRUD | Image Upload | Validation | UI Handling | Complete Interview Guide

---

# 📌 What is Product Management Module?

The Product Management Module is one of the core modules of an eCommerce application.

It allows administrators to:

- Add Products
- View Products
- Update Products
- Delete Products
- Upload Product Images
- Manage Product Categories
- Search Products
- Filter Products

---

# Real World Example

```text
Amazon

↓

Electronics

↓

Laptop

↓

Dell Inspiron
```

Admin can

- Add Laptop
- Upload Image
- Set Price
- Set Category
- Edit Details
- Delete Product

---

# Product Module Architecture

```text
Admin

↓

Product Controller

↓

Service

↓

Repository

↓

EF Core

↓

SQL Server

↓

Images Folder (wwwroot)
```

---

# Product CRUD Flow

```text
Browser

↓

Controller

↓

Model Validation

↓

Image Upload

↓

Database

↓

Redirect

↓

Product List
```

---

# 🎯 Recommended Learning Order (Most Important First)

| Priority | Topic | Interview Frequency |
|-----------|--------------------------------------|----------------|
| ⭐⭐⭐⭐⭐ | Product CRUD | Very High |
| ⭐⭐⭐⭐⭐ | Product Model | Very High |
| ⭐⭐⭐⭐⭐ | Image Upload | Very High |
| ⭐⭐⭐⭐⭐ | IFormFile | Very High |
| ⭐⭐⭐⭐⭐ | File Upload Validation | Very High |
| ⭐⭐⭐⭐⭐ | Model Validation | Very High |
| ⭐⭐⭐⭐⭐ | Category Relationship | Very High |
| ⭐⭐⭐⭐⭐ | ViewModel | Very High |
| ⭐⭐⭐⭐⭐ | CRUD Views | Very High |
| ⭐⭐⭐⭐⭐ | Tag Helpers | Very High |
| ⭐⭐⭐⭐ | Image Preview | High |
| ⭐⭐⭐⭐ | Image Update | High |
| ⭐⭐⭐⭐ | Image Delete | High |
| ⭐⭐⭐⭐ | TempData | High |
| ⭐⭐⭐⭐ | Search | High |
| ⭐⭐⭐⭐ | Pagination | High |
| ⭐⭐⭐⭐ | Dependency Injection | High |
| ⭐⭐⭐⭐ | Async CRUD | High |
| ⭐⭐⭐ | Multiple Image Upload | Medium |
| ⭐⭐⭐ | File Size Validation | Medium |
| ⭐⭐⭐ | Extension Validation | Medium |
| ⭐⭐ | Cloud Storage | Low |

---

# Part 1 : Product Model

```csharp
public class Product
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

    [Range(1,1000000)]

    public decimal Price
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    public string ImagePath
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

# Database Table

```text
Products

----------------------------

Id

Name

Price

Description

ImagePath

CategoryId
```

---

# Relationship

```text
Category

↓

Products
```

One Category

↓

Many Products

---

# Category Model

```csharp
public class Category
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

    public ICollection<Product>
    Products
    {
        get;
        set;
    }
}
```

---

# Part 2 : Product ViewModel

Never upload files directly in Entity.

Use ViewModel.

```csharp
public class ProductCreateVM
{
    [Required]

    public string Name
    {
        get;
        set;
    }

    [Range(1,100000)]

    public decimal Price
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    [Required]

    public int CategoryId
    {
        get;
        set;
    }

    public IFormFile Image
    {
        get;
        set;
    }
}
```

---

# Why Use ViewModel?

- Better Security
- Separate UI from Database
- Image Upload Support
- Prevent Over Posting

---

# Part 3 : Product Controller

```csharp
public class ProductsController
: Controller
{
    private readonly
    AppDbContext context;

    private readonly
    IWebHostEnvironment environment;

    public ProductsController(
    AppDbContext context,
    IWebHostEnvironment environment)
    {
        this.context = context;

        this.environment = environment;
    }
}
```

---

# Part 4 : Read Products

```csharp
public IActionResult Index()
{
    var products =
    context.Products

    .Include(x=>x.Category)

    .ToList();

    return View(products);
}
```

---

# View

```csharp
@model List<Product>

<table>

@foreach(var item in Model)
{
<tr>

<td>@item.Name</td>

<td>@item.Price</td>

<td>@item.Category.Name</td>

</tr>
}

</table>
```

---

# Part 5 : Create Product

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

public async Task<IActionResult>
Create(ProductCreateVM model)
{
    if(!ModelState.IsValid)
    {
        return View(model);
    }

    string imageName = "";

    if(model.Image != null)
    {
        imageName =
        Guid.NewGuid().ToString()

        + Path.GetExtension(
        model.Image.FileName);

        string path =
        Path.Combine(

        environment.WebRootPath,

        "images",

        imageName);

        using FileStream stream =
        new(path,FileMode.Create);

        await model.Image
        .CopyToAsync(stream);
    }

    Product product =
    new Product
    {
        Name=model.Name,

        Price=model.Price,

        Description=model.Description,

        CategoryId=model.CategoryId,

        ImagePath=imageName
    };

    context.Products.Add(product);

    await context.SaveChangesAsync();

    return RedirectToAction("Index");
}
```

---

# Part 6 : HTML Form

```html
<form

asp-action="Create"

enctype="multipart/form-data">

<input asp-for="Name"/>

<input asp-for="Price"/>

<textarea
asp-for="Description">
</textarea>

<select
asp-for="CategoryId">
</select>

<input

type="file"

asp-for="Image"/>

<button>

Save

</button>

</form>
```

---

# Why enctype is Required?

```html
multipart/form-data
```

Without it

```text
Image Upload

❌ Doesn't Work
```

---

# Part 7 : IFormFile ⭐⭐⭐⭐⭐

Used to receive uploaded files.

```csharp
public IFormFile Image
{
    get;
    set;
}
```

---

# Saving Image

```csharp
using FileStream stream =
new(path,FileMode.Create);

await image.CopyToAsync(stream);
```

---

# Image Folder

```text
wwwroot

    images

        laptop.jpg

        phone.jpg
```

---

# Display Image

```html
<img

src="~/images/@item.ImagePath"

width="120"/>
```

---

# Part 8 : Image Validation

## Validate Extension

```csharp
string extension =

Path.GetExtension(
model.Image.FileName);

if(extension != ".jpg"
&& extension != ".png")
{
    ModelState.AddModelError(
    "",
    "Invalid Image");
}
```

---

# Validate Size

```csharp
if(model.Image.Length >

2 * 1024 * 1024)
{
    ModelState.AddModelError(
    "",
    "Maximum 2MB");
}
```

---

# Allowed Extensions

```text
jpg

jpeg

png

webp
```

---

# Part 9 : Edit Product

Load Existing Data

```csharp
var product =
await context.Products

.FindAsync(id);
```

---

Update

```csharp
product.Name = model.Name;

product.Price = model.Price;
```

---

Replace Image

```csharp
if(model.Image != null)
{
}
```

Delete old image

↓

Save new image

↓

Update ImagePath

---

# Delete Product

```csharp
context.Products.Remove(product);

await context.SaveChangesAsync();
```

---

# Delete Image File

```csharp
System.IO.File.Delete(path);
```

Delete database record

+

Delete physical image.

---

# Part 10 : Validation

```csharp
if(!ModelState.IsValid)
{
    return View(model);
}
```

---

# Validation Attributes

```csharp
[Required]

[StringLength(100)]

[Range(1,10000)]
```

---

# Validation Messages

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

# Part 11 : Search

```csharp
public IActionResult Index(
string search)
{
    var products =
    context.Products

    .Where(

    x=>x.Name

    .Contains(search))

    .ToList();

    return View(products);
}
```

---

# Part 12 : Pagination

Instead of

```text
10000 Products
```

Show

```text
10 Products Per Page
```

---

# Part 13 : Async CRUD

Read

```csharp
await context.Products

.ToListAsync();
```

---

Insert

```csharp
await context.SaveChangesAsync();
```

---

# Part 14 : TempData

```csharp
TempData["Success"] =

"Product Saved";
```

View

```html
@if(TempData["Success"]!=null)
{
<p>

@TempData["Success"]

</p>
}
```

---

# Part 15 : UI Handling

Product Card

```html
<div class="card">

<img

src="~/images/@item.ImagePath"/>

<h3>

@item.Name

</h3>

<p>

@item.Price

</p>

</div>
```

---

# Bootstrap Table

```html
<table

class="table table-striped">
```

---

# Image Preview (Client-Side)

```html
<input

type="file"

id="image"/>

<img

id="preview"

width="200"/>
```

```javascript
image.onchange = e => {
    preview.src =
        URL.createObjectURL(
            e.target.files[0]);
};
```

---

# Product Module Flow ⭐⭐⭐⭐⭐

```text
Admin

↓

Create Product

↓

Validation

↓

Upload Image

↓

Save Image

↓

Save Product

↓

Database

↓

Product List
```

---

# Folder Structure

```text
Controllers

ProductsController.cs

Models

Product.cs

ViewModels

ProductCreateVM.cs

Views

Products

Index.cshtml

Create.cshtml

Edit.cshtml

Delete.cshtml

wwwroot

images
```

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Use ViewModels

✅ Validate Image Type

✅ Validate File Size

✅ Use Unique File Names (GUID)

✅ Store Image Path, Not Image Binary

✅ Delete Old Images

✅ Use Async CRUD

✅ Validate ModelState

✅ Protect Forms with AntiForgeryToken

---

# Common Mistakes

❌ Saving images inside the database

❌ Not validating file extensions

❌ Using original file names

❌ Forgetting `multipart/form-data`

❌ Not deleting old images

❌ Uploading files without size checks

---

# 🎯 Most Asked Interview Questions

## Q1. What is Product CRUD?

Create, Read, Update, and Delete operations for products.

---

## Q2. Why Use `IFormFile`?

To receive uploaded files in ASP.NET Core.

---

## Q3. Why Use `multipart/form-data`?

It allows file data to be sent with the form.

---

## Q4. Why Use a ViewModel Instead of the Entity?

- Security
- Better validation
- File upload support
- Prevent over-posting

---

## Q5. Where Should Images Be Stored?

Store image files in `wwwroot/images` (or external storage) and save only the image path in the database.

---

## Q6. Why Generate a GUID File Name?

To avoid file name collisions and prevent overwriting existing files.

---

## Q7. How Do You Validate Uploaded Images?

- Check file extension
- Check file size
- Verify content type if needed
- Accept only trusted image formats

---

## Q8. Why Delete the Old Image During Update?

To prevent orphaned files and save storage space.

---

## Q9. How Do You Display Uploaded Images?

```html
<img src="~/images/@Model.ImagePath" />
```

---

## Q10. Difference Between Entity and ViewModel?

| Entity | ViewModel |
|--------|-----------|
| Database Model | UI Model |
| Maps to table | Used by views/forms |
| No `IFormFile` | Can contain `IFormFile` |

---

## Q11. Why Use Async CRUD?

Improves scalability by avoiding blocking database and file I/O operations.

---

## Q12. Why Should You Validate Uploaded Files?

To prevent uploading malicious files, oversized files, and unsupported formats.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Product CRUD
- [x] Product Entity
- [x] Product ViewModel
- [x] Category Relationship
- [x] CRUD Controllers
- [x] CRUD Views
- [x] Model Validation
- [x] IFormFile
- [x] `multipart/form-data`
- [x] Image Upload
- [x] Image Display
- [x] Image Update
- [x] Image Delete
- [x] File Extension Validation
- [x] File Size Validation
- [x] Unique File Names (GUID)
- [x] TempData
- [x] Search
- [x] Pagination
- [x] Async CRUD
- [x] UI Handling
- [x] AntiForgeryToken

Mastering these topics will help you answer **95%+ of ASP.NET Core Product Management, File Upload, and Image Handling interview questions** and build a secure, production-ready Product Management module.