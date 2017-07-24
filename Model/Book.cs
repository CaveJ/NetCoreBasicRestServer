using Newtonsoft.Json;

namespace librarysample.Model
{
    public sealed class Book
    {
        public Book()
        {
            ID = -1;
        }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("isbn")]
        public string ISBN { get; set; }
        [JsonProperty("id")]
        public int ID { get; set; }
    }
}