using Library.Book.Application.Behaviors;
using Library.Book.Application.Commands.RequestModels;
using Library.Book.Application.IntegrationEvents.Events;
using Library.Book.Domain.Repositories;
using Library.Domain.Core.Bus;
using Library.Infra.ResponseFormatter.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Library.Book.Application.Commands.Handlers;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly INotifier _notifier;
    private readonly IApplicationEventBus _eventBus;
    private readonly ILogger<ReturnBookCommandHandler> _logger;

    public ReturnBookCommandHandler(
        IBookRepository bookRepository,
        INotifier notifier,
        IApplicationEventBus eventBus,
        ILogger<ReturnBookCommandHandler> logger)
    {
        _bookRepository = bookRepository;
        _notifier = notifier;
        _eventBus = eventBus;
        _logger = logger;
    }

    public async Task<Unit> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("----- Returning a book - Request: {@request}", request);
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);

        if (book is null)
        {
            _notifier.AddError("Book Id", ErrorBehavior.BookNotFound, request.BookId);
            _notifier.SetStatuCode(HttpStatusCode.NotFound);
            return Unit.Value;
        }

        if (book.IsAvailable)
        {
            _notifier.AddError("Book Id", ErrorBehavior.BookAlreadyReturned, request.BookId);
            _notifier.SetStatuCode(HttpStatusCode.Conflict);
            return Unit.Value;
        }

        book.IsAvailable = true;
        _bookRepository.UpdateBook(book);
        await _bookRepository.SaveChangesAsync();
        _logger.LogInformation("----- Book returned - lent book: {@book}", book);

        var eventMessage = new BookReturnedAcceptedIntegrationEvent(request.StudentId, request.BookId);

        try
        {
            _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", eventMessage.Id, eventMessage);
            await _eventBus.PublishEvent(eventMessage);
        }
        catch (Exception e)
        {
            _notifier.AddError("500", e.Message, null);
        }

        return Unit.Value;
    }
}
