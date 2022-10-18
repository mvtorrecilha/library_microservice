using Library.Course.Application.Queries.RequestModels;
using Library.Course.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Library.Course.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;
    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/v1/courses")]
    public async Task<ActionResult<IEnumerable<CourseItem>>> GetAllCourses()
    {
        var coursesResponse = await _mediator.Send(new GetAllCoursesQuery());

        return Ok(coursesResponse.Courses);
    }
}
