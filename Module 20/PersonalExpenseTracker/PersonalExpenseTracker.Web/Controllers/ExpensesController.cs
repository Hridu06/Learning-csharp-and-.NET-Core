using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;
using PersonalExpenseTracker.Web.Models;

namespace PersonalExpenseTracker.Web.Controllers;

public class ExpensesController(IExpenseService expenses, ICategoryService categories, IBudgetService budgets) : Controller
{
    public async Task<IActionResult> Index(ExpenseListQuery query, CancellationToken ct)
    {
        var result = await expenses.SearchAsync(query.ToSearch(), ct);
        query.Page = result.Page;
        return View(new ExpenseIndexViewModel(query, result, await categories.GetAllAsync(ct)));
    }

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
                await WarnIfOverBudgetAsync(model, ct);
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
                await WarnIfOverBudgetAsync(model, ct);
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

    // Confirmation happens in the shared delete modal; returnUrl keeps the user's filters and page.
    [HttpPost]
    public async Task<IActionResult> Delete(int id, string? returnUrl, CancellationToken ct)
    {
        var result = await expenses.DeleteAsync(id, ct);
        if (result.NotFound) return NotFound();
        if (result.Succeeded) FlashSuccess(result, "Expense deleted successfully.");
        else TempData["Error"] = result.Error;
        return Url.IsLocalUrl(returnUrl) ? LocalRedirect(returnUrl) : RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Receipt(int id, CancellationToken ct)
    {
        var expense = await expenses.GetByIdAsync(id, ct);
        if (expense is null || expense.ReceiptFileName is null) return NotFound();
        return View(expense);
    }

    // Receipts are stored outside wwwroot; the service only returns files owned by the current user.
    public async Task<IActionResult> ReceiptImage(int id, CancellationToken ct)
    {
        var file = await expenses.GetReceiptAsync(id, ct);
        if (file is null) return NotFound();
        Response.Headers.CacheControl = "private, max-age=3600";
        return PhysicalFile(file.Path, file.ContentType);
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

    // Appended to any storage warning so both reach the user in one toast.
    private async Task WarnIfOverBudgetAsync(ExpenseFormViewModel model, CancellationToken ct)
    {
        var warning = await budgets.GetOverBudgetWarningAsync(model.CategoryId!.Value, model.Date!.Value, ct);
        if (warning is null) return;
        TempData["Warning"] = TempData["Warning"] is string existing ? $"{existing} {warning}" : warning;
    }
}
