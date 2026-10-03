using Microsoft.EntityFrameworkCore;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Roles;
using ShopStock.Domain.Models.Users;
using ShopStock.Infra.Data.Context;
using ShopStock.Infra.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Repository
{


    public class RoleRepository : IRoleRepository
    {
        EshopDbContext _Context;

        public RoleRepository(EshopDbContext context)
        {
            _Context = context;
        }

        public async Task<IEnumerable<Role>> GetRolesAllAsync()
        {
            return await _Context.Roles.Where(i=>!i.IsDelete).ToListAsync();
        }

        public async Task AddRoleAsync(int UserId, List<int> selectedRole)
        {
            foreach (var role in selectedRole)
            {
                await _Context.UserRoles.AddAsync(new Domain.Models.Roles.UserRole()
                {
                    UserId = UserId,
                    RoleId = role


                });
            }
        }

        public async Task DeleteRoleAsync(int UserId)
        {

            var userrole= await _Context.UserRoles.Where(x => x.UserId == UserId).ToListAsync();
 
            _Context.UserRoles.RemoveRange(userrole);
        }

        public async Task CreateAsync(Role Role)
        {
           await _Context.Roles.AddAsync(Role);
       await     _Context.SaveChangesAsync(); 
        }

        public async Task UpdateAsync(Role Role)
        {


           _Context.Roles.Update(Role);
            await _Context.SaveChangesAsync();
                
        }

        public async Task DeleteAsync(int Roleid)
        {
            var role = await _Context.Roles.SingleOrDefaultAsync(i=>i.Id==Roleid);

            role.IsDelete = true;

            UpdateAsync(role);

        }

        public async Task DeleteAsync(Role Role)
        {
            var role = await _Context.Roles.SingleOrDefaultAsync(i => i.Id == Role.Id);

            role.IsDelete = true;

            UpdateAsync(role);
        }

        public async Task<Role>? GetRoleByIdAsync(int RoleId)
        {
            return await _Context.Roles
           .Include(x => x.RolePermissions).ThenInclude(i => i.permission)
           .Where(x => !x.IsDelete)
           .SingleOrDefaultAsync(x => x.Id == RoleId);
        }
    }
}

