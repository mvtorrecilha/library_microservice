namespace Library.Student.Domain.Entities;

public class StudentItem
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public Guid CourseId { get; set; }
}
