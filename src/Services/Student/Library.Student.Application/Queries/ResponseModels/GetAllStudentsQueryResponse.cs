using Library.Student.Domain.Entities;

namespace Library.Student.Application.Queries.ResponseModels;

public class GetAllStudentsQueryResponse
{
    public List<StudentItem> Students { get; set; }
}
