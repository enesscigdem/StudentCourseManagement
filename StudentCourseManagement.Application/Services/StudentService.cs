using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Application.Interfaces;
using StudentCourseManagement.Domain.Entities;
using StudentCourseManagement.Infrastructure.Data;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext _context;

    public StudentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Course) // İlişkili kursları da dahil edin
            .Where(x => x.StudentId == id)
            .FirstOrDefaultAsync();
    }


    public async Task AddStudentAsync(Student student)
    {
        student.EnrollmentDate = DateTime.SpecifyKind(student.EnrollmentDate, DateTimeKind.Utc);
        student.CreatedAt = DateTime.UtcNow;
        student.IsActive = true; // Yeni eklenen
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
            student.IsActive = false; // Silme işlemi sadece soft-delete olacak
            await _context.SaveChangesAsync();
        }
    }
    public async Task AssignCoursesToStudentAsync(int studentId, List<int> courseIds)
    {
        var existingCourseIds = _context.StudentCourses
            .Where(sc => sc.StudentsStudentId == studentId)
            .Select(sc => sc.CoursesCourseId)
            .ToHashSet(); // Set olarak almak daha hızlı kontrol sağlar

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



}