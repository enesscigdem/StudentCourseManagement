using StudentCourseManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentCourseManagement.Application.ViewModels;

namespace StudentCourseManagement.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<SettingsViewModel>> GetRolesAsync();
        Task<CreateRoleViewModel> GetRoleByIdAsync(string roleId);
        Task CreateRoleAsync(CreateRoleViewModel model);
        Task UpdateRoleAsync(EditRoleViewModel model);
        Task DeleteRoleAsync(string roleId);
        Task<List<UserRoleViewModel>> GetUsersInRoleAsync(string roleId);
        Task UpdateUsersInRoleAsync(string roleId, List<UserRoleViewModel> users);
    }
}