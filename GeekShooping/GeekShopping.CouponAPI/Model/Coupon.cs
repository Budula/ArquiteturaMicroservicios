using GeekShooping.CouponAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponAPI.Model
{
    [Table("coupon")]
    public class Coupon : BaseEntity
    {   
        [Column("counpon_code")]
        [Required]
        [StringLength(30)]
        public string CounponCode { get; set; }

        [Column("discount_amount")]
        [Required]       
        public decimal DiscountAmount { get; set; }

    }
}
