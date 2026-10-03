using ShopStock.Domain.Models.Prouducts;
using ShopStock.Domain.ViewModels.Prouduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Domain.Contracts
{
    public interface IProuductRepository:IGenrericRepository<Prouduct>
    {

        IQueryable<Prouduct> GetFilterProuducts();

        Task<bool> IsExist(int ProuductId);
        Task DeleteProuductTags(int ProuductId);
        Task AddGallery(ProuductGallery gallery);
        Task<  Prouduct>GetProuductForEdit(int ProuductId);
        Task AddProuductTagsAsync(int prouductid,List<ProuductTagsViewModel> tags);
        Task< IQueryable< Prouduct>> GetProuductByCatgorySlug(string Slug);

        Task<ProuductGallery> GetGalleryById(int Id);
         Task<Prouduct>GetProuductForShowModalByIdAsync(int id);
        Task<IQueryable<Prouduct>> GetProuductForShowAsync();
        Task<Prouduct> GetProuductPage(int IProuductIdd);
        Task DeleteGallery(int Id);
        Task<bool> IsExistSlugAsync(string slug);
    }
}
