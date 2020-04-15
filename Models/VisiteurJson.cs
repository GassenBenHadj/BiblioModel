using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioModel.Models
{
    public partial class Visiteur
    {
        private string _partitionkeystr;
        private PartitionKey _partitionkey;

        private int _id;

        public Visiteur()
        {
            _id = this.GetHashCode();
            _partitionkeystr = $"{Id}_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}";
            _partitionkey = new PartitionKey(_partitionkeystr);
        }

        [JsonProperty(PropertyName = "partitionKey")]
        [NotMapped]
        public PartitionKey PartitionKey
        {
            get { return _partitionkey; }
            private set { _partitionkey = value; }
        }



        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
