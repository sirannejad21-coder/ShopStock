using Microsoft.EntityFrameworkCore;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Roles;
using ShopStock.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Repository
{
    public class PermissionRepository(EshopDbContext _Context) : IPermissionRepository
    {
        public async Task DeletePermissionAsync(int RoleId)
        {
            var permission = await _Context.RolePermissions.Where(x => x.RoleId == RoleId).ToListAsync();

            _Context.RolePermissions.RemoveRange(permission);

            await _Context.SaveChangesAsync();
        }

    

        public async Task<IEnumerable<Permission>> GetPermissionsAllAsync()
        {
            return await _Context.Permissions.ToListAsync();
        }

        public async Task AddPermissionRoleAsync(int RollId, List<int> selectedPermission)
        {
            var role = await _Context.Roles
    .FirstOrDefaultAsync(x => x.Id == RollId);

            if (role == null)
                throw new Exception($"Role با Id = {RollId} پیدا نشد.");

            foreach (var permission in selectedPermission)
            {

                await _Context.RolePermissions.AddAsync(new RolePermission()
                {

                    RoleId = RollId,
                    PermissionId = permission

                });


            }
            _Context.SaveChanges();

        }

        public async Task<Permission?> GetPermissionByNameAsync(string PermissionName)
        {
            return await _Context.Permissions
          .Include(x => x.RolePermissions)
          .SingleOrDefaultAsync(x => x.UnicName == PermissionName);
        }

        public async Task<Permission?> GetPermissionByIdAsync(int Id)
        {
            return await _Context.Permissions.SingleOrDefaultAsync(x=>x.Id == Id);
        }

        public async Task DeleteAsync(Permission permission)
        {
            _Context.Permissions.Remove(permission);
       await     _Context.SaveChangesAsync();
        }

        public async Task CreateAsync(Permission permission)
        {
       await   _Context.Permissions.AddAsync(permission);
          await  _Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Permission permission)
        {
           _Context.Permissions.Update(permission);
         await   _Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int permissionId)
        {
            var permission =await  _Context.Permissions.SingleOrDefaultAsync(s => s.Id == permissionId);
            if (permission != null) await DeleteAsync(permission);

        }
    }
}
