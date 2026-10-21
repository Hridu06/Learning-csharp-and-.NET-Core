namespace PersonalExpenseTracker.Service.Models;

public record CategoryDto(int Id, string Name, string? Description, int ExpenseCount, decimal Total);
public record CategoryInput(string Name, string? Description);
public record ExpenseDto(int Id, string Title, decimal Amount, DateTime Date, string? Notes,
    string? ReceiptFileName, int CategoryId, string CategoryName);
public record ExpenseInput(string Title, decimal Amount, DateTime Date, int CategoryId, string? Notes);
public record ReceiptUpload(string FileName, long Length, Stream Content);
public record SummaryDto(DateTime Month, decimal CurrentMonthTotal,
    IReadOnlyList<CategoryDto> Categories, IReadOnlyList<ExpenseDto> RecentExpenses);

public record OperationResult(bool Succeeded, string? Error = null, string? Field = null,
    bool NotFound = false, string? Warning = null)
{
    public static OperationResult Success(string? warning = null) => new(true, Warning: warning);
    public static OperationResult Fail(string error, string? field = null) => new(false, error, field);
    public static OperationResult Missing() => new(false, "The requested record no longer exists.", NotFound: true);
}
