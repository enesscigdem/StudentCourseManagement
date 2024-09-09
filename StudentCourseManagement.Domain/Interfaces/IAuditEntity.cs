namespace StudentCourseManagement.Domain.Entities
{
    public interface IAuditEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
        bool IsActive { get; set; }
    }
}
