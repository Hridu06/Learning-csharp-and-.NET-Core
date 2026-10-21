using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data.Entities;

namespace PersonalExpenseTracker.Data;

public class ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : DbContext(options)
{
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var category = modelBuilder.Entity<ExpenseCategory>();
        category.Property(c => c.Name).HasMaxLength(100).IsRequired()
            .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        category.Property(c => c.Description).HasMaxLength(500);
        category.HasIndex(c => c.Name).IsUnique();
        category.HasData(
            new ExpenseCategory { Id = 1, Name = "Food", Description = "Groceries, meals and dining out" },
            new ExpenseCategory { Id = 2, Name = "Transport", Description = "Public transport, fuel and travel" },
            new ExpenseCategory { Id = 3, Name = "Utilities", Description = "Electricity, water, internet and other bills" },
            new ExpenseCategory { Id = 4, Name = "Entertainment", Description = "Movies, hobbies and leisure" });

        var expense = modelBuilder.Entity<Expense>();
        expense.Property(e => e.Title).HasMaxLength(100).IsRequired();
        expense.Property(e => e.Amount).HasPrecision(18, 2);
        expense.Property(e => e.Date).HasColumnType("date");
        expense.Property(e => e.Notes).HasMaxLength(2000);
        expense.Property(e => e.ReceiptFileName).HasMaxLength(100);
        expense.HasIndex(e => e.Date);
        expense.HasOne(e => e.Category).WithMany(c => c.Expenses)
            .HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);
    }
}
