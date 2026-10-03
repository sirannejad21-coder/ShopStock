using Microsoft.EntityFrameworkCore;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Prouducts;
using ShopStock.Domain.ViewModels.Prouduct;
using ShopStock.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopStock.Infra.Data.Repository
{
    public class ProuductRepository(EshopDbContext _Contex) : IProuductRepository
    {
        public async Task AddGallery(ProuductGallery gallery)
        {
         await  _Contex.prouductGalleries.AddAsync(gallery);
         await   _Contex.SaveChangesAsync();
        }

        public async Task AddProuductTagsAsync(int prouductid, List<ProuductTagsViewModel> tags)
        {


            foreach (var tag in tags) {
            
            
      await  _Contex.prouductTags.AddAsync(
                
                new ProuductTag() { TagTitle=tag.value, ProuductId=prouductid,}

                
                );

            }
           await _Contex.SaveChangesAsync();

        }

        public void Create(Prouduct entity)
        {
            _Contex.Prouducts.Add(entity);
            _Contex.SaveChanges();
        }

        public async Task DeleteGallery(int Id)
        {
           var gallery=await GetGalleryById(Id);

            if (gallery == null)
            {
                return;
            }

            _Contex.prouductGalleries.Remove(gallery);
        }

        public async Task DeleteProuductTags(int ProuductId)
        {
            var tags = await _Contex.prouductTags
       .Where(i => i.ProuductId == ProuductId)
       .ToListAsync();

            _Contex.prouductTags.RemoveRange(tags);

            await _Contex.SaveChangesAsync();
        }

        public IEnumerable<Prouduct> GetAll()
        {
            return _Contex.Prouducts;
        }

        public async Task< List<Prouduct>> GetAllAsync()
        {
            return await _Contex.Prouducts.ToListAsync();
        }

        public Prouduct GetById(object id)
        {
            return _Contex.Prouducts.Find(id);
        }

        public async Task<Prouduct> GetByIdAsync(int id)
        {
            return await _Contex.Prouducts.SingleOrDefaultAsync(i => i.Id == id);
        }

        public IQueryable<Prouduct> GetFilterProuducts()
        {
            return _Contex.Prouducts.AsQueryable();
        }

        public async Task<ProuductGallery> GetGalleryById(int Id)


        {
            return await _Contex.prouductGalleries.AsNoTracking().FirstOrDefaultAsync(i=>i.Id==Id);
        }

        public async Task<IQueryable< Prouduct>> GetProuductByCatgorySlug(string Slug)
        {

         return   _Contex.Prouducts
                .Include(c=>c.Catgory).Where(c=>c.Catgory.Slug==Slug).AsQueryable();
         
        }

        public async Task<Prouduct> GetProuductForEdit(int ProuductId)
        {
            return await _Contex.Prouducts.Where(v=>v.Id==ProuductId).Include(v => v.ProuductFeatures).Include(v => v.ProuductGalleries)
                .Include(v => v.ProuductColors).Include(v=>v.ProuductTags).SingleOrDefaultAsync();
        }

        public async Task<IQueryable<Prouduct>> GetProuductForShowAsync()
        {
            return  _Contex.Prouducts.Include(c=>c.ProuductColors).OrderByDescending(c=>c.CreateDate).Take(12).AsQueryable();
        }

        public async Task<Prouduct> GetProuductForShowModalByIdAsync(int id)
        {
            return await _Contex.Prouducts.Include(i => i.ProuductColors)
                .Include(i=>i.ProuductFeatures).Include(i=>i.ProuductGalleries).Include(i=>i.Catgory)
                .ThenInclude(i=>i.Parent).SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Prouduct> GetProuductPage(int ProuductId)
        {
            return await _Contex.Prouducts.Where(v => v.Id == ProuductId)
                .Include(v => v.ProuductFeatures).Include(v => v.ProuductGalleries)
           .Include(v => v.ProuductColors).Include(v=>v.Catgory).ThenInclude(v=>v.Parent).ThenInclude(v=>v.Parent)
           .Include(v => v.ProuductTags).SingleOrDefaultAsync();
        }

        public Prouduct GetWithIncludes(int id)
        {
            return _Contex.Prouducts.Include(i => i.ProuductGalleries).Include(i => i.ProuductColors)
                .Include(i => i.ProuductFeatures).SingleOrDefault(i => i.Id == id);
        }

        public async Task<bool> IsExist(int ProuductId)
        {
            return await _Contex.Prouducts.AnyAsync(i => i.Id == ProuductId);

        }

        public async Task<bool> IsExistSlugAsync(string slug)
        {
            return await _Contex.Prouducts
          .AnyAsync(x => x.Slug == slug);
        }

        public bool Remove(Prouduct entity)
        {
            var prouduct = GetById(entity.Id);

            prouduct.IsDelete = true;
            return true;
            Save();
        }

        public bool Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            return _Contex.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _Contex.SaveChangesAsync();


        }

        public Prouduct Select(Expression<Func<Prouduct, bool>> where)
        {
            return _Contex.Prouducts.FirstOrDefault(where);
        }

        public async Task<Prouduct> SelectAsync(Expression<Func<Prouduct, bool>> where)
        {
            return await _Contex.Prouducts.FirstOrDefaultAsync(where);
        }

        public void Update(Prouduct entity)
        {
           _Contex.Prouducts.Update(entity);
            _Contex.SaveChanges();
        }

        public void Update(int id)
        {
          var product=  GetById(id);
            Update(product);
        }
    }
}