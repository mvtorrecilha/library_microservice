using MediatR;

namespace Library.Book.Application.Notifications;

public class BorrowedBookNotification : INotification
{
    public Guid BookId { get; set; }
    public Guid StudentId { get; set; }
}
