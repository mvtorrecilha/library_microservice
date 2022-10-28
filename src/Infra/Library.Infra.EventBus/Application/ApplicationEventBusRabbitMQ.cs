using Autofac;
using Library.Domain.Core.Bus;
using Library.Domain.Core.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;

namespace Library.Infra.EventBus.Application;

public class ApplicationEventBusRabbitMQ : IApplicationEventBus
{
    const string AUTOFAC_SCOPE_NAME = "library-events_bus";
    const string EXCHANGE_NAME = "library-events-exchange";

    private readonly IApplicationRabbitMQPersistentConnection _persistentConnection;
    private readonly Dictionary<string, List<Type>> _handlers;
    private readonly List<Type> _eventTypes;
    private readonly ILogger<ApplicationEventBusRabbitMQ> _logger;
    private IModel _consumerChannel;
    private readonly int _retryCount;
    private readonly ILifetimeScope _autofac;

    public ApplicationEventBusRabbitMQ(
        IApplicationRabbitMQPersistentConnection persistentConnection,
        ILogger<ApplicationEventBusRabbitMQ> logger,
        ILifetimeScope autofac,
        int retryCount)
    {
        _persistentConnection = persistentConnection;
        _logger = logger;
        _retryCount = retryCount;
        _handlers = new Dictionary<string, List<Type>>();
        _eventTypes = new List<Type>();
        _autofac = autofac;
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

        var arguments = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", "DeadLetterExchange" }
        };
        var eventName = @event.GetType().Name;
        channel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: false, arguments: arguments);

        channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: "direct");
        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        var jsonMessage = JsonConvert.SerializeObject(@event, settings);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        policy.Execute(() =>
        {
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.DeliveryMode = 2;

            channel.BasicPublish(exchange: "", routingKey: eventName, mandatory: true, basicProperties: properties, body: body);
        });

        return Task.CompletedTask;
    }

    private IModel CreateConsumerChannel()
    {
        if (!_persistentConnection.IsConnected)
        {
            _persistentConnection.TryConnect();
        }

        _logger.LogTrace("Creating RabbitMQ consumer channel");

        var channel = _persistentConnection.CreateModel();

        return channel;
    }

    public void Dispose()
    {
        if (_consumerChannel != null)
        {
            _consumerChannel.Dispose();
        }
    }

    public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(TH);

        if (!_eventTypes.Contains(typeof(T)))
        {
            _eventTypes.Add(typeof(T));
        }

        if (!_handlers.ContainsKey(eventName))
        {
            _handlers.Add(eventName, new List<Type>());
        }

        if (_handlers[eventName].Any(s => s.GetType() == handlerType))
        {
            throw new ArgumentException(
                $"Handler Type {handlerType.Name} already is registered for '{eventName}'", nameof(handlerType));
        }

        _handlers[eventName].Add(handlerType);

        _consumerChannel = CreateConsumerChannel();
        StartBasicConsume<T>();
    }


    private void StartBasicConsume<T>() where T : IntegrationEvent
    {
        _logger.LogTrace("Starting RabbitMQ basic consume");

        if(_consumerChannel is null)
        {
            _logger.LogError("StartBasicConsume can't call on _consumerChannel == null");
        }

        _consumerChannel.ExchangeDeclare("DeadLetterExchange", ExchangeType.Fanout);
        _consumerChannel.QueueDeclare("DeadLetterQueue", true, false, false, null);
        _consumerChannel.QueueBind("DeadLetterQueue", "DeadLetterExchange","");

        var arguments = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", "DeadLetterExchange" }
        };

        _consumerChannel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: "direct");
        var eventName = typeof(T).Name;
        _consumerChannel.QueueDeclare(queue: eventName, durable: false, exclusive: false, autoDelete: false, arguments: arguments);

        _consumerChannel.CallbackException += (sender, ea) =>
        {
            _logger.LogWarning(ea.Exception, "Recreating RabbitMQ consumer channel");

            _consumerChannel.Dispose();
            _consumerChannel = CreateConsumerChannel();
        };

        var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

        consumer.Received += Consumer_Received;

        _consumerChannel.BasicConsume(
            queue: eventName,
            autoAck: false,
            consumer: consumer);
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

        try
        {
            await ProcessEvent(eventName, message).ConfigureAwait(false);
            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "----- ERROR Processing message \"{Message}\"", message);
            _consumerChannel.BasicNack(eventArgs.DeliveryTag, false, false);
        }
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (!_handlers.ContainsKey(eventName))
        {
            return;
        }

        var subscriptions = _handlers[eventName];
        foreach (var subscription in subscriptions)
        {
            var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
            await using var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME);
            var handler = scope.ResolveOptional(subscription);

            var @event = JsonConvert.DeserializeObject(message, eventType);
            var conreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
            await (Task)conreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
        }
    }
}

