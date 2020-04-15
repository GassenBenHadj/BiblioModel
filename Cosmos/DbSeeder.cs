using BiblioModel.Models;
using System;
using System.Collections.Generic;
namespace BiblioModel.Cosmos
{
    static public class DbSeeder
    {
        static string[] names = { "Talha Vinson", "Daniella Gilbert", "Rowena Lynch",
                            "Jaxon Edge", "Amir Felix", "Taran Greig", "Alessandro Bradley","Minahil Callahan",
                            "Kris Salinas","Aayan Baxter","Nadir Carr","Terrell Watt","Isha Driscoll",
                            "Alina Houston","Juno Hackett"};

        static string[] titles = { "Thief Of Dread",
                            "Warrior Of Despair",
                            "Aliens Of The Eclipse",
                            "Gods Of My Imagination",
                            "Criminals And Strangers",
                            "Armies And Enemies",
                            "Honor Of The Stars",
                            "Crossbow Of History",
                            "Hiding The Titans",
                            "Faith Of A Storm",
                            "Guardian Of The Great",
                            "Butcher With Wings",
                            "Witches Of Silver",
                            "Lords Of Fire",
                            "Enemies And Children",
                            "Agents And Criminals",
                            "Sword Of The Night",
                            "Argument Of Sorrow",
                            "Shelter At The Leaders",
                            "Question The Emperor",
                            "Army Of Heaven",
                            "Duke Of Perfection",
                            "Invaders Of The Banished",
                            "Dogs Of The Eternal",
                            "Snakes And Cats",
                            "Butchers And Pirates",
                           "Unity Of The Great",
                            "Creation Of The Forsaken",
                            "Forsaking The Nation",
                            "Changing Dreams"
        };

        static public IEnumerable<Visiteur> Seed()
        {
            int cursor = 0;
            List<Visiteur> liste = new List<Visiteur>();
            foreach (var name in names)
            {
                var random = new Random().Next(0,1);
                Visiteur visiteur = new Visiteur
                {
                    Nom = name,
                    Livres = new Livre[]
                    {
                        new Livre{Prix= new Random().Next(50,120),
                            Titre=titles[cursor], Taille =new Random().Next(1,3)},
                         new Livre{Prix= new Random().Next(50,120),
                            Titre=titles[++cursor], Taille =new Random().Next(1,3)},
                    }
                };
                cursor++;
                yield return visiteur;
            }
        }
    }
}
