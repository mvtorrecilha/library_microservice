using Library.Book.Application.Commands.RequestModels;
using Library.Book.Application.IntegrationEvents.Events;
using Library.Book.Application.Notifications;
using Library.Domain.Core.Bus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Book.Application.IntegrationEvents.EventHandling;

public class BookBorrowingAcceptedIntegrationEventHandler : IEventHandler<BookBorrowingAcceptedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookBorrowingAcceptedIntegrationEventHandler> _logger;

    public BookBorrowingAcceptedIntegrationEventHandler(
        IMediator mediator,
        ILogger<BookBorrowingAcceptedIntegrationEventHandler> logger
        )
	{
       _mediator = mediator;
        _logger = logger;
	}

    public async Task Handle(BookBorrowingAcceptedIntegrationEvent @event)
    {
        _logger.LogInformation("----- Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        var request = new BorrowBookCommand() { BookId = @event.BookId, StudentId = @event.StudentId };

        _logger.LogInformation(
                       "----- Sending command: {CommandName} - ({@Command})",
                       nameof(request),
                       request);

        var result = await _mediator.Send(request);

        if(result)
        {
            await _mediator.Publish(new BorrowedBookNotification
            {
                BookId = request.BookId,
                StudentId = request.StudentId
            });
        }
    }
}
