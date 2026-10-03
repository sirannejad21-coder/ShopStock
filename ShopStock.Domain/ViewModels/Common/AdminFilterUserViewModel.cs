using ShopStock.Domain.Enums.Common;
using ShopStock.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Common
{
    public class AdminFilterUserViewModel:BasePaging<Models.Users.User>
    {

        [Display(Name = "نام")]

        [MaxLength(50, ErrorMessage = "نام نمی‌تواند بیشتر از ۵۰ کاراکتر باشد")]
        public string? FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]

        [MaxLength(50, ErrorMessage = "نام خانوادگی نمی‌تواند بیشتر از ۵۰ کاراکتر باشد")]
        public string? LastName { get; set; }


        [Display(Name = "ایمیل")]
   
        public string? Email { get; set; }




        [Display(Name = "شماره موبایل")]
  
        public string? Mobile { get; set; }

        [Display(Name = "وضعیت فعال بودن")]
        public bool IsActive { get; set; }
        [Display(Name = "وضعیت حذف")]
        public FilterDeleteResult DeleteStutus { get; set; }
    }
}
