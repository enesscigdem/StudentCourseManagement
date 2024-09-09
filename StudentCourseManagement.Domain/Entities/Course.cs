namespace StudentCourseManagement.Domain.Entities
{
    public class Course : IAuditEntity
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<StudentCourse>? StudentCourses { get; set; }

        // Audit Properties
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsActive { get; set; }
    }
}