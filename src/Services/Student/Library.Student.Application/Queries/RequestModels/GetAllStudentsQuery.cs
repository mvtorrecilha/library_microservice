using Library.Student.Application.Queries.ResponseModels;
using MediatR;

namespace Library.Student.Application.Queries.RequestModels;

public record GetAllStudentsQuery : IRequest<GetAllStudentsQueryResponse>
{
}
