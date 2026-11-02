using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Repository.Models;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

public class IncomeService(IIncomeRepository incomes, ICurrentUser currentUser) : IIncomeService
{
    public async Task<PagedResult<IncomeDto>> SearchAsync(IncomeSearch search, CancellationToken ct = default)
    {
        var sort = search.Sort?.ToLowerInvariant() switch
        {
            "title" => IncomeSortField.Title,
            "amount" => IncomeSortField.Amount,
            "source" => IncomeSortField.Source,
            _ => IncomeSortField.Date
        };
        var pageSize = Math.Clamp(search.PageSize, 5, 100);
        var page = await incomes.SearchAsync(UserId, new IncomeFilter(search.Search, search.Source, search.From, search.To,
            sort, search.Descending, search.Page, pageSize), ct);
        return new PagedResult<IncomeDto>(page.Items.Select(Map).ToList(), page.TotalCount, page.TotalAmount, page.Page, pageSize);
    }

    public async Task<IncomeDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var income = await incomes.GetByIdAsync(UserId, id, ct);
        return income is null ? null : Map(income);
    }

    public async Task<OperationResult> CreateAsync(IncomeInput input, CancellationToken ct = default)
    {
        var validation = Validate(input);
        if (validation is not null) return validation;
        await incomes.AddAsync(new Income
        {
            UserId = UserId, Title = input.Title.Trim(), Amount = input.Amount, Date = input.Date.Date,
            Source = input.Source, Notes = Clean(input.Notes)
        }, ct);
        return OperationResult.Success();
    }

    public async Task<OperationResult> UpdateAsync(int id, IncomeInput input, CancellationToken ct = default)
    {
        var income = await incomes.GetByIdAsync(UserId, id, ct);
        if (income is null) return OperationResult.Missing();
        var validation = Validate(input);
        if (validation is not null) return validation;
        income.Title = input.Title.Trim();
        income.Amount = input.Amount;
        income.Date = input.Date.Date;
        income.Source = input.Source;
        income.Notes = Clean(input.Notes);
        await incomes.UpdateAsync(income, ct);
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var income = await incomes.GetByIdAsync(UserId, id, ct);
        if (income is null) return OperationResult.Missing();
        await incomes.DeleteAsync(income, ct);
        return OperationResult.Success();
    }

    private static OperationResult? Validate(IncomeInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Title) || input.Title.Trim().Length > 100)
            return OperationResult.Fail("Enter a title of 1 to 100 characters.", "Title");
        if (input.Amount <= 0 || input.Amount > 9999999999999999.99m)
            return OperationResult.Fail("Enter an amount greater than zero and within the allowed range.", "Amount");
        if (decimal.Round(input.Amount, 2) != input.Amount)
            return OperationResult.Fail("Use no more than two decimal places.", "Amount");
        if (input.Date == default)
            return OperationResult.Fail("Please choose a valid date.", "Date");
        if (!Enum.IsDefined(input.Source))
            return OperationResult.Fail("Please choose a source.", "Source");
        if (input.Notes?.Length > 2000)
            return OperationResult.Fail("Notes cannot exceed 2000 characters.", "Notes");
        return null;
    }

    private static IncomeDto Map(Income i) => new(i.Id, i.Title, i.Amount, i.Date, i.Source, i.Notes);

    private string UserId => currentUser.UserId;

    private static string? Clean(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
