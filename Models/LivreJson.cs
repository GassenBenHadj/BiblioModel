using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioModel.Models
{
    public partial class Livre
    {
        #region champs
        private string _partitionkey;
        int _tailleforjson = 1;
        #endregion


        [JsonProperty(PropertyName = "partitionKey")]
        [NotMapped]
        public string Partitionkey { get => _partitionkey; set => _partitionkey = value; }

        [NotMapped]
        public int TailleForJson
        {
            get { return _tailleforjson; }
            private set
            {
                switch (_tailleLivre)
                {
                    case TailleLivre.Petit:
                        _tailleforjson = 1;
                        break;
                    case TailleLivre.Moyen:
                        _tailleforjson = 2;
                        break;
                    case TailleLivre.Grand:
                        _tailleforjson = 3;
                        break;
                    default:
                        _tailleforjson = 1;
                        break;
                }
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
