using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Web.Models;

namespace PersonalExpenseTracker.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => RedirectToAction("Index", "Summary");

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
    });
}
