using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopStock.Domain.Enums.Common
{
    public enum FilterDeleteResult
    {
    [Display(Name ="حذف نشده")]    NotDelete,
        [Display(Name = " همه")] All,
        [Display(Name = "حذف شده")] Delete


    }
}
