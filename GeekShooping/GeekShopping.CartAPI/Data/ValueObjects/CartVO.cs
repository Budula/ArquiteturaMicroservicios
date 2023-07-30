using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GeekShopping.CartAPI.Model;

namespace GeekShooping.CartAPI.Data.ValueObjects
{
    public class CartVO
    {
        public CartHeaderVO CartHeader { get; set; }
        public IEnumerable<CartDetailVO> CartDetails { get; set; }
    }
}
