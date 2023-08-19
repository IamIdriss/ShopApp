using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using ShopApp.PaymentsAPI.Repository;
using ShopApp.PaymentsAPI.RabbitMQSender;
using System.Text;
using Newtonsoft.Json;
using ShopApp.PaymentsAPI.Models.Dtos;

namespace ShopApp.PaymentsAPI.Services
{
    public class RabbitMQPaymentConsumer :BackgroundService
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private readonly IRabbitMQPaymentResultMessageSender _rabbitMQPaymentResultMessageSender;
        private readonly IPaymentProcessor _paymentProcessor;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQPaymentConsumer(IRabbitMQPaymentResultMessageSender rabbitMQPaymentResultMessageSender,
            IPaymentProcessor paymentProcessor
           )
        {
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
            _channel.QueueDeclare("paymentRequestMessageQueue", false, false, false);
            _rabbitMQPaymentResultMessageSender = rabbitMQPaymentResultMessageSender;
            _paymentProcessor = paymentProcessor;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var paymentRequestMessageDto = JsonConvert.DeserializeObject<PaymentRequestMessageDto>(content);
                HandleMessage(paymentRequestMessageDto);
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume("paymentRequestMessageQueue", false, consumer);
            return Task.CompletedTask;
        }

        private void HandleMessage(PaymentRequestMessageDto paymentRequestMessageDto)
        {
            var result = _paymentProcessor.ProcessPayment();
            var paymentUpdateMessage = new PaymentUpdateMessageDto()
            {
                OrderId = paymentRequestMessageDto.OrderId,
                Status = result,
                Email = paymentRequestMessageDto.Email
            };

            try
            {
                _rabbitMQPaymentResultMessageSender.SendMessage(paymentUpdateMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
