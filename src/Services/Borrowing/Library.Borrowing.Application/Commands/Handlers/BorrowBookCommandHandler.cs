using Library.Book.Application.Commands.RequestModels;
using Library.Borrowing.Domain.Entities;
using Library.Borrowing.Domain.Repositories;
using MediatR;

namespace Library.Borrowing.Application.Commands.Handlers;

public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, bool>
{
    private readonly IBorrowingRepository _borrowingRepository;

    public BorrowBookCommandHandler(IBorrowingRepository borrowingRepository)
    {
        _borrowingRepository = borrowingRepository;
    }

    public async Task<bool> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
    {

        if (!await _borrowingRepository.IsValidToBorrow(request.BookId))
        {
            return false;
        }

        var bookToBorrow = new BorrowingHistory
        {
            StudentId = request.StudentId,
            BookId = request.BookId,
            BorrowDate = DateTime.Now
        };

        await _borrowingRepository.AddBorrowingAsync(bookToBorrow);
        _borrowingRepository.SaveChanges();

        return true;
    }
}
