using WebScrapper.Models;

namespace WebScrapper.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductFromUrl(string url);
    }
}
