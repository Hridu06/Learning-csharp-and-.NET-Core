using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Service.Interfaces;

namespace PersonalExpenseTracker.Web.Controllers;

public class SummaryController(ISummaryService summary) : Controller
{
    // ?month=2026-10 shows October 2026; anything missing or unparseable shows the current month.
    public async Task<IActionResult> Index(string? month, CancellationToken ct)
    {
        DateTime? selected = DateTime.TryParseExact(month, "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed)
            ? parsed
            : null;
        return View(await summary.GetAsync(selected, ct));
    }
}
