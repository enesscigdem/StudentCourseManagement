using Microsoft.AspNetCore.Mvc;
using StudentCourseManagement.Application.Services;
using System.Threading.Tasks;
using StudentCourseManagement.Application.Interfaces;
using StudentCourseManagement.Application.ViewModels;

namespace StudentCourseManagement.Presentation.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IRoleService _roleService;

        public SettingsController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _roleService.GetRolesAsync();
            return View(model);
        }

        public async Task<IActionResult> CreateRole()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _roleService.CreateRoleAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> EditRole(string roleId)
        {
            var role = await _roleService.GetRoleByIdAsync(roleId);
            if (role == null) return NotFound();

            var usersInRole = await _roleService.GetUsersInRoleAsync(roleId);
            var model = new EditRoleViewModel
            {
                RoleId = roleId,
                RoleName = role.RoleName,
                Users = usersInRole
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _roleService.UpdateRoleAsync(model);
                await _roleService.UpdateUsersInRoleAsync(model.RoleId, model.Users);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            await _roleService.DeleteRoleAsync(roleId);
            return RedirectToAction("Index");
        }
    }
}
