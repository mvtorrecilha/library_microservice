namespace Library.Student.Application.Queries.ResponseModels;

public record GetStudentWithCourseByIdQueryResponse
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
}
