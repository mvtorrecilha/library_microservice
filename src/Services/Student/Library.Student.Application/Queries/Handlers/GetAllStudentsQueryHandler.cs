using Library.Student.Application.Queries.RequestModels;
using Library.Student.Application.Queries.ResponseModels;
using Library.Student.Domain.Repositories;
using MediatR;

namespace Library.Student.Application.Queries.Handlers;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, GetAllStudentsQueryResponse>
{
    private readonly IStudentRepository _studentRepository;

    public GetAllStudentsQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<GetAllStudentsQueryResponse> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentRepository.GetAllStudentAsync();

        return new GetAllStudentsQueryResponse
        {
            Students = students.ToList()
        };
    }
}
