using System.ComponentModel.DataAnnotations;

namespace StudentCourseManagement.Domain.Entities
{
    public class Student : IAuditEntity
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Kayıt tarihi zorunludur.")]
        public DateTime EnrollmentDate { get; set; }

        // İlişkili Kurslar
        public ICollection<StudentCourse>? StudentCourses { get; set; }

        // Audit Properties
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsActive { get; set; }
    }
}