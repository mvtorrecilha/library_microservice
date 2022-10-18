using Library.Borrowing.Domain.Entities;

namespace Library.Borrowing.Domain.Repositories;

public interface IBorrowingRepository
{
    Task<IEnumerable<BorrowingHistory>> GetAllBorrowingHistoryAsync();

    Task AddBorrowingAsync(BorrowingHistory borrowing);

    void UpdateBorrowing(BorrowingHistory borrowing);

    Task<bool> IsValidToBorrow(Guid bookId);

    int SaveChanges();
}
