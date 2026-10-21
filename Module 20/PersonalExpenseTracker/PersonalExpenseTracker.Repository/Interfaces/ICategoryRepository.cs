using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Interfaces;

public interface ICategoryRepository
{
    Task<IReadOnlyList<CategoryAggregate>> GetAllWithTotalsAsync(CancellationToken ct = default);
    Task<ExpenseCategory?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsAsync(int id, CancellationToken ct = default);
    Task<bool> NameExistsAsync(string name, int? exceptId = null, CancellationToken ct = default);
    Task<bool> HasExpensesAsync(int id, CancellationToken ct = default);
    Task AddAsync(ExpenseCategory category, CancellationToken ct = default);
    Task UpdateAsync(ExpenseCategory category, CancellationToken ct = default);
    Task DeleteAsync(ExpenseCategory category, CancellationToken ct = default);
}
