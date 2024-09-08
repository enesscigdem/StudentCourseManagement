using StudentCourseManagement.Domain.Entities;

public class DashboardViewModel
{
    public int TotalStudents { get; set; }
    public int TotalCourses { get; set; }
    public List<Student> RecentActivities { get; set; }
}