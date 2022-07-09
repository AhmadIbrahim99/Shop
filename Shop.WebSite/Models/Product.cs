using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shop.WebSite.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Maker { get; set; }
        [JsonPropertyName("img")]
        public string Image { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int[] Rating { get; set; }
        public override string ToString() => JsonSerializer.Serialize<Product>(this);

    }
}
