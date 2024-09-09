using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Application.Interfaces;
using StudentCourseManagement.Domain.Entities;
using StudentCourseManagement.Infrastructure.Data;

public class CourseService : ICourseService
{
    private readonly ICacheService _cacheService;
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context, ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<List<Course>> GetAllCoursesAsync()
    {
        var cacheKey = "courses_list";
        try
        {
            var cachedCourses = await _cacheService.GetCacheAsync<List<Course>>(cacheKey);
            if (cachedCourses != null)
            {
                return cachedCourses;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache read failed: {ex.Message}");
        }

        var courses = await _context.Courses.ToListAsync();
        try
        {
            await _cacheService.SetCacheAsync(cacheKey, courses, TimeSpan.FromMinutes(10));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache write failed: {ex.Message}");
        }

        return courses;
    }


    public async Task<Course> GetCourseByIdAsync(int id)
    {
        var cacheKey = $"course_{id}";
        try
        {
            var cachedCourse = await _cacheService.GetCacheAsync<Course>(cacheKey);
            if (cachedCourse != null)
            {
                return cachedCourse;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache read failed: {ex.Message}");
        }

        var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == id);
        try
        {
            await _cacheService.SetCacheAsync(cacheKey, course, TimeSpan.FromMinutes(10));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache write failed: {ex.Message}");
        }

        return course;
    }

    public async Task AddCourseAsync(Course course)
    {
        course.StartDate = DateTime.SpecifyKind(course.StartDate, DateTimeKind.Utc);
        course.EndDate = DateTime.SpecifyKind(course.EndDate, DateTimeKind.Utc);
        course.CreatedAt = DateTime.UtcNow;
        course.IsActive = true;
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCourseAsync(Course course)
    {
        course.ModifiedAt = DateTime.UtcNow;
        course.StartDate = DateTime.SpecifyKind(course.StartDate, DateTimeKind.Utc);
        course.EndDate = DateTime.SpecifyKind(course.EndDate, DateTimeKind.Utc);
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await GetCourseByIdAsync(id);
        if (course != null)
        {
            course.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }
}