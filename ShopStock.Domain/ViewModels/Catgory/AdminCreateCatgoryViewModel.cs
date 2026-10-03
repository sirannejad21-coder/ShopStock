using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.ViewModels.Catgory
{
    public class AdminCreateCatgoryViewModel
    {

        public int? ParentId { get; set; }
        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Tittle { get; set; }

        public IFormFile? Image { get; set; }
        [Display(Name = "عنوان آدرس بار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Slug { get; set; }
        public string? CatgoryParentTittle { get; set; }
    }
}
