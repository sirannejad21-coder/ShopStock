using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using ShopStock.Application.Generator;
using ShopStock.Application.Mapper;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Enums.Common;
using ShopStock.Domain.Models.Catgories;
using ShopStock.Domain.ViewModels.Catgory;

using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implitation
{
    public class CatgoryService (ICatgoryRepository _repository, IMemoryCache cache): ICatgoryService
    {
        public async Task CreateAsync(AdminCreateCatgoryViewModel catgory)
        {





            var cat = new Catgory()
            {
                Slug = catgory.Slug.Trim(),
                ParentId = catgory.ParentId,
                Tittle = catgory.Tittle.Trim(),
                IsDelete=false,
                CreateDate = DateTime.Now,

                
               
            };
         var imagename= await  SaveImageFile(catgory.Image);
            cat.ImageName = imagename;

            await _repository.Create(cat);
            cache.Remove("GetAllCatgoryForMegaAsync");
        }

        public async Task DeleteAsync(int id)
        {
           await _repository.DeleteAsync(id);
        }

        public async Task EditByAdminAsync(AdminEditCatgoryViewModel model)
        {

            var category = await _repository.GetCatgoryByIdAsync(model.CatgoryId);

            if (category == null)
            {
                return;
            }



            if (model.Image != null)
            {
                var imagename = await SaveImageFile(model.Image);
                if (!string.IsNullOrEmpty(imagename))
                {
                    category.ImageName = imagename;
                }
            }


            category.UpdateDate = DateTime.Now;
            category.Slug = model.Slug.Trim();
            category.Tittle = model.Tittle.Trim();
            category.ParentId = model.ParentId;
            
        
            
                


           

         await   _repository.Update(category);
            cache.Remove("GetAllCatgoryForMegaAsync");
        }

        public async Task<AdminFilterCatgoryViewModel> FilterAsync(AdminFilterCatgoryViewModel adminFilter)
        {

          IQueryable<Catgory> query = await _repository.FilterAsync();

            if (query != null)
            {

                if (adminFilter.ParentId.HasValue)
                {

                    query=query.Where(x => x.ParentId==adminFilter.ParentId);

                }
                else
                {

               query=   query.Where(x => x.ParentId == null);
                }


                if (!string.IsNullOrEmpty(adminFilter.Tittle))
                {


                    query = query.Where(a => a.Tittle.Contains(adminFilter.Tittle));


                }
                switch (adminFilter.DeleteResult)
                {
                    case FilterDeleteResult.NotDelete:
                        query = query.Where(u => !u.IsDelete);
                        break;
                    case FilterDeleteResult.All:
                        // No filter applied
                        break;
                    case FilterDeleteResult.Delete:
                        query = query.Where(u => u.IsDelete);
                        break;


                }
    

            }
        query = query.OrderByDescending(i => i.CreateDate);

  adminFilter.Paging(UserMapper.MapperCatgory(query));

 return adminFilter; 

        }

        public async Task<IEnumerable<Catgory>> GetAllCatgoryForMegaAsync()
        {
          


            string cacheKey = "GetAllCatgoryForMegaAsync";

           if( cache.TryGetValue(cacheKey, out IEnumerable<Catgory> catgory))
            {

                return catgory;

            }
            else
            {

                catgory= await _repository.GetAllCatgoryForMegaMenueAsync();
                cache.Set(cacheKey, catgory, TimeSpan.FromHours(1));
                return catgory;

            }


        }

        public async Task<List<CatgoryViewModel>> GetCatgories(int? ParentId)
        {
            var catgory= await _repository.GetCatgoriesByParentIdAsync(ParentId);

            return catgory.Select(u => new CatgoryViewModel {
            
            Tittle = u.Tittle,
            ImageName = u.ImageName,
            ParentId =u.ParentId,
            Id=u.Id,
            IsDeleted=u.IsDelete

            
            }).ToList();
        }

        public async Task<Catgory> GetCatgoryByIdAsync(int id)
        {
            return await _repository.GetCatgoryByIdAsync(id);
        }

    

        public async Task<bool> IsExistSlugAsync(string Slug)
        {
            return await _repository.IsExistSlugAsync(Slug);
        }

        public async Task<bool> IsExistSlugAsync(string slug, int categoryId)
        {

            return await _repository.IsExistSlugAsync(slug, categoryId);   

        }




        #region Utilities

        private async Task<string> SaveImageFile(IFormFile formFile)
        {
            string avatarName =
                NameGenerator.GenerateuniqName()
                + Path.GetExtension(formFile.FileName).ToLower();

            string avatarFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "CatgoriesImage"
            );

            if (!Directory.Exists(avatarFolder))
            {
                Directory.CreateDirectory(avatarFolder);
            }

            string savePath = Path.Combine(
                avatarFolder,
                avatarName
            );

            using (var stream = new FileStream(
                savePath,
                FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return avatarName;
        }


        #endregion
    }
}
