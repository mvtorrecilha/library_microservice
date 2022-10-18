using Library.Book.Application.Queries.ResponseModels;
using MediatR;

namespace Library.Book.Application.Queries.RequestModels;

public record GetAllBooksQuery : IRequest<GetAllBooksQueryResponse>
{
}
