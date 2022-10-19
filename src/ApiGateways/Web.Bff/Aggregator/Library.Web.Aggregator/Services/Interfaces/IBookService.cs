using Library.Web.Aggregator.Models.Book;

namespace Library.Web.Aggregator.Services.Interfaces;

public interface IBookService
{
    Task BorrowBookAsync(BorrowingBookRequest borrowingBookRequest);
}

