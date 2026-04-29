using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly LmsContext _context;

        public DeleteModel(LmsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Course = await _context.Courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Course == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var course = await _context.Courses.FindAsync(Course.Id);

            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}