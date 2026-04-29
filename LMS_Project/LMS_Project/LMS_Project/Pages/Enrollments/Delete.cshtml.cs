using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Enrollments
{
    public class DeleteModel : PageModel
    {
        private readonly LmsContext _context;

        public DeleteModel(LmsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentCourse StudentCourse { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int studentId, int courseId)
        {
            StudentCourse = await _context.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (StudentCourse == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var enrollment = await _context.StudentCourses
                .FirstOrDefaultAsync(sc => sc.StudentId == StudentCourse.StudentId &&
                                           sc.CourseId == StudentCourse.CourseId);

            if (enrollment != null)
            {
                _context.StudentCourses.Remove(enrollment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}