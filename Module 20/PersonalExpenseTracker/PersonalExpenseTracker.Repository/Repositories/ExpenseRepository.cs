using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;

namespace PersonalExpenseTracker.Repository.Repositories;

public class ExpenseRepository(ExpenseTrackerDbContext context) : RepositoryBase(context), IExpenseRepository
{
    public async Task<IReadOnlyList<Expense>> GetAllAsync(CancellationToken ct = default) =>
        await Context.Expenses.AsNoTracking().Include(e => e.Category)
            .OrderByDescending(e => e.Date).ThenByDescending(e => e.Id).ToListAsync(ct);

    public Task<Expense?> GetByIdAsync(int id, CancellationToken ct = default) =>
        Context.Expenses.AsNoTracking().Include(e => e.Category).FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<IReadOnlyList<Expense>> GetRecentAsync(int count, CancellationToken ct = default) =>
        await Context.Expenses.AsNoTracking().Include(e => e.Category)
            .OrderByDescending(e => e.Date).ThenByDescending(e => e.Id).Take(count).ToListAsync(ct);

    public async Task<decimal> GetTotalAsync(DateTime from, DateTime until, CancellationToken ct = default) =>
        await Context.Expenses.Where(e => e.Date >= from && e.Date < until)
            .SumAsync(e => (decimal?)e.Amount, ct) ?? 0m;

    public async Task AddAsync(Expense expense, CancellationToken ct = default)
    {
        Context.Expenses.Add(expense);
        await SaveAsync(ct);
    }

    public async Task UpdateAsync(Expense expense, CancellationToken ct = default)
    {
        Context.Entry(expense).State = EntityState.Modified;
        await SaveAsync(ct);
    }

    public async Task DeleteAsync(Expense expense, CancellationToken ct = default)
    {
        Context.Expenses.Remove(expense);
        await SaveAsync(ct);
    }
}
