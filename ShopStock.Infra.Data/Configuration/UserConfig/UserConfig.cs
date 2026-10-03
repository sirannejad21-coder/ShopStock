using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStock.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Configuration.UserConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region key
            builder.HasKey(u => u.Id);
            #endregion

            #region indexes
            builder.Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired(false);
            builder.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired(true);
            builder.Property(u => u.EmailActiveCode)
                .HasMaxLength(10)
                .IsRequired(true);
        
            builder.Property(u => u.UserName)
                .HasMaxLength(50)
                .IsRequired(true);
      
        
            builder.Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired(true);
            #endregion


            #region Relations
            builder.HasMany(u => u.UserRoles)
                .WithOne(r => r.user)
                .HasForeignKey(r => r.UserId);
            #endregion

        }
    }
}
