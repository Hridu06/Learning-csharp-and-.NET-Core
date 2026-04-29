using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly LmsContext _context;

        public IndexModel(LmsContext context)
        {
            _context = context;
        }

        public IList<Student> Students { get; set; } = new List<Student>();

        public async Task OnGetAsync()
        {
            Students = await _context.Students.ToListAsync();
        }
    }
}