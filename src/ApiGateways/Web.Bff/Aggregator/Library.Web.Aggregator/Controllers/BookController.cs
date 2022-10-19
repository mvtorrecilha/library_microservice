using Library.Infra.ResponseFormatter;
using Library.Web.Aggregator.Models.Book;
using Library.Web.Aggregator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Aggregator.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IResponseFormatterResult _responseFormatter;

    public BookController(
       IBookService bookService,
        IResponseFormatterResult responseFormatter
        )
    {
        _bookService = bookService;
        _responseFormatter = responseFormatter;
    }

    [HttpPost]
    [Route("/api/v1/book/borrow-book")]
    public async Task<ActionResult> BorrowBookAsync(BorrowingBookRequest borrowingBookRequest)
    {
        await _bookService.BorrowBookAsync(borrowingBookRequest);
        return _responseFormatter.Format();
    }
}
