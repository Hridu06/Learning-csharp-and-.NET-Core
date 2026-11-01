using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Interfaces;

public interface IExpenseRepository
{
    Task<IReadOnlyList<Expense>> GetAllAsync(CancellationToken ct = default);
    Task<ExpensePage> SearchAsync(ExpenseFilter filter, CancellationToken ct = default);
    Task<IReadOnlyList<MonthlyTotal>> GetMonthlyTotalsAsync(DateTime from, DateTime until, CancellationToken ct = default);
    Task<Expense?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IReadOnlyList<Expense>> GetRecentAsync(int count, CancellationToken ct = default);
    Task<decimal> GetTotalAsync(DateTime from, DateTime until, CancellationToken ct = default);
    Task AddAsync(Expense expense, CancellationToken ct = default);
    Task UpdateAsync(Expense expense, CancellationToken ct = default);
    Task DeleteAsync(Expense expense, CancellationToken ct = default);
}
