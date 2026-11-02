using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Repositories;

public class IncomeRepository(ExpenseTrackerDbContext context) : RepositoryBase(context), IIncomeRepository
{
    public async Task<IncomePage> SearchAsync(string userId, IncomeFilter filter, CancellationToken ct = default)
    {
        var query = Context.Incomes.AsNoTracking().Where(i => i.UserId == userId);
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var term = filter.Search.Trim();
            query = query.Where(i => i.Title.Contains(term) || (i.Notes != null && i.Notes.Contains(term)));
        }
        if (filter.Source.HasValue) query = query.Where(i => i.Source == filter.Source.Value);
        if (filter.From.HasValue) query = query.Where(i => i.Date >= filter.From.Value.Date);
        if (filter.To.HasValue) query = query.Where(i => i.Date <= filter.To.Value.Date);

        var totalCount = await query.CountAsync(ct);
        var totalAmount = await query.SumAsync(i => (decimal?)i.Amount, ct) ?? 0m;
        var lastPage = Math.Max(1, (int)Math.Ceiling(totalCount / (double)filter.PageSize));
        var page = Math.Clamp(filter.Page, 1, lastPage);

        var ordered = (filter.Sort, filter.Descending) switch
        {
            (IncomeSortField.Title, false) => query.OrderBy(i => i.Title),
            (IncomeSortField.Title, true) => query.OrderByDescending(i => i.Title),
            (IncomeSortField.Amount, false) => query.OrderBy(i => i.Amount),
            (IncomeSortField.Amount, true) => query.OrderByDescending(i => i.Amount),
            (IncomeSortField.Source, false) => query.OrderBy(i => i.Source),
            (IncomeSortField.Source, true) => query.OrderByDescending(i => i.Source),
            (_, false) => query.OrderBy(i => i.Date),
            _ => query.OrderByDescending(i => i.Date)
        };
        // Id as tie-breaker keeps paging stable when sort values repeat.
        var items = await ordered.ThenByDescending(i => i.Id)
            .Skip((page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync(ct);
        return new IncomePage(items, totalCount, totalAmount, page);
    }

    public Task<Income?> GetByIdAsync(string userId, int id, CancellationToken ct = default) =>
        Context.Incomes.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id && i.UserId == userId, ct);

    public async Task<decimal> GetTotalAsync(string userId, DateTime? from = null, DateTime? until = null, CancellationToken ct = default) =>
        await Context.Incomes.Where(i => i.UserId == userId
                && (!from.HasValue || i.Date >= from.Value) && (!until.HasValue || i.Date < until.Value))
            .SumAsync(i => (decimal?)i.Amount, ct) ?? 0m;

    public async Task<IReadOnlyList<MonthlyTotal>> GetMonthlyTotalsAsync(string userId, DateTime from, DateTime until, CancellationToken ct = default) =>
        await Context.Incomes.Where(i => i.UserId == userId && i.Date >= from && i.Date < until)
            .GroupBy(i => new { i.Date.Year, i.Date.Month })
            .Select(g => new MonthlyTotal(g.Key.Year, g.Key.Month, g.Sum(i => i.Amount)))
            .ToListAsync(ct);

    public async Task AddAsync(Income income, CancellationToken ct = default)
    {
        Context.Incomes.Add(income);
        await SaveAsync(ct);
    }

    public async Task UpdateAsync(Income income, CancellationToken ct = default)
    {
        Context.Entry(income).State = EntityState.Modified;
        await SaveAsync(ct);
    }

    public async Task DeleteAsync(Income income, CancellationToken ct = default)
    {
        Context.Incomes.Remove(income);
        await SaveAsync(ct);
    }
}
