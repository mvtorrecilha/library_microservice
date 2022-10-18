namespace Library.Book.API.Models;

public record ReturnBookRequest
{
    public Guid BookId { get; set; }

    public Guid StudentId { get; set; }
}
