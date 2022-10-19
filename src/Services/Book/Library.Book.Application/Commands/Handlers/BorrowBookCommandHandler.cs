using Library.Book.Application.Commands.RequestModels;
using Library.Book.Domain.Repositories;
using MediatR;

namespace Library.Book.Application.Commands.Handlers;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, bool>
{
    private readonly IBookRepository _bookRepository;

    public BorrowBookCommandHandler(
        IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<bool> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetBookByIdAsync(request.BookId);
        book.IsAvailable = false;
        _bookRepository.UpdateBook(book);
        await _bookRepository.SaveChangesAsync();

        return true;
    }
}
