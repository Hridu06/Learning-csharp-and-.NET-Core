using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Repository.Interfaces;

namespace PersonalExpenseTracker.Repository.Repositories;

public class UserRepository(ExpenseTrackerDbContext context) : RepositoryBase(context), IUserRepository
{
    public async Task<IReadOnlyDictionary<string, int>> GetExpenseCountsAsync(CancellationToken ct = default) =>
        await Context.Expenses.GroupBy(e => e.UserId)
            .Select(g => new { UserId = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.UserId, x => x.Count, ct);

    public Task<int> CountExpensesAsync(CancellationToken ct = default) => Context.Expenses.CountAsync(ct);

    public async Task<IReadOnlyList<string>> GetReceiptFileNamesAsync(string userId, CancellationToken ct = default) =>
        await Context.Expenses.Where(e => e.UserId == userId && e.ReceiptFileName != null)
            .Select(e => e.ReceiptFileName!).ToListAsync(ct);

    public Task<decimal?> GetMonthlyBudgetAsync(string userId, CancellationToken ct = default) =>
        Context.Users.Where(u => u.Id == userId).Select(u => u.MonthlyBudget).FirstOrDefaultAsync(ct);

    public Task SetMonthlyBudgetAsync(string userId, decimal? amount, CancellationToken ct = default) =>
        Context.Users.Where(u => u.Id == userId).ExecuteUpdateAsync(s => s.SetProperty(u => u.MonthlyBudget, amount), ct);

    // Loans (payments cascade), income and expenses, then categories, then the user
    // (Identity's own tables cascade from the user row).
    // One transaction, so a failure never leaves a half-deleted account.
    public async Task DeleteWithDataAsync(string userId, CancellationToken ct = default)
    {
        await using var transaction = await Context.Database.BeginTransactionAsync(ct);
        await Context.Loans.Where(l => l.UserId == userId).ExecuteDeleteAsync(ct);
        await Context.Incomes.Where(i => i.UserId == userId).ExecuteDeleteAsync(ct);
        await Context.Expenses.Where(e => e.UserId == userId).ExecuteDeleteAsync(ct);
        await Context.ExpenseCategories.Where(c => c.UserId == userId).ExecuteDeleteAsync(ct);
        await Context.Users.Where(u => u.Id == userId).ExecuteDeleteAsync(ct);
        await transaction.CommitAsync(ct);
    }
}
