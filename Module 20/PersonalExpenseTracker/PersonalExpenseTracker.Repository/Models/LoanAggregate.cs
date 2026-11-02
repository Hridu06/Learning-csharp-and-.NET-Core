using PersonalExpenseTracker.Data.Entities;

namespace PersonalExpenseTracker.Repository.Models;

// A loan with its repayments already summed in the database.
public record LoanAggregate(int Id, LoanType Type, string Person, decimal Principal, DateTime StartDate,
    DateTime? DueDate, string? Notes, decimal Paid, int PaymentCount);
