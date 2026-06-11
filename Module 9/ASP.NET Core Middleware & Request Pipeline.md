# ASP.NET Core Middleware & Request Pipeline (Easy to Advanced)

---

# 📌 What is Middleware?

Middleware is a software component that handles HTTP requests and responses in ASP.NET Core.

Middleware sits inside the Request Processing Pipeline.

---

# 📌 What is Request Pipeline?

The Request Pipeline is a sequence of middleware components through which every HTTP request passes.

---

# Simple Pipeline Flow ⭐⭐⭐⭐⭐

```text
Client Request
      ↓
Middleware 1
      ↓
Middleware 2
      ↓
Middleware 3
      ↓
Endpoint / Controller
      ↓
Response Back Through Middleware
```

---

# Real-Life Example ⭐⭐⭐⭐⭐

Think of middleware like airport security checks:

```text
Request →
Security Check →
Passport Check →
Luggage Check →
Boarding →
Response
```

Each middleware performs a task.

---

# Why Middleware is Important?

Middleware is heavily used for:

- Authentication
- Authorization
- Logging
- Exception Handling
- Routing
- CORS
- Static Files
- HTTPS Redirection
- Session
- Caching

---

# 🎯 Recommended Learning Order (Most Important First)

For .NET Full Stack Interviews:

| Priority | Topic | Interview Frequency |
|-----------|---------|------------------|
| ⭐⭐⭐⭐⭐ | Middleware Basics | Very High |
| ⭐⭐⭐⭐⭐ | Request Pipeline | Very High |
| ⭐⭐⭐⭐⭐ | app.Use() | Very High |
| ⭐⭐⭐⭐⭐ | app.Run() | Very High |
| ⭐⭐⭐⭐⭐ | app.Map() | Very High |
| ⭐⭐⭐⭐⭐ | Custom Middleware | Very High |
| ⭐⭐⭐⭐⭐ | Middleware Execution Order | Very High |
| ⭐⭐⭐⭐⭐ | Built-in Middleware | Very High |
| ⭐⭐⭐⭐⭐ | UseRouting() | Very High |
| ⭐⭐⭐⭐⭐ | UseAuthentication() | Very High |
| ⭐⭐⭐⭐⭐ | UseAuthorization() | Very High |
| ⭐⭐⭐⭐ | Endpoint Middleware | High |
| ⭐⭐⭐⭐ | Exception Middleware | High |
| ⭐⭐⭐⭐ | HTTPS Redirection | High |
| ⭐⭐⭐⭐ | Static Files Middleware | High |
| ⭐⭐⭐⭐ | Branching Pipeline | High |
| ⭐⭐⭐⭐ | Short-Circuiting | High |
| ⭐⭐⭐ | IMiddleware Interface | Medium |
| ⭐⭐⭐ | Dependency Injection in Middleware | Medium |
| ⭐⭐⭐ | Terminal Middleware | Medium |
| ⭐⭐ | Inline Middleware | Low |
| ⭐⭐ | Middleware vs Filters | Low |

---

# Middleware Pipeline Architecture ⭐⭐⭐⭐⭐

```text
Request
   ↓
Middleware 1
   ↓
Middleware 2
   ↓
Middleware 3
   ↓
Endpoint
   ↑
Response
```

Response travels backward.

---

# Basic ASP.NET Core Pipeline ⭐⭐⭐⭐⭐

```csharp
var builder =
    WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Run(async context =>
{
    await context.Response.WriteAsync(
        "Hello World");
});

app.Run();
```

---

# app.Run() ⭐⭐⭐⭐⭐

Terminal middleware.

Stops pipeline execution.

```csharp
app.Run(async context =>
{
    await context.Response.WriteAsync(
        "Terminal Middleware");
});
```

No middleware executes after `Run()`.

---

# app.Use() ⭐⭐⭐⭐⭐

Adds middleware into pipeline.

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine("Before");

    await next();

    Console.WriteLine("After");
});
```

---

# Middleware Flow ⭐⭐⭐⭐⭐

```text
Before Middleware
     ↓
Next Middleware
     ↓
