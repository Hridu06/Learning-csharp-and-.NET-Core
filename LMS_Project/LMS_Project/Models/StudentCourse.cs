namespace LMS_Project.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public User? Student { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        public decimal? Grade { get; set; }
    }
}