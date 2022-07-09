using Microsoft.AspNetCore.Hosting;
using Shop.WebSite.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Shop.WebSite.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }
        private string JsonFileName { get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); } }
        public IEnumerable<Product> GetProducts()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Product[]>(
            jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                    );
            }
        }
        public void AddRating(string productId, int rating)
        {
            var produts = GetProducts();
            //LINQ
            var query = produts.First(x => x.Id == productId);
            if (query.Rating == null) { 
                query.Rating = new int[] { rating };
            }
            else 
            {
                var Ratings = query.Rating.ToList();
                Ratings.Add(rating);
                query.Rating = Ratings.ToArray();
            }
            using (var outPutStream = File.OpenWrite(JsonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outPutStream, new JsonWriterOptions {
                        SkipValidation = true, Indented = true }),
                    produts);
            }
        }

    }
}
