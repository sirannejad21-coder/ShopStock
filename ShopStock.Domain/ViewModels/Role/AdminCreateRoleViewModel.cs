using ShopStock.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.ViewModels.Role
{
    public class AdminCreateRoleViewModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

      public  List<int>? PermissionSelectedId { get; set; }    
      public  IEnumerable<Permission>? Permissions { get; set; }


    }
}
