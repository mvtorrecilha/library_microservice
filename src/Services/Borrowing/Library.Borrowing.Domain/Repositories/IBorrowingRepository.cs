using Library.Borrowing.Domain.Entities;

namespace Library.Borrowing.Domain.Repositories;

public interface IBorrowingRepository
{
    Task<IEnumerable<BorrowingHistory>> GetAllAsync();

    Task<bool> IsValidToBorrow(Guid bookId);
}
