using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Domain.Entities;
using StudentCourseManagement.Domain.Interfaces;
using StudentCourseManagement.Infrastructure.Data;

namespace StudentCourseManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            return await _context.Students.Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}