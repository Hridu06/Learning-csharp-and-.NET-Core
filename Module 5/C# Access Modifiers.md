# Access Modifiers ⭐⭐⭐⭐⭐

## What are Access Modifiers?

Access Modifiers control the visibility and accessibility of classes, methods, properties, and fields.

They help implement Encapsulation.

---

# Types of Access Modifiers

| Modifier | Accessible Within Same Class | Same Project | Derived Class | Other Projects |
|-----------|-----------|-----------|-----------|-----------|
| private | ✅ | ❌ | ❌ | ❌ |
| protected | ✅ | ❌ | ✅ | ❌ |
| internal | ✅ | ✅ | ✅ | ❌ |
| protected internal | ✅ | ✅ | ✅ | ✅ (Derived Classes) |
| private protected | ✅ | ✅ (Derived Only) | ✅ | ❌ |
| public | ✅ | ✅ | ✅ | ✅ |

---

# 1. private ⭐⭐⭐⭐⭐

Accessible only inside the same class.

```csharp
class Student
{
    private string name = "Sabbir";

    public void Show()
    {
        Console.WriteLine(name);
    }
}
```

❌ Invalid

```csharp
Student student = new();

Console.WriteLine(student.name);
```

---

# 2. public ⭐⭐⭐⭐⭐

Accessible from anywhere.

```csharp
class Student
{
    public string Name = "Sabbir";
}
```

Usage:

```csharp
Student student = new();

Console.WriteLine(student.Name);
```

Output:

```text
Sabbir
```

---

# 3. protected ⭐⭐⭐⭐⭐

Accessible only inside the class and derived classes.

```csharp
class Animal
{
    protected string Name = "Animal";
}

class Dog : Animal
{
    public void Show()
    {
        Console.WriteLine(Name);
    }
}
```

---

# 4. internal ⭐⭐⭐⭐

Accessible only within the same project (assembly).

```csharp
internal class Student
{
}
```

Used heavily in enterprise applications.

---

# 5. protected internal ⭐⭐⭐

Combination of:

```text
protected OR internal
```

```csharp
protected internal string Name;
```

Accessible:

- Same Assembly
- Derived Classes

---

# 6. private protected ⭐⭐⭐

Combination of:

```text
private AND protected
```

Accessible only:

- Same Assembly
- Derived Classes

```csharp
private protected string Name;
```

---

# Real World Example ⭐⭐⭐⭐⭐

```csharp
class BankAccount
{
    private decimal balance;

    public decimal Balance
    {
        get { return balance; }
    }

    public void Deposit(decimal amount)
    {
        balance += amount;
    }
}
```

Usage:

```csharp
BankAccount account = new();

account.Deposit(1000);

Console.WriteLine(account.Balance);
```

Output:

```text
1000
```

Notice:

```csharp
account.balance = 50000;
```

❌ Not allowed.

This is Encapsulation using Access Modifiers.

---

# Interview Questions

## Q1. What is an Access Modifier?

An Access Modifier controls where a member can be accessed from.

---

## Q2. What is the Default Access Modifier for a Class?

```csharp
internal
```

---

## Q3. What is the Default Access Modifier for Class Members?

```csharp
private
```

---

## Q4. Difference Between private and protected?

| private | protected |
|----------|----------|
| Same Class Only | Same Class + Child Class |
| More Restricted | Less Restricted |

---

## Q5. Difference Between public and internal?

| public | internal |
|----------|----------|
| Accessible Everywhere | Accessible Only Inside Assembly |

---

## Q6. Which Access Modifier Is Most Common in Enterprise Applications?

1. private
2. public
3. protected
4. internal

---

# Must Master

- [x] private
- [x] public
- [x] protected
- [x] internal
- [x] protected internal
- [x] private protected
- [x] Default Access Modifiers
- [x] Access Modifiers + Encapsulation

These questions appear in almost every .NET OOP interview.