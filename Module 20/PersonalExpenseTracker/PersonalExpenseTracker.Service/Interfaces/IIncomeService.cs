using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

public interface IIncomeService
{
    Task<PagedResult<IncomeDto>> SearchAsync(IncomeSearch search, CancellationToken ct = default);
    Task<IncomeDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<OperationResult> CreateAsync(IncomeInput input, CancellationToken ct = default);
    Task<OperationResult> UpdateAsync(int id, IncomeInput input, CancellationToken ct = default);
    Task<OperationResult> DeleteAsync(int id, CancellationToken ct = default);
}
