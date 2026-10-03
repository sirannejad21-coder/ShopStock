using ShopStock.Domain.Enums.Common;
using ShopStock.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShopStock.Domain.ViewModels.Catgory
{
    public class AdminFilterCatgoryViewModel:BasePaging<CatgoryViewModel>
    {

        public int? ParentId { get; set; }
        [DisplayName("عنوان گروه")]
        public string? Tittle { get; set; }
        [DisplayName("وضعیت حذف")]
        public FilterDeleteResult DeleteResult { get; set; }


    }
}
