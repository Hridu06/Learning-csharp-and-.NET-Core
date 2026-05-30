# C# Operators (Easy to Advanced)

Operators are special symbols that perform operations on variables and values.

```csharp
int a = 10;
int b = 5;

int result = a + b; // + is an operator
```

---

# 1. Arithmetic Operators (Basic)

Used for mathematical calculations.

| Operator | Meaning | Example |
|----------|----------|----------|
| `+` | Addition | `a + b` |
| `-` | Subtraction | `a - b` |
| `*` | Multiplication | `a * b` |
| `/` | Division | `a / b` |
| `%` | Modulus (Remainder) | `a % b` |

### Example

```csharp
int a = 10;
int b = 3;

Console.WriteLine(a + b); // 13
Console.WriteLine(a - b); // 7
Console.WriteLine(a * b); // 30
Console.WriteLine(a / b); // 3
Console.WriteLine(a % b); // 1
```

---

# 2. Assignment Operators

Used to assign values to variables.

| Operator | Example | Same As |
|----------|----------|----------|
| `=` | `x = 10` | Assign value |
| `+=` | `x += 5` | `x = x + 5` |
| `-=` | `x -= 5` | `x = x - 5` |
| `*=` | `x *= 5` | `x = x * 5` |
| `/=` | `x /= 5` | `x = x / 5` |
| `%=` | `x %= 5` | `x = x % 5` |

### Example

```csharp
int x = 10;

x += 5; // 15
x -= 2; // 13
x *= 2; // 26
```

---

# 3. Comparison (Relational) Operators

Used to compare values.

Result is always `true` or `false`.

| Operator | Meaning |
|----------|----------|
| `==` | Equal to |
| `!=` | Not equal to |
| `>` | Greater than |
| `<` | Less than |
| `>=` | Greater than or equal |
| `<=` | Less than or equal |

### Example

```csharp
int age = 20;

Console.WriteLine(age > 18);  // True
Console.WriteLine(age < 18);  // False
Console.WriteLine(age == 20); // True
```

---

# 4. Logical Operators

Used to combine conditions.

| Operator | Meaning |
|----------|----------|
| `&&` | AND |
| `||` | OR |
| `!` | NOT |

### Example

```csharp
int age = 25;
bool hasLicense = true;

Console.WriteLine(age >= 18 && hasLicense);
// True
```

### AND (`&&`)

Both conditions must be true.

```csharp
true && true  // True
true && false // False
```

### OR (`||`)

At least one condition must be true.

```csharp
true || false // True
```

### NOT (`!`)

Reverses result.

```csharp
bool isStudent = true;

Console.WriteLine(!isStudent);
// False
```

---

# 5. Increment and Decrement Operators

## Increment (`++`)

Adds 1.

```csharp
int x = 5;
x++;

Console.WriteLine(x); // 6
```

## Decrement (`--`)

Subtracts 1.

```csharp
int x = 5;
x--;

Console.WriteLine(x); // 4
```

---

# 6. Unary Operators

Operate on a single operand.

| Operator | Meaning |
|----------|----------|
| `+x` | Positive |
| `-x` | Negative |
| `++x` | Pre-increment |
| `--x` | Pre-decrement |
| `!x` | Logical NOT |

### Example

```csharp
int x = 10;

Console.WriteLine(-x); // -10
Console.WriteLine(+x); // 10
```

---

# 7. Pre-Increment vs Post-Increment

## Post Increment

```csharp
int x = 5;

Console.WriteLine(x++); // 5
Console.WriteLine(x);   // 6
```

## Pre Increment

```csharp
int x = 5;

Console.WriteLine(++x); // 6
Console.WriteLine(x);   // 6
```

### Interview Question

```csharp
int a = 5;
int b = a++;

Console.WriteLine(a); // 6
Console.WriteLine(b); // 5
```

---

# 8. Conditional (Ternary) Operator

Short form of `if-else`.

### Syntax

```csharp
condition ? trueValue : falseValue;
```

### Example

```csharp
int age = 20;

string result = age >= 18 ? "Adult" : "Minor";

Console.WriteLine(result);
```

---

# 9. Null-Coalescing Operator (`??`)

Returns first non-null value.

```csharp
string? name = null;

string result = name ?? "Guest";

Console.WriteLine(result);
// Guest
```

---

# 10. Null-Coalescing Assignment (`??=`)

