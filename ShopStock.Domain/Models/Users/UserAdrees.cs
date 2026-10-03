using ShopStock.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopStock.Domain.Models.Users
{
    public class UserAdrees:BaseEntity
    {

        public int UserId { get; set; }
        [Required]
        [MaxLength(300)]
        public string Tittle { get; set; }
        [Required]
        [MaxLength(300)]
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Adrees { get; set; }


        #region Relation

        [ForeignKey("UserId")]
        public User? user { get; set; }

        #endregion

    }
}
