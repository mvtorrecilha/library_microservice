using Library.Book.Application.Commands.ResponseModels;
using MediatR;

namespace Library.Book.Application.Commands.RequestModels;

public record GetAllBooksQuery : IRequest<GetAllBooksQueryResponse>
{
}
