# C# LINQ (Language Integrated Query) — Easy to Advanced

## 📌 What is LINQ?

LINQ (Language Integrated Query) is a feature in C# that allows querying and manipulating data using C# syntax.

LINQ works with:

- Collections
- Arrays
- Lists
- Databases
- XML
- Entity Framework
- APIs

---

# Why LINQ is Important?

LINQ is one of the **MOST IMPORTANT topics** for:

- ASP.NET Core
- Entity Framework
- APIs
- Database Queries
- Real-world Business Logic
- Interview Questions

---

# 🎯 Recommended Learning Order (Most Important First)

For .NET Full Stack Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | What is LINQ? | Very High |
| ⭐⭐⭐⭐⭐ | Method Syntax | Very High |
| ⭐⭐⭐⭐⭐ | Query Syntax | Very High |
| ⭐⭐⭐⭐⭐ | Where() | Very High |
| ⭐⭐⭐⭐⭐ | Select() | Very High |
| ⭐⭐⭐⭐⭐ | OrderBy() | Very High |
| ⭐⭐⭐⭐⭐ | FirstOrDefault() | Very High |
| ⭐⭐⭐⭐⭐ | SingleOrDefault() | Very High |
| ⭐⭐⭐⭐⭐ | Any() | Very High |
| ⭐⭐⭐⭐⭐ | Count() | Very High |
| ⭐⭐⭐⭐⭐ | LINQ with Lambda | Very High |
| ⭐⭐⭐⭐⭐ | LINQ with List<T> | Very High |
| ⭐⭐⭐⭐ | GroupBy() | High |
| ⭐⭐⭐⭐ | Join() | High |
| ⭐⭐⭐⭐ | Distinct() | High |
| ⭐⭐⭐⭐ | Skip() & Take() | High |
| ⭐⭐⭐⭐ | Aggregate Functions | High |
| ⭐⭐⭐⭐ | Deferred Execution | High |
| ⭐⭐⭐⭐ | Projection | High |
| ⭐⭐⭐ | SelectMany() | Medium |
| ⭐⭐⭐ | ToDictionary() | Medium |
| ⭐⭐⭐ | Union / Intersect / Except | Medium |
| ⭐⭐⭐ | AsEnumerable() | Medium |
| ⭐⭐ | IQueryable vs IEnumerable | Low |
| ⭐⭐ | Expression Trees | Low |

---

# Sample Data Used in Examples

```csharp
List<int> numbers = new()
{
    1,2,3,4,5,6,7,8,9,10
};

List<Student> students = new()
{
    new Student
    {
        Id = 1,
        Name = "Sabbir",
        Age = 25,
        Department = "CSE",
        Salary = 50000
    },

    new Student
    {
        Id = 2,
        Name = "Rahim",
        Age = 22,
        Department = "EEE",
        Salary = 30000
    },

    new Student
    {
        Id = 3,
        Name = "Karim",
        Age = 27,
        Department = "CSE",
        Salary = 70000
    }
};
```

---

# 1. LINQ Method Syntax ⭐⭐⭐⭐⭐

Most commonly used syntax.

```csharp
var result =
    numbers.Where(x => x > 5);
```

---

# 2. LINQ Query Syntax ⭐⭐⭐⭐⭐

SQL-like syntax.

```csharp
var result =
    from number in numbers
    where number > 5
    select number;
```

---

# Method Syntax vs Query Syntax

| Method Syntax | Query Syntax |
|---------------|-------------|
| Most Common | SQL-Like |
| Flexible | Easier for Beginners |
| Uses Lambda | Uses Query Keywords |

---

# 3. Where() ⭐⭐⭐⭐⭐

Filters data.

```csharp
var result =
    numbers.Where(x => x > 5);

foreach(var item in result)
{
    Console.WriteLine(item);
}
```

Output:

```text
6
7
8
9
10
```

---

# 4. Select() ⭐⭐⭐⭐⭐

Projects/transforms data.

```csharp
var result =
    students.Select(x => x.Name);

foreach(var item in result)
{
    Console.WriteLine(item);
}
```

Output:

```text
Sabbir
Rahim
Karim
```

---

# 5. OrderBy() ⭐⭐⭐⭐⭐

Ascending order.

```csharp
var result =
    students.OrderBy(x => x.Salary);

foreach(var item in result)
{
    Console.WriteLine(item.Name);
}
```

---

# 6. OrderByDescending() ⭐⭐⭐⭐

Descending order.

```csharp
var result =
    students.OrderByDescending(
        x => x.Salary);
```

