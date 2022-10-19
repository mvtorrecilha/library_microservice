using Grpc.Core;
using GrpcBook;
using GrpcStudent;
using Library.Domain.Core.Bus;
using Library.Infra.ResponseFormatter.Common;
using Library.Web.Aggregator.Behaviors;
using Library.Web.Aggregator.IntegrationEvents.Events;
using Library.Web.Aggregator.Models.Book;
using Library.Web.Aggregator.Services.Interfaces;
using System.Net;

namespace Library.Web.Aggregator.Services;

public class BookService : IBookService
{
    private readonly StudentGrpc.StudentGrpcClient _grpcStudentClient;
    private readonly BookGrpc.BookGrpcClient _grpcBookClient;
    private readonly IApplicationEventBus _eventBus;
    private readonly ILogger<BookService> _logger;
    private readonly INotifier _notifier;

    public BookService(
        StudentGrpc.StudentGrpcClient grpcStudentClient,
        BookGrpc.BookGrpcClient grpcBookClient,
        IApplicationEventBus eventBus,
        ILogger<BookService> logger,
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
        if (borrowingBookRequest is null)
        {
            return;
        }

        if (!await IsValidBookBorrowingRequest(borrowingBookRequest.StudentId, borrowingBookRequest.BookId))
        {
            return;
        }

        var eventMessage = new BookBorrowingAcceptedIntegrationEvent(borrowingBookRequest.StudentId, borrowingBookRequest.BookId);

        try
        {
            await _eventBus.PublishEvent(eventMessage);
        }
        catch (Exception e)
        {
            _notifier.AddError("500", e.Message, null);
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

            if (string.IsNullOrEmpty(student.Id))
            {
                _notifier.AddError("Email", ErrorBehavior.StudentNotFound, "");
                _notifier.SetStatuCode(HttpStatusCode.NotFound);
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
            _notifier.AddError(e.StatusCode.ToString(), e.Message, null);
            return false;
        }
    }
}
