# C# Arrays (Easy to Advanced)

## 📌 What is an Array?

An Array is a collection of elements of the same data type stored in contiguous memory locations.

Instead of creating multiple variables:

```csharp
string student1 = "Sabbir";
string student2 = "Rahim";
string student3 = "Karim";
```

We can use an Array:

```csharp
string[] students =
{
    "Sabbir",
    "Rahim",
    "Karim"
};
```

---

# 🎯 Recommended Learning Order (Most Important First)

If you're preparing for a **.NET Full Stack Developer Interview**, follow this order:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | Array Declaration & Initialization | Very High |
| ⭐⭐⭐⭐⭐ | Accessing Array Elements | Very High |
| ⭐⭐⭐⭐⭐ | Array Length Property | Very High |
| ⭐⭐⭐⭐⭐ | Looping Through Arrays | Very High |
| ⭐⭐⭐⭐⭐ | foreach with Arrays | Very High |
| ⭐⭐⭐⭐⭐ | Single-Dimensional Arrays | Very High |
| ⭐⭐⭐⭐ | Multi-Dimensional Arrays | High |
| ⭐⭐⭐⭐ | Jagged Arrays | High |
| ⭐⭐⭐⭐ | Array Methods (`Sort`, `Reverse`, etc.) | High |
| ⭐⭐⭐⭐ | Searching Arrays | High |
| ⭐⭐⭐⭐ | Copying Arrays | High |
| ⭐⭐⭐ | Array Indexing | Medium |
| ⭐⭐⭐ | Range & Index Operators | Medium |
| ⭐⭐ | Array Class Methods | Low |
| ⭐⭐ | Memory Concepts | Low |

---

# 1. Array Declaration ⭐⭐⭐⭐⭐

## Syntax

```csharp
datatype[] arrayName;
```

### Example

```csharp
int[] numbers;
string[] names;
```

---

# 2. Array Initialization ⭐⭐⭐⭐⭐

### Method 1

```csharp
int[] numbers = new int[5];
```

Creates 5 elements.

Default values:

```text
0 0 0 0 0
```

---

### Method 2

```csharp
int[] numbers =
{
    10,
    20,
    30,
    40,
    50
};
```

---

### Method 3

```csharp
int[] numbers = new int[]
{
    10,
    20,
    30
};
```

---

# 3. Accessing Array Elements ⭐⭐⭐⭐⭐

Arrays use zero-based indexing.

```csharp
int[] numbers =
{
    10,
    20,
    30
};

Console.WriteLine(numbers[0]);
Console.WriteLine(numbers[1]);
Console.WriteLine(numbers[2]);
```

Output:

```text
10
20
30
```

---

# 4. Updating Array Elements ⭐⭐⭐⭐⭐

```csharp
int[] numbers =
{
    10,
    20,
    30
};

numbers[1] = 100;

Console.WriteLine(numbers[1]);
```

Output:

```text
100
```

---

# 5. Array Length Property ⭐⭐⭐⭐⭐

Returns total elements.

```csharp
int[] numbers =
{
    10,
    20,
    30,
    40
};

Console.WriteLine(numbers.Length);
```

Output:

```text
4
```

---

# 6. Looping Through Arrays ⭐⭐⭐⭐⭐

Using `for`.

```csharp
int[] numbers =
{
    10,
    20,
    30,
    40
};

for(int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine(numbers[i]);
}
```

---

# 7. foreach with Arrays ⭐⭐⭐⭐⭐

Most common approach.

```csharp
int[] numbers =
{
    10,
    20,
    30
};

foreach(int number in numbers)
{
    Console.WriteLine(number);
}
```

---

# 8. Array Index Out Of Range ⭐⭐⭐⭐

### Wrong

```csharp
int[] numbers =
{
    10,
    20,
    30
};

Console.WriteLine(numbers[5]);
```

Output:

```text
IndexOutOfRangeException
```

---

# 9. Finding Maximum Value ⭐⭐⭐⭐

```csharp
int[] numbers =
{
    10,
    50,
    20,
    90,
    30
};

int max = numbers[0];

foreach(int number in numbers)
{
    if(number > max)
    {
        max = number;
    }
}

Console.WriteLine(max);
```

Output:

```text
90
```

---

# 10. Finding Minimum Value ⭐⭐⭐⭐

```csharp
int[] numbers =
{
    10,
    50,
    20,
    90,
    30
};

int min = numbers[0];

foreach(int number in numbers)
{
    if(number < min)
    {
        min = number;
    }
}

Console.WriteLine(min);
```

Output:

```text
10
```

---

# 11. Sum of Array Elements ⭐⭐⭐⭐⭐

```csharp
int[] numbers =
{
    10,
    20,
    30
};

int sum = 0;

foreach(int number in numbers)
{
    sum += number;
}

Console.WriteLine(sum);
```

Output:

```text
60
```

---

# 12. Average of Array Elements ⭐⭐⭐⭐

```csharp
int[] numbers =
{
    10,
    20,
    30
};

int sum = 0;

foreach(int number in numbers)
{
    sum += number;
}

double average = (double)sum / numbers.Length;

Console.WriteLine(average);
```

Output:

```text
20
```

---

# 13. Searching in Array ⭐⭐⭐⭐

## Linear Search

```csharp
int[] numbers =
{
    10,
    20,
    30,
    40
};

int search = 30;

bool found = false;

foreach(int number in numbers)
{
    if(number == search)
    {
        found = true;
        break;
    }
}

Console.WriteLine(found);
```

Output:

```text
True
```

---

# 14. Array.Sort() ⭐⭐⭐⭐

