using Library.Book.Application.Commands.RequestModels;
using Library.Borrowing.Application.IntegrationEvents.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Borrowing.Application.IntegrationEvents.EventHandling;

public class BookBorrowingAcceptedIntegrationEventHandler : INotificationHandler<BookBorrowingAcceptedIntegrationEvent>
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

    public async Task Handle(BookBorrowingAcceptedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        var request = new BorrowBookCommand() { BookId = @event.BookId, StudentId = @event.StudentId };

        var result = await _mediator.Send(request);
        throw new NotImplementedException();
    }
}
