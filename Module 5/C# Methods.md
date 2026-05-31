# C# Methods (Easy to Advanced)

## 📌 What is a Method?

A Method is a block of reusable code that performs a specific task.

Methods help us:

- Reuse code
- Reduce duplication
- Improve readability
- Improve maintainability
- Organize business logic

Think of a method as a machine:

```text
Input → Processing → Output
```

Example:

```csharp
int result = Add(10, 20);
```

Here:

```text
Input      = 10, 20
Processing = Addition
Output     = 30
```

---

# 🎯 Recommended Learning Order (Most Important First)

For .NET Full Stack Developer Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | What is a Method? | Very High |
| ⭐⭐⭐⭐⭐ | Method Syntax | Very High |
| ⭐⭐⭐⭐⭐ | Parameters & Arguments | Very High |
| ⭐⭐⭐⭐⭐ | Return Types | Very High |
| ⭐⭐⭐⭐⭐ | Void Methods | Very High |
| ⭐⭐⭐⭐⭐ | Method Overloading | Very High |
| ⭐⭐⭐⭐⭐ | Static Methods | Very High |
| ⭐⭐⭐⭐⭐ | Instance Methods | Very High |
| ⭐⭐⭐⭐⭐ | Pass By Value | Very High |
| ⭐⭐⭐⭐⭐ | Pass By Reference (`ref`) | Very High |
| ⭐⭐⭐⭐ | `out` Parameters | High |
| ⭐⭐⭐⭐ | Optional Parameters | High |
| ⭐⭐⭐⭐ | Named Arguments | High |
| ⭐⭐⭐⭐ | Recursive Methods | High |
| ⭐⭐⭐⭐ | Expression-Bodied Methods | High |
| ⭐⭐⭐ | Local Functions | Medium |
| ⭐⭐⭐ | Generic Methods | Medium |
| ⭐⭐⭐ | Extension Methods | Medium |
| ⭐⭐ | Async Methods | Low |
| ⭐⭐ | Lambda Methods | Low |

---

# 1. Method Syntax ⭐⭐⭐⭐⭐

## Structure

```csharp
access_modifier return_type MethodName(parameters)
{
    // code
}
```

Example:

```csharp
public static void SayHello()
{
    Console.WriteLine("Hello");
}
```

---

# 2. Method Without Parameters ⭐⭐⭐⭐⭐

```csharp
static void Welcome()
{
    Console.WriteLine("Welcome to C#");
}
```

Call:

```csharp
Welcome();
```

Output:

```text
Welcome to C#
```

---

# 3. Method With Parameters ⭐⭐⭐⭐⭐

```csharp
static void Greet(string name)
{
    Console.WriteLine($"Hello {name}");
}
```

Call:

```csharp
Greet("Sabbir");
```

Output:

```text
Hello Sabbir
```

---

# 4. Parameters vs Arguments ⭐⭐⭐⭐⭐

Method:

```csharp
static void Add(int a, int b)
{
}
```

Parameters:

```text
a
b
```

Method Call:

```csharp
Add(10, 20);
```

Arguments:

```text
10
20
```

---

# 5. Void Methods ⭐⭐⭐⭐⭐

Returns nothing.

```csharp
static void DisplayMessage()
{
    Console.WriteLine("Welcome");
}
```

---

# 6. Methods Returning Values ⭐⭐⭐⭐⭐

```csharp
static int Add(int a, int b)
{
    return a + b;
}
```

Usage:

```csharp
int result = Add(10, 20);

Console.WriteLine(result);
```

Output:

```text
30
```

---

# 7. Different Return Types ⭐⭐⭐⭐⭐

### int

```csharp
static int GetAge()
{
    return 25;
}
```

### string

```csharp
static string GetName()
{
    return "Sabbir";
}
```

### bool

```csharp
static bool IsAdult()
{
    return true;
}
```

---

# 8. Multiple Return Statements ⭐⭐⭐⭐

```csharp
static string GetGrade(int marks)
{
    if(marks >= 80)
        return "A";

    return "B";
}
```

---

