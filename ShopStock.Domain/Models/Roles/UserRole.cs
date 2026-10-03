using ShopStock.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Models.Roles
{
    public class UserRole
    {

        public int Id { get; set; }
        public int RoleId { get; set; }

        public int UserId { get; set; }

        #region Relation
        public User user { get; set; }
        public Role Role { get; set; }

        #endregion




    }
}
