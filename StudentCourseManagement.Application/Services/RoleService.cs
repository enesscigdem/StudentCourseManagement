using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Application.Interfaces;
using StudentCourseManagement.Presentation.ViewModels;

namespace StudentCourseManagement.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<List<SettingsViewModel>> GetRolesAsync()
        {
            return await Task.Run(() => _roleManager.Roles
                .Select(r => new SettingsViewModel
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    UserCount = _userManager.Users.Count(u => _userManager.IsInRoleAsync(u, r.Name).Result)
                }).ToList());
        }

        public async Task<CreateRoleViewModel> GetRoleByIdAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return null;

            return new CreateRoleViewModel { RoleName = role.Name };
        }

        public async Task CreateRoleAsync(CreateRoleViewModel model)
        {
            var role = new IdentityRole { Name = model.RoleName };
            await _roleManager.CreateAsync(role);
        }

        public async Task UpdateRoleAsync(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role != null)
            {
                role.Name = model.RoleName;
                await _roleManager.UpdateAsync(role);
            }
        }

        public async Task DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
        }

        public async Task<List<UserRoleViewModel>> GetUsersInRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return new List<UserRoleViewModel>();

            var users = await _userManager.Users.ToListAsync();
            return users.Select(u => new UserRoleViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                IsSelected = _userManager.IsInRoleAsync(u, role.Name).Result
            }).ToList();
        }

        public async Task UpdateUsersInRoleAsync(string roleId, List<UserRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return;

            foreach (var user in users)
            {
                var identityUser = await _userManager.FindByIdAsync(user.UserId);
                if (identityUser == null) continue;

                if (user.IsSelected && !await _userManager.IsInRoleAsync(identityUser, role.Name))
                {
                    await _userManager.AddToRoleAsync(identityUser, role.Name);
                }
                else if (!user.IsSelected && await _userManager.IsInRoleAsync(identityUser, role.Name))
                {
                    await _userManager.RemoveFromRoleAsync(identityUser, role.Name);
                }
            }
        }
    }
}