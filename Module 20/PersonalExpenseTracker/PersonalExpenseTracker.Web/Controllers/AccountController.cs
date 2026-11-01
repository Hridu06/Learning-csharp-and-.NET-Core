using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;
using PersonalExpenseTracker.Web.Models;

namespace PersonalExpenseTracker.Web.Controllers;

[AllowAnonymous]
public class AccountController(IAccountService accounts) : Controller
{
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToLocal(returnUrl);
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid) return View(model);

        var outcome = await accounts.LoginAsync(model.Email, model.Password, model.RememberMe);
        if (outcome == LoginOutcome.Succeeded) return RedirectToLocal(returnUrl);
        // One message for unknown email and wrong password, so accounts can't be probed.
        ModelState.AddModelError(string.Empty, outcome switch
        {
            LoginOutcome.LockedOut => "This account is locked. Try again in a few minutes or contact the administrator.",
            LoginOutcome.NotAllowed => "This account is not allowed to sign in yet.",
            _ => "Invalid email or password."
        });
        return View(model);
    }

    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Summary");
        return View(new RegisterViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await accounts.RegisterAsync(new RegisterInput(model.FullName, model.Email, model.Password), ct);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(result.Field ?? string.Empty, result.Error!);
            return View(model);
        }
        TempData["Success"] = $"Welcome, {model.FullName.Trim()}! We've added a few starter categories for you.";
        return RedirectToAction("Index", "Summary");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await accounts.LogoutAsync();
        TempData["Success"] = "You have been signed out.";
        return RedirectToAction(nameof(Login));
    }

    public IActionResult AccessDenied() => View();

    private IActionResult RedirectToLocal(string? returnUrl) =>
        Url.IsLocalUrl(returnUrl) ? LocalRedirect(returnUrl) : RedirectToAction("Index", "Summary");
}
