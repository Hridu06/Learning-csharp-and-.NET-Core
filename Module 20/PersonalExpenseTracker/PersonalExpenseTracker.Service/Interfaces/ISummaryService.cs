using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Interfaces;

public interface ISummaryService
{
    // Any day in the month to show; null or a future month means the current month.
    Task<SummaryDto> GetAsync(DateTime? month = null, CancellationToken ct = default);
}
