using Library.Book.Application.Commands.RequestModels;
using Library.Book.Application.IntegrationEvents.Events;
using Library.Book.Domain.Repositories;
using Library.Domain.Core.Bus;
using MediatR;

namespace Library.Book.Application.Commands.Handlers;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, bool>
{
    private readonly IBookRepository _bookRepository;
    private readonly IApplicationEventBus _eventBus;

    public BorrowBookCommandHandler(
        IBookRepository bookRepository,
        IApplicationEventBus eventBus)
    {
        _bookRepository = bookRepository;
        _eventBus = eventBus;
    }

    public async Task<bool> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);
        book.IsAvailable = false;
        _bookRepository.UpdateBook(book);
        await _bookRepository.SaveChangesAsync();

        var eventMessage = new RegisterBorrowHistoryIntegrationEvent(request.StudentId, request.BookId);
        await _eventBus.PublishEvent(eventMessage);

        return true;
    }
}
