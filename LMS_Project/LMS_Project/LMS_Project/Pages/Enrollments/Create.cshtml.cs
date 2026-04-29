using LMS_Project.Data;
using LMS_Project.Models;
using LMS_Project.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Enrollments
{
    public class CreateModel : PageModel
    {
        private readonly LmsContext _context;

        public CreateModel(LmsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentCourse StudentCourse { get; set; } = new();

        public SelectList Students { get; set; }
        public SelectList Courses { get; set; }

        public async Task OnGetAsync()
        {
            Students = new SelectList(await _context.Users
                .Where(u => u.Role == UserRole.Student)
                .ToListAsync(), "Id", "Name");

            Courses = new SelectList(await _context.Courses
                .ToListAsync(), "Id", "Title");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            StudentCourse.EnrollmentDate = DateTime.Now;

            _context.StudentCourses.Add(StudentCourse);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}