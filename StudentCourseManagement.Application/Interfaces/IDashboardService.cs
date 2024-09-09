using System.Collections.Generic;
using System.Threading.Tasks;
using StudentCourseManagement.Application.ViewModels;
using StudentCourseManagement.Domain.Entities;

namespace StudentCourseManagement.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync();
    }
}