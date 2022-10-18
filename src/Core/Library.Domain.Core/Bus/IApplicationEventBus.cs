using Library.Domain.Core.Events;

namespace Library.Domain.Core.Bus;

public interface IApplicationEventBus
{
    Task PublishEvent<T>(T @event) where T : IntegrationEvent;

    void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IEventHandler<T>;

    void Dispose();
}