using System.ComponentModel.DataAnnotations.Schema;

namespace StudentCourseManagement.Domain.Entities
{
    public class StudentCourse : IAuditEntity
    {
        public int CoursesCourseId { get; set; }
        public int StudentsStudentId { get; set; }

        [ForeignKey(nameof(CoursesCourseId))]
        public Course Course { get; set; }

        [ForeignKey(nameof(StudentsStudentId))]
        public Student Student { get; set; }
        
        // Audit Properties
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsActive { get; set; }
    }
}