using Microsoft.Extensions.DependencyInjection;
using ShopStock.Application.Services.Implitation;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.IOC
{
    public static class DIContainer
    {

    public static void RegisterServices(this IServiceCollection services)
        {
            #region Repository

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<ICatgoryRepository, CatgoryRepository>();
            services.AddScoped<IProuductRepository, ProuductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped(typeof(IGenrericRepository<>),typeof(GenericRepository<>));



            #endregion


            #region Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ICatgoryService, CatgoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProuductService, ProuductService>();
            #endregion


        }

    }
}
