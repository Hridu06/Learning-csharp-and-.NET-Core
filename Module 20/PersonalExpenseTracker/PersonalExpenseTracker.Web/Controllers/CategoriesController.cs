using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;
using PersonalExpenseTracker.Web.Models;

namespace PersonalExpenseTracker.Web.Controllers;

public class CategoriesController(ICategoryService categories) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct) =>
        View(await categories.GetAllAsync(ct));

    public IActionResult Create() => View(new CategoryFormViewModel());

    [HttpPost]
    public async Task<IActionResult> Create(CategoryFormViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid) { TempData["Error"] = "Please correct the highlighted errors."; return View(model); }
        var result = await categories.CreateAsync(new CategoryInput(model.Name, model.Description), ct);
        if (!result.Succeeded)
        {
            TempData["Error"] = "The category could not be saved. Please correct the highlighted errors.";
            ModelState.AddModelError(result.Field ?? string.Empty, result.Error!);
            return View(model);
        }
        TempData["Success"] = "Category added successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var category = await categories.GetByIdAsync(id, ct);
        return category is null ? NotFound() : View(new CategoryFormViewModel
        {
            Id = category.Id, Name = category.Name, Description = category.Description
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, CategoryFormViewModel model, CancellationToken ct)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) { TempData["Error"] = "Please correct the highlighted errors."; return View(model); }
        var result = await categories.UpdateAsync(id, new CategoryInput(model.Name, model.Description), ct);
        if (result.NotFound) return NotFound();
        if (!result.Succeeded)
        {
            TempData["Error"] = "The category could not be saved. Please correct the highlighted errors.";
            ModelState.AddModelError(result.Field ?? string.Empty, result.Error!);
            return View(model);
        }
        TempData["Success"] = "Category updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var category = await categories.GetByIdAsync(id, ct);
        return category is null ? NotFound() : View(category);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken ct)
    {
        var result = await categories.DeleteAsync(id, ct);
        if (result.NotFound) return NotFound();
        TempData[result.Succeeded ? "Success" : "Error"] =
            result.Succeeded ? "Category deleted successfully." : result.Error;
        return RedirectToAction(nameof(Index));
    }
}
