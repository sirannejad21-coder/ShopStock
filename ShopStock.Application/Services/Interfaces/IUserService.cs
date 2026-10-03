using ShopStock.Domain.Enums;
using ShopStock.Domain.Enums.Common;
using ShopStock.Domain.Models.Users;
using ShopStock.Domain.ViewModels.Common;
using ShopStock.Domain.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<AdminUserResult> CreatCreatAdminUserAsyncAdminUser(AdminCreateViewModel adminCreateView);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserForDelete(int UserId);
        Task<User> GetUserForEdit(int UserId);
        Task DeleteByAdmin(int userid);
        Task EditByAdmin(AdminEditViewModel model);
        Task <AdminFilterUserViewModel > adminFilterUserViewModel(AdminFilterUserViewModel filterUserViewModel);

    }
}
