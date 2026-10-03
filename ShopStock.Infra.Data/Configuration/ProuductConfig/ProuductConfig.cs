using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStock.Domain.Models.Prouducts;
using ShopStock.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Configuration.ProuductConfig
{
    public class ProuductConfig : IEntityTypeConfiguration<Prouduct>
    {
        public void Configure(EntityTypeBuilder<Prouduct> builder)
        {
           builder.HasKey(x => x.Id);
            builder.Property(x=>x.Tittle).HasMaxLength(200).IsRequired();
            builder.Property(x=>x.CatgoryId).IsRequired();
            builder.Property(x=>x.Price).IsRequired();

        }
    }
}
