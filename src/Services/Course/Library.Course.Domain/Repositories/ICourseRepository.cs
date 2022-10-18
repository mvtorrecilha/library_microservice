using Library.Course.Domain.Entities;

namespace Library.Course.Domain.Repositories;

public interface ICourseRepository
{
    Task<IEnumerable<CourseItem>> GetAllCoursesAsync();
}
