using StudentCourseManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentCourseManagement.Application.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task AssignCoursesToStudentAsync(int studentId, List<int> courseIds);
        Task<Student> MyCoursesAsync(string userEmail);
    }
}