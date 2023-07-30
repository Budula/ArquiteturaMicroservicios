using GeekShooping.ProductAPI.Data.ValueObjects;
using GeekShooping.ProductAPI.Repository;
using GeekShooping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new
                        ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _productRepository.FindAll();            
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _productRepository.FindById(id);
            if(product == null) return NotFound();
            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO product)
        {
            if (product == null) return BadRequest();
            product = await _productRepository.Create(product);            
            return Ok(product);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO product)
        {
            if (product == null) return BadRequest();
            product = await _productRepository.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductVO>> Delete(long id)
        {
            var status = await _productRepository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}
