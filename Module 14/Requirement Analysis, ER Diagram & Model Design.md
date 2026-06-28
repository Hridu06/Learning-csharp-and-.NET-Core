# Requirement Analysis, ER Diagram & Model Design (Easy to Advanced)

## eCommerce System Overview | Database Design (ER Diagram) | Model Design | Entity Relationship

---

# 📌 What is Requirement Analysis?

Requirement Analysis is the process of understanding what the client wants before starting software development.

It answers:

```text
What should the system do?

Who will use it?

What features are needed?
```

It is the **first phase** of the Software Development Life Cycle (SDLC).

---

# Why Requirement Analysis is Important?

Without Requirement Analysis:

```text
❌ Wrong Features
❌ Wrong Database
❌ Project Delay
❌ Budget Overrun
❌ Client Dissatisfaction
```

With Requirement Analysis:

```text
✅ Clear Requirements
✅ Better Database Design
✅ Better Architecture
✅ Easier Development
```

---

# Software Development Flow ⭐⭐⭐⭐⭐

```text
Requirement Analysis
        ↓
System Design
        ↓
Database Design
        ↓
Development
        ↓
Testing
        ↓
Deployment
        ↓
Maintenance
```

---

# Functional Requirements ⭐⭐⭐⭐⭐

Functional Requirements describe **what the system should do**.

Example for an eCommerce System:

- User Registration
- User Login
- Product Management
- Shopping Cart
- Place Order
- Payment
- Order Tracking
- Product Review

---

# Non-Functional Requirements ⭐⭐⭐⭐⭐

Describe **how the system should perform**.

Examples:

- Security
- Performance
- Scalability
- Reliability
- Availability
- Maintainability

---

# Example Requirements

### Functional

```text
User can login

User can add products

User can order products
```

### Non-Functional

```text
System response < 2 seconds

Supports 5000 users

99.9% uptime
```

---

# Requirement Gathering Techniques ⭐⭐⭐⭐

- Client Interview
- Questionnaire
- Observation
- Workshops
- Existing System Analysis
- Documentation Review

---

# Requirement Analysis Deliverables ⭐⭐⭐⭐

- Software Requirement Specification (SRS)
- Use Cases
- User Stories
- ER Diagram
- UML Diagrams
- Wireframes

---

# 🎯 Recommended Learning Order (Most Important First)

| Priority | Topic | Interview Frequency |
|-----------|-------------------------------|----------------|
| ⭐⭐⭐⭐⭐ | Requirement Analysis | Very High |
| ⭐⭐⭐⭐⭐ | Functional Requirements | Very High |
| ⭐⭐⭐⭐⭐ | Non-Functional Requirements | Very High |
| ⭐⭐⭐⭐⭐ | Entity | Very High |
| ⭐⭐⭐⭐⭐ | Attribute | Very High |
| ⭐⭐⭐⭐⭐ | Primary Key | Very High |
| ⭐⭐⭐⭐⭐ | Foreign Key | Very High |
| ⭐⭐⭐⭐⭐ | Relationships | Very High |
| ⭐⭐⭐⭐⭐ | ER Diagram | Very High |
| ⭐⭐⭐⭐⭐ | One-to-One | Very High |
| ⭐⭐⭐⭐⭐ | One-to-Many | Very High |
| ⭐⭐⭐⭐⭐ | Many-to-Many | Very High |
| ⭐⭐⭐⭐⭐ | Model Design | Very High |
| ⭐⭐⭐⭐⭐ | Navigation Properties | Very High |
| ⭐⭐⭐⭐⭐ | Entity Framework Models | Very High |
| ⭐⭐⭐⭐ | Normalization | High |
| ⭐⭐⭐⭐ | Junction Table | High |
| ⭐⭐⭐⭐ | Composite Key | High |
| ⭐⭐⭐⭐ | Indexes | High |
| ⭐⭐⭐⭐ | Cascade Delete | High |
| ⭐⭐⭐ | Self Relationship | Medium |
| ⭐⭐⭐ | Database Constraints | Medium |
| ⭐⭐ | Denormalization | Low |

---

# Part 1 : eCommerce System Overview

## Modules

```text
Authentication

Customer

Admin

Category

Product

Shopping Cart

Order

Payment

Review

Wishlist
```

