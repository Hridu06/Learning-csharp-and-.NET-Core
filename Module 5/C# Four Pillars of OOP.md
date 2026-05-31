# C# Four Pillars of OOP (Easy to Advanced)

## 📌 What are the Four Pillars of OOP?

Object-Oriented Programming (OOP) is built on four core principles:

1. Encapsulation
2. Inheritance
3. Polymorphism
4. Abstraction

These four pillars help developers build:

- Secure Applications
- Reusable Code
- Scalable Systems
- Maintainable Software

---

# 🎯 Recommended Learning Order (Most Important First)

For .NET Full Stack Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | Encapsulation | Very High |
| ⭐⭐⭐⭐⭐ | Inheritance | Very High |
| ⭐⭐⭐⭐⭐ | Polymorphism | Very High |
| ⭐⭐⭐⭐⭐ | Abstraction | Very High |
| ⭐⭐⭐⭐⭐ | Access Modifiers | Very High |
| ⭐⭐⭐⭐⭐ | Method Overriding | Very High |
| ⭐⭐⭐⭐⭐ | Virtual & Override | Very High |
| ⭐⭐⭐⭐⭐ | Abstract Class | Very High |
| ⭐⭐⭐⭐⭐ | Interface | Very High |
| ⭐⭐⭐⭐ | Method Hiding (`new`) | High |
| ⭐⭐⭐⭐ | Multiple Interface Implementation | High |
| ⭐⭐⭐⭐ | Runtime Polymorphism | High |
| ⭐⭐⭐⭐ | Compile-Time Polymorphism | High |
| ⭐⭐⭐ | Sealed Class | Medium |
| ⭐⭐⭐ | Sealed Method | Medium |
| ⭐⭐⭐ | Upcasting & Downcasting | Medium |
| ⭐⭐ | Explicit Interface Implementation | Low |

---

# 1. Encapsulation ⭐⭐⭐⭐⭐

## What is Encapsulation?

Encapsulation means:

```text
Binding Data + Methods Together
&
Restricting Direct Access To Data
```

Encapsulation protects internal data from unauthorized access.

---

## Without Encapsulation

❌ Bad Practice

```csharp
class Student
{
    public int Age;
}
```

Usage:

```csharp
Student student = new();

student.Age = -50;
```

Invalid data can be stored.

---

## With Encapsulation

✅ Good Practice

```csharp
class Student
{
    private int age;

    public int Age
    {
        get
        {
            return age;
        }

        set
        {
            if(value > 0)
            {
                age = value;
            }
        }
    }
}
```

Usage:

```csharp
Student student = new();

student.Age = 25;

Console.WriteLine(student.Age);
```

Output:

```text
25
```

---

# Encapsulation Using Auto Properties

```csharp
class Student
{
    public string Name { get; set; }
}
```

---

# Encapsulation Benefits

- Data Security
- Validation
- Better Maintenance
- Controlled Access

---

# 2. Inheritance ⭐⭐⭐⭐⭐

## What is Inheritance?

Inheritance allows one class to acquire properties and methods of another class.

```text
Parent Class → Child Class
```

---

## Example

```csharp
class Animal
{
    public void Eat()
    {
        Console.WriteLine("Eating");
    }
}

class Dog : Animal
{
}
```

Usage:

```csharp
Dog dog = new();

dog.Eat();
```

Output:

```text
Eating
```

---

# Real Life Example

```text
Animal
   ↑
 Dog
   ↑
 GermanShepherd
```

---

# Types of Inheritance in C#

| Type | Supported |
|--------|------------|
| Single | ✅ |
| Multilevel | ✅ |
| Hierarchical | ✅ |
| Multiple Class Inheritance | ❌ |
| Multiple Interface Inheritance | ✅ |

---

# Single Inheritance

```csharp
class Animal
{
}

class Dog : Animal
{
}
```

---

# Multilevel Inheritance

```csharp
class Animal
{
}

class Dog : Animal
{
}

class Puppy : Dog
{
}
```

---

# Hierarchical Inheritance

```csharp
class Animal
{
}

class Dog : Animal
{
}

class Cat : Animal
{
}
```

---

# 3. Polymorphism ⭐⭐⭐⭐⭐

## What is Polymorphism?

Polymorphism means:

```text
One Name
Many Forms
```

---

# Types of Polymorphism

1. Compile-Time Polymorphism
2. Runtime Polymorphism

---

# Compile-Time Polymorphism

Achieved using:

```text
Method Overloading
```

---

## Example

```csharp
class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }
}
```

Usage:

```csharp
Calculator calculator = new();

calculator.Add(10,20);

calculator.Add(10.5,20.5);
```

---

# Runtime Polymorphism

Achieved using:

```text
Method Overriding
```

---

# Virtual Method

```csharp
class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Animal Sound");
    }
}
```

---

# Override Method

```csharp
class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Bark");
    }
}
```

Usage:

```csharp
Animal animal = new Dog();

animal.Sound();
```

Output:

```text
Bark
```

---

# Method Overloading vs Overriding

| Overloading | Overriding |
|-------------|------------|
| Same Class | Parent-Child |
| Compile Time | Runtime |
| Different Parameters | Same Parameters |
| No Inheritance Required | Inheritance Required |

---

# Method Hiding (`new`) ⭐⭐⭐⭐

