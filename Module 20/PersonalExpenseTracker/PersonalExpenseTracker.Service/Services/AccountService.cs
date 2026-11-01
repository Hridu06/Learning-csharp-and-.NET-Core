using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

public class AccountService(UserManager<ApplicationUser> users, SignInManager<ApplicationUser> signIn,
    ICategoryRepository categories, IUserRepository userData, IReceiptStorage receipts, IAvatarStorage avatars,
    ICurrentUser currentUser, TimeProvider clock, ILogger<AccountService> logger) : IAccountService
{
    public async Task<OperationResult> RegisterAsync(RegisterInput input, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(input.FullName) || input.FullName.Trim().Length > 100)
            return OperationResult.Fail("Enter your name (up to 100 characters).", "FullName");
        var email = input.Email.Trim();
        var user = new ApplicationUser
        {
            UserName = email, Email = email, FullName = input.FullName.Trim(), CreatedAt = clock.GetUtcNow().UtcDateTime
        };
        var result = await users.CreateAsync(user, input.Password);
        if (!result.Succeeded) return FromIdentity(result, "Password");

        await users.AddToRoleAsync(user, AppRoles.User);
        await categories.AddRangeAsync(DefaultCategories.For(user.Id), ct);
        await signIn.SignInAsync(user, isPersistent: false);
        return OperationResult.Success();
    }

    public async Task<LoginOutcome> LoginAsync(string email, string password, bool rememberMe)
    {
        var user = await users.FindByEmailAsync(email.Trim());
        if (user is null) return LoginOutcome.Failed;
        var result = await signIn.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: true);
        if (result.Succeeded) return LoginOutcome.Succeeded;
        if (result.IsLockedOut) return LoginOutcome.LockedOut;
        return result.IsNotAllowed ? LoginOutcome.NotAllowed : LoginOutcome.Failed;
    }

    public Task LogoutAsync() => signIn.SignOutAsync();

    public async Task<ProfileDto> GetProfileAsync(CancellationToken ct = default)
    {
        var user = await GetUserAsync();
        return new ProfileDto(user.Id, user.FullName, user.Email ?? string.Empty, user.AvatarFileName, user.CreatedAt,
            await users.IsInRoleAsync(user, AppRoles.Admin));
    }

    public async Task<OperationResult> UpdateProfileAsync(ProfileInput input, ReceiptUpload? avatar, CancellationToken ct = default)
    {
        var user = await GetUserAsync();
        if (string.IsNullOrWhiteSpace(input.FullName) || input.FullName.Trim().Length > 100)
            return OperationResult.Fail("Enter your name (up to 100 characters).", "FullName");

        var email = input.Email.Trim();
        var emailChanged = !string.Equals(email, user.Email, StringComparison.OrdinalIgnoreCase);
        // Changing the sign-in email is sensitive, so it needs the current password.
        if (emailChanged && (string.IsNullOrEmpty(input.CurrentPassword) || !await users.CheckPasswordAsync(user, input.CurrentPassword)))
            return OperationResult.Fail("Enter your current password to change your email address.", "CurrentPassword");

        string? newAvatar = null;
        try
        {
            if (avatar is not null) newAvatar = await avatars.SaveAsync(avatar, ct);
        }
        catch (ReceiptValidationException ex) { return OperationResult.Fail(ex.Message, "Avatar"); }

        var oldAvatar = user.AvatarFileName;
        user.FullName = input.FullName.Trim();
        if (emailChanged) { user.Email = email; user.UserName = email; }
        if (newAvatar is not null) user.AvatarFileName = newAvatar;
        else if (input.RemoveAvatar) user.AvatarFileName = null;

        var result = await users.UpdateAsync(user);
        if (!result.Succeeded)
        {
            avatars.Delete(newAvatar);
            return FromIdentity(result, "Email");
        }
        // A new email signs out other sessions on their next security check.
        if (emailChanged) await users.UpdateSecurityStampAsync(user);
        if (user.AvatarFileName != oldAvatar) avatars.Delete(oldAvatar);
        await signIn.RefreshSignInAsync(user);
        return OperationResult.Success();
    }

    public async Task<OperationResult> ChangePasswordAsync(string currentPassword, string newPassword)
    {
        var user = await GetUserAsync();
        var result = await users.ChangePasswordAsync(user, currentPassword, newPassword);
        if (!result.Succeeded) return FromIdentity(result, "NewPassword");
        await signIn.RefreshSignInAsync(user);
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteAccountAsync(string password, CancellationToken ct = default)
    {
        var user = await GetUserAsync();
        if (!await users.CheckPasswordAsync(user, password))
            return OperationResult.Fail("The password is incorrect.", "Password");
        if (await users.IsInRoleAsync(user, AppRoles.Admin) && (await users.GetUsersInRoleAsync(AppRoles.Admin)).Count == 1)
            return OperationResult.Fail("You are the only administrator. Make someone else an admin before deleting your account.");

        var files = await userData.GetReceiptFileNamesAsync(user.Id, ct);
        await userData.DeleteWithDataAsync(user.Id, ct);
        await signIn.SignOutAsync();

        // Files go last: a leftover file is harmless, a missing database row is not.
        var cleaned = avatars.Delete(user.AvatarFileName);
        foreach (var file in files) cleaned = receipts.Delete(file) && cleaned;
        if (!cleaned) logger.LogWarning("Some files for deleted user {UserId} could not be removed.", user.Id);
        return OperationResult.Success();
    }

    public async Task<StoredFile?> GetAvatarAsync(CancellationToken ct = default) =>
        avatars.Find((await GetUserAsync()).AvatarFileName);

    private async Task<ApplicationUser> GetUserAsync() =>
        await users.FindByIdAsync(currentUser.UserId)
        ?? throw new InvalidOperationException("The signed-in user no longer exists.");

    private static OperationResult FromIdentity(IdentityResult result, string passwordField)
    {
        var codes = result.Errors.Select(e => e.Code).ToList();
        // Email doubles as the user name, so duplicates are reported once, in plain words.
        if (codes.Contains(nameof(IdentityErrorDescriber.DuplicateEmail)) || codes.Contains(nameof(IdentityErrorDescriber.DuplicateUserName)))
            return OperationResult.Fail("An account with this email address already exists.", "Email");
        var field = codes[0] switch
        {
            nameof(IdentityErrorDescriber.InvalidEmail) or nameof(IdentityErrorDescriber.InvalidUserName) => "Email",
            nameof(IdentityErrorDescriber.PasswordMismatch) => "CurrentPassword",
            var code when code.StartsWith("Password", StringComparison.Ordinal) => passwordField,
            _ => null
        };
        return OperationResult.Fail(string.Join(" ", result.Errors.Select(e => e.Description)), field);
    }
}
