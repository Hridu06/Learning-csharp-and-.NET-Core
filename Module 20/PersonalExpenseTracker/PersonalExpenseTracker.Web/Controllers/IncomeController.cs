using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;
using PersonalExpenseTracker.Web.Models;

namespace PersonalExpenseTracker.Web.Controllers;

public class IncomeController(IIncomeService incomes) : Controller
{
    public async Task<IActionResult> Index(IncomeListQuery query, CancellationToken ct)
    {
        var result = await incomes.SearchAsync(query.ToSearch(), ct);
        query.Page = result.Page;
        return View(new IncomeIndexViewModel(query, result));
    }

    public IActionResult Create() => View(new IncomeFormViewModel { Date = DateTime.Today });

    [HttpPost]
    public async Task<IActionResult> Create(IncomeFormViewModel model, CancellationToken ct)
    {
        if (ModelState.IsValid)
        {
            var result = await incomes.CreateAsync(ToInput(model), ct);
            if (result.Succeeded)
            {
                TempData["Success"] = "Income added successfully.";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(result.Field ?? string.Empty, result.Error!);
        }
        TempData["Error"] = "Please correct the highlighted errors.";
        return View(model);
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var income = await incomes.GetByIdAsync(id, ct);
        return income is null ? NotFound() : View(new IncomeFormViewModel
        {
            Id = income.Id, Title = income.Title, Amount = income.Amount, Date = income.Date,
            Source = income.Source, Notes = income.Notes
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, IncomeFormViewModel model, CancellationToken ct)
    {
        if (id != model.Id) return BadRequest();
        if (ModelState.IsValid)
        {
            var result = await incomes.UpdateAsync(id, ToInput(model), ct);
            if (result.NotFound) return NotFound();
            if (result.Succeeded)
            {
                TempData["Success"] = "Income updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(result.Field ?? string.Empty, result.Error!);
        }
        TempData["Error"] = "Please correct the highlighted errors.";
        return View(model);
    }

    // Confirmation happens in the shared delete modal; returnUrl keeps the user's filters and page.
    [HttpPost]
    public async Task<IActionResult> Delete(int id, string? returnUrl, CancellationToken ct)
    {
        var result = await incomes.DeleteAsync(id, ct);
        if (result.NotFound) return NotFound();
        TempData["Success"] = "Income deleted successfully.";
        return Url.IsLocalUrl(returnUrl) ? LocalRedirect(returnUrl) : RedirectToAction(nameof(Index));
    }

    private static IncomeInput ToInput(IncomeFormViewModel model) =>
        new(model.Title, model.Amount!.Value, model.Date!.Value, model.Source!.Value, model.Notes);
}
