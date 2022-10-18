using Library.Domain.Core.Events;

namespace Library.Domain.Core.Bus;

public interface IEventHandler<in TEvent> : IEventHandler
        where TEvent : IntegrationEvent
{
    Task Handle(TEvent @event);
}

public interface IEventHandler
{

}
