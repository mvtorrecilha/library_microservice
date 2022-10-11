using Library.Book.Domain.Entities.Base;

namespace Library.Book.Domain.Entities;

public class BookItem : BaseEntity
{
    public string Title { get; set; }

    public string Author { get; set; }

    public int Pages { get; set; }

    public string Publisher { get; set; }

    public Guid CourseId { get; set; }

    public Guid CategoryId { get; set; }

    public Category? Category { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsAvailable { get; set; }
}
