using PersonalExpenseTracker.Data.Entities;

namespace PersonalExpenseTracker.Repository.Models;

public enum ExpenseSortField { Date, Title, Amount, Category }

public record ExpenseFilter(string? Search, int? CategoryId, DateTime? From, DateTime? To,
    decimal? MinAmount, decimal? MaxAmount, ExpenseSortField Sort, bool Descending, int Page, int PageSize);

// Page is the page actually returned, clamped to the available range.
public record ExpensePage(IReadOnlyList<Expense> Items, int TotalCount, decimal TotalAmount, int Page);

public record MonthlyTotal(int Year, int Month, decimal Total);
