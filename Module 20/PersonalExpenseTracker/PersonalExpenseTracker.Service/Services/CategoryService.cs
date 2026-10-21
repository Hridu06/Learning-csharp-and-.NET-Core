using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

public class CategoryService(ICategoryRepository categories) : ICategoryService
{
    private const string LinkedMessage = "This category has linked expenses. Move or delete those expenses before deleting the category.";

    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken ct = default) =>
        (await categories.GetAllWithTotalsAsync(ct))
        .Select(c => new CategoryDto(c.Id, c.Name, c.Description, c.ExpenseCount, c.Total)).ToList();

    public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var category = await categories.GetByIdAsync(id, ct);
        return category is null ? null : new CategoryDto(category.Id, category.Name, category.Description, 0, 0);
    }

    public async Task<OperationResult> CreateAsync(CategoryInput input, CancellationToken ct = default)
    {
        var validation = await ValidateAsync(input, null, ct);
        if (validation is not null) return validation;
        try
        {
            await categories.AddAsync(new ExpenseCategory
            {
                Name = input.Name.Trim(), Description = Clean(input.Description)
            }, ct);
            return OperationResult.Success();
        }
        catch (RepositoryConflictException ex) when (ex.Kind == ConflictKind.Duplicate)
        {
            return OperationResult.Fail("A category with this name already exists.", "Name");
        }
    }

    public async Task<OperationResult> UpdateAsync(int id, CategoryInput input, CancellationToken ct = default)
    {
        var category = await categories.GetByIdAsync(id, ct);
        if (category is null) return OperationResult.Missing();
        var validation = await ValidateAsync(input, id, ct);
        if (validation is not null) return validation;
        category.Name = input.Name.Trim();
        category.Description = Clean(input.Description);
        try
        {
            await categories.UpdateAsync(category, ct);
            return OperationResult.Success();
        }
        catch (RepositoryConflictException ex) when (ex.Kind == ConflictKind.Duplicate)
        {
            return OperationResult.Fail("A category with this name already exists.", "Name");
        }
    }

    public async Task<OperationResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var category = await categories.GetByIdAsync(id, ct);
        if (category is null) return OperationResult.Missing();
        if (await categories.HasExpensesAsync(id, ct)) return OperationResult.Fail(LinkedMessage);
        try
        {
            await categories.DeleteAsync(category, ct);
            return OperationResult.Success();
        }
        catch (RepositoryConflictException ex) when (ex.Kind == ConflictKind.RelatedRecord)
        {
            return OperationResult.Fail(LinkedMessage);
        }
    }

    private async Task<OperationResult?> ValidateAsync(CategoryInput input, int? id, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.Name) || input.Name.Trim().Length > 100)
            return OperationResult.Fail("Enter a category name of 1 to 100 characters.", "Name");
        if (input.Description?.Length > 500)
            return OperationResult.Fail("The description cannot exceed 500 characters.", "Description");
        if (await categories.NameExistsAsync(input.Name.Trim(), id, ct))
            return OperationResult.Fail("A category with this name already exists.", "Name");
        return null;
    }

    private static string? Clean(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
