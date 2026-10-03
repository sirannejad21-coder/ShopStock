using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.ViewModels.Catgory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Contracts
{
    public interface ICatgoryRepository
    {
        Task<Catgory> GetCatgoryByIdAsync(int id);
        Task<IEnumerable<Catgory>> GetAllCatgoryForMegaMenueAsync();
        Task<IQueryable<Catgory>> FilterAsync();
        Task<bool> IsExistSlugAsync(string Slug);
        Task<bool> IsExistSlugAsync(string slug, int categoryId);
        Task Create(Catgory catgory);
        Task Update(Catgory catgory);
        Task Update(int catgoryid);
        Task DeleteAsync(int id);

        Task<List<Catgory>> GetCatgoriesByParentIdAsync(int? parentid);

    }
}
