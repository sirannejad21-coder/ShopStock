using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopStock.Domain.Contracts
{
    public interface IGenrericRepository<T> where T : class 
    {

        IEnumerable<T> GetAll();
       Task< List<T>> GetAllAsync();
        T GetById(object id);
        Task<T> GetByIdAsync(int id);  
        
        void  Create(T entity);
        void Update(T entity);
        void Update(int id);
    
        int Save();
        Task<int> SaveAsync();
        bool Remove(T entity);
        bool Remove(int Id);



        T GetWithIncludes(int id);

        public T Select(Expression<Func<T,bool>> where);
        public Task<T> SelectAsync(Expression<Func<T, bool>> where);







    }
   
}
