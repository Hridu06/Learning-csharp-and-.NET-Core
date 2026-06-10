# C# Multithreading & Asynchronous Programming (Easy to Advanced)

## 📌 What is Multithreading?

Multithreading allows a program to run multiple threads simultaneously.

A Thread is the smallest unit of execution.

Example:

```text
Browser

→ Downloading File
→ Playing Video
→ Loading Page
```

All happen at the same time.

---

# 📌 What is Asynchronous Programming?

Asynchronous Programming allows tasks to run without blocking the main thread.

Example:

```text
User clicks button
↓
API request starts
↓
User can still use application
↓
Response comes later
```

---

# Difference Between Multithreading and Async Programming

| Multithreading | Async Programming |
|---------------|-------------------|
| Multiple Threads | Non-Blocking Tasks |
| CPU Parallelism | I/O Optimization |
| Uses Threads Directly | Uses async/await |
| Good for CPU Work | Good for I/O Work |

---

# Why These Topics Are Important?

Used heavily in:

- ASP.NET Core APIs
- Database Calls
- File Uploads
- Background Services
- Real-time Systems
- Gaming
- High Performance Apps

---

# 🎯 Recommended Learning Order (Most Important First)

For .NET Full Stack Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | Thread Basics | Very High |
| ⭐⭐⭐⭐⭐ | async & await | Very High |
| ⭐⭐⭐⭐⭐ | Task | Very High |
| ⭐⭐⭐⭐⭐ | Task<T> | Very High |
| ⭐⭐⭐⭐⭐ | Thread vs Task | Very High |
| ⭐⭐⭐⭐⭐ | Synchronous vs Asynchronous | Very High |
| ⭐⭐⭐⭐⭐ | await Keyword | Very High |
| ⭐⭐⭐⭐⭐ | Task.Run() | Very High |
| ⭐⭐⭐⭐⭐ | Deadlock Basics | Very High |
| ⭐⭐⭐⭐ | Parallel Programming | High |
| ⭐⭐⭐⭐ | ThreadPool | High |
| ⭐⭐⭐⭐ | CancellationToken | High |
| ⭐⭐⭐⭐ | Exception Handling in Tasks | High |
| ⭐⭐⭐⭐ | ConfigureAwait | High |
| ⭐⭐⭐⭐ | Task.WhenAll() | High |
| ⭐⭐⭐⭐ | Task.WhenAny() | High |
| ⭐⭐⭐ | lock Keyword | Medium |
| ⭐⭐⭐ | Race Condition | Medium |
| ⭐⭐⭐ | SemaphoreSlim | Medium |
| ⭐⭐⭐ | Concurrent Collections | Medium |
| ⭐⭐ | Monitor | Low |
| ⭐⭐ | Mutex | Low |
| ⭐⭐ | ReaderWriterLockSlim | Low |

---

# 1. What is a Thread? ⭐⭐⭐⭐⭐

A Thread is an independent path of execution.

Every application starts with:

```text
Main Thread
```

---

# Single Thread Example

```csharp
Console.WriteLine("Task 1");

Thread.Sleep(3000);

Console.WriteLine("Task 2");
```

Output:

```text
Task 1
(wait 3 sec)
Task 2
```

Application blocks.

---

# 2. Creating a Thread ⭐⭐⭐⭐⭐

```csharp
using System.Threading;

class Program
{
    static void Print()
    {
        Console.WriteLine(
            "Thread Running");
    }

    static void Main()
    {
        Thread thread =
            new Thread(Print);

        thread.Start();
    }
}
```

---

# 3. Thread with Lambda ⭐⭐⭐⭐

```csharp
Thread thread = new Thread(() =>
{
    Console.WriteLine("Running");
});

thread.Start();
```

---

# 4. Thread.Sleep() ⭐⭐⭐⭐

Pauses thread.

```csharp
Thread.Sleep(2000);
```

Pauses for:

```text
2 seconds
```

---

# 5. Foreground vs Background Thread ⭐⭐⭐

| Foreground | Background |
|------------|------------|
| Keeps App Alive | Ends with App |
| Important Work | Secondary Work |

---

# 6. Thread Join ⭐⭐⭐

Waits for thread completion.

```csharp
thread.Join();
```

---

# 7. Race Condition ⭐⭐⭐

Occurs when multiple threads access shared data simultaneously.

---

# Problem Example

```csharp
int counter = 0;

Parallel.For(0,1000, x =>
{
    counter++;
});

Console.WriteLine(counter);
```

