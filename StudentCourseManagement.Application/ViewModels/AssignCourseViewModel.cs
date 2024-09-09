using StudentCourseManagement.Domain.Entities;
using System.Collections.Generic;

namespace StudentCourseManagement.Application.ViewModels
{
    public class AssignCourseViewModel
    {
        public Student Student { get; set; }
        public List<Course> AvailableCourses { get; set; }
        public List<int> SelectedCourseIds { get; set; }
    }
}