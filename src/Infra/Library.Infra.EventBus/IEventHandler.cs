using Library.Infra.EventBus.Events;

namespace Library.Infra.EventBus;

public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : IntegrationEvent
{
    Task Handle(TEvent @event);
}

public interface IEventHandler
{

}
