using Library.Book.Application.Commands.ResponseModels;
using MediatR;

namespace Library.Book.Application.Commands.RequestModels;

public record ValidateBookCommand : IRequest<ValidateBookCommandResponse>
{
    public Guid BookId { get; set; }

    public Guid CourseId { get; set; }
}
