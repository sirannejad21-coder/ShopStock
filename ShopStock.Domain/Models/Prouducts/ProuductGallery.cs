using ShopStock.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopStock.Domain.Models.Prouducts
{
    public class ProuductGallery:BaseEntity
    {

        public int ProuductId { get; set; }

        public string Alt { get; set; }

        public string ImageName { get; set; }
        [ForeignKey("ProuductId")]
        public Prouduct? Prouduct { get; set; }


    }
}
