using ShopStock.Domain.Models.Prouducts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.ViewModels.Prouduct
{
    public class ProuductViewModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public int CatgoryId { get; set; }
        public int DiscountPercent { get; set; }

        public string CatgoryName { get; set; }

        public double Price { get; set; }
        public double PriceWithDiscount { get; set; }
        public string? ShortDescript { get; set; }

        public string? Review { get; set; }

        public string? DetailReview { get; set; }

        public string? ImageName { get; set; }


     //   public int Count { get; set; }
        public bool Active { get; set; }
        public bool IsDelete { get; set; }

        public DateTime CreatDate { get; set; }

        public List<ProuductColor>? ProuductColors { get; set; }
        public List<ProuductFeature>? ProuductFeatures { get; set; }
        public List<ProuductGallery>? ProuductGalleries { get; set; }





    }
}
