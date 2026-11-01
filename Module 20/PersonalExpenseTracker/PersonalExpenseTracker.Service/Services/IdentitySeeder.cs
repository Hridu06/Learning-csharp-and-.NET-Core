using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

// Ensures roles exist and turns the placeholder admin from the AddIdentity migration into a real account.
public class IdentitySeeder(UserManager<ApplicationUser> users, RoleManager<IdentityRole> roles,
    ICategoryRepository categories, ILogger<IdentitySeeder> logger)
{
    public async Task SeedAsync(AdminSeedOptions options, CancellationToken ct = default)
    {
        foreach (var role in new[] { AppRoles.Admin, AppRoles.User })
        {
            if (!await roles.RoleExistsAsync(role)) await roles.CreateAsync(new IdentityRole(role));
        }

        var admin = await users.FindByIdAsync(AppRoles.AdminUserId);
        if (admin is null) return;

        if (admin.PasswordHash is null)
        {
            if (string.IsNullOrWhiteSpace(options.Email) || string.IsNullOrWhiteSpace(options.Password))
            {
                logger.LogWarning("The admin account has no credentials yet. Set Admin:Email and Admin:Password (for example with dotnet user-secrets) and restart.");
            }
            else
            {
                admin.Email = options.Email.Trim();
                admin.UserName = admin.Email;
                admin.FullName = options.FullName;
                admin.EmailConfirmed = true;
                var result = await users.UpdateAsync(admin);
                if (result.Succeeded) result = await users.AddPasswordAsync(admin, options.Password);
                if (!result.Succeeded)
                    logger.LogError("Could not configure the admin account: {Errors}", string.Join(" ", result.Errors.Select(e => e.Description)));
            }
        }

        if (!await users.IsInRoleAsync(admin, AppRoles.Admin)) await users.AddToRoleAsync(admin, AppRoles.Admin);
        // A fresh database has no legacy categories for the admin, so give them the defaults.
        if (!await categories.AnyAsync(admin.Id, ct)) await categories.AddRangeAsync(DefaultCategories.For(admin.Id), ct);
    }
}
