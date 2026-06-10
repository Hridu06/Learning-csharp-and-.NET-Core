# C# Delegates and Lambda Expressions (Easy to Advanced)

## 📌 What is a Delegate?

A Delegate is a type-safe function pointer.

It stores a reference to a method.

### Simple Meaning

```text
Variable → Stores Data

Delegate → Stores Method
```

---

# 📌 What is a Lambda Expression?

A Lambda Expression is a short and cleaner way to write anonymous functions.

Example:

```csharp
(int x) => x * x
```

---

# Why Are Delegates Important?

Delegates are heavily used in:

- Events
- LINQ
- ASP.NET Core
- Callbacks
- Async Programming
- Task Parallel Library
- Entity Framework
- Middleware Pipelines

---

# 🎯 Recommended Learning Order (Most Important First)

For .NET Full Stack Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | Delegate Basics | Very High |
| ⭐⭐⭐⭐⭐ | Delegate Syntax | Very High |
| ⭐⭐⭐⭐⭐ | Lambda Expressions | Very High |
| ⭐⭐⭐⭐⭐ | Func Delegate | Very High |
| ⭐⭐⭐⭐⭐ | Action Delegate | Very High |
| ⭐⭐⭐⭐⭐ | Predicate Delegate | Very High |
| ⭐⭐⭐⭐⭐ | Anonymous Methods | Very High |
| ⭐⭐⭐⭐ | Multicast Delegates | High |
| ⭐⭐⭐⭐ | Delegate Chaining | High |
| ⭐⭐⭐⭐ | Events | High |
| ⭐⭐⭐⭐ | Expression Lambdas | High |
| ⭐⭐⭐⭐ | Statement Lambdas | High |
| ⭐⭐⭐ | Generic Delegates | Medium |
| ⭐⭐⭐ | Callback Methods | Medium |
| ⭐⭐⭐ | Closures | Medium |
| ⭐⭐ | Covariance & Contravariance | Low |
| ⭐⭐ | Expression Trees | Low |

---

# 1. Delegate Basics ⭐⭐⭐⭐⭐

## Syntax

```csharp
delegate returnType DelegateName(parameters);
```

Example:

```csharp
delegate void MessageDelegate();
```

---

# 2. Creating a Delegate ⭐⭐⭐⭐⭐

```csharp
delegate void PrintDelegate();

class Program
{
    static void Show()
    {
        Console.WriteLine("Hello");
    }

    static void Main()
    {
        PrintDelegate del = Show;

        del();
    }
}
```

Output:

```text
Hello
```

---

# How Delegate Works

```text
Delegate → Stores Method Reference

del → Show()
```

---

# 3. Delegate with Parameters ⭐⭐⭐⭐⭐

```csharp
delegate int AddDelegate(
    int a,
    int b);

class Program
{
    static int Add(int x, int y)
    {
        return x + y;
    }

    static void Main()
    {
        AddDelegate del = Add;

        int result = del(10,20);

        Console.WriteLine(result);
    }
}
```

Output:

```text
30
```

---

# 4. Multicast Delegates ⭐⭐⭐⭐

One delegate calling multiple methods.

```csharp
delegate void Notify();

class Program
{
    static void Email()
    {
        Console.WriteLine("Email Sent");
    }

    static void Sms()
    {
        Console.WriteLine("SMS Sent");
    }

    static void Main()
    {
        Notify notify = Email;

        notify += Sms;

        notify();
    }
}
```

Output:

```text
Email Sent
SMS Sent
```

---

# 5. Delegate Chaining ⭐⭐⭐⭐

Adding multiple methods.

```csharp
notify += Email;
notify += Sms;
```

Removing methods:

```csharp
notify -= Sms;
```

---

# 6. Anonymous Methods ⭐⭐⭐⭐⭐

Method without name.

```csharp
delegate void Print();

Print print = delegate ()
{
    Console.WriteLine("Hello");
};

print();
```

Output:

```text
Hello
```

---

# 7. Lambda Expression Basics ⭐⭐⭐⭐⭐

Short form of anonymous methods.

---

# Syntax

```csharp
(parameters) => expression
```

---

# Example

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

# 8. Expression Lambda ⭐⭐⭐⭐

Single expression.

```csharp
x => x * x
```

Example:

```csharp
Func<int,int> cube =
    x => x * x * x;

Console.WriteLine(cube(3));
```

Output:

```text
27
```

---

# 9. Statement Lambda ⭐⭐⭐⭐

Multiple statements.

```csharp
(x,y) =>
{
    int result = x + y;

    return result;
}
```

Example:

```csharp
Func<int,int,int> add =
    (x,y) =>
    {
        return x + y;
    };

Console.WriteLine(add(10,20));
```

Output:

```text
30
```

---

# 10. Func Delegate ⭐⭐⭐⭐⭐

Built-in generic delegate that returns value.

---

# Syntax

```csharp
Func<input,output>
```

---

# Example

```csharp
Func<int,int> square =
    x => x * x;

Console.WriteLine(square(5));
```

---

# Multiple Parameters

```csharp
Func<int,int,int> add =
    (x,y) => x + y;

Console.WriteLine(add(10,20));
```

---

# 11. Action Delegate ⭐⭐⭐⭐⭐

Built-in delegate with NO return value.

---

# Syntax

```csharp
Action<parameters>
```

---

# Example

```csharp
Action<string> greet =
    name => Console.WriteLine(
        $"Hello {name}");

greet("Sabbir");
```

Output:

```text
Hello Sabbir
```

---

# 12. Predicate Delegate ⭐⭐⭐⭐⭐

Built-in delegate that returns boolean.

---

# Syntax

