# Checkout Workflow & Order Management (ASP.NET Core MVC + EF Core)

> Easy → Advanced roadmap with interview questions, examples, and
> priority list.

# Priority List

  Priority     Topic                      Importance
  ------------ -------------------------- ----------------
  ⭐⭐⭐⭐⭐   Checkout Flow              Must Know
  ⭐⭐⭐⭐⭐   Order Entity               Must Know
  ⭐⭐⭐⭐⭐   OrderItems Entity          Must Know
  ⭐⭐⭐⭐⭐   Database Transactions      Must Know
  ⭐⭐⭐⭐⭐   SaveChanges & Commit       Must Know
  ⭐⭐⭐⭐⭐   Stock Validation           Must Know
  ⭐⭐⭐⭐⭐   Clear Cart                 Must Know
  ⭐⭐⭐⭐     Payment Flow               Very Important
  ⭐⭐⭐⭐     Order Status               Very Important
  ⭐⭐⭐⭐     Rollback Transaction       Very Important
  ⭐⭐⭐       Inventory Update           Important
  ⭐⭐⭐       Order History              Important
  ⭐⭐⭐       Invoice Generation         Important
  ⭐⭐         Distributed Transactions   Advanced

------------------------------------------------------------------------

# Checkout Workflow

``` text
Cart
  ↓
Validate Cart
  ↓
Validate Stock
  ↓
Calculate Total
  ↓
Payment
  ↓
Begin Transaction
  ↓
Create Order
  ↓
Create OrderItems
  ↓
Reduce Stock
  ↓
Clear Cart
  ↓
Commit
```

------------------------------------------------------------------------

# What is Checkout?

Checkout converts a user's shopping cart into a permanent order after
validation and payment.

------------------------------------------------------------------------

# Database Design

## Order

``` text
Id
UserId
OrderDate
TotalAmount
Status
PaymentMethod
ShippingAddress
```

## OrderItem

``` text
Id
OrderId
ProductId
Quantity
UnitPrice
Subtotal
```

Relationship

``` text
Order
  |
  | 1
  |
Many
OrderItems
  |
Product
```

------------------------------------------------------------------------

# Entity Example

``` csharp
public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
        = new List<OrderItem>();
}
```

``` csharp
public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public Order Order { get; set; }
    public Product Product { get; set; }
}
```

------------------------------------------------------------------------

# Checkout Algorithm

``` text
Get Cart
   ↓
Cart Empty?
   ↓
No
   ↓
Validate Stock
   ↓
Payment Success?
   ↓
Yes
   ↓
Begin Transaction
   ↓
Save Order
   ↓
Save OrderItems
   ↓
Update Product Stock
   ↓
Clear Cart
   ↓
Commit
```

------------------------------------------------------------------------

# Create Order Example

``` csharp
var order = new Order
{
    UserId = userId,
    OrderDate = DateTime.UtcNow,
    TotalAmount = total
};

_context.Orders.Add(order);
_context.SaveChanges();
```

------------------------------------------------------------------------

# Add Order Items

``` csharp
foreach(var item in cart)
{
    _context.OrderItems.Add(new OrderItem
    {
        OrderId = order.Id,
        ProductId = item.ProductId,
        Quantity = item.Quantity,
        UnitPrice = item.UnitPrice
    });
}

_context.SaveChanges();
```

------------------------------------------------------------------------

# Database Transactions ⭐⭐⭐⭐⭐

A transaction ensures **all operations succeed or none are saved**.

## Why use transactions?

-   Prevent partial data
-   Maintain consistency
-   Protect inventory
-   Handle failures safely

------------------------------------------------------------------------

# Transaction Flow

``` text
Begin Transaction
      ↓
Insert Order
      ↓
Insert OrderItems
      ↓
Update Stock
      ↓
Clear Cart
      ↓
Commit

If Error
      ↓
Rollback
```

------------------------------------------------------------------------

# EF Core Transaction Example

``` csharp
using var transaction = await _context.Database.BeginTransactionAsync();

try
{
    _context.Orders.Add(order);
    await _context.SaveChangesAsync();

    foreach(var item in cart)
    {
        _context.OrderItems.Add(new OrderItem
        {
            OrderId = order.Id,
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice
        });
    }

    await _context.SaveChangesAsync();

    await transaction.CommitAsync();
}
catch
{
    await transaction.RollbackAsync();
    throw;
}
```

------------------------------------------------------------------------

# Stock Validation

``` csharp
if(product.Stock < item.Quantity)
{
    throw new Exception("Insufficient stock.");
}
```

------------------------------------------------------------------------

# Reduce Inventory

``` csharp
product.Stock -= item.Quantity;
```

------------------------------------------------------------------------

# Clear Cart

``` csharp
_context.CartItems.RemoveRange(cartItems);
await _context.SaveChangesAsync();
```

------------------------------------------------------------------------

# Order Status

Typical values:

``` text
Pending
Paid
Processing
Shipped
Delivered
Cancelled
Refunded
```

------------------------------------------------------------------------

# Payment Flow

``` text
Checkout
   ↓
Payment Gateway
   ↓
Success
   ↓
Save Order
```

------------------------------------------------------------------------

# Common Interview Questions

1.  What is the checkout process?
2.  Why use separate Order and OrderItem tables?
3.  Why are database transactions important?
4.  What is Commit?
5.  What is Rollback?
6.  What happens if stock runs out during checkout?
7.  Why validate stock before payment or before commit?
8.  How do you calculate the order total?
9.  Why clear the cart only after a successful transaction?
10. What is ACID in database transactions?

------------------------------------------------------------------------

# Best Practices

-   Always validate cart and stock.
-   Use database transactions for checkout.
-   Commit only after all operations succeed.
-   Rollback on any failure.
-   Keep business logic inside OrderService.
-   Store price snapshots in OrderItems.
-   Never trust client-side totals.
-   Clear the cart only after a successful commit.
