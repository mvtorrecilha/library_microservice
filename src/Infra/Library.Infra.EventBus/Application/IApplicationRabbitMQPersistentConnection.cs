using RabbitMQ.Client;

namespace Library.Infra.EventBus.Application;

public interface IApplicationRabbitMQPersistentConnection : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();
}

