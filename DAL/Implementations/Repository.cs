using BiblioModel.DAL.Interfaces;
using BiblioModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioModel.DAL.Implementations
    {
        abstract public class Repository<T> : IRepository<T> where T : class
        {
  
            public BiblioDBContext Context { get;}

            public Repository(BiblioDBContext context)
            {
                Context = context;
            }

            protected void Save() => Context.SaveChanges();
            //Context.Set<T>().Count();
            abstract public Task<int> Count();

            async public virtual IAsyncEnumerable<T> FindAll(Func<T, bool> predicate) 
            {
                //IAsyncEnumerable<T> result = Context.Set<T>().Where(predicate) as IAsyncEnumerable<T>;
                var result = await GetAll().ToListAsync();
                foreach (var item in result)
                {
                    if (predicate.Invoke(item) != true)
                       continue;
                    else
                       yield return item;
                }
            }

            //Context.Add(entity);
            //Save();
            abstract public void Create(T entity);

                //Context.Remove(entity);
                //Save();
                abstract public void Delete(T entity);

                //Context.Set<T>().Where(predicate);
                abstract public Task<T> Find(int id);

                //Context.Set<T>();
                abstract public IAsyncEnumerable<T> GetAll();

                //Context.Entry(entity).State = EntityState.Modified;
                //Save();
                abstract public void Update(T entity);
            }
    }
