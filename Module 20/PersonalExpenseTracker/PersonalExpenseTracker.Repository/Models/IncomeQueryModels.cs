using PersonalExpenseTracker.Data.Entities;

namespace PersonalExpenseTracker.Repository.Models;

public enum IncomeSortField { Date, Title, Amount, Source }

public record IncomeFilter(string? Search, IncomeSource? Source, DateTime? From, DateTime? To,
    IncomeSortField Sort, bool Descending, int Page, int PageSize);

// Page is the page actually returned, clamped to the available range.
public record IncomePage(IReadOnlyList<Income> Items, int TotalCount, decimal TotalAmount, int Page);
