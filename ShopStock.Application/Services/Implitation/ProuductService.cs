using Microsoft.AspNetCore.Http;
using ShopStock.Application.Convertor;
using ShopStock.Application.Generator;
using ShopStock.Application.Mapper;
using ShopStock.Application.Security;
using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Enums;
using ShopStock.Domain.Models.Prouducts;
using ShopStock.Domain.ViewModels.Prouduct;
using ShopStock.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ShopStock.Application.Services.Implitation
{
    public class ProuductService : IProuductService
    {
        #region dependecy
        IProuductRepository _ProuductRepository;


        public ProuductService(IProuductRepository prouductRepository)
        {
            _ProuductRepository = prouductRepository;

        }



        #endregion
        #region filter
        public async Task<AdminFilterProuductViewModdel> AdminFilterProuductGetAsync(AdminFilterProuductViewModdel filter)
        {
            IQueryable<Prouduct> query =  _ProuductRepository.GetFilterProuducts();

            if (filter != null)
            {
                query = query.Where(v => v.IsDelete == false);



                if (!string.IsNullOrEmpty(filter.Title))
                {

                    query = query.Where(v => v.Tittle.Contains(filter.Title.Trim()));

                }

            }
            query = query.OrderByDescending(v => v.CreateDate);
            await filter.Paging(UserMapper.MapperProuduct(query));
            return filter;


        }
        #endregion

        #region create
        public async Task CreateProuductAsync(AdminCreateProuductViewModel prouduct)
        {

            string imageName;
            #region Save Avatar

            // اگر کاربر عکس انتخاب کرده باشد
            if (prouduct.FormFile != null &&
                prouduct.FormFile.Length > 0)
            {
                if (!prouduct.FormFile.ImageValid())
                {
                    return;
                }

                // خیلی مهم:
                // نام فایل ذخیره شده را داخل Avatar قرار می‌دهیم
                imageName = await SaveImageFile(prouduct.FormFile);
            }
            else
            {
                // اگر عکس انتخاب نشده
                imageName = "nophoto.jpg";
            }

            #endregion


            #region mapper
            Prouduct Prouduct = new Prouduct()
            {
                CatgoryId = prouduct.CatgoryId,
                Count = prouduct.Count,
                CreateDate = DateTime.Now,
                Active = prouduct.Active,
                Price = prouduct.Price,
                Review = prouduct.Review,
                DetailReview = prouduct.DetailReview,
                Tittle = prouduct.Tittle,
                ShortDescript = prouduct.ShortDescript,
                ImageName = imageName,
                IsDelete = false,
                Slug = prouduct.Slug,




            };
            #endregion
            _ProuductRepository.Create(Prouduct);


            #region Tags

            if (!string.IsNullOrEmpty(prouduct.Tags))
            {

                var tags = JsonSerializer.Deserialize<List<ProuductTagsViewModel>>(prouduct.Tags);



                await _ProuductRepository.AddProuductTagsAsync(Prouduct.Id, tags);

            }

            #endregion

            #region Gallery
            if (prouduct.Galleries != null && prouduct.Galleries.Any())
            {

                foreach (var image in prouduct.Galleries)
                {

                    var imagenameGallery = await SaveImageFile(image);

                    ProuductGallery gallery = new ProuductGallery()
                    {
                        Alt = Prouduct.Tittle,
                        CreateDate = DateTime.Now,
                        ImageName = imagenameGallery,
                        IsDelete = false,
                        ProuductId = Prouduct.Id

                    };
                    await _ProuductRepository.AddGallery(gallery);

                }
            #endregion
            }

        }

        #endregion

        #region Edit

        public async Task<AdminEditProuductViewModel> AdminProuductGetForEditAsync(int Id)
        {
            var prouduct = await _ProuductRepository.GetProuductForEdit(Id);
            if (prouduct == null)
            {
                return null;
            }

            AdminEditProuductViewModel model = new AdminEditProuductViewModel()
            {

                Price = prouduct.Price,
                Active = prouduct.Active,
                CatgoryId = prouduct.CatgoryId,
                Count = prouduct.Count,
                DetailReview = prouduct.DetailReview,
                Review = prouduct.Review,
                Tittle = prouduct.Tittle,
                ShortDescript = prouduct.ShortDescript,
                Slug= prouduct.Slug,
                ImageName = prouduct.ImageName,
                ProuductGalleries = prouduct.ProuductGalleries?.ToList(),
                Tags = string.Join(",",
            prouduct.ProuductTags?.Select(x => x.TagTitle)
            ?? Enumerable.Empty<string>())
                
                


            };



            return model;


        }


        public async Task EditProuductAsync(AdminEditProuductViewModel model)
        {
            var prouduct = await _ProuductRepository.GetByIdAsync(model.Id);

            if (prouduct == null)
            {
                return;
            }

            prouduct.Active = model.Active;
            prouduct.CatgoryId = model.CatgoryId;
            prouduct.UpdateDate = DateTime.Now;
            prouduct.DetailReview = model.DetailReview;
            prouduct.Price = model.Price;
            prouduct.Count = model.Count;
            prouduct.Review = model.Review;
            prouduct.Tittle = model.Tittle;
            prouduct.ShortDescript = model.ShortDescript;


            #region Save Image

            if (model.FormFile != null &&
                model.FormFile.Length > 0)
            {
                if (!model.FormFile.ImageValid())
                {
                    return;
                }

                if (!string.IsNullOrEmpty(prouduct.ImageName) &&
                    prouduct.ImageName != "nophoto.jpg")
                {
                    DeleteImage(prouduct.ImageName);
                }

                var imageName = await SaveImageFile(model.FormFile);

                prouduct.ImageName = imageName;
            }

            #endregion


            #region Tags

            await _ProuductRepository.DeleteProuductTags(model.Id);

            if (!string.IsNullOrEmpty(model.Tags))
            {
                var tags = JsonSerializer.Deserialize<List<ProuductTagsViewModel>>(model.Tags);

                if (tags != null && tags.Any())
                {
                    await _ProuductRepository.AddProuductTagsAsync(
                        model.Id,
                        tags
                    );
                }
            }

            #endregion


            #region Gallery

            if (model.Galleries != null &&
                model.Galleries.Any())
            {
                foreach (var image in model.Galleries)
                {
                    var imagenameGallery = await SaveImageFile(image);

                    ProuductGallery gallery = new ProuductGallery()
                    {
                        Alt = model.Tittle,
                        CreateDate = DateTime.Now,
                        ImageName = imagenameGallery,
                        IsDelete = false,
                        ProuductId = model.Id
                    };

                    await _ProuductRepository.AddGallery(gallery);
                }
            }

            #endregion


            _ProuductRepository.Update(prouduct);
        }
        #endregion

        public async Task DeleteImageGallery(int id)
        {
        var gallery=   await _ProuductRepository.GetGalleryById(id);

            DeleteImage(gallery.ImageName);

            await _ProuductRepository.DeleteGallery(id);

         await   _ProuductRepository.SaveAsync();

        }



        private void DeleteImage(string imageName)
        {
            string deletePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "ProuductImages",
                imageName
            );

            if (File.Exists(deletePath))
            {
                File.Delete(deletePath);
            }
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
                "ProuductImages"
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

            ImageResizer resizer = new ImageResizer();
            var thumbpath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/ProuductImages/Thumb", avatarName);



            resizer.ImageResize(savePath, thumbpath, 120, 210);


            return avatarName;
        }

        



        #endregion

public async Task<IEnumerable<Prouduct>> GetProuductForShowAsync()
        {
         return await _ProuductRepository.GetProuductForShowAsync();



        }

        public async Task<Prouduct> GetProuductForShowModalByIdAsync(int id)
        {
         return await _ProuductRepository.GetProuductForShowModalByIdAsync(id);
        }

        public async Task<IEnumerable<Prouduct>> GetProuductByCatgorySlugAsync(string Slug)
        {
          return await _ProuductRepository.GetProuductByCatgorySlug(Slug);
        }

        public async Task<Prouduct> GetProuductPage(int Id)
        {
            return await _ProuductRepository.GetProuductPage(Id);
        }

        public async Task<bool> IsExistSlugAsync(string slug)
        {
      return await _ProuductRepository.IsExistSlugAsync(slug);
        }
    }
}
