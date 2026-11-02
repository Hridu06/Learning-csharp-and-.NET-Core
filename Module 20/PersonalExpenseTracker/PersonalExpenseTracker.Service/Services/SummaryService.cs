using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Repository.Models;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

public class SummaryService(IExpenseRepository expenses, IIncomeRepository incomes, ICategoryService categories,
    ICategoryRepository categoryTotals, IBudgetService budgets, ILoanService loans, ICurrentUser currentUser, TimeProvider clock) : ISummaryService
{
    private const int TrendMonths = 12;

    public async Task<SummaryDto> GetAsync(DateTime? month = null, CancellationToken ct = default)
    {
        var userId = currentUser.UserId;
        var today = clock.GetLocalNow().Date;
        var thisMonth = new DateTime(today.Year, today.Month, 1);
        // Future months have no data yet, so they fall back to the current month.
        var start = month is { } m && new DateTime(m.Year, m.Month, 1) < thisMonth ? new DateTime(m.Year, m.Month, 1) : thisMonth;
        var end = start.AddMonths(1);
        var trendStart = start.AddMonths(1 - TrendMonths);

        // Sequential queries share one scoped DbContext.
        var total = await expenses.GetTotalAsync(userId, start, end, ct);
        var previous = await expenses.GetTotalAsync(userId, start.AddMonths(-1), start, ct);
        var todayTotal = await expenses.GetTotalAsync(userId, today, today.AddDays(1), ct);
        var allTime = await categories.GetAllAsync(ct);
        var recent = await expenses.GetRecentAsync(userId, 5, start, end, ct);
        var monthly = await expenses.GetMonthlyTotalsAsync(userId, trendStart, end, ct);
        var monthCategories = (await categoryTotals.GetTotalsForPeriodAsync(userId, start, end, ct))
            .Select(c => new CategoryDto(c.Id, c.Name, c.Description, c.Color, c.Icon, c.ExpenseCount, c.Total)).ToList();

        var income = await incomes.GetTotalAsync(userId, start, end, ct);
        var previousIncome = await incomes.GetTotalAsync(userId, start.AddMonths(-1), start, ct);
        var allTimeIncome = await incomes.GetTotalAsync(userId, ct: ct);
        var monthlyIncome = await incomes.GetMonthlyTotalsAsync(userId, trendStart, end, ct);
        var budget = await budgets.GetOverviewAsync(start, ct);
        var loanSummary = await loans.GetSummaryAsync(ct);

        // Fill months without entries so the chart shows a continuous timeline ending at the selected month.
        var trend = Enumerable.Range(0, TrendMonths).Select(i => trendStart.AddMonths(i))
            .Select(mo => new MonthTotalDto(mo, Find(monthly, mo), Find(monthlyIncome, mo)))
            .ToList();
        return new SummaryDto(start, total, previous, todayTotal, allTime,
            recent.Select(ExpenseService.Map).ToList(), trend, monthCategories, income, previousIncome, allTimeIncome,
            budget, loanSummary, thisMonth, start == thisMonth ? today.Day : DateTime.DaysInMonth(start.Year, start.Month));
    }

    private static decimal Find(IReadOnlyList<MonthlyTotal> totals, DateTime month) =>
        totals.FirstOrDefault(t => t.Year == month.Year && t.Month == month.Month)?.Total ?? 0m;
}
