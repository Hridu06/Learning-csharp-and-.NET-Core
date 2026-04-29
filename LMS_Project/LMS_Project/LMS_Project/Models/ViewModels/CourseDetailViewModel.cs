namespace LMS_Project.Models.ViewModels
{
    public class CourseDetailViewModel
    {
        public Course? Course { get; set; }

        public List<Assignment>? Assignments { get; set; }

        public List<StudentCourse>? Enrollments { get; set; }
    }
}