using System.ComponentModel.DataAnnotations;

namespace StudentCourseManagement.Domain.Entities
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "İsim alanı zorunludur.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Kayıt tarihi zorunludur.")]
        public DateTime EnrollmentDate { get; set; }

        // İlişkili Kurslar
        public ICollection<Course>? Courses { get; set; }
    }

}