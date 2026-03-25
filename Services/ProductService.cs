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
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip
                           | System.Net.DecompressionMethods.Deflate
            };

            var httpClient = new HttpClient(handler);

            httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            var html = await httpClient.GetStringAsync(url);

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var title = doc.DocumentNode
                .SelectSingleNode("//span[@id='productTitle']")
                ?.InnerText.Trim();

            var price = doc.DocumentNode
                .SelectSingleNode("//span[@class='a-price-whole']")
                ?.InnerText;

            var image = doc.DocumentNode
                .SelectSingleNode("//img[@id='landingImage']")
                ?.GetAttributeValue("src", "");

            return new Product
            {
                Title = title,
                Price = price,
                Image = image
            };

        }
    }
}
