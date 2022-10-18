using Library.Student.Application.Queries.RequestModels;
using Library.Student.Application.Queries.ResponseModels;
using Library.Student.Domain.Repositories;
using MediatR;

namespace Library.Student.Application.Queries.Handlers;

public class GetStudentWithCourseByIdQueryHandler : IRequestHandler<GetStudentWithCourseByIdQuery, GetStudentWithCourseByIdQueryResponse>
{
    private readonly IStudentRepository _studentRepository;

    public GetStudentWithCourseByIdQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<GetStudentWithCourseByIdQueryResponse> Handle(GetStudentWithCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.StudentId);

        if (student is null)
        {
            return null;
        }

        return new GetStudentWithCourseByIdQueryResponse
        {
            StudentId = student.Id,
            CourseId = student.CourseId
        };
    }
}
