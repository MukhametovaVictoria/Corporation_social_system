using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly ILogger<RabbitMqService> _logger;
        private readonly IConfiguration _configuration;

        public RabbitMqService(ILogger<RabbitMqService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void SendMessage(object obj, string hostName, string queue)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message, hostName, queue);
        }

        public void SendMessage(string message, string hostName, string queue)
        {
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    var factory = new ConnectionFactory() { 
                        HostName = hostName, 
                        UserName = _configuration[$"RabbitMq:{hostName}:UserName"],
                        Password = _configuration[$"RabbitMq:{hostName}:Password"]
                    };
                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: queue,
                                       durable: true,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null);

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                       routingKey: queue,
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
