using Microsoft.AspNetCore.Http;
using ShopStock.Domain.Models.Prouducts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Prouduct
{
    public class AdminEditProuductViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public int CatgoryId { get; set; }

        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string Tittle { get; set; } = string.Empty;

        [Display(Name = "عنوان Url")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string Slug { get; set; } = string.Empty;

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت باید بزرگ‌تر یا مساوی صفر باشد")]
        public double Price { get; set; }

        [Display(Name = "توضیحات کوتاه")]

        public string? ShortDescript { get; set; } = string.Empty;

        [Display(Name = "توضیحات")]
        public string? Review { get; set; }

        [Display(Name = "توضیحات کامل")]
        public string? DetailReview { get; set; }

        [Display(Name = "تصویر محصول")]
        public IFormFile? FormFile { get; set; }
        [DisplayName("گالری تصاویر")]
        public IFormFile[]? Galleries { get; set; }

        public string? ImageName { get; set; }

        [Display(Name = "تعداد موجودی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [Range(0, int.MaxValue, ErrorMessage = "تعداد موجودی نمی‌تواند منفی باشد")]
        public int Count { get; set; } = 0;

        [Display(Name = "فعال")]
        public bool Active { get; set; }

        public string? Tags { get; set; }

        public List<ProuductGallery>? ProuductGalleries { get; set; }
    }
}
