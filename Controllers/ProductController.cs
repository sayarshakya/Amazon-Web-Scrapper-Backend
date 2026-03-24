using Microsoft.AspNetCore.Mvc;
using WebScrapper.Interfaces;
using WebScrapper.Models;

namespace WebScrapper.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> GetProduct([FromBody] ProductRequest request)
        {
            var result = await _service.GetProductFromUrl(request.Url);
            return Ok(result);
        }
    }
}
