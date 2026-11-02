using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Interfaces;

// Every read is scoped to the owning user; ids from other users behave as if they don't exist.
public interface IIncomeRepository
{
    Task<IncomePage> SearchAsync(string userId, IncomeFilter filter, CancellationToken ct = default);
    Task<Income?> GetByIdAsync(string userId, int id, CancellationToken ct = default);
    Task<decimal> GetTotalAsync(string userId, DateTime? from = null, DateTime? until = null, CancellationToken ct = default);
    Task<IReadOnlyList<MonthlyTotal>> GetMonthlyTotalsAsync(string userId, DateTime from, DateTime until, CancellationToken ct = default);
    Task AddAsync(Income income, CancellationToken ct = default);
    Task UpdateAsync(Income income, CancellationToken ct = default);
    Task DeleteAsync(Income income, CancellationToken ct = default);
}
