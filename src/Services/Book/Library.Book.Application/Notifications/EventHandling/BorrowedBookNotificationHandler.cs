using MediatR;

namespace Library.Book.Application.Notifications.EventHandling;

public class BorrowedBookNotificationHandler : INotificationHandler<BorrowedBookNotification>
{
    public Task Handle(BorrowedBookNotification notification, CancellationToken cancellationToken)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"Borrowed a book. Book and student id: '{notification.BookId} - {notification.StudentId}'");
        });
    }
}
