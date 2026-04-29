using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly LmsContext _context;

        public EditModel(LmsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (Student == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var studentInDb = await _context.Students.FindAsync(Student.Id);

            if (studentInDb == null)
            {
                return NotFound();
            }

            studentInDb.Name = Student.Name;
            studentInDb.Email = Student.Email;
            studentInDb.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}