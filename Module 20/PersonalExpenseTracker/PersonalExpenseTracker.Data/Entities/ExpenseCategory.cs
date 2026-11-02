namespace PersonalExpenseTracker.Data.Entities;

public class ExpenseCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    // Hex colour (#RRGGBB) and Bootstrap Icons name used to style the category in the UI.
    public string Color { get; set; } = "#6366F1";
    public string Icon { get; set; } = "tag";
    // Spending limit applied to every month; null means no budget for this category.
    public decimal? MonthlyBudget { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
