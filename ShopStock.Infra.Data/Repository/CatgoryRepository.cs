using Microsoft.EntityFrameworkCore;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.ViewModels.Catgory;
using ShopStock.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Repository
{
    public class CatgoryRepository(EshopDbContext _Contex) : ICatgoryRepository
    {
        public async Task Create(Catgory catgory)
        {
      await   _Contex.catgories.AddAsync(catgory);
         await   _Contex.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
        var catgory= await  GetCatgoryByIdAsync(id);
            catgory.IsDelete = true;
          await  Update(catgory);

        }

        public async Task<IQueryable<Catgory>> FilterAsync()
        {

            return  _Contex.catgories.AsQueryable();
        }

        public async Task<IEnumerable<Catgory>> GetAllCatgoryForMegaMenueAsync()
        {


            return await _Contex.catgories.ToListAsync();
        }

        public async Task<List<Catgory>> GetCatgoriesByParentIdAsync(int? parentid)
        {
            return await _Contex.catgories.Where(i=>i.ParentId == parentid&&i.IsDelete==false).ToListAsync();
        }

        public async Task<Catgory> GetCatgoryByIdAsync(int id)
        {
           return await _Contex.catgories.FindAsync(id);      
        }

        public async Task<bool> IsExistSlugAsync(string Slug)
        {
           return await _Contex.catgories.AnyAsync(c=>c.Slug==Slug.Trim());
        }

        public async Task<bool> IsExistSlugAsync(string slug, int categoryId)
        {
            slug = slug.Trim();

            return await _Contex.catgories
                .AnyAsync(x =>
                    x.Slug == slug &&
                    x.Id != categoryId);
        }

        public async Task Update(Catgory catgory)
        {

             _Contex.catgories.Update(catgory);

           await _Contex.SaveChangesAsync();
        }

        public async Task Update(int catgoryid)
        {
           var catgory= await GetCatgoryByIdAsync(catgoryid);

            Update(catgory);
        }
    }
}
