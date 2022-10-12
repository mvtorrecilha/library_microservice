using Library.Student.Application.Queries.ResponseModels;
using MediatR;

namespace Library.Student.Application.Queries.RequestModels;

public record GetStudentWithCourseByIdQuery : IRequest<GetStudentWithCourseByIdQueryResponse>
{
    public Guid StudentId { get; set; }
}
