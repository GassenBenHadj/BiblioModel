using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioModel.Models
{
    public class RelationLivreVisiteur
    {
        DateTime _trace;
        Livre _livres;
        Visiteur  _visiteurs;
        
        public RelationLivreVisiteur()
        {
            _trace = DateTime.UtcNow;
        }    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int LivreId { get; set; }
        [JsonIgnore]
        public virtual Livre Livre { get {return _livres; } set { _livres = value; } }
        public int VisiteurId { get; set; }
        [JsonIgnore]
        public virtual Visiteur Visiteur { get { return _visiteurs; } set { _visiteurs = value; } }
        public DateTime Trace { get { return _trace; } private set { _trace = value; } }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
