using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Roles;
using ShopStock.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Contracts
{
  
    public interface IRoleRepository
    {

        Task<IEnumerable<Role>> GetRolesAllAsync();
  
        Task<Role>? GetRoleByIdAsync(int RoleId);
        Task CreateAsync(Role Role);
        Task UpdateAsync(Role Role);
        Task DeleteAsync(int Roleid);
        Task DeleteAsync(Role Role);
        Task AddRoleAsync(int UserId, List<int> selectedRole);
      


    }
}
