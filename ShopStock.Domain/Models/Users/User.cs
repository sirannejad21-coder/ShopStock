using ShopStock.Domain.Models.Common;
using ShopStock.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.Models.Users
{
    public class User:BaseEntity
    {

        #region properties
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; }
        public string EmailActiveCode { get; set; }
        public bool IsActiveEmail { get; set; }
        public string UserName { get; set; }

        public string? Mobile { get; set; }
        public int? MobileActiveCode { get; set; }
        public string Password { get; set; }

        public string? Avatar { get; set; }

        [Display(Name = "کد ملی")]
        [RegularExpression(
      @"^\d{10}$",
      ErrorMessage = "کد ملی باید ۱۰ رقم باشد"
  )]
        public string? NationalCode { get; set; }

        public bool IsActive { get; set; }

        #endregion



        #region Relation

        public ICollection<UserAdrees>? Adrees { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

        #endregion

    }
}
