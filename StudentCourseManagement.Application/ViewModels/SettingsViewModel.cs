using Microsoft.AspNetCore.Identity;

namespace StudentCourseManagement.Application.ViewModels
{
    public class SettingsViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserCount { get; set; }
    }
}