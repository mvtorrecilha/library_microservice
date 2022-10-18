using Library.Borrowing.Domain.Entities;

namespace Library.Borrowing.Domain.Repositories;

public interface IBorrowingRepository
{
    Task<IEnumerable<BorrowingHistory>> GetAllAsync();

    Task<bool> IsValidToBorrow(Guid bookId);

    Task AddAsync(BorrowingHistory borrowing);

    void Update(BorrowingHistory borrowing);

    int Complete();
}
