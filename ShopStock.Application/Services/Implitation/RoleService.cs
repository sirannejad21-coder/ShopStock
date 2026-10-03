using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Roles;
using ShopStock.Domain.ViewModels.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implitation
{
    public class RoleService(IRoleRepository _Repository,IPermissionRepository _permission) : IRoleService
    {
        public async Task AdminCreateRolesAsync(AdminCreateRoleViewModel model,List<int>selected)
        {


            var role = new Role
            {
                CreateDate = DateTime.Now,
                RoleName = model.RoleName
            };

            await _Repository.CreateAsync(role);

            await _permission.AddPermissionRoleAsync(
                role.Id,
                selected);
        }

        public async Task AdminEditRole(Role role,List<int>selected)
        {
            var existingRole = await _Repository.GetRoleByIdAsync(role.Id);

            if (existingRole == null)
                throw new Exception("Role پیدا نشد.");

            existingRole.RoleName = role.RoleName;

            await _permission.DeletePermissionAsync(role.Id);

            await _permission.AddPermissionRoleAsync(role.Id, selected);

            await _Repository.UpdateAsync(existingRole);
        }

        public async Task DeleteRole(int id)
        {
         await _Repository.DeleteAsync(id);
        }

    

        public async Task<Role> GetRoleByIdAsync(int id)
        {
            return  await _Repository.GetRoleByIdAsync(id);
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
          return await  _Repository.GetRolesAllAsync();
        }
    }
}
