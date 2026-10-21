using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken ct = default);
    Task<CategoryDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<OperationResult> CreateAsync(CategoryInput input, CancellationToken ct = default);
    Task<OperationResult> UpdateAsync(int id, CategoryInput input, CancellationToken ct = default);
    Task<OperationResult> DeleteAsync(int id, CancellationToken ct = default);
}
