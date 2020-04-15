using Microsoft.EntityFrameworkCore;
namespace BiblioModel.DAL.Implementations
{
    public static class VisiteurRepositoryExtensions
    {
        static public int MaxSessionParJour(this VisitieurRepository rep)
           => rep.Context.Database.ExecuteSqlRaw("sp_MaxSessionParJour", null);
        
        static public int MinSessionParJour(this VisitieurRepository rep)
            => rep.Context.Database.ExecuteSqlRaw("sp_MinSessionParJour", null);

        static public int AvgSessionParJour(this VisitieurRepository rep)
             => rep.Context.Database.ExecuteSqlRaw("sp_AvgSessionParJour", null);

        static public int CountSessionParJour(this VisitieurRepository rep)
             => rep.Context.Database.ExecuteSqlRaw("sp_CountSessionParJour", null);
    }
}
