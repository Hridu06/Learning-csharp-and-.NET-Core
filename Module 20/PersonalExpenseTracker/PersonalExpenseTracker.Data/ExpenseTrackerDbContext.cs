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
        category.Property(c => c.Color).HasMaxLength(7).IsRequired();
        category.Property(c => c.Icon).HasMaxLength(50).IsRequired();
        category.HasIndex(c => c.Name).IsUnique();
        category.HasData(
            new ExpenseCategory { Id = 1, Name = "Food", Description = "Groceries, meals and dining out", Color = "#F97316", Icon = "basket" },
            new ExpenseCategory { Id = 2, Name = "Transport", Description = "Public transport, fuel and travel", Color = "#0EA5E9", Icon = "bus-front" },
            new ExpenseCategory { Id = 3, Name = "Utilities", Description = "Electricity, water, internet and other bills", Color = "#EAB308", Icon = "lightning-charge" },
            new ExpenseCategory { Id = 4, Name = "Entertainment", Description = "Movies, hobbies and leisure", Color = "#A855F7", Icon = "film" });

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
