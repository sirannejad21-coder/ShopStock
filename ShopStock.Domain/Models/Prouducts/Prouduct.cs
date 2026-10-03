using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopStock.Domain.Models.Prouducts
{
    public class Prouduct:BaseEntity
    {
        public int CatgoryId { get; set; }
        [ForeignKey("CatgoryId")]
        public Catgory? Catgory { get; set; }

        public string Tittle { get; set; }
        public double Price { get; set; }
        public string Slug { get; set; }
        public string? ShortDescript { get; set; }

        public string? Review { get; set; }

        public string? DetailReview { get; set; }

        public string? ImageName { get; set; }


        public int Count { get; set; }
        public bool Active { get; set; }

        public ICollection<ProuductColor>? ProuductColors { get; set; }
        public ICollection<ProuductFeature>? ProuductFeatures { get; set; }
        public ICollection<ProuductGallery>? ProuductGalleries { get; set; }

        public ICollection<ProuductTag>? ProuductTags { get; set; }


    }
}
