using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Repositories;

public class ExpenseRepository(ExpenseTrackerDbContext context) : RepositoryBase(context), IExpenseRepository
{
    public async Task<IReadOnlyList<Expense>> GetAllAsync(CancellationToken ct = default) =>
        await Context.Expenses.AsNoTracking().Include(e => e.Category)
            .OrderByDescending(e => e.Date).ThenByDescending(e => e.Id).ToListAsync(ct);

    public async Task<ExpensePage> SearchAsync(ExpenseFilter filter, CancellationToken ct = default)
    {
        var query = Context.Expenses.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var term = filter.Search.Trim();
            query = query.Where(e => e.Title.Contains(term) || (e.Notes != null && e.Notes.Contains(term)));
        }
        if (filter.CategoryId.HasValue) query = query.Where(e => e.CategoryId == filter.CategoryId.Value);
        if (filter.From.HasValue) query = query.Where(e => e.Date >= filter.From.Value.Date);
        if (filter.To.HasValue) query = query.Where(e => e.Date <= filter.To.Value.Date);
        if (filter.MinAmount.HasValue) query = query.Where(e => e.Amount >= filter.MinAmount.Value);
        if (filter.MaxAmount.HasValue) query = query.Where(e => e.Amount <= filter.MaxAmount.Value);

        var totalCount = await query.CountAsync(ct);
        var totalAmount = await query.SumAsync(e => (decimal?)e.Amount, ct) ?? 0m;
        var lastPage = Math.Max(1, (int)Math.Ceiling(totalCount / (double)filter.PageSize));
        var page = Math.Clamp(filter.Page, 1, lastPage);

        var ordered = (filter.Sort, filter.Descending) switch
        {
            (ExpenseSortField.Title, false) => query.OrderBy(e => e.Title),
            (ExpenseSortField.Title, true) => query.OrderByDescending(e => e.Title),
            (ExpenseSortField.Amount, false) => query.OrderBy(e => e.Amount),
            (ExpenseSortField.Amount, true) => query.OrderByDescending(e => e.Amount),
            (ExpenseSortField.Category, false) => query.OrderBy(e => e.Category.Name),
            (ExpenseSortField.Category, true) => query.OrderByDescending(e => e.Category.Name),
            (_, false) => query.OrderBy(e => e.Date),
            _ => query.OrderByDescending(e => e.Date)
        };
        // Id as tie-breaker keeps paging stable when sort values repeat.
        var items = await ordered.ThenByDescending(e => e.Id).Include(e => e.Category)
            .Skip((page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync(ct);
        return new ExpensePage(items, totalCount, totalAmount, page);
    }

    public async Task<IReadOnlyList<MonthlyTotal>> GetMonthlyTotalsAsync(DateTime from, DateTime until, CancellationToken ct = default) =>
        await Context.Expenses.Where(e => e.Date >= from && e.Date < until)
            .GroupBy(e => new { e.Date.Year, e.Date.Month })
            .Select(g => new MonthlyTotal(g.Key.Year, g.Key.Month, g.Sum(e => e.Amount)))
            .ToListAsync(ct);

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
