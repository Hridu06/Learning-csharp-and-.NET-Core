# Database Fundamentals & Microsoft SQL Server (MSSQL) (Easy to Advanced)

## Database Installation | MSSQL Overview | SQL Server Fundamentals

---

# 📌 What is a Database?

A Database is an organized collection of related data that can be stored, managed, retrieved, and updated efficiently.

Example:

```text
Student Database

Student
--------
Id
Name
Department
CGPA
```

Instead of storing information in Excel or text files, databases store data in structured tables.

---

# Why Do We Need a Database?

Without a database:

- Data duplication
- Difficult searching
- Poor security
- Difficult backup
- Slow performance

With a database:

- Fast searching
- Data security
- Data consistency
- Data integrity
- Multi-user access

---

# Real-Life Examples

- Facebook → User Database
- Amazon → Product Database
- Banking System → Customer Database
- Hospital → Patient Database
- University → Student Database

---

# 🎯 Recommended Learning Order (Most Important First)

| Priority | Topic | Interview Frequency |
|-----------|-------------------------------|----------------|
| ⭐⭐⭐⭐⭐ | What is Database? | Very High |
| ⭐⭐⭐⭐⭐ | DBMS vs RDBMS | Very High |
| ⭐⭐⭐⭐⭐ | SQL Server Architecture | Very High |
| ⭐⭐⭐⭐⭐ | MSSQL Overview | Very High |
| ⭐⭐⭐⭐⭐ | Database Objects | Very High |
| ⭐⭐⭐⭐⭐ | Tables | Very High |
| ⭐⭐⭐⭐⭐ | Rows & Columns | Very High |
| ⭐⭐⭐⭐⭐ | Primary Key | Very High |
| ⭐⭐⭐⭐⭐ | Foreign Key | Very High |
| ⭐⭐⭐⭐⭐ | Data Types | Very High |
| ⭐⭐⭐⭐⭐ | SQL Server Management Studio (SSMS) | Very High |
| ⭐⭐⭐⭐⭐ | Database Creation | Very High |
| ⭐⭐⭐⭐ | SQL Server Installation | High |
| ⭐⭐⭐⭐ | SQL Server Editions | High |
| ⭐⭐⭐⭐ | Constraints | High |
| ⭐⭐⭐⭐ | Indexes | High |
| ⭐⭐⭐⭐ | Normalization | High |
| ⭐⭐⭐ | Views | Medium |
| ⭐⭐⭐ | Stored Procedures | Medium |
| ⭐⭐⭐ | Functions | Medium |
| ⭐⭐ | Triggers | Low |
| ⭐⭐ | Transactions | Low |

---

# Part 1 : Database Basics

# What is DBMS? ⭐⭐⭐⭐⭐

DBMS stands for:

```text
Database Management System
```

A DBMS is software used to:

- Store Data
- Retrieve Data
- Update Data
- Delete Data

Examples:

- SQL Server
- MySQL
- Oracle
- PostgreSQL
- SQLite

---

# What is RDBMS? ⭐⭐⭐⭐⭐

RDBMS stands for:

```text
Relational Database Management System
```

Stores data in related tables.

Example:

```text
Customer

Order

Product
```

All connected using relationships.

---

# DBMS vs RDBMS

| DBMS | RDBMS |
|------|--------|
| Stores Data | Stores Related Data |
| No Relationships | Uses Relationships |
| Less Secure | More Secure |
| Small Systems | Enterprise Systems |

---

# What is SQL?

SQL stands for:

```text
Structured Query Language
```

Used to:

- Create Database
- Create Tables
- Insert Data
- Update Data
- Delete Data
- Query Data

---

# SQL Categories

| Category | Purpose |
|----------|----------|
| DDL | Create Database Objects |
| DML | Insert/Update/Delete |
| DQL | Select Data |
| DCL | Permissions |
| TCL | Transactions |

---

# Part 2 : Microsoft SQL Server (MSSQL)

# What is Microsoft SQL Server?

Microsoft SQL Server (MSSQL) is a Relational Database Management System (RDBMS) developed by Microsoft.

It is widely used with:

- ASP.NET Core
- Entity Framework Core
- Dapper
- Azure
- Enterprise Applications

---

# SQL Server Features

- High Performance
- Security
- Backup & Restore
- Stored Procedures
- Transactions
- Views
- Indexing
- Scalability

---

# SQL Server Editions

