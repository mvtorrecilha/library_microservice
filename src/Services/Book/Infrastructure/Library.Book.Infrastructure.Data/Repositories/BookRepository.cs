using Library.Book.Domain.Entities;
using Library.Book.Domain.Repositories;
using Library.Book.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Book.Infrastructure.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookContext _context;

    public BookRepository(BookContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookItem>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<BookItem> GetByIdAsync(Guid Id)
    {
        return await _context.Books.FindAsync(Id);
    }
}
