using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ISBN must be unique
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();

            // AvailableCopies <= TotalCopies
            modelBuilder.Entity<Book>()
                .ToTable(t =>
                    t.HasCheckConstraint(
                        "CK_Book_Copies",
                        "[AvailableCopies] <= [TotalCopies]"));

            // Default BorrowDate = Current DateTime
            modelBuilder.Entity<BorrowRecord>()
                .Property(b => b.BorrowDate)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}