using System.ComponentModel.DataAnnotations;

namespace Library.Web.Aggregator.Models.Book;

public class BorrowingBookRequest
{
    [Required]
    public Guid BookId { get; set; }

    [Required]
    public Guid StudentId { get; set; }
}
