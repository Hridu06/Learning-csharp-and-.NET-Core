using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTracker.Data.Entities;

namespace PersonalExpenseTracker.Data;

public class ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();
    public DbSet<Income> Incomes => Set<Income>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<LoanPayment> LoanPayments => Set<LoanPayment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var user = modelBuilder.Entity<ApplicationUser>();
        user.Property(u => u.FullName).HasMaxLength(100).IsRequired();
        user.Property(u => u.AvatarFileName).HasMaxLength(100);
        user.Property(u => u.MonthlyBudget).HasPrecision(18, 2);

        // Default categories are created per user at registration, so nothing is seeded here.
        var category = modelBuilder.Entity<ExpenseCategory>();
        category.Property(c => c.Name).HasMaxLength(100).IsRequired()
            .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        category.Property(c => c.Description).HasMaxLength(500);
        category.Property(c => c.Color).HasMaxLength(7).IsRequired();
        category.Property(c => c.Icon).HasMaxLength(50).IsRequired();
        category.Property(c => c.MonthlyBudget).HasPrecision(18, 2);
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

        var income = modelBuilder.Entity<Income>();
        income.Property(i => i.Title).HasMaxLength(100).IsRequired();
        income.Property(i => i.Amount).HasPrecision(18, 2);
        income.Property(i => i.Date).HasColumnType("date");
        income.Property(i => i.Source).HasConversion<string>().HasMaxLength(20);
        income.Property(i => i.Notes).HasMaxLength(2000);
        income.HasIndex(i => new { i.UserId, i.Date });
        income.HasOne(i => i.User).WithMany()
            .HasForeignKey(i => i.UserId).OnDelete(DeleteBehavior.Restrict);

        var loan = modelBuilder.Entity<Loan>();
        loan.Property(l => l.Type).HasConversion<string>().HasMaxLength(20);
        loan.Property(l => l.Person).HasMaxLength(100).IsRequired();
        loan.Property(l => l.Principal).HasPrecision(18, 2);
        loan.Property(l => l.StartDate).HasColumnType("date");
        loan.Property(l => l.DueDate).HasColumnType("date");
        loan.Property(l => l.Notes).HasMaxLength(2000);
        loan.HasIndex(l => new { l.UserId, l.Type });
        loan.HasOne(l => l.User).WithMany()
            .HasForeignKey(l => l.UserId).OnDelete(DeleteBehavior.Restrict);
        // Payments belong to their loan and go with it.
        loan.HasMany(l => l.Payments).WithOne(p => p.Loan)
            .HasForeignKey(p => p.LoanId).OnDelete(DeleteBehavior.Cascade);

        var payment = modelBuilder.Entity<LoanPayment>();
        payment.Property(p => p.Amount).HasPrecision(18, 2);
        payment.Property(p => p.Date).HasColumnType("date");
        payment.Property(p => p.Notes).HasMaxLength(500);
    }
}
