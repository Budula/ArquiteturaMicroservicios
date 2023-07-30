using GeekShooping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _repository;

        public CartController(ICartRepository cartRepository)
        {
            _repository = cartRepository ?? throw new
                        ArgumentNullException(nameof(cartRepository));
        }

        [HttpGet("find-cart/{id}")]
        [Authorize]
        public async Task<ActionResult<CartVO>> FindById(string userId)
        {
            var cart = await _repository.FindCartByUserId(userId);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [Authorize]
        [HttpPost("add-cart")]
        public async Task<ActionResult<ProductVO>> AddCart([FromBody] CartVO cartVo)
        {
            if (cartVo == null) return BadRequest();
            cartVo = await _repository.SaveOrUpdateCart(cartVo);
            return Ok(cartVo);
        }

        [Authorize]
        [HttpPut("update-cart")]
        public async Task<ActionResult<ProductVO>> UpdateCart([FromBody] CartVO cartVo)
        {
            if (cartVo == null) return BadRequest();
            cartVo = await _repository.SaveOrUpdateCart(cartVo);
            return Ok(cartVo);
        }

        [HttpDelete("remove-cart{id}")]
       // [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductVO>> Delete(long id)
        {
            var status = await _repository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}