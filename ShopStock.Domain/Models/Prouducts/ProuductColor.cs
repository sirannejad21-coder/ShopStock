using ShopStock.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopStock.Domain.Models.Prouducts
{
    public class ProuductColor : BaseEntity
    {
        [Display(Name = "محصول")]
        [Required(ErrorMessage = "انتخاب محصول الزامی است")]
        public int ProuductId { get; set; }


        [Display(Name = "نام رنگ")]
        [Required(ErrorMessage = "نام رنگ الزامی است")]
        [MaxLength(100, ErrorMessage = "نام رنگ نمی‌تواند بیشتر از 100 کاراکتر باشد")]
        public string Name { get; set; }


        [Display(Name = "کد رنگ")]
        [Required(ErrorMessage = "کد رنگ الزامی است")]
        [MaxLength(50, ErrorMessage = "کد رنگ نمی‌تواند بیشتر از 50 کاراکتر باشد")]
        public string Code { get; set; }


        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "قیمت الزامی است")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت نمی‌تواند منفی باشد")]
        public double Price { get; set; }


        [Display(Name = "رنگ پیش‌فرض")]
        public bool IsDefult { get; set; }


        [Display(Name = "تعداد")]
        [Required(ErrorMessage = "تعداد الزامی است")]
        [Range(0, int.MaxValue, ErrorMessage = "تعداد نمی‌تواند منفی باشد")]
        public int Quantity { get; set; }


        [ForeignKey("ProuductId")]
        public Prouduct? Prouduct { get; set; }
    }
}