using Library.Infra.Repository;
using Library.Student.Domain.Entities;
using Library.Student.Domain.Repositories;
using Library.Student.Infrastructure.Data.Context;
using System.Collections.Generic;

namespace Library.Student.Infrastructure.Data.Repositories;

public class StudentRepository : RepositoryBase<StudentContext, StudentItem>, IStudentRepository
{
    public StudentRepository(StudentContext studentContext)
            : base(studentContext)
    {
    }

    public async Task<StudentItem> GetStudentByIdAsync(Guid Id) =>
            await GetByIdAsync(Id);

    public async Task<IEnumerable<StudentItem>> GetAllStudentAsync() =>
            await GetAllAsync();
}
