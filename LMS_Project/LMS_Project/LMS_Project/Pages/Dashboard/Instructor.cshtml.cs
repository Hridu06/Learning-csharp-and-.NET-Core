using LMS_Project.Data;
using LMS_Project.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Dashboard
{
    public class InstructorModel : PageModel
    {
        private readonly LmsContext _context;

        public InstructorModel(LmsContext context)
        {
            _context = context;
        }

        public DashboardViewModel Dashboard { get; set; } = new();

        public async Task OnGetAsync()
        {
            Dashboard.TotalCoursesTaught = await _context.Courses.CountAsync();

            Dashboard.TotalStudents = await _context.StudentCourses
                .Select(sc => sc.StudentId)
                .Distinct()
                .CountAsync();

            Dashboard.PendingAssignments = await _context.StudentAssignments
                .CountAsync(sa => sa.Status == Models.Enums.AssignmentStatus.Pending);
        }
    }
}