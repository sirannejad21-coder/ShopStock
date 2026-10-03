using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShopStock.Domain.ViewModels.Catgory
{
    public class CatgoryViewModel
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        [DisplayName("عنوان نقش")]
        public string Tittle { get; set; }

        public string? ImageName { get; set; }

        public bool IsDeleted { get; set; }


    }
}
