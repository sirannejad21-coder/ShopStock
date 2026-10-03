using ShopStock.Application.Services.Interfaces;
using ShopStock.Application.Statics;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implitation
{
    public class PermissionService(IPermissionRepository _Repository,IUserRepository _userRepository) : IPermissionService
    {
        public async Task<bool> CheckUserPermissionAsync(int UserId, string PermissionName)
        {
            var user = await _userRepository.GetUserFullData(UserId);
            if (user == null)
                return false;

            if (user.UserRoles == null)
                return false;

            var permission = await _Repository
                .GetPermissionByNameAsync(PermissionName);

            if (permission == null)
                return false;

            if (permission.RolePermissions == null)
                return false;

            return user.UserRoles.Any(u =>
                permission.RolePermissions.Any(r =>
                    r.RoleId == u.RoleId));



        }

        public Task<bool> CheckUserPermissionAsync(int UserId, IEnumerable<string> PermissionName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return await _Repository.GetPermissionsAllAsync();
        }
    }
}
