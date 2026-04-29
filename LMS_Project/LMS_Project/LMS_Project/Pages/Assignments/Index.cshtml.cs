using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project.Pages.Assignments
{
    public class IndexModel : PageModel
    {
        private readonly LmsContext _context;

        public IndexModel(LmsContext context)
        {
            _context = context;
        }

        public IList<Assignment> Assignments { get; set; } = new List<Assignment>();

        public async Task OnGetAsync()
        {
            Assignments = await _context.Assignments
                .Include(a => a.Course)
                .Where(a => !a.Deleted)
                .ToListAsync();
        }
    }
}