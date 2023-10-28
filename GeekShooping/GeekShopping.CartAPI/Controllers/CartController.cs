using GeekShooping.CartAPI.Data.ValueObjects;
using GeekShopping.CartAPI.Messages;
using GeekShopping.CartAPI.RabbitMQSender;
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
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository repository, IRabbitMQMessageSender rabbitMQMessage)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this._rabbitMQMessageSender = rabbitMQMessage ?? throw new ArgumentNullException(nameof(rabbitMQMessage));
        }

        [HttpGet("find-cart/{id}")]
        [Authorize]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            var cart = await _repository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [Authorize]
        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO cartVo)
        {
            if (cartVo == null) return BadRequest();
            cartVo = await _repository.SaveOrUpdateCart(cartVo);
            return Ok(cartVo);
        }

        [Authorize]
        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO cartVo)
        {
            if (cartVo == null) return BadRequest();
            cartVo = await _repository.SaveOrUpdateCart(cartVo);
            return Ok(cartVo);
        }

        [HttpDelete("remove-cart/{id}")]
       // [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<CartVO>> Delete(long id)
        {
            var status = await _repository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO cartVo)
        {
            if (cartVo == null) return BadRequest();
            var status = await _repository.ApplyCoupon(cartVo.CartHeader.UserId, cartVo.CartHeader.CouponCode);
            if (!status) return NotFound();
            return Ok(status);
        }
        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
        {
            var status = await _repository.RemoveCoupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }
        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
            if(vo?.UserId == null) return BadRequest();
            var cart = await _repository.FindCartByUserId(vo.UserId);
            if (cart == null) return NotFound();
            vo.CartDetails = cart.CartDetails;
            vo.Time = DateTime.Now;
            //TASK RabbitMQ logic comes here!!
            _rabbitMQMessageSender.SendMessage(vo, "checkoutqueue");
            return Ok(vo);
        }
    }
}