﻿using GeekShooping.CartAPI.Data.ValueObjects;

namespace GeekShopping.CartAPI.Repository
{
    public interface ICartRepository
    {        
        Task<CartVO> SaveOrUpdateCart(CartVO cartVo);
        Task<bool> RemoveFromCart(long cartDetailsId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
        Task<CartVO> FindCartByUserId(string userId);       
    }
}
