using ShopApp.MessageBus;

namespace ShopApp.PaymentsAPI.RabbitMQSender
{
    public interface IRabbitMQPaymentResultMessageSender
    {
        void SendMessage(BaseMessage message);
    }
}
