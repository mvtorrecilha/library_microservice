namespace Library.Borrowing.Domain.Entities;

public class BorrowingHistory
{
    public Guid Id { get; set; }

    public Guid BookId { get; set; }

    public Guid StudentId { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime? ReturnDate { get; set; }
}
