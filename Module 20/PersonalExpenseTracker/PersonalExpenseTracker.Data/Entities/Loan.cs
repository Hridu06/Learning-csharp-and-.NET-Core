namespace PersonalExpenseTracker.Data.Entities;

// Borrowed: the user owes Person. Lent: Person owes the user. Stored by name.
public enum LoanType { Borrowed, Lent }

// Loans are tracked separately from income and expenses: borrowing is not income,
// and repaying is not spending.
public class Loan
{
    public int Id { get; set; }
    public LoanType Type { get; set; }
    public string Person { get; set; } = string.Empty;
    public decimal Principal { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Notes { get; set; }
    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    public ICollection<LoanPayment> Payments { get; set; } = new List<LoanPayment>();
}

public class LoanPayment
{
    public int Id { get; set; }
    public int LoanId { get; set; }
    public Loan Loan { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string? Notes { get; set; }
}
