using ShopApp.ShoppingCartAPI.Models.Dto;

namespace ShopApp.ShoppingCartAPI.Messages
{
    public class CheckoutMessageDto
    {
        public int CartHeaderId { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; } = "";
        public double OrderTotal { get; set; }
        public double DiscountTotal { get; set; } = 0;
        public double GrandTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
        public IEnumerable<CartDetailsDto> CartDetails { get; set; }
    }
}
