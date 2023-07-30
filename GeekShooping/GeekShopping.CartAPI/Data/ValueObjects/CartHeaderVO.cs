using GeekShooping.CartAPI.Model.Base;

namespace GeekShooping.CartAPI.Data.ValueObjects
{

    public class CartHeaderVO : BaseEntity
    {
        public string UserId { get; set; }

        public string CouponCode { get; set; }
    }
}
