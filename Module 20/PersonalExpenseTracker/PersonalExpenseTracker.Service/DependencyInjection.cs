using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Repository.Repositories;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Services;

namespace PersonalExpenseTracker.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddExpenseTracker(this IServiceCollection services,
        string connectionString, string receiptDirectory)
    {
        services.AddDbContext<ExpenseTrackerDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISummaryService, SummaryService>();
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IReceiptStorage>(provider =>
            new FileReceiptStorage(receiptDirectory, provider.GetRequiredService<ILogger<FileReceiptStorage>>()));
        return services;
    }

    // Startup calls this through the service layer; controllers only depend on service contracts.
    public static async Task InitializeExpenseTrackerAsync(this IServiceProvider provider, CancellationToken ct = default)
    {
        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ExpenseTrackerDbContext>();
        await context.Database.MigrateAsync(ct);
    }
}
