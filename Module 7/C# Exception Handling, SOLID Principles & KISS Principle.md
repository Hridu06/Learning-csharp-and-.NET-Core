# C# Exception Handling, SOLID Principles & KISS Principle (Easy to Advanced)

---

# Part 1: Exception Handling

# 📌 What is Exception Handling?

Exception Handling is a mechanism to handle runtime errors gracefully without crashing the application.

Without Exception Handling:

```csharp
int result = 10 / 0;
```

Application crashes.

With Exception Handling:

```csharp
try
{
    int result = 10 / 0;
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
```

Application continues safely.

---

# 🎯 Recommended Learning Order (Exception Handling)

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | try-catch | Very High |
| ⭐⭐⭐⭐⭐ | finally Block | Very High |
| ⭐⭐⭐⭐⭐ | Exception Class | Very High |
| ⭐⭐⭐⭐⭐ | throw Keyword | Very High |
| ⭐⭐⭐⭐⭐ | Custom Exceptions | Very High |
| ⭐⭐⭐⭐⭐ | Exception Best Practices | Very High |
| ⭐⭐⭐⭐ | Multiple catch Blocks | High |
| ⭐⭐⭐⭐ | Inner Exceptions | High |
| ⭐⭐⭐⭐ | Exception Filters | High |
| ⭐⭐⭐⭐ | Async Exception Handling | High |
| ⭐⭐⭐ | Stack Trace | Medium |
| ⭐⭐⭐ | Rethrow Exception | Medium |
| ⭐⭐ | Global Exception Handling | Low |

---

# 1. try-catch ⭐⭐⭐⭐⭐

Basic exception handling.

```csharp
try
{
    int result = 10 / 0;
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
```

Output:

```text
Attempted to divide by zero.
```

---

# 2. finally Block ⭐⭐⭐⭐⭐

Always executes.

```csharp
try
{
    Console.WriteLine("Try");
}
catch
{
    Console.WriteLine("Catch");
}
finally
{
    Console.WriteLine("Finally");
}
```

Output:

```text
Try
Finally
```

---

# 3. Multiple catch Blocks ⭐⭐⭐⭐

```csharp
try
{
    int[] numbers = {1,2,3};

    Console.WriteLine(numbers[10]);
}
catch(IndexOutOfRangeException ex)
{
    Console.WriteLine("Index Error");
}
catch(Exception ex)
{
    Console.WriteLine("General Error");
}
```

---

# 4. Exception Class ⭐⭐⭐⭐⭐

Base class for all exceptions.

```csharp
catch(Exception ex)
{
    Console.WriteLine(ex.Message);

    Console.WriteLine(ex.StackTrace);
}
```

---

# Common Exception Types

| Exception | Cause |
|-----------|-------|
| DivideByZeroException | Divide by zero |
| NullReferenceException | Null object access |
| IndexOutOfRangeException | Invalid index |
| FormatException | Invalid format |
| InvalidOperationException | Invalid operation |

---

# 5. throw Keyword ⭐⭐⭐⭐⭐

Throws custom exception.

```csharp
int age = -10;

if(age < 0)
{
    throw new Exception(
        "Invalid Age");
}
```

---

# 6. Custom Exception ⭐⭐⭐⭐⭐

```csharp
class InvalidAgeException
    : Exception
{
    public InvalidAgeException(
        string message)
        : base(message)
    {
    }
}
```

Usage:

```csharp
throw new InvalidAgeException(
    "Age cannot be negative");
```

---

# 7. Rethrow Exception ⭐⭐⭐

```csharp
try
{
}
catch(Exception)
{
    throw;
}
```

Preserves original stack trace.

---

# 8. Exception Filters ⭐⭐⭐⭐

```csharp
try
{
}
catch(Exception ex)
when(ex.Message.Contains("Error"))
{
}
```

---

# 9. Async Exception Handling ⭐⭐⭐⭐

```csharp
try
{
    await Task.Run(() =>
    {
        throw new Exception("Error");
    });
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
```

---

# 10. Global Exception Handling ⭐⭐

ASP.NET Core:

```csharp
app.UseExceptionHandler();
```

---

# Exception Handling Best Practices ⭐⭐⭐⭐⭐

✅ Catch specific exceptions

✅ Use finally for cleanup

✅ Log exceptions

✅ Avoid empty catch blocks

✅ Use custom exceptions for business logic

❌ Do NOT swallow exceptions silently

---

# Real-World Example: Banking ⭐⭐⭐⭐⭐

```csharp
decimal balance = 1000;
decimal withdraw = 2000;

try
{
    if(withdraw > balance)
    {
        throw new Exception(
            "Insufficient Balance");
    }

    balance -= withdraw;
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
```

---

# 🎯 Most Asked Exception Handling Interview Questions

## Q1. What is Exception Handling?

Mechanism to handle runtime errors gracefully.

---

## Q2. Difference Between Exception and Error?

| Exception | Error |
|-----------|------|
| Recoverable | Serious/System Level |

---

## Q3. What is finally Block?

Always executes regardless of exception.

---

## Q4. Why Use Custom Exceptions?

To represent business-specific errors.

---

## Q5. Difference Between throw and throw ex?

| throw | throw ex |
|-------|----------|
| Preserves Stack Trace | Resets Stack Trace |

---

# Part 2: SOLID Principles

# 📌 What is SOLID?

SOLID is a set of 5 design principles for writing maintainable and scalable software.

Created by:

```text
Robert C. Martin (Uncle Bob)
```

---

# 🎯 Recommended Learning Order (SOLID)

