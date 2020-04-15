using BiblioModel.Cosmos;
using BiblioModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace BiblioModel.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConnectionProviderAttribute :Attribute
    {
        public ProviderType Provider { get; set; }

        public string ConnectionString { get; set; }
   

        public ConnectionProviderAttribute(ProviderType provider)
        {
            provider = Provider;
            MethodInfo configureservice = Type.GetType("Startup").GetMethod("ConfigureService");
            MethodInfo onconfiguring = Type.GetType("BiblioDBContext").GetMethod("OnConfiguring");
            
            if (configureservice!=null)
            {
              var services =  configureservice.GetParameters()[0]
                    as IServiceCollection;
                InjectSevice(services);
            } 
            else
            {
                throw new Exception("Not appropriate method to decorate");
            }

        }
        public void SelectProvider(DbContextOptionsBuilder optionsBuilder)
        {
            switch (Provider)
            {
                case ProviderType.InMemory:
                    optionsBuilder.UseInMemoryDatabase(ConnectionString);
                    break;
                case ProviderType.SqlServer:
                    optionsBuilder.UseSqlServer(ConnectionString);
                    break;
                case ProviderType.PostgreSQL:
                    optionsBuilder.UseNpgsql(ConnectionString);
                    break;
                case ProviderType.CosmosDB:
                    break;
                default:
                    throw new Exception("A provider should be selected");
            }


        }
        public void InjectSevice(IServiceCollection services)
        {
            switch (Provider)
            {
                case ProviderType.InMemory:
                    services.AddDbContext<BiblioDBContext>(options =>
                        services.AddDbContext<BiblioDBContext>(options =>
                                options.UseInMemoryDatabase(ConnectionString)));
                    break;
                case ProviderType.SqlServer:
                    services.AddDbContext<BiblioDBContext>(options =>
                                options.UseSqlServer(ConnectionString));
                    break;
                case ProviderType.PostgreSQL:
                    services.AddDbContext<BiblioDBContext>(options =>
                                options.UseNpgsql(ConnectionString));
                    break;
                case ProviderType.CosmosDB:
                    services.AddScoped(typeof(ICosmosDBService), typeof(CosmosDBService));
                    break;
                default:
                    break;
            }
        }


    }
    public enum ProviderType
    {
        InMemory,SqlServer,PostgreSQL,CosmosDB 
    }
}
