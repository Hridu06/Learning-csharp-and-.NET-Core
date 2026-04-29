using LMS_Project.Data;
using LMS_Project.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LMS_Project.Models.Enums;

namespace LMS_Project.Pages.Dashboard
{
    public class StudentModel : PageModel
    {
        private readonly LmsContext _context;

        public StudentModel(LmsContext context)
        {
            _context = context;
        }

        public int EnrolledCoursesCount { get; set; }
        public int CompletedAssignmentsCount { get; set; }
        public decimal GradeProgress { get; set; }

        public List<StudentCourse> EnrolledCourses { get; set; } = new();

        public async Task OnGetAsync()
        {
            int studentId = 2; // sample student

            EnrolledCourses = await _context.StudentCourses
                .Include(sc => sc.Course)
                .Where(sc => sc.StudentId == studentId)
                .ToListAsync();

            EnrolledCoursesCount = EnrolledCourses.Count;

            CompletedAssignmentsCount = await _context.StudentAssignments
                .CountAsync(sa => sa.StudentId == studentId && sa.Status == AssignmentStatus.Graded);

            var grades = EnrolledCourses
                .Where(sc => sc.Grade.HasValue)
                .Select(sc => sc.Grade.Value)
                .ToList();

            GradeProgress = grades.Any() ? grades.Average() : 0;
        }
    }
}