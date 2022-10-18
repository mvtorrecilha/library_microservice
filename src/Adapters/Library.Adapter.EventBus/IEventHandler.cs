using Library.Adapter.EventBus.Events;

namespace Library.Adapter.EventBus;

public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : IntegrationEvent
{
    Task Handle(TEvent @event);
}

public interface IEventHandler
{

}
