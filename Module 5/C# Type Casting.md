# C# Type Casting (Easy to Advanced)

## 📌 What is Type Casting?

Type Casting is the process of converting one data type into another data type.

### Why do we need Casting?

- Convert compatible types
- Work with different data types
- Handle user input
- Read data from databases/APIs
- Support inheritance and polymorphism
- Prevent runtime errors

---

# ⭐ Recommended Learning Order (Most Important First)

If you're preparing for a **.NET Full Stack Interview**, focus on these topics first:

| Priority | Topic | Interview Frequency |
|----------|--------|---------------------|
| ⭐⭐⭐⭐⭐ | Implicit Casting | Very High |
| ⭐⭐⭐⭐⭐ | Explicit Casting | Very High |
| ⭐⭐⭐⭐⭐ | Parse vs TryParse | Very High |
| ⭐⭐⭐⭐⭐ | Convert Class | Very High |
| ⭐⭐⭐⭐⭐ | Boxing & Unboxing | Very High |
| ⭐⭐⭐⭐⭐ | Upcasting & Downcasting | Very High |
| ⭐⭐⭐⭐⭐ | `is` and `as` Operators | Very High |
| ⭐⭐⭐⭐ | Pattern Matching | High |
| ⭐⭐⭐⭐ | Checked & Unchecked | High |
| ⭐⭐⭐ | Object Casting | Medium |
| ⭐⭐⭐ | Dynamic Casting | Medium |
| ⭐⭐ | Generic Casting | Low |
| ⭐⭐ | Custom Conversion Operators | Low |
| ⭐ | Advanced Conversion Operators | Rare |

---

# 1. Implicit Casting ⭐⭐⭐⭐⭐

Automatic conversion from a smaller type to a larger compatible type.

## Example

```csharp
int number = 100;

long longNumber = number;
float floatNumber = number;
double doubleNumber = number;
```

### Memory Flow

```text
byte → short → int → long → float → double → decimal
```

### Why?

No risk of data loss.

---

# 2. Explicit Casting ⭐⭐⭐⭐⭐

Manual conversion from a larger type to a smaller type.

## Example

```csharp
double price = 99.99;

int result = (int)price;

Console.WriteLine(result);
```

Output:

```text
99
```

### Why?

The decimal part is removed.

---

# 3. Data Loss During Casting ⭐⭐⭐⭐

```csharp
double value = 10.99;

int result = (int)value;

Console.WriteLine(result);
```

Output:

```text
10
```

The fractional part is truncated, not rounded.

---

# 4. Overflow During Casting ⭐⭐⭐⭐

```csharp
int number = 300;

byte value = (byte)number;

Console.WriteLine(value);
```

Output:

```text
44
```

### Why?

```text
Byte Range = 0 - 255
```

300 exceeds the limit.

---

# 5. Checked and Unchecked ⭐⭐⭐⭐

## Checked

Throws exception when overflow occurs.

```csharp
checked
{
    int number = 300;
    byte value = (byte)number;
}
```

Output:

```text
OverflowException
```

---

## Unchecked

Ignores overflow.

```csharp
unchecked
{
    int number = 300;
    byte value = (byte)number;
}
```

---

# 6. Convert Class ⭐⭐⭐⭐⭐

Used for safe type conversion.

## Example

```csharp
string age = "25";

int result = Convert.ToInt32(age);

Console.WriteLine(result);
```

Output:

```text
25
```

---

## Convert vs Casting

### Casting

```csharp
double value = 10.9;

int result = (int)value;
```

Output:

```text
10
```

---

### Convert

```csharp
double value = 10.9;

int result = Convert.ToInt32(value);
```

Output:

```text
11
```

Convert rounds the value.

---

# 7. Parse Method ⭐⭐⭐⭐

Converts string into a specific type.

```csharp
string age = "25";

int result = int.Parse(age);
```

---

## Problem

```csharp
string age = "ABC";

int.Parse(age);
```

Output:

```text
FormatException
```

---

# 8. TryParse Method ⭐⭐⭐⭐⭐

Safest way to convert user input.

```csharp
string age = "25";

bool success = int.TryParse(age, out int result);

Console.WriteLine(success);
Console.WriteLine(result);
```

Output:

```text
True
25
```

---

## Invalid Input

```csharp
string age = "ABC";

bool success = int.TryParse(age, out int result);

Console.WriteLine(success);
```

Output:

```text
False
```

No exception occurs.

---

# 9. ToString() ⭐⭐⭐⭐

Converts values to string.

```csharp
int age = 25;

string text = age.ToString();
```

---

# 10. Boxing ⭐⭐⭐⭐⭐

Converting a Value Type to a Reference Type.

```csharp
int value = 100;

object obj = value;
```

### What Happens?

```text
Stack → Heap
```

---

# 11. Unboxing ⭐⭐⭐⭐⭐

Converting a Reference Type back to a Value Type.

```csharp
object obj = 100;

int value = (int)obj;
```

### What Happens?

```text
Heap → Stack
```

---

# 12. Boxing and Unboxing Example ⭐⭐⭐⭐⭐

