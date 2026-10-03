using Microsoft.EntityFrameworkCore;
using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.Models.Permission;
using ShopStock.Domain.Models.Prouducts;
using ShopStock.Domain.Models.Roles;
using ShopStock.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Context
{


  
        public class EshopDbContext(DbContextOptions<EshopDbContext> options)
            : DbContext(options)
        {

        #region user
        public DbSet<User> Users { get; set; }

            public DbSet<UserAdrees> UserAdrees { get; set; }

        #endregion

        #region Role
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion

        #region permission
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region catgory

        public DbSet<Catgory> catgories { get; set; }

        #endregion

        #region Prouduct

        public DbSet<Prouduct> Prouducts { get; set; }
        public DbSet<ProuductColor> prouductColors { get; set; }
        public DbSet<ProuductGallery> prouductGalleries { get; set; }
        public DbSet<ProuductFeature> ProuductFeatures { get; set; }
        public DbSet<ProuductTag> prouductTags { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

            #region Queryfilter

            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Catgory>().HasQueryFilter(x => !x.IsDelete);

            #endregion
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

         
                base.OnModelCreating(modelBuilder);
            }

      
    }
}
