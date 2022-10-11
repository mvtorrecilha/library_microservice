using Library.Book.Domain.Entities.Base;

namespace Library.Book.Domain.Entities;

public class Category : BaseEntity
{
    public string? Name { get; set; }

    public ICollection<BookItem>? Books { get; set; }
}