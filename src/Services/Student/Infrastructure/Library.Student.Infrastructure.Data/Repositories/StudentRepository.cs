using Library.Student.Domain.Entities;
using Library.Student.Domain.Repositories;
using Library.Student.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Student.Infrastructure.Data.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly StudentContext _context;

    public StudentRepository(StudentContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentItem>> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<StudentItem> GetByIdAsync(Guid Id)
    {
        return await _context.Students.FindAsync(Id);
    }
}
