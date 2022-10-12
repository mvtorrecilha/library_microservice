using System.ComponentModel.DataAnnotations;

namespace Library.Web.Aggregator.Models.Borrowing;

public class BorrowingBookRequest
{
    [Required]
    public Guid BookId { get; set; }

    [Required]
    public Guid StudentId { get; set; }
}
