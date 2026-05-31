# C# OOP Fundamentals (Without the Four Pillars)

> This guide focuses on **Class, Object, Constructors, Fields, Properties, Methods, Static Members, Memory Concepts, Object Initialization, and Other Core OOP Concepts**, excluding the four pillars (Encapsulation, Inheritance, Polymorphism, Abstraction).

---

# 🎯 Recommended Learning Order (Most Important First)

For .NET Full Stack Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | Class | Very High |
| ⭐⭐⭐⭐⭐ | Object | Very High |
| ⭐⭐⭐⭐⭐ | Fields | Very High |
| ⭐⭐⭐⭐⭐ | Properties | Very High |
| ⭐⭐⭐⭐⭐ | Constructors | Very High |
| ⭐⭐⭐⭐⭐ | Methods in Class | Very High |
| ⭐⭐⭐⭐⭐ | Static Members | Very High |
| ⭐⭐⭐⭐⭐ | Object Initialization | Very High |
| ⭐⭐⭐⭐ | Constructor Overloading | High |
| ⭐⭐⭐⭐ | Readonly Fields | High |
| ⭐⭐⭐⭐ | Constants | High |
| ⭐⭐⭐⭐ | this Keyword | High |
| ⭐⭐⭐⭐ | Object Lifetime | High |
| ⭐⭐⭐⭐ | Reference Type Behavior | High |
| ⭐⭐⭐ | Destructor | Medium |
| ⭐⭐⭐ | Anonymous Objects | Medium |
| ⭐⭐⭐ | Nested Classes | Medium |
| ⭐⭐ | Object Class Methods | Low |
| ⭐⭐ | Partial Classes | Low |

---

# 1. What is OOP?

Object-Oriented Programming (OOP) is a programming approach where software is designed using objects that represent real-world entities.

Examples:

```text
Student
Car
Employee
Product
Order
Customer
```

Each object contains:

```text
Data  → Properties/Fields
Behavior → Methods
```

---

# 2. What is a Class? ⭐⭐⭐⭐⭐

A Class is a blueprint or template for creating objects.

### Example

```csharp
class Student
{
    public string Name;
    public int Age;
}
```

### Real-Life Example

```text
Class = Car Design

Object = Toyota Car
Object = Honda Car
Object = BMW Car
```

---

# 3. What is an Object? ⭐⭐⭐⭐⭐

An Object is an instance of a class.

### Example

```csharp
Student student = new Student();
```

Here:

```text
Student → Class

student → Object
```

---

# 4. Creating Multiple Objects ⭐⭐⭐⭐⭐

```csharp
Student student1 = new Student();
Student student2 = new Student();
Student student3 = new Student();
```

Each object has its own data.

```csharp
student1.Name = "Sabbir";
student2.Name = "Rahim";
student3.Name = "Karim";
```

---

# 5. Fields ⭐⭐⭐⭐⭐

Fields store data inside a class.

```csharp
class Student
{
    public string Name;
    public int Age;
}
```

Usage:

```csharp
Student student = new();

student.Name = "Sabbir";
student.Age = 25;
```

---

# 6. Methods Inside Class ⭐⭐⭐⭐⭐

Methods define behavior.

```csharp
class Student
{
    public string Name;

    public void Display()
    {
        Console.WriteLine(Name);
    }
}
```

Usage:

```csharp
Student student = new();

student.Name = "Sabbir";

student.Display();
```

Output:

```text
Sabbir
```

---

# 7. Properties ⭐⭐⭐⭐⭐

Properties provide controlled access to data.

### Auto Property

```csharp
class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
}
```

Usage:

```csharp
Student student = new();

student.Name = "Sabbir";
student.Age = 25;
```

---

# Why Properties Are Preferred Over Fields

❌ Field

```csharp
public string Name;
```

✅ Property

```csharp
public string Name { get; set; }
```

Advantages:

- Validation
- Flexibility
- Data Binding
- Better Design

---

# 8. Constructor ⭐⭐⭐⭐⭐

Special method that runs automatically when object is created.

```csharp
class Student
{
    public Student()
    {
        Console.WriteLine("Constructor Called");
    }
}
```

Usage:

```csharp
Student student = new Student();
```

Output:

```text
Constructor Called
```

---

# 9. Parameterized Constructor ⭐⭐⭐⭐⭐

```csharp
class Student
{
    public string Name;

    public Student(string name)
    {
        Name = name;
    }
}
```

Usage:

```csharp
Student student =
    new Student("Sabbir");
```

---

# 10. Constructor Overloading ⭐⭐⭐⭐

Multiple constructors.

```csharp
class Student
{
    public Student()
    {
    }

    public Student(string name)
    {
    }

    public Student(string name, int age)
    {
    }
}
```

---

# 11. this Keyword ⭐⭐⭐⭐

Refers to current object.

```csharp
class Student
{
    public string Name;

    public Student(string name)
    {
        this.Name = name;
    }
}
```

---

# 12. Object Initializer ⭐⭐⭐⭐⭐

Cleaner object creation.

```csharp
Student student = new()
{
    Name = "Sabbir",
    Age = 25
};
```

Instead of:

```csharp
Student student = new();

student.Name = "Sabbir";
student.Age = 25;
```

---

# 13. Static Fields ⭐⭐⭐⭐⭐

Shared by all objects.

```csharp
class Student
{
    public static int Count = 0;

    public Student()
    {
        Count++;
    }
}
```

Usage:

```csharp
new Student();
new Student();

Console.WriteLine(Student.Count);
```

Output:

```text
2
```

---

# 14. Static Methods ⭐⭐⭐⭐⭐

