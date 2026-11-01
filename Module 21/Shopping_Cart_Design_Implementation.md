# Shopping Cart Design & Implementation (ASP.NET Core MVC + EF Core)

## Priority List

  Priority     Topic                        Interview Importance
  ------------ ---------------------------- ----------------------
  ⭐⭐⭐⭐⭐   Shopping Cart Concept        Must Know
  ⭐⭐⭐⭐⭐   Add to Cart Logic            Must Know
  ⭐⭐⭐⭐⭐   Session Cart                 Must Know
  ⭐⭐⭐⭐⭐   Database Cart                Must Know
  ⭐⭐⭐⭐⭐   Cart Table Design            Must Know
  ⭐⭐⭐⭐⭐   Update Quantity              Must Know
  ⭐⭐⭐⭐⭐   Remove Item                  Must Know
  ⭐⭐⭐⭐⭐   Calculate Total Price        Must Know
  ⭐⭐⭐⭐     Guest vs Logged-in Cart      Very Important
  ⭐⭐⭐⭐     Merge Session Cart into DB   Very Important
  ⭐⭐⭐⭐     Checkout Flow                Very Important
  ⭐⭐⭐       Coupon System                Important
  ⭐⭐⭐       Inventory Validation         Important
  ⭐⭐⭐       AJAX Cart                    Important
  ⭐⭐⭐       Mini Cart                    Important
  ⭐⭐         Redis Cart                   Advanced
  ⭐⭐         Distributed Session          Advanced
  ⭐⭐         Cart Expiration              Advanced

------------------------------------------------------------------------

# Shopping Cart Concept

A shopping cart temporarily stores products selected by a user before
checkout.

Flow:

``` text
Browse Products
      ↓
Add To Cart
      ↓
Session / Database
      ↓
Update Quantity
      ↓
Remove Item
      ↓
Checkout
      ↓
Order Created
```

------------------------------------------------------------------------

# Session Cart vs Database Cart

  Session Cart                 Database Cart
  ---------------------------- -------------------
  Guest users                  Logged-in users
  Temporary                    Persistent
  Fast                         Reliable
  Browser only                 Multi-device
  Lost after session expires   Available anytime

## Configure Session

``` csharp
builder.Services.AddSession();

app.UseSession();
```

Save:

``` csharp
HttpContext.Session.SetString("Cart", json);
```

Read:

``` csharp
var json = HttpContext.Session.GetString("Cart");
```

------------------------------------------------------------------------

# Database Design

## Product

``` text
Id
Name
Price
Stock
Image
```

## Cart

``` text
Id
UserId
CreatedDate
```

## CartItem

``` text
Id
CartId
ProductId
Quantity
UnitPrice
```

Relationship:

``` text
Cart
  |
  | 1
  |
Many
CartItems
  |
  |
Product
```

Entity:

``` csharp
public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ICollection<CartItem> Items { get; set; }
}
```

``` csharp
public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Cart Cart { get; set; }
    public Product Product { get; set; }
}
```

------------------------------------------------------------------------

# Add To Cart Logic

Algorithm:

``` text
Click Add To Cart
       ↓
Check Product Exists In Cart?
       ↓
 Yes ---------- No
  |              |
Increase Qty   Create New Item
```

Example:

``` csharp
public IActionResult Add(int productId)
{
    var item = cart.FirstOrDefault(x => x.ProductId == productId);

    if(item == null)
    {
        cart.Add(new CartItem
        {
            ProductId = productId,
            Quantity = 1
        });
    }
    else
    {
        item.Quantity++;
    }

    return RedirectToAction("Index");
}
```

Database version:

``` csharp
var item = _context.CartItems
    .FirstOrDefault(x => x.ProductId == productId && x.CartId == cartId);

if(item == null)
{
    _context.CartItems.Add(new CartItem
    {
        ProductId = productId,
        Quantity = 1
    });
}
else
{
    item.Quantity++;
}

_context.SaveChanges();
```

------------------------------------------------------------------------

# Update Quantity

``` csharp
item.Quantity++;
```

Decrease:

``` csharp
item.Quantity--;

if(item.Quantity == 0)
{
    // Remove item
}
```

------------------------------------------------------------------------

# Remove Item

``` csharp
_context.CartItems.Remove(item);
```

Session:

``` csharp
cart.Remove(item);
```

------------------------------------------------------------------------

# Calculate Total

``` csharp
var total = cart.Sum(x => x.Quantity * x.UnitPrice);
```

------------------------------------------------------------------------

# Checkout Flow

``` text
Cart
 ↓
Validate
 ↓
Payment
 ↓
Create Order
 ↓
Reduce Stock
 ↓
Clear Cart
```

------------------------------------------------------------------------

# Common Interview Questions

1.  What is a shopping cart?
2.  Session cart vs Database cart?
3.  Why use Session for guest users?
4.  Why use Database for logged-in users?
5.  What happens if the same product is added twice?
6.  How is total calculated?
7.  Why use a CartService?
8.  Why validate stock before checkout?
9.  How do you merge a guest cart after login?
10. How would you scale a shopping cart for millions of users?

------------------------------------------------------------------------

# Best Practices

-   Use Session for guest users.
-   Use Database for authenticated users.
-   Keep business logic inside `CartService`.
-   Validate stock before checkout.
-   Prevent duplicate cart rows.
-   Use transactions during checkout.
-   Clear cart only after successful order.
-   Use AJAX for a better user experience.
