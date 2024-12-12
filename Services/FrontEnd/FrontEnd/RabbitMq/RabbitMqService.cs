using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FrontEnd.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly ILogger<RabbitMqService> _logger;

        public RabbitMqService(ILogger<RabbitMqService> logger)
        {
            _logger = logger;
        }

        public void SendMessage(object obj, string queueName)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message, queueName);
        }

        public void SendMessage(string message, string queueName)
        {
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    var factory = new ConnectionFactory() { HostName = "localhost" };
                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: queueName,
                                       durable: true,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null);

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                       routingKey: queueName,
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
