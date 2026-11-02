namespace PersonalExpenseTracker.Repository.Interfaces;

// Account-level data operations that span users (admin statistics) or all of one user's data.
public interface IUserRepository
{
    Task<IReadOnlyDictionary<string, int>> GetExpenseCountsAsync(CancellationToken ct = default);
    Task<int> CountExpensesAsync(CancellationToken ct = default);
    Task<IReadOnlyList<string>> GetReceiptFileNamesAsync(string userId, CancellationToken ct = default);
    Task DeleteWithDataAsync(string userId, CancellationToken ct = default);
    Task<decimal?> GetMonthlyBudgetAsync(string userId, CancellationToken ct = default);
    Task SetMonthlyBudgetAsync(string userId, decimal? amount, CancellationToken ct = default);
}
