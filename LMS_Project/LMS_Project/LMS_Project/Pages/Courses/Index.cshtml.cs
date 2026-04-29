using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly LmsContext _context;

        public IndexModel(LmsContext context)
        {
            _context = context;
        }

        public IList<Course> Courses { get; set; } = new List<Course>();

        public async Task OnGetAsync()
        {
            Courses = await _context.Courses
                .Include(c => c.Instructor)
                .ToListAsync();
        }
    }
}