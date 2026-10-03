using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Roles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Configuration.PermissionConfig
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(i=>i.UnicName).IsUnicode().HasMaxLength(200).IsRequired();
            builder.Property(i => i.DisplayName).HasMaxLength(200).IsRequired();
            builder.HasMany(i => i.RolePermissions).WithOne(i=>i.permission).HasForeignKey(i=>i.PermissionId);
            builder.HasOne(i=>i.Parent).WithMany().HasForeignKey(i=>i.ParentId);    




        }
    }
}
