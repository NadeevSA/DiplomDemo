using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DesignTask
{
    public class RabbitMqReciever : BackgroundService
    {
        public IConfiguration Configuration;
        private IMediator _mediator;
        private IModel _channel;
        private IConnection _connection;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;

        
        public RabbitMqReciever(IConfiguration configuration, IMediator mediator)
        {
            Configuration = configuration;
            this._mediator = mediator;
            _hostname = configuration.GetSection("RabbitMq:Hostname").Value;
            _queueName = configuration.GetSection("RabbitMq:QueueName").Value;
            _username = configuration.GetSection("RabbitMq:UserName").Value;
            _password = configuration.GetSection("RabbitMq:Password").Value;
            InitializeRabbitMqListener();
        }
        
        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var exampleModelCommand = JsonConvert.DeserializeObject<IRequest>(content, settings);

                HandleMessage(exampleModelCommand);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            _channel.BasicConsume(_queueName, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(IRequest command)
        {
            _mediator.Send(command);
        }

        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}