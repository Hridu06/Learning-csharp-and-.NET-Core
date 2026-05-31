# C# Functions / Methods (Easy to Advanced)

## 📌 What is a Function (Method)?

A Function (called a Method in C#) is a block of code that performs a specific task and can be reused multiple times.

### Why Use Functions?

- Code Reusability
- Better Readability
- Easier Maintenance
- Reduce Duplication
- Improve Testing

Without Function:

```csharp
Console.WriteLine(10 + 20);
Console.WriteLine(50 + 60);
Console.WriteLine(100 + 200);
```

With Function:

```csharp
Add(10, 20);
Add(50, 60);
Add(100, 200);

static void Add(int a, int b)
{
    Console.WriteLine(a + b);
}
```

---

# 🎯 Recommended Learning Order (Most Important First)

If you're preparing for a **.NET Full Stack Developer Interview**, follow this order:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | Method Basics | Very High |
| ⭐⭐⭐⭐⭐ | Parameters & Arguments | Very High |
| ⭐⭐⭐⭐⭐ | Return Type | Very High |
| ⭐⭐⭐⭐⭐ | Method Overloading | Very High |
| ⭐⭐⭐⭐⭐ | Static Methods | Very High |
| ⭐⭐⭐⭐⭐ | Pass By Value | Very High |
| ⭐⭐⭐⭐⭐ | Pass By Reference (`ref`) | Very High |
| ⭐⭐⭐⭐⭐ | Optional Parameters | Very High |
| ⭐⭐⭐⭐ | Named Arguments | High |
| ⭐⭐⭐⭐ | `out` Parameters | High |
| ⭐⭐⭐⭐ | Expression-Bodied Methods | High |
| ⭐⭐⭐⭐ | Recursive Methods | High |
| ⭐⭐⭐⭐ | Extension Methods | High |
| ⭐⭐⭐ | Local Functions | Medium |
| ⭐⭐⭐ | Lambda Expressions | Medium |
| ⭐⭐ | Generic Methods | Low |
| ⭐⭐ | Async Methods | Low |
| ⭐⭐ | Delegates & Func/Action | Low |

---

# 1. Method Basics ⭐⭐⭐⭐⭐

## Syntax

```csharp
access_modifier return_type MethodName()
{
    // code
}
```

Example:

```csharp
static void SayHello()
{
    Console.WriteLine("Hello World");
}
```

Calling Method:

```csharp
SayHello();
```

Output:

```text
Hello World
```

---

# 2. Method with Parameters ⭐⭐⭐⭐⭐

Parameters receive values.

```csharp
static void Greet(string name)
{
    Console.WriteLine($"Hello {name}");
}
```

Calling:

```csharp
Greet("Sabbir");
```

Output:

```text
Hello Sabbir
```

---

# 3. Parameters vs Arguments ⭐⭐⭐⭐⭐

```csharp
static void Add(int a, int b)
{
}
```

Parameters:

```csharp
a
b
```

Arguments:

```csharp
Add(10, 20);
```

Arguments:

```text
10
20
```

---

# 4. Method with Return Value ⭐⭐⭐⭐⭐

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

# 5. Void Method ⭐⭐⭐⭐⭐

Returns nothing.

```csharp
static void PrintMessage()
{
    Console.WriteLine("Welcome");
}
```

---

# 6. Multiple Return Statements ⭐⭐⭐⭐

```csharp
static string GetGrade(int marks)
{
    if (marks >= 80)
        return "A";

    return "B";
}
```

---

# 7. Method Overloading ⭐⭐⭐⭐⭐

Same method name with different parameters.

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
Console.WriteLine(Add(10, 20));
Console.WriteLine(Add(10.5, 20.5));
```

---

# 8. Static Methods ⭐⭐⭐⭐⭐

Belong to the class, not an object.

```csharp
class Calculator
{
    public static int Add(int a, int b)
    {
        return a + b;
    }
}
```

Usage:

```csharp
Calculator.Add(10, 20);
```

---

# 9. Instance Methods ⭐⭐⭐⭐

Require object creation.

```csharp
class Calculator
{
    public int Add(int a, int b)
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

# 10. Pass By Value ⭐⭐⭐⭐⭐

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

Original value unchanged.

---

# 11. Pass By Reference (`ref`) ⭐⭐⭐⭐⭐

Changes original value.

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

---

# 12. `out` Parameter ⭐⭐⭐⭐

Must be assigned inside method.

```csharp
static void GetValues(out int number)
{
    number = 100;
}
```

Usage:

```csharp
GetValues(out int value);

Console.WriteLine(value);
```

Output:

```text
100
```

---

# Difference Between ref and out

| ref | out |
|------|------|
| Must be initialized | No initialization required |
| Value can be read | Must assign before return |
| Input + Output | Output only |

---

# 13. Optional Parameters ⭐⭐⭐⭐⭐

```csharp
static void Display(string name = "Guest")
{
    Console.WriteLine(name);
}
```

Usage:

```csharp
Display();
Display("Sabbir");
```

Output:

```text
Guest
Sabbir
```

---

# 14. Named Arguments ⭐⭐⭐⭐

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

# 15. Expression-Bodied Methods ⭐⭐⭐⭐

Short syntax.

```csharp
static int Square(int number)
    => number * number;
```

Usage:

```csharp
Console.WriteLine(Square(5));
```

Output:

```text
25
```

---

# 16. Recursive Methods ⭐⭐⭐⭐

Method calling itself.

Example: Factorial

```csharp
static int Factorial(int number)
{
    if(number == 1)
        return 1;

    return number *
           Factorial(number - 1);
}
```

Usage:

```csharp
Console.WriteLine(Factorial(5));
```

Output:

```text
120
```

---

# 17. Local Functions ⭐⭐⭐

Method inside another method.

```csharp
void Process()
{
    int Add(int a, int b)
    {
        return a + b;
    }

    Console.WriteLine(Add(10,20));
}
```

---

# 18. Generic Methods ⭐⭐

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

# 19. Lambda Expression ⭐⭐⭐

```csharp
Func<int,int> square =
    x => x * x;

Console.WriteLine(square(5));
```

Output:

```text
25
```

---

# 20. Extension Methods ⭐⭐⭐⭐

Add methods to existing classes.

```csharp
public static class StringExtensions
{
    public static string ToUpperFirstLetter(
        this string value)
    {
        return value.Substring(0,1).ToUpper()
             + value.Substring(1);
    }
}
```

Usage:

```csharp
string name = "sabbir";

Console.WriteLine(
    name.ToUpperFirstLetter());
```

---

# 21. Async Methods ⭐⭐

```csharp
public async Task<string> GetDataAsync()
{
    await Task.Delay(1000);

    return "Data Loaded";
}
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

Console.WriteLine(Add(10,20));
Console.WriteLine(Subtract(20,10));
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
bool result =
    ValidateLogin("admin","1234");

Console.WriteLine(result);
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

Must return value.

---

## Wrong Parameter Count

❌ Wrong

```csharp
Add(10);
```

Method expects two parameters.

---

# 🎯 Most Asked Interview Questions

## Q1. What is a Method?

A Method is a reusable block of code that performs a specific task.

---

## Q2. Difference Between Parameter and Argument?

### Parameter

```csharp
void Add(int a,int b)
```

### Argument

```csharp
Add(10,20);
```

---

## Q3. Difference Between Void and Return Type?

### Void

```csharp
void Display()
{
}
```

Returns nothing.

### Return Type

```csharp
int Add()
{
    return 10;
}
```

Returns a value.

---

## Q4. What is Method Overloading?

Same method name with different parameter lists.

```csharp
Add(int,int)

Add(double,double)
```

---

## Q5. Difference Between Static and Instance Method?

| Static | Instance |
|----------|----------|
| Class Level | Object Level |
| No object needed | Object required |
| Faster access | More flexible |

---

## Q6. What is Pass By Value?

Copy of variable is passed.

Original value remains unchanged.

---

## Q7. What is Pass By Reference?

Original variable is passed.

Changes affect original value.

```csharp
ref
```

---

## Q8. Difference Between ref and out?

| ref | out |
|------|------|
| Initialized before call | No need |
| Input & Output | Output only |

---

## Q9. What is Recursion?

A method calling itself.

Example:

```csharp
Factorial()
```

---

## Q10. What is an Extension Method?

A method that adds functionality to existing classes without modifying them.

---

## Q11. What is a Generic Method?

A method that works with multiple data types.

```csharp
Display<T>()
```

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Method Basics
- [x] Parameters
- [x] Arguments
- [x] Return Type
- [x] Void Methods
- [x] Method Overloading
- [x] Static Methods
- [x] Instance Methods
- [x] Pass By Value
- [x] Pass By Reference (`ref`)
- [x] out Parameters
- [x] Optional Parameters
- [x] Named Arguments
- [x] Recursion
- [x] Extension Methods

Mastering these topics will help you answer **95%+ of Function/Method-related C#/.NET interview questions**.