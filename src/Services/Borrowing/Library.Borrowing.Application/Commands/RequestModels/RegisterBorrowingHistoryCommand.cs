using MediatR;

namespace Library.Borrowing.Application.Commands.RequestModels;

public record RegisterBorrowingHistoryCommand : IRequest<bool>
{
    public Guid StudentId { get; set; }

    public Guid BookId { get; set; } 
}
