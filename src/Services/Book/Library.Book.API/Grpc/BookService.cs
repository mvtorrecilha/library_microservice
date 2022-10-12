using Grpc.Core;
using GrpcBook;
using Library.Book.Application.Commands.RequestModels;
using Library.Book.Application.Commands.ResponseModels;
using MediatR;
using static GrpcBook.BookGrpc;

namespace Library.Book.API.Grpc;

public class BookService : BookGrpcBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookService> _logger;

    public BookService(IMediator mediator, ILogger<BookService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public override async Task<ValidateBookResponse> ValidateBook(ValidateBookRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Begin grpc call from method {Method} for book id {Id}", context.Method, request.BookId);
        var response = await _mediator.Send(
            new ValidateBookCommand() { BookId = Guid.Parse(request.BookId), CourseId = Guid.Parse(request.BookId)});

        return MapToGetStudentWithCourseByIdResponse(response);
    }

    private ValidateBookResponse MapToGetStudentWithCourseByIdResponse(ValidateBookCommandResponse bookResponse)
    {
        if (bookResponse == null)
        {
            return null;
        }

        var map = new ValidateBookResponse
        {
            IsValid = bookResponse.isValid
        };

        return map;
    }
}
