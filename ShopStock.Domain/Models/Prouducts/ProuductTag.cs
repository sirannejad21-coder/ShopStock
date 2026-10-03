using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopStock.Domain.Models.Prouducts
{
    public class ProuductTag
    {
        [Key]
        public  int TagId { get; set; }
        public int ProuductId { get; set; }

        public string TagTitle { get; set; }

        [ForeignKey("ProuductId")]
        public Prouduct Prouduct { get; set; }

    }
}
