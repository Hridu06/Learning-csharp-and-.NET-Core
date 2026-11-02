using System.ComponentModel.DataAnnotations;
using System.Globalization;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Web.Models;

public class IncomeFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a title.")]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter an amount.")]
    [Range(typeof(decimal), "0.01", "9999999999999999.99", ErrorMessage = "The amount must be greater than zero.")]
    [Display(Name = "Amount (BDT)")]
    public decimal? Amount { get; set; }

    [Required(ErrorMessage = "Please choose a date.")]
    [DataType(DataType.Date)]
    public DateTime? Date { get; set; }

    [Required(ErrorMessage = "Please choose a source.")]
    public IncomeSource? Source { get; set; }

    [StringLength(2000)]
    public string? Notes { get; set; }
}

// Bound from the Income list query string: ?q=&source=&from=&to=&sort=&dir=&page=
public class IncomeListQuery
{
    public const int PageSize = 10;
    private static readonly string[] SortKeys = ["date", "title", "amount", "source"];

    public string? Q { get; set; }
    public IncomeSource? Source { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Sort { get; set; }
    public string? Dir { get; set; }
    public int Page { get; set; } = 1;

    public string SortKey => SortKeys.Contains(Sort?.ToLowerInvariant()) ? Sort!.ToLowerInvariant() : "date";
    public bool Descending => Dir is "asc" or "desc" ? Dir == "desc" : DefaultDescending(SortKey);
    public bool HasFilters => !string.IsNullOrWhiteSpace(Q) || Source.HasValue || From.HasValue || To.HasValue;

    public static bool DefaultDescending(string sortKey) => sortKey is "date" or "amount";

    public IncomeSearch ToSearch() => new(Q, Source, From, To, SortKey, Descending, Page, PageSize);

    // Route values for links that keep the current filters while changing sort or page.
    public Dictionary<string, string> RouteData(string? sort = null, bool? descending = null, int? page = null)
    {
        var values = new Dictionary<string, string>();
        if (!string.IsNullOrWhiteSpace(Q)) values["q"] = Q.Trim();
        if (Source.HasValue) values["source"] = Source.Value.ToString();
        if (From.HasValue) values["from"] = From.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        if (To.HasValue) values["to"] = To.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        values["sort"] = sort ?? SortKey;
        values["dir"] = (descending ?? Descending) ? "desc" : "asc";
        var targetPage = page ?? Page;
        if (targetPage > 1) values["page"] = targetPage.ToString(CultureInfo.InvariantCulture);
        return values;
    }
}

public record IncomeIndexViewModel(IncomeListQuery Query, PagedResult<IncomeDto> Result);
