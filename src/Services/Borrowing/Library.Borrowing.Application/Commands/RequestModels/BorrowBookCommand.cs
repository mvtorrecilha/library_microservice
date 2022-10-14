using MediatR;

namespace Library.Book.Application.Commands.RequestModels;

public record BorrowBookCommand : IRequest<bool>
{
    public Guid BookId { get; set; }

    public Guid StudentId { get; set; }
}
