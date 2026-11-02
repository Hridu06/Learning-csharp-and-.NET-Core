using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Interfaces;

// Every read is scoped to the owning user; ids from other users behave as if they don't exist.
public interface ILoanRepository
{
    Task<IReadOnlyList<LoanAggregate>> GetAllAsync(string userId, CancellationToken ct = default);
    // Includes the payments, newest first.
    Task<Loan?> GetByIdAsync(string userId, int id, CancellationToken ct = default);
    Task<decimal> GetPaidAsync(int loanId, CancellationToken ct = default);
    Task AddAsync(Loan loan, CancellationToken ct = default);
    Task UpdateAsync(Loan loan, CancellationToken ct = default);
    Task DeleteAsync(Loan loan, CancellationToken ct = default);
    Task AddPaymentAsync(LoanPayment payment, CancellationToken ct = default);
    // Returns false when the payment doesn't exist on this user's loan.
    Task<bool> DeletePaymentAsync(string userId, int loanId, int paymentId, CancellationToken ct = default);
}
