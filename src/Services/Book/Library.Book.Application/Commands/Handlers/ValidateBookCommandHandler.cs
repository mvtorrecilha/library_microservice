using Library.Book.Application.Commands.RequestModels;
using Library.Book.Application.Commands.ResponseModels;
using Library.Book.Domain.Repositories;
using MediatR;

namespace Library.Book.Application.Commands.Handlers;

public class ValidateBookCommandHandler : IRequestHandler<ValidateBookCommand, ValidateBookCommandResponse>
{
    private readonly IBookRepository _bookRepository;

    public ValidateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<ValidateBookCommandResponse> Handle(ValidateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if(book is null)
        {
            return null;
        }

        if(book.CourseId != request.CourseId)
        {
            return null;
        }

        return new ValidateBookCommandResponse
        {
            isValid = true
        };
    }
}
