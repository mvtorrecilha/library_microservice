using Library.Course.Domain.Entities;

namespace Library.Course.Application.Queries.ResponseModels;

public class GetAllCoursesQueryResponse
{
    public List<CourseItem> Courses { get; set; }
}
