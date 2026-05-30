# .NET Full Stack Interview Preparation Guide

A comprehensive guide covering the most frequently asked .NET interview concepts for Full Stack Developers.

---

# Table of Contents

1. Value Types vs Reference Types
2. var vs dynamic vs object
3. Nullable Types
4. Type Casting
5. Boxing and Unboxing
6. Enums
7. Arrays vs Collections
8. String vs StringBuilder
9. Records vs Classes
10. Stack vs Heap Memory

---

# 1. Value Types vs Reference Types

One of the most fundamental concepts in .NET.

## Value Types

Value types store the actual data directly.

### Examples

```csharp
int age = 25;
double salary = 50000.50;
bool isActive = true;
char grade = 'A';
```

### Common Value Types

- int
- long
- float
- double
- decimal
- bool
- char
- struct
- enum

### Characteristics

- Usually stored on the Stack
- Contains actual value
- Assignment copies data
- Faster access
- Cannot be null unless nullable

### Example

```csharp
int a = 10;
int b = a;

b = 20;

Console.WriteLine(a); // 10
Console.WriteLine(b); // 20
```

A copy is created.

---

## Reference Types

Reference types store a memory reference to an object.

### Examples

```csharp
string name = "Sabbir";
List<int> numbers = new List<int>();
```

### Common Reference Types

- class
- string
- array
- interface
- delegate
- object

### Characteristics

- Object stored on Heap
- Variable stores reference
- Assignment copies reference
- Multiple variables can point to same object

### Example

```csharp
Person p1 = new Person();
p1.Name = "John";

Person p2 = p1;

p2.Name = "Mike";

Console.WriteLine(p1.Name); // Mike
```

### Interview Answer

> Value types store actual data and are usually allocated on the stack, while reference types store references to objects located on the heap. Assigning a value type creates a copy of the data, whereas assigning a reference type copies the reference.

---

# 2. var vs dynamic vs object

## var

Introduced in C# 3.0.

Type is determined at compile time.

```csharp
var name = "Sabbir";
var age = 25;
```

Compiler translates to:

```csharp
string name = "Sabbir";
int age = 25;
```

### Characteristics

- Strongly typed
- Compile-time checking
- Type cannot change

```csharp
var value = 10;

value = "Hello"; // Error
```

---

## dynamic

Introduced in C# 4.0.

Type resolution happens at runtime.

```csharp
dynamic value = 10;

value = "Hello";
value = true;
```

### Characteristics

- Runtime binding
- No compile-time checking
- Slower
- More error-prone

```csharp
dynamic x = "Hello";

Console.WriteLine(x.Length);
```

Works.

```csharp
dynamic x = 10;

Console.WriteLine(x.Length);
```

Runtime Exception.

---

## object

Base type of all .NET types.

```csharp
object value = 10;

value = "Hello";
```

### Characteristics

- Can hold any type
- Compile-time checking
- Usually requires casting

```csharp
object value = "Hello";

string text = (string)value;
```

### Comparison Table

| Feature | var | dynamic | object |
|----------|----------|----------|----------|
| Type Determined | Compile Time | Runtime | Compile Time |
| Type Safe | ✅ Yes | ❌ No | ✅ Yes |
| Performance | Fast | Slower | Good |
| Casting Needed | No | No | Often |
| IntelliSense | Full | Limited | Limited |

---

# 3. Nullable Types

Normally value types cannot be null.

```csharp
int age = null; // Error
```

Nullable types solve this.

```csharp
int? age = null;
```

Equivalent to:

```csharp
Nullable<int> age = null;
```

## Why Use Nullable Types?

Database columns may contain NULL.

```csharp
public int? ManagerId { get; set; }
```

---

## Checking Values

```csharp
int? age = 25;

if(age.HasValue)
{
    Console.WriteLine(age.Value);
}
```

---

## Null-Coalescing Operator

```csharp
int? age = null;

int result = age ?? 0;

Console.WriteLine(result);
```

Output:

```text
0
```

---

# 4. Type Casting

## Implicit Casting

Automatic conversion.

```csharp
int number = 100;

long largeNumber = number;
```

Safe conversion.

Examples:

