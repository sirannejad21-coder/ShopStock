using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "رمز عبور قدیم را وارد کنید")]
        [RegularExpression(
@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
ErrorMessage = "رمز عبور باید حداقل ۸ کاراکتر و شامل حروف بزرگ، حروف کوچک، عدد و یک کاراکتر خاص باشد."
)]
        public string OldPassword { get; set; }


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
