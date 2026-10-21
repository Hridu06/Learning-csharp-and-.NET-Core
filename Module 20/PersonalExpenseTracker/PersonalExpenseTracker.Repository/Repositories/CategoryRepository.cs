using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Repositories;

public class CategoryRepository(ExpenseTrackerDbContext context) : RepositoryBase(context), ICategoryRepository
{
    public async Task<IReadOnlyList<CategoryAggregate>> GetAllWithTotalsAsync(CancellationToken ct = default) =>
        await Context.ExpenseCategories.AsNoTracking().OrderBy(c => c.Name)
            .Select(c => new CategoryAggregate(c.Id, c.Name, c.Description, c.Expenses.Count,
                c.Expenses.Sum(e => (decimal?)e.Amount) ?? 0m)).ToListAsync(ct);

    public Task<ExpenseCategory?> GetByIdAsync(int id, CancellationToken ct = default) =>
        Context.ExpenseCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);

    public Task<bool> ExistsAsync(int id, CancellationToken ct = default) =>
        Context.ExpenseCategories.AnyAsync(c => c.Id == id, ct);

    public Task<bool> NameExistsAsync(string name, int? exceptId = null, CancellationToken ct = default) =>
        Context.ExpenseCategories.AnyAsync(c => c.Name == name && (!exceptId.HasValue || c.Id != exceptId.Value), ct);

    public Task<bool> HasExpensesAsync(int id, CancellationToken ct = default) =>
        Context.Expenses.AnyAsync(e => e.CategoryId == id, ct);

    public async Task AddAsync(ExpenseCategory category, CancellationToken ct = default)
    {
        Context.ExpenseCategories.Add(category);
        await SaveAsync(ct);
    }

    public async Task UpdateAsync(ExpenseCategory category, CancellationToken ct = default)
    {
        Context.Entry(category).State = EntityState.Modified;
        await SaveAsync(ct);
    }

    public async Task DeleteAsync(ExpenseCategory category, CancellationToken ct = default)
    {
        Context.ExpenseCategories.Remove(category);
        await SaveAsync(ct);
    }
}
