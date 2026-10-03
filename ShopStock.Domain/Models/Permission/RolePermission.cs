using ShopStock.Domain.Models.Common;
using ShopStock.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Models.Permission
{
    public class RolePermission
    {
        public int PermissionId { get; set; }

        public int RoleId { get; set; }


        #region relation

        public Permission permission { get; set; }
        public Role role { get; set; }

        #endregion

    }
}
