using Microsoft.EntityFrameworkCore;
namespace BiblioModel.Models
{
    public interface IUnitOfWork
    {
        DbContext Context { get;}
        void RegisterAdd<T>(T entité) where T : class;
        void RegisterDelete<T>(T entité) where T : class;
        void RegisterModifiy<T>(T entité) where T : class;
        void RegisterUnchange<T>(T entité) where T : class;
        void Commit();
    }
}

