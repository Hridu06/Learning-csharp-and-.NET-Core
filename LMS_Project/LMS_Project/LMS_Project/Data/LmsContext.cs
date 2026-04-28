using LMS_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Data
{
    public class LmsContext : DbContext
    {
        public LmsContext(DbContextOptions<LmsContext> options)
            : base(options)
        {
        }

        // =========================
        // DbSets
        // =========================
        public DbSet<User> Users { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Separate configuration class
            LmsContextModelCreating.Configure(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateModifiedDates();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateModifiedDates();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateModifiedDates()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is User user && entry.State == EntityState.Modified)
                {
                    user.ModifiedDate = DateTime.Now;
                }
            }
        }
    }
}