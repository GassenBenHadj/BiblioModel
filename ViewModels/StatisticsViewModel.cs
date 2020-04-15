using BiblioModel.DAL.Implementations;

namespace BiblioModel.ViewModels
{
    
    public class StatisticsViewModel
    {

#pragma warning disable 0649  
        VisitieurRepository _visiteurRepository;
        LivreRepository _livreRepository;
#pragma warning restore 0649

        public int AverageSessionParJour() => _visiteurRepository.AvgSessionParJour();
        public int MaxSessionParJour() => _visiteurRepository.MaxSessionParJour();
        public int MinSessionParJour() => _visiteurRepository.MinSessionParJour();
        public int CountSessionParJour() => _visiteurRepository.CountSessionParJour();
        public int AveragePrix() => _livreRepository.AvgPrix();
        public int MaxPrix() => _livreRepository.MaxPrix();
        public int MinPrix() => _livreRepository.MinPrix();
    }
}
