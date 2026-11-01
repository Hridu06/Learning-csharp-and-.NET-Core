using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

public interface IExpenseService
{
    Task<PagedResult<ExpenseDto>> SearchAsync(ExpenseSearch search, CancellationToken ct = default);
    Task<StoredFile?> GetReceiptAsync(int id, CancellationToken ct = default);
    Task<ExpenseDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<OperationResult> CreateAsync(ExpenseInput input, ReceiptUpload? receipt, CancellationToken ct = default);
    Task<OperationResult> UpdateAsync(int id, ExpenseInput input, ReceiptUpload? receipt, CancellationToken ct = default);
    Task<OperationResult> DeleteAsync(int id, CancellationToken ct = default);
}
