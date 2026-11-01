using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

// Account management only: admins see counts, never other users' expenses or receipts.
public interface IAdminService
{
    Task<AdminDashboardDto> GetDashboardAsync(CancellationToken ct = default);
    Task<OperationResult> SetLockedAsync(string userId, bool locked);
    Task<OperationResult> SetAdminAsync(string userId, bool isAdmin);
}
