using Library.Borrowing.Application.Commands.RequestModels;
using Library.Borrowing.Domain.Repositories;
using MediatR;

namespace Library.Borrowing.Application.Commands.Handlers;

public class RegisterReturnBookHistoryCommandHandler : IRequestHandler<RegisterReturnBookHistoryCommand, bool>
{
    private readonly IBorrowingRepository _borrowingRepository;

    public RegisterReturnBookHistoryCommandHandler(IBorrowingRepository borrowingRepository)
    {
        _borrowingRepository = borrowingRepository;
    }

    public async Task<bool> Handle(RegisterReturnBookHistoryCommand request, CancellationToken cancellationToken)
    {
        var borrowing = await _borrowingRepository.GetBorrowingHistoryAsync(request.StudentId, request.BookId);
        if (borrowing is null)
        {
            return false;
        }

        borrowing.ReturnDate = DateTime.Now;
        _borrowingRepository.UpdateBorrowing(borrowing);
        await _borrowingRepository.SaveChangesAsync();

        return true;
    }
}