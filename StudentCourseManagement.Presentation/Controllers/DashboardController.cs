using Microsoft.AspNetCore.Mvc;
using StudentCourseManagement.Application.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using StudentCourseManagement.Application.Interfaces;

namespace StudentCourseManagement.Presentation.Controllers
{
    [Authorize]
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