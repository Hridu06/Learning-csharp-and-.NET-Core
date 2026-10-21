using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;
using PersonalExpenseTracker.Web.Models;

namespace PersonalExpenseTracker.Web.Controllers;

public class ExpensesController(IExpenseService expenses, ICategoryService categories) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct) => View(await expenses.GetAllAsync(ct));

    public async Task<IActionResult> Create(CancellationToken ct)
    {
        var model = new ExpenseFormViewModel { Date = DateTime.Today };
        await LoadCategoriesAsync(model, ct);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ExpenseFormViewModel model, CancellationToken ct)
    {
        if (ModelState.IsValid)
        {
            await using var stream = model.Receipt?.OpenReadStream();
            var result = await expenses.CreateAsync(ToInput(model), ToUpload(model.Receipt, stream), ct);
            if (result.Succeeded)
            {
                FlashSuccess(result, "Expense added successfully.");
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "The expense could not be saved. Please correct the highlighted errors.";
            ModelState.AddModelError(result.Field ?? string.Empty, result.Error!);
        }
        TempData["Error"] ??= "Please correct the highlighted errors.";
        await LoadCategoriesAsync(model, ct);
        return View(model);
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var expense = await expenses.GetByIdAsync(id, ct);
        if (expense is null) return NotFound();
        var model = new ExpenseFormViewModel
        {
            Id = expense.Id, Title = expense.Title, Amount = expense.Amount, Date = expense.Date,
            CategoryId = expense.CategoryId, Notes = expense.Notes,
            ExistingReceiptFileName = expense.ReceiptFileName
        };
        await LoadCategoriesAsync(model, ct);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ExpenseFormViewModel model, CancellationToken ct)
    {
        if (id != model.Id) return BadRequest();
        if (ModelState.IsValid)
        {
            await using var stream = model.Receipt?.OpenReadStream();
            var result = await expenses.UpdateAsync(id, ToInput(model), ToUpload(model.Receipt, stream), ct);
            if (result.NotFound) return NotFound();
            if (result.Succeeded)
            {
                FlashSuccess(result, "Expense updated successfully.");
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "The expense could not be saved. Please correct the highlighted errors.";
            ModelState.AddModelError(result.Field ?? string.Empty, result.Error!);
        }
        var expense = await expenses.GetByIdAsync(id, ct);
        if (expense is null) return NotFound();
        model.ExistingReceiptFileName = expense.ReceiptFileName;
        TempData["Error"] ??= "Please correct the highlighted errors.";
        await LoadCategoriesAsync(model, ct);
        return View(model);
    }

    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var expense = await expenses.GetByIdAsync(id, ct);
        return expense is null ? NotFound() : View(expense);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var result = await expenses.DeleteAsync(id, ct);
        if (result.NotFound) return NotFound();
        if (result.Succeeded) FlashSuccess(result, "Expense deleted successfully.");
        else TempData["Error"] = result.Error;
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Receipt(int id, CancellationToken ct)
    {
        var expense = await expenses.GetByIdAsync(id, ct);
        if (expense is null || expense.ReceiptFileName is null) return NotFound();
        return View(expense);
    }

    private async Task LoadCategoriesAsync(ExpenseFormViewModel model, CancellationToken ct)
    {
        model.Categories = (await categories.GetAllAsync(ct))
            .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
    }

    private static ExpenseInput ToInput(ExpenseFormViewModel model) =>
        new(model.Title, model.Amount!.Value, model.Date!.Value, model.CategoryId!.Value, model.Notes);

    private static ReceiptUpload? ToUpload(IFormFile? file, Stream? stream) =>
        file is null || stream is null ? null : new(file.FileName, file.Length, stream);

    private void FlashSuccess(OperationResult result, string message)
    {
        TempData["Success"] = message;
        if (result.Warning is not null) TempData["Warning"] = result.Warning;
    }
}
