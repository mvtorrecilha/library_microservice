using Grpc.Core;
using GrpcBook;
using GrpcStudent;
using Library.Domain.Core.Bus;
using Library.Infra.ResponseFormatter.Common;
using Library.Web.Aggregator.IntegrationEvents.Events;
using Library.Web.Aggregator.Models.Borrowing;
using Library.Web.Aggregator.Services.Interfaces;

namespace Library.Web.Aggregator.Services;

public class BorrowingService : IBorrowingService
{
    private readonly StudentGrpc.StudentGrpcClient _grpcStudentClient;
    private readonly IApplicationEventBus _eventBus;
    private readonly BookGrpc.BookGrpcClient _grpcBookClient;
    private readonly ILogger<BorrowingService> _logger;
    private readonly INotifier _notifier;

    public BorrowingService(
        StudentGrpc.StudentGrpcClient grpcStudentClient,
        BookGrpc.BookGrpcClient grpcBookClient,
        IApplicationEventBus eventBus,
        ILogger<BorrowingService> logger,
        INotifier notifier)
    {
        _grpcStudentClient = grpcStudentClient;
        _grpcBookClient = grpcBookClient;
        _eventBus = eventBus;
        _notifier = notifier;
        _logger = logger;
    }

    public async Task BorrowBookAsync(BorrowingBookRequest borrowingBookRequest)
    {
        if(borrowingBookRequest is null)
        {
            return;
        }

        if(! await IsValidBookBorrowingRequest(borrowingBookRequest.BookId, borrowingBookRequest.StudentId))
        {
            return;
        }

        var eventMessage = new BookBorrowingAcceptedIntegrationEvent(borrowingBookRequest.BookId, borrowingBookRequest.StudentId);

        try
        {
           await _eventBus.PublishEvent(eventMessage);
        }
        catch (Exception e)
        {
            throw e;
        }  
    }

    private async Task<bool> IsValidBookBorrowingRequest(Guid studentId, Guid bookId)
    {
        try
        {
            var studentRequest = new GetStudentWithCourseByIdRequest
            {
                Id = studentId.ToString()
            };

            var student = await _grpcStudentClient.GetStudentWithCourseByIdAsync(studentRequest);

            if (student is null)
            {
                return false;
            }

            var bookRequest = new ValidateBookRequest
            {
                BookId = bookId.ToString(),
                CourseId = student.CourseId.ToString()
            };

            var book = await _grpcBookClient.ValidateBookAsync(bookRequest);

            if (!book.IsValid)
            {
                return false;
            }

            return true;
        }
        catch (RpcException e)
        {
            throw e;
        }
        
    }
}
