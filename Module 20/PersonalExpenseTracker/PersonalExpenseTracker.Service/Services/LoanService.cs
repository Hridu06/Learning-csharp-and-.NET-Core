using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Repository.Models;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

public class LoanService(ILoanRepository loans, ICurrentUser currentUser, TimeProvider clock) : ILoanService
{
    public async Task<IReadOnlyList<LoanDto>> GetAllAsync(CancellationToken ct = default) =>
        (await loans.GetAllAsync(UserId, ct)).Select(Map).ToList();

    public async Task<LoanSummaryDto> GetSummaryAsync(CancellationToken ct = default)
    {
        var open = (await GetAllAsync(ct)).Where(l => l.Status != LoanStatus.Settled).ToList();
        var nextDue = open.Where(l => l.DueDate.HasValue).OrderBy(l => l.DueDate).FirstOrDefault();
        return new LoanSummaryDto(
            open.Where(l => l.Type == LoanType.Borrowed).Sum(l => l.Remaining),
            open.Where(l => l.Type == LoanType.Lent).Sum(l => l.Remaining),
            open.Count, open.Count(l => l.Status == LoanStatus.Overdue), nextDue);
    }

    public async Task<LoanDetailDto?> GetDetailAsync(int id, CancellationToken ct = default)
    {
        var loan = await loans.GetByIdAsync(UserId, id, ct);
        if (loan is null) return null;
        var paid = loan.Payments.Sum(p => p.Amount);
        var dto = Map(new LoanAggregate(loan.Id, loan.Type, loan.Person, loan.Principal, loan.StartDate, loan.DueDate,
            loan.Notes, paid, loan.Payments.Count));
        return new LoanDetailDto(dto, loan.Payments.Select(p => new LoanPaymentDto(p.Id, p.Amount, p.Date, p.Notes)).ToList());
    }

    public async Task<OperationResult> CreateAsync(LoanInput input, CancellationToken ct = default)
    {
        var validation = Validate(input);
        if (validation is not null) return validation;
        await loans.AddAsync(new Loan
        {
            UserId = UserId, Type = input.Type, Person = input.Person.Trim(), Principal = input.Principal,
            StartDate = input.StartDate.Date, DueDate = input.DueDate?.Date, Notes = Clean(input.Notes)
        }, ct);
        return OperationResult.Success();
    }

    public async Task<OperationResult> UpdateAsync(int id, LoanInput input, CancellationToken ct = default)
    {
        var loan = await loans.GetByIdAsync(UserId, id, ct);
        if (loan is null) return OperationResult.Missing();
        var validation = Validate(input);
        if (validation is not null) return validation;
        var paid = loan.Payments.Sum(p => p.Amount);
        if (input.Principal < paid)
            return OperationResult.Fail($"The amount can't be less than what has already been repaid (৳ {paid:N2}).", "Principal");

        loan.Type = input.Type;
        loan.Person = input.Person.Trim();
        loan.Principal = input.Principal;
        loan.StartDate = input.StartDate.Date;
        loan.DueDate = input.DueDate?.Date;
        loan.Notes = Clean(input.Notes);
        await loans.UpdateAsync(loan, ct);
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var loan = await loans.GetByIdAsync(UserId, id, ct);
        if (loan is null) return OperationResult.Missing();
        await loans.DeleteAsync(loan, ct);
        return OperationResult.Success();
    }

    public async Task<OperationResult> AddPaymentAsync(int loanId, LoanPaymentInput input, CancellationToken ct = default)
    {
        var loan = await loans.GetByIdAsync(UserId, loanId, ct);
        if (loan is null) return OperationResult.Missing();
        if (input.Amount <= 0 || decimal.Round(input.Amount, 2) != input.Amount)
            return OperationResult.Fail("Enter a payment greater than zero with at most two decimal places.", "Amount");
        if (input.Date == default)
            return OperationResult.Fail("Please choose a valid date.", "Date");
        if (input.Date.Date < loan.StartDate)
            return OperationResult.Fail("A payment can't be dated before the loan started.", "Date");
        if (input.Notes?.Length > 500)
            return OperationResult.Fail("Notes cannot exceed 500 characters.", "Notes");

        var remaining = loan.Principal - await loans.GetPaidAsync(loanId, ct);
        if (input.Amount > remaining)
            return OperationResult.Fail($"The payment can't be more than what's left (৳ {remaining:N2}).", "Amount");

        await loans.AddPaymentAsync(new LoanPayment
        {
            LoanId = loanId, Amount = input.Amount, Date = input.Date.Date, Notes = Clean(input.Notes)
        }, ct);
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeletePaymentAsync(int loanId, int paymentId, CancellationToken ct = default) =>
        await loans.DeletePaymentAsync(UserId, loanId, paymentId, ct) ? OperationResult.Success() : OperationResult.Missing();

    private LoanDto Map(LoanAggregate l)
    {
        var today = clock.GetLocalNow().Date;
        var status = l.Paid >= l.Principal ? LoanStatus.Settled
            : l.DueDate < today ? LoanStatus.Overdue
            : LoanStatus.Active;
        return new LoanDto(l.Id, l.Type, l.Person, l.Principal, l.Paid, l.StartDate, l.DueDate, l.Notes, l.PaymentCount, status);
    }

    private static OperationResult? Validate(LoanInput input)
    {
        if (!Enum.IsDefined(input.Type))
            return OperationResult.Fail("Choose whether you borrowed or lent the money.", "Type");
        if (string.IsNullOrWhiteSpace(input.Person) || input.Person.Trim().Length > 100)
            return OperationResult.Fail("Enter a name of 1 to 100 characters.", "Person");
        if (input.Principal <= 0 || input.Principal > 9999999999999999.99m || decimal.Round(input.Principal, 2) != input.Principal)
            return OperationResult.Fail("Enter an amount greater than zero with at most two decimal places.", "Principal");
        if (input.StartDate == default)
            return OperationResult.Fail("Please choose a valid date.", "StartDate");
        if (input.DueDate.HasValue && input.DueDate.Value.Date < input.StartDate.Date)
            return OperationResult.Fail("The due date can't be before the start date.", "DueDate");
        if (input.Notes?.Length > 2000)
            return OperationResult.Fail("Notes cannot exceed 2000 characters.", "Notes");
        return null;
    }

    private string UserId => currentUser.UserId;

    private static string? Clean(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
