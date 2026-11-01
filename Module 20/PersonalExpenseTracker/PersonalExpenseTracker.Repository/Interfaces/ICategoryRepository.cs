using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Interfaces;

// Every read is scoped to the owning user; ids from other users behave as if they don't exist.
public interface ICategoryRepository
{
    Task<IReadOnlyList<CategoryAggregate>> GetAllWithTotalsAsync(string userId, CancellationToken ct = default);
    Task<ExpenseCategory?> GetByIdAsync(string userId, int id, CancellationToken ct = default);
    Task<bool> ExistsAsync(string userId, int id, CancellationToken ct = default);
    Task<bool> NameExistsAsync(string userId, string name, int? exceptId = null, CancellationToken ct = default);
    Task<bool> AnyAsync(string userId, CancellationToken ct = default);
    Task<bool> HasExpensesAsync(int id, CancellationToken ct = default);
    Task AddAsync(ExpenseCategory category, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<ExpenseCategory> categories, CancellationToken ct = default);
    Task UpdateAsync(ExpenseCategory category, CancellationToken ct = default);
    Task DeleteAsync(ExpenseCategory category, CancellationToken ct = default);
}
