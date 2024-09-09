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
        return await _context.Students.Where(x => x.StudentId == id).FirstOrDefaultAsync();
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
        var student = await _context.Students.Include(s => s.Courses).FirstOrDefaultAsync(s => s.StudentId == studentId);
        if (student != null)
        {
            student.Courses.Clear();
            foreach (var courseId in courseIds)
            {
                var course = await _context.Courses.FindAsync(courseId);
                if (course != null)
                {
                    student.Courses.Add(course);
                }
            }
            await _context.SaveChangesAsync();
        }
    }

}