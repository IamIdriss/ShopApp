namespace ShopApp.ShoppingCartAPI.RabbitMQSender
{
    public interface IRabbitMQCheckoutMessageSender
    {
        void SendMessage(BaseMessage message, string queueName);
    }
}
