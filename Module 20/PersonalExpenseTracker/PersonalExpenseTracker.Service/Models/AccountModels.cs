using PersonalExpenseTracker.Data.Entities;

namespace PersonalExpenseTracker.Service.Models;

public record StoredFile(string Path, string ContentType);

public record RegisterInput(string FullName, string Email, string Password);
public enum LoginOutcome { Succeeded, Failed, LockedOut, NotAllowed }

public record ProfileDto(string Id, string FullName, string Email, string? AvatarFileName, DateTime CreatedAt, bool IsAdmin);
// CurrentPassword is only checked when the email address changes.
public record ProfileInput(string FullName, string Email, string? CurrentPassword, bool RemoveAvatar);

public record AdminUserDto(string Id, string FullName, string? Email, DateTime CreatedAt, int ExpenseCount,
    bool IsAdmin, bool IsLocked, bool IsCurrentUser);
public record AdminDashboardDto(IReadOnlyList<AdminUserDto> Users, int NewUsersThisMonth, int TotalExpenses)
{
    public int TotalUsers => Users.Count;
    public int AdminCount => Users.Count(u => u.IsAdmin);
    public int LockedCount => Users.Count(u => u.IsLocked);
}

// Bound from the "Admin" configuration section (use User Secrets in development).
public class AdminSeedOptions
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string FullName { get; set; } = "Administrator";
}

// Extra claims added to the sign-in cookie so the layout can show the user without a database call.
public static class AppClaims
{
    public const string FullName = "full_name";
    public const string Avatar = "avatar";
}

public static class DefaultCategories
{
    public static IEnumerable<ExpenseCategory> For(string userId) =>
    [
        new() { UserId = userId, Name = "Food", Description = "Groceries, meals and dining out", Color = "#F97316", Icon = "basket" },
        new() { UserId = userId, Name = "Transport", Description = "Public transport, fuel and travel", Color = "#0EA5E9", Icon = "bus-front" },
        new() { UserId = userId, Name = "Utilities", Description = "Electricity, water, internet and other bills", Color = "#EAB308", Icon = "lightning-charge" },
        new() { UserId = userId, Name = "Entertainment", Description = "Movies, hobbies and leisure", Color = "#A855F7", Icon = "film" }
    ];
}
