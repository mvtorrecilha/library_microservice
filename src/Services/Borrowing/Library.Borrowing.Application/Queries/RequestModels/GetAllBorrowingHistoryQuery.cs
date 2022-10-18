using Library.Borrowing.Application.Queries.ResponseModels;
using MediatR;

namespace Library.Borrowing.Application.Queries.RequestModels;

public record GetAllBorrowingHistoryQuery : IRequest<GetAllBorrowingHistoryResponse>
{
}