| Edition | Purpose |
|----------|----------|
| Express | Free Learning Version |
| Developer | Full Features (Free) |
| Standard | Business |
| Enterprise | Large Enterprise |

---

# Recommended for Learning

```text
SQL Server Developer Edition
```

or

```text
SQL Server Express
```

---

# SQL Server Components

```text
SQL Server Database Engine

↓

SQL Server Agent

↓

SSMS

↓

Database
```

---

# SQL Server Architecture ⭐⭐⭐⭐⭐

```text
Application

↓

ADO.NET / EF Core

↓

SQL Server

↓

Database

↓

Tables
```

---

# SQL Server Installation

## Step 1

Download

```text
Microsoft SQL Server Developer Edition
```

---

## Step 2

Install:

```text
Database Engine Services
```

---

## Step 3

Install:

```text
SQL Server Management Studio (SSMS)
```

---

# SQL Server Management Studio (SSMS)

SSMS is the graphical tool used to manage SQL Server.

Used for:

- Creating Databases
- Creating Tables
- Running SQL Queries
- Backup & Restore
- Security Management

---

# SSMS Interface

```text
Object Explorer

↓

Databases

↓

Tables

↓

Views

↓

Stored Procedures
```

---

# Part 3 : Database Objects

Database contains:

```text
Tables

Views

Indexes

Stored Procedures

Functions

Triggers
```

---

# What is a Table?

A Table stores data.

Example

```text
Employee
```

| Id | Name | Salary |
|----|------|---------|
|1|Sabbir|50000|
|2|Rahim|45000|

---

# Rows

Each record.

Example

```text
1

Sabbir

50000
```

---

# Columns

Each field.

Example

```text
Id

Name

Salary
```

---

# Primary Key ⭐⭐⭐⭐⭐

Uniquely identifies every row.

Example

```text
EmployeeId
```

Cannot:

- Duplicate
- Be NULL

---

# Foreign Key ⭐⭐⭐⭐⭐

Creates relationship between tables.

Example

```text
Employee

DepartmentId
```

References:

```text
Department.Id
```

---

# Part 4 : Data Types

# Integer Types

```text
INT

BIGINT

SMALLINT

TINYINT
```

---

# Decimal Types

```text
DECIMAL

NUMERIC

FLOAT

REAL
```

---

# String Types ⭐⭐⭐⭐⭐

```text
VARCHAR

NVARCHAR

CHAR

NCHAR

TEXT
```

---

# Date Types ⭐⭐⭐⭐

```text
DATE

TIME

DATETIME

DATETIME2
```

---

# Boolean

```text
BIT
```

Values

```text
0

1
```

---

# Binary

```text
VARBINARY
```

---

# Choosing Correct Data Type

| Requirement | Data Type |
|-------------|-----------|
| Name | NVARCHAR(100) |
| Email | NVARCHAR(200) |
| Age | INT |
| Salary | DECIMAL(18,2) |
| Birth Date | DATE |
| Is Active | BIT |

---

# Part 5 : Database Creation

Create Database

```sql
CREATE DATABASE ECommerceDB;
```

---

Use Database

```sql
USE ECommerceDB;
```

---

# Create Table

```sql
CREATE TABLE Products
(
    Id INT PRIMARY KEY,

    Name NVARCHAR(100),

    Price DECIMAL(18,2)
);
```

---

# Insert Data

```sql
INSERT INTO Products
VALUES
(
1,
'Laptop',
80000
);
```

---

# Select Data

```sql
SELECT *

FROM Products;
```

---

# Part 6 : Constraints

# What is Constraint?

Rules applied to data.

---

# Common Constraints

| Constraint | Purpose |
|------------|----------|
| PRIMARY KEY | Unique Identifier |
| FOREIGN KEY | Relationship |
| NOT NULL | Mandatory Value |
| UNIQUE | No Duplicate |
| CHECK | Custom Rule |
| DEFAULT | Default Value |

---

# NOT NULL

```sql
Name NVARCHAR(100)
NOT NULL
```

---

# UNIQUE

```sql
Email NVARCHAR(100)

UNIQUE
```

---

# CHECK

```sql
CHECK(Age>=18)
```

---

# DEFAULT

```sql
IsActive BIT

DEFAULT 1
```

---

# Part 7 : Indexes

# What is an Index?

Improves query performance.

Example

Without Index

```text
Search entire table
```

With Index

```text
Jump directly to data
```

