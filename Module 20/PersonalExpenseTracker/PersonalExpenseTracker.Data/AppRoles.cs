namespace PersonalExpenseTracker.Data;

public static class AppRoles
{
    public const string Admin = "Admin";
    public const string User = "User";

    // Created by the AddIdentity migration so expenses recorded before accounts existed have an owner.
    // Startup gives it the email and password from the "Admin" configuration section.
    public const string AdminUserId = "00000000-0000-0000-0000-000000000001";
}
