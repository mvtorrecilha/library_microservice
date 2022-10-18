using Library.Borrowing.Application.Queries.RequestModels;
using Library.Borrowing.Application.Queries.ResponseModels;
using Library.Borrowing.Domain.Repositories;
using MediatR;

namespace Library.Borrowing.Application.Queries.Handlers;

public class GetAllBorrowingHistoryQueryHandler : IRequestHandler<GetAllBorrowingHistoryQuery, GetAllBorrowingHistoryResponse>
{
    private readonly IBorrowingRepository _borrowingRepository;

    public GetAllBorrowingHistoryQueryHandler(IBorrowingRepository borrowingRepository)
    {
        _borrowingRepository = borrowingRepository;
    }

    public async Task<GetAllBorrowingHistoryResponse> Handle(GetAllBorrowingHistoryQuery request, CancellationToken cancellationToken)
    {
        var histories = await _borrowingRepository.GetAllBorrowingHistoryAsync();

        return new GetAllBorrowingHistoryResponse
        {
            BorrowingHistories = histories.ToList()
        };
    }
}
