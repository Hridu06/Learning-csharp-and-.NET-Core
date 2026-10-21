using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PersonalExpenseTracker.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("The DefaultConnection connection string is missing.");
var webRoot = builder.Environment.WebRootPath ?? Path.Combine(builder.Environment.ContentRootPath, "wwwroot");
builder.Services.AddExpenseTracker(connectionString, Path.Combine(webRoot, "uploads", "receipts"));

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
// Runtime uploads are created after build, so they must use static-file middleware.
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context => context.Context.Response.Headers["X-Content-Type-Options"] = "nosniff"
});
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(name: "default", pattern: "{controller=Summary}/{action=Index}/{id?}")
    .WithStaticAssets();

// Applies Code First migrations (including the four seeded categories) on first run.
await app.Services.InitializeExpenseTrackerAsync();
app.Run();
