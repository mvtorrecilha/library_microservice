using Grpc.Core;
using GrpcBook;
using Library.Book.Application.Commands.RequestModels;
using Library.Book.Application.Commands.ResponseModels;
using Library.Infra.ResponseFormatter.Common;
using MediatR;
using static GrpcBook.BookGrpc;

namespace Library.Book.API.Grpc;

public class BookService : BookGrpcBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookService> _logger;
    private readonly INotifier _notifier;

    public BookService(
        IMediator mediator,
        ILogger<BookService> logger,
       INotifier notifier)
    {
        _mediator = mediator;
        _logger = logger;
        _notifier = notifier;
    }

    public override async Task<ValidateBookResponse> ValidateBook(ValidateBookRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Begin grpc call from method {Method} for book id {Id}", context.Method, request.BookId);
        var response = await _mediator.Send(
            new ValidateBookCommand() { BookId = Guid.Parse(request.BookId), CourseId = Guid.Parse(request.CourseId)});

        if (_notifier.HasError)
        {
            throw GrpcCommon.GetRpcException(_notifier.StatusCode, _notifier.Errors);
        }

        return MapToValidateBookResponse(response);
    }

    private ValidateBookResponse MapToValidateBookResponse(ValidateBookCommandResponse bookResponse)
    {
        var map = new ValidateBookResponse
        {
            IsValid = bookResponse.isValid
        };

        return map;
    }
}
