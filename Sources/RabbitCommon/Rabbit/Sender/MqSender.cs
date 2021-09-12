namespace RabbitCommand.Rabbit.Sender
{
    using System;
    using System.Text;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using RabbitMQ.Client;

    public class MqSender<T> : IMqSender<T>
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _username;
        private readonly string _workingQueueName;
        private readonly string _exchangerName;
        private IConnection _connection;

        public MqSender(IConfiguration configuration)
        {
            _exchangerName = configuration.GetSection("RabbitMq:ExchangerName").Value;
            _workingQueueName = configuration.GetSection("RabbitMq:WorkingQueueName").Value;
            _hostname = configuration.GetSection("RabbitMq:Hostname").Value;
            _username = configuration.GetSection("RabbitMq:UserName").Value;
            _password = configuration.GetSection("RabbitMq:Password").Value;
        }

        public void Send(T command)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: _workingQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                var json = JsonConvert.SerializeObject(command, settings);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: _exchangerName, routingKey: _workingQueueName, basicProperties: null, body: body);
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
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