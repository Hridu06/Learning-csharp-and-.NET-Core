namespace PersonalExpenseTracker.Service.Models;

public enum BudgetStatus { None, OnTrack, Warning, Over }

public static class BudgetRules
{
    public const decimal WarningPercent = 80;
    // How far usage may run ahead of the calendar before we call it "ahead of pace".
    public const decimal PaceTolerance = 10;

    public static BudgetStatus StatusFor(decimal? usedPercent) => usedPercent switch
    {
        null => BudgetStatus.None,
        > 100 => BudgetStatus.Over,
        >= WarningPercent => BudgetStatus.Warning,
        _ => BudgetStatus.OnTrack
    };
}

// One category's spending this month against its (optional) monthly limit.
public record BudgetLine(int CategoryId, string Name, string Color, string Icon, decimal? Limit, decimal Spent)
{
    public decimal? Percent => Limit > 0 ? Spent / Limit * 100 : null;
    public decimal? Remaining => Limit - Spent;
    public BudgetStatus Status => BudgetRules.StatusFor(Percent);
}

public record BudgetOverviewDto(DateTime Month, int Day, int DaysInMonth, decimal? OverallLimit, decimal SpentThisMonth,
    IReadOnlyList<BudgetLine> Lines)
{
    public decimal TimeElapsedPercent => (decimal)Day / DaysInMonth * 100;

    // The overall limit when set; otherwise the sum of category limits covering budgeted categories only.
    public decimal? EffectiveLimit => OverallLimit ?? (Lines.Any(l => l.Limit.HasValue) ? Lines.Sum(l => l.Limit ?? 0) : null);
    public decimal EffectiveSpent => OverallLimit.HasValue ? SpentThisMonth : Lines.Where(l => l.Limit.HasValue).Sum(l => l.Spent);
    public decimal? UsedPercent => EffectiveLimit > 0 ? EffectiveSpent / EffectiveLimit * 100 : null;
    public decimal? Remaining => EffectiveLimit - EffectiveSpent;
    public BudgetStatus Status => BudgetRules.StatusFor(UsedPercent);
    public bool HasBudget => EffectiveLimit.HasValue;
    public bool AheadOfPace => UsedPercent is { } used && used <= 100 && used > TimeElapsedPercent + BudgetRules.PaceTolerance;
}
