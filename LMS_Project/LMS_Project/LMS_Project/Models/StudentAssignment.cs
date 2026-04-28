using LMS_Project.Models.Enums;

namespace LMS_Project.Models
{
    public class StudentAssignment
    {
        // Composite Key
        public int StudentId { get; set; }
        public User? Student { get; set; }

        public int AssignmentId { get; set; }
        public Assignment? Assignment { get; set; }

        public DateTime? SubmissionDate { get; set; }

        public decimal? PointsEarned { get; set; }

        public AssignmentStatus Status { get; set; } = AssignmentStatus.Pending;

        public string? Feedback { get; set; }
    }
}