using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using ShopApp.OrdersAPI.Repository;
using System.Text;
using Newtonsoft.Json;
using ShopApp.OrdersAPI.Models.Dtos;

namespace ShopApp.OrdersAPI.Services
{
    public class RabbitMQPaymentConsumer :BackgroundService
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly OrderRepository _orderRepository;
        private IConnection _connection;
        private IModel _channel;
        //private const string _fanoutExchangeName = "Fanout_Payment_Update_Exchange";
        private const string _directExchangeName = "Direct_Payment_Update_Exchange";
        private const string _paymentOrderQueueName = "Payment_Order_Queue";
        string queueName = "";

        public RabbitMQPaymentConsumer(OrderRepository orderRepository)
        {
            _hostname = "localhost";
            _username = "guest";
            _password = "guest";
            _orderRepository = orderRepository;

            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            //_channel.ExchangeDeclare(_fanoutExchangeName, ExchangeType.Fanout);
            //queueName = _channel.QueueDeclare().QueueName;
            _channel.ExchangeDeclare(_directExchangeName, ExchangeType.Direct, false);
            _channel.QueueDeclare(_paymentOrderQueueName, false, false, false);
            //_channel.QueueBind(queueName, _fanoutExchangeName, "");
            _channel.QueueBind(_paymentOrderQueueName, _directExchangeName, "PaymentOrder");
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var paymentUpdateMessageDto = JsonConvert.DeserializeObject<PaymentUpdateMessageDto>(content);
                HandleMessage(paymentUpdateMessageDto).GetAwaiter().GetResult();
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            //_channel.BasicConsume(queueName, false, consumer);
            _channel.BasicConsume(_paymentOrderQueueName, false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleMessage(PaymentUpdateMessageDto paymentUpdateMessageDto)
        {

            try
            {
                await _orderRepository.UpdateOrderPaymentStatus(paymentUpdateMessageDto.OrderId,
                    paymentUpdateMessageDto.Status);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
