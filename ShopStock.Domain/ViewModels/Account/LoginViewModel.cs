using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری یا ایمیل ")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string UserNameOrEmail { get; set; }


        [Display(Name = "رمز عبور")]

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]


        public string Password { get; set; }

        [Display(Name = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }

        public  string? ImageData { get; set; }


        //[Display(Name = "کد امنیتی")]
        //[Required(ErrorMessage = "{0} را وارد کنید")]
        //public string CapchaAnswer { get; set; }




    }
}
