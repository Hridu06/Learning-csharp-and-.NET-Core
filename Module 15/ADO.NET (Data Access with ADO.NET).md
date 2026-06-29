# ADO.NET (Data Access with ADO.NET) - Complete Guide (Easy to Advanced)

## Introduction to ADO.NET | Read & Write Data with ADO.NET | SQL Server Connectivity

---

# 📌 What is ADO.NET?

ADO.NET (**ActiveX Data Objects .NET**) is Microsoft's data access technology used to connect .NET applications with databases like SQL Server, MySQL, Oracle, etc.

It allows applications to:

- Connect to Database
- Execute SQL Queries
- Insert Data
- Update Data
- Delete Data
- Read Data
- Execute Stored Procedures
- Manage Transactions

---

# Real World Architecture

```text
ASP.NET Core MVC

        ↓

      ADO.NET

        ↓

SQL Server Database
```

---

# Why Use ADO.NET?

- Fast
- Lightweight
- Full Control
- Direct SQL Execution
- No ORM Overhead
- High Performance

---

# When to Use ADO.NET?

✅ Enterprise Applications

✅ Banking Systems

✅ Reporting Systems

✅ Legacy Projects

✅ Performance Critical Applications

---

# ADO.NET vs Entity Framework

| ADO.NET | Entity Framework |
|----------|------------------|
| Raw SQL | ORM |
| Faster | Easier |
| More Code | Less Code |
| Full Control | Automatic Mapping |
| Better Performance | Better Productivity |

---

# 🎯 Recommended Learning Order (Most Important First)

| Priority | Topic | Interview Frequency |
|-----------|-------------------------------|----------------|
| ⭐⭐⭐⭐⭐ | What is ADO.NET? | Very High |
| ⭐⭐⭐⭐⭐ | Connection String | Very High |
| ⭐⭐⭐⭐⭐ | SqlConnection | Very High |
| ⭐⭐⭐⭐⭐ | SqlCommand | Very High |
| ⭐⭐⭐⭐⭐ | ExecuteReader() | Very High |
| ⭐⭐⭐⭐⭐ | ExecuteNonQuery() | Very High |
| ⭐⭐⭐⭐⭐ | ExecuteScalar() | Very High |
| ⭐⭐⭐⭐⭐ | SqlDataReader | Very High |
| ⭐⭐⭐⭐⭐ | Parameterized Query | Very High |
| ⭐⭐⭐⭐⭐ | SQL Injection Prevention | Very High |
| ⭐⭐⭐⭐⭐ | CRUD Operations | Very High |
| ⭐⭐⭐⭐⭐ | using Statement | Very High |
| ⭐⭐⭐⭐ | SqlDataAdapter | High |
| ⭐⭐⭐⭐ | DataSet | High |
| ⭐⭐⭐⭐ | DataTable | High |
| ⭐⭐⭐⭐ | Stored Procedures | High |
| ⭐⭐⭐⭐ | Transactions | High |
| ⭐⭐⭐⭐ | Async Database Calls | High |
| ⭐⭐⭐ | Connection Pooling | Medium |
| ⭐⭐⭐ | Bulk Copy | Medium |
| ⭐⭐ | DataView | Low |

---

# ADO.NET Architecture ⭐⭐⭐⭐⭐

```text
ASP.NET Core

        ↓

SqlConnection

        ↓

SqlCommand

        ↓

SQL Server
```

---

# Main Components

```text
SqlConnection

SqlCommand

SqlDataReader

SqlDataAdapter

DataSet

DataTable

SqlTransaction
```

---

# Part 1 : Connection String

# What is Connection String? ⭐⭐⭐⭐⭐

Connection String contains database connection information.

Example

```text
Server=(localdb)\MSSQLLocalDB;

Database=ShopDB;

Trusted_Connection=True;

TrustServerCertificate=True;
```

---

# SQL Authentication

```text
Server=localhost;

Database=ShopDB;

User Id=sa;

Password=123456;
```

---

# Windows Authentication

```text
Trusted_Connection=True;
```

---

# Store Connection String

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

# Reading Connection String

```csharp
var connectionString =
configuration.GetConnectionString(
"DefaultConnection");
```

---

# Part 2 : SqlConnection ⭐⭐⭐⭐⭐

Used to connect to SQL Server.

Example

```csharp
using SqlConnection connection =
new SqlConnection(connectionString);

connection.Open();

Console.WriteLine("Connected");
```

---

# Open Connection

```csharp
connection.Open();
```

---

# Close Connection

```csharp
connection.Close();
```

