using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Domain.Entities;
using StudentCourseManagement.Domain.Interfaces;
using StudentCourseManagement.Infrastructure.Data;

namespace StudentCourseManagement.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _context.Courses.FindAsync(courseId);
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task AddCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourse(int courseId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }
    }
}