```csharp
int[] numbers =
{
    50,
    20,
    90,
    10
};

Array.Sort(numbers);

foreach(int number in numbers)
{
    Console.WriteLine(number);
}
```

Output:

```text
10
20
50
90
```

---

# 15. Array.Reverse() ⭐⭐⭐⭐

```csharp
int[] numbers =
{
    10,
    20,
    30
};

Array.Reverse(numbers);

foreach(int number in numbers)
{
    Console.WriteLine(number);
}
```

Output:

```text
30
20
10
```

---

# 16. Array.IndexOf() ⭐⭐⭐⭐

```csharp
int[] numbers =
{
    10,
    20,
    30
};

int index =
    Array.IndexOf(numbers, 20);

Console.WriteLine(index);
```

Output:

```text
1
```

---

# 17. Array.Exists() ⭐⭐⭐

```csharp
int[] numbers =
{
    10,
    20,
    30
};

bool exists =
    Array.Exists(numbers,
    n => n == 20);

Console.WriteLine(exists);
```

Output:

```text
True
```

---

# 18. Copying Arrays ⭐⭐⭐⭐

```csharp
int[] source =
{
    10,
    20,
    30
};

int[] destination =
    new int[source.Length];

Array.Copy(
    source,
    destination,
    source.Length);
```

---

# 19. Multi-Dimensional Array ⭐⭐⭐⭐

Table-like structure.

```csharp
int[,] matrix =
{
    {1,2,3},
    {4,5,6}
};
```

Access:

```csharp
Console.WriteLine(matrix[1,2]);
```

Output:

```text
6
```

---

# 20. Loop Through Multi-Dimensional Array ⭐⭐⭐⭐

```csharp
int[,] matrix =
{
    {1,2},
    {3,4}
};

for(int row = 0; row < 2; row++)
{
    for(int col = 0; col < 2; col++)
    {
        Console.WriteLine(
            matrix[row,col]);
    }
}
```

---

# 21. Jagged Array ⭐⭐⭐⭐

Array of arrays.

```csharp
int[][] numbers =
{
    new int[] {1,2},
    new int[] {3,4,5},
    new int[] {6}
};
```

Access:

```csharp
Console.WriteLine(numbers[1][2]);
```

Output:

```text
5
```

---

# 22. Range Operator (`..`) ⭐⭐⭐

Modern C#.

```csharp
int[] numbers =
{
    10,
    20,
    30,
    40,
    50
};

var result = numbers[1..4];

foreach(var item in result)
{
    Console.WriteLine(item);
}
```

Output:

```text
20
30
40
```

---

# 23. Index From End (`^`) ⭐⭐⭐

```csharp
int[] numbers =
{
    10,
    20,
    30,
    40
};

Console.WriteLine(numbers[^1]);
```

Output:

```text
40
```

---

# Real-World Example: Student Marks ⭐⭐⭐⭐⭐

```csharp
int[] marks =
{
    80,
    90,
    70,
    85
};

int total = 0;

foreach(int mark in marks)
{
    total += mark;
}

double average =
    (double)total / marks.Length;

Console.WriteLine(
    $"Average = {average}");
```

---

# Real-World Example: Product Prices ⭐⭐⭐⭐⭐

```csharp
decimal[] prices =
{
    100,
    200,
    300
};

decimal total = 0;

foreach(decimal price in prices)
{
    total += price;
}

Console.WriteLine(total);
```

---

# Common Mistakes

## Wrong Index

```csharp
numbers[5];
```

Array has only 3 elements.

Results:

```text
IndexOutOfRangeException
```

---

## Wrong Loop Condition

❌ Wrong

```csharp
for(int i=0;
    i<=numbers.Length;
    i++)
{
}
```

Should be:

```csharp
for(int i=0;
    i<numbers.Length;
    i++)
{
}
```

---

# 🎯 Most Asked Interview Questions

## Q1. What is an Array?

An Array is a collection of same-type elements stored in contiguous memory.

---

## Q2. Why Does Array Start at Index 0?

Because memory addressing starts from offset 0.

---

## Q3. Difference Between Array and List?

| Array | List |
|---------|---------|
| Fixed Size | Dynamic Size |
| Faster | Flexible |
| Less Memory Overhead | More Features |

---

## Q4. Difference Between Multi-Dimensional and Jagged Array?

### Multi-Dimensional

```csharp
int[,] matrix;
```

Fixed rows and columns.

---

### Jagged Array

```csharp
int[][]
```

Rows can have different lengths.

---

## Q5. Difference Between for and foreach?

### for

```csharp
for(...)
{
}
```

Has index access.

---

### foreach

```csharp
foreach(...)
{
}
```

Simpler and safer.

---

## Q6. How Do You Find Maximum Value in an Array?

Loop through array and compare values.

---

## Q7. How Do You Sort an Array?

```csharp
Array.Sort(numbers);
```

---

## Q8. What Exception Occurs for Invalid Index?

```text
IndexOutOfRangeException
```

---

## Q9. Can Array Size Be Changed?

No.

Arrays are fixed-size after creation.

---

## Q10. Which Is Better for Dynamic Data?

```text
List<T>
```

Because size can grow or shrink.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Array Declaration
- [x] Array Initialization
- [x] Array Indexing
- [x] Length Property
- [x] for Loop with Arrays
- [x] foreach Loop with Arrays
- [x] Sum / Average
- [x] Max / Min
- [x] Search Operations
- [x] Array.Sort()
- [x] Array.Reverse()
- [x] Multi-Dimensional Arrays
- [x] Jagged Arrays
- [x] Array vs List
- [x] IndexOutOfRangeException

Mastering these topics will help you answer **95%+ of Array-related C#/.NET interview questions**.