Usually not needed because `using` disposes it automatically.

---

# Using Statement ⭐⭐⭐⭐⭐

Best Practice

```csharp
using SqlConnection connection =
new SqlConnection(connectionString);
```

Automatically closes connection.

---

# Part 3 : SqlCommand ⭐⭐⭐⭐⭐

Executes SQL commands.

Example

```csharp
SqlCommand command =
new SqlCommand(
"SELECT * FROM Products",
connection);
```

---

# ExecuteReader()

Used for:

```text
SELECT
```

---

# ExecuteNonQuery()

Used for:

```text
INSERT

UPDATE

DELETE
```

---

# ExecuteScalar()

Returns:

```text
Single Value
```

Example

```sql
SELECT COUNT(*)

FROM Products
```

---

# Part 4 : Reading Data

# SqlDataReader ⭐⭐⭐⭐⭐

Fast forward-only reader.

---

# Example

```csharp
using SqlConnection connection =
new(connectionString);

connection.Open();

SqlCommand command =
new(
"SELECT * FROM Products",
connection);

SqlDataReader reader =
command.ExecuteReader();

while(reader.Read())
{
    Console.WriteLine(
        reader["Name"]);
}
```

---

# Access by Column Name

```csharp
reader["Name"]
```

---

# Access by Index

```csharp
reader[0]
```

---

# Read Integer

```csharp
reader.GetInt32(0);
```

---

# Read String

```csharp
reader.GetString(1);
```

---

# Read Decimal

```csharp
reader.GetDecimal(2);
```

---

# Part 5 : INSERT Data

Table

```text
Products

Id

Name

Price
```

---

# INSERT Example ⭐⭐⭐⭐⭐

```csharp
string sql =
@"INSERT INTO Products
(Name,Price)

VALUES

(@Name,@Price)";
```

---

```csharp
SqlCommand command =
new(sql,connection);

command.Parameters.AddWithValue(
"@Name",
"Laptop");

command.Parameters.AddWithValue(
"@Price",
70000);

command.ExecuteNonQuery();
```

---

# Rows Affected

```csharp
int rows =
command.ExecuteNonQuery();
```

---

# Part 6 : UPDATE Data

```csharp
string sql =
@"UPDATE Products

SET Price=@Price

WHERE Id=@Id";
```

---

```csharp
command.Parameters.AddWithValue(
"@Price",
80000);

command.Parameters.AddWithValue(
"@Id",
1);

command.ExecuteNonQuery();
```

---

# Part 7 : DELETE Data

```csharp
string sql =
@"DELETE

FROM Products

WHERE Id=@Id";
```

---

```csharp
command.Parameters.AddWithValue(
"@Id",
1);

command.ExecuteNonQuery();
```

---

# Part 8 : ExecuteScalar()

Count Products

```csharp
SqlCommand command =
new(
"SELECT COUNT(*) FROM Products",
connection);

int total =
(int)command.ExecuteScalar();
```

---

# MAX Price

```sql
SELECT MAX(Price)

FROM Products
```

---

# Part 9 : Parameterized Query ⭐⭐⭐⭐⭐

Most Important Topic

Never write

❌

```csharp
string sql =
"SELECT *

FROM Users

WHERE Name='"

+ name +

"'";
```

SQL Injection risk.

---

Correct

```csharp
string sql =
@"SELECT *

FROM Users

WHERE Name=@Name";
```

---

```csharp
command.Parameters.AddWithValue(
"@Name",
name);
```

---

# SQL Injection ⭐⭐⭐⭐⭐

Attack

```sql
' OR 1=1 --
```

Can bypass login.

Prevent using:

```text
Parameterized Queries
```

---

# Part 10 : DataTable

Stores data in memory.

```csharp
DataTable table =
new();
```

---

# SqlDataAdapter

```csharp
SqlDataAdapter adapter =
new(command);

adapter.Fill(table);
```

---

# DataSet ⭐⭐⭐⭐

Collection of DataTables.

```csharp
DataSet ds =
new();
```

---

# DataAdapter Example

```csharp
SqlDataAdapter adapter =
new(command);

DataTable table =
new();

adapter.Fill(table);
```

---

# Loop DataTable

```csharp
foreach(DataRow row
in table.Rows)
{
    Console.WriteLine(
        row["Name"]);
}
```

---

# Part 11 : Stored Procedures

Create Procedure

```sql
CREATE PROCEDURE
GetProducts

AS

SELECT *

FROM Products
```

---

# Call Procedure

