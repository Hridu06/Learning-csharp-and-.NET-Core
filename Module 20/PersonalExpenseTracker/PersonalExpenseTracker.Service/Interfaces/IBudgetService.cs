using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

public interface IBudgetService
{
    // Any day in the month to show; null means the current month.
    Task<BudgetOverviewDto> GetOverviewAsync(DateTime? month = null, CancellationToken ct = default);
    // A null amount removes the limit.
    Task<OperationResult> SetCategoryBudgetAsync(int categoryId, decimal? amount, CancellationToken ct = default);
    Task<OperationResult> SetOverallBudgetAsync(decimal? amount, CancellationToken ct = default);
    // A message when the expense's category or the overall budget is exceeded in that expense's month.
    Task<string?> GetOverBudgetWarningAsync(int categoryId, DateTime date, CancellationToken ct = default);
}
