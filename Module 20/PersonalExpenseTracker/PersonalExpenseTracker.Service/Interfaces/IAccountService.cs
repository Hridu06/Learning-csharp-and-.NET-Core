using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

public interface IAccountService
{
    // Creates the account with the User role and default categories, then signs in.
    Task<OperationResult> RegisterAsync(RegisterInput input, CancellationToken ct = default);
    Task<LoginOutcome> LoginAsync(string email, string password, bool rememberMe);
    Task LogoutAsync();

    // The members below act on the signed-in user.
    Task<ProfileDto> GetProfileAsync(CancellationToken ct = default);
    Task<OperationResult> UpdateProfileAsync(ProfileInput input, ReceiptUpload? avatar, CancellationToken ct = default);
    Task<OperationResult> ChangePasswordAsync(string currentPassword, string newPassword);
    Task<OperationResult> DeleteAccountAsync(string password, CancellationToken ct = default);
    Task<StoredFile?> GetAvatarAsync(CancellationToken ct = default);
}
