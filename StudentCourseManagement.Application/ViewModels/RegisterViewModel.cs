using System.ComponentModel.DataAnnotations;

namespace StudentCourseManagement.Application.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçersiz email formatı.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Şifre en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", 
            ErrorMessage = "Şifre en az bir küçük harf, bir büyük harf, bir rakam ve bir özel karakter içermelidir.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi doğrulayın.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}