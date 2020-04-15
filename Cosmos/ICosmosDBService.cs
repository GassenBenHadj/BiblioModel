using BiblioModel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiblioModel.Cosmos
{
    public interface ICosmosDBService
    {
        string ContainerName { get; set; }
        string DatabaseName { get; set; }

        Task AddVisiteurAsync<T>(T visiteur) where T : Visiteur;
        Task DeleteVisiteurAsync(Visiteur visiteur);
        Task<List<Visiteur>> QueryAsync(string query);
        Task ReplaceVisiteurAsync(Visiteur newVisiteur);
        Task StartServiceAsync();
    }
}