using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

public interface ILoanService
{
    Task<IReadOnlyList<LoanDto>> GetAllAsync(CancellationToken ct = default);
    Task<LoanSummaryDto> GetSummaryAsync(CancellationToken ct = default);
    Task<LoanDetailDto?> GetDetailAsync(int id, CancellationToken ct = default);
    Task<OperationResult> CreateAsync(LoanInput input, CancellationToken ct = default);
    Task<OperationResult> UpdateAsync(int id, LoanInput input, CancellationToken ct = default);
    Task<OperationResult> DeleteAsync(int id, CancellationToken ct = default);
    Task<OperationResult> AddPaymentAsync(int loanId, LoanPaymentInput input, CancellationToken ct = default);
    Task<OperationResult> DeletePaymentAsync(int loanId, int paymentId, CancellationToken ct = default);
}
