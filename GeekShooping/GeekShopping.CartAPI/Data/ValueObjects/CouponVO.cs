using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeekShooping.CartAPI.Data.ValueObjects
{
    public class CouponVO
    {
        public long Id { get; set; }
        public string CounponCode { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
