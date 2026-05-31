# C# Loops (Easy to Advanced)

## 📌 What are Loops?

Loops are used to execute a block of code repeatedly until a condition becomes false.

Without Loop:

```csharp
Console.WriteLine("Hello");
Console.WriteLine("Hello");
Console.WriteLine("Hello");
Console.WriteLine("Hello");
Console.WriteLine("Hello");
```

With Loop:

```csharp
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine("Hello");
}
```

---

# 🎯 Recommended Learning Order (Most Important First)

If you're preparing for a **.NET Full Stack Developer Interview**, follow this order:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | for Loop | Very High |
| ⭐⭐⭐⭐⭐ | foreach Loop | Very High |
| ⭐⭐⭐⭐⭐ | while Loop | Very High |
| ⭐⭐⭐⭐⭐ | break Statement | Very High |
| ⭐⭐⭐⭐⭐ | continue Statement | Very High |
| ⭐⭐⭐⭐ | do-while Loop | High |
| ⭐⭐⭐⭐ | Nested Loops | High |
| ⭐⭐⭐⭐ | Looping Arrays & Collections | High |
| ⭐⭐⭐⭐ | Loop Performance Concepts | High |
| ⭐⭐⭐ | Infinite Loops | Medium |
| ⭐⭐⭐ | Loop Variables Scope | Medium |
| ⭐⭐⭐ | Pattern Printing | Medium |
| ⭐⭐ | goto Statement | Low |
| ⭐⭐ | Parallel Loop Concepts | Low |

---

# 1. for Loop ⭐⭐⭐⭐⭐

Most commonly used loop.

## Syntax

```csharp
for (initialization; condition; increment/decrement)
{
    // code
}
```

## Example

```csharp
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine(i);
}
```

### Output

```text
1
2
3
4
5
```

---

# How for Loop Works

```csharp
for (int i = 1; i <= 3; i++)
{
    Console.WriteLine(i);
}
```

### Step-by-Step

```text
i = 1 → print 1
i = 2 → print 2
i = 3 → print 3
i = 4 → condition false → stop
```

---

# 2. Reverse for Loop ⭐⭐⭐⭐

```csharp
for (int i = 5; i >= 1; i--)
{
    Console.WriteLine(i);
}
```

### Output

```text
5
4
3
2
1
```

---

# 3. while Loop ⭐⭐⭐⭐⭐

Used when the number of iterations is unknown.

## Syntax

```csharp
while(condition)
{
    // code
}
```

## Example

```csharp
int i = 1;

while (i <= 5)
{
    Console.WriteLine(i);
    i++;
}
```

### Output

```text
1
2
3
4
5
```

---

# 4. do-while Loop ⭐⭐⭐⭐

Executes at least one time.

## Syntax

```csharp
do
{
}
while(condition);
```

## Example

```csharp
int i = 1;

do
{
    Console.WriteLine(i);
    i++;
}
while (i <= 5);
```

### Output

```text
1
2
3
4
5
```

---

# Difference Between while and do-while

### while

Checks condition first.

```csharp
int i = 10;

while (i < 5)
{
    Console.WriteLine(i);
}
```

Output:

```text
Nothing
```

---

### do-while

Runs once before checking.

```csharp
int i = 10;

do
{
    Console.WriteLine(i);
}
while (i < 5);
```

Output:

```text
10
```

---

# 5. foreach Loop ⭐⭐⭐⭐⭐

Used for arrays and collections.

## Example

```csharp
string[] names =
{
    "Sabbir",
    "Rahim",
    "Karim"
};

foreach (string name in names)
{
    Console.WriteLine(name);
}
```

### Output

```text
Sabbir
Rahim
Karim
```

---

# Why foreach is Preferred?

- Cleaner syntax
- No index management
- Less chance of errors
- Read-only iteration

---

# 6. break Statement ⭐⭐⭐⭐⭐

Terminates a loop immediately.

## Example

```csharp
for (int i = 1; i <= 10; i++)
{
    if (i == 5)
    {
        break;
    }

    Console.WriteLine(i);
}
```

### Output

```text
1
2
3
4
```

---

# 7. continue Statement ⭐⭐⭐⭐⭐

Skips current iteration.

## Example

```csharp
for (int i = 1; i <= 5; i++)
{
    if (i == 3)
    {
        continue;
    }

    Console.WriteLine(i);
}
```

### Output

```text
1
2
4
5
```

---

# 8. Infinite Loop ⭐⭐⭐

A loop that never ends.

## Example

```csharp
while(true)
{
    Console.WriteLine("Running...");
}
```

### Use Cases

- Background Services
- Game Loops
- Server Monitoring

Always include an exit condition.

---

# 9. Nested Loops ⭐⭐⭐⭐

Loop inside another loop.

## Example

```csharp
for (int row = 1; row <= 3; row++)
{
    for (int col = 1; col <= 3; col++)
    {
        Console.Write(col + " ");
    }

    Console.WriteLine();
}
```

### Output

```text
1 2 3
1 2 3
1 2 3
```

---

# 10. Pattern Printing ⭐⭐⭐

Common Interview Question.

## Example

