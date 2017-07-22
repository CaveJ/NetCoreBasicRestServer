using Newtonsoft.Json;

namespace librarysample.Model
{
    public sealed class Book
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("author")]
        public string Author {get; set; }
        [JsonProperty("isbn")]
        public string ISBN {get; set;}
        [JsonProperty("id")]
        public int ID {get; set;}
    }
}