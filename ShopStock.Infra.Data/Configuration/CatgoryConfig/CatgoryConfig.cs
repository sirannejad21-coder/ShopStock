using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Configuration.CatgoryConfig
{
    public class CatgoryConfig : IEntityTypeConfiguration<Catgory>
    {
        public void Configure(EntityTypeBuilder<Catgory> builder)
        {

            builder.HasKey(i=>i.Id);

            builder.Property(i=>i.Tittle).HasMaxLength(200);
            builder.Property(i => i.ImageName).IsRequired();
            builder.Property(i => i.Slug).HasMaxLength(250).IsRequired();

            builder.HasOne(i=>i.Parent).WithMany(i=>i.catgories)
                .HasForeignKey(i=>i.ParentId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