```csharp
int value = 100;

object obj = value;     // Boxing

int result = (int)obj;  // Unboxing
```

---

# 13. Upcasting ⭐⭐⭐⭐⭐

Child Object → Parent Reference

```csharp
class Animal
{
}

class Dog : Animal
{
}

Dog dog = new Dog();

Animal animal = dog;
```

### Why?

Every Dog is an Animal.

---

# 14. Downcasting ⭐⭐⭐⭐⭐

Parent Reference → Child Reference

```csharp
Animal animal = new Dog();

Dog dog = (Dog)animal;
```

---

# 15. Invalid Downcasting ⭐⭐⭐⭐

```csharp
Animal animal = new Animal();

Dog dog = (Dog)animal;
```

Output:

```text
InvalidCastException
```

---

# 16. Safe Casting Using `as` ⭐⭐⭐⭐⭐

```csharp
Animal animal = new Animal();

Dog? dog = animal as Dog;

if (dog != null)
{
    Console.WriteLine("Success");
}
```

Returns `null` instead of throwing an exception.

---

# 17. Safe Type Checking Using `is` ⭐⭐⭐⭐⭐

```csharp
Animal animal = new Dog();

if (animal is Dog)
{
    Dog dog = (Dog)animal;
}
```

---

# 18. Pattern Matching ⭐⭐⭐⭐

Modern C# approach.

```csharp
Animal animal = new Dog();

if (animal is Dog dog)
{
    Console.WriteLine("Dog Found");
}
```

Recommended over traditional casting.

---

# 19. Object Casting ⭐⭐⭐

```csharp
object value = "Hello";

string text = (string)value;
```

---

## Safe Version

```csharp
if (value is string text)
{
    Console.WriteLine(text);
}
```

---

# 20. Dynamic Casting ⭐⭐⭐

```csharp
dynamic data = "Hello";

int value = data;
```

Output:

```text
Runtime Exception
```

Dynamic skips compile-time checking.

---

# 21. Generic Casting ⭐⭐

```csharp
List<object> items = new()
{
    10,
    20,
    30
};

int value = (int)items[0];
```

---

# 22. Custom Conversion Operator ⭐⭐

```csharp
class Meter
{
    public double Value { get; set; }

    public static implicit operator double(Meter meter)
    {
        return meter.Value;
    }
}
```

Usage:

```csharp
Meter meter = new Meter
{
    Value = 100
};

double value = meter;
```

---

# 23. Custom Explicit Operator ⭐⭐

```csharp
class Meter
{
    public double Value { get; set; }

    public static explicit operator int(Meter meter)
    {
        return (int)meter.Value;
    }
}
```

Usage:

```csharp
int value = (int)meter;
```

---

# Common Casting Exceptions

| Exception | Cause |
|------------|---------|
| `InvalidCastException` | Invalid cast |
| `FormatException` | Invalid string format |
| `OverflowException` | Value exceeds range |
| `NullReferenceException` | Accessing null object |

---

# 🎯 Most Asked Interview Questions

## Q1. What is Type Casting?

Converting one data type into another data type.

---

## Q2. Difference Between Implicit and Explicit Casting?

### Implicit

```csharp
int a = 10;
double b = a;
```

Automatic conversion.

### Explicit

```csharp
double a = 10.5;
int b = (int)a;
```

Manual conversion.

---

## Q3. Difference Between Parse and TryParse?

| Parse | TryParse |
|---------|---------|
| Throws Exception | Returns False |
| Less Safe | More Safe |
| Used when input is guaranteed | Used for user input |

---

## Q4. Difference Between Convert and Casting?

### Casting

```csharp
(int)10.9
```

Output:

```text
10
```

### Convert

```csharp
Convert.ToInt32(10.9)
```

Output:

```text
11
```

---

## Q5. What is Boxing?

```csharp
int value = 10;

object obj = value;
```

Value Type → Reference Type.

---

## Q6. What is Unboxing?

```csharp
object obj = 10;

int value = (int)obj;
```

Reference Type → Value Type.

---

## Q7. What is Upcasting?

```csharp
Dog dog = new Dog();

Animal animal = dog;
```

Child → Parent.

---

## Q8. What is Downcasting?

```csharp
Animal animal = new Dog();

Dog dog = (Dog)animal;
```

Parent → Child.

---

## Q9. Difference Between `is` and `as`?

| `is` | `as` |
|------|------|
| Checks Type | Performs Safe Cast |
| Returns bool | Returns object/null |

---

## Q10. Why is Boxing Expensive?

- Creates object on Heap
- Additional memory allocation
- Extra Garbage Collection
- Slower than direct value types

---

# 🚀 Must-Master Topics Before Interview

- [x] Implicit Casting
- [x] Explicit Casting
- [x] Convert Class
- [x] Parse vs TryParse
- [x] ToString()
- [x] Boxing & Unboxing
- [x] Upcasting & Downcasting
- [x] `is` Operator
- [x] `as` Operator
- [x] Pattern Matching
- [x] Checked & Unchecked

If you master the topics above, you'll be able to answer around **90% of C#/.NET casting interview questions**.