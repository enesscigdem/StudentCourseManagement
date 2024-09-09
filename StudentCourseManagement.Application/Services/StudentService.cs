using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Application.Interfaces;
using StudentCourseManagement.Domain.Entities;
using StudentCourseManagement.Infrastructure.Data;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public StudentService(ApplicationDbContext context, UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Course)
            .Where(x => x.StudentId == id)
            .FirstOrDefaultAsync();
    }


    public async Task AddStudentAsync(Student student)
    {
        student.EnrollmentDate = DateTime.SpecifyKind(student.EnrollmentDate, DateTimeKind.Utc);
        student.CreatedAt = DateTime.UtcNow;
        student.IsActive = true;
        var user = new IdentityUser { UserName = student.Email, Email = student.Email };
        await _userManager.CreateAsync(user, student.Password);
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStudentAsync(Student student)
    {
        student.ModifiedAt = DateTime.UtcNow;
        student.EnrollmentDate = DateTime.SpecifyKind(student.EnrollmentDate, DateTimeKind.Utc);
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await GetStudentByIdAsync(id);
        if (student != null)
        {
            student.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }

    public async Task AssignCoursesToStudentAsync(int studentId, List<int> courseIds)
    {
        var existingCourseIds = _context.StudentCourses
            .Where(sc => sc.StudentsStudentId == studentId)
            .Select(sc => sc.CoursesCourseId)
            .ToHashSet();

        foreach (var courseId in courseIds)
        {
            if (!existingCourseIds.Contains(courseId))
            {
                await _context.StudentCourses.AddAsync(new StudentCourse
                {
                    StudentsStudentId = studentId,
                    CoursesCourseId = courseId
                });
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Student> MyCoursesAsync(string userEmail)
    {
        var student = await _context.Students
            .Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Course)
            .FirstOrDefaultAsync(s => s.Email == userEmail);

        return student;
    }
}