Output may be incorrect.

---

# 8. lock Keyword ⭐⭐⭐

Prevents race condition.

```csharp
object obj = new();

lock(obj)
{
    counter++;
}
```

---

# 9. Monitor ⭐⭐

Advanced locking mechanism.

```csharp
Monitor.Enter(obj);

try
{
}
finally
{
    Monitor.Exit(obj);
}
```

---

# 10. Mutex ⭐⭐

Cross-process synchronization.

```csharp
Mutex mutex = new Mutex();
```

---

# 11. SemaphoreSlim ⭐⭐⭐

Limits concurrent access.

```csharp
SemaphoreSlim semaphore =
    new SemaphoreSlim(2);
```

Allows:

```text
2 threads at a time
```

---

# 12. ThreadPool ⭐⭐⭐⭐

Reusable threads managed by .NET.

```csharp
ThreadPool.QueueUserWorkItem(x =>
{
    Console.WriteLine("Running");
});
```

Better than creating many threads manually.

---

# 13. Parallel Programming ⭐⭐⭐⭐

Runs tasks in parallel.

```csharp
Parallel.For(1,5, i =>
{
    Console.WriteLine(i);
});
```

---

# Parallel.ForEach ⭐⭐⭐⭐

```csharp
Parallel.ForEach(numbers, number =>
{
    Console.WriteLine(number);
});
```

---

# 14. Task ⭐⭐⭐⭐⭐

Modern way for asynchronous work.

```csharp
Task task = Task.Run(() =>
{
    Console.WriteLine("Running");
});

task.Wait();
```

---

# Why Task Is Better Than Thread

| Thread | Task |
|--------|------|
| Manual | Managed |
| Heavyweight | Lightweight |
| More Complex | Easier |

---

# 15. Task<T> ⭐⭐⭐⭐⭐

Returns value.

```csharp
Task<int> task =
    Task.Run(() =>
    {
        return 10 + 20;
    });

Console.WriteLine(task.Result);
```

Output:

```text
30
```

---

# 16. async Keyword ⭐⭐⭐⭐⭐

Marks method as asynchronous.

```csharp
public async Task Show()
{
}
```

---

# 17. await Keyword ⭐⭐⭐⭐⭐

Waits asynchronously.

```csharp
await Task.Delay(2000);
```

Does NOT block thread.

---

# 18. Async Method ⭐⭐⭐⭐⭐

```csharp
static async Task Show()
{
    await Task.Delay(2000);

    Console.WriteLine("Completed");
}
```

---

# 19. Async Method Returning Value ⭐⭐⭐⭐⭐

```csharp
static async Task<int> Add()
{
    await Task.Delay(1000);

    return 10 + 20;
}
```

Usage:

```csharp
int result = await Add();
```

---

# 20. Synchronous vs Asynchronous ⭐⭐⭐⭐⭐

## Synchronous

```csharp
Download();
Process();
Save();
```

One after another.

---

## Asynchronous

```csharp
await DownloadAsync();

await ProcessAsync();

await SaveAsync();
```

Non-blocking.

---

# 21. Task.Run() ⭐⭐⭐⭐⭐

Runs work on background thread.

```csharp
await Task.Run(() =>
{
    Console.WriteLine("Heavy Work");
});
```

---

# 22. Task.Delay() ⭐⭐⭐⭐⭐

Non-blocking delay.

```csharp
await Task.Delay(2000);
```

Preferred over:

```csharp
Thread.Sleep()
```

---

# Thread.Sleep vs Task.Delay ⭐⭐⭐⭐⭐

| Thread.Sleep | Task.Delay |
|--------------|------------|
| Blocks Thread | Non-Blocking |
| Synchronous | Asynchronous |

---

# 23. Task.WhenAll() ⭐⭐⭐⭐

Runs multiple tasks together.

```csharp
Task task1 = Task.Delay(1000);

Task task2 = Task.Delay(2000);

await Task.WhenAll(task1, task2);
```

Waits for ALL tasks.

---

# 24. Task.WhenAny() ⭐⭐⭐⭐

Waits for first completed task.

```csharp
await Task.WhenAny(task1, task2);
```

---

# 25. CancellationToken ⭐⭐⭐⭐

Cancels tasks safely.

```csharp
CancellationTokenSource cts =
    new();

CancellationToken token =
    cts.Token;
```

---

# Example

```csharp
Task.Run(() =>
{
    while(!token.IsCancellationRequested)
    {
        Console.WriteLine("Running");
    }
}, token);

cts.Cancel();
```

