# C# Conditional Statements (Easy to Advanced)

## 📌 What are Conditional Statements?

Conditional Statements allow a program to make decisions and execute different blocks of code based on conditions.

Think of them like real-life decisions:

```text
If it rains → Take an umbrella.
Otherwise → Go outside normally.
```

In C#:

```csharp
if (isRaining)
{
    Console.WriteLine("Take an umbrella");
}
```

---

# 🎯 Recommended Learning Order (Most Important First)

If you're preparing for a **.NET Full Stack Developer Interview**, follow this order:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | if Statement | Very High |
| ⭐⭐⭐⭐⭐ | if-else Statement | Very High |
| ⭐⭐⭐⭐⭐ | else-if Ladder | Very High |
| ⭐⭐⭐⭐⭐ | Nested if | Very High |
| ⭐⭐⭐⭐⭐ | Ternary Operator (`?:`) | Very High |
| ⭐⭐⭐⭐⭐ | switch Statement | Very High |
| ⭐⭐⭐⭐ | switch Expression | High |
| ⭐⭐⭐⭐ | Pattern Matching in switch | High |
| ⭐⭐⭐⭐ | Logical Operators with Conditions | High |
| ⭐⭐⭐ | Null Conditional Checks | Medium |
| ⭐⭐⭐ | Null Coalescing in Conditions | Medium |
| ⭐⭐ | Advanced Pattern Matching | Low |
| ⭐⭐ | Relational Patterns | Low |

---

# 1. if Statement ⭐⭐⭐⭐⭐

Executes code only when the condition is true.

## Syntax

```csharp
if (condition)
{
    // code
}
```

## Example

```csharp
int age = 20;

if (age >= 18)
{
    Console.WriteLine("You are eligible to vote.");
}
```

### Output

```text
You are eligible to vote.
```

---

# 2. if-else Statement ⭐⭐⭐⭐⭐

Executes one block if condition is true, otherwise another block.

## Syntax

```csharp
if (condition)
{
}
else
{
}
```

## Example

```csharp
int age = 15;

if (age >= 18)
{
    Console.WriteLine("Adult");
}
else
{
    Console.WriteLine("Minor");
}
```

### Output

```text
Minor
```

---

# 3. else-if Ladder ⭐⭐⭐⭐⭐

Used when multiple conditions need to be checked.

## Example

```csharp
int marks = 85;

if (marks >= 90)
{
    Console.WriteLine("Grade A+");
}
else if (marks >= 80)
{
    Console.WriteLine("Grade A");
}
else if (marks >= 70)
{
    Console.WriteLine("Grade B");
}
else
{
    Console.WriteLine("Fail");
}
```

### Output

```text
Grade A
```

---

# 4. Nested if ⭐⭐⭐⭐⭐

An if statement inside another if statement.

## Example

```csharp
int age = 22;
bool hasLicense = true;

if (age >= 18)
{
    if (hasLicense)
    {
        Console.WriteLine("Can Drive");
    }
}
```

### Output

```text
Can Drive
```

---

# 5. Logical Operators in Conditions ⭐⭐⭐⭐

Used to combine multiple conditions.

## AND (`&&`)

Both conditions must be true.

```csharp
int age = 25;
bool hasLicense = true;

if (age >= 18 && hasLicense)
{
    Console.WriteLine("Can Drive");
}
```

### Output

```text
Can Drive
```

---

## OR (`||`)

At least one condition must be true.

```csharp
bool hasPassport = false;
bool hasNationalId = true;

if (hasPassport || hasNationalId)
{
    Console.WriteLine("Identity Verified");
}
```

---

## NOT (`!`)

Reverses a boolean value.

```csharp
bool isBlocked = false;

if (!isBlocked)
{
    Console.WriteLine("Access Granted");
}
```

---

# 6. Ternary Operator ⭐⭐⭐⭐⭐

Short form of if-else.

## Syntax

```csharp
condition ? trueValue : falseValue;
```

## Example

```csharp
int age = 20;

string result = age >= 18
                ? "Adult"
                : "Minor";

Console.WriteLine(result);
```

### Output

```text
Adult
```

---

# 7. Multiple Ternary Conditions ⭐⭐⭐⭐

```csharp
int marks = 85;

string grade =
    marks >= 90 ? "A+" :
    marks >= 80 ? "A" :
    marks >= 70 ? "B" :
    "Fail";

Console.WriteLine(grade);
```

### Output

```text
A
```

---

# 8. switch Statement ⭐⭐⭐⭐⭐

Alternative to multiple else-if conditions.

## Syntax

```csharp
switch(variable)
{
    case value:
        break;

    default:
        break;
}
```

## Example

```csharp
int day = 3;

switch (day)
{
    case 1:
        Console.WriteLine("Sunday");
        break;

    case 2:
        Console.WriteLine("Monday");
        break;

    case 3:
        Console.WriteLine("Tuesday");
        break;

    default:
        Console.WriteLine("Invalid");
        break;
}
```

### Output

```text
Tuesday
```

---

# 9. switch with Multiple Cases ⭐⭐⭐⭐

