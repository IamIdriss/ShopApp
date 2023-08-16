using Newtonsoft.Json;
using RabbitMQ.Client;
using ShopApp.MessageBus;
using System.Text;

namespace ShopApp.ShoppingCartAPI.RabbitMQSender
{
    public class RabbitMqCheckoutMessageSender : IRabbitMQCheckoutMessageSender
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;
        private IConnection _connection;

        public RabbitMqCheckoutMessageSender()
        {
            _hostname = "localhost";
            _username = "guest";
            _password = "guest";
        }
        public void SendMessage(BaseMessage message, string queueName)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queueName, false, false, false);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
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
