using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.ViewModels.Catgory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Interfaces
{
    public interface ICatgoryService
    {
        Task<Catgory> GetCatgoryByIdAsync(int id);
        Task EditByAdminAsync(AdminEditCatgoryViewModel model);
        Task CreateAsync(AdminCreateCatgoryViewModel catgory);
        Task<bool> IsExistSlugAsync(string Slug);
        Task<bool> IsExistSlugAsync(string slug, int categoryId);
        Task<IEnumerable<Catgory>> GetAllCatgoryForMegaAsync();
     
        Task<AdminFilterCatgoryViewModel> FilterAsync(AdminFilterCatgoryViewModel adminFilter);
        Task DeleteAsync(int id);

        Task<List<CatgoryViewModel>> GetCatgories(int? ParentId);

    }
}
