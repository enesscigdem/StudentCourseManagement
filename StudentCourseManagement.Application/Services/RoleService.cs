using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.Application.Interfaces;
using StudentCourseManagement.Application.ViewModels;

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
            var roles = await _roleManager.Roles.ToListAsync(); // Rolleri al
            var roleViewModels = new List<SettingsViewModel>();

            foreach (var role in roles)
            {
                var users = await _userManager.Users.ToListAsync(); // Tüm kullanıcıları al
                var userCount = 0;

                foreach (var user in users)
                {
                    // Her kullanıcı için rol kontrolü yap
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userCount++;
                    }
                }

                roleViewModels.Add(new SettingsViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    UserCount = userCount
                });
            }

            return roleViewModels;
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
            var userRoleViewModels = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var isSelected = await _userManager.IsInRoleAsync(user, role.Name);
                userRoleViewModels.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = isSelected
                });
            }

            return userRoleViewModels;
        }

        public async Task UpdateUsersInRoleAsync(string roleId, List<UserRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) return;

            foreach (var user in users)
            {
                var identityUser = await _userManager.FindByIdAsync(user.UserId);
                if (identityUser == null) continue;

                // Eğer kullanıcı seçildiyse ve bu rolde değilse, rol ekle
                if (user.IsSelected && !await _userManager.IsInRoleAsync(identityUser, role.Name))
                {
                    await _userManager.AddToRoleAsync(identityUser, role.Name);
                }
                // Eğer kullanıcı seçilmediyse ve bu rolde ise, rolü çıkar
                else if (!user.IsSelected && await _userManager.IsInRoleAsync(identityUser, role.Name))
                {
                    await _userManager.RemoveFromRoleAsync(identityUser, role.Name);
                }
            }
        }

    }
}
