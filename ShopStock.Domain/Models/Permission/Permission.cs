using ShopStock.Domain.Models.Common;
using ShopStock.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Models.Permission
{
    public class Permission
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string UnicName { get; set; }



        public string DisplayName { get; set; }


        #region relation

        public ICollection<RolePermission> RolePermissions { get; set; }

        public Permission? Parent { get; set; }
        #endregion
  





    }
}
