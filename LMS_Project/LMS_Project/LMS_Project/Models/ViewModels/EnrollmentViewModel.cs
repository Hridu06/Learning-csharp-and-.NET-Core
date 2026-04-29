namespace LMS_Project.Models.ViewModels
{
    public class EnrollmentViewModel
    {
        public Student? Student { get; set; }

        public List<StudentCourse>? EnrolledCourses { get; set; }

        public List<Course>? AvailableCourses { get; set; }
    }
}