using Library.Borrowing.Application.Commands.RequestModels;
using Library.Borrowing.Application.IntegrationEvents.Events;
using Library.Borrowing.Application.Notifications;
using Library.Domain.Core.Bus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Borrowing.Application.IntegrationEvents.EventHandling;

public class BookReturnedAcceptedIntegrationEventHandler : IEventHandler<BookReturnedAcceptedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookReturnedAcceptedIntegrationEventHandler> _logger;

    public BookReturnedAcceptedIntegrationEventHandler(
        IMediator mediator,
        ILogger<BookReturnedAcceptedIntegrationEventHandler> logger
        )
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(BookReturnedAcceptedIntegrationEvent @event)
    {
        _logger.LogInformation("----- Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        var request = new RegisterReturnBookHistoryCommand() { StudentId = @event.StudentId, BookId = @event.BookId };

        _logger.LogInformation(
                       "----- Sending command: {CommandName} - ({@Command})",
                       nameof(request),
                       request);

        var result = await _mediator.Send(request);

        if (result)
        {
            await _mediator.Publish(new ReturningRegisteredNotification
            {
                BookId = request.BookId,
                StudentId = request.StudentId
            });
        }
    }
}
