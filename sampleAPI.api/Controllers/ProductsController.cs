using Microsoft.AspNetCore.Mvc;
using sampleAPI.Models;
using sampleAPI.Repositories;

namespace sampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {
            await _repository.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _repository.DeleteProduct(id);
            return Ok();
        }
    }
}
