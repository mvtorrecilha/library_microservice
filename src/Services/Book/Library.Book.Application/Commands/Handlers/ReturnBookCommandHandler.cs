using Library.Book.Application.Behaviors;
using Library.Book.Application.Commands.RequestModels;
using Library.Book.Domain.Repositories;
using Library.Infra.ResponseFormatter.Common;
using MediatR;
using System.Net;

namespace Library.Book.Application.Commands.Handlers;

public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly INotifier _notifier;

    public ReturnBookCommandHandler(
        IBookRepository bookRepository,
        INotifier notifier)
    {
        _bookRepository = bookRepository;
        _notifier = notifier;
    }

    public async Task<Unit> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
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

        return Unit.Value;
    }
}
