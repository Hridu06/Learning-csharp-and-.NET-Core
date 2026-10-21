using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

public interface ISummaryService
{
    Task<SummaryDto> GetAsync(CancellationToken ct = default);
}
