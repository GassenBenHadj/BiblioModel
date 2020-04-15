using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiblioModel.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /*Opérations CRUD */
        IAsyncEnumerable<T> GetAll();
        Task<T> Find(int Id);
        IAsyncEnumerable<T> FindAll(Func<T, bool> predicate);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        /*Avoir le nombre total*/
        Task<int> Count();
    }
}
