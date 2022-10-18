using Library.Adapter.EventBus.Events;

namespace Library.Adapter.EventBus.Application;

public interface IApplicationEventBus
{
    Task PublishEvent<T>(T @event) where T : IntegrationEvent;

    void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IEventHandler<T>;

    void Dispose();
}