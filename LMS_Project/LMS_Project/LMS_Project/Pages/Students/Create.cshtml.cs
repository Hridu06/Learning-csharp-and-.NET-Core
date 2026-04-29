using LMS_Project.Data;
using LMS_Project.Models;
using LMS_Project.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LMS_Project.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly LmsContext _context;

        public CreateModel(LmsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Student.Role = UserRole.Student;
            Student.CreatedDate = DateTime.Now;
            Student.ModifiedDate = DateTime.Now;

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}