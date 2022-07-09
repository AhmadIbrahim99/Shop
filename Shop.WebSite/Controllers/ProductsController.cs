using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.WebSite.Models;
using Shop.WebSite.Services;
using System.Collections.Generic;

namespace Shop.WebSite.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public JsonFileProductService ProductService { get; }

        public ProductsController(JsonFileProductService productService) 
        {
            this.ProductService = productService;
        }
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return ProductService.GetProducts();
        }
        [Route("Rate")]
        [HttpGet]
        public IActionResult Get
            (
            [FromQuery]string ProductId, 
            [FromQuery]int Rating
            ) 
        {
            ProductService.AddRating(ProductId, Rating);
            return Ok();
        }
    }
}
