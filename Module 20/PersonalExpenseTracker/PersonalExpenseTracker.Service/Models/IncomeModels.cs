using PersonalExpenseTracker.Data.Entities;

namespace PersonalExpenseTracker.Service.Models;

public record IncomeDto(int Id, string Title, decimal Amount, DateTime Date, IncomeSource Source, string? Notes);
public record IncomeInput(string Title, decimal Amount, DateTime Date, IncomeSource Source, string? Notes);

// Sort is one of "date", "title", "amount" or "source"; unknown values fall back to date.
public record IncomeSearch(string? Search = null, IncomeSource? Source = null, DateTime? From = null, DateTime? To = null,
    string? Sort = null, bool Descending = true, int Page = 1, int PageSize = 10);

// How each fixed income source is labelled and drawn in the UI.
public record IncomeSourceInfo(IncomeSource Source, string Label, string Icon, string Color);

public static class IncomeSources
{
    public static readonly IReadOnlyList<IncomeSourceInfo> All =
    [
        new(IncomeSource.Salary, "Salary", "briefcase", "#16A34A"),
        new(IncomeSource.Freelance, "Freelance", "laptop", "#0EA5E9"),
        new(IncomeSource.Business, "Business", "shop", "#F59E0B"),
        new(IncomeSource.Investment, "Investment", "graph-up-arrow", "#8B5CF6"),
        new(IncomeSource.Gift, "Gift", "gift", "#EC4899"),
        new(IncomeSource.Other, "Other", "three-dots", "#64748B")
    ];

    public static IncomeSourceInfo Get(IncomeSource source) => All.First(s => s.Source == source);
}
