using Microsoft.EntityFrameworkCore;
using ShopStock.Domain.Contracts;
using ShopStock.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Infra.Data.Repository
{


    public class GenericRepository<T> : IGenrericRepository<T> where T : class
    {


        EshopDbContext _Context;
        readonly DbSet<T> _dbSet;

        public GenericRepository(EshopDbContext context)
        {
            _Context = context;
            _dbSet = _Context.Set<T>();
        }
        public void Create(T entity)
        {
            _Context.Add(entity);
            Save();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task< List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T GetWithIncludes(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T entity)
        {
         _dbSet.Remove(entity);
            Save();
            return true;
        }

        public int Save()
        {
         return  _Context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
          return await _Context.SaveChangesAsync();
        }

        public T Select(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Task<T> SelectAsync(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
           _dbSet.Update(entity);
            Save();
        }

        public void Update(int id)
        {
            T item = GetById(id);
            Update(item);

        }

        public bool Remove(int Id)
        {
            T item = GetById(Id);
            try
            {

                Remove(item);
                return true;
            }
            catch { return false; }

        }
    }
}
