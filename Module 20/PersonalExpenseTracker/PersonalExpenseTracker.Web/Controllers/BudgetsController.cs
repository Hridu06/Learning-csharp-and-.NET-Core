using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Web.Controllers;

public class BudgetsController(IBudgetService budgets) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct) => View(await budgets.GetOverviewAsync(ct: ct));

    // An empty amount removes the limit.
    [HttpPost]
    public async Task<IActionResult> SetCategory(int categoryId, decimal? amount, CancellationToken ct)
    {
        if (!ModelState.IsValid) return Invalid();
        var result = await budgets.SetCategoryBudgetAsync(categoryId, amount, ct);
        if (result.NotFound) return NotFound();
        return Done(result, amount is null ? "Budget removed." : "Budget saved.", $"category-{categoryId}");
    }

    [HttpPost]
    public async Task<IActionResult> SetOverall(decimal? amount, CancellationToken ct)
    {
        if (!ModelState.IsValid) return Invalid();
        var result = await budgets.SetOverallBudgetAsync(amount, ct);
        return Done(result, amount is null ? "Overall budget removed." : "Overall budget saved.", null);
    }

    // A value that doesn't parse binds as null; without this check it would silently remove the budget.
    private IActionResult Invalid()
    {
        TempData["Error"] = "Please enter a valid amount.";
        return RedirectToAction(nameof(Index));
    }

    private IActionResult Done(OperationResult result, string success, string? fragment)
    {
        TempData[result.Succeeded ? "Success" : "Error"] = result.Succeeded ? success : result.Error;
        return RedirectToAction(nameof(Index), null, fragment);
    }
}
