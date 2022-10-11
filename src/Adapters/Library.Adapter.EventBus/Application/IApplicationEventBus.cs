using Library.Adapter.EventBus.Events;

namespace Library.Adapter.EventBus.Application;

public interface IApplicationEventBus
{
    Task PublishEvent<T>(T @event) where T : IntegrationEvent;

    void StartConsumer();

    void Dispose();
}