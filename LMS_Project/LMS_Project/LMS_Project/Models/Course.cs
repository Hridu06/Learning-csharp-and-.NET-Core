using System.ComponentModel.DataAnnotations;

namespace LMS_Project.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0, 100)]
        public int Credits { get; set; }

        public int MaxEnrollment { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // FK
        public int InstructorId { get; set; }

        // ✅ Strong typing
        public Instructor? Instructor { get; set; }

        // Many-to-Many
        public ICollection<StudentCourse>? StudentEnrollments { get; set; }

        // One-to-Many
        public ICollection<Assignment>? Assignments { get; set; }
    }
}