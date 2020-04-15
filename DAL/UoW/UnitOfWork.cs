using Microsoft.EntityFrameworkCore;

namespace BiblioModel.Models
{
    public class UnitOfWork<U> : IUnitOfWork where U : DbContext
    {
        public DbContext Context { get; private set; }

        public UnitOfWork(U context)
            => Context = context;

        public void Commit()=> Context.SaveChanges();
     
        public void RegisterAdd<T>(T entité) where T : class
         =>Context.Add<T>(entité);
        

        public void RegisterDelete<T>(T entité) where T : class
        =>Context.Remove<T>(entité);
        
        public void RegisterModifiy<T>(T entité) where T : class
        =>Context.Entry<T>(entité).State = EntityState.Modified;
        
        public void RegisterUnchange<T>(T entité) where T : class
        =>Context.Entry<T>(entité).State = EntityState.Unchanged;
        
    }
}


