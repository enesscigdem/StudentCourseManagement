namespace StudentCourseManagement.Domain.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        // Kursa kayıtlı öğrenciler
        public ICollection<Student>? Students { get; set; }
    }
}