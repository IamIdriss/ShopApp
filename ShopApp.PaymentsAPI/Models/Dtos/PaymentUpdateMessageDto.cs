using ShopApp.MessageBus;

namespace ShopApp.PaymentsAPI.Models.Dtos
{
    public class PaymentUpdateMessageDto :BaseMessage
    {
        public int OrderId { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
