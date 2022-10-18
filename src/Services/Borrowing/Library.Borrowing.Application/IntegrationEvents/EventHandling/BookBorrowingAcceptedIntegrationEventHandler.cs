using Library.Infra.EventBus;
using Library.Book.Application.Commands.RequestModels;
using Library.Borrowing.Application.IntegrationEvents.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Borrowing.Application.IntegrationEvents.EventHandling;

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
        var request = new BorrowBookCommand() { BookId = @event.BookId, StudentId = @event.StudentId };

        var result = await _mediator.Send(request);

        if(!result)
        {
            //TODO: Send notification
        }

        //TODO: Send event to update book to not available
    }
}