---

# User Flow ⭐⭐⭐⭐⭐

```text
Register
     ↓
Login
     ↓
Browse Products
     ↓
Add To Cart
     ↓
Checkout
     ↓
Payment
     ↓
Order Confirmation
```

---

# Main Entities ⭐⭐⭐⭐⭐

```text
User

Product

Category

Cart

CartItem

Order

OrderItem

Payment

Review
```

---

# Part 2 : Database Design

# What is Database Design?

Database Design is the process of organizing data into tables and defining relationships.

Good database design provides:

- Faster Queries
- Less Data Duplication
- Better Performance
- Easier Maintenance

---

# Database Design Steps

```text
Requirements
      ↓
Entities
      ↓
Attributes
      ↓
Relationships
      ↓
ER Diagram
      ↓
Tables
```

---

# Part 3 : Entity

# What is an Entity? ⭐⭐⭐⭐⭐

An Entity is a real-world object stored in the database.

Examples:

```text
Customer

Product

Employee

Order
```

---

# Example

```text
Entity

Product
```

Attributes:

```text
Id

Name

Price

Stock
```

---

# C# Entity Example

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

    public int Stock
    {
        get;
        set;
    }
}
```

---

# Part 4 : Attributes

Attributes describe an entity.

Example:

```text
Product

Id

Name

Price

Quantity
```

---

# Types of Attributes

- Simple
- Composite
- Derived
- Multivalued

---

# Part 5 : Primary Key ⭐⭐⭐⭐⭐

Uniquely identifies each record.

```text
Product

Id
```

Example:

```text
Id

1

2

3
```

Cannot repeat.

---

# C#

```csharp
public int Id
{
    get;
    set;
}
```

---

# Part 6 : Foreign Key ⭐⭐⭐⭐⭐

Connects two tables.

Example

```text
Product

CategoryId
```

CategoryId references

```text
Category.Id
```

---

# C#

```csharp
public int CategoryId
{
    get;
    set;
}
```

---

# Part 7 : Relationships

## One-to-One ⭐⭐⭐⭐⭐

One record matches exactly one record.

Example

```text
Person

Passport
```

---

# C#

```csharp
public class User
{
    public UserProfile
        Profile
    {
        get;
        set;
    }
}
```

---

# One-to-Many ⭐⭐⭐⭐⭐

Most common relationship.

```text
Category

↓

Products
```

One Category

Many Products

---

# Example

Category

```text
Electronics
```

Products

```text
Laptop

Phone

Mouse
```

---

# C#

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

# Many-to-Many ⭐⭐⭐⭐⭐

Students ↔ Courses

Products ↔ Orders

Needs Junction Table.

---

Example

```text
Product

Order

↓

OrderItem
```

---

# OrderItem

```text
OrderId

ProductId

Quantity
```

---

# C#

```csharp
public class OrderItem
{
    public int OrderId
    {
        get;
        set;
    }

    public int ProductId
    {
        get;
        set;
    }
}
```

---

# Part 8 : ER Diagram

# What is ER Diagram? ⭐⭐⭐⭐⭐

ER =

```text
Entity Relationship Diagram
```

Used to visualize database design.

---

# Symbols

| Symbol | Meaning |
|----------|----------|
| Rectangle | Entity |
| Oval | Attribute |
| Diamond | Relationship |

---

# Simple ER Diagram

```text
Category
    |
    | 1
    |
    |------< Product

CategoryId (FK)
```

---

# eCommerce ER Diagram ⭐⭐⭐⭐⭐

```text
Customer
    |
    | 1
    |
    |------< Order
                |
                | 1
                |
                |------< OrderItem >------ Product
                                |
                                |
                             Quantity

Category
    |
    | 1
    |
    |------< Product

Product
    |
    | 1
    |
    |------< Review

Customer
    |
    | 1
    |
    |------< Review
```

---

# Database Tables

```text
Customer

Product

Category

Order

OrderItem

Payment

Review
```

---

# Part 9 : Model Design

# What is Model Design?

Converting database tables into C# classes.

---

# Product Model ⭐⭐⭐⭐⭐

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

# Order Model

```csharp
public class Order
{
    public int Id
    {
        get;
        set;
    }

