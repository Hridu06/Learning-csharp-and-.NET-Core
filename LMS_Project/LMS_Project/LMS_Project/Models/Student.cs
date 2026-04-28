namespace LMS_Project.Models
{
    public class Student : User
    {
        // Student ↔ Course
        public new ICollection<StudentCourse>? Enrollments { get; set; }

        // Student ↔ Assignment
        public new ICollection<StudentAssignment>? AssignmentSubmissions { get; set; }
    }
}