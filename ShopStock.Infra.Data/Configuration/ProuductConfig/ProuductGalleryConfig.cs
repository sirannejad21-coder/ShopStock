using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStock.Domain.Models.Prouducts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Configuration.ProuductConfig
{
    public class ProuductGalleryConfig : IEntityTypeConfiguration<ProuductGallery>
    {
        public void Configure(EntityTypeBuilder<ProuductGallery> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Alt).HasMaxLength(200).IsRequired();
            builder.Property(i => i.ImageName).HasMaxLength(200).IsRequired();
            builder.Property(i => i.ProuductId).IsRequired();
        }
    }
}
