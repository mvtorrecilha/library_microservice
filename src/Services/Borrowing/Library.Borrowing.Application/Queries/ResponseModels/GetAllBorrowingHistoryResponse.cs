using Library.Borrowing.Domain.Entities;

namespace Library.Borrowing.Application.Queries.ResponseModels;

public class GetAllBorrowingHistoryResponse
{
    public List<BorrowingHistory> BorrowingHistories { get; set; }
}
