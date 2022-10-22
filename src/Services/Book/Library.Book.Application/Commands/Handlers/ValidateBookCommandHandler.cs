using Library.Book.Application.Behaviors;
using Library.Book.Application.Commands.RequestModels;
using Library.Book.Application.Commands.ResponseModels;
using Library.Book.Domain.Repositories;
using Library.Infra.ResponseFormatter.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Library.Book.Application.Commands.Handlers;

public class ValidateBookCommandHandler : IRequestHandler<ValidateBookCommand, ValidateBookCommandResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly INotifier _notifier;
    private readonly ILogger<ValidateBookCommandHandler> _logger;

    public ValidateBookCommandHandler(
        IBookRepository bookRepository,
        INotifier notifier,
        ILogger<ValidateBookCommandHandler> logger)
    {
        _bookRepository = bookRepository;
        _notifier = notifier;
        _logger = logger;
    }

    public async Task<ValidateBookCommandResponse> Handle(ValidateBookCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("----- Validating request to borrow a book - Request: {@request}", request);
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);

        if(book is null)
        {
            _notifier.AddError("Book Id", ErrorBehavior.BookNotFound, request.BookId);
            _notifier.SetStatuCode(HttpStatusCode.NotFound);
            _logger.LogInformation("----- Book not found with: {@BookId}", request.BookId);
            return new ValidateBookCommandResponse();
        }

        if (!book.IsAvailable)
        {
            _notifier.AddError("Book Id", ErrorBehavior.BookAlreadyLent, request.BookId);
            _notifier.SetStatuCode(HttpStatusCode.Forbidden);
            _logger.LogInformation("----- Book is not available to borrow: {@BookId}", request.BookId);
            return new ValidateBookCommandResponse();
        }

        if (book.CourseId != request.CourseId)
        {
            _notifier.AddError("Id", ErrorBehavior.TheBookDoesNotBelongToTheCourse, request.BookId);
            _notifier.SetStatuCode(HttpStatusCode.Forbidden);
            _logger.LogInformation("----- The book does not belong to the course: {@BookId} and Course {@CourseId}", request.BookId, request.CourseId);
            return new ValidateBookCommandResponse();
        }

        _logger.LogInformation("----- Book is valid to borrow.");
        return new ValidateBookCommandResponse
        {
            isValid = true
        };
    }
}
