using System;
namespace BiblioModel.Models
{
    
    public class Utilisateur:Visiteur
    { 
        public string Profil { get; set; }
        public int SessionsParJour { get; set; }
    }
}


