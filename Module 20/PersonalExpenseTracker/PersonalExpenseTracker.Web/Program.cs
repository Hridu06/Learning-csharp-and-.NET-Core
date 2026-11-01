using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Service;
using PersonalExpenseTracker.Service.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    // Every page requires sign-in unless it opts out with [AllowAnonymous].
    options.Filters.Add(new AuthorizeFilter());
});
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Policies.AdminOnly, policy => policy.RequireRole(AppRoles.Admin));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("The DefaultConnection connection string is missing.");
// Uploads live outside wwwroot so they can only be reached through authorized controller actions.
var dataRoot = Path.Combine(builder.Environment.ContentRootPath, "App_Data");
builder.Services.AddExpenseTracker(connectionString,
    Path.Combine(dataRoot, "receipts"), Path.Combine(dataRoot, "avatars"));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "ExpenseTracker.Auth";
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(14);
    options.SlidingExpiration = true;
});

var app = builder.Build();
var culture = CultureInfo.GetCultureInfo("en-GB");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture),
    SupportedCultures = [culture],
    SupportedUICultures = [culture]
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}
app.UseStatusCodePagesWithReExecute("/Home/Status/{0}");
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context => context.Context.Response.Headers["X-Content-Type-Options"] = "nosniff"
});
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(name: "default", pattern: "{controller=Summary}/{action=Index}/{id?}")
    .WithStaticAssets();

// Applies migrations, creates the roles and finishes setting up the admin account from configuration.
await app.Services.InitializeExpenseTrackerAsync(
    builder.Configuration.GetSection("Admin").Get<AdminSeedOptions>() ?? new AdminSeedOptions());
app.Run();

public static class Policies
{
    public const string AdminOnly = nameof(AdminOnly);
}
