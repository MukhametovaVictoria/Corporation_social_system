using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace TestIdentity.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly ILogger<RabbitMqService> _logger;
        private readonly string? _hostName;
        private readonly string? _queue;

        public RabbitMqService(ILogger<RabbitMqService> logger)
        {
            _logger = logger;
            _hostName = "localhost";
            _queue = "AuthUsersQueue";
        }

        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    var factory = new ConnectionFactory() { HostName = _hostName };
                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: _queue,
                                       durable: true,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null);

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                       routingKey: _queue,
                                       basicProperties: null,
                                       body: body);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }
    }
}
