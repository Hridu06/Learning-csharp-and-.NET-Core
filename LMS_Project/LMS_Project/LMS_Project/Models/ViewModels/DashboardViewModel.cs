namespace LMS_Project.Models.ViewModels
{
    public class DashboardViewModel
    {
        // Instructor Dashboard
        public int TotalCoursesTaught { get; set; }

        public int TotalStudents { get; set; }

        public int PendingAssignments { get; set; }

        // Student Dashboard
        public int EnrolledCourses { get; set; }

        public int CompletedAssignments { get; set; }

        public decimal AverageGrade { get; set; }
    }
}