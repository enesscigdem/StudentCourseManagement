using System.Collections.Generic;
using System.Threading.Tasks;
using StudentCourseManagement.Domain.Entities;

namespace StudentCourseManagement.Application.Interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
    }
}