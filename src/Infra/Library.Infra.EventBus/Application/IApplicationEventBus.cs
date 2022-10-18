using Library.Infra.EventBus.Events;

namespace Library.Infra.EventBus.Application;

public interface IApplicationEventBus
{
    Task PublishEvent<T>(T @event) where T : IntegrationEvent;

    void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IEventHandler<T>;

    void Dispose();
}