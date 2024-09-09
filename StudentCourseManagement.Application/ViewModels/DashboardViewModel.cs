using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using StudentCourseManagement.Domain.Entities;

namespace StudentCourseManagement.Application.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public List<Student> RecentStudentActivities { get; set; }
        public List<Course> RecentCourseActivities { get; set; }
        public List<RoleAssignmentViewModel> RecentRoleAssignments { get; set; }
    }

    public class RoleAssignmentViewModel
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}