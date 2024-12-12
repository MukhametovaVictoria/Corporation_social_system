using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using WebApi.Settings;

namespace WebApi.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        private ApplicationSettings _applicationSettings;
        private readonly ILogger<RabbitMqService> _logger;

        public RabbitMqService(ILogger<RabbitMqService> logger)
        {
            _applicationSettings = new ApplicationSettings();
            _logger = logger;
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
                    foreach (var queueName in _applicationSettings.Queues)
                    {
                        var factory = new ConnectionFactory() { HostName = _applicationSettings.HostName };
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
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }
    }
}
