using ShopStock.Domain.Enums;
using ShopStock.Domain.Models.Users;
using ShopStock.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Interfaces
{


    public interface IAccountService
    {

        Task<RegisterUserResult> RegisterAsync(RegisterViewModel model);

        Task<LoginUserResult> LoginAsync(LoginViewModel model);
        Task<bool> ActiveAccountAsync(string ActiveCode);
        Task<User> GetUserByEmailOrUsernameAsync(string EmailOrUserName);
        Task<bool> ChangePasswordAsyc(int userid, ChangePasswordViewModel model);

        Task<bool> FinishProfile(int userid,ProfileViewModel model);
        Task<bool> IsCompleteProfile(int userid);
        Task<ProfileViewModel> GetUserProfileAsync(int userid);
    }
}
