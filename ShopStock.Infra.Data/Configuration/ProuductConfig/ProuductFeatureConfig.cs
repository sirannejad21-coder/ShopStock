using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStock.Domain.Models.Prouducts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Configuration.ProuductConfig
{
    internal class ProuductFeatureConfig : IEntityTypeConfiguration<ProuductFeature>
    {
        public void Configure(EntityTypeBuilder<ProuductFeature> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Value).HasMaxLength(200).IsRequired();
            builder.Property(i => i.Name).HasMaxLength(200).IsRequired();
            builder.Property(i => i.ProuductId).IsRequired();
        }
    }
}
