using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Settings;

namespace WebApi.RabbitMq
{
    public class RabbitMqListener : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private ApplicationSettings _applicationSettings;
        private readonly ILogger<RabbitMqListener> _logger;

        public RabbitMqListener(ILogger<RabbitMqListener> logger)
        {
            _applicationSettings = new ApplicationSettings();
            _logger = logger;

            var factory = new ConnectionFactory { HostName = _applicationSettings.HostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _applicationSettings.EmployeeFromAuthServiceQueue, type: ExchangeType.Fanout, durable: true);
            _channel.BasicQos(0, 1, false);
            var queueName = _channel.QueueDeclare(queue: _applicationSettings.EmployeeFromAuthServiceQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: queueName,
                    exchange: _applicationSettings.EmployeeFromAuthServiceQueue,
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

                    if (!string.IsNullOrEmpty(message) && RedirectToAnotherAction(message).Result)
                        _channel.BasicAck(ea.DeliveryTag, false);
                    else
                        _channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, true);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            };

            _channel.BasicConsume(_applicationSettings.EmployeeFromAuthServiceQueue, false, consumer);

            return Task.CompletedTask;
        }

        private async Task<bool> RedirectToAnotherAction(string message)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var userModel = JsonSerializer.Deserialize<UserModel>(message, options);
            if (userModel != null)
            {
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync($"{_applicationSettings.SiteUrl}/api/Employee/CreateOrUpdateEmployee", userModel);
                    if(response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return JsonSerializer.Deserialize<bool>(result, options);
                    }
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