---

# Clustered Index

Sorts table physically.

One per table.

---

# Non-Clustered Index

Separate lookup structure.

Many allowed.

---

# Part 8 : Normalization

Purpose

```text
Reduce Duplicate Data
```

Normal Forms

```text
1NF

2NF

3NF
```

Most applications use:

```text
3NF
```

---

# Example

Wrong

```text
Student

DepartmentName
```

Repeated many times.

Better

```text
Department Table

Student Table

DepartmentId
```

---

# Part 9 : Views

Virtual table.

```sql
CREATE VIEW ProductView

AS

SELECT *

FROM Products;
```

---

# Part 10 : Stored Procedure

Reusable SQL.

```sql
CREATE PROCEDURE GetProducts

AS

SELECT *

FROM Products;
```

Execute

```sql
EXEC GetProducts;
```

---

# Part 11 : Functions

Returns value.

```sql
CREATE FUNCTION
```

Used inside SQL statements.

---

# Part 12 : Triggers

Automatically executes after:

- INSERT
- UPDATE
- DELETE

---

# Part 13 : Transactions

Used to maintain data consistency.

Commands

```sql
BEGIN TRANSACTION

COMMIT

ROLLBACK
```

---

# ACID Properties ⭐⭐⭐⭐

| Property | Meaning |
|----------|----------|
| Atomicity | All or Nothing |
| Consistency | Valid State |
| Isolation | Independent Transactions |
| Durability | Permanent Changes |

---

# Database Design Flow ⭐⭐⭐⭐⭐

```text
Requirement Analysis

↓

ER Diagram

↓

Database

↓

Tables

↓

Relationships

↓

Constraints

↓

Indexes
```

---

# Real-World eCommerce Database ⭐⭐⭐⭐⭐

Tables

```text
Users

Categories

Products

Orders

OrderItems

Payments

Reviews
```

Relationships

```text
Category

↓

Products

↓

OrderItems

↓

Orders

↓

Users
```

---

# Common Mistakes

## Using Wrong Data Type

❌

```text
Price VARCHAR
```

Use

```text
DECIMAL(18,2)
```

---

## Missing Primary Key

Bad database design.

---

## Duplicate Data

Store Category Name repeatedly.

Use Foreign Key instead.

---

## No Indexes

Leads to slow queries.

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Use Primary Keys

✅ Use Foreign Keys

✅ Choose Correct Data Types

✅ Normalize Database

✅ Create Indexes

✅ Use Constraints

✅ Backup Regularly

---

# 🎯 Most Asked Interview Questions

## Q1. What is a Database?

An organized collection of related data.

---

## Q2. Difference Between DBMS and RDBMS?

| DBMS | RDBMS |
|------|--------|
| Stores Data | Stores Related Data |

---

## Q3. What is SQL?

Structured Query Language used to manage databases.

---

## Q4. What is MSSQL?

Microsoft SQL Server, a Relational Database Management System.

---

## Q5. What is SSMS?

SQL Server Management Studio used to manage SQL Server.

---

## Q6. What is a Primary Key?

Unique identifier for each record.

---

## Q7. What is a Foreign Key?

Creates relationships between tables.

---

## Q8. Difference Between VARCHAR and NVARCHAR?

| VARCHAR | NVARCHAR |
|----------|-----------|
| Non-Unicode | Unicode (Supports all languages) |

---

## Q9. What is Normalization?

Process of reducing duplicate data and improving database structure.

---

## Q10. What is an Index?

A database object that improves query performance.

---

## Q11. What are ACID Properties?

- Atomicity
- Consistency
- Isolation
- Durability

---

## Q12. What is a Constraint?

A rule that restricts invalid data from being stored in the database.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Database Basics
- [x] DBMS vs RDBMS
- [x] SQL Basics
- [x] MSSQL Overview
- [x] SQL Server Architecture
- [x] SQL Server Installation
- [x] SSMS
- [x] Database Objects
- [x] Tables
- [x] Rows & Columns
- [x] Primary Keys
- [x] Foreign Keys
- [x] Data Types
- [x] Constraints
- [x] Indexes
- [x] Normalization
- [x] Views
- [x] Stored Procedures
- [x] Transactions
- [x] ACID Properties

Mastering these topics will help you answer **95%+ of Database Fundamentals and Microsoft SQL Server interview questions** for ASP.NET Core and .NET Full Stack Developer roles.