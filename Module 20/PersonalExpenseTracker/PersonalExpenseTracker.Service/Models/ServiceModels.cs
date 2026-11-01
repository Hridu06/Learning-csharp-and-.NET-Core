namespace PersonalExpenseTracker.Service.Models;

public record CategoryDto(int Id, string Name, string? Description, string Color, string Icon,
    int ExpenseCount, decimal Total);
public record CategoryInput(string Name, string? Description, string Color, string Icon);
public record ExpenseDto(int Id, string Title, decimal Amount, DateTime Date, string? Notes,
    string? ReceiptFileName, int CategoryId, string CategoryName, string CategoryColor, string CategoryIcon);
public record ExpenseInput(string Title, decimal Amount, DateTime Date, int CategoryId, string? Notes);
public record ReceiptUpload(string FileName, long Length, Stream Content);

// Sort is one of "date", "title", "amount" or "category"; unknown values fall back to date.
public record ExpenseSearch(string? Search = null, int? CategoryId = null, DateTime? From = null, DateTime? To = null,
    decimal? MinAmount = null, decimal? MaxAmount = null, string? Sort = null, bool Descending = true,
    int Page = 1, int PageSize = 10);
public record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, decimal TotalAmount, int Page, int PageSize)
{
    public int TotalPages => Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));
}

public record MonthTotalDto(DateTime Month, decimal Total);
public record SummaryDto(DateTime Month, decimal CurrentMonthTotal, decimal PreviousMonthTotal, decimal TodayTotal,
    IReadOnlyList<CategoryDto> Categories, IReadOnlyList<ExpenseDto> RecentExpenses, IReadOnlyList<MonthTotalDto> MonthlyTotals)
{
    public decimal AllTimeTotal => Categories.Sum(c => c.Total);
    public int ExpenseCount => Categories.Sum(c => c.ExpenseCount);
    public CategoryDto? TopCategory => Categories.Where(c => c.Total > 0).MaxBy(c => c.Total);

    // Null when there is nothing to compare against.
    public decimal? MonthOverMonthChange => PreviousMonthTotal == 0 ? null
        : (CurrentMonthTotal - PreviousMonthTotal) / PreviousMonthTotal * 100;
}

public static class CategoryAppearance
{
    public const string DefaultColor = "#6366F1";
    public const string DefaultIcon = "tag";

    public static readonly IReadOnlyList<string> Colors =
    [
        "#6366F1", "#0EA5E9", "#14B8A6", "#22C55E", "#84CC16", "#EAB308",
        "#F97316", "#EF4444", "#EC4899", "#A855F7", "#64748B", "#78716C"
    ];

    // Bootstrap Icons names (without the "bi-" prefix).
    public static readonly IReadOnlyList<string> Icons =
    [
        "tag", "basket", "cart", "cup-hot", "egg-fried", "bus-front", "car-front", "fuel-pump",
        "airplane", "lightning-charge", "droplet", "wifi", "phone", "house", "film", "controller",
        "music-note", "heart-pulse", "capsule", "book", "mortarboard", "bag", "gift", "briefcase",
        "piggy-bank", "credit-card", "tools", "three-dots"
    ];
}

public record OperationResult(bool Succeeded, string? Error = null, string? Field = null,
    bool NotFound = false, string? Warning = null)
{
    public static OperationResult Success(string? warning = null) => new(true, Warning: warning);
    public static OperationResult Fail(string error, string? field = null) => new(false, error, field);
    public static OperationResult Missing() => new(false, "The requested record no longer exists.", NotFound: true);
}
