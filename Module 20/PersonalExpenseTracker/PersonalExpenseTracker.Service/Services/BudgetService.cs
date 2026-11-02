using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

public class BudgetService(ICategoryRepository categories, IUserRepository users, IExpenseRepository expenses,
    ICurrentUser currentUser, TimeProvider clock) : IBudgetService
{
    public async Task<BudgetOverviewDto> GetOverviewAsync(DateTime? month = null, CancellationToken ct = default)
    {
        var userId = currentUser.UserId;
        var today = clock.GetLocalNow().Date;
        var thisMonth = new DateTime(today.Year, today.Month, 1);
        var start = month is { } m && new DateTime(m.Year, m.Month, 1) < thisMonth ? new DateTime(m.Year, m.Month, 1) : thisMonth;
        var daysInMonth = DateTime.DaysInMonth(start.Year, start.Month);
        // A past month is complete, so pace is measured against its last day.
        var day = start == thisMonth ? today.Day : daysInMonth;
        var all = await categories.GetAllWithTotalsAsync(userId, ct);
        var spentByCategory = (await categories.GetTotalsForPeriodAsync(userId, start, start.AddMonths(1), ct))
            .ToDictionary(c => c.Id, c => c.Total);
        var spent = await expenses.GetTotalAsync(userId, start, start.AddMonths(1), ct);
        var overall = await users.GetMonthlyBudgetAsync(userId, ct);

        // Budgeted categories first (closest to their limit on top), then the rest by spending.
        var lines = all.Select(c => new BudgetLine(c.Id, c.Name, c.Color, c.Icon, c.MonthlyBudget, spentByCategory.GetValueOrDefault(c.Id)))
            .OrderBy(l => l.Limit.HasValue ? 0 : 1)
            .ThenByDescending(l => l.Percent ?? 0)
            .ThenByDescending(l => l.Spent)
            .ToList();
        return new BudgetOverviewDto(start, day, daysInMonth, overall, spent, lines);
    }

    public async Task<OperationResult> SetCategoryBudgetAsync(int categoryId, decimal? amount, CancellationToken ct = default)
    {
        var validation = Validate(amount);
        if (validation is not null) return validation;
        return await categories.SetBudgetAsync(currentUser.UserId, categoryId, amount, ct)
            ? OperationResult.Success()
            : OperationResult.Missing();
    }

    public async Task<OperationResult> SetOverallBudgetAsync(decimal? amount, CancellationToken ct = default)
    {
        var validation = Validate(amount);
        if (validation is not null) return validation;
        await users.SetMonthlyBudgetAsync(currentUser.UserId, amount, ct);
        return OperationResult.Success();
    }

    public async Task<string?> GetOverBudgetWarningAsync(int categoryId, DateTime date, CancellationToken ct = default)
    {
        var userId = currentUser.UserId;
        var start = new DateTime(date.Year, date.Month, 1);
        var category = await categories.GetByIdAsync(userId, categoryId, ct);
        if (category?.MonthlyBudget is { } limit)
        {
            var spent = (await categories.GetTotalsForPeriodAsync(userId, start, start.AddMonths(1), ct))
                .FirstOrDefault(c => c.Id == categoryId)?.Total ?? 0m;
            if (spent > limit)
                return $"{category.Name} is over its {start:MMMM} budget by ৳ {spent - limit:N2} (৳ {spent:N2} of ৳ {limit:N2}).";
        }
        if (await users.GetMonthlyBudgetAsync(userId, ct) is { } overall)
        {
            var total = await expenses.GetTotalAsync(userId, start, start.AddMonths(1), ct);
            if (total > overall)
                return $"You're over your {start:MMMM} budget by ৳ {total - overall:N2}.";
        }
        return null;
    }

    private static OperationResult? Validate(decimal? amount)
    {
        if (amount is null) return null;
        if (amount <= 0 || amount > 9999999999999999.99m)
            return OperationResult.Fail("Enter a budget greater than zero, or leave it empty to remove it.", "Amount");
        if (decimal.Round(amount.Value, 2) != amount)
            return OperationResult.Fail("Use no more than two decimal places.", "Amount");
        return null;
    }
}
