using Microsoft.EntityFrameworkCore;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Roles;
using ShopStock.Domain.Models.Users;
using ShopStock.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace ShopStock.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        EshopDbContext _context;

        public UserRepository(EshopDbContext context)
        {
            _context = context;
        }

       
       

   

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await SaveAsync();

        }

        public async Task DeleteAsync(int userid)
        {
            var olduser =await GetbyIdAsync(userid);

            if (olduser != null) {
            
          await  DeleteAsync(olduser);
            
            }

        }

        public async Task DeleteAsync(User user)
        {
            user.IsDelete = true;
            user.IsActive = false;
            user.DeleteDate= DateTime.Now;   
            await UpdateAsync(user);
        }

        public async Task<IQueryable<User>> FilterAsync()
        {
            return  _context.Users.AsQueryable();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Where(I=>I.IsDelete==false).ToListAsync();
        }

        public async Task<User> GetbyIdAsync(int userid)
        {
            return await _context.Users.Where(I => I.IsDelete == false).FirstOrDefaultAsync(u => u.Id == userid);
        }

   
        public async Task<User> GetUserByActiveCodeAsync(string activeCode)
        {
            

            return await _context.Users.Where(I => I.IsDelete == false)
                .SingleOrDefaultAsync(s => s.EmailActiveCode == activeCode);
        }

        public async Task<User> GetUserByEmailOrUserNameAsync(string emailOrUsername)
        {
           return await _context.Users.Where(I => I.IsDelete == false).SingleOrDefaultAsync(u => u.Email == emailOrUsername || u.UserName == emailOrUsername);
        }

        public async Task<User> GetUserFullData(int userid)
        {
            return await _context.Users.Where(I => I.IsDelete == false).Include(r => r.UserRoles).ThenInclude(r=>r.Role).
                SingleOrDefaultAsync(r => r.Id == userid);
        }

        public async Task<bool> IsExistEmailAsync(string email)
        {
         return  await _context.Users.AnyAsync(u=>u.Email == email)  ;
        }

        public async Task<bool> IsExistMobileAsync(string Mobile)
        {
            return await _context.Users.AnyAsync(u => u.Mobile == Mobile);
        }

        public async Task<bool> IsExistUsernameAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await SaveAsync();
        }
    }
}
