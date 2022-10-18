using Library.Book.Application.Queries.RequestModels;
using Library.Book.Application.Queries.ResponseModels;
using Library.Book.Domain.Repositories;
using MediatR;

namespace Library.Book.Application.Queries.Handlers;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, GetAllBooksQueryResponse>
{
    private readonly IBookRepository _bookRepository;

    public GetAllBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<GetAllBooksQueryResponse> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllBooksAsync();

        return new GetAllBooksQueryResponse
        {
            Books = books.ToList()
        };
    }
}
