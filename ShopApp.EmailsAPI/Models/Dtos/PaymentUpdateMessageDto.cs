namespace ShopApp.EmailsAPI.Models.Dtos
{
    public class PaymentUpdateMessageDto
    {
        public int OrderId { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
