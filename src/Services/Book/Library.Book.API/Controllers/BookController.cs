using Library.Book.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Book.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    public BookController()
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/books")]
    public async Task<ActionResult<IEnumerable<BookItem>>> GetAllBooks()
    {
        return null;
    }
}
