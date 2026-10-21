using Microsoft.Extensions.Logging;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Service.Interfaces;
using PersonalExpenseTracker.Service.Models;

namespace PersonalExpenseTracker.Service.Services;

public class ExpenseService(IExpenseRepository expenses, ICategoryRepository categories,
    IReceiptStorage receipts, ILogger<ExpenseService> logger) : IExpenseService
{
    public async Task<IReadOnlyList<ExpenseDto>> GetAllAsync(CancellationToken ct = default) =>
        (await expenses.GetAllAsync(ct)).Select(Map).ToList();

    public async Task<ExpenseDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var expense = await expenses.GetByIdAsync(id, ct);
        return expense is null ? null : Map(expense);
    }

    public async Task<OperationResult> CreateAsync(ExpenseInput input, ReceiptUpload? receipt, CancellationToken ct = default)
    {
        var validation = await ValidateAsync(input, ct);
        if (validation is not null) return validation;
        string? savedFile = null;
        try
        {
            if (receipt is not null) savedFile = await receipts.SaveAsync(receipt, ct);
            await expenses.AddAsync(new Expense
            {
                Title = input.Title.Trim(), Amount = input.Amount, Date = input.Date.Date,
                CategoryId = input.CategoryId, Notes = Clean(input.Notes), ReceiptFileName = savedFile
            }, ct);
            return OperationResult.Success();
        }
        catch (ReceiptValidationException ex) { return OperationResult.Fail(ex.Message, "Receipt"); }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            receipts.Delete(savedFile);
            logger.LogError(ex, "Receipt could not be saved.");
            return OperationResult.Fail("The receipt could not be saved. Please try again.", "Receipt");
        }
        catch (RepositoryConflictException ex) when (ex.Kind == ConflictKind.RelatedRecord)
        {
            receipts.Delete(savedFile);
            return OperationResult.Fail("The selected category no longer exists. Please select another.", "CategoryId");
        }
        catch
        {
            receipts.Delete(savedFile);
            throw;
        }
    }

    public async Task<OperationResult> UpdateAsync(int id, ExpenseInput input, ReceiptUpload? receipt, CancellationToken ct = default)
    {
        var expense = await expenses.GetByIdAsync(id, ct);
        if (expense is null) return OperationResult.Missing();
        var validation = await ValidateAsync(input, ct);
        if (validation is not null) return validation;

        var oldFile = expense.ReceiptFileName;
        string? newFile = null;
        try
        {
            if (receipt is not null) newFile = await receipts.SaveAsync(receipt, ct);
            expense.Title = input.Title.Trim();
            expense.Amount = input.Amount;
            expense.Date = input.Date.Date;
            expense.CategoryId = input.CategoryId;
            expense.Notes = Clean(input.Notes);
            // No upload means keep the existing receipt.
            expense.ReceiptFileName = newFile ?? oldFile;
            await expenses.UpdateAsync(expense, ct);
        }
        catch (ReceiptValidationException ex) { return OperationResult.Fail(ex.Message, "Receipt"); }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            receipts.Delete(newFile);
            logger.LogError(ex, "Receipt could not be replaced.");
            return OperationResult.Fail("The receipt could not be saved. Please try again.", "Receipt");
        }
        catch (RepositoryConflictException ex) when (ex.Kind == ConflictKind.RelatedRecord)
        {
            receipts.Delete(newFile);
            return OperationResult.Fail("The selected category no longer exists. Please select another.", "CategoryId");
        }
        catch
        {
            receipts.Delete(newFile);
            throw;
        }

        var cleaned = newFile is null || receipts.Delete(oldFile);
        return OperationResult.Success(cleaned ? null : "The expense was updated, but the old receipt could not be removed from disk.");
    }

    public async Task<OperationResult> DeleteAsync(int id, CancellationToken ct = default)
    {
        var expense = await expenses.GetByIdAsync(id, ct);
        if (expense is null) return OperationResult.Missing();
        await expenses.DeleteAsync(expense, ct);
        var cleaned = receipts.Delete(expense.ReceiptFileName);
        return OperationResult.Success(cleaned ? null : "The expense was deleted, but its receipt could not be removed from disk.");
    }

    private async Task<OperationResult?> ValidateAsync(ExpenseInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.Title) || input.Title.Trim().Length > 100)
            return OperationResult.Fail("Enter a title of 1 to 100 characters.", "Title");
        if (input.Amount <= 0 || input.Amount > 9999999999999999.99m)
            return OperationResult.Fail("Enter an amount greater than zero and within the allowed range.", "Amount");
        if (decimal.Round(input.Amount, 2) != input.Amount)
            return OperationResult.Fail("Use no more than two decimal places.", "Amount");
        if (input.Date == default)
            return OperationResult.Fail("Please choose a valid date.", "Date");
        if (input.Notes?.Length > 2000)
            return OperationResult.Fail("Notes cannot exceed 2000 characters.", "Notes");
        if (!await categories.ExistsAsync(input.CategoryId, ct))
            return OperationResult.Fail("Please select an existing category.", "CategoryId");
        return null;
    }

    public static ExpenseDto Map(Expense e) =>
        new(e.Id, e.Title, e.Amount, e.Date, e.Notes, e.ReceiptFileName, e.CategoryId, e.Category.Name);

    private static string? Clean(string? value) => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
