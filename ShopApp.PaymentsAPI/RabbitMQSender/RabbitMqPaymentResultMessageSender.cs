using Newtonsoft.Json;
using RabbitMQ.Client;
using ShopApp.MessageBus;
using System.Text;

namespace ShopApp.PaymentsAPI.RabbitMQSender
{
    public class RabbitMqPaymentResultMessageSender : IRabbitMQPaymentResultMessageSender
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;
        //private const string _fanoutExchangeName = "Fanout_Payment_Update_Exchange";
        private const string _directExchangeName = "Direct_Payment_Update_Exchange";
        private const string _paymentEmailQueueName = "Payment_Email_Queue";
        private const string _paymentOrderQueueName = "Payment_Order_Queue";

        public RabbitMqPaymentResultMessageSender()
        {
            _hostname = "localhost";
            _username = "guest";
            _password = "guest";
        }
        public void SendMessage(BaseMessage message)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                //channel.ExchangeDeclare(_fanoutExchangeName, ExchangeType.Fanout, false);
                channel.ExchangeDeclare(_directExchangeName, ExchangeType.Direct, false);
                channel.QueueDeclare(_paymentEmailQueueName, false, false, false);
                channel.QueueDeclare(_paymentOrderQueueName, false, false, false);
                channel.QueueBind(_paymentEmailQueueName, _directExchangeName, "PaymentEmail");
                channel.QueueBind(_paymentOrderQueueName, _directExchangeName, "PaymentOrder");
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                //channel.BasicPublish(_fanoutExchangeName, "", basicProperties: null, body: body);
                channel.BasicPublish(_directExchangeName, "PaymentEmail", basicProperties: null, body: body);
                channel.BasicPublish(_directExchangeName, "PaymentOrder", basicProperties: null, body: body);
            }

        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                //log exceptions
                throw;
            }
        }
        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }
            CreateConnection();
            return _connection != null;
        }
    }
}
