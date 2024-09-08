using Microsoft.AspNetCore.Mvc;
using StudentCourseManagement.Application.Services;
using System.Threading.Tasks;
using StudentCourseManagement.Application.Interfaces;

namespace StudentCourseManagement.Presentation.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _dashboardService.GetDashboardDataAsync();
            return View(viewModel);
        }
    }
}