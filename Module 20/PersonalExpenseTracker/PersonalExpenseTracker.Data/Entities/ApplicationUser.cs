using Microsoft.AspNetCore.Identity;

namespace PersonalExpenseTracker.Data.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string? AvatarFileName { get; set; }
    public DateTime CreatedAt { get; set; }
}
