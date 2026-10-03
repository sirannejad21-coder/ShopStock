using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Roles;
using ShopStock.Domain.ViewModels.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRolesAsync();
  
        Task<Role> GetRoleByIdAsync(int id);
        Task DeleteRole(int id);
        Task AdminEditRole(Role role,List<int>selected);
        Task AdminCreateRolesAsync(AdminCreateRoleViewModel model, List<int> selected);

    }
}
