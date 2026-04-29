using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Assignments
{
    public class CreateModel : PageModel
    {
        private readonly LmsContext _context;

        public CreateModel(LmsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Assignment Assignment { get; set; } = new();

        public SelectList CourseList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            CourseList = new SelectList(courses, "Id", "Title");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var courses = await _context.Courses.ToListAsync();
                CourseList = new SelectList(courses, "Id", "Title");
                return Page();
            }

            _context.Assignments.Add(Assignment);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}