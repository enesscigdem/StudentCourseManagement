using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Application.Interfaces;
using StudentCourseManagement.Domain.Entities;
using StudentCourseManagement.Infrastructure.Data;

public class StudentService : IStudentService
{
    private readonly ICacheService _cacheService;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public StudentService(ApplicationDbContext context, UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager, ICacheService cacheService)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _cacheService = cacheService;
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        var cacheKey = "students_list";
        try
        {
            var cachedStudents = await _cacheService.GetCacheAsync<List<Student>>(cacheKey);
            if (cachedStudents != null)
            {
                return cachedStudents;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache read failed: {ex.Message}");
        }

        var students = await _context.Students.ToListAsync();
        try
        {
            await _cacheService.SetCacheAsync(cacheKey, students, TimeSpan.FromMinutes(10));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache write failed: {ex.Message}");
        }

        return students;
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        var cacheKey = $"student_{id}";
        try
        {
            var cachedStudent = await _cacheService.GetCacheAsync<Student>(cacheKey);
            if (cachedStudent != null)
            {
                return cachedStudent;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache read failed: {ex.Message}");
        }

        var student = await _context.Students
            .Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Course)
            .FirstOrDefaultAsync(x => x.StudentId == id);

        try
        {
            await _cacheService.SetCacheAsync(cacheKey, student, TimeSpan.FromMinutes(30));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache write failed: {ex.Message}");
        }

        return student;
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