using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Service.Interfaces;

namespace PersonalExpenseTracker.Web.Controllers;

public class SummaryController(ISummaryService summary) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct) => View(await summary.GetAsync(ct));
}
