using LMS_Project.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS_Project.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public UserRole Role { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        // Instructor → Courses
        public ICollection<Course>? Courses { get; set; }

        // Student → Enrollments
        public ICollection<StudentCourse>? Enrollments { get; set; }
    }
}