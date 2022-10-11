using Library.Adapter.EventBus.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;

namespace Library.Adapter.EventBus.Application;

public class ApplicationEventBusRabbitMQ : IApplicationEventBus
{
    private const string _queueName = "library-events";

    private readonly IApplicationRabbitMQPersistentConnection _persistentConnection;
    private readonly ILogger<ApplicationEventBusRabbitMQ> _logger;
    private readonly IMediator _mediator;
    private IModel _consumerChannel;
    private readonly int _retryCount;

    public ApplicationEventBusRabbitMQ(
        IApplicationRabbitMQPersistentConnection persistentConnection,
        ILogger<ApplicationEventBusRabbitMQ> logger,
        IMediator mediator)
    {
        _persistentConnection = persistentConnection;
        _logger = logger;
        _mediator = mediator;
        _retryCount = 5;
    }

    public Task PublishEvent<T>(T @event) where T : IntegrationEvent
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        var policy = Policy.Handle<BrokerUnreachableException>()
            .Or<SocketException>()
            .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
            {
                _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
            });

        using var channel = _persistentConnection.CreateModel();

        _logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

        channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        var jsonMessage = JsonConvert.SerializeObject(@event, settings);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        policy.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();

            channel.BasicPublish(exchange: "", routingKey: _queueName, mandatory: true, basicProperties: properties, body: body);
        });

        return Task.CompletedTask;
    }

    public void StartConsumer()
    {
        _consumerChannel = CreateConsumerChannel();
        StartBasicConsume();
    }

    public void Dispose()
    {
        if (_consumerChannel != null)
        {
            _consumerChannel.Dispose();
        }
    }

    private IModel CreateConsumerChannel()
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        _logger.LogTrace("Creating RabbitMQ consumer channel");

        var channel = _persistentConnection.CreateModel();

        channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        channel.CallbackException += (sender, ea) =>
        {
            _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

            _consumerChannel.Dispose();
            _consumerChannel = CreateConsumerChannel();
            StartBasicConsume();
        };

        return channel;
    }

    private void StartBasicConsume()
    {
        _logger.LogTrace("Starting RabbitMQ basic consume");

        if (_consumerChannel != null)
        {
            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.Received += Consumer_Received;

            _consumerChannel.BasicConsume(
                queue: _queueName,
                autoAck: false,
                consumer: consumer);
        }
        else
        {
            _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
        }
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
    {
        var jsonData = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        var message = JsonConvert.DeserializeObject(jsonData, settings);

        try
        {
            await _mediator.Publish(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "----- ERROR Processing message \"{Message}\"", message);
        }

        // Even on exception we take the message off the queue.
        // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
        // For more information see: https://www.rabbitmq.com/dlx.html
        _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
    }
}

