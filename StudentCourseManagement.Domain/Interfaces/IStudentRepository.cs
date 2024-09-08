using StudentCourseManagement.Domain.Entities;

namespace StudentCourseManagement.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentById(int studentId);
        Task<List<Student>> GetAllStudents();
        Task AddStudent(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int studentId);
    }
}