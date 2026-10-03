using ShopStock.Application.Generator;
using ShopStock.Application.Security;
using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.Models.Prouducts;
using ShopStock.Domain.Models.Roles;
using ShopStock.Domain.Models.Users;
using ShopStock.Domain.ViewModels.Account;
using ShopStock.Domain.ViewModels.Catgory;
using ShopStock.Domain.ViewModels.Prouduct;
using ShopStock.Domain.ViewModels.Role;
using ShopStock.Domain.ViewModels.User;
using ShopStock.Infra.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Mapper
{
    public static class UserMapper
    {
        public static User MappertoUser(RegisterViewModel model)
        {

            return new User()
            {
                UserName = model.UserName,
                Password = PasswordHelper.EncodePasswordMd5(model.Password),
                Email = model.Email,
                IsActive = true,
                IsActiveEmail = false,
                CreateDate = DateTime.Now,
                Avatar = "NoPhoto",
                EmailActiveCode = NameGenerator.GenerateuniqName(),
            };

        }
        public static ProfileViewModel MappertoUser(User user)
        {

            return new ProfileViewModel()
            {
                Avatar = user.Avatar,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mobile = user.Mobile,
                NationalCode = user.NationalCode
            };

        }

        public static User MappertoUser(AdminCreateViewModel model)
        {

            var user = new Domain.Models.Users.User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                Mobile = model.Mobile,
                NationalCode = model.NationalCode,
                Password = model.Password,
                Avatar = string.IsNullOrEmpty(model.Avatar)
            ? "nophoto.jpg"
            : model.Avatar,
                IsActive = model.IsActive,
                IsDelete = false,
                UpdateDate = DateTime.Now,
                EmailActiveCode = Guid.NewGuid().ToString("N"),


            };
            return user;
        }

        public static AdminEditViewModel MappertoAdminEdit(User model)
        {
            if (model == null)
                return null;


            return new AdminEditViewModel()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                Mobile = model.Mobile,
                NationalCode = model.NationalCode,
                Password = null,
                Avatar = string.IsNullOrEmpty(model.Avatar)
            ? "nophoto.jpg"
            : model.Avatar,
                IsActive = model.IsActive,
                SelectedRoles = model.UserRoles?
            .Select(u => u.RoleId)
            .ToList()
            ?? new List<int>()




            };
        }

        public static List<AdminCreateRoleViewModel> mappertoadmincreatrole(List<Role> Role)
        {
            var result = new List<AdminCreateRoleViewModel>();

            foreach (var role in Role)
            {
                result.Add(new AdminCreateRoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.RoleName
                });
            }

            return result;



        }


        public static IQueryable<CatgoryViewModel> MapperCatgory(
      IQueryable<Catgory> catgory)
        {
            return catgory
                .Select(cat => new CatgoryViewModel
                {
                    Id = cat.Id,
                    ImageName = cat.ImageName,
                    IsDeleted = cat.IsDelete,
                    ParentId = cat.ParentId,
                    Tittle = cat.Tittle,


                })
                .AsQueryable();
        }


        public static AdminEditCatgoryViewModel mappertoeditcat(Catgory catgory)
        {

            return new AdminEditCatgoryViewModel()
            {
                CatgoryId = catgory.Id,
                ImageName = catgory.ImageName,
                Tittle = catgory.Tittle,
                Slug = catgory.Slug,
                ParentId = catgory.ParentId,


            };
        }


        public static IQueryable<ProuductViewModel> MapperProuduct(
IQueryable<Prouduct> prouduct)
        {
            return prouduct.Select
                (cat => new ProuductViewModel
                {
                    Id = cat.Id,
                    Title = cat.Tittle,
                    Active = cat.Active,
                    CatgoryId = cat.CatgoryId,
                    Price = cat.Price,
                    CreatDate = cat.CreateDate,
                    DetailReview = cat.DetailReview,
                    ImageName = cat.ImageName,
                    IsDelete = cat.IsDelete,
                    Review = cat.Review,
                    ShortDescript = cat.ShortDescript,
                    CatgoryName=cat.Catgory.Tittle
                })
               ;
        }


    }
}