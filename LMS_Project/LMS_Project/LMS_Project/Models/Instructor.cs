namespace LMS_Project.Models
{
    public class Instructor : User
    {
        // Optional (already inherited, but OK to keep for clarity)
        public new ICollection<Course>? Courses { get; set; }
    }
}