# 9. Method Overloading ⭐⭐⭐⭐⭐

Same method name, different parameter lists.

```csharp
static int Add(int a, int b)
{
    return a + b;
}

static double Add(double a, double b)
{
    return a + b;
}
```

Usage:

```csharp
Console.WriteLine(Add(10,20));
Console.WriteLine(Add(10.5,20.5));
```

---

# 10. Static Methods ⭐⭐⭐⭐⭐

Belong to the class.

```csharp
class Calculator
{
    public static int Add(
        int a,
        int b)
    {
        return a + b;
    }
}
```

Usage:

```csharp
Calculator.Add(10,20);
```

No object required.

---

# 11. Instance Methods ⭐⭐⭐⭐⭐

Require object creation.

```csharp
class Calculator
{
    public int Add(
        int a,
        int b)
    {
        return a + b;
    }
}
```

Usage:

```csharp
Calculator calculator = new();

calculator.Add(10,20);
```

---

# Static vs Instance Methods ⭐⭐⭐⭐⭐

| Static | Instance |
|----------|----------|
| Class Level | Object Level |
| No Object Required | Object Required |
| Faster Access | More Flexible |
| Shared Logic | Object-Specific Logic |

---

# 12. Pass By Value ⭐⭐⭐⭐⭐

Default behavior.

```csharp
static void Change(int number)
{
    number = 100;
}

int value = 10;

Change(value);

Console.WriteLine(value);
```

Output:

```text
10
```

Original value remains unchanged.

---

# 13. Pass By Reference (`ref`) ⭐⭐⭐⭐⭐

```csharp
static void Change(ref int number)
{
    number = 100;
}
```

Usage:

```csharp
int value = 10;

Change(ref value);

Console.WriteLine(value);
```

Output:

```text
100
```

Original value changes.

---

# 14. `out` Parameters ⭐⭐⭐⭐

```csharp
static void GetData(out int number)
{
    number = 100;
}
```

Usage:

```csharp
GetData(out int value);

Console.WriteLine(value);
```

Output:

```text
100
```

---

# Difference Between ref and out ⭐⭐⭐⭐⭐

| ref | out |
|------|------|
| Must Initialize | No Initialization Required |
| Input + Output | Output Only |
| Can Read Existing Value | Must Assign New Value |

Example:

```csharp
int x = 10;

Update(ref x);
```

```csharp
GetValue(out int x);
```

---

# 15. Optional Parameters ⭐⭐⭐⭐

```csharp
static void Register(
    string name,
    string country = "Bangladesh")
{
    Console.WriteLine(
        $"{name} - {country}");
}
```

Usage:

```csharp
Register("Sabbir");
```

Output:

```text
Sabbir - Bangladesh
```

---

# 16. Named Arguments ⭐⭐⭐⭐

```csharp
static void Register(
    string name,
    int age)
{
}
```

Usage:

```csharp
Register(
    age: 25,
    name: "Sabbir");
```

---

# 17. Expression-Bodied Methods ⭐⭐⭐⭐

Short syntax for simple methods.

```csharp
static int Square(int number)
    => number * number;
```

Usage:

```csharp
Console.WriteLine(
    Square(5));
```

Output:

```text
25
```

---

# 18. Recursive Methods ⭐⭐⭐⭐

Method calling itself.

Factorial Example:

```csharp
static int Factorial(int n)
{
    if(n == 1)
        return 1;

    return n *
           Factorial(n - 1);
}
```

Usage:

```csharp
Console.WriteLine(
    Factorial(5));
```

Output:

```text
120
```

---

# 19. Local Functions ⭐⭐⭐

Method inside another method.

```csharp
void Process()
{
    int Add(int a, int b)
    {
        return a + b;
    }

    Console.WriteLine(
        Add(10,20));
}
```

---

# 20. Generic Methods ⭐⭐⭐

```csharp
static T Display<T>(T value)
{
    return value;
}
```

Usage:

```csharp
Display<int>(10);

Display<string>("Hello");
```

---

# 21. Extension Methods ⭐⭐⭐

