using Library.Course.Application.Queries.ResponseModels;
using MediatR;

namespace Library.Course.Application.Queries.RequestModels;

public record GetAllCoursesQuery : IRequest<GetAllCoursesQueryResponse>
{
}
