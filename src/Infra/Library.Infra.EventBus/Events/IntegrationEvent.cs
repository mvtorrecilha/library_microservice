using MediatR;

namespace Library.Infra.EventBus.Events;

public abstract record IntegrationEvent : Message, IRequest { }