```csharp
SqlCommand command =
new(
"GetProducts",
connection);

command.CommandType =
CommandType.StoredProcedure;
```

---

# Part 12 : Transactions

```csharp
SqlTransaction transaction =
connection.BeginTransaction();
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

# Example

```csharp
try
{
    transaction.Commit();
}
catch
{
    transaction.Rollback();
}
```

---

# Part 13 : Async Programming ⭐⭐⭐⭐

Open Connection

```csharp
await connection.OpenAsync();
```

---

Execute Reader

```csharp
await command.ExecuteReaderAsync();
```

---

Execute Non Query

```csharp
await command.ExecuteNonQueryAsync();
```

---

# Part 14 : CRUD Example ⭐⭐⭐⭐⭐

Product Class

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

Insert

```csharp
using SqlConnection connection =
new(connectionString);

connection.Open();

SqlCommand command =
new(
@"INSERT INTO Products
(Name,Price)

VALUES

(@Name,@Price)",
connection);

command.Parameters.AddWithValue(
"@Name",
"Laptop");

command.Parameters.AddWithValue(
"@Price",
80000);

command.ExecuteNonQuery();
```

---

Read

```csharp
SqlCommand command =
new(
"SELECT * FROM Products",
connection);

SqlDataReader reader =
command.ExecuteReader();

while(reader.Read())
{
    Console.WriteLine(
reader["Name"]);
}
```

---

Update

```csharp
UPDATE Products

SET Price=90000

WHERE Id=1
```

---

Delete

```csharp
DELETE

FROM Products

WHERE Id=1
```

---

# ADO.NET Object Flow ⭐⭐⭐⭐⭐

```text
Connection String

↓

SqlConnection

↓

Open()

↓

SqlCommand

↓

ExecuteReader()

↓

SqlDataReader

↓

Close()
```

---

# Common Mistakes

## Forgetting Open()

```csharp
connection.Open();
```

---

## Not Using using

Causes connection leaks.

---

## SQL Injection

Never concatenate SQL strings.

---

## Forgetting Parameters

Always use

```csharp
@Name
```

---

## Leaving Reader Open

Dispose reader properly.

---

# Best Practices ⭐⭐⭐⭐⭐

✅ Use using statement

✅ Use Parameterized Queries

✅ Store Connection String in appsettings.json

✅ Use Async Methods

✅ Close Readers

✅ Use Stored Procedures for complex queries

✅ Handle Exceptions

---

# 🎯 Most Asked Interview Questions

## Q1. What is ADO.NET?

ADO.NET is Microsoft's data access technology used to connect .NET applications with databases.

---

## Q2. What is SqlConnection?

Used to establish a connection with SQL Server.

---

## Q3. Difference Between ExecuteReader(), ExecuteNonQuery(), and ExecuteScalar()?

| Method | Purpose |
|---------|----------|
| ExecuteReader() | Read multiple rows |
| ExecuteNonQuery() | INSERT, UPDATE, DELETE |
| ExecuteScalar() | Return a single value |

---

## Q4. What is SqlDataReader?

A fast, forward-only, read-only data reader.

---

## Q5. What is SqlCommand?

Executes SQL statements and stored procedures.

---

## Q6. Why Use Parameterized Queries?

To prevent SQL Injection attacks.

---

## Q7. What is SQL Injection?

An attack where malicious SQL is injected into application queries.

---

## Q8. Difference Between DataReader and DataTable?

| DataReader | DataTable |
|------------|-----------|
| Connected | Disconnected |
| Fast | Slower |
| Read Only | Editable |

---

## Q9. What is SqlDataAdapter?

Transfers data between database and DataTable/DataSet.

---

## Q10. What is DataSet?

An in-memory collection of DataTables.

---

## Q11. What is Connection Pooling?

Reusing existing database connections to improve performance.

---

## Q12. Why Use using Statement?

Automatically disposes database resources and closes connections.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] ADO.NET Basics
- [x] SqlConnection
- [x] Connection Strings
- [x] SqlCommand
- [x] ExecuteReader()
- [x] ExecuteNonQuery()
- [x] ExecuteScalar()
- [x] SqlDataReader
- [x] Parameterized Queries
- [x] SQL Injection Prevention
- [x] CRUD Operations
- [x] DataTable
- [x] DataSet
- [x] SqlDataAdapter
- [x] Stored Procedures
- [x] Transactions
- [x] Async Database Calls
- [x] Best Practices

Mastering these topics will help you answer **95%+ of ADO.NET interview questions** and build efficient, secure, and high-performance data access layers in ASP.NET Core applications.
