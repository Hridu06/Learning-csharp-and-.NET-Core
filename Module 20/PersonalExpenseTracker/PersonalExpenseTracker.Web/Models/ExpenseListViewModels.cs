using System.Globalization;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Web.Models;

// Bound from the Expenses list query string: ?q=&categoryId=&from=&to=&min=&max=&sort=&dir=&page=
public class ExpenseListQuery
{
    public const int PageSize = 10;
    private static readonly string[] SortKeys = ["date", "title", "amount", "category"];

    public string? Q { get; set; }
    public int? CategoryId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
    public string? Sort { get; set; }
    public string? Dir { get; set; }
    public int Page { get; set; } = 1;

    public string SortKey => SortKeys.Contains(Sort?.ToLowerInvariant()) ? Sort!.ToLowerInvariant() : "date";
    public bool Descending => Dir is "asc" or "desc" ? Dir == "desc" : DefaultDescending(SortKey);
    public bool HasAdvancedFilters => From.HasValue || To.HasValue || Min.HasValue || Max.HasValue;
    public bool HasFilters => !string.IsNullOrWhiteSpace(Q) || CategoryId.HasValue || HasAdvancedFilters;

    // Dates and amounts sort "biggest/newest first" by default; text columns sort A to Z.
    public static bool DefaultDescending(string sortKey) => sortKey is "date" or "amount";

    public ExpenseSearch ToSearch() =>
        new(Q, CategoryId, From, To, Min, Max, SortKey, Descending, Page, PageSize);

    // Route values for links that keep the current filters while changing sort or page.
    public Dictionary<string, string> RouteData(string? sort = null, bool? descending = null, int? page = null)
    {
        var values = new Dictionary<string, string>();
        if (!string.IsNullOrWhiteSpace(Q)) values["q"] = Q.Trim();
        if (CategoryId.HasValue) values["categoryId"] = CategoryId.Value.ToString(CultureInfo.InvariantCulture);
        if (From.HasValue) values["from"] = From.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        if (To.HasValue) values["to"] = To.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        if (Min.HasValue) values["min"] = Min.Value.ToString(CultureInfo.InvariantCulture);
        if (Max.HasValue) values["max"] = Max.Value.ToString(CultureInfo.InvariantCulture);
        values["sort"] = sort ?? SortKey;
        values["dir"] = (descending ?? Descending) ? "desc" : "asc";
        var targetPage = page ?? Page;
        if (targetPage > 1) values["page"] = targetPage.ToString(CultureInfo.InvariantCulture);
        return values;
    }
}

public record ExpenseIndexViewModel(ExpenseListQuery Query, PagedResult<ExpenseDto> Result,
    IReadOnlyList<CategoryDto> Categories);

// Query is null when the table is shown without sorting (e.g. on the dashboard).
public record ExpenseTableViewModel(IReadOnlyList<ExpenseDto> Items, ExpenseListQuery? Query = null);
