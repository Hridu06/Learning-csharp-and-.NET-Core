using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Interfaces;

// Every read is scoped to the owning user; ids from other users behave as if they don't exist.
public interface IExpenseRepository
{
    Task<ExpensePage> SearchAsync(string userId, ExpenseFilter filter, CancellationToken ct = default);
    Task<IReadOnlyList<MonthlyTotal>> GetMonthlyTotalsAsync(string userId, DateTime from, DateTime until, CancellationToken ct = default);
    Task<Expense?> GetByIdAsync(string userId, int id, CancellationToken ct = default);
    Task<IReadOnlyList<Expense>> GetRecentAsync(string userId, int count, CancellationToken ct = default);
    Task<decimal> GetTotalAsync(string userId, DateTime from, DateTime until, CancellationToken ct = default);
    Task AddAsync(Expense expense, CancellationToken ct = default);
    Task UpdateAsync(Expense expense, CancellationToken ct = default);
    Task DeleteAsync(Expense expense, CancellationToken ct = default);
}
