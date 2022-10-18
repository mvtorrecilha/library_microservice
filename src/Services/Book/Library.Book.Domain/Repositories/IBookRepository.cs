using Library.Book.Domain.Entities;

namespace Library.Book.Domain.Repositories;

public interface IBookRepository
{
    Task<BookItem> GetBookByIdAsync(Guid Id);

    Task<IEnumerable<BookItem>> GetAllBooksAsync();
}
