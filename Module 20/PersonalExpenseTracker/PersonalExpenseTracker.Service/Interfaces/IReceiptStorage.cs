using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

public interface IReceiptStorage
{
    Task<string> SaveAsync(ReceiptUpload upload, CancellationToken ct = default);
    bool Delete(string? fileName);
}
