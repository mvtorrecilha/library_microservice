using Grpc.Core;
using GrpcStudent;
using Library.Student.Application.Queries.RequestModels;
using Library.Student.Application.Queries.ResponseModels;
using MediatR;
using static GrpcStudent.StudentGrpc;

namespace Library.Student.API.Grpc;

public class StudentService : StudentGrpcBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<StudentService> _logger;

    public StudentService(IMediator mediator, ILogger<StudentService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public override async Task<GetStudentWithCourseByIdResponse> GetStudentWithCourseByIdAsync(GetStudentWithCourseByIdRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Begin grpc call from method {Method} for student id {Id}", context.Method, request.Id);
        var response = await _mediator.Send(new GetStudentWithCourseByIdQuery() { StudentId = Guid.Parse(request.Id)});

        return MapToGetStudentWithCourseByIdResponse(response);
    }

    private GetStudentWithCourseByIdResponse MapToGetStudentWithCourseByIdResponse(GetStudentWithCourseByIdQueryResponse studentResponse)
    {
        if (studentResponse == null)
        {
            return null;
        }

        var map = new GetStudentWithCourseByIdResponse
        {
            Id = studentResponse.StudentId.ToString(),
            CourseId = studentResponse.CourseId.ToString()
        };

        return map;
    }
}
