using Newtonsoft.Json.Linq;
using WebScrapper.Helpers;
using WebScrapper.Interfaces;
using WebScrapper.Models;

namespace WebScrapper.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ProductService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<Product> GetProductFromUrl(string url)
        {
            var asin = AmazonHelper.ExtractAsin(url);

            if (string.IsNullOrEmpty(asin))
                throw new Exception("Invalid Amazon URL");

            var apiKey = _config["ScraperApi:Key"];

            var apiUrl = $"https://api.rainforestapi.com/request?api_key={apiKey}&type=product&amazon_domain=amazon.com&asin={asin}";

            var response = await _httpClient.GetStringAsync(apiUrl);

            var json = JObject.Parse(response);

            return new Product
            {
                Title = json["product"]?["title"]?.ToString(),
                Price = json["product"]?["buybox_winner"]?["price"]?["value"]?.ToString(),
                Image = json["product"]?["main_image"]?["link"]?.ToString(),
                Rating = json["product"]?["rating"]?.ToString()
            };
        }
    }
}
