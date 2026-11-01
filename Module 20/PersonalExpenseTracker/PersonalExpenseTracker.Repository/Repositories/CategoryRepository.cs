using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;
using PersonalExpenseTracker.Data.Entities;
using PersonalExpenseTracker.Repository.Interfaces;
using PersonalExpenseTracker.Repository.Models;

namespace PersonalExpenseTracker.Repository.Repositories;

public class CategoryRepository(ExpenseTrackerDbContext context) : RepositoryBase(context), ICategoryRepository
{
    public async Task<IReadOnlyList<CategoryAggregate>> GetAllWithTotalsAsync(string userId, CancellationToken ct = default) =>
        await Context.ExpenseCategories.AsNoTracking().Where(c => c.UserId == userId).OrderBy(c => c.Name)
            .Select(c => new CategoryAggregate(c.Id, c.Name, c.Description, c.Color, c.Icon, c.Expenses.Count,
                c.Expenses.Sum(e => (decimal?)e.Amount) ?? 0m)).ToListAsync(ct);

    public Task<ExpenseCategory?> GetByIdAsync(string userId, int id, CancellationToken ct = default) =>
        Context.ExpenseCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId, ct);

    public Task<bool> ExistsAsync(string userId, int id, CancellationToken ct = default) =>
        Context.ExpenseCategories.AnyAsync(c => c.Id == id && c.UserId == userId, ct);

    public Task<bool> NameExistsAsync(string userId, string name, int? exceptId = null, CancellationToken ct = default) =>
        Context.ExpenseCategories.AnyAsync(c => c.UserId == userId && c.Name == name
            && (!exceptId.HasValue || c.Id != exceptId.Value), ct);

    public Task<bool> AnyAsync(string userId, CancellationToken ct = default) =>
        Context.ExpenseCategories.AnyAsync(c => c.UserId == userId, ct);

    public Task<bool> HasExpensesAsync(int id, CancellationToken ct = default) =>
        Context.Expenses.AnyAsync(e => e.CategoryId == id, ct);

    public async Task AddAsync(ExpenseCategory category, CancellationToken ct = default)
    {
        Context.ExpenseCategories.Add(category);
        await SaveAsync(ct);
    }

    public async Task AddRangeAsync(IEnumerable<ExpenseCategory> categories, CancellationToken ct = default)
    {
        Context.ExpenseCategories.AddRange(categories);
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
