
using Microsoft.EntityFrameworkCore;

namespace BiblioModel.DAL.Implementations
{
    static public class LivreRepositoryExtensions
    {
        static public int MaxPrix(this LivreRepository rep)
          => rep.Context.Database.ExecuteSqlRaw("sp_MaxPrix", null);

        static public int MinPrix(this LivreRepository rep)
            => rep.Context.Database.ExecuteSqlRaw("sp_MinPrix", null);

        static public int AvgPrix(this LivreRepository rep)
             => rep.Context.Database.ExecuteSqlRaw("sp_AvgPrix", null);
    }
}
