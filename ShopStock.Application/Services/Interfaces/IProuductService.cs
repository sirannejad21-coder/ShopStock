using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.Models.Prouducts;
using ShopStock.Domain.ViewModels.Common;
using ShopStock.Domain.ViewModels.Prouduct;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Interfaces
{
    public interface IProuductService
    {

        Task CreateProuductAsync(AdminCreateProuductViewModel prouduct);
        Task EditProuductAsync(AdminEditProuductViewModel model);
        Task<AdminFilterProuductViewModdel> AdminFilterProuductGetAsync(AdminFilterProuductViewModdel filter);
        Task DeleteImageGallery(int  id);
        Task<IEnumerable<Prouduct>> GetProuductByCatgorySlugAsync(string Slug);
        Task <Prouduct> GetProuductForShowModalByIdAsync(int id);
        Task<AdminEditProuductViewModel> AdminProuductGetForEditAsync(int Id);
        Task<IEnumerable<Prouduct>> GetProuductForShowAsync();
        Task<Prouduct> GetProuductPage(int Id);
        Task<bool> IsExistSlugAsync(string slug);
       





    }
}
