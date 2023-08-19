using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShopApp.OrdersAPI.Models;
using ShopApp.OrdersAPI.Models.Dtos;
using ShopApp.OrdersAPI.RabbitMQSender;
using ShopApp.OrdersAPI.Repository;
using System.Text;

namespace ShopApp.OrdersAPI.Services
{
    public class RabbitMQCheckoutConsumer : BackgroundService
    {
        private readonly OrderRepository _orderRepository;
        private readonly IRabbitMQPaymentRequestMessageSender _rabbitMQPaymentRequestMessageSender;
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQCheckoutConsumer(OrderRepository orderRepository,
            IRabbitMQPaymentRequestMessageSender rabbitMQPaymentRequestMessageSender
            )
        {
            _orderRepository = orderRepository;
            _rabbitMQPaymentRequestMessageSender = rabbitMQPaymentRequestMessageSender;
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
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var checkoutMessageDto = JsonConvert.DeserializeObject<CheckoutMessageDto>(content);
                HandleMessage(checkoutMessageDto).GetAwaiter().GetResult();
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume("checkoutmessagequeue", false, consumer);
            return Task.CompletedTask;
        }

        private async Task HandleMessage(CheckoutMessageDto checkoutMessageDto)
        {
            OrderHeader orderHeader = new()
            {
                UserId = checkoutMessageDto.UserId,
                FirstName = checkoutMessageDto.FirstName,
                LastName = checkoutMessageDto.LastName,
                OrderDetails = new List<OrderDetails>(),
                CardNumber = checkoutMessageDto.CardNumber,
                CouponCode = checkoutMessageDto.CouponCode,
                CVV = checkoutMessageDto.CVV,
                DiscountTotal = checkoutMessageDto.DiscountTotal,
                Email = checkoutMessageDto.Email,
                ExpiryMonthYear = checkoutMessageDto.ExpiryMonthYear,
                OrderDate = DateTime.Now,
                OrderTotal = checkoutMessageDto.OrderTotal,
                PaymentStatus = false,
                Phone = checkoutMessageDto.Phone,
                OrderDeliveryDate = checkoutMessageDto.PaymentDate
            };

            foreach (var item in checkoutMessageDto.CartDetails)
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Price = item.Product.Price,
                    Count = item.Count
                };

                orderHeader.OrderDetails.Add(orderDetails);
            };

            await _orderRepository.AddOrder(orderHeader);

            var paymentRequestMessageDto = new PaymentRequestMessageDto()
            {
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                Email = orderHeader.Email,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal
            };
            try
            {
                _rabbitMQPaymentRequestMessageSender.SendMessage(paymentRequestMessageDto, "paymentRequestMessageQueue");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
