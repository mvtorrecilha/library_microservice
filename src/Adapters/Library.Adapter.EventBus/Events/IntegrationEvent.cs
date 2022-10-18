using MediatR;

namespace Library.Adapter.EventBus.Events;

public abstract record IntegrationEvent : Message, IRequest { }