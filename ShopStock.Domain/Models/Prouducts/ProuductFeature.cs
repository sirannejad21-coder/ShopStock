using ShopStock.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopStock.Domain.Models.Prouducts
{
    public class ProuductFeature : BaseEntity
    {
        [Display(Name = "محصول")]
        [Required(ErrorMessage = "انتخاب محصول الزامی است")]
        public int ProuductId { get; set; }

        [Display(Name = "نام ویژگی")]
        [Required(ErrorMessage = "نام ویژگی الزامی است")]
        [MaxLength(200, ErrorMessage = "نام ویژگی نمی‌تواند بیشتر از 200 کاراکتر باشد")]
        public string Value { get; set; }

        [Display(Name = "مقدار ویژگی")]
        [Required(ErrorMessage = "مقدار ویژگی الزامی است")]
        [MaxLength(200, ErrorMessage = "مقدار ویژگی نمی‌تواند بیشتر از 200 کاراکتر باشد")]
        public string Name { get; set; }

        [ForeignKey(nameof(ProuductId))]
        public Prouduct? Prouduct { get; set; }
    }
}