using Library.Student.Application.Queries.RequestModels;
using Library.Student.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Student.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;
    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/v1/students")]
    public async Task<ActionResult<IEnumerable<StudentItem>>> GetAllStudents()
    {
       var studentsResponse =  await _mediator.Send(new GetAllStudentsQuery());

        return Ok(studentsResponse.Students);
    }
}
