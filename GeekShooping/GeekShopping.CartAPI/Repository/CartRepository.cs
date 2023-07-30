using AutoMapper;
using GeekShooping.CartAPI.Data.ValueObjects;
using GeekShooping.CartAPI.Model.Context;
using GeekShopping.CartAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public CartRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeader = await _context.CartHeader.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cartHeader != null)
            {
                _context.CartDetail.RemoveRange(
                    _context.CartDetail.Where(c => c.CartHeaderId == cartHeader.Id)
                    );
                _context.CartHeader.Remove(cartHeader);
                _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _context.CartHeader.FirstOrDefaultAsync(c => c.UserId == userId),
            };
            cart.CartDetails = _context.CartDetail
                .Where(c => c.CartHeaderId == cart.CartHeader.Id).Include(c => c.Product);
            return  _mapper.Map<CartVO>(cart);
        }

        public Task<CartVO> GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _context.CartDetail.FirstOrDefaultAsync(c=> c.Id == cartDetailsId);
                int total = _context.CartDetail.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();
                _context.CartDetail.Remove(cartDetail);
                if(total == 1)
                {
                    var cartHeaderToRemove = await _context.CartHeader.FirstOrDefaultAsync(c=>c.Id == cartDetail.CartHeaderId);
                    _context.CartHeader.Remove(cartHeaderToRemove);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO cartVo)
        {
            Cart cart = _mapper.Map<Cart>(cartVo);
            var product = await _context.Products.FirstOrDefaultAsync(
                        p => p.Id == cartVo.CartDetails.FirstOrDefault().ProductId);
            if (product == null)
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }
            var cartHeader = await _context.CartHeader.AsNoTracking().FirstOrDefaultAsync(
                c => c.UserId == cart.CartHeader.UserId);
            if(cartHeader== null) 
            {
                _context.CartHeader.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetail.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                var cartDetail = await _context.CartDetail.AsNoTracking().FirstOrDefaultAsync(
                    p=> p.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    p.CartHeaderId == cartHeader.Id);
                if (cartDetail == null)
                {                  
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetail.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    _context.CartDetail.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartVO>(cart);
        }
    }
}
