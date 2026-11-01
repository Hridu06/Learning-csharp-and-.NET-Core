using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Web.Controllers;

[Authorize(Policy = Policies.AdminOnly)]
public class AdminController(IAdminService admin) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct) => View(await admin.GetDashboardAsync(ct));

    [HttpPost]
    public async Task<IActionResult> Lock(string id) =>
        Done(await admin.SetLockedAsync(id, true), "The account has been locked.");

    [HttpPost]
    public async Task<IActionResult> Unlock(string id) =>
        Done(await admin.SetLockedAsync(id, false), "The account has been unlocked.");

    [HttpPost]
    public async Task<IActionResult> MakeAdmin(string id) =>
        Done(await admin.SetAdminAsync(id, true), "The user is now an administrator.");

    [HttpPost]
    public async Task<IActionResult> RemoveAdmin(string id) =>
        Done(await admin.SetAdminAsync(id, false), "Administrator access has been removed.");

    private IActionResult Done(OperationResult result, string success)
    {
        if (result.NotFound) return NotFound();
        TempData[result.Succeeded ? "Success" : "Error"] = result.Succeeded ? success : result.Error;
        return RedirectToAction(nameof(Index));
    }
}
