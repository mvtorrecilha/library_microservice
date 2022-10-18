using Library.Course.Application.Queries.RequestModels;
using Library.Course.Application.Queries.ResponseModels;
using Library.Course.Domain.Repositories;
using MediatR;

namespace Library.Course.Application.Queries.Handlers;

public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, GetAllCoursesQueryResponse>
{
    private readonly ICourseRepository _courseRepository;

    public GetAllCoursesQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<GetAllCoursesQueryResponse> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllCoursesAsync();

        return new GetAllCoursesQueryResponse
        {
            Courses = courses.ToList()
        };
    }
}
