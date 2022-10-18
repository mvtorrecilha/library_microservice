using MediatR;

namespace Library.Domain.Core.Events;

public abstract record IntegrationEvent : Message, IRequest { }