    public DateTime OrderDate
    {
        get;
        set;
    }

    public ICollection<OrderItem>
        OrderItems
    {
        get;
        set;
    }
}
```

---

# OrderItem Model

```csharp
public class OrderItem
{
    public int OrderId
    {
        get;
        set;
    }

    public int ProductId
    {
        get;
        set;
    }

    public int Quantity
    {
        get;
        set;
    }
}
```

---

# Navigation Properties ⭐⭐⭐⭐⭐

Enable Entity Framework relationships.

Example

```csharp
public Category Category
{
    get;
    set;
}
```

---

Collection Navigation

```csharp
public ICollection<Product>
Products
{
    get;
    set;
}
```

---

# Model Relationships

```text
Category

↓

Products

↓

OrderItems

↓

Orders
```

---

# Normalization ⭐⭐⭐⭐

Goal

```text
Remove Duplicate Data
```

Normal Forms

```text
1NF

2NF

3NF
```

Most projects use

```text
3NF
```

---

# Composite Key ⭐⭐⭐⭐

More than one Primary Key.

Example

```text
OrderId

ProductId
```

Together become key.

---

# Junction Table ⭐⭐⭐⭐

Used for Many-to-Many.

Example

```text
StudentCourse

OrderItem

EmployeeProject
```

---

# Cascade Delete ⭐⭐⭐⭐

Deleting parent automatically deletes child.

Example

Delete

```text
Order
```

Deletes

```text
OrderItems
```

---

# Real-World Entity Relationship ⭐⭐⭐⭐⭐

```text
Customer

↓

Orders

↓

OrderItems

↓

Products

↓

Category
```

---

# Common Mistakes

## No Foreign Keys

Results in disconnected tables.

---

## Duplicate Data

Store Category Name in Product repeatedly.

Wrong.

Use CategoryId.

---

## Missing Navigation Properties

Entity Framework cannot navigate relationships easily.

---

## Using Many-to-Many Without Junction Table

Wrong database design.

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Identify Entities first

✅ Define Primary Keys

✅ Define Foreign Keys

✅ Normalize database

✅ Use Navigation Properties

✅ Keep relationships simple

✅ Use junction tables for Many-to-Many

---

# 🎯 Most Asked Interview Questions

## Q1. What is Requirement Analysis?

The process of gathering and analyzing client requirements before development.

---

## Q2. Difference Between Functional and Non-Functional Requirements?

| Functional | Non-Functional |
|------------|----------------|
| What the system does | How the system performs |

---

## Q3. What is an Entity?

A real-world object stored in the database.

---

## Q4. What is an ER Diagram?

A diagram that represents entities and their relationships.

---

## Q5. What is a Primary Key?

Uniquely identifies each row.

---

## Q6. What is a Foreign Key?

Connects two related tables.

---

## Q7. Difference Between One-to-One and One-to-Many?

| One-to-One | One-to-Many |
|------------|-------------|
| One record → One record | One record → Many records |

---

## Q8. What is Many-to-Many Relationship?

Many records on both sides, implemented using a junction table.

---

## Q9. What is Normalization?

Process of reducing duplicate data and improving database structure.

---

## Q10. What are Navigation Properties?

Properties used by Entity Framework to represent relationships between entities.

---

## Q11. Why Is OrderItem Needed?

To represent the Many-to-Many relationship between Orders and Products.

---

## Q12. What is Model Design?

Converting database tables into C# classes with properties and relationships.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Requirement Analysis
- [x] Functional Requirements
- [x] Non-Functional Requirements
- [x] Database Design
- [x] Entities
- [x] Attributes
- [x] Primary Keys
- [x] Foreign Keys
- [x] ER Diagram
- [x] One-to-One Relationship
- [x] One-to-Many Relationship
- [x] Many-to-Many Relationship
- [x] Junction Table
- [x] Model Design
- [x] Entity Framework Models
- [x] Navigation Properties
- [x] Normalization
- [x] Composite Keys
- [x] Cascade Delete

Mastering these topics will help you answer **95%+ of Requirement Analysis, Database Design, ER Diagram, and Model Design interview questions** and design scalable, production-ready ASP.NET Core applications.