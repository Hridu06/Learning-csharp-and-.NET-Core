namespace PersonalExpenseTracker.Data.Entities;

// Fixed list for now; stored by name so adding a source later doesn't renumber existing rows.
public enum IncomeSource { Salary, Freelance, Business, Investment, Gift, Other }

public class Income
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public IncomeSource Source { get; set; }
    public string? Notes { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}