Assigns value only if variable is null.

```csharp
string? name = null;

name ??= "Guest";

Console.WriteLine(name);
```

---

# 11. Null Conditional Operators

## `?.`

```csharp
Student? student = null;

Console.WriteLine(student?.Name);
```

## `?[]`

```csharp
int[]? numbers = null;

Console.WriteLine(numbers?[0]);
```

---

# 12. Bitwise Operators

Work directly on binary bits.

| Operator | Meaning |
|----------|----------|
| `&` | Bitwise AND |
| `|` | Bitwise OR |
| `^` | XOR |
| `~` | NOT |
| `<<` | Left Shift |
| `>>` | Right Shift |

### Example

```csharp
int a = 5; // 0101
int b = 3; // 0011

Console.WriteLine(a & b); // 1
Console.WriteLine(a | b); // 7
```

---

# 13. Shift Operators

## Left Shift

```csharp
int x = 5;

Console.WriteLine(x << 1);
// 10
```

## Right Shift

```csharp
int x = 8;

Console.WriteLine(x >> 1);
// 4
```

---

# 14. Type Testing Operator (`is`)

Checks object type.

```csharp
object value = "Hello";

Console.WriteLine(value is string);
// True
```

### Pattern Matching

```csharp
object value = 100;

if (value is int number)
{
    Console.WriteLine(number);
}
```

---

# 15. Type Casting Operator (`as`)

Safe casting.

```csharp
object text = "Hello";

string? name = text as string;

Console.WriteLine(name);
```

Returns `null` if casting fails.

---

# 16. `typeof` Operator

Gets type information.

```csharp
Console.WriteLine(typeof(int));
Console.WriteLine(typeof(string));
```

---

# 17. `sizeof` Operator

Gets size of value types.

```csharp
Console.WriteLine(sizeof(int));
// 4
```

---

# 18. `nameof` Operator

Returns variable or member name as string.

```csharp
string name = "Sabbir";

Console.WriteLine(nameof(name));
// name
```

---

# 19. Member Access Operator (`.`)

Access class members.

```csharp
string name = "Sabbir";

Console.WriteLine(name.Length);
```

---

# 20. Index Operator (`[]`)

Access array elements.

```csharp
int[] numbers = { 10, 20, 30 };

Console.WriteLine(numbers[0]);
```

---

# 21. Range Operator (`..`) - Modern C#

```csharp
int[] nums = {1,2,3,4,5,6};

var result = nums[1..4];

foreach(var item in result)
{
    Console.WriteLine(item);
}
```

Output:

```text
2
3
4
```

---

# 22. Index From End Operator (`^`)

```csharp
int[] nums = {1,2,3,4,5};

Console.WriteLine(nums[^1]);
// 5
```

---

# 23. Lambda Operator (`=>`)

Used in LINQ and methods.

```csharp
var square = (int x) => x * x;

Console.WriteLine(square(5));
```

Output:

```text
25
```

---

# 24. Advanced Pattern Matching Operators

## `is not`

```csharp
if(value is not null)
{
    Console.WriteLine("Not Null");
}
```

## Property Pattern

```csharp
if(student is { Age: > 18 })
{
    Console.WriteLine("Adult");
}
```

---

# Interview-Wise Most Important Operators

✅ Arithmetic Operators (`+`, `-`, `*`, `/`, `%`)

✅ Assignment Operators (`=`, `+=`, `-=`, `*=`, `/=`)

✅ Comparison Operators (`==`, `!=`, `>`, `<`, `>=`, `<=`)

✅ Logical Operators (`&&`, `||`, `!`)

✅ Increment/Decrement (`++`, `--`)

✅ Ternary Operator (`?:`)

✅ Null-Coalescing (`??`, `??=`)

✅ Type Operators (`is`, `as`, `typeof`)

✅ Range (`..`) and Index (`^`)

✅ Lambda (`=>`)

---

# Learning Order (Recommended)

1. Arithmetic Operators
2. Assignment Operators
3. Comparison Operators
4. Logical Operators
5. Increment & Decrement
6. Unary Operators
7. Ternary Operator
8. Null Operators (`??`, `?.`, `??=`)
9. Type Operators (`is`, `as`, `typeof`)
10. Bitwise & Shift Operators
11. Range & Index Operators
12. Lambda Operator
13. Advanced Pattern Matching

Master the first 8 topics before moving to advanced operators.