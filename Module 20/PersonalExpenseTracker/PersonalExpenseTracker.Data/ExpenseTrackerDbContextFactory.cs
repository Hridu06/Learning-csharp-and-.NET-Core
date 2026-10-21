using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PersonalExpenseTracker.Data;

// Keeps migration generation independent of the MVC application's startup.
public class ExpenseTrackerDbContextFactory : IDesignTimeDbContextFactory<ExpenseTrackerDbContext>
{
    public ExpenseTrackerDbContext CreateDbContext(string[] args)
    {
        var connection = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? @"Server=(localdb)\MSSQLLocalDB;Database=PersonalExpenseTrackerDb;Trusted_Connection=True;TrustServerCertificate=True";
        var options = new DbContextOptionsBuilder<ExpenseTrackerDbContext>().UseSqlServer(connection).Options;
        return new ExpenseTrackerDbContext(options);
    }
}
