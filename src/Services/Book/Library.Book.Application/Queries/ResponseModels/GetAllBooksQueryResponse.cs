using Library.Book.Domain.Entities;

namespace Library.Book.Application.Queries.ResponseModels;

public class GetAllBooksQueryResponse
{
    public List<BookItem> Books { get; set; }
}