```csharp
Predicate<T>
```

---

# Example

```csharp
Predicate<int> isEven =
    number => number % 2 == 0;

Console.WriteLine(isEven(4));
```

Output:

```text
True
```

---

# Func vs Action vs Predicate ⭐⭐⭐⭐⭐

| Delegate | Returns Value? | Use Case |
|----------|----------------|----------|
| Func | ✅ Yes | Calculations |
| Action | ❌ No | Printing/Logging |
| Predicate | ✅ bool | Conditions |

---

# 13. Generic Delegates ⭐⭐⭐

```csharp
delegate T Operation<T>(T a, T b);
```

Usage:

```csharp
Operation<int> add =
    (x,y) => x + y;

Console.WriteLine(add(10,20));
```

---

# 14. Delegates as Parameters ⭐⭐⭐⭐

```csharp
delegate int Operation(
    int a,
    int b);

class Program
{
    static int Calculate(
        int x,
        int y,
        Operation operation)
    {
        return operation(x,y);
    }

    static int Add(int a, int b)
    {
        return a + b;
    }

    static void Main()
    {
        int result =
            Calculate(10,20,Add);

        Console.WriteLine(result);
    }
}
```

Output:

```text
30
```

---

# 15. Callback Methods ⭐⭐⭐

```csharp
static void Process(Action callback)
{
    Console.WriteLine("Processing");

    callback();
}
```

Usage:

```csharp
Process(() =>
{
    Console.WriteLine("Completed");
});
```

Output:

```text
Processing
Completed
```

---

# 16. Events ⭐⭐⭐⭐

Events are based on delegates.

```csharp
class Process
{
    public delegate void Completed();

    public event Completed OnCompleted;

    public void Start()
    {
        Console.WriteLine("Started");

        OnCompleted?.Invoke();
    }
}
```

Usage:

```csharp
Process process = new();

process.OnCompleted += () =>
{
    Console.WriteLine("Finished");
};

process.Start();
```

Output:

```text
Started
Finished
```

---

# 17. Lambda with LINQ ⭐⭐⭐⭐⭐

Most important real-world usage.

```csharp
List<int> numbers =
    new() {1,2,3,4,5};

var evenNumbers =
    numbers.Where(
        x => x % 2 == 0);

foreach(var item in evenNumbers)
{
    Console.WriteLine(item);
}
```

Output:

```text
2
4
```

---

# 18. Closures ⭐⭐⭐

Lambda accessing outer variable.

```csharp
int number = 10;

Func<int> show =
    () => number;

Console.WriteLine(show());
```

Output:

```text
10
```

---

# 19. Expression Trees ⭐⭐

Used in LINQ Providers & Entity Framework.

```csharp
Expression<Func<int,int>>
```

Advanced topic.

---

# 20. Covariance & Contravariance ⭐⭐

Advanced delegate compatibility concepts.

Rarely asked for juniors.

---

# Real-World Example: Calculator ⭐⭐⭐⭐⭐

```csharp
Func<int,int,int> add =
    (x,y) => x + y;

Func<int,int,int> multiply =
    (x,y) => x * y;

Console.WriteLine(add(10,20));

Console.WriteLine(multiply(10,20));
```

---

# Real-World Example: Filtering Data ⭐⭐⭐⭐⭐

```csharp
List<int> numbers =
    new() {1,2,3,4,5,6};

var even =
    numbers.Where(
        x => x % 2 == 0);

foreach(var item in even)
{
    Console.WriteLine(item);
}
```

Output:

```text
2
4
6
```

---

# Common Mistakes

## Forgetting Delegate Signature Match

❌ Wrong

```csharp
delegate int AddDelegate(int a,int b);

void Show()
{
}
```

Method signature must match.

---

## Null Delegate Invocation

❌ Wrong

```csharp
notify();
```

If delegate is null → Exception.

✅ Better

```csharp
notify?.Invoke();
```

---

# 🎯 Most Asked Interview Questions

## Q1. What is a Delegate?

A type-safe function pointer that stores method references.

---

## Q2. Why Are Delegates Used?

- Events
- Callbacks
- LINQ
- Async Programming

---

## Q3. What is a Lambda Expression?

Short syntax for anonymous functions.

```csharp
x => x * x
```

---

## Q4. Difference Between Delegate and Lambda?

| Delegate | Lambda |
|----------|---------|
| Type | Syntax |
| Stores Method | Creates Anonymous Function |

---

## Q5. Difference Between Func and Action?

| Func | Action |
|------|--------|
| Returns Value | No Return |
| Used for Calculations | Used for Tasks |

---

## Q6. What is Predicate?

Delegate returning boolean.

```csharp
Predicate<int>
```

---

## Q7. What is Multicast Delegate?

Delegate calling multiple methods.

---

## Q8. What is Anonymous Method?

Method without a name.

```csharp
delegate()
{
}
```

---

## Q9. What is Callback Function?

A method passed as parameter and executed later.

---

## Q10. What are Events?

Events are wrappers around delegates used for notifications.

---

## Q11. Why Are Lambdas Important in LINQ?

LINQ heavily depends on lambda expressions for querying data.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Delegate Basics
- [x] Delegate Syntax
- [x] Multicast Delegates
- [x] Anonymous Methods
- [x] Lambda Expressions
- [x] Func
- [x] Action
- [x] Predicate
- [x] Delegate as Parameter
- [x] Callback Methods
- [x] Events
- [x] Lambda with LINQ
- [x] Expression Lambdas
- [x] Statement Lambdas

Mastering these topics will help you answer **95%+ of Delegate & Lambda Expression interview questions in .NET Full Stack Development.**