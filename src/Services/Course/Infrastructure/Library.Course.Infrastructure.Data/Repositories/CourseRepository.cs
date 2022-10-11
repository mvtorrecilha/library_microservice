using Library.Course.Domain.Entities;
using Library.Course.Domain.Repositories;
using Library.Course.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Course.Infrastructure.Data.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly CourseContext _context;

    public CourseRepository(CourseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CourseItem>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<CourseItem> GetByIdAsync(Guid Id)
    {
        return await _context.Courses.FindAsync(Id);
    }
}