```csharp
class Animal
{
    public void Sound()
    {
        Console.WriteLine("Animal");
    }
}

class Dog : Animal
{
    public new void Sound()
    {
        Console.WriteLine("Dog");
    }
}
```

---

# 4. Abstraction ⭐⭐⭐⭐⭐

## What is Abstraction?

Abstraction means:

```text
Showing Essential Information

Hiding Implementation Details
```

Example:

```text
ATM Machine

Visible:
Withdraw
Deposit

Hidden:
Database Queries
Transactions
Network Calls
```

---

# Abstraction Using Abstract Class

```csharp
abstract class Animal
{
    public abstract void Sound();
}
```

Cannot create object:

```csharp
Animal animal = new Animal();
```

❌ Error

---

# Derived Class

```csharp
class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Bark");
    }
}
```

Usage:

```csharp
Dog dog = new();

dog.Sound();
```

Output:

```text
Bark
```

---

# Interface ⭐⭐⭐⭐⭐

Another way to achieve abstraction.

---

## Interface Declaration

```csharp
interface IAnimal
{
    void Sound();
}
```

---

## Implementation

```csharp
class Dog : IAnimal
{
    public void Sound()
    {
        Console.WriteLine("Bark");
    }
}
```

Usage:

```csharp
Dog dog = new();

dog.Sound();
```

---

# Abstract Class vs Interface

| Abstract Class | Interface |
|---------------|-----------|
| Can Have Fields | Cannot |
| Constructor Allowed | Not Allowed |
| Partial Implementation | Full Contract |
| Single Inheritance | Multiple Supported |

---

# Multiple Interface Implementation

```csharp
interface IPrint
{
    void Print();
}

interface IScan
{
    void Scan();
}

class Printer : IPrint, IScan
{
    public void Print()
    {
        Console.WriteLine("Print");
    }

    public void Scan()
    {
        Console.WriteLine("Scan");
    }
}
```

---

# Explicit Interface Implementation

```csharp
interface IA
{
    void Show();
}

interface IB
{
    void Show();
}
```

Implementation:

```csharp
class Test : IA, IB
{
    void IA.Show()
    {
        Console.WriteLine("IA");
    }

    void IB.Show()
    {
        Console.WriteLine("IB");
    }
}
```

---

# Sealed Class ⭐⭐⭐

Cannot be inherited.

```csharp
sealed class Animal
{
}
```

---

# Sealed Method ⭐⭐⭐

Cannot be overridden further.

```csharp
class Animal
{
    public virtual void Sound()
    {
    }
}

class Dog : Animal
{
    public sealed override void Sound()
    {
    }
}
```

---

# Real-World Example: Banking System ⭐⭐⭐⭐⭐

```csharp
abstract class Account
{
    public abstract void Withdraw();
}

class SavingsAccount : Account
{
    public override void Withdraw()
    {
        Console.WriteLine(
            "Savings Withdrawal");
    }
}

class CurrentAccount : Account
{
    public override void Withdraw()
    {
        Console.WriteLine(
            "Current Withdrawal");
    }
}
```

Usage:

```csharp
Account account =
    new SavingsAccount();

account.Withdraw();
```

Output:

```text
Savings Withdrawal
```

---

# 🎯 Most Asked Interview Questions

## Q1. What are the Four Pillars of OOP?

1. Encapsulation
2. Inheritance
3. Polymorphism
4. Abstraction

---

## Q2. What is Encapsulation?

Binding data and methods together while restricting direct access to data.

---

## Q3. What is Inheritance?

Acquiring properties and methods of another class.

---

## Q4. What is Polymorphism?

One name having many forms.

---

## Q5. Types of Polymorphism?

1. Compile-Time
2. Runtime

---

## Q6. Difference Between Overloading and Overriding?

| Overloading | Overriding |
|------------|------------|
| Compile Time | Runtime |
| Same Class | Parent-Child |
| Different Parameters | Same Parameters |

---

## Q7. What is Abstraction?

Showing only essential features while hiding implementation details.

---

## Q8. Difference Between Abstract Class and Interface?

| Abstract Class | Interface |
|---------------|-----------|
| Can Have Constructor | Cannot |
| Can Have Fields | Cannot |
| Partial Implementation | Contract Only |

---

## Q9. Can We Create Object of Abstract Class?

No.

```csharp
abstract class Animal
{
}
```

Cannot instantiate.

---

## Q10. Why Doesn't C# Support Multiple Inheritance?

To avoid:

```text
Diamond Problem
```

C# allows multiple interfaces instead.

---

## Q11. What is Virtual Method?

A method that can be overridden.

```csharp
virtual
```

---

## Q12. What is Override Method?

A method that replaces parent implementation.

```csharp
override
```

---

## Q13. What is Sealed Class?

Cannot be inherited.

---

## Q14. What is Method Hiding?

Using:

```csharp
new
```

instead of:

```csharp
override
```

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Encapsulation
- [x] Access Modifiers
- [x] Properties
- [x] Inheritance
- [x] Single Inheritance
- [x] Multilevel Inheritance
- [x] Polymorphism
- [x] Method Overloading
- [x] Method Overriding
- [x] Virtual Methods
- [x] Override Methods
- [x] Abstraction
- [x] Abstract Classes
- [x] Interfaces
- [x] Abstract Class vs Interface
- [x] Multiple Interface Implementation
- [x] Sealed Classes

Mastering these topics will help you answer **95%+ of OOP interview questions for Junior, Mid-Level, and Senior .NET Developer positions.**