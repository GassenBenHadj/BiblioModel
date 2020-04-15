#define SQLSERVER
//#define POSTGRESQL
//#define INMEMORY
//#define COSMOSDB
//#define lazymode
using Microsoft.EntityFrameworkCore;
namespace BiblioModel.Models
{
    public class BiblioDBContext:DbContext
    {

        public BiblioDBContext()
        {

        }
        public BiblioDBContext(DbContextOptions<BiblioDBContext> options) :base(options)
        {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if SQLSERVER
            optionsBuilder.UseSqlServer(@"Data Source=PC2020\BI2020;Initial Catalog=BiblioDBV2;" +
                                                                "Integrated Security=True");
#endif
#if POSTGRESQL
             optionsBuilder.UseNpgsql(@"Host=localhost;Database=bibliodb;" +
                                                            "Username=postgres;Password=123");
#endif
#if INMEMORY
             optionsBuilder.UseInMemoryDatabase(databaseName: "Auteurs", null);
#endif
#if COSMOSDB
            //Nothing to code here
#endif
#if lazymode
            optionsBuilder.UseLazyLoadingProxies();
#endif

        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelationLivreVisiteur>()
                .HasKey(lv => new { lv.LivreId, lv.VisiteurId });

            modelBuilder.Entity<RelationLivreVisiteur>()
                 .HasOne(lv => lv.Visiteur)
                 .WithMany(l => l.RelationLivreVisiteurs)
                 .HasForeignKey(lv => lv.LivreId);

            modelBuilder.Entity<RelationLivreVisiteur>()
                 .HasOne(lv => lv.Livre)
                 .WithMany(l => l.RelationLivreVisiteurs)
                 .HasForeignKey(lv => lv.VisiteurId);

            modelBuilder.Entity<Livre>()
                .Property<int>("Id")
                .UseIdentityColumn();


            modelBuilder.Entity<Moderateur>();
            modelBuilder.Entity<Auteur>();
            modelBuilder.Entity<Utilisateur>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Visiteur> Visiteurs { get; set; }
        public DbSet<Livre> Livres { get; set; }
        public DbSet<RelationLivreVisiteur> RelationLivreVisiteurs { get; set; }
    }
}













