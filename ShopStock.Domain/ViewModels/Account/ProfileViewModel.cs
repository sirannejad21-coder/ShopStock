using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Account
{
    public class ProfileViewModel
    {

 

[Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [StringLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
    public string FirstName { get; set; } = string.Empty;


    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [StringLength(50, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
    public string LastName { get; set; } = string.Empty;


    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [RegularExpression(
        @"^09\d{9}$",
        ErrorMessage = "شماره موبایل معتبر وارد کنید"
    )]
    public string Mobile { get; set; } = string.Empty;


    [Display(Name = "کد ملی")]
    [RegularExpression(
        @"^\d{10}$",
        ErrorMessage = "کد ملی باید ۱۰ رقم باشد"
    )]
    public string? NationalCode { get; set; }


    [Display(Name = "تصویر پروفایل")]
    public string? Avatar { get; set; }


}
}
