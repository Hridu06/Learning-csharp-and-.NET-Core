using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Repositories;

public class LoanRepository(ExpenseTrackerDbContext context) : RepositoryBase(context), ILoanRepository
{
    public async Task<IReadOnlyList<LoanAggregate>> GetAllAsync(string userId, CancellationToken ct = default) =>
        await Context.Loans.AsNoTracking().Where(l => l.UserId == userId)
            .OrderByDescending(l => l.StartDate).ThenByDescending(l => l.Id)
            .Select(l => new LoanAggregate(l.Id, l.Type, l.Person, l.Principal, l.StartDate, l.DueDate, l.Notes,
                l.Payments.Sum(p => (decimal?)p.Amount) ?? 0m, l.Payments.Count))
            .ToListAsync(ct);

    public Task<Loan?> GetByIdAsync(string userId, int id, CancellationToken ct = default) =>
        Context.Loans.AsNoTracking()
            .Include(l => l.Payments.OrderByDescending(p => p.Date).ThenByDescending(p => p.Id))
            .FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId, ct);

    public async Task<decimal> GetPaidAsync(int loanId, CancellationToken ct = default) =>
        await Context.LoanPayments.Where(p => p.LoanId == loanId).SumAsync(p => (decimal?)p.Amount, ct) ?? 0m;

    public async Task AddAsync(Loan loan, CancellationToken ct = default)
    {
        Context.Loans.Add(loan);
        await SaveAsync(ct);
    }

    public async Task UpdateAsync(Loan loan, CancellationToken ct = default)
    {
        // Only the loan row changes here; payments are added and removed through their own methods.
        Context.Entry(loan).State = EntityState.Modified;
        await SaveAsync(ct);
    }

    public async Task DeleteAsync(Loan loan, CancellationToken ct = default)
    {
        // The database cascades the delete to the loan's payments.
        await Context.Loans.Where(l => l.Id == loan.Id && l.UserId == loan.UserId).ExecuteDeleteAsync(ct);
    }

    public async Task AddPaymentAsync(LoanPayment payment, CancellationToken ct = default)
    {
        Context.LoanPayments.Add(payment);
        await SaveAsync(ct);
    }

    public async Task<bool> DeletePaymentAsync(string userId, int loanId, int paymentId, CancellationToken ct = default) =>
        await Context.LoanPayments
            .Where(p => p.Id == paymentId && p.LoanId == loanId && p.Loan.UserId == userId)
            .ExecuteDeleteAsync(ct) > 0;
}
