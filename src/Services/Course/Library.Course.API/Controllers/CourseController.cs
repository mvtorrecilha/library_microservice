using Library.Course.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Course.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    public CourseController()
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/courses")]
    public async Task<ActionResult<IEnumerable<CourseItem>>> GetAllCourses()
    {
        return null;
    }
}