```csharp
public static class StringExtensions
{
    public static string Capitalize(
        this string value)
    {
        return value.ToUpper();
    }
}
```

Usage:

```csharp
string name = "sabbir";

Console.WriteLine(
    name.Capitalize());
```

---

# 22. Async Methods ⭐⭐

Very common in ASP.NET Core.

```csharp
public async Task<string>
GetDataAsync()
{
    await Task.Delay(1000);

    return "Data Loaded";
}
```

Usage:

```csharp
string result =
    await GetDataAsync();
```

---

# 23. Lambda Methods ⭐⭐

```csharp
Func<int,int> square =
    x => x * x;

Console.WriteLine(
    square(5));
```

Output:

```text
25
```

---

# Real-World Example: Calculator ⭐⭐⭐⭐⭐

```csharp
static int Add(int a, int b)
{
    return a + b;
}

static int Subtract(int a, int b)
{
    return a - b;
}

static int Multiply(int a, int b)
{
    return a * b;
}

static double Divide(
    int a,
    int b)
{
    return (double)a / b;
}
```

Usage:

```csharp
Console.WriteLine(Add(10,20));
Console.WriteLine(Subtract(20,10));
Console.WriteLine(Multiply(10,20));
Console.WriteLine(Divide(20,10));
```

---

# Real-World Example: Login Validation ⭐⭐⭐⭐⭐

```csharp
static bool ValidateLogin(
    string username,
    string password)
{
    return username == "admin"
        && password == "1234";
}
```

Usage:

```csharp
bool isValid =
    ValidateLogin(
        "admin",
        "1234");

Console.WriteLine(isValid);
```

Output:

```text
True
```

---

# Common Mistakes

## Missing Return

❌ Wrong

```csharp
static int Add(int a,int b)
{
}
```

Must return a value.

---

## Wrong Parameter Count

❌ Wrong

```csharp
Add(10);
```

Method expects 2 parameters.

---

## Forgetting ref Keyword

❌ Wrong

```csharp
Change(value);
```

Must be:

```csharp
Change(ref value);
```

---

# 🎯 Most Asked Interview Questions

## Q1. What is a Method?

A reusable block of code that performs a specific task.

---

## Q2. Difference Between Method and Function?

In C#, methods are functions that belong to a class.

---

## Q3. Difference Between Parameter and Argument?

Parameter:

```csharp
void Add(int a,int b)
```

Argument:

```csharp
Add(10,20);
```

---

## Q4. Difference Between Void and Return Type?

Void:

```csharp
void Display()
{
}
```

Returns nothing.

Return Type:

```csharp
int Add()
{
    return 10;
}
```

Returns value.

---

## Q5. What is Method Overloading?

Same method name with different parameter lists.

```csharp
Add(int,int)

Add(double,double)
```

---

## Q6. Difference Between Static and Instance Methods?

| Static | Instance |
|----------|----------|
| Class Level | Object Level |
| No Object | Requires Object |

---

## Q7. What is Pass By Value?

A copy of variable is passed.

---

## Q8. What is Pass By Reference?

Original variable is passed using:

```csharp
ref
```

---

## Q9. Difference Between ref and out?

| ref | out |
|------|------|
| Input + Output | Output Only |
| Must Initialize | No Need |

---

## Q10. What is Recursion?

A method calling itself.

Example:

```csharp
Factorial()
```

---

## Q11. What is a Generic Method?

A method that works with multiple data types.

```csharp
Display<T>()
```

---

## Q12. What is an Extension Method?

Adds new functionality to existing classes without modifying them.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Method Basics
- [x] Method Syntax
- [x] Parameters & Arguments
- [x] Return Types
- [x] Void Methods
- [x] Method Overloading
- [x] Static Methods
- [x] Instance Methods
- [x] Pass By Value
- [x] Pass By Reference (`ref`)
- [x] `out` Parameters
- [x] Optional Parameters
- [x] Named Arguments
- [x] Recursion
- [x] Generic Methods
- [x] Extension Methods
- [x] Async Methods

Mastering these topics will help you answer **95%+ of Method-related C#/.NET interview questions**.