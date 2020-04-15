//#define lazy
#define notlazy
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace BiblioModel.Models
{
    public partial class Livre
    {
        #region champs
        int _id;
        TailleLivre _tailleLivre;  
        #endregion
      
        public Livre()
        {
            //_id = GetHashCode();
            _partitionkey = $"{_id}_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}";
            
        }
#if lazy
        private readonly ILazyLoader _lazyLoader;
        private ICollection<RelationLivreVisiteur> _relationlivreVisiteurs;
        public Livre(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public virtual ICollection<RelationLivreVisiteur> RelationLivreVisiteurs
        {
            get
            { 
                return  _lazyLoader.Load(this, ref _relationlivreVisiteurs);
            }
            set { _relationlivreVisiteurs = value; }
        }
#endif
#if notlazy
       
        public virtual ICollection<RelationLivreVisiteur> RelationLivreVisiteurs
        {get;set;}
#endif



        [JsonProperty(PropertyName = "id")]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
            
        } 
        public string Titre { get; set; }
        public int Prix { get; internal set; }
        [JsonIgnore]
        public TailleLivre Taille {
            get { return _tailleLivre; }
            set { _tailleLivre = value; }
        }
        
    }
}
