using StudentCourseManagement.Domain.Entities;

namespace StudentCourseManagement.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course> GetCourseById(int courseId);
        Task<List<Course>> GetAllCourses();
        Task AddCourse(Course course);
        Task UpdateCourse(Course course);
        Task DeleteCourse(int courseId);
    }
}