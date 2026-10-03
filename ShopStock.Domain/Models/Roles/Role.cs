using ShopStock.Domain.Models.Common;
using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Models.Roles
{
    public class Role:BaseEntity
    {
   
        public string RoleName { get; set; }

        #region Relation
        public ICollection<UserRole> userRoles { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }


        #endregion

    }
}
