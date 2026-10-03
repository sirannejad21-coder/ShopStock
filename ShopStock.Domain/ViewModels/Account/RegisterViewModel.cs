using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string UserName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده صحیح نیست")]
        public string Email { get; set; }


        [Display(Name = "رمز عبور")]
       
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [RegularExpression(
    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
    ErrorMessage = "رمز عبور باید حداقل ۸ کاراکتر و شامل حروف بزرگ، حروف کوچک، عدد و یک کاراکتر خاص باشد."
)]
        public string Password { get; set; }
      


        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور با رمز عبور یکسان نیست")]
        public string RePassword { get; set; }

    }
}
