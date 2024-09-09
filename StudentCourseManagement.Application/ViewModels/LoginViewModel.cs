using System.ComponentModel.DataAnnotations;

namespace StudentCourseManagement.Application.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçersiz Email adresi.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")] 
        public bool RememberMe { get; set; }
    }
}