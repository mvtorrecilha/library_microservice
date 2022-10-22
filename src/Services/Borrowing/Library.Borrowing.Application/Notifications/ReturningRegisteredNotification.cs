using MediatR;

namespace Library.Borrowing.Application.Notifications;

public class ReturningRegisteredNotification : INotification
{
    public Guid BookId { get; set; }
    public Guid StudentId { get; set; }
}
