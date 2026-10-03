using Microsoft.AspNetCore.Http;
using ShopStock.Application.Generator;
using ShopStock.Application.Mapper;
using ShopStock.Application.Security;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Enums;
using ShopStock.Domain.Enums.Common;
using ShopStock.Domain.Models.Users;
using ShopStock.Domain.ViewModels.Common;
using ShopStock.Domain.ViewModels.User;

namespace ShopStock.Application.Services.Implitation
{
    public class UserService : IUserService
    {
        #region constractor
        private readonly IUserRepository _Repository;
        IRoleRepository _RoleRepository;
        IPermissionRepository _permission;

        public UserService(IUserRepository repository, IRoleRepository roleRepository, IPermissionRepository permission)
        {
            _Repository = repository;
            _RoleRepository = roleRepository;
            _permission = permission;
        }
        #endregion
        #region Create User
        public async Task<AdminUserResult> CreatCreatAdminUserAsyncAdminUser(
            AdminCreateViewModel model)
        {
            #region Validation

            if (string.IsNullOrEmpty(model.UserName) ||
                string.IsNullOrEmpty(model.Password) ||
                string.IsNullOrEmpty(model.Email))
            {
                return AdminUserResult.Error;
            }

            if (await _Repository.IsExistUsernameAsync(model.UserName))
            {
                return AdminUserResult.UsernameDuplicate;
            }

            if (await _Repository.IsExistEmailAsync(model.Email))
            {
                return AdminUserResult.EmailDuplicate;
            }

            if (!string.IsNullOrEmpty(model.Mobile) &&
                await _Repository.IsExistMobileAsync(model.Mobile))
            {
                return AdminUserResult.MobileDuplicate;
            }

            #endregion


            #region Save Avatar

            // اگر کاربر عکس انتخاب کرده باشد
            if (model.AvatarFile != null &&
                model.AvatarFile.Length > 0)
            {
                if (!model.AvatarFile.ImageValid())
                {
                    return AdminUserResult.InvalidImage;
                }

                // خیلی مهم:
                // نام فایل ذخیره شده را داخل Avatar قرار می‌دهیم
                model.Avatar = await SaveImageFile(model.AvatarFile);
            }
            else
            {
                // اگر عکس انتخاب نشده
                model.Avatar = "nophoto.jpg";
            }

            #endregion




            await _Repository.CreateAsync(
                UserMapper.MappertoUser(model)
            );




            #region Add Roles

            if (model.SelectedRoles != null &&
                model.SelectedRoles.Any())
            {
                var user =
                    await _Repository.GetUserByEmailOrUserNameAsync(
                        model.UserName
                    );

                if (user != null)
                {
                    await _RoleRepository.AddRoleAsync(
                        user.Id,
                        model.SelectedRoles
                    );

                    await _Repository.SaveAsync();
                }
            }

            #endregion


            return AdminUserResult.Succes;
        }
        #endregion


        #region delete
        public async Task<User> GetUserForDelete(int UserId)
        {
            return await _Repository.GetUserFullData(UserId);
        }

        public async Task DeleteByAdmin(int userid)
        {

            await _Repository.DeleteAsync(userid);


        }
        #endregion
        #region get
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _Repository.GetAllAsync();
        }
        #endregion

        #region Utilities

        private async Task<string> SaveImageFile(IFormFile formFile)
        {
            string avatarName =
                NameGenerator.GenerateuniqName()
                + Path.GetExtension(formFile.FileName).ToLower();

            string avatarFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Avatar"
            );

            if (!Directory.Exists(avatarFolder))
            {
                Directory.CreateDirectory(avatarFolder);
            }

            string savePath = Path.Combine(
                avatarFolder,
                avatarName
            );

            using (var stream = new FileStream(
                savePath,
                FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return avatarName;
        }


        #endregion

        #region Edit
        public async Task<User> GetUserForEdit(int UserId)
        {
            return await _Repository.GetUserFullData(UserId);
        }

        public async Task EditByAdmin(AdminEditViewModel model)
        {

            #region edit
            var user = await _Repository.GetbyIdAsync(model.Id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Mobile = model.Mobile;
            user.NationalCode = model.NationalCode;
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                user.Password = model.Password;
            }
            user.IsActive = model.IsActive;

            #endregion
            #region Add Roles

            if (model.SelectedRoles != null &&
                model.SelectedRoles.Any())
            {
                await _permission.DeletePermissionAsync(user.Id);
                var useredit =
                    await _Repository.GetUserByEmailOrUserNameAsync(
                        model.UserName
                    );

                if (useredit != null)
                {
                    await _RoleRepository.AddRoleAsync(
                        useredit.Id,
                        model.SelectedRoles
                    );

                    await _Repository.SaveAsync();
                }
            }

            #endregion
            #region Save Avatar

            // اگر کاربر عکس انتخاب کرده باشد
            if (model.AvatarFile != null &&
                model.AvatarFile.Length > 0)
            {
                if (model.AvatarFile.ImageValid())

                    // خیلی مهم:
                    // نام فایل ذخیره شده را داخل Avatar قرار می‌دهیم
                    model.Avatar = await SaveImageFile(model.AvatarFile);
                user.Avatar = model.Avatar;
            }

            #endregion

            if (user != null)
                await _Repository.UpdateAsync(user);
        }




        #endregion

        #region Filter
        public async Task<AdminFilterUserViewModel> adminFilterUserViewModel(AdminFilterUserViewModel filterUserViewModel)
        {
            IQueryable<User> query = await _Repository.FilterAsync();

            if (filterUserViewModel != null)
            {

                if (!string.IsNullOrEmpty(filterUserViewModel.FirstName))
                {
                    query = query.Where(u => u.FirstName.Contains(filterUserViewModel.FirstName));


                }

                if (!string.IsNullOrEmpty(filterUserViewModel.LastName))
                {
                    query = query.Where(u => u.LastName.Contains(filterUserViewModel.LastName));


                }

                if (!string.IsNullOrEmpty(filterUserViewModel.Email))
                {
                    query = query.Where(u => u.Email.Contains(filterUserViewModel.Email));


                }

                if (!string.IsNullOrEmpty(filterUserViewModel.Mobile))
                {
                    query = query.Where(u => u.Mobile.Contains(filterUserViewModel.Mobile));

                }


                switch (filterUserViewModel.DeleteStutus)
                {
                    case FilterDeleteResult.NotDelete:
                        query = query.Where(u => !u.IsDelete);
                        break;
                    case FilterDeleteResult.All:
                        // No filter applied
                        break;
                    case FilterDeleteResult.Delete:
                        query = query.Where(u => u.IsDelete);
                        break;


                }


            }


            query = query.OrderByDescending(u => u.CreateDate);

            filterUserViewModel.Paging(query);

            return filterUserViewModel;

        }
        #endregion
    }
}