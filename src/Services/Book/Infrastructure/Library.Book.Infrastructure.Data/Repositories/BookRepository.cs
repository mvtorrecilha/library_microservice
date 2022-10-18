using Library.Book.Domain.Entities;
using Library.Book.Domain.Repositories;
using Library.Book.Infrastructure.Data.Context;
using Library.Infra.Repository;

namespace Library.Book.Infrastructure.Data.Repositories;

public class BookRepository : RepositoryBase<BookContext, BookItem>, IBookRepository
{
    public BookRepository(BookContext bookContext)
             : base(bookContext)
    {
    }

    public async Task<BookItem> GetBookByIdAsync(Guid Id) =>
            await GetByIdAsync(Id);

    public async Task<IEnumerable<BookItem>> GetAllBooksAsync() =>
            await GetAllAsync();
}
