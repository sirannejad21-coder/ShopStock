using ShopStock.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Contracts
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetPermissionsAllAsync();
        Task AddPermissionRoleAsync(int RollId, List<int> selectedPermission);
        Task<Permission?> GetPermissionByNameAsync(string PermissionName);
        Task<Permission?> GetPermissionByIdAsync(int Id);

        Task DeleteAsync(Permission permission);
        Task DeleteAsync(int permissionId);
        Task CreateAsync(Permission permission);
        Task UpdateAsync(Permission permission);



        Task DeletePermissionAsync(int RoleId);

    }
}
