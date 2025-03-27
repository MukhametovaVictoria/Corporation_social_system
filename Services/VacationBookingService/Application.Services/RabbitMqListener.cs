using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class RabbitMqListener : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMqListener> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMqListener(ILogger<RabbitMqListener> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;

            var factory = new ConnectionFactory { HostName = _configuration["RabbitMqConsumer:Host"], 
                UserName = _configuration["RabbitMqConsumer:UserName"], Password = _configuration["RabbitMqConsumer:Password"]
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _configuration["RabbitMqConsumer:Queue"], type: ExchangeType.Fanout, durable: true);
            _channel.BasicQos(0, 1, false);
            var queueName = _channel.QueueDeclare(queue: _configuration["RabbitMqConsumer:Queue"], durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName,
                    exchange: _configuration["RabbitMqConsumer:Queue"],
                    routingKey: "");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                try
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());

                    if (!string.IsNullOrEmpty(message) && HandleResult(message).Result)
                        _channel.BasicAck(ea.DeliveryTag, false);
                    else
                        _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, true);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            };

            _channel.BasicConsume(_configuration["RabbitMqConsumer:Queue"], false, consumer);

            return Task.CompletedTask;
        }

        private async Task<bool> HandleResult(string message)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var employee = JsonSerializer.Deserialize<Employee>(message, options);
            if (employee != null)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var employeeRepository = scope.ServiceProvider.GetRequiredService<IEmployeeRepository>();
                    await employeeRepository.CreateOrUpdateEmployeeAsync(employee);
                }
            }

            return false;
        } 

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
