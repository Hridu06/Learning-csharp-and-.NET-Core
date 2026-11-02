using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;
using PersonalExpenseTracker.Web.Models;

namespace PersonalExpenseTracker.Web.Controllers;

public class LoansController(ILoanService loans) : Controller
{
    public async Task<IActionResult> Index(string? tab, CancellationToken ct)
    {
        var all = await loans.GetAllAsync(ct);
        tab = tab is "borrowed" or "lent" or "settled" ? tab : "active";
        var shown = tab switch
        {
            "borrowed" => all.Where(l => l.Type == LoanType.Borrowed && l.Status != LoanStatus.Settled),
            "lent" => all.Where(l => l.Type == LoanType.Lent && l.Status != LoanStatus.Settled),
            "settled" => all.Where(l => l.Status == LoanStatus.Settled),
            _ => all.Where(l => l.Status != LoanStatus.Settled)
        };
        // Overdue first, then the soonest due date, then loans without a due date.
        var ordered = shown.OrderBy(l => l.Status == LoanStatus.Overdue ? 0 : 1)
            .ThenBy(l => l.DueDate ?? DateTime.MaxValue).ToList();
        return View(new LoansIndexViewModel(ordered, await loans.GetSummaryAsync(ct), tab,
            all.Count(l => l.Status == LoanStatus.Settled)));
    }

    public IActionResult Create(LoanType? type) =>
        View(new LoanFormViewModel { Type = type, StartDate = DateTime.Today });

    [HttpPost]
    public async Task<IActionResult> Create(LoanFormViewModel model, CancellationToken ct)
    {
        if (ModelState.IsValid)
        {
            var result = await loans.CreateAsync(ToInput(model), ct);
            if (result.Succeeded)
            {
                TempData["Success"] = "Loan added successfully.";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(result.Field ?? string.Empty, result.Error!);
        }
        TempData["Error"] = "Please correct the highlighted errors.";
        return View(model);
    }

    public async Task<IActionResult> Details(int id, CancellationToken ct)
    {
        var detail = await loans.GetDetailAsync(id, ct);
        return detail is null ? NotFound() : View(new LoanDetailsViewModel(detail, new LoanPaymentFormModel { Date = DateTime.Today }));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken ct)
    {
        var detail = await loans.GetDetailAsync(id, ct);
        if (detail is null) return NotFound();
        var loan = detail.Loan;
        return View(new LoanFormViewModel
        {
            Id = loan.Id, Type = loan.Type, Person = loan.Person, Principal = loan.Principal,
            StartDate = loan.StartDate, DueDate = loan.DueDate, Notes = loan.Notes
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, LoanFormViewModel model, CancellationToken ct)
    {
        if (id != model.Id) return BadRequest();
        if (ModelState.IsValid)
        {
            var result = await loans.UpdateAsync(id, ToInput(model), ct);
            if (result.NotFound) return NotFound();
            if (result.Succeeded)
            {
                TempData["Success"] = "Loan updated successfully.";
                return RedirectToAction(nameof(Details), new { id });
            }
            ModelState.AddModelError(result.Field ?? string.Empty, result.Error!);
        }
        TempData["Error"] = "Please correct the highlighted errors.";
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddPayment(int id, [Bind(Prefix = "Payment")] LoanPaymentFormModel model, CancellationToken ct)
    {
        if (ModelState.IsValid)
        {
            var result = await loans.AddPaymentAsync(id, new LoanPaymentInput(model.Amount!.Value, model.Date!.Value, model.Notes), ct);
            if (result.NotFound) return NotFound();
            if (result.Succeeded)
            {
                TempData["Success"] = "Payment recorded.";
                return RedirectToAction(nameof(Details), new { id });
            }
            ModelState.AddModelError(result.Field is null ? string.Empty : $"Payment.{result.Field}", result.Error!);
        }
        var detail = await loans.GetDetailAsync(id, ct);
        if (detail is null) return NotFound();
        TempData["Error"] = "Please correct the highlighted errors.";
        return View(nameof(Details), new LoanDetailsViewModel(detail, model));
    }

    // Opened from the shared delete modal, which posts to this URL with ?paymentId=.
    [HttpPost]
    public async Task<IActionResult> DeletePayment(int id, int paymentId, CancellationToken ct)
    {
        var result = await loans.DeletePaymentAsync(id, paymentId, ct);
        if (result.NotFound) return NotFound();
        TempData["Success"] = "Payment removed.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await loans.DeleteAsync(id, ct);
        if (result.NotFound) return NotFound();
        TempData["Success"] = "Loan deleted.";
        return RedirectToAction(nameof(Index));
    }

    private static LoanInput ToInput(LoanFormViewModel model) =>
        new(model.Type!.Value, model.Person, model.Principal!.Value, model.StartDate!.Value, model.DueDate, model.Notes);
}
