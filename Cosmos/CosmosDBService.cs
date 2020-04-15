using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using BiblioModel.Models;
using System.Diagnostics;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BiblioModel.Cosmos
{
    public class CosmosDBService : ICosmosDBService
    {
        CosmosClient _cosmosClient;
        public string DatabaseName { get; set; }
        private Database _database;
        public string ContainerName { get; set; }
        private Container _container;

        public CosmosDBService()
        {

        }
        private async Task CreateDatabaseAsync()
        {
            _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);
            Debug.WriteLine($"Created Database: {0}\n", _database.Id);
        }

        public async Task StartServiceAsync()
        {
            _cosmosClient = new CosmosClient("https://localhost:8081",
               "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw=="
               );
            await CreateDatabaseAsync();
            await CreateContainerAsync();
        }



        private async Task CreateContainerAsync()
        {
            _container = await _database.CreateContainerIfNotExistsAsync(ContainerName, "/Visiteurs");
            Debug.WriteLine($"Created Container: {0}\n", _container.Id);
        }

        private async Task AddVisiteurToContainerAsync<T>(T visiteur) where T : Visiteur
        {
            try
            {
                // Read the item to see if it exists.  
                ItemResponse<T> itemResponse = await _container.CreateItemAsync<T>(visiteur);
                Debug.WriteLine($"Visiteur id: {0} vient d'être ajouté", itemResponse.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                Debug.WriteLine(ex.InnerException);
            }
        }
        public async Task AddVisiteurAsync<T>(T visiteur) where T : Visiteur
        {
            await AddVisiteurToContainerAsync(visiteur);
        }



        private async Task<List<Visiteur>> QueryItemsAsync(string query)
        {
            var sqlQueryText = (query == null) ? "SELECT * FROM c" : query;

            Debug.WriteLine($"query: {0}", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Visiteur> queryResultSetIterator = _container.GetItemQueryIterator<Visiteur>(queryDefinition);

            List<Visiteur> visiteurs = new List<Visiteur>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Visiteur> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (var visiteur in currentResultSet)
                {
                    visiteurs.Add(visiteur);
                    Debug.WriteLine("\tAjouter {0}\n", visiteur);
                }
            }

            return visiteurs;
        }
        public async Task<List<Visiteur>> QueryAsync(string query)
        {
            var visiteurs = await QueryItemsAsync(query);
            return visiteurs;
        }

        private async Task ReplaceVisiteurItemAsync(Visiteur newVisiteur)
        {
            ItemResponse<Visiteur> wakefieldvisiteurResponse = await _container.ReadItemAsync<Visiteur>(newVisiteur.Id.ToString(), new PartitionKey(newVisiteur.Id));
            var itemBody = wakefieldvisiteurResponse.Resource;
            itemBody = newVisiteur;


            // replace the item with the updated content
            wakefieldvisiteurResponse = await _container.ReplaceItemAsync<Visiteur>(itemBody, itemBody.Id.ToString(), new PartitionKey(itemBody.Id));
            Debug.WriteLine("Updated Visiteur id: [{0},{1}].\n \tBody is now: {2}\n", itemBody.Id);
        }
        public async Task ReplaceVisiteurAsync(Visiteur newVisiteur)
        {
            await ReplaceVisiteurItemAsync(newVisiteur);
        }


        private async Task DeleteVisiteurItemAsync(Visiteur visiteur)
        {
            FeedIterator<string> feedIterator = _container.GetItemLinqQueryable<Visiteur>()
                .Where(x => x.Id == visiteur.Id)
                .Select(x => x.Id) as FeedIterator<string>;

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<string> results = await feedIterator.ReadNextAsync();
                foreach (var id in results)
                {
                    await _container.DeleteItemAsync<Visiteur>(id, new PartitionKey(id));
                }
            }


            //ItemResponse<Visiteur> wakefieldvisiteurResponse = await _container.DeleteItemAsync<Visiteur>(visiteur.Id, visiteur.PartitionKey);
            Debug.WriteLine("Suppression visiteur id: {0}\n", visiteur.Id);
        }
        public async Task DeleteVisiteurAsync(Visiteur visiteur)
        {
            await DeleteVisiteurItemAsync(visiteur);
        }



        private async Task DeleteDatabaseAndCleanupAsync()
        {
            DatabaseResponse databaseResourceResponse = await _database.DeleteAsync();
            Debug.WriteLine("Suppression Database: {0}\n", _database.Id);
            _cosmosClient.Dispose();
        }
        private async Task DeleteDatabaseAsync()
        {
            await DeleteDatabaseAndCleanupAsync();
        }
    }
}
