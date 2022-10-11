using Library.Student.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.Student.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    public StudentController()
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("/api/students")]
    public async Task<ActionResult<IEnumerable<StudentItem>>> GetAllStudents()
    {
        return null;
    }
}