---

# 7. First() ⭐⭐⭐⭐

Returns first item.

```csharp
var student =
    students.First();

Console.WriteLine(student.Name);
```

---

# 8. FirstOrDefault() ⭐⭐⭐⭐⭐

Returns first item or default value.

```csharp
var student =
    students.FirstOrDefault(
        x => x.Id == 10);
```

Result:

```text
null
```

Safer than `First()`.

---

# First vs FirstOrDefault

| First | FirstOrDefault |
|-------|----------------|
| Exception if empty | Returns default |
| Less Safe | Safer |

---

# 9. Single() ⭐⭐⭐⭐

Returns exactly one item.

```csharp
var student =
    students.Single(x => x.Id == 1);
```

---

# 10. SingleOrDefault() ⭐⭐⭐⭐⭐

Returns one item or default.

```csharp
var student =
    students.SingleOrDefault(
        x => x.Id == 10);
```

---

# Single vs First

| Single | First |
|--------|--------|
| Exactly One | First Match |
| Throws if multiple | No Issue |

---

# 11. Any() ⭐⭐⭐⭐⭐

Checks existence.

```csharp
bool exists =
    students.Any(x => x.Age > 25);

Console.WriteLine(exists);
```

Output:

```text
True
```

---

# 12. All() ⭐⭐⭐⭐

Checks whether all items satisfy condition.

```csharp
bool result =
    numbers.All(x => x > 0);

Console.WriteLine(result);
```

Output:

```text
True
```

---

# 13. Count() ⭐⭐⭐⭐⭐

Counts items.

```csharp
int total =
    students.Count();

Console.WriteLine(total);
```

Output:

```text
3
```

---

# Conditional Count

```csharp
int total =
    students.Count(
        x => x.Department == "CSE");
```

---

# 14. Sum() ⭐⭐⭐⭐

```csharp
int total =
    numbers.Sum();

Console.WriteLine(total);
```

---

# 15. Average() ⭐⭐⭐⭐

```csharp
double average =
    numbers.Average();

Console.WriteLine(average);
```

---

# 16. Max() ⭐⭐⭐⭐

```csharp
int max =
    numbers.Max();

Console.WriteLine(max);
```

---

# 17. Min() ⭐⭐⭐⭐

```csharp
int min =
    numbers.Min();

Console.WriteLine(min);
```

---

# 18. Distinct() ⭐⭐⭐⭐

Removes duplicates.

```csharp
List<int> numbers =
    new() {1,1,2,2,3,3};

var result =
    numbers.Distinct();

foreach(var item in result)
{
    Console.WriteLine(item);
}
```

Output:

```text
1
2
3
```

---

# 19. Skip() ⭐⭐⭐⭐

Skips elements.

```csharp
var result =
    numbers.Skip(5);
```

---

# 20. Take() ⭐⭐⭐⭐

Takes elements.

```csharp
var result =
    numbers.Take(3);
```

---

# Paging Example ⭐⭐⭐⭐⭐

```csharp
var page =
    students
    .Skip(10)
    .Take(10);
```

Very common in APIs.

---

# 21. GroupBy() ⭐⭐⭐⭐

Groups data.

```csharp
var result =
    students.GroupBy(
        x => x.Department);

foreach(var group in result)
{
    Console.WriteLine(group.Key);

    foreach(var item in group)
    {
        Console.WriteLine(item.Name);
    }
}
```

---

# 22. Join() ⭐⭐⭐⭐

Combines two collections.

```csharp
var result =
    students.Join(
        departments,
        student => student.DepartmentId,
        department => department.Id,
        (student, department) =>
            new
            {
                student.Name,
                department.DepartmentName
            });
```

---

# 23. SelectMany() ⭐⭐⭐

Flattens collections.

```csharp
var result =
    students.SelectMany(
        x => x.Subjects);
```

---

# 24. Contains() ⭐⭐⭐⭐

```csharp
bool exists =
    numbers.Contains(5);
```

---

# 25. ToList() ⭐⭐⭐⭐⭐

Converts query result to List.

```csharp
var result =
    numbers
    .Where(x => x > 5)
    .ToList();
```

---

# 26. ToDictionary() ⭐⭐⭐

```csharp
var result =
    students.ToDictionary(
        x => x.Id,
        x => x.Name);
```

---

# 27. Union() ⭐⭐⭐

Combines unique values.

```csharp
var result =
    list1.Union(list2);
```

---

# 28. Intersect() ⭐⭐⭐

Common values.