After Middleware
```

---

# Output Example ⭐⭐⭐⭐⭐

```text
Before
Hello World
After
```

---

# app.Map() ⭐⭐⭐⭐⭐

Branches pipeline based on route.

```csharp
app.Map("/admin", adminApp =>
{
    adminApp.Run(async context =>
    {
        await context.Response.WriteAsync(
            "Admin Page");
    });
});
```

---

# Built-in Middleware ⭐⭐⭐⭐⭐

| Middleware | Purpose |
|------------|----------|
| UseRouting | Route Matching |
| UseAuthentication | User Authentication |
| UseAuthorization | Permission Checking |
| UseStaticFiles | CSS/JS/Images |
| UseCors | Cross-Origin Requests |
| UseSession | Session Handling |
| UseExceptionHandler | Global Error Handling |
| UseHttpsRedirection | Redirect HTTP to HTTPS |

---

# UseRouting() ⭐⭐⭐⭐⭐

Matches URL to endpoint.

```csharp
app.UseRouting();
```

---

# UseAuthentication() ⭐⭐⭐⭐⭐

Checks user identity.

```csharp
app.UseAuthentication();
```

---

# UseAuthorization() ⭐⭐⭐⭐⭐

Checks permissions.

```csharp
app.UseAuthorization();
```

---

# IMPORTANT Middleware Order ⭐⭐⭐⭐⭐

Correct Order:

```csharp
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
```

---

# Wrong Order ❌

```csharp
app.UseAuthorization();

app.UseAuthentication();
```

Authentication must come first.

---

# UseStaticFiles() ⭐⭐⭐⭐

Serves files from:

```text
wwwroot
```

```csharp
app.UseStaticFiles();
```

---

# Example

```text
wwwroot/logo.png
```

Access:

```text
https://localhost/logo.png
```

---

# HTTPS Redirection ⭐⭐⭐⭐

```csharp
app.UseHttpsRedirection();
```

Redirects:

```text
http → https
```

---

# Exception Handling Middleware ⭐⭐⭐⭐

```csharp
app.UseExceptionHandler("/error");
```

Global exception handling.

---

# Custom Middleware ⭐⭐⭐⭐⭐

# 📌 What is Custom Middleware?

Middleware created by developer.

---

# Custom Middleware Example ⭐⭐⭐⭐⭐

```csharp
public class LoggingMiddleware
{
    private readonly
        RequestDelegate next;

    public LoggingMiddleware(
        RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        Console.WriteLine(
            "Request Started");

        await next(context);

        Console.WriteLine(
            "Request Finished");
    }
}
```

---

# Register Custom Middleware ⭐⭐⭐⭐⭐

```csharp
app.UseMiddleware<
    LoggingMiddleware>();
```

---

# Middleware Execution Flow ⭐⭐⭐⭐⭐

```text
Request Started
     ↓
Next Middleware
     ↓
Request Finished
```

---

# Short-Circuiting ⭐⭐⭐⭐

Stopping pipeline early.

```csharp
app.Use(async (context, next) =>
{
    await context.Response.WriteAsync(
        "Stopped");

    // No next()
});
```

Pipeline stops here.

---

# Inline Middleware ⭐⭐

Middleware directly inside Program.cs.

```csharp
app.Use(async (context, next) =>
{
    await next();
});
```

---

# IMiddleware Interface ⭐⭐⭐

Alternative middleware approach.

```csharp
public class LoggingMiddleware
    : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        await next(context);
    }
}
```

---

# Dependency Injection in Middleware ⭐⭐⭐

```csharp
public class LoggingMiddleware
{
    private readonly ILogger<
        LoggingMiddleware> logger;

    public LoggingMiddleware(
        RequestDelegate next,
        ILogger<LoggingMiddleware> logger)
    {
        this.logger = logger;
    }
}
```

---

# Endpoint Middleware ⭐⭐⭐⭐

```csharp
app.MapControllers();
```

or

```csharp
app.MapGet();
```

Final request destination.

---

# Middleware vs Filters ⭐⭐⭐

| Middleware | Filters |
|------------|----------|
| Global Pipeline | MVC Only |
| Before MVC | Inside MVC |
| Entire Request | Controller/Action |

---

# Request Lifecycle ⭐⭐⭐⭐⭐

```text
Client Request
    ↓
