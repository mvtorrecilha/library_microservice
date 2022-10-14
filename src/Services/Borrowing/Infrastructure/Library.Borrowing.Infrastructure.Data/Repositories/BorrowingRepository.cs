using Library.Borrowing.Domain.Entities;
using Library.Borrowing.Domain.Repositories;
using Library.Borrowing.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Borrowing.Infrastructure.Data.Repositories;

public class BorrowingRepository : IBorrowingRepository
{
    private readonly BorrowContext _context;

    public BorrowingRepository(BorrowContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BorrowingHistory>> GetAllAsync()
    {
        return await _context.BorrowingHistories.ToListAsync();
    }

    public async Task<bool> IsValidToBorrow(Guid bookId)
    {
        var result = await _context.BorrowingHistories
            .Where(p => p.BookId == bookId && p.ReturnDate == null)
            .FirstOrDefaultAsync();

        if (result is not null)
            return false;

        return true;
    }
}
