using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductServiceReal.Models;

namespace ProductServiceReal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Laptop", Price = 1200 },
            new Product { ProductId = 2, ProductName = "Phone", Price = 800 }
        };

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            product.ProductId = products.Max(p => p.ProductId) + 1;
            products.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }
    }

}
