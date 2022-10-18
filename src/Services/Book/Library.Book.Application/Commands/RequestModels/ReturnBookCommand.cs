using MediatR;

namespace Library.Book.Application.Commands.RequestModels;

public record ReturnBookCommand : IRequest
{
    public Guid BookId { get; set; }

    public Guid StudentId { get; set; }
}
