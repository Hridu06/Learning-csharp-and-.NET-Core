using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Enrollments
{
    public class IndexModel : PageModel
    {
        private readonly LmsContext _context;

        public IndexModel(LmsContext context)
        {
            _context = context;
        }

        public IList<StudentCourse> Enrollments { get; set; } = new List<StudentCourse>();

        public async Task OnGetAsync()
        {
            Enrollments = await _context.StudentCourses
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
        }
    }
}