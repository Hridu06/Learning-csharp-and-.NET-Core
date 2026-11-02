using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Interfaces;

// Every read is scoped to the owning user; ids from other users behave as if they don't exist.
public interface ICategoryRepository
{
    Task<IReadOnlyList<CategoryAggregate>> GetAllWithTotalsAsync(string userId, CancellationToken ct = default);
    // Only categories with spending in [from, until), biggest first.
    Task<IReadOnlyList<CategoryAggregate>> GetTotalsForPeriodAsync(string userId, DateTime from, DateTime until, CancellationToken ct = default);
    Task<ExpenseCategory?> GetByIdAsync(string userId, int id, CancellationToken ct = default);
    Task<bool> ExistsAsync(string userId, int id, CancellationToken ct = default);
    Task<bool> NameExistsAsync(string userId, string name, int? exceptId = null, CancellationToken ct = default);
    Task<bool> AnyAsync(string userId, CancellationToken ct = default);
    // Returns false when the category doesn't exist for this user.
    Task<bool> SetBudgetAsync(string userId, int id, decimal? amount, CancellationToken ct = default);
    Task<bool> HasExpensesAsync(int id, CancellationToken ct = default);
    Task AddAsync(ExpenseCategory category, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<ExpenseCategory> categories, CancellationToken ct = default);
    Task UpdateAsync(ExpenseCategory category, CancellationToken ct = default);
    Task DeleteAsync(ExpenseCategory category, CancellationToken ct = default);
}
