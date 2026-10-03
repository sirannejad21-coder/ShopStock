using ShopStock.Domain.Models.Common;
using ShopStock.Domain.Models.Prouducts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Models.Catgories
{
    public class Catgory:BaseEntity
    {

        public int? ParentId { get; set; }

        public string Tittle { get; set; }

        public string? ImageName { get; set; }

        public string Slug { get; set; }

        public Catgory? Parent { get; set; }

        public ICollection<Catgory>? catgories { get; set; }=new List<Catgory>();
        public ICollection<Prouduct>? prouducts { get; set; } = new List<Prouduct>();







    }
}
