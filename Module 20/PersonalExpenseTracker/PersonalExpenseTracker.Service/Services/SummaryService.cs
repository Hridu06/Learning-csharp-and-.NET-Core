using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

public class SummaryService(IExpenseRepository expenses, ICategoryService categories, TimeProvider clock) : ISummaryService
{
    public async Task<SummaryDto> GetAsync(CancellationToken ct = default)
    {
        var today = clock.GetLocalNow().Date;
        var start = new DateTime(today.Year, today.Month, 1);
        // Sequential queries share one scoped DbContext.
        var total = await expenses.GetTotalAsync(start, start.AddMonths(1), ct);
        var totals = await categories.GetAllAsync(ct);
        var recent = await expenses.GetRecentAsync(5, ct);
        return new SummaryDto(start, total, totals, recent.Select(ExpenseService.Map).ToList());
    }
}
