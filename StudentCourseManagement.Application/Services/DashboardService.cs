using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Infrastructure.Data;
using StudentCourseManagement.Presentation.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using StudentCourseManagement.Application.Interfaces;

namespace StudentCourseManagement.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            var totalStudents = await _context.Students.CountAsync();
            var totalCourses = await _context.Courses.CountAsync();
            var recentActivities = await _context.Students
                .OrderByDescending(s => s.EnrollmentDate)
                .Take(5)
                .ToListAsync();

            return new DashboardViewModel
            {
                TotalStudents = totalStudents,
                TotalCourses = totalCourses,
                RecentActivities = recentActivities
            };
        }
    }
}