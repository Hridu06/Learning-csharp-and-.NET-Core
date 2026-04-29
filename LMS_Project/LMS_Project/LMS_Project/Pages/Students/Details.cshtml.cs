using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly LmsContext _context;

        public DetailsModel(LmsContext context)
        {
            _context = context;
        }

        public Student? Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (Student == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}