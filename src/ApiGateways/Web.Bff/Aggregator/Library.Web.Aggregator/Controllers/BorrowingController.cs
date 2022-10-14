using Library.Adapter.ResponseFormatter;
using Library.Web.Aggregator.Models.Borrowing;
using Library.Web.Aggregator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Aggregator.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BorrowingController : ControllerBase
{
    private readonly IBorrowingService _borrowingService;
    private readonly IResponseFormatterResult _responseFormatter;

    public BorrowingController(
        IBorrowingService borrowingService,
        IResponseFormatterResult responseFormatter
        )
    {
        _borrowingService = borrowingService;
        _responseFormatter = responseFormatter;
    }

    [HttpPost]
    [Route("/api/v1/borrowing")]
    public async Task<ActionResult> BorrowBookAsync(BorrowingBookRequest borrowingBookRequest)
    {
        await _borrowingService.BorrowBookAsync(borrowingBookRequest);
        return _responseFormatter.Format();
    }
}