Kestrel Server
    ↓
Middleware Pipeline
    ↓
Routing
    ↓
Authentication
    ↓
Authorization
    ↓
Controller/Endpoint
    ↓
Response
```

---

# Kestrel Server ⭐⭐⭐⭐

Default ASP.NET Core web server.

```text
Client ↔ Kestrel ↔ Middleware
```

---

# Real-World Example: Logging Middleware ⭐⭐⭐⭐⭐

```csharp
app.Use(async (context, next) =>
{
    Console.WriteLine(
        $"Request:
        {context.Request.Path}");

    await next();

    Console.WriteLine(
        $"Response:
        {context.Response.StatusCode}");
});
```

---

# Real-World Example: API Key Middleware ⭐⭐⭐⭐⭐

```csharp
app.Use(async (context, next) =>
{
    if(!context.Request.Headers
        .ContainsKey("ApiKey"))
    {
        context.Response.StatusCode = 401;

        await context.Response.WriteAsync(
            "API Key Missing");

        return;
    }

    await next();
});
```

---

# Real-World Example: Timing Middleware ⭐⭐⭐⭐

```csharp
app.Use(async (context, next) =>
{
    var watch =
        Stopwatch.StartNew();

    await next();

    watch.Stop();

    Console.WriteLine(
        $"Time:
        {watch.ElapsedMilliseconds}");
});
```

---

# Common Mistakes

# ❌ Forgetting await next()

```csharp
app.Use(async (context, next) =>
{
});
```

Pipeline stops unexpectedly.

---

# ❌ Wrong Middleware Order

```csharp
UseAuthorization()
UseAuthentication()
```

Wrong sequence.

---

# ❌ Multiple app.Run()

Only first terminal middleware executes.

---

# Middleware Best Practices ⭐⭐⭐⭐⭐

✅ Keep middleware small

✅ Use middleware for cross-cutting concerns

✅ Maintain correct order

✅ Use async methods

✅ Log exceptions

✅ Avoid heavy business logic inside middleware

---

# 🎯 Most Asked Interview Questions

## Q1. What is Middleware?

Middleware is a software component that handles HTTP requests and responses.

---

## Q2. What is Request Pipeline?

Sequence of middleware components through which request passes.

---

## Q3. Difference Between app.Use and app.Run?

| app.Use | app.Run |
|----------|----------|
| Continues Pipeline | Terminates Pipeline |
| Calls next() | No next() |

---

## Q4. What is app.Map?

Creates branch pipeline based on URL path.

---

## Q5. Why Middleware Order Is Important?

Request flows sequentially.

Wrong order causes failures.

---

## Q6. Difference Between Authentication and Authorization?

| Authentication | Authorization |
|---------------|---------------|
| Who Are You? | What Can You Access? |

---

## Q7. What is Custom Middleware?

Middleware created by developer for custom logic.

---

## Q8. What is Short-Circuiting?

Stopping request pipeline before reaching next middleware.

---

## Q9. What is Terminal Middleware?

Middleware that ends pipeline.

Example:

```csharp
app.Run()
```

---

## Q10. Difference Between Middleware and Filters?

| Middleware | Filters |
|------------|----------|
| Entire Pipeline | MVC Only |

---

## Q11. What is Kestrel?

Built-in ASP.NET Core web server.

---

## Q12. Why Use Middleware?

For:

- Logging
- Authentication
- Authorization
- Exception Handling
- Request Processing

---

# 🚀 Must-Master Topics Before .NET Interview

- [x] Middleware Basics
- [x] Request Pipeline
- [x] app.Use()
- [x] app.Run()
- [x] app.Map()
- [x] Custom Middleware
- [x] Middleware Order
- [x] UseRouting()
- [x] UseAuthentication()
- [x] UseAuthorization()
- [x] Static Files Middleware
- [x] HTTPS Redirection
- [x] Exception Middleware
- [x] Short-Circuiting
- [x] Dependency Injection in Middleware
- [x] Endpoint Middleware

Mastering these topics will help you answer **95%+ of Middleware & Request Pipeline interview questions in ASP.NET Core Full Stack Development.**