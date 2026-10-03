using ShopStock.Application.Extentions;
using ShopStock.Application.Generator;
using ShopStock.Application.Mapper;
using ShopStock.Application.Security;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Enums;
using ShopStock.Domain.Models.Users;
using ShopStock.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implitation
{
    public class AccountService(IUserRepository _repository) : IAccountService
    {
        public async Task<bool> ActiveAccountAsync(string ActiveCode)
        {
            var User = await _repository.GetUserByActiveCodeAsync(ActiveCode);
            if (User == null) return false;

            User.IsActiveEmail = true;
            User.EmailActiveCode = NameGenerator.GenerateuniqName();
         await  _repository.UpdateAsync(User);

            return true;
           

        }

        public async Task<bool> ChangePasswordAsyc(int userid, ChangePasswordViewModel model)
        {
            var user= await _repository.GetbyIdAsync(userid);

            if (user == null) throw new Exception("پسورد یافت نشد");

            if (PasswordHelper.VerifyPassword(model.OldPassword, user.Password) == false) return false;

            user.Password = PasswordHelper.EncodePasswordMd5(model.Password);

          await  _repository.UpdateAsync(user);
        
            return true;
        }

        public async Task<bool> FinishProfile(int userid, ProfileViewModel model)
        {
            var user = await _repository.GetbyIdAsync(userid);
            if (user == null) throw new Exception("کاربر یافت نشد");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Mobile = model.Mobile;
            user.NationalCode = model.NationalCode;
            user.Avatar= model.Avatar;
            await _repository.UpdateAsync(user);
            return true;
        }

        public async Task<User> GetUserByEmailOrUsernameAsync(string EmailOrUserName)
        {
            return await _repository.GetUserByEmailOrUserNameAsync(EmailOrUserName);
        }

        public async Task<ProfileViewModel> GetUserProfileAsync(int userid)
        {
            var user=await _repository.GetbyIdAsync(userid);

      return  Mapper.UserMapper.MappertoUser(user);

           
        }

        public async Task<bool> IsCompleteProfile(int userid)
        {
           var user=await _repository.GetbyIdAsync(userid);

            if (user.Mobile == null || user.Mobile == "")
            {
                return true;
            }
            return false;   
        }

        public async Task<LoginUserResult> LoginAsync(LoginViewModel model)
        {
            var user = await _repository.GetUserByEmailOrUserNameAsync(
                model.UserNameOrEmail.FixUsername()
              
              );

            if (user == null)
            {
                return LoginUserResult.NotFound;
            }

            if(user.IsDelete == true)
            {
                return LoginUserResult.NotFound;
            }

            if (PasswordHelper.VerifyPassword(model.Password, user.Password) == false)
            {
                return LoginUserResult.NotFound;
            }   

            if (!user.IsActiveEmail)
            {
                return LoginUserResult.NotActive;
            }

            return LoginUserResult.Success;
        }

        public async Task<RegisterUserResult> RegisterAsync(RegisterViewModel model)
        {
            #region validation

            if (string.IsNullOrEmpty(model.UserName) ||
                string.IsNullOrEmpty(model.Email) ||
                string.IsNullOrEmpty(model.Password))
            {

                return RegisterUserResult.InvalidInout;

            }



            if (await _repository.IsExistEmailAsync(model.Email.FixEmail()))
            {

                return RegisterUserResult.EmailDuplecate;

            }

            if (await _repository.IsExistUsernameAsync(model.UserName.FixUsername()))
            {

                return RegisterUserResult.UserDuplecate;

            }
            #endregion

            var user = UserMapper.MappertoUser(model);

            await _repository.CreateAsync(user);

            return RegisterUserResult.Succes;
        }
    }
}
