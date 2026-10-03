using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopStock.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Configuration.RolePermissionConfig
{
    public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(x => new
            {
         
                x.PermissionId,
                x.RoleId
            });
            builder.HasOne(x => x.role)
           .WithMany(x => x.RolePermissions)
           .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.permission)
                   .WithMany(x => x.RolePermissions)
                   .HasForeignKey(x => x.PermissionId);

        }
    }
}
