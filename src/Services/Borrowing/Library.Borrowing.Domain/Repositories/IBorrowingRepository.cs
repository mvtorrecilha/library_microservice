using Library.Borrowing.Domain.Entities;

namespace Library.Borrowing.Domain.Repositories;

public interface IBorrowingRepository
{
    Task<IEnumerable<BorrowingHistory>> GetAllBorrowingHistoryAsync();

    Task AddBorrowingAsync(BorrowingHistory borrowing);

    void UpdateBorrowing(BorrowingHistory borrowing);

    Task<BorrowingHistory> GetBorrowingHistoryAsync(Guid studentId, Guid bookId);

    Task<bool> IsValidToBorrow(Guid bookId);

    Task<int> SaveChangesAsync();
}
