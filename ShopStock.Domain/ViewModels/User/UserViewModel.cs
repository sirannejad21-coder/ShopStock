
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopStock.Domain.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name = "نام")]
       
        [MaxLength(50, ErrorMessage = "نام نمی‌تواند بیشتر از ۵۰ کاراکتر باشد")]
        public string? FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
       
        [MaxLength(50, ErrorMessage = "نام خانوادگی نمی‌تواند بیشتر از ۵۰ کاراکتر باشد")]
        public string? LastName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "وارد کردن ایمیل الزامی است")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نیست")]
        public string Email { get; set; }


        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "وارد کردن نام کاربری الزامی است")]
        [MinLength(3, ErrorMessage = "نام کاربری حداقل باید ۳ کاراکتر باشد")]
        [MaxLength(50, ErrorMessage = "نام کاربری نمی‌تواند بیشتر از ۵۰ کاراکتر باشد")]
        public string UserName { get; set; }


        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "وارد کردن شماره موبایل الزامی است")]
        [RegularExpression(
            @"^09\d{9}$",
            ErrorMessage = "شماره موبایل باید به صورت 09xxxxxxxxx باشد"
        )]
        public string? Mobile { get; set; }


        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "وارد کردن رمز عبور الزامی است")]
        [MinLength(6, ErrorMessage = "رمز عبور حداقل باید ۶ کاراکتر باشد")]
        public string Password { get; set; }


        [Display(Name = "تصویر کاربر")]
        public string? Avatar { get; set; }


        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "وارد کردن کد ملی الزامی است")]
        [RegularExpression(
            @"^\d{10}$",
            ErrorMessage = "کد ملی باید ۱۰ رقم باشد"
        )]
        public string? NationalCode { get; set; }


        [Display(Name = "وضعیت فعال بودن")]
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }


    }
}

