# C# Variables and Data Types Guide

## 1. What is a Variable?

A variable is a named storage location that holds data.

```csharp
string name = "Sabbir";
int age = 25;
```

Here:

* `name` is a variable
* `age` is a variable

---

# 2. Basic Data Types

## Integer Types

| Data Type | Size    | Example |
| --------- | ------- | ------- |
| byte      | 1 byte  | 255     |
| short     | 2 bytes | 32000   |
| int       | 4 bytes | 100     |
| long      | 8 bytes | 1000000 |

```csharp
byte marks = 90;
int age = 25;
long population = 8000000000;
```

## Decimal Types

| Data Type | Example |
| --------- | ------- |
| float     | 3.14f   |
| double    | 3.14159 |
| decimal   | 99.99m  |

```csharp
float pi = 3.14f;
double value = 3.1415926535;
decimal salary = 25000.75m;
```

## Character Type

```csharp
char grade = 'A';
```

## Boolean Type

```csharp
bool isActive = true;
bool isDeleted = false;
```

## String Type

```csharp
string name = "Hridoy";
```

---

# 3. Type Inference (`var`)

The compiler automatically determines the type.

```csharp
var name = "Sabbir";
var age = 25;
var salary = 25000.50;
```

Equivalent to:

```csharp
string name = "Sabbir";
int age = 25;
double salary = 25000.50;
```

---

# 4. Constants

A constant value cannot be changed.

```csharp
const double PI = 3.14159;
```

❌ Not Allowed:

```csharp
PI = 5.5;
```

---

# 5. Nullable Types

Normally value types cannot be `null`.

```csharp
int? age = null;
```

Useful in databases.

```csharp
DateTime? joiningDate = null;
```

---

# 6. Object Type

The base type of all C# types.

```csharp
object name = "Sabbir";
object age = 25;
object salary = 50000m;
```

---

# 7. Dynamic Type

Type checking happens at runtime.

```csharp
dynamic data = "Hello";

data = 100;
data = true;
```

### Difference

```csharp
var value = 100;      // Fixed type
dynamic value2 = 100; // Flexible type
```

---

# 8. Type Casting

## Implicit Casting

Small → Large

```csharp
int age = 25;
double ageDouble = age;
```

## Explicit Casting

Large → Small

```csharp
double price = 99.99;
int newPrice = (int)price;
```

Output:

```text
99
```

---

# 9. Parse and Convert

## Parse

```csharp
string number = "100";
int result = int.Parse(number);
```

## Convert

```csharp
string number = "100";
int result = Convert.ToInt32(number);
```

## TryParse

Safest way.

```csharp
string input = "100";

if(int.TryParse(input, out int result))
{
    Console.WriteLine(result);
}
```

---

# 10. Value Types vs Reference Types

## Value Types

Stored directly.

* int
* double
* decimal
* bool
* char
* struct

```csharp
int a = 10;
int b = a;

b = 20;

Console.WriteLine(a); // 10
```

## Reference Types

Stored by reference.

* string
* class
* array
* object
* dynamic

```csharp
Person p1 = new Person();
Person p2 = p1;
```

Both point to the same object.

---

# 11. Boxing and Unboxing

## Boxing

Value Type → Object

```csharp
int number = 100;

object obj = number;
```

## Unboxing

Object → Value Type

```csharp
object obj = 100;

int number = (int)obj;
```

---

# 12. Enum

Used for fixed values.

```csharp
enum Status
{
    Draft,
    Submitted,
    Approved,
    Rejected
}
```

Usage:

```csharp
Status currentStatus = Status.Approved;
```

---

# 13. Arrays

```csharp
int[] numbers = { 10, 20, 30, 40 };
```

Access:

```csharp
Console.WriteLine(numbers[0]);
```

---

# 14. Advanced: Anonymous Types

```csharp
var student = new
{
    Id = 1,
    Name = "Sabbir",
    Age = 25
};
```

---

# 15. Advanced: Tuple

```csharp
(string Name, int Age) person = ("Sabbir", 25);

Console.WriteLine(person.Name);
```

---

# 16. Advanced: Record Type

```csharp
public record Student(
    int Id,
    string Name
);
```

Usage:

```csharp
var student = new Student(1, "Sabbir");
```

---

# Interview Questions

## Difference Between `var`, `dynamic`, and `object`

| Feature       | var            | dynamic      | object           |
| ------------- | -------------- | ------------ | ---------------- |
| Type Checking | Compile Time   | Runtime      | Compile Time     |
| Typing        | Strongly Typed | Weakly Typed | Requires Casting |
| Performance   | Fast           | Slower       | Moderate         |

---

## Difference Between `int` and `int?`

```csharp
int age = 25;
int? age2 = null;
```

* `int` cannot be null.
* `int?` can store null values.

---

## Difference Between Value Type and Reference Type

### Value Type

* Stores actual value.
* Copied independently.

### Reference Type

* Stores memory address.
* Multiple variables can reference the same object.

---

## Difference Between Parse and TryParse

### Parse()

```csharp
int.Parse("100");
```

* Throws exception if conversion fails.

### TryParse()

```csharp
int.TryParse("100", out int result);
```

* Returns `true` or `false`.
* Safer and recommended.
