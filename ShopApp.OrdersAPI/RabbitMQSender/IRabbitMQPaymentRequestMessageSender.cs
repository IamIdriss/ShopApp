using ShopApp.MessageBus;

namespace ShopApp.OrdersAPI.RabbitMQSender
{
    public interface IRabbitMQPaymentRequestMessageSender
    {
        void SendMessage(BaseMessage message, string queueName);
    }
}
