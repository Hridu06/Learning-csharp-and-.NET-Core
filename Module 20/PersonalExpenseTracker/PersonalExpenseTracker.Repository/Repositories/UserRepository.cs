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

    // Expenses, then categories, then the user (Identity's own tables cascade from the user row).
    // One transaction, so a failure never leaves a half-deleted account.
    public async Task DeleteWithDataAsync(string userId, CancellationToken ct = default)
    {
        await using var transaction = await Context.Database.BeginTransactionAsync(ct);
        await Context.Expenses.Where(e => e.UserId == userId).ExecuteDeleteAsync(ct);
        await Context.ExpenseCategories.Where(c => c.UserId == userId).ExecuteDeleteAsync(ct);
        await Context.Users.Where(u => u.Id == userId).ExecuteDeleteAsync(ct);
        await transaction.CommitAsync(ct);
    }
}
