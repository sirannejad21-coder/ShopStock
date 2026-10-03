using Microsoft.AspNetCore.Http;
using ShopStock.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.User
{
    public class AdminEditViewModel
    {
        [Display(Name = "نام")]

        [MaxLength(50, ErrorMessage = "نام نمی‌تواند بیشتر از ۵۰ کاراکتر باشد")]
        public string? FirstName { get; set; }
        public int Id { get; set; }

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


        [Display(Name = "رمز کاربری")]
        public string? Password { get; set; }


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

        public IFormFile? AvatarFile { get; set; }

        // تمام نقش‌های موجود
        public IEnumerable<Domain.Models.Roles.Role> Roles { get; set; } = new List<Domain.Models.Roles.Role>();

        // ID نقش‌هایی که ادمین انتخاب کرده
        public List<int> SelectedRoles { get; set; } = new List<int>();


    }
}
