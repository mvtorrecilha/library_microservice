using Library.Book.Application.Commands.RequestModels;
using Library.Book.Application.IntegrationEvents.Events;
using Library.Book.Domain.Repositories;
using Library.Domain.Core.Bus;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Library.Book.Application.Commands.Handlers;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, bool>
{
    private readonly IBookRepository _bookRepository;
    private readonly IApplicationEventBus _eventBus;
    private readonly ILogger<BorrowBookCommandHandler> _logger;

    public BorrowBookCommandHandler(
        IBookRepository bookRepository,
        IApplicationEventBus eventBus,
        ILogger<BorrowBookCommandHandler> logger)
    {
        _bookRepository = bookRepository;
        _eventBus = eventBus;
        _logger = logger;
    }

    public async Task<bool> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);
        book.IsAvailable = false;

        _logger.LogInformation("----- Borrowing a book - Borrow Request: {@request}", request);
        _bookRepository.UpdateBook(book);
        await _bookRepository.SaveChangesAsync();
        
        var eventMessage = new RegisterBorrowHistoryIntegrationEvent(request.StudentId, request.BookId);

        _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", eventMessage.Id, eventMessage);
        await _eventBus.PublishEvent(eventMessage);

        return true;
    }
}