| Priority | Principle | Interview Frequency |
|-----------|------------|------------------|
| ⭐⭐⭐⭐⭐ | SRP | Very High |
| ⭐⭐⭐⭐⭐ | OCP | Very High |
| ⭐⭐⭐⭐⭐ | LSP | Very High |
| ⭐⭐⭐⭐⭐ | ISP | Very High |
| ⭐⭐⭐⭐⭐ | DIP | Very High |

---

# S → Single Responsibility Principle (SRP) ⭐⭐⭐⭐⭐

## One Class = One Responsibility

❌ Bad

```csharp
class UserService
{
    public void Register()
    {
    }

    public void SendEmail()
    {
    }

    public void SaveToDatabase()
    {
    }
}
```

Too many responsibilities.

---

## Good

```csharp
class UserService
{
    public void Register()
    {
    }
}

class EmailService
{
    public void SendEmail()
    {
    }
}

class UserRepository
{
    public void Save()
    {
    }
}
```

---

# O → Open/Closed Principle (OCP) ⭐⭐⭐⭐⭐

## Open for Extension, Closed for Modification

❌ Bad

```csharp
class PaymentService
{
    public void Pay(string type)
    {
        if(type == "Bkash")
        {
        }
        else if(type == "Card")
        {
        }
    }
}
```

---

## Good

```csharp
interface IPayment
{
    void Pay();
}

class BkashPayment : IPayment
{
    public void Pay()
    {
    }
}

class CardPayment : IPayment
{
    public void Pay()
    {
    }
}
```

---

# L → Liskov Substitution Principle (LSP) ⭐⭐⭐⭐⭐

Derived classes should replace base classes safely.

❌ Bad

```csharp
class Bird
{
    public virtual void Fly()
    {
    }
}

class Ostrich : Bird
{
    public override void Fly()
    {
        throw new Exception();
    }
}
```

Ostrich cannot fly.

---

# Good

```csharp
class Bird
{
}

class FlyingBird : Bird
{
    public virtual void Fly()
    {
    }
}
```

---

# I → Interface Segregation Principle (ISP) ⭐⭐⭐⭐⭐

Clients should not depend on methods they don't use.

❌ Bad

```csharp
interface IWorker
{
    void Work();

    void Eat();
}
```

Robot doesn't eat.

---

# Good

```csharp
interface IWork
{
    void Work();
}

interface IEat
{
    void Eat();
}
```

---

# D → Dependency Inversion Principle (DIP) ⭐⭐⭐⭐⭐

High-level modules should depend on abstractions, not concrete classes.

❌ Bad

```csharp
class SqlServer
{
}

class UserService
{
    SqlServer sql =
        new SqlServer();
}
```

---

# Good

```csharp
interface IDatabase
{
    void Save();
}

class SqlServer : IDatabase
{
    public void Save()
    {
    }
}

class UserService
{
    private readonly IDatabase database;

    public UserService(
        IDatabase database)
    {
        this.database = database;
    }
}
```

---

# Benefits of SOLID ⭐⭐⭐⭐⭐

- Scalable Code
- Maintainable Code
- Reusable Components
- Easier Testing
- Loose Coupling

---

# 🎯 Most Asked SOLID Interview Questions

## Q1. What is SOLID?

5 design principles for maintainable software.

---

## Q2. What is SRP?

One class should have one responsibility.

---

## Q3. What is OCP?

Open for extension, closed for modification.

---

## Q4. What is DIP?

Depend on abstractions, not concrete implementations.

---

## Q5. Why Is SOLID Important?

Improves scalability, maintainability, and testing.

---

# Part 3: KISS Principle

# 📌 What is KISS?

KISS means:

```text
Keep It Simple, Stupid
```

Goal:

```text
Write simple and understandable code.
```

---

# ❌ Complex Code

```csharp
if((age > 18 && age < 60)
    || (isAdmin && isVerified))
{
}
```

---

# ✅ Simpler Code

```csharp
bool isAdult =
    age > 18 && age < 60;

bool hasAccess =
    isAdult || (isAdmin && isVerified);

if(hasAccess)
{
}
```

---

# KISS Principle Benefits ⭐⭐⭐⭐⭐

- Easier Maintenance
- Better Readability
- Fewer Bugs
- Easier Debugging
- Better Team Collaboration

---

# Real-World KISS Example ⭐⭐⭐⭐⭐

❌ Bad

```csharp
public bool Check(int x)
{
    if(x % 2 == 0)
        return true;
    else
        return false;
}
```

---

# ✅ Good

```csharp
public bool Check(int x)
{
    return x % 2 == 0;
}
```

---

# KISS Best Practices ⭐⭐⭐⭐⭐

✅ Use meaningful names

✅ Keep methods short

✅ Avoid unnecessary complexity

✅ Avoid deep nesting

✅ Write readable code

---

# 🎯 Most Asked KISS Interview Questions

## Q1. What is KISS Principle?

Keep code simple and readable.

---

## Q2. Why Is KISS Important?

Simple code is easier to:

- Maintain
- Debug
- Scale

---

## Q3. Difference Between Simple and Short Code?

Simple code:

```text
Easy to Understand
```

Not necessarily fewer lines.

---

# 🚀 Must-Master Topics Before .NET Interview

## Exception Handling

- [x] try-catch
- [x] finally
- [x] throw
- [x] Custom Exceptions
- [x] Async Exception Handling
- [x] Exception Best Practices

---

## SOLID Principles

- [x] SRP
- [x] OCP
- [x] LSP
- [x] ISP
- [x] DIP

---

## KISS Principle

- [x] Simple Code Design
- [x] Readable Methods
- [x] Maintainable Code

Mastering these topics will help you answer **95%+ of Software Design & Exception Handling interview questions in .NET Full Stack Development.**