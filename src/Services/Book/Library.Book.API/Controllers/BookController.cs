using Library.Book.API.Models;
using Library.Book.Application.Commands.RequestModels;
using Library.Book.Application.Queries.RequestModels;
using Library.Book.Domain.Entities;
using Library.Infra.ResponseFormatter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Book.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IResponseFormatterResult _responseFormatter;
    public BookController(
        IMediator mediator,
         IResponseFormatterResult responseFormatter)
    {
        _mediator = mediator;
        _responseFormatter = responseFormatter;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/v1/books")]
    public async Task<ActionResult<IEnumerable<BookItem>>> GetAllBooksAsyc()
    {
        var booksResponse = await _mediator.Send(new GetAllBooksQuery());

        return Ok(booksResponse.Books);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/v1/book/return-book")]
    public async Task<ActionResult<IEnumerable<BookItem>>> ReturnBookAsync(ReturnBookRequest returnBookRequest)
    {
        await _mediator
            .Send(new ReturnBookCommand() { BookId = returnBookRequest.BookId, StudentId = returnBookRequest.StudentId });

        return _responseFormatter.Format();
    }
}
