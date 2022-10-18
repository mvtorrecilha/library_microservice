using Library.Student.Domain.Entities;

namespace Library.Student.Domain.Repositories;

public interface IStudentRepository
{
    Task<StudentItem> GetStudentByIdAsync(Guid Id);

    Task<IEnumerable<StudentItem>> GetAllStudentAsync();
}