- int → long
- float → double

---

## Explicit Casting

Manual conversion.

```csharp
double salary = 5000.75;

int amount = (int)salary;
```

Output:

```text
5000
```

Fraction is lost.

---

## Convert Class

```csharp
string number = "100";

int value = Convert.ToInt32(number);
```

---

## Parse

```csharp
int value = int.Parse("100");
```

---

## TryParse

Safest approach.

```csharp
bool success = int.TryParse("100", out int result);
```

---

# 5. Boxing and Unboxing

## Boxing

Value Type → Object Type

```csharp
int number = 10;

object obj = number;
```

---

## Unboxing

Object Type → Value Type

```csharp
object obj = 10;

int number = (int)obj;
```

---

## Performance Cost

Boxing allocates memory on the heap.

```csharp
int number = 10;

object obj = number;
```

Can increase:

- Heap usage
- Garbage Collection
- Memory overhead

---

# 6. Enums

Enums represent a fixed set of named constants.

```csharp
public enum OrderStatus
{
    Pending,
    Approved,
    Rejected
}
```

Usage:

```csharp
OrderStatus status = OrderStatus.Approved;
```

---

# 7. Arrays vs Collections

## Array

```csharp
int[] numbers = {1,2,3,4};
```

### Characteristics

- Fixed size
- Fast access
- Less flexible

---

## Collections

### List<T>

```csharp
List<string> names = new();
```

### Dictionary<TKey,TValue>

```csharp
Dictionary<int,string> users =
    new Dictionary<int,string>();
```

### HashSet<T>

```csharp
HashSet<int> ids = new();
```

### Queue<T>

```csharp
Queue<string> queue = new();
```

### Stack<T>

```csharp
Stack<string> stack = new();
```

### Comparison

| Feature | Array | Collection |
|----------|----------|----------|
| Size | Fixed | Dynamic |
| Performance | Faster | Slight Overhead |
| Flexibility | Low | High |
| Add/Remove | Difficult | Easy |

---

# 8. String vs StringBuilder

## String

Immutable.

```csharp
string name = "Sabbir";

name += " Hossain";
```

Creates a new object.

---

## StringBuilder

Mutable.

```csharp
StringBuilder sb = new StringBuilder();

sb.Append("Hello");
sb.Append(" World");
```

Efficient for repeated modifications.

---

## When to Use

### Use String

```csharp
string fullName = firstName + lastName;
```

### Use StringBuilder

- Reports
- CSV generation
- JSON generation
- Large loops

---

# 9. Records vs Classes

## Class

```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

Reference equality.

---

## Record

```csharp
public record Employee(
    int Id,
    string Name
);
```

Value-based equality.

```csharp
var e1 = new Employee(1,"John");
var e2 = new Employee(1,"John");

Console.WriteLine(e1 == e2);
```

Output:

```text
True
```

---

# 10. Stack vs Heap Memory

## Stack

Stores:

- Local variables
- Method parameters
- Value types

```csharp
int age = 25;
```

### Characteristics

- Fast allocation
- Auto cleanup
- Small memory area

---

## Heap

Stores:

- Objects
- Arrays
- Classes

```csharp
Person person = new Person();
```

Object stored in Heap.

Reference stored in Stack.

---

## Garbage Collector (GC)

```csharp
Person p = new Person();

p = null;
```

Object becomes eligible for garbage collection.

---

# Quick Interview Revision

✅ Value Type → Stores actual value

✅ Reference Type → Stores reference to heap object

✅ var → Compile-time type inference

✅ dynamic → Runtime type resolution

✅ object → Base type of all .NET types

✅ Nullable → Allows value types to hold null

✅ Implicit Casting → Automatic conversion

✅ Explicit Casting → Manual conversion

✅ Boxing → Value Type → Object

✅ Unboxing → Object → Value Type

✅ Enum → Named constants

✅ Array → Fixed size

✅ Collection → Dynamic size

✅ String → Immutable

✅ StringBuilder → Mutable

✅ Record → Value-based equality

✅ Class → Behavior-focused reference type

✅ Stack → Fast memory allocation

✅ Heap → Managed by Garbage Collector

---
