using Library.Borrowing.Application.Queries.RequestModels;
using Library.Borrowing.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Borrowing.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BorrowingController : ControllerBase
{
    private readonly IMediator _mediator;
    public BorrowingController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/v1/borrowing-history")]
    public async Task<ActionResult<IEnumerable<BorrowingHistory>>> GetAllBorrowingHistory()
    {
        var borrowingResponse = await _mediator.Send(new GetAllBorrowingHistoryQuery());

        return Ok(borrowingResponse.BorrowingHistories);
    }
}