```csharp
for (int row = 1; row <= 5; row++)
{
    for (int col = 1; col <= row; col++)
    {
        Console.Write("*");
    }

    Console.WriteLine();
}
```

### Output

```text
*
**
***
****
*****
```

---

# 11. Looping Through Arrays ⭐⭐⭐⭐

```csharp
int[] numbers =
{
    10,
    20,
    30,
    40
};

for (int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine(numbers[i]);
}
```

---

# Using foreach

```csharp
foreach (int number in numbers)
{
    Console.WriteLine(number);
}
```

---

# 12. Looping Through List<T> ⭐⭐⭐⭐

```csharp
List<string> students = new()
{
    "Sabbir",
    "Rahim",
    "Karim"
};

foreach (string student in students)
{
    Console.WriteLine(student);
}
```

---

# 13. Looping Through Dictionary ⭐⭐⭐

```csharp
Dictionary<int, string> students =
    new Dictionary<int, string>()
{
    {1,"Sabbir"},
    {2,"Rahim"}
};

foreach (var item in students)
{
    Console.WriteLine($"{item.Key} - {item.Value}");
}
```

---

# 14. Loop Variable Scope ⭐⭐⭐

```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(i);
}
```

This is invalid:

```csharp
Console.WriteLine(i);
```

Because `i` exists only inside the loop.

---

# 15. Multiple Variables in for Loop ⭐⭐⭐

```csharp
for (int i = 0, j = 10; i < 5; i++, j--)
{
    Console.WriteLine($"{i} {j}");
}
```

---

# 16. foreach vs for ⭐⭐⭐⭐

## for

```csharp
for (int i = 0; i < numbers.Length; i++)
{
}
```

### Advantages

- Access index
- Modify elements
- More control

---

## foreach

```csharp
foreach(var item in numbers)
{
}
```

### Advantages

- Cleaner
- Safer
- Easier to read

---

# 17. Performance Considerations ⭐⭐⭐⭐

### Fastest

```csharp
for
```

### Readability

```csharp
foreach
```

### Unknown Iterations

```csharp
while
```

### Must Execute Once

```csharp
do-while
```

---

# 18. goto Statement (Rare) ⭐⭐

```csharp
int i = 0;

start:

Console.WriteLine(i);

i++;

if(i < 5)
{
    goto start;
}
```

Not recommended.

---

# Real-World Example: Sum of Numbers ⭐⭐⭐⭐⭐

```csharp
int sum = 0;

for (int i = 1; i <= 10; i++)
{
    sum += i;
}

Console.WriteLine(sum);
```

Output:

```text
55
```

---

# Real-World Example: User Login Attempts ⭐⭐⭐⭐⭐

```csharp
int attempts = 0;

while (attempts < 3)
{
    Console.WriteLine("Enter Password");

    attempts++;
}
```

---

# Real-World Example: Process Orders ⭐⭐⭐⭐⭐

```csharp
List<string> orders = new()
{
    "Order1",
    "Order2",
    "Order3"
};

foreach (string order in orders)
{
    Console.WriteLine($"Processing {order}");
}
```

---

# Common Mistakes

## Forgetting Increment

❌ Wrong

```csharp
int i = 1;

while(i <= 5)
{
    Console.WriteLine(i);
}
```

Infinite Loop.

---

## Wrong Condition

❌ Wrong

```csharp
for(int i = 1; i >= 5; i++)
{
}
```

Loop never executes.

---

# 🎯 Most Asked Interview Questions

## Q1. What is a Loop?

A loop repeatedly executes a block of code until a condition becomes false.

---

## Q2. Difference Between for and while?

| for | while |
|------|------|
| Known iterations | Unknown iterations |
| Compact syntax | More flexible |
| Most common | Condition-driven |

---

## Q3. Difference Between while and do-while?

| while | do-while |
|---------|---------|
| Condition checked first | Executes once before checking |
| May run zero times | Runs at least once |

---

## Q4. Difference Between for and foreach?

| for | foreach |
|------|------|
| Has index | No index |
| Can modify collection elements | Read-only iteration |
| Faster in some scenarios | Cleaner syntax |

---

## Q5. What does break do?

Stops the loop immediately.

```csharp
break;
```

---

## Q6. What does continue do?

Skips current iteration.

```csharp
continue;
```

---

## Q7. What is an Infinite Loop?

A loop that never ends.

```csharp
while(true)
{
}
```

---

## Q8. What is a Nested Loop?

A loop inside another loop.

```csharp
for(...)
{
    for(...)
    {
    }
}
```

---

## Q9. Which Loop is Most Used in Real Projects?

1. foreach
2. for
3. while
4. do-while

---

## Q10. Which Loop Should I Use?

| Situation | Loop |
|------------|---------|
| Fixed number of iterations | `for` |
| Collection traversal | `foreach` |
| Unknown iterations | `while` |
| Execute at least once | `do-while` |

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] for Loop
- [x] foreach Loop
- [x] while Loop
- [x] do-while Loop
- [x] break Statement
- [x] continue Statement
- [x] Nested Loops
- [x] Looping Arrays
- [x] Looping Collections
- [x] Infinite Loops
- [x] for vs foreach
- [x] while vs do-while

Mastering these topics will help you answer **95%+ of Loop-related C#/.NET interview questions**.