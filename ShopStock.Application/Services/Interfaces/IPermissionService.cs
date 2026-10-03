using ShopStock.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Interfaces
{
    public interface IPermissionService
    {

        Task<IEnumerable<Permission>> GetPermissionsAsync();

        Task<bool> CheckUserPermissionAsync(int UserId,string PermissionName);
        Task<bool> CheckUserPermissionAsync(int UserId,IEnumerable< string> PermissionName);
    }
}
