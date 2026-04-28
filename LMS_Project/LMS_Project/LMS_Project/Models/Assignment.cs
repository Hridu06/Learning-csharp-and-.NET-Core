using System.ComponentModel.DataAnnotations;

namespace LMS_Project.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        // Foreign Key
        public int CourseId { get; set; }

        // Navigation Property
        public Course? Course { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0, 1000)]
        public int MaxPoints { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Soft Delete
        public bool Deleted { get; set; } = false;

        public ICollection<StudentAssignment>? StudentAssignments { get; set; }
    }
}