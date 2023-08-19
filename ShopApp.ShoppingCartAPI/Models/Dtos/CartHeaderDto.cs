namespace ShopApp.ShoppingCartAPI.Models.Dtos
{
    public class CartHeaderDto
    {

        public int CartHeaderId { get; set; }
        public string UserId { get; set; }

        public string CouponCode { get; set; } = "";
    }
}
