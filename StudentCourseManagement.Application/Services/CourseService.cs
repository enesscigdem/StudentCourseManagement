using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Application.Interfaces;
using StudentCourseManagement.Domain.Entities;
using StudentCourseManagement.Infrastructure.Data;

public class CourseService : ICourseService
{
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetAllCoursesAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        return await _context.Courses.Where(x => x.CourseId == id).FirstOrDefaultAsync();
    }

    public async Task AddCourseAsync(Course course)
    {
        course.StartDate = DateTime.SpecifyKind(course.StartDate, DateTimeKind.Utc);
        course.EndDate = DateTime.SpecifyKind(course.EndDate, DateTimeKind.Utc);
        course.CreatedAt = DateTime.UtcNow;
        course.IsActive = true; // Yeni eklenen
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
            course.IsActive = false; // Soft-delete işlemi
            await _context.SaveChangesAsync();
        }
    }
}