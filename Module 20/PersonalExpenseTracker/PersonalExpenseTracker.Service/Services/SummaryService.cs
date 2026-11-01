using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

public class SummaryService(IExpenseRepository expenses, ICategoryService categories, ICurrentUser currentUser,
    TimeProvider clock) : ISummaryService
{
    private const int TrendMonths = 12;

    public async Task<SummaryDto> GetAsync(CancellationToken ct = default)
    {
        var userId = currentUser.UserId;
        var today = clock.GetLocalNow().Date;
        var start = new DateTime(today.Year, today.Month, 1);
        var trendStart = start.AddMonths(1 - TrendMonths);
        // Sequential queries share one scoped DbContext.
        var total = await expenses.GetTotalAsync(userId, start, start.AddMonths(1), ct);
        var previous = await expenses.GetTotalAsync(userId, start.AddMonths(-1), start, ct);
        var todayTotal = await expenses.GetTotalAsync(userId, today, today.AddDays(1), ct);
        var totals = await categories.GetAllAsync(ct);
        var recent = await expenses.GetRecentAsync(userId, 5, ct);
        var monthly = await expenses.GetMonthlyTotalsAsync(userId, trendStart, start.AddMonths(1), ct);

        // Fill months without expenses so the chart shows a continuous timeline.
        var trend = Enumerable.Range(0, TrendMonths).Select(i => trendStart.AddMonths(i))
            .Select(m => new MonthTotalDto(m, monthly.FirstOrDefault(t => t.Year == m.Year && t.Month == m.Month)?.Total ?? 0m))
            .ToList();
        return new SummaryDto(start, total, previous, todayTotal, totals,
            recent.Select(ExpenseService.Map).ToList(), trend);
    }
}