---

# 26. Exception Handling in Async ⭐⭐⭐⭐

```csharp
try
{
    await Task.Run(() =>
    {
        throw new Exception();
    });
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
```

---

# 27. Deadlock ⭐⭐⭐⭐⭐

Occurs when threads wait for each other forever.

---

# Common ASP.NET Deadlock

❌ Dangerous

```csharp
var result =
    GetDataAsync().Result;
```

or

```csharp
GetDataAsync().Wait();
```

Can cause deadlock.

---

# Correct

```csharp
await GetDataAsync();
```

---

# 28. ConfigureAwait(false) ⭐⭐⭐⭐

Avoids context capture.

```csharp
await Task.Delay(1000)
    .ConfigureAwait(false);
```

Important in library development.

---

# 29. Concurrent Collections ⭐⭐⭐

Thread-safe collections.

Examples:

```text
ConcurrentDictionary
ConcurrentQueue
ConcurrentBag
```

---

# 30. ReaderWriterLockSlim ⭐⭐

Optimized read/write locking.

Advanced topic.

---

# Real-World Example: API Call ⭐⭐⭐⭐⭐

```csharp
public async Task<string>
GetDataAsync()
{
    HttpClient client = new();

    return await client.GetStringAsync(
        "https://example.com");
}
```

---

# Real-World Example: File Processing ⭐⭐⭐⭐⭐

```csharp
await Task.Run(() =>
{
    for(int i=0; i<1000; i++)
    {
        Console.WriteLine(i);
    }
});
```

---

# Real-World Example: Parallel Tasks ⭐⭐⭐⭐⭐

```csharp
Task task1 = Task.Delay(2000);

Task task2 = Task.Delay(3000);

await Task.WhenAll(task1, task2);

Console.WriteLine("Completed");
```

---

# Common Mistakes

## Using .Result or .Wait()

❌ Dangerous

```csharp
task.Result
```

Can cause deadlock.

---

## Using Thread Instead of Task

❌ Old Approach

```csharp
new Thread()
```

✅ Better

```csharp
Task.Run()
```

---

## Forgetting await

❌ Wrong

```csharp
GetDataAsync();
```

May cause unexpected behavior.

---

# 🎯 Most Asked Interview Questions

## Q1. What is Multithreading?

Running multiple threads simultaneously.

---

## Q2. What is Asynchronous Programming?

Running tasks without blocking the main thread.

---

## Q3. Difference Between Thread and Task?

| Thread | Task |
|--------|------|
| Low-Level | High-Level |
| Heavyweight | Lightweight |
| Manual | Managed |

---

## Q4. Difference Between Thread.Sleep and Task.Delay?

| Thread.Sleep | Task.Delay |
|--------------|------------|
| Blocking | Non-Blocking |

---

## Q5. What is async?

Marks method as asynchronous.

---

## Q6. What is await?

Waits asynchronously without blocking thread.

---

## Q7. What is Deadlock?

Two operations waiting for each other forever.

---

## Q8. Why Is async/await Important in ASP.NET Core?

Improves:

- Scalability
- Performance
- Responsiveness

---

## Q9. Difference Between Parallelism and Asynchrony?

| Parallelism | Asynchrony |
|-------------|------------|
| Multiple CPU Tasks | Non-Blocking I/O |
| Multithreading | Async Programming |

---

## Q10. What is CancellationToken?

Used to cancel asynchronous operations safely.

---

## Q11. Why Use Task Instead of Thread?

- Easier
- Better Performance
- ThreadPool Integration
- Modern Approach

---

## Q12. What Causes Race Condition?

Multiple threads modifying shared data simultaneously.

---

## Q13. What is lock Keyword?

Prevents multiple threads accessing critical section simultaneously.

---

## Q14. What is ThreadPool?

Pool of reusable threads managed by .NET runtime.

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Thread Basics
- [x] Thread vs Task
- [x] async & await
- [x] Task
- [x] Task<T>
- [x] Task.Run()
- [x] Task.Delay()
- [x] Thread.Sleep vs Task.Delay
- [x] Synchronous vs Asynchronous
- [x] Task.WhenAll()
- [x] Task.WhenAny()
- [x] CancellationToken
- [x] Exception Handling in Async
- [x] Deadlock Basics
- [x] Parallel Programming
- [x] lock Keyword
- [x] Race Condition
- [x] ThreadPool

Mastering these topics will help you answer **95%+ of Multithreading & Async Programming interview questions in .NET Full Stack Development.**