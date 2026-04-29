using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Courses
{
    public class DetailsModel : PageModel
    {
        private readonly LmsContext _context;

        public DetailsModel(LmsContext context)
        {
            _context = context;
        }

        public Course? Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Course == null)
                return NotFound();

            return Page();
        }
    }
}