using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StudentCourseManagement.Application.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Rol ismi gereklidir.")]
        [Display(Name = "Rol Adı")]
        public string RoleName { get; set; }
    }
}