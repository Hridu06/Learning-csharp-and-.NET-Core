using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;
using PersonalExpenseTracker.Web.Models;

namespace PersonalExpenseTracker.Web.Controllers;

public class ProfileController(IAccountService accounts) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var account = await accounts.GetProfileAsync(ct);
        return View(new ProfilePageViewModel
        {
            Account = account,
            Profile = new ProfileFormModel { FullName = account.FullName, Email = account.Email }
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update([Bind(Prefix = "Profile")] ProfileFormModel model, CancellationToken ct)
    {
        if (ModelState.IsValid)
        {
            await using var stream = model.Avatar?.OpenReadStream();
            var upload = model.Avatar is null || stream is null ? null : new ReceiptUpload(model.Avatar.FileName, model.Avatar.Length, stream);
            var result = await accounts.UpdateProfileAsync(
                new ProfileInput(model.FullName, model.Email, model.CurrentPassword, model.RemoveAvatar), upload, ct);
            if (result.Succeeded)
            {
                TempData["Success"] = "Your profile has been updated.";
                return RedirectToAction(nameof(Index));
            }
            AddError("Profile", result);
        }
        return await RedisplayAsync("profile", ct, profile: model);
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword([Bind(Prefix = "Password")] ChangePasswordModel model, CancellationToken ct)
    {
        if (ModelState.IsValid)
        {
            var result = await accounts.ChangePasswordAsync(model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                TempData["Success"] = "Your password has been changed.";
                return RedirectToAction(nameof(Index));
            }
            AddError("Password", result);
        }
        return await RedisplayAsync("password", ct);
    }

    [HttpPost]
    public async Task<IActionResult> Delete([Bind(Prefix = "Delete")] DeleteAccountModel model, CancellationToken ct)
    {
        if (ModelState.IsValid)
        {
            var result = await accounts.DeleteAccountAsync(model.Password, ct);
            if (result.Succeeded)
            {
                TempData["Success"] = "Your account and all of its data have been deleted.";
                return RedirectToAction("Login", "Account");
            }
            AddError("Delete", result);
        }
        return await RedisplayAsync("delete", ct);
    }

    // Served through the controller because avatars are stored outside wwwroot.
    public async Task<IActionResult> Avatar(CancellationToken ct)
    {
        var file = await accounts.GetAvatarAsync(ct);
        if (file is null) return NotFound();
        Response.Headers.CacheControl = "private, max-age=86400";
        return PhysicalFile(file.Path, file.ContentType);
    }

    private void AddError(string prefix, OperationResult result) =>
        ModelState.AddModelError(result.Field is null ? string.Empty : $"{prefix}.{result.Field}", result.Error!);

    private async Task<IActionResult> RedisplayAsync(string section, CancellationToken ct, ProfileFormModel? profile = null)
    {
        var account = await accounts.GetProfileAsync(ct);
        TempData["Error"] = "Please correct the highlighted errors.";
        return View(nameof(Index), new ProfilePageViewModel
        {
            Account = account,
            Profile = profile ?? new ProfileFormModel { FullName = account.FullName, Email = account.Email },
            ActiveSection = section
        });
    }
}
