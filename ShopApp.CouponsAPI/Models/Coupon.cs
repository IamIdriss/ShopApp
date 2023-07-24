namespace ShopApp.CouponsAPI.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public float DiscountAmount { get; set; }
    }
}
