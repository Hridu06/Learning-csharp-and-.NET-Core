using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

public interface IReceiptStorage
{
    Task<string> SaveAsync(ReceiptUpload upload, CancellationToken ct = default);
    bool Delete(string? fileName);
    // Full path of a stored file, or null if the name is invalid or the file is missing.
    StoredFile? Find(string? fileName);
}

// Same storage rules for profile pictures, kept in their own folder.
public interface IAvatarStorage : IReceiptStorage;
