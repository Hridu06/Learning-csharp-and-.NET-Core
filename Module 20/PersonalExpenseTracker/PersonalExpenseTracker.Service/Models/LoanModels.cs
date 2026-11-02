using PersonalExpenseTracker.Data.Entities;

namespace PersonalExpenseTracker.Service.Models;

public enum LoanStatus { Active, Overdue, Settled }

public record LoanDto(int Id, LoanType Type, string Person, decimal Principal, decimal Paid, DateTime StartDate,
    DateTime? DueDate, string? Notes, int PaymentCount, LoanStatus Status)
{
    public decimal Remaining => Math.Max(0, Principal - Paid);
    public decimal PaidPercent => Principal == 0 ? 0 : Math.Min(100, Paid / Principal * 100);
}

public record LoanPaymentDto(int Id, decimal Amount, DateTime Date, string? Notes);
public record LoanDetailDto(LoanDto Loan, IReadOnlyList<LoanPaymentDto> Payments);

public record LoanInput(LoanType Type, string Person, decimal Principal, DateTime StartDate, DateTime? DueDate, string? Notes);
public record LoanPaymentInput(decimal Amount, DateTime Date, string? Notes);

// Outstanding balances only: settled loans don't count.
public record LoanSummaryDto(decimal YouOwe, decimal OwedToYou, int ActiveCount, int OverdueCount, LoanDto? NextDue)
{
    public bool HasLoans => ActiveCount > 0;
}
