using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data.Entities;

namespace PersonalExpenseTracker.Data;

public class ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var user = modelBuilder.Entity<ApplicationUser>();
        user.Property(u => u.FullName).HasMaxLength(100).IsRequired();
        user.Property(u => u.AvatarFileName).HasMaxLength(100);

        // Default categories are created per user at registration, so nothing is seeded here.
        var category = modelBuilder.Entity<ExpenseCategory>();
        category.Property(c => c.Name).HasMaxLength(100).IsRequired()
            .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        category.Property(c => c.Description).HasMaxLength(500);
        category.Property(c => c.Color).HasMaxLength(7).IsRequired();
        category.Property(c => c.Icon).HasMaxLength(50).IsRequired();
        category.HasIndex(c => new { c.UserId, c.Name }).IsUnique();
        category.HasOne(c => c.User).WithMany()
            .HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);

        var expense = modelBuilder.Entity<Expense>();
        expense.Property(e => e.Title).HasMaxLength(100).IsRequired();
        expense.Property(e => e.Amount).HasPrecision(18, 2);
        expense.Property(e => e.Date).HasColumnType("date");
        expense.Property(e => e.Notes).HasMaxLength(2000);
        expense.Property(e => e.ReceiptFileName).HasMaxLength(100);
        expense.HasIndex(e => new { e.UserId, e.Date });
        expense.HasOne(e => e.Category).WithMany(c => c.Expenses)
            .HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);
        // Account deletion removes a user's data explicitly in one transaction (see UserRepository).
        expense.HasOne(e => e.User).WithMany()
            .HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}
