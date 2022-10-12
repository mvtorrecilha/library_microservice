using Grpc.Core;
using GrpcBook;
using GrpcStudent;
using Library.Adapter.ResponseFormatter.Common;
using Library.Web.Aggregator.Models.Borrowing;
using Library.Web.Aggregator.Services.Interfaces;

namespace Library.Web.Aggregator.Services;

public class BorrowingService : IBorrowingService
{
    private readonly StudentGrpc.StudentGrpcClient _grpcStudentClient;
    private readonly BookGrpc.BookGrpcClient _grpcBookClient;
    private readonly ILogger<BorrowingService> _logger;
    private readonly INotifier _notifier;

    public BorrowingService(
        StudentGrpc.StudentGrpcClient grpcStudentClient,
        BookGrpc.BookGrpcClient grpcBookClient,
        ILogger<BorrowingService> logger,
        INotifier notifier)
    {
        _grpcStudentClient = grpcStudentClient;
        _grpcBookClient = grpcBookClient;
        _notifier = notifier;
        _logger = logger;
    }

    public async Task BorrowBookAsync(BorrowingBookRequest borrowingBookRequest)
    {
        if (borrowingBookRequest is null)
        {
            return;
        }

        try
        {
            var studentRequest = new GetStudentWithCourseByIdRequest
            {
                Id = borrowingBookRequest.StudentId.ToString()
            };

            var student = await _grpcStudentClient.GetStudentWithCourseByIdAsync(studentRequest);

            if (student is null)
            {
                return;
            }

            var bookRequest = new ValidateBookRequest
            {
                BookId = borrowingBookRequest.BookId.ToString(),
                CourseId = student.CourseId.ToString()
            };

            var book = await _grpcBookClient.ValidateBookAsync(bookRequest);

            if (!book.IsValid)
            {
                return;
            }

            // await _grpcBorrowClient.BorrowBookAsync(borrowBookRequest);

        }
        catch (RpcException e)
        {
            throw e;
        }
    }
}
