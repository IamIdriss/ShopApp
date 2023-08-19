using RabbitMQ.Client;
using ShopApp.OrdersAPI.Repository;

namespace ShopApp.OrdersAPI.Services
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _orderRepository;
        
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQCheckoutConsumer(OrderRepository orderRepository
            )
        {
            _orderRepository = orderRepository;
            
            _hostname = "localhost";
            _username = "guest";
            _password = "guest";
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("checkoutmessagequeue", false, false, false);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