```csharp
var result =
    list1.Intersect(list2);
```

---

# 29. Except() ⭐⭐⭐

Differences.

```csharp
var result =
    list1.Except(list2);
```

---

# 30. Deferred Execution ⭐⭐⭐⭐

LINQ executes only when iterated.

```csharp
var result =
    numbers.Where(x => x > 5);
```

No execution yet.

Execution occurs during:

```csharp
foreach
ToList()
Count()
```

---

# 31. Immediate Execution ⭐⭐⭐⭐

```csharp
var result =
    numbers
    .Where(x => x > 5)
    .ToList();
```

Runs immediately.

---

# 32. IEnumerable vs IQueryable ⭐⭐

| IEnumerable | IQueryable |
|-------------|------------|
| In-Memory | Database Query |
| Faster Small Data | Optimized DB Queries |
| LINQ to Objects | LINQ to SQL |

---

# 33. LINQ with Lambda ⭐⭐⭐⭐⭐

```csharp
var result =
    students
    .Where(x => x.Age > 24)
    .Select(x => x.Name);
```

---

# 34. Projection ⭐⭐⭐⭐

Selecting specific fields.

```csharp
var result =
    students.Select(x =>
        new
        {
            x.Name,
            x.Age
        });
```

---

# 35. Anonymous Objects ⭐⭐⭐⭐

```csharp
var result =
    students.Select(x =>
        new
        {
            x.Name,
            x.Salary
        });
```

---

# Real-World Example: Search API ⭐⭐⭐⭐⭐

```csharp
var result =
    students
    .Where(x =>
        x.Name.Contains("Sa"))
    .OrderBy(x => x.Name)
    .ToList();
```

---

# Real-World Example: Pagination ⭐⭐⭐⭐⭐

```csharp
int page = 2;
int pageSize = 10;

var result =
    students
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToList();
```

---

# Real-World Example: Dashboard Statistics ⭐⭐⭐⭐⭐

```csharp
int totalStudents =
    students.Count();

decimal totalSalary =
    students.Sum(x => x.Salary);

double averageSalary =
    students.Average(x => x.Salary);
```

---

# Common Mistakes

## Forgetting ToList()

❌ Wrong

```csharp
var result =
    students.Where(x => x.Age > 20);
```

May execute multiple times.

---

## Using Single Instead of First

❌ Dangerous

```csharp
students.Single(x => x.Department=="CSE");
```

Throws exception if multiple.

---

# 🎯 Most Asked Interview Questions

## Q1. What is LINQ?

LINQ is a feature that allows querying data using C# syntax.

---

## Q2. Difference Between Query Syntax and Method Syntax?

| Query Syntax | Method Syntax |
|--------------|--------------|
| SQL-Like | Lambda-Based |
| Less Common | Most Used |

---

## Q3. Difference Between First and FirstOrDefault?

| First | FirstOrDefault |
|-------|----------------|
| Exception if empty | Returns default |

---

## Q4. Difference Between Single and First?

| Single | First |
|--------|--------|
| Exactly one item | First match |

---

## Q5. Difference Between IEnumerable and IQueryable?

| IEnumerable | IQueryable |
|-------------|------------|
| Memory | Database |
| LINQ to Objects | LINQ to SQL |

---

## Q6. What is Deferred Execution?

LINQ query executes only when enumerated.

---

## Q7. What is Projection in LINQ?

Selecting specific fields.

```csharp
Select()
```

---

## Q8. Why Is LINQ Important in ASP.NET Core?

Used heavily in:

- Entity Framework
- APIs
- Database Queries
- Filtering
- Pagination

---

## Q9. Difference Between Any and Count?

| Any | Count |
|-----|------|
| Checks existence | Counts all |
| Faster | Slower |

---

## Q10. Most Important LINQ Methods?

1. Where
2. Select
3. OrderBy
4. FirstOrDefault
5. Any
6. Count
7. GroupBy
8. Join
9. Skip/Take

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] LINQ Basics
- [x] Method Syntax
- [x] Query Syntax
- [x] Where()
- [x] Select()
- [x] OrderBy()
- [x] FirstOrDefault()
- [x] SingleOrDefault()
- [x] Any()
- [x] Count()
- [x] Sum()
- [x] Average()
- [x] GroupBy()
- [x] Join()
- [x] Skip() & Take()
- [x] Distinct()
- [x] Deferred Execution
- [x] Projection
- [x] LINQ with Lambda

Mastering these topics will help you answer **95%+ of LINQ interview questions in .NET Full Stack Development.**