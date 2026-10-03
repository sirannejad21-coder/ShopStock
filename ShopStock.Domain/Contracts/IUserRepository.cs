using ShopStock.Domain.Models.Roles;
using ShopStock.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Contracts
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int userid);
        Task DeleteAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task <User> GetbyIdAsync(int userid);
        Task<User> GetUserFullData(int userid);

        Task<User> GetUserByActiveCodeAsync(string activeCode);
        Task<User> GetUserByEmailOrUserNameAsync(string emailOrUsername);

        Task<bool> IsExistEmailAsync(string email);
        Task <bool> IsExistUsernameAsync(string username);
        Task<bool> IsExistMobileAsync(string Mobile);
        Task<IQueryable<User>> FilterAsync();



        Task SaveAsync();
       
    }
}
