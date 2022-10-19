using Library.Book.Application.Behaviors;
using Library.Book.Application.Commands.RequestModels;
using Library.Book.Application.Commands.ResponseModels;
using Library.Book.Domain.Repositories;
using Library.Infra.ResponseFormatter.Common;
using MediatR;
using System.Net;

namespace Library.Book.Application.Commands.Handlers;

public class ValidateBookCommandHandler : IRequestHandler<ValidateBookCommand, ValidateBookCommandResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly INotifier _notifier;

    public ValidateBookCommandHandler(
        IBookRepository bookRepository,
        INotifier notifier)
    {
        _bookRepository = bookRepository;
        _notifier = notifier;
    }

    public async Task<ValidateBookCommandResponse> Handle(ValidateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);

        if(book is null)
        {
            _notifier.AddError("Book Id", ErrorBehavior.BookNotFound, request.BookId);
            _notifier.SetStatuCode(HttpStatusCode.NotFound);
            return new ValidateBookCommandResponse();
        }

        if (!book.IsAvailable)
        {
            _notifier.AddError("Book Id", ErrorBehavior.BookAlreadyLent, request.BookId);
            _notifier.SetStatuCode(HttpStatusCode.Forbidden);
            return new ValidateBookCommandResponse();
        }

        if (book.CourseId != request.CourseId)
        {
            _notifier.AddError("Id", ErrorBehavior.TheBookDoesNotBelongToTheCourse, request.BookId);
            _notifier.SetStatuCode(HttpStatusCode.Forbidden);
            return new ValidateBookCommandResponse();
        }

        return new ValidateBookCommandResponse
        {
            isValid = true
        };
    }
}
