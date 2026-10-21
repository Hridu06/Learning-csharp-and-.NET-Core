using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data;

namespace PersonalExpenseTracker.Repository.Repositories;

public abstract class RepositoryBase(ExpenseTrackerDbContext context)
{
    protected ExpenseTrackerDbContext Context { get; } = context;

    protected async Task SaveAsync(CancellationToken ct)
    {
        try { await Context.SaveChangesAsync(ct); }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 2601 or 2627 })
        {
            throw new RepositoryConflictException(ConflictKind.Duplicate, ex);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 547 })
        {
            throw new RepositoryConflictException(ConflictKind.RelatedRecord, ex);
        }
    }
}
