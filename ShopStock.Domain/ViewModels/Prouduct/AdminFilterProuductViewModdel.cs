using ShopStock.Domain.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShopStock.Domain.ViewModels.Prouduct
{
    public class AdminFilterProuductViewModdel:BasePaging<ProuductViewModel>
    {
        [DisplayName("عنوان")]
        public string Title { get; set; }



    }
}
