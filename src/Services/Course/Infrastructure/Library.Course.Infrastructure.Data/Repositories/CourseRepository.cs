using Library.Course.Domain.Entities;
using Library.Course.Domain.Repositories;
using Library.Course.Infrastructure.Data.Context;
using Library.Infra.Repository;

namespace Library.Course.Infrastructure.Data.Repositories;

public class CourseRepository : RepositoryBase<CourseContext, CourseItem>, ICourseRepository
{
    public CourseRepository(CourseContext courseContext)
            : base(courseContext)
    {
    }

    public async Task<IEnumerable<CourseItem>> GetAllCoursesAsync() =>
            await GetAllAsync();
}
