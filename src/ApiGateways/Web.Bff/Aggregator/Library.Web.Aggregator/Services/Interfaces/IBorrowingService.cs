using Library.Web.Aggregator.Models.Borrowing;

namespace Library.Web.Aggregator.Services.Interfaces;

public interface IBorrowingService
{
    Task BorrowBookAsync(BorrowingBookRequest borrowingBookRequest);
}
