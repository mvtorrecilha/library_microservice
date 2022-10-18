using Library.Borrowing.Domain.Entities;
using Library.Borrowing.Domain.Repositories;
using Library.Borrowing.Infrastructure.Data.Context;
using Library.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace Library.Borrowing.Infrastructure.Data.Repositories;

public class BorrowingRepository : RepositoryBase<BorrowContext, BorrowingHistory>, IBorrowingRepository
{
    public BorrowingRepository(BorrowContext borrowContext)
           : base(borrowContext)
    {
    }

    public async Task<IEnumerable<BorrowingHistory>> GetAllBorrowingHistoryAsync() =>
          await GetAllAsync();

    public async Task AddBorrowingAsync(BorrowingHistory borrowing) =>
            await CreateAsync(borrowing);

    public void UpdateBorrowing(BorrowingHistory borrowing) =>
            Update(borrowing);

    public async Task<BorrowingHistory> GetBorrowingHistoryAsync(Guid studentId, Guid bookId)
    {
        return await _context.BorrowingHistories
            .Where(bh => bh.BookId == bookId &&
                   bh.StudentId == studentId)
            .FirstOrDefaultAsync();
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

    public async Task<int> SaveChangesAsync() =>
           await CompleteAsync();
}