Belong to class, not object.

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
Calculator.Add(10,20);
```

---

# 15. Instance Members ⭐⭐⭐⭐⭐

Require object creation.

```csharp
class Student
{
    public void Display()
    {
    }
}
```

Usage:

```csharp
Student student = new();

student.Display();
```

---

# Static vs Instance Members ⭐⭐⭐⭐⭐

| Static | Instance |
|----------|----------|
| Class Level | Object Level |
| Shared | Individual |
| No Object Required | Object Required |

---

# 16. Readonly Fields ⭐⭐⭐⭐

Can be assigned only once.

```csharp
class Student
{
    public readonly string University;

    public Student()
    {
        University = "DIU";
    }
}
```

---

# 17. Constants ⭐⭐⭐⭐

Compile-time constants.

```csharp
class AppConfig
{
    public const double PI = 3.14159;
}
```

Usage:

```csharp
Console.WriteLine(AppConfig.PI);
```

---

# readonly vs const ⭐⭐⭐⭐

| readonly | const |
|-----------|--------|
| Runtime | Compile Time |
| Can use Constructor | Cannot |
| More Flexible | Fixed Value |

---

# 18. Reference Type Behavior ⭐⭐⭐⭐

Classes are Reference Types.

```csharp
Student s1 = new();

Student s2 = s1;
```

Both reference same object.

```csharp
s2.Name = "Sabbir";

Console.WriteLine(s1.Name);
```

Output:

```text
Sabbir
```

---

# 19. Object Lifetime ⭐⭐⭐⭐

Object exists until Garbage Collector removes it.

```csharp
Student student = new Student();
```

Memory:

```text
Reference → Stack

Object → Heap
```

---

# 20. Destructor ⭐⭐⭐

Runs before object is destroyed.

```csharp
class Student
{
    ~Student()
    {
        Console.WriteLine("Destroyed");
    }
}
```

Rarely used in modern C#.

---

# 21. Anonymous Object ⭐⭐⭐

```csharp
var student = new
{
    Name = "Sabbir",
    Age = 25
};
```

Usage:

```csharp
Console.WriteLine(student.Name);
```

---

# 22. Nested Class ⭐⭐⭐

```csharp
class Outer
{
    public class Inner
    {
    }
}
```

Usage:

```csharp
Outer.Inner obj =
    new Outer.Inner();
```

---

# 23. Partial Class ⭐⭐

Split a class into multiple files.

File 1:

```csharp
public partial class Student
{
    public string Name { get; set; }
}
```

File 2:

```csharp
public partial class Student
{
    public int Age { get; set; }
}
```

Compiler combines them.

---

# 24. Object Class ⭐⭐

Every class inherits basic methods.

```csharp
ToString()

Equals()

GetHashCode()

GetType()
```

Example:

```csharp
Student student = new();

Console.WriteLine(
    student.GetType());
```

---

# Real-World Example: Student Class ⭐⭐⭐⭐⭐

```csharp
class Student
{
    public string Name { get; set; }

    public int Age { get; set; }

    public Student(
        string name,
        int age)
    {
        Name = name;
        Age = age;
    }

    public void Display()
    {
        Console.WriteLine(
            $"{Name} - {Age}");
    }
}
```

Usage:

```csharp
Student student =
    new Student(
        "Sabbir",
        25);

student.Display();
```

Output:

```text
Sabbir - 25
```

---

# Common Mistakes

## Forgetting Object Creation

❌ Wrong

```csharp
Student.Display();
```

Instance method requires object.

---

## Accessing Null Object

❌ Wrong

```csharp
Student student = null;

student.Display();
```

Output:

```text
NullReferenceException
```

---

# 🎯 Most Asked Interview Questions

## Q1. What is a Class?

A blueprint used to create objects.

---

## Q2. What is an Object?

An instance of a class.

---

## Q3. Difference Between Class and Object?

| Class | Object |
|---------|---------|
| Blueprint | Instance |
| Logical | Physical |
| No Memory | Uses Memory |

---

## Q4. What is a Constructor?

A special method that executes automatically when an object is created.

---

## Q5. Can a Constructor Return a Value?

No.

Constructors cannot have return types.

---

## Q6. What is Constructor Overloading?

Multiple constructors with different parameters.

---

## Q7. What is the `this` Keyword?

Reference to current object.

---

## Q8. Difference Between Field and Property?

| Field | Property |
|----------|----------|
| Stores Data | Controls Access |
| Less Flexible | More Flexible |

---

## Q9. Difference Between Static and Instance Members?

| Static | Instance |
|----------|----------|
| Shared | Individual |
| No Object | Object Required |

---

## Q10. Difference Between readonly and const?

| readonly | const |
|-----------|--------|
| Runtime | Compile Time |
| Constructor Allowed | Not Allowed |

---

## Q11. Where Are Objects Stored?

```text
Object → Heap

Reference → Stack
```

---

## Q12. What is an Anonymous Object?

Object without a named class.

```csharp
var person = new
{
    Name = "Sabbir"
};
```

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Class
- [x] Object
- [x] Fields
- [x] Properties
- [x] Methods in Class
- [x] Constructor
- [x] Parameterized Constructor
- [x] Constructor Overloading
- [x] this Keyword
- [x] Object Initializer
- [x] Static Members
- [x] Instance Members
- [x] readonly
- [x] const
- [x] Reference Type Behavior
- [x] Object Lifetime
- [x] Anonymous Objects

Mastering these topics will help you answer **90%+ of OOP Fundamentals questions before moving to the Four Pillars (Encapsulation, Inheritance, Polymorphism, and Abstraction).**