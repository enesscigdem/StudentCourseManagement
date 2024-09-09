using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Application.Interfaces;
using StudentCourseManagement.Application.ViewModels;
using StudentCourseManagement.Infrastructure.Data;

public class DashboardService : IDashboardService
{
    private readonly ICacheService _cacheService;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public DashboardService(ApplicationDbContext context, UserManager<IdentityUser> userManager, ICacheService cacheService)
    {
        _context = context;
        _userManager = userManager;
        _cacheService = cacheService;
    }

    public async Task<DashboardViewModel> GetDashboardDataAsync()
    {
        var totalStudents = await _context.Students.CountAsync();
        var totalCourses = await _context.Courses.CountAsync();

        var recentStudentActivities = await _context.Students
            .OrderByDescending(s => s.EnrollmentDate)
            .Take(5)
            .ToListAsync();

        var recentCourseActivities = await _context.Courses
            .OrderByDescending(c => c.StartDate)
            .Take(5)
            .ToListAsync();

        var users = await _userManager.Users.ToListAsync();
        var recentRoleAssignments = new List<RoleAssignmentViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                recentRoleAssignments.Add(new RoleAssignmentViewModel
                {
                    UserName = user.UserName,
                    RoleName = roles.First()
                });
            }
        }

        return new DashboardViewModel
        {
            TotalStudents = totalStudents,
            TotalCourses = totalCourses,
            RecentStudentActivities = recentStudentActivities,
            RecentCourseActivities = recentCourseActivities,
            RecentRoleAssignments = recentRoleAssignments
        };
    }
}