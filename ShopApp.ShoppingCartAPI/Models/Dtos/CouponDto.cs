namespace ShopApp.ShoppingCartAPI.Models.Dtos
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public float DiscountAmount { get; set; }
    }
}
