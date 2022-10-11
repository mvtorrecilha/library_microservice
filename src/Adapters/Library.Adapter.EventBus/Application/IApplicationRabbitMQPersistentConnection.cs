using RabbitMQ.Client;

namespace Library.Adapter.EventBus.Application;

public interface IApplicationRabbitMQPersistentConnection : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();
}

