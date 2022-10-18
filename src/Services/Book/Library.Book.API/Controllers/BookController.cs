using Library.Book.Application.Commands.RequestModels;
using Library.Book.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Book.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/v1/books")]
    public async Task<ActionResult<IEnumerable<BookItem>>> GetAllBooks()
    {
        var booksResponse = await _mediator.Send(new GetAllBooksQuery());

        return Ok(booksResponse.Books);
    }
}
