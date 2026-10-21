namespace PersonalExpenseTracker.Data.Entities;

public class Expense
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
    public string? ReceiptFileName { get; set; }
    public int CategoryId { get; set; }
    public ExpenseCategory Category { get; set; } = null!;
}
