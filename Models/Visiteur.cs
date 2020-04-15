//#define lazy
#define notlazy
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace BiblioModel.Models
{
    public partial class Visiteur
    {

#if lazy
        private readonly ILazyLoader _lazyLoader;
        private ICollection<RelationLivreVisiteur> _relationlivreVisiteurs;
        public Visiteur(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        [JsonIgnore]
        public virtual ICollection<RelationLivreVisiteur> RelationLivreVisiteurs
        {
            get { return _lazyLoader.Load(this, ref _relationlivreVisiteurs); }
            set { _relationlivreVisiteurs = value; }
        }

#endif
        [JsonProperty(PropertyName = "id")]
        public int Id {
            get { return _id; }
            private set { _id = value; } 
        }
        public string Nom { get; set; }
        public string Discriminator { get; private set; }
#if notlazy
        public virtual ICollection<RelationLivreVisiteur> RelationLivreVisiteurs
        {
            get; set;
        }
    }
#endif
}



