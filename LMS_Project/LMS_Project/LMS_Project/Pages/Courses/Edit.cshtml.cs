using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LMS_Project.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly LmsContext _context;

        public EditModel(LmsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            Course = course;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(Course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}