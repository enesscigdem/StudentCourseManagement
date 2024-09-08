using Microsoft.AspNetCore.Identity;

public class EditRoleViewModel
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public List<UserRoleViewModel> Users { get; set; }
}