```csharp
char grade = 'A';

switch (grade)
{
    case 'A':
    case 'B':
        Console.WriteLine("Passed");
        break;

    default:
        Console.WriteLine("Failed");
        break;
}
```

---

# 10. Modern switch Expression ⭐⭐⭐⭐

Introduced in modern C#.

## Example

```csharp
int day = 1;

string dayName = day switch
{
    1 => "Sunday",
    2 => "Monday",
    3 => "Tuesday",
    _ => "Invalid Day"
};

Console.WriteLine(dayName);
```

### Output

```text
Sunday
```

---

# 11. Pattern Matching with switch ⭐⭐⭐⭐

```csharp
object value = 100;

string result = value switch
{
    int => "Integer",
    string => "String",
    bool => "Boolean",
    _ => "Unknown"
};

Console.WriteLine(result);
```

### Output

```text
Integer
```

---

# 12. Relational Pattern Matching ⭐⭐⭐

```csharp
int age = 25;

string category = age switch
{
    < 13 => "Child",
    < 18 => "Teenager",
    < 60 => "Adult",
    _ => "Senior"
};

Console.WriteLine(category);
```

### Output

```text
Adult
```

---

# 13. Null Conditional Check ⭐⭐⭐

Safe null checking.

```csharp
Student? student = null;

if (student?.Name != null)
{
    Console.WriteLine(student.Name);
}
```

No exception occurs.

---

# 14. Null Coalescing in Conditions ⭐⭐⭐

```csharp
string? name = null;

string result = name ?? "Guest";

Console.WriteLine(result);
```

### Output

```text
Guest
```

---

# 15. Advanced Pattern Matching ⭐⭐

```csharp
object person = new
{
    Name = "Sabbir",
    Age = 25
};

if (person is { Age: > 18 })
{
    Console.WriteLine("Adult");
}
```

---

# Real-World Example: Login System ⭐⭐⭐⭐⭐

```csharp
string username = "admin";
string password = "1234";

if (username == "admin" &&
    password == "1234")
{
    Console.WriteLine("Login Successful");
}
else
{
    Console.WriteLine("Invalid Credentials");
}
```

---

# Real-World Example: ATM Withdrawal ⭐⭐⭐⭐⭐

```csharp
decimal balance = 5000;
decimal withdraw = 2000;

if (withdraw <= balance)
{
    balance -= withdraw;

    Console.WriteLine("Withdrawal Successful");
    Console.WriteLine($"Balance: {balance}");
}
else
{
    Console.WriteLine("Insufficient Balance");
}
```

---

# Common Mistakes

## Mistake 1

Using assignment instead of comparison.

❌ Wrong

```csharp
if (age = 18)
{
}
```

✅ Correct

```csharp
if (age == 18)
{
}
```

---

## Mistake 2

Missing braces.

❌

```csharp
if (age > 18)
    Console.WriteLine("Adult");
    Console.WriteLine("Welcome");
```

Only first line belongs to if.

---

# 🎯 Most Asked Interview Questions

## Q1. What is a Conditional Statement?

A Conditional Statement allows execution of different code blocks based on conditions.

---

## Q2. Difference Between if and switch?

| if | switch |
|------|------|
| Can evaluate complex conditions | Best for fixed values |
| More flexible | Cleaner for many options |
| Slightly slower | Often faster |

---

## Q3. Difference Between if-else and Ternary Operator?

### if-else

```csharp
if(age >= 18)
{
    result = "Adult";
}
else
{
    result = "Minor";
}
```

### Ternary

```csharp
result = age >= 18
        ? "Adult"
        : "Minor";
```

---

## Q4. When Should You Use switch?

Use switch when checking one variable against many fixed values.

Example:

```csharp
switch(day)
{
}
```

---

## Q5. What is Nested if?

An if statement inside another if statement.

```csharp
if(condition1)
{
    if(condition2)
    {
    }
}
```

---

## Q6. What are Logical Operators?

| Operator | Meaning |
|----------|----------|
| `&&` | AND |
| `||` | OR |
| `!` | NOT |

---

## Q7. What is Pattern Matching?

A modern C# feature for checking type and values in a cleaner way.

```csharp
if(obj is string text)
{
}
```

---

## Q8. Difference Between switch Statement and switch Expression?

### switch Statement

```csharp
switch(day)
{
}
```

Multiple lines and uses `break`.

### switch Expression

```csharp
day switch
{
}
```

Returns a value directly.

---

## Q9. Which Conditional Statements Are Most Used in Real Projects?

1. if
2. if-else
3. else-if ladder
4. switch
5. Ternary Operator
6. Pattern Matching

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] if Statement
- [x] if-else Statement
- [x] else-if Ladder
- [x] Nested if
- [x] Logical Operators (`&&`, `||`, `!`)
- [x] Ternary Operator (`?:`)
- [x] switch Statement
- [x] switch Expression
- [x] Pattern Matching
- [x] Null Checking

Mastering these topics will help you answer **90%+ of Conditional Statement questions in .NET interviews**.