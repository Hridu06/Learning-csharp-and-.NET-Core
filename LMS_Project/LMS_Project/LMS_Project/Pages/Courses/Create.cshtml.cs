using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LMS_Project.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly LmsContext _context;

        public CreateModel(LmsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Course { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Course.InstructorId = 1;

            _context.Courses.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}