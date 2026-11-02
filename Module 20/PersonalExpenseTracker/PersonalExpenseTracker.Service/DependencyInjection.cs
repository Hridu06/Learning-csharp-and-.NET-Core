using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Repository.Repositories;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;
using PersonalExpenseTracker.Service.Services;

namespace PersonalExpenseTracker.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddExpenseTracker(this IServiceCollection services,
        string connectionString, string receiptDirectory, string avatarDirectory)
    {
        services.AddDbContext<ExpenseTrackerDbContext>(options => options.UseSqlServer(connectionString));
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<ExpenseTrackerDbContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<AppClaimsPrincipalFactory>();
        // Re-check the security stamp often so locks and role changes reach active sessions quickly.
        services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.FromMinutes(1));

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, HttpCurrentUser>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<IBudgetService, BudgetService>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<ISummaryService, SummaryService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IdentitySeeder>();
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IReceiptStorage>(provider =>
            new FileReceiptStorage(receiptDirectory, provider.GetRequiredService<ILogger<FileReceiptStorage>>()));
        services.AddScoped<IAvatarStorage>(provider =>
            new FileAvatarStorage(avatarDirectory, provider.GetRequiredService<ILogger<FileReceiptStorage>>()));
        return services;
    }

    // Startup calls this through the service layer; controllers only depend on service contracts.
    public static async Task InitializeExpenseTrackerAsync(this IServiceProvider provider, AdminSeedOptions admin,
        CancellationToken ct = default)
    {
        using var scope = provider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ExpenseTrackerDbContext>();
        await context.Database.MigrateAsync(ct);
        await scope.ServiceProvider.GetRequiredService<IdentitySeeder>().SeedAsync(admin, ct);
    }
}
