using Library.Borrowing.Application.Commands.RequestModels;
using Library.Borrowing.Domain.Entities;
using Library.Borrowing.Domain.Repositories;
using MediatR;

namespace Library.Borrowing.Application.Commands.Handlers;

public class RegisterBorrowingHistoryCommandHandler : IRequestHandler<RegisterBorrowingHistoryCommand, bool>
{
    private readonly IBorrowingRepository _borrowingRepository;

    public RegisterBorrowingHistoryCommandHandler(IBorrowingRepository borrowingRepository)
    {
        _borrowingRepository = borrowingRepository;
    }

    public async Task<bool> Handle(RegisterBorrowingHistoryCommand request, CancellationToken cancellationToken)
    {
        var borrowHistry = new BorrowingHistory
        {
            StudentId = request.StudentId,
            BookId =  request.BookId,
            BorrowDate = DateTime.Now
        };

        await _borrowingRepository.AddBorrowingAsync(borrowHistry);
        await _borrowingRepository.SaveChangesAsync();

        return true;
    }
}