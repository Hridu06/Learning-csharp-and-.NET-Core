using LMS_Project.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LmsContext _context;

        public IndexModel(LmsContext context)
        {
            _context = context;
        }

        public int TotalCourses { get; set; }
        public int TotalStudents { get; set; }
        public int TotalAssignments { get; set; }
        public int TotalEnrollments { get; set; }

        public async Task OnGetAsync()
        {
            TotalCourses = await _context.Courses.CountAsync();

            TotalStudents = await _context.Users
                .CountAsync(u => u.Role == Models.Enums.UserRole.Student);

            TotalAssignments = await _context.Assignments.CountAsync();

            TotalEnrollments = await _context.StudentCourses.CountAsync();
